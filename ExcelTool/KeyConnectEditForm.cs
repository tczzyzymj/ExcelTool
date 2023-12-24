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
    public partial class KeyConnectEditForm : FormBase
    {
        private DataProcessActionBase? mFromAction = null;
        private FileDataBase? mSelectTargetTable = null;
        private CommonWorkSheetData? mSelectSheet = null;
        private const int mColumIndexForConnectInfo = 2;
        private const int mColumIndexForEditConnect = 3;
        private const int mColumIndexForSetConnect = 4;

        private List<KeyData> mWorkSheetKeyListData = new List<KeyData>();

        public KeyConnectEditForm()
        {
            InitializeComponent();
            this.DataViewForKeyList.AllowUserToAddRows = false;
        }

        public bool InitData(DataProcessActionBase targetAction)
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

        private void KeyConnectEditForm_Load(object sender, EventArgs e)
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

        private List<KeyData> mSelectKeyList = new List<KeyData>();

        private const int mColumIndexForSelect = 2;
        private void DataViewForKeyConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        if (!mSelectKeyList.Contains(_targetKey))
                        {
                            mSelectKeyList.Add(_targetKey);
                        }
                    }
                    else
                    {
                        mSelectKeyList.Remove(_targetKey);
                    }

                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void InternalRefreshForSelctKeyBox()
        {

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

        private void ComboBoxForLoadedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _dataList = TableDataManager.Ins().GetTableDataList();
            var _index = this.ComboBoxForLoadedFile.SelectedIndex;
            mSelectTargetTable = _dataList[_index];
            InternalRefreshSheetComboBox();
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

        private void ComboBoxForWorkSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectSheet = mSelectTargetTable?.GetWorkSheetByIndex(this.ComboBoxForWorkSheet.SelectedIndex);
            InternalRefreshForKey();
        }

        private bool InternalRefreshForKey()
        {
            this.DataViewForKeyList.Rows.Clear();
            var _sheetIndex = this.ComboBoxForWorkSheet.SelectedIndex;
            var _keyList = mSelectSheet?.GetKeyListData();
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

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private const int mRemoveColumIndex = 5;// 移除按钮
        private const int mConnectActionBtnColumIndex = 3; // 关联其他表的行为按钮
        private const int mConnectKeyBtnColumIndex = 2; // 关联其他表的KEY按钮

        private void DataViewForAction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mFromAction == null)
            {
                throw new Exception($"DataViewForAction_CellContentClick 出错， mFromAction 为空，请检查");
            }

            switch (e.ColumnIndex)
            {
                case mConnectActionBtnColumIndex:
                {
                    //var _targetAction = mFromAction.ActionListAfterFindValues[e.RowIndex];
                    //if (_targetAction is DataProcessActionForFindRowData _finalAction)
                    //{
                    //    KeyConnectEditForm _form = new KeyConnectEditForm();
                    //    _form.InitData(_finalAction);
                    //    _form.ShowDialog();

                    //    // 都去刷新一下关联数据
                    //}

                    break;
                }
                case mRemoveColumIndex:
                {
                    //mFromAction.ActionListAfterFindValues.RemoveAt(e.RowIndex);
                    break;
                }
                case mConnectKeyBtnColumIndex:
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
    }
}
