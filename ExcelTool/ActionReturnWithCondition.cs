using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ExcelTool.MainTypeDefine;

namespace ExcelTool
{
    public partial class ActionReturnWithCondition : Form
    {
        private ActionConditionReturnSpecific? mFromAction = null;

        public ActionReturnWithCondition()
        {
            InitializeComponent();
            this.DataGridViewForFilterFunc.AllowUserToAddRows = false;
        }

        public void InitData(ActionConditionReturnSpecific fromData)
        {
            mFromAction = fromData;
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            if(mFromAction == null)
            {
                CommonUtil.ShowError($"{BtnFinishConfig_Click} 错误，mFromAction 为空");
                return;
            }
            if (mFromAction.FilterFuncList.Count < 1)
            {
                CommonUtil.ShowError($"没有设置过滤方法，请检查!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private const int mRemoveIndex = 3; // 移除按钮下标
        private const int mCompareDataColumIndex = 0; // 比较类型下标
        private const int mCompareValueColumIndex = 1; // 比较值下标

        private void DataGridViewForFilterFunc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }
            if (mFromAction == null)
            {
                CommonUtil.ShowError($"{DataGridViewForFilterFunc_CellContentClick} 错误，mFromAction 为空");
                return;
            }
            if (e.ColumnIndex == mRemoveIndex)
            {
                // 点击移除按钮
                var _funcList = mFromAction.FilterFuncList;
                if (_funcList != null)
                {
                    mFromAction.FilterFuncList.RemoveAt(e.RowIndex);

                    // 刷新一下
                    DataGridViewForFilterFunc.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void BtnAddFilterFunc_Click(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                MessageBox.Show("没有 KeyData ，请检查！", "错误");
                return;
            }

            if (string.IsNullOrEmpty(this.TextBoxForCompareValue.Text))
            {
                MessageBox.Show("比较内容为空，请检查!", "错误");
                return;
            }

            var _tempCompareValueType = (MainTypeDefine.FilterCompareValueType)ComboBoxForValueType.SelectedIndex;

            FilterFuncBase? _filterFuncBase = null;

            switch (_tempCompareValueType)
            {
                case MainTypeDefine.FilterCompareValueType.IntValue:
                {
                    _filterFuncBase = new FilterForCompareIntValue();
                    break;
                }
                case MainTypeDefine.FilterCompareValueType.StringValue:
                {
                    _filterFuncBase = new FilterForCompareStringValue();
                    break;
                }
                default:
                {
                    throw new Exception($"未处理的类型：{_tempCompareValueType}，请检查！");
                }
            }

            if (_filterFuncBase == null)
            {
                return;
            }

            if (!_filterFuncBase.SetCompareValue(ComboBoxForCompareType.SelectedIndex, this.TextBoxForCompareValue.Text))
            {
                return;
            }

            mFromAction.FilterFuncList.Add(_filterFuncBase);

            DataGridViewForFilterFunc.Rows.Add(
                ((FilterCompareValueType)_filterFuncBase.GetIntCompareType()).ToString(),
                _filterFuncBase.GetCompareWayName(),
                _filterFuncBase.GetCompareValue(),
                "移除"
            );
        }

        private void TextBoxForReturnString_TextChanged(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                return;
            }

            mFromAction.MatchReturnResult = TextBoxForReturnString.Text;
        }

        private void ComboBoxForValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InternalRefreshForCompareWay();
        }

        private void InternalRefreshForCompareWay()
        {
            switch (ComboBoxForValueType.SelectedIndex)
            {
                case 0:
                {
                    ComboBoxForCompareType.BeginUpdate();
                    ComboBoxForCompareType.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayTypeForNumber));
                    ComboBoxForCompareType.SelectedItem = Enum.GetName(
                        typeof(MainTypeDefine.FilterCompareWayTypeForNumber),
                        MainTypeDefine.FilterCompareWayTypeForNumber.Equal
                    );

                    ComboBoxForCompareType.EndUpdate();
                    break;
                }
                case 1:
                {
                    ComboBoxForCompareType.BeginUpdate();
                    ComboBoxForCompareType.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayForString));
                    ComboBoxForCompareType.SelectedItem = Enum.GetName(
                        typeof(MainTypeDefine.FilterCompareWayForString),
                        MainTypeDefine.FilterCompareWayForString.ContainsIgnoreCase
                    );

                    ComboBoxForCompareType.EndUpdate();

                    break;
                }
            }
        }

        private void ActionReturnWithCondition_Load(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                MessageBox.Show("没有 KeyData ，触发自动关闭，请检查!", "错误");
                Close();
                return;
            }

            // 初始化下拉选择
            {
                ComboBoxForValueType.BeginUpdate();
                ComboBoxForValueType.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareValueType));
                ComboBoxForValueType.SelectedItem = Enum.GetName(
                    typeof(MainTypeDefine.FilterCompareValueType),
                    MainTypeDefine.FilterCompareValueType.IntValue
                );
                ComboBoxForValueType.EndUpdate();

                InternalRefreshForCompareWay();
            }

            var _filterFuncList = mFromAction.FilterFuncList;

            for (int i = 0; i < _filterFuncList.Count; ++i)
            {
                DataGridViewForFilterFunc.Rows.Add(
                    ((FilterCompareValueType)_filterFuncList[i].GetIntCompareType()).ToString(),
                    _filterFuncList[i].GetCompareWayName(),
                    _filterFuncList[i].GetCompareValue(),
                    "移除"
                );
            }

            this.TextBoxForReturnString.Text = mFromAction.MatchReturnResult;
        }
    }
}
