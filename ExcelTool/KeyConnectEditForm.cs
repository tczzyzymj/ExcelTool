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
        private KeyData mFromKey = null;
        private TableBaseData? mSelectTargetTable = null;
        private CommonWorkSheetData? mSelectSheet = null;
        private const int mColumIndexForConnectInfo = 2;
        private const int mColumIndexForEditConnect = 3;
        private const int mColumIndexForSetConnect = 4;

        private List<KeyData>? mWorkSheetKeyListData = null;

        public KeyConnectEditForm()
        {
            InitializeComponent();
            this.DataViewForKeyConfig.AllowUserToAddRows = false;
        }

        public bool InitData(KeyData targetKey)
        {
            if (targetKey == null)
            {
                MessageBox.Show("传入的 KeyData 为空，请检查！", "错误");
                this.Close();
                return false;
            }

            var fromData = targetKey.GetOwnerTable();
            if (fromData == null)
            {
                MessageBox.Show("传入的 ExcelFileBase 为空，请检查！", "错误");
                this.Close();
                return false;
            }

            mFromKey = targetKey;
            return true;
        }

        private void KeyConnectEditForm_Load(object sender, EventArgs e)
        {
            LabelForFromTable.Text = $"{mFromKey.GetOwnerTableName(false)}--Key:{mFromKey.GetKeyName()}";

            // 这里先默认选择一下加载的源数据文件
            mSelectTargetTable = TableDataManager.Instance().GetSourceFileData();

            // 这里为 file combobox 的已加载文件做显示
            {
                ComboBoxForLoadedFile.BeginUpdate();
                var _dataList = TableDataManager.Instance().GetTableDataList();

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

        private void DataViewForKeyConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case mColumIndexForEditConnect:
                {
                    if (mWorkSheetKeyListData == null)
                    {
                        return;
                    }

                    KeyConnectEditForm _newForm = new KeyConnectEditForm();
                    var _targetKey = mWorkSheetKeyListData[e.RowIndex];
                    _newForm.InitData(_targetKey);
                    if (_newForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!CommonUtil.IsSafeNoCycleReferenceForKey(mWorkSheetKeyListData[e.RowIndex]))
                        {
                            _targetKey.ClearNextConnectKey();
                        }
                        else
                        {
                            var _cell = DataViewForKeyConfig.Rows[e.RowIndex].Cells[mColumIndexForConnectInfo];
                            _cell.Value = _targetKey.GetConnectInfo();
                            this.DataViewForKeyConfig.UpdateCellValue(mColumIndexForConnectInfo, e.RowIndex);
                        }
                    }
                    break;
                }
                case mColumIndexForSetConnect:
                {
                    if (mWorkSheetKeyListData == null)
                    {
                        return;
                    }

                    // 设置为了关联的 key , 这里要注意，不是多选，是单选的，如果选中了一个，那么其他的要取消选择
                    var _allRows = this.DataViewForKeyConfig.Rows;
                    var _checkBoxCell = (DataGridViewCheckBoxCell)_allRows[e.RowIndex].Cells[e.ColumnIndex];

                    var _triggerKey = mWorkSheetKeyListData[e.RowIndex];

                    bool _isSelect = (bool)_checkBoxCell.EditedFormattedValue;
                    if (_isSelect)
                    {
                        mFromKey.SetNextConnectKey(new WeakReference<KeyData>(_triggerKey));
                        for (int _row = 0; _row < _allRows.Count; ++_row)
                        {
                            if (_row == e.RowIndex)
                            {
                                continue;
                            }

                            var _cell = (DataGridViewCheckBoxCell)_allRows[_row].Cells[mColumIndexForSetConnect];
                            _cell.Value = false;
                        }
                    }
                    else
                    {
                        mFromKey.ClearNextConnectKey();
                    }

                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void DataViewForKeyConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataViewForKeyConfig_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnLoadNewFile_Click(object sender, EventArgs e)
        {
            ChooseFileConfigForm _form = new ChooseFileConfigForm();
            _form.SetInitData(LoadFileType.NormalFile);
            _form.ShowDialog();

            var _index = TableDataManager.Instance().TryGetTableIndexByPath(_form.LastChooseFileAbsolutePath);
            if (_index >= 0)
            {
                ComboBoxForLoadedFile.SelectedIndex = _index;
            }

            this.ComboBoxForLoadedFile.Update();
        }

        private void ComboBoxForLoadedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _dataList = TableDataManager.Instance().GetTableDataList();
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
            this.DataViewForKeyConfig.Rows.Clear();
            var _sheetIndex = this.ComboBoxForWorkSheet.SelectedIndex;
            var _keyList = mSelectSheet?.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                MessageBox.Show($"当前选择的sheet:[{_sheetIndex}] 无法获取 Key 数据，请检查！", "错误");
                return false;
            }

            if (mFromKey == null)
            {
                return false;
            }

            for (int i = 0; i < _keyList.Count; i++)
            {
                var _key = _keyList[i];
                this.DataViewForKeyConfig.Rows.Add(
                    CommonUtil.GetZM(_key.GetKeyIndexForShow()),
                    _key.GetKeyName(),
                    CommonUtil.GetKeyConnectFullInfo(_key),
                    "编辑",
                    mFromKey.GetNextConnectKey() == _key
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
    }
}
