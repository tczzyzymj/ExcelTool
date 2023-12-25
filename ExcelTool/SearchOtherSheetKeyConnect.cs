using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class SearchOtherSheetKeyConnect : Form
    {
        private DataProcessActionForFindRowDataInOtherSheet mFromAction = null;
        private FileDataBase? mSelectTargetTable = null;
        public CommonWorkSheetData SelectSheet = null;
        private List<KeyData> mWorkSheetKeyListData = new List<KeyData>();

        public SearchOtherSheetKeyConnect()
        {
            InitializeComponent();
        }

        public bool InitData(DataProcessActionForFindRowDataInOtherSheet targetAction)
        {
            if (targetAction == null)
            {
                MessageBox.Show("传入的 KeyData 为空，请检查！", "错误");
                this.Close();
                return false;
            }

            mFromAction = targetAction;
            return true;
        }

        private bool InternalRefreshForKey()
        {
            this.DataViewForKeyList.Rows.Clear();
            var _sheetIndex = this.ComboBoxForWorkSheet.SelectedIndex;
            var _keyList = SelectSheet?.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                throw new Exception($"当前选择的sheet:[{_sheetIndex}] 无法获取 Key 数据，请检查！");
            }

            if (mFromAction == null)
            {
                return false;
            }

            for (int i = 0; i < _keyList.Count; i++)
            {
                var _key = _keyList[i];
                this.DataViewForKeyList.Rows.Add(
                    CommonUtil.GetZM(_key.GetKeyIndexForShow()),
                    _key.KeyName,
                    string.Empty,//CommonUtil.GetKeyConnectFullInfo(_key),
                    "编辑",
                    false//mFromKey.GetNextConnectKey() == _key
                );
            }

            mWorkSheetKeyListData = _keyList;

            return true;
        }

        private void SearchOtherSheetKeyConnect_Load(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                throw new Exception($"KeyConnectEditForm_Load 错误，mFromAction为空");
            }

            //LabelForFromTable.Text = $"关联：{mFromAction.SearchTargetSheet.DisplayName}--Key:{mFromAction.GetKeyName()}";

            // 这里先默认选择一下加载的源数据文件
            mSelectTargetTable = TableDataManager.Ins().GetSourceFileData();

            // 这里为 file combobox 的已加载文件做显示
            {
                ComboBoxForLoadedFile.BeginUpdate();
                var _dataList = TableDataManager.Ins().GetTableDataList();

                ComboBoxForLoadedFile.DataSource = _dataList;
                ComboBoxForLoadedFile.ValueMember = "DisplayIndex";
                ComboBoxForLoadedFile.DisplayMember = "DisplayName";
                if (_dataList != null && _dataList.Count > 0)
                {
                    ComboBoxForLoadedFile.SelectedIndex = 0;
                }

                ComboBoxForLoadedFile.EndUpdate();
            }

            this.InternalRefreshSheetComboBox();
        }

        private void InternalRefreshSheetComboBox()
        {
            if (mSelectTargetTable == null)
            {
                return;
            }
            var _sheetList = mSelectTargetTable.GetWorkSheetList();
            ComboBoxForWorkSheet.BeginUpdate();
            ComboBoxForWorkSheet.DataSource = _sheetList;
            ComboBoxForWorkSheet.ValueMember = "IndexInListForShow";
            ComboBoxForWorkSheet.DisplayMember = "DisplayName";
            ComboBoxForWorkSheet.SelectedIndex = 0;
            ComboBoxForWorkSheet.EndUpdate();
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            if (SelectSheet == null)
            {
                MessageBox.Show($"{BtnFinishConfig_Click} 错误，SelectSheet 为空");
                return;
            }

            if (SelectKeyList.Count != mFromAction.MatchKeyList.Count)
            {
                MessageBox.Show(
                    $"{BtnFinishConfig_Click} 错误，原操作的 key 数量：{mFromAction.MatchKeyList.Count} ，当前选择匹配 Key 数量：{SelectKeyList.Count} ；不一致，请重新选择"
                );

                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public List<KeyData> SelectKeyList = new List<KeyData>();
        private const int mColumIndexForSelect = 2;
        private void DataViewForKeyList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case mColumIndexForSelect:
                {
                    var _targetKey = this.mWorkSheetKeyListData[e.RowIndex];
                    var _cell = this.DataViewForKeyList.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    bool _isSelected = (bool)_cell.EditedFormattedValue;
                    if (_isSelected)
                    {
                        if (!SelectKeyList.Contains(_targetKey))
                        {
                            SelectKeyList.Add(_targetKey);
                        }
                    }
                    else
                    {
                        SelectKeyList.Remove(_targetKey);
                    }

                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void ComboBoxForWorkSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSheet = mSelectTargetTable?.GetWorkSheetByIndex(this.ComboBoxForWorkSheet.SelectedIndex);
            InternalRefreshForKey();
        }

        private void ComboBoxForLoadedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _dataList = TableDataManager.Ins().GetTableDataList();
            var _index = this.ComboBoxForLoadedFile.SelectedIndex;
            mSelectTargetTable = _dataList[_index];
            InternalRefreshSheetComboBox();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var _searchContent = TextBoxForSearch.Text.Trim().ToLower();

            var _rows = this.DataViewForKeyList.Rows;
            for (int i = 0; i < mWorkSheetKeyListData.Count; ++i)
            {
                if (mWorkSheetKeyListData[i].KeyName.ToLower().Contains(_searchContent))
                {
                    _rows[i].Visible = true;
                }
                else
                {
                    _rows[i].Visible = false;
                }
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            var _rows = this.DataViewForKeyList.Rows;
            for (int i = 0; i < _rows.Count; ++i)
            {
                _rows[i].Visible = true;
            }
        }

        private void BtnLoadNewFile_Click(object sender, EventArgs e)
        {
            ChooseFileConfigForm _form = new ChooseFileConfigForm();
            _form.SetInitData(LoadFileType.NormalFile);
            _form.ShowDialog();

            var _index = TableDataManager.Ins().TryGetTableIndexByPath(_form.LastChooseFileAbsolutePath);
            if (_index >= 0)
            {
                ComboBoxForLoadedFile.SelectedIndex = _index;
            }

            this.ComboBoxForLoadedFile.Update();
        }
    }
}
