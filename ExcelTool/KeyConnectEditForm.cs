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
        private FileDataBase? mTargetFile = null;
        private CommonWorkSheetData? mFromSheet = null;
        private CommonWorkSheetData? mSelectSheet = null;
        private const int mColumIndexForConnectInfo = 2;
        private const int mColumIndexForEditConnect = 3;
        private const int mColumIndexForSetConnect = 4;

        private List<KeyData> mWorkSheetKeyListData = new List<KeyData>();

        private List<CommonDataForClass> mDataClassList = new List<CommonDataForClass>();

        private CommonDataForClass mChooseActionType = null;

        private bool mCanLoadNewFile = true;

        public KeyConnectEditForm()
        {
            InitializeComponent();
            this.DataViewForKeyList.AllowUserToAddRows = false;
            DataViewForAction.AllowUserToAddRows = false;
        }

        public bool InitData(DataProcessActionBase targetAction, bool canLoadNewFile, CommonWorkSheetData fromSheet)
        {
            if (targetAction == null)
            {
                MessageBox.Show("传入的 KeyData 为空，请检查！", "错误");
                this.Close();
                return false;
            }
            mFromSheet = fromSheet;
            mCanLoadNewFile = canLoadNewFile;
            mFromAction = targetAction;
            return true;
        }

        private void KeyConnectEditForm_Load(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                throw new Exception($"KeyConnectEditForm_Load 错误，mFromAction为空");
            }

            BtnLoadNewFile.Visible = mCanLoadNewFile;
            LabelLoadedFile.Visible = mCanLoadNewFile;
            ComboBoxForLoadedFile.Visible = mCanLoadNewFile;
            LabelSelectSheet.Visible = mCanLoadNewFile;
            ComboBoxForWorkSheet.Visible = mCanLoadNewFile;

            if (!mCanLoadNewFile && mFromAction is ActionFindRowDataInOtherSheet _findAction)
            {
                LabelForFromTable.Text = _findAction.SearchTargetSheet.GetOwnerTable()?.DisplayName + "->" + _findAction.SearchTargetSheet.DisplayName;
                mSelectSheet = _findAction.SearchTargetSheet;
                mTargetFile = mSelectSheet?.GetOwnerTable();
            }

            mDataClassList = CommonUtil.CreateComboBoxDataForType<DataProcessActionBase>();
            ComboBoxForActionTypeList.DataSource = null;
            ComboBoxForActionTypeList.Items.Clear();
            ComboBoxForActionTypeList.DataSource = mDataClassList;
            ComboBoxForActionTypeList.ValueMember = "Index";
            ComboBoxForActionTypeList.DisplayMember = "Name";
            MultiDataSplitSymbol.Text = mFromAction.MultiValueSplitSymbol;

            InternalRefreshForActionDataView();
            InternalRefreshAfterLoadFile();
            InternalRefreshSheetComboBox();
            InternalRefreshComboBoxForSelectKey();
            mFromSheet = null;
        }

        private void InternalRefreshAfterLoadFile()
        {
            if (!mCanLoadNewFile)
            {
                return;
            }

            // 这里为 file combobox 的已加载文件做显示
            ComboBoxForLoadedFile.BeginUpdate();
            ComboBoxForLoadedFile.DataSource = null;
            ComboBoxForLoadedFile.Items.Clear();
            var _dataList = TableDataManager.Ins().GetLoadedFileList();
            ComboBoxForLoadedFile.DataSource = _dataList;
            ComboBoxForLoadedFile.ValueMember = "DisplayIndex";
            ComboBoxForLoadedFile.DisplayMember = "DisplayName";
            ComboBoxForLoadedFile.EndUpdate();

            if (mFromSheet != null)
            {
                var _index = TableDataManager.Ins().TryGetTableIndex(mFromSheet.GetOwnerTable());
                if (_index >= 0 && _dataList != null && _dataList.Count > 0)
                {
                    ComboBoxForLoadedFile.SelectedIndex = _index;
                }
            }
            else if (mTargetFile != null)
            {
                var _index = TableDataManager.Ins().TryGetTableIndex(mTargetFile);
                if (_index >= 0 && _dataList != null && _dataList.Count > 0)
                {
                    ComboBoxForLoadedFile.SelectedIndex = _index;
                }
            }
        }

        private List<KeyData> mSelectKeyList = new List<KeyData>();

        private const int mColumIndexForSelect = 2;
        private void DataViewForKeyConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }

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
            _form.SetInitData(LoadFileType.NormalFile, mSelectSheet);
            if (_form.ShowDialog() == DialogResult.OK)
            {
                mTargetFile = _form.GetChooseFile();
                InternalRefreshAfterLoadFile();
            }
        }

        private void ComboBoxForLoadedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _index = this.ComboBoxForLoadedFile.SelectedIndex;
            if (_index >= 0)
            {
                mTargetFile = TableDataManager.Ins().GetLoadedFileList()[_index];
                InternalRefreshSheetComboBox();
            }
        }

        private void InternalRefreshSheetComboBox()
        {
            if (mTargetFile == null)
            {
                return;
            }

            var _sheetList = mTargetFile.GetWorkSheetList();
            if (_sheetList.Count > 0)
            {
                ComboBoxForWorkSheet.DataSource = null;
                ComboBoxForWorkSheet.Items.Clear();
                ComboBoxForWorkSheet.BeginUpdate();
                ComboBoxForWorkSheet.DataSource = _sheetList;
                ComboBoxForWorkSheet.ValueMember = "IndexInListForShow";
                ComboBoxForWorkSheet.DisplayMember = "DisplayName";
                if (mFromSheet != null)
                {
                    var _tempIndex = _sheetList.IndexOf(mFromSheet);
                    if (_tempIndex >= 0 && _sheetList != null && _sheetList.Count > 0)
                    {
                        ComboBoxForWorkSheet.SelectedIndex = _tempIndex;
                    }
                }

                ComboBoxForWorkSheet.EndUpdate();
            }
        }

        private void ComboBoxForWorkSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ComboBoxForWorkSheet.SelectedIndex < 0)
            {
                return;
            }

            mSelectSheet = mTargetFile?.GetWorkSheetByIndex(this.ComboBoxForWorkSheet.SelectedIndex);
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

        private const int mBindKeyColumIndex = 1; // 绑定的 key
        private const int mDetailConfigIndex = 2; // 设置查找其他表KEY
        private const int mRemoveColumIndex = 4;// 移除按钮
        private const int mMoveUpColum = 5; // 上移按钮
        private const int mMoveDownColum = 6; // 下移按钮
        private void DataViewForAction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }

            if (mFromAction == null)
            {
                MessageBox.Show($"DataViewForAction_CellContentClick 出错， mFromAction 为空，请检查", "错误");
                return;
            }

            switch (e.ColumnIndex)
            {
                //case mConnectActionBtnColumIndex:
                //{
                //    var _action = this.mTargetActionList[e.RowIndex];
                //    if (_action is DataProcessActionForFindRowDataInOtherSheet _findAction)
                //    {
                //        if (_findAction.SearchTargetSheet == null)
                //        {
                //            MessageBox.Show("请先设置查找表 Key");
                //            return;
                //        }
                //        KeyConnectEditForm _newForm = new KeyConnectEditForm();
                //        _newForm.InitData(_action, false, _findAction.SearchTargetSheet);
                //        if (_newForm.ShowDialog() == DialogResult.OK)
                //        {
                //            InternalRefreshForActionDataView();
                //        }
                //    }

                //    break;
                //}
                case mRemoveColumIndex:
                {
                    mTargetActionList.RemoveAt(e.RowIndex);
                    InternalRefreshForActionDataView();
                    break;
                }
                case mDetailConfigIndex:
                {
                    // 根据不同的行为，打开不同的编辑界面
                    var _action = this.mTargetActionList[e.RowIndex];
                    if (_action is ActionFindRowDataInOtherSheet _findAction)
                    {
                    }

                    //if (_action is DataProcessActionForFindRowDataInOtherSheet _findAction)
                    //{
                    //    ChooseFileConfigForm _newForm = new ChooseFileConfigForm();
                    //    _newForm.SetFindAction(_findAction);
                    //    if (_newForm.ShowDialog() == DialogResult.OK)
                    //    {
                    //        var _keyList = _newForm.GetSelectKeyList();

                    //        if (_keyList.Count <= 0)
                    //        {
                    //            MessageBox.Show("为什么没有配置任何 Key？", "错误");
                    //            return;
                    //        }

                    //        _findAction.SearchKeyList = _keyList;

                    //        _findAction.SearchTargetSheet = _newForm.GetChooseSheet();

                    //        InternalRefreshForActionDataView();
                    //    }
                    //}

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
            else if (mFromAction is ActionFindRowDataInOtherSheet _findAction)
            {
                _findAction.FollowActionList.Add(_newClassIns);
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
            else if (mFromAction is ActionFindRowDataInOtherSheet _findAction)
            {
                mTargetActionList = _findAction.FollowActionList;
            }

            this.DataViewForAction.DataSource = null;
            this.DataViewForAction.Rows.Clear();
            for (int i = 0; i < mTargetActionList.Count; ++i)
            {
                string _contentForSetSearchKey = "无功能";
                string _contentForSetActionAfterSearch = "无功能";
                StringBuilder _connectInfo = new StringBuilder();
                if (mTargetActionList[i] is ActionFindRowDataInOtherSheet _findAction)
                {
                    if (_findAction.SearchTargetSheet != null)
                    {
                        var _tempInfo = string.Format("{0}->{1}",
                            _findAction.SearchTargetSheet.GetOwnerTable()?.DisplayName,
                            _findAction.SearchTargetSheet.DisplayName
                        );
                        _connectInfo.Append(_tempInfo);
                        _connectInfo.Append("->");
                    }

                    _contentForSetSearchKey = _findAction.SearchKeyList.Count > 0 ? "已设置" : "设置";
                    _contentForSetActionAfterSearch = _findAction.FollowActionList.Count > 0 ? "已设置" : "设置";
                    foreach (var _tempKey in _findAction.SearchKeyList)
                    {
                        _connectInfo.Append(_tempKey.KeyName);
                        _connectInfo.Append(";");
                    }
                }
                var _index = this.DataViewForAction.Rows.Add(
                    mTargetActionList[i].GetType().GetCustomAttribute<ProcessActionAttribute>().DisplayName,
                    null,
                    _contentForSetSearchKey,
                    _contentForSetActionAfterSearch,
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
