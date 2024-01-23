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
    public partial class NormalActionConnectEditForm : Form
    {
        public NormalActionConnectEditForm()
        {
            InitializeComponent();
            DataViewForAction.AllowUserToAddRows = false;
        }

        private List<CommonDataForComboBox> mListDataForActionType = new List<CommonDataForComboBox>();

        private List<CommonDataForComboBox> mListDataForReturnType = new List<CommonDataForComboBox>();

        private CommonDataForComboBox? mChooseActionType = null;

        private NormalActionBase? mFromAction = null;

        private MultiResultReturnType mReturnType = MultiResultReturnType.SingleString;

        public bool InitData(NormalActionBase targetAction)
        {
            if (targetAction == null)
            {
                CommonUtil.ShowError("传入的 NormalActionBase 为空，请检查");
                this.Close();
                return false;
            }

            mFromAction = targetAction;

            return true;
        }

        private void ComboBoxForActionTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _index = this.ComboBoxForActionTypeList.SelectedIndex;
            if (_index >= 0 && _index < mListDataForActionType.Count)
            {
                mChooseActionType = this.mListDataForActionType[_index];
            }
        }

        private void NormalActionConnectEditForm_Load(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                CommonUtil.ShowError($"KeyConnectEditForm_Load 错误，mFromAction为空");
                return;
            }

            mListDataForActionType = CommonUtil.CreateComboBoxDataForType<ActionCore>();
            ComboBoxForActionTypeList.DataSource = null;
            ComboBoxForActionTypeList.Items.Clear();
            ComboBoxForActionTypeList.DataSource = mListDataForActionType;
            ComboBoxForActionTypeList.ValueMember = "Index";
            ComboBoxForActionTypeList.DisplayMember = "DisplayName";
            MultiDataSplitSymbol.Text = mFromAction.MultiValueSplitSymbol;

            mListDataForReturnType = CommonUtil.CreateComboBoxDataForEnum<MultiResultReturnType>();
            ComboBoxForReturnType.DataSource = null;
            ComboBoxForReturnType.Items.Clear();
            ComboBoxForReturnType.DataSource = mListDataForReturnType;
            ComboBoxForReturnType.ValueMember = "RealValue";
            ComboBoxForReturnType.DisplayMember = "DisplayName";

            var mTargetActionList = mFromAction.FollowSequenceAction.ActionSequence;

            this.DataViewForAction.DataSource = null;
            this.DataViewForAction.Rows.Clear();
            for (int i = 0; i < mTargetActionList.Count; ++i)
            {
                string _contentForSetSearchKey = "无功能";
                StringBuilder _connectInfo = new StringBuilder();
                var _singleAction = mTargetActionList[i];

                if (_singleAction.HaveDetailEdit())
                {
                    _contentForSetSearchKey = "详细设置";
                }

                var _index = this.DataViewForAction.Rows.Add(
                    mTargetActionList[i].GetType().GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    _contentForSetSearchKey,
                    string.Empty,
                    "移除",
                    "↑",
                    "↓",
                    "设置"
                );

                var _row = DataViewForAction.Rows[_index];

                {
                    //// 显示绑定KEY相关信息
                    //var _cell = _row.Cells[mBindKeyColumIndex] as DataGridViewComboBoxCell;
                    //if (_cell != null)
                    //{
                    //    foreach (var _key in mTargetActionList[i].MatchKeyIndexList)
                    //    {
                    //        _cell.Items.Add(_key.KeyNameWithIndex);
                    //    }

                    //    _cell.Value = mTargetActionList[i].MatchKeyList[0].KeyNameWithIndex;
                    //}
                }
            }
        }

        private void ComboBoxForReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _index = this.ComboBoxForReturnType.SelectedIndex;
            if (_index >= 0 && _index < mListDataForReturnType.Count)
            {
                mReturnType = (MultiResultReturnType)mListDataForReturnType[_index].RealValue;
            }
        }

        private void BtnAddAction_Click(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                CommonUtil.ShowError($"{BtnAddAction_Click} 错误，mFromAction 为空");
                return;
            }

            if (mChooseActionType == null)
            {
                CommonUtil.ShowError($"{BtnAddAction_Click} 错误，mChooseActionType为空，请检查");
                return;
            }

            if (mChooseActionType.TargetType == null)
            {
                CommonUtil.ShowError($"{BtnAddAction_Click} 错误，mChooseActionType.TargetType，请检查");
                return;
            }

            var _newClassIns = CommonUtil.GetNewDataForChooseAction(mChooseActionType.TargetType, mReturnType) as NormalActionBase;
            if (_newClassIns == null)
            {
                CommonUtil.ShowError($"{BtnAddAction_Click} 错误，GetNewDataForChooseAction 为空，请检查");
                return;
            }

            mFromAction.FollowSequenceAction.ActionSequence.Add(_newClassIns);

            _newClassIns.MatchKeyIndexList.Clear();
            _newClassIns.MatchKeyNameList.Clear();

            InternalRefreshForActionDataView();
        }

        /// <summary>
        /// 刷新当前行为步骤
        /// </summary>
        private void InternalRefreshForActionDataView()
        {
            if (mFromAction == null)
            {
                CommonUtil.ShowError("mFromAction 为空，请检查！");
                return;
            }

            var mTargetActionList = mFromAction.FollowSequenceAction.ActionSequence;

            this.DataViewForAction.DataSource = null;
            this.DataViewForAction.Rows.Clear();
            for (int i = 0; i < mTargetActionList.Count; ++i)
            {
                string _contentForSetSearchKey = "无功能";
                StringBuilder _connectInfo = new StringBuilder();
                var _singleAction = mTargetActionList[i];

                if (_singleAction.HaveDetailEdit())
                {
                    _contentForSetSearchKey = "详细设置";
                }

                var _index = this.DataViewForAction.Rows.Add(
                    mTargetActionList[i].GetType().GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    _contentForSetSearchKey,
                    string.Empty,
                    "移除",
                    "↑",
                    "↓",
                    "设置"
                );

                var _row = DataViewForAction.Rows[_index];

                {
                    //// 显示绑定KEY相关信息
                    //var _cell = _row.Cells[mBindKeyColumIndex] as DataGridViewComboBoxCell;
                    //if (_cell != null)
                    //{
                    //    foreach (var _key in mTargetActionList[i].MatchKeyIndexList)
                    //    {
                    //        _cell.Items.Add(_key.KeyNameWithIndex);
                    //    }

                    //    _cell.Value = mTargetActionList[i].MatchKeyList[0].KeyNameWithIndex;
                    //}
                }
            }
        }

        private bool InternalSwapForActionList(int chooseIndex, bool isUp)
        {
            if (mFromAction == null || mFromAction.FollowSequenceAction.ActionSequence.Count < 2)
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

            if (_targetIndex < 0 || _targetIndex >= mFromAction.FollowSequenceAction.ActionSequence.Count)
            {
                return false;
            }

            var _tempData = mFromAction.FollowSequenceAction.ActionSequence[chooseIndex];
            mFromAction.FollowSequenceAction.ActionSequence[chooseIndex] = mFromAction.FollowSequenceAction.ActionSequence[_targetIndex];
            mFromAction.FollowSequenceAction.ActionSequence[_targetIndex] = _tempData;

            return true;
        }

        private const int mDetailConfigIndex = 1; // 自己的详细设置
        private const int mRemoveColumIndex = 3;// 移除按钮
        private const int mMoveUpColum = 4; // 上移按钮
        private const int mMoveDownColum = 5; // 下移按钮
        private const int mFollowActionColum = 6; // 下移按钮

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
                case mRemoveColumIndex:
                {
                    mFromAction.FollowSequenceAction.ActionSequence.RemoveAt(e.RowIndex);
                    InternalRefreshForActionDataView();
                    break;
                }
                case mDetailConfigIndex:
                {
                    // 根据不同的行为，打开不同的编辑界面
                    var _action = this.mFromAction.FollowSequenceAction.ActionSequence[e.RowIndex];
                    _action.OpenDetailEditForm();

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
                case mFollowActionColum:
                {
                    var _action = this.mFromAction.FollowSequenceAction.ActionSequence[e.RowIndex];
                    if (_action is FindAction _findAction)
                    {
                        var _workSheetData = _findAction.GetSearchWorkSheetData();
                        if (_workSheetData == null)
                        {
                            CommonUtil.ShowError("当前查找行为没有配置查找目标，请在详细配置中设置后，再设置后续配置");
                            return;
                        }

                        SequenceActionConnectEditForm _form = new SequenceActionConnectEditForm();
                        if (!_form.InitData(_findAction.FollowSequenceAction))
                        {
                            return;
                        }

                        _form.ShowDialog();
                    }
                    else if (_action is NormalActionBase _normalAction)
                    {
                        // 这里就去做普通的后续配置
                        NormalActionConnectEditForm _form = new NormalActionConnectEditForm();
                        if (!_form.InitData(_normalAction))
                        {
                            return;
                        }
                        _form.ShowDialog();
                    }
                    else
                    {
                        CommonUtil.ShowError($"未处理的类类型：[{_action.GetType().FullName}]");
                        break;
                    }

                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
