using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class KeyConnectEditForm : Form
    {
        private DataProcessActionBase mFromAction = null;
        private FileDataBase? mSelectTargetTable = null;
        private CommonWorkSheetData? mSelectSheet = null;
        private const int mColumIndexForConnectInfo = 2;
        private const int mColumIndexForEditConnect = 3;
        private const int mColumIndexForSetConnect = 4;

        private List<KeyData> mWorkSheetKeyListData = new List<KeyData>();

        private List<CommonDataForClass> mDataClassList = new List<CommonDataForClass>();

        private CommonDataForClass mChooseActionType = null;

        public KeyConnectEditForm()
        {
            InitializeComponent();
            this.DataViewForKeyList.AllowUserToAddRows = false;
            DataViewForAction.AllowUserToAddRows = false;
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

            mDataClassList = CommonUtil.CreateComboBoxDataForType<DataProcessActionBase>();
            ComboBoxForActionTypeList.DataSource = null;
            ComboBoxForActionTypeList.Items.Clear();
            ComboBoxForActionTypeList.DataSource = mDataClassList;
            ComboBoxForActionTypeList.ValueMember = "Index";
            ComboBoxForActionTypeList.DisplayMember = "Name";

            //LabelForFromTable.Text = $"关联：{mFromAction.SearchTargetSheet.DisplayName}--Key:{mFromAction.GetKeyName()}";

            // 这里先默认选择一下加载的源数据文件
            mSelectTargetTable = TableDataManager.Ins().GetSourceFileData();
            InternalRefreshForActionDataView();
            InternalRefreshForLoadFiles();

            this.MultiDataSplitSymbol.Text = mFromAction.MultiValueSplitSymbol;

            this.InternalRefreshSheetComboBox();

            InternalRefreshComboBoxForSelectKey();
        }

        private void InternalRefreshForLoadFiles()
        {
            // 这里为 file combobox 的已加载文件做显示
            {
                ComboBoxForLoadedFile.DataSource = null;
                ComboBoxForLoadedFile.Items.Clear();
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

                    this.InternalRefreshComboBoxForSelectKey();

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
            InternalRefreshForLoadFiles();
            var _index = TableDataManager.Ins().TryGetTableIndexByPath(_form.LastChooseFileAbsolutePath);
            ComboBoxForLoadedFile.SelectedIndex = _index;
            this.ComboBoxForLoadedFile.Update();
        }

        private void ComboBoxForLoadedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            InternalRefreshSheetComboBox();
        }

        private void InternalRefreshSheetComboBox()
        {
            if (mSelectTargetTable == null)
            {
                return;
            }
            var _sheetList = mSelectTargetTable.GetWorkSheetList();
            if (_sheetList.Count > 0)
            {
                ComboBoxForWorkSheet.DataSource = null;
                ComboBoxForWorkSheet.Items.Clear();
                ComboBoxForWorkSheet.BeginUpdate();
                ComboBoxForWorkSheet.DataSource = _sheetList;
                ComboBoxForWorkSheet.ValueMember = "IndexInListForShow";
                ComboBoxForWorkSheet.DisplayMember = "DisplayName";
                ComboBoxForWorkSheet.SelectedIndex = 0;
                ComboBoxForWorkSheet.EndUpdate();
            }
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
                    false
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
        private const int mMoveUpColum = 6; // 上移按钮
        private const int mMoveDownColum = 7; // 下移按钮
        private const int mBindKeyColumIndex = 1; // 绑定的 key

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
                    var _action = this.mTargetActionList[e.RowIndex];
                    if (_action is DataProcessActionForFindRowDataInOtherSheet)
                    {
                        KeyConnectEditForm _newForm = new KeyConnectEditForm();
                        _newForm.InitData(_action);
                        _newForm.ShowDialog();
                    }

                    break;
                }
                case mRemoveColumIndex:
                {
                    mTargetActionList.RemoveAt(e.RowIndex);
                    InternalRefreshForActionDataView();
                    break;
                }
                case mConnectKeyBtnColumIndex:
                {
                    var _action = this.mTargetActionList[e.RowIndex];
                    if (_action is DataProcessActionForFindRowDataInOtherSheet _findAction)
                    {
                        SearchOtherSheetKeyConnect _newForm = new SearchOtherSheetKeyConnect();
                        _newForm.InitData(_findAction);
                        if (_newForm.ShowDialog() == DialogResult.OK)
                        {
                            _findAction.SearchKeyList = _newForm.SelectKeyList;
                            _findAction.SearchTargetSheet = _newForm.SelectSheet;
                        }
                    }

                    break;
                }
                case mMoveDownColum:
                {
                    // 点击了下移按钮
                    if (InternalSwapForActionList(e.RowIndex, false))
                    {
                        InternalRefreshForActionDataView();
                    }

                    break;
                }
                case mMoveUpColum:
                {
                    // 点击了上移按钮
                    if (InternalSwapForActionList(e.RowIndex, true))
                    {
                        InternalRefreshForActionDataView();
                    }

                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private bool InternalSwapForActionList(int chooseIndex, bool isUp)
        {
            if (this.mTargetActionList == null || mTargetActionList.Count < 2)
            {
                return false;
            }

            int _targetIndex = chooseIndex;
            if (isUp)
            {
                _targetIndex = _targetIndex - 1;
            }
            else
            {
                _targetIndex = _targetIndex + 1;
            }

            if (_targetIndex < 0 || _targetIndex >= mTargetActionList.Count)
            {
                return false;
            }

            var _tempData = mTargetActionList[chooseIndex];
            mTargetActionList[chooseIndex] = mTargetActionList[_targetIndex];
            mTargetActionList[_targetIndex] = _tempData;

            return true;
        }


        private void BtnAddAction_Click(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                MessageBox.Show($"{BtnAddAction_Click} 错误，mFromAction 为空");
                return;
            }

            if (mSelectKeyList.Count < 1)
            {
                return;
            }

            if (mChooseActionType == null)
            {
                MessageBox.Show($"{BtnAddAction_Click} 错误，mChooseActionType为空，请检查");
                return;
            }

            var _newClassIns = GetNewDataForChooseAction();
            if (_newClassIns == null)
            {
                return;
            }

            _newClassIns.MatchKeyList.AddRange(mSelectKeyList);

            if (mFromAction is SourceAction _sourceAction)
            {
                _sourceAction.ActionList.Add(_newClassIns);
                mSelectKeyList.Clear();
                InternalClearAllKeySelect();
                InternalRefreshForActionDataView();
            }
            else if (mFromAction is DataProcessActionForFindRowDataInOtherSheet _findAction)
            {
                _findAction.ActionListAfterFindValues.Add(_newClassIns);
                mSelectKeyList.Clear();
                InternalClearAllKeySelect();
                InternalRefreshForActionDataView();
            }
            else
            {
                throw new Exception($"{BtnAddAction_Click} 错误，类型未处理：{mFromAction.GetType().FullName}");
            }
        }

        private void InternalClearAllKeySelect()
        {
            var _rows = this.DataViewForKeyList.Rows;

            for (int i = 0; i < _rows.Count; ++i)
            {
                _rows[i].Cells[mColumIndexForSelect].Value = false;
            }
        }

        private List<DataProcessActionBase> mTargetActionList = new List<DataProcessActionBase>();

        /// <summary>
        /// 刷新当前行为步骤
        /// </summary>
        private void InternalRefreshForActionDataView()
        {
            if (mFromAction is SourceAction _sourceAction)
            {
                mTargetActionList = _sourceAction.ActionList;
            }
            else if (mFromAction is DataProcessActionForFindRowDataInOtherSheet _findAction)
            {
                mTargetActionList = _findAction.ActionListAfterFindValues;
            }

            this.DataViewForAction.DataSource = null;
            this.DataViewForAction.Rows.Clear();
            for (int i = 0; i < mTargetActionList.Count; ++i)
            {
                var _index = this.DataViewForAction.Rows.Add(
                    mTargetActionList[i].GetType().GetCustomAttribute<ProcessActionAttribute>().DisplayName,
                    null,
                    mTargetActionList[i] is DataProcessActionForFindRowDataInOtherSheet ? "设置" : "无功能",
                    mTargetActionList[i] is DataProcessActionForFindRowDataInOtherSheet ? "设置" : "无功能",
                    string.Empty,
                    "移除",
                    "↑",
                    "↓"
                );

                var _row = DataViewForAction.Rows[_index];

                {
                    // 处理绑定 KEY
                    var _cell = _row.Cells[mBindKeyColumIndex] as DataGridViewComboBoxCell;
                    if (_cell != null)
                    {
                        foreach (var _key in mTargetActionList[i].MatchKeyList)
                        {
                            _cell.Items.Add(_key.KeyNameWithIndex);
                        }

                        _cell.Value = mTargetActionList[i].MatchKeyList[0].KeyNameWithIndex;
                    }
                }
            }
        }

        private DataProcessActionBase? GetNewDataForChooseAction()
        {
            DataProcessActionBase _result = null;

            if (mChooseActionType == null)
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，mChooseActionType为空，请检查");
                return null;
            }

            if (mChooseActionType.TargetType == null)
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，mChooseActionType.TargetType 为空，请检查");
                return null;
            }

            var _className = mChooseActionType.TargetType.FullName;

            if (string.IsNullOrEmpty(_className))
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，mChooseActionType.TargetType.FullName 为空，请检查");
                return null;
            }

            _result = Assembly.GetExecutingAssembly().CreateInstance(_className, true) as DataProcessActionBase;

            if (_result == null)
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，实例化失败，请检查");

                return null;
            }

            return _result;
        }

        private void MultiDataSplitSymbol_TextChanged(object sender, EventArgs e)
        {
            this.mFromAction.MultiValueSplitSymbol = this.MultiDataSplitSymbol.Text;
        }

        private void InternalRefreshComboBoxForSelectKey()
        {
            ComboBoxForSelectKey.DataSource = null;
            ComboBoxForSelectKey.Items.Clear();
            ComboBoxForSelectKey.BeginUpdate();
            ComboBoxForSelectKey.DataSource = mSelectKeyList;
            ComboBoxForSelectKey.ValueMember = "KeyIndexInList";
            ComboBoxForSelectKey.DisplayMember = "KeyNameWithIndex";
            if (mSelectKeyList.Count > 0)
            {
                ComboBoxForSelectKey.SelectedIndex = 0;
            }

            ComboBoxForSelectKey.EndUpdate();
        }

        private void ComboBoxForSelectKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxForSelectKey_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxForActionTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _index = this.ComboBoxForActionTypeList.SelectedIndex;
            if (_index >= 0 && _index < mDataClassList.Count)
            {
                mChooseActionType = this.mDataClassList[_index];
            }
        }
    }
}
