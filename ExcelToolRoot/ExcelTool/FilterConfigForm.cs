using OfficeOpenXml.Drawing.Slicer.Style;
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
    public partial class FilterConfigForm : Form
    {
        private int mRowIndex = 0;
        private int mColumnIndex = 0;
        private KeyData? mKeyData = null;
        private LoadFileType mFromFileType = LoadFileType.NormalFile;

        public FilterConfigForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.DataGridViewForFilterFunc.AllowUserToAddRows = false;
        }

        public void InitInfo(int rowIndex, int columIndex, KeyData targetData, LoadFileType fromFileType)
        {
            this.mRowIndex = rowIndex;
            this.mColumnIndex = columIndex;
            mKeyData = targetData;
            mFromFileType = fromFileType;
        }

        private void BtnAddFilterFunc_Click(object sender, EventArgs e)
        {
            if (mKeyData == null)
            {
                MessageBox.Show("没有 KeyData ，请检查！", "错误");
                return;
            }

            if (string.IsNullOrEmpty(this.TextBoxForCompareValue.Text))
            {
                MessageBox.Show("内容为空，请检查!", "错误");
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

            var _funcList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, mKeyData);
            if (_funcList != null)
            {
                _funcList.Add(_filterFuncBase);
            }
            else
            {
                TableDataManager.Ins().AddSourceFileDataFilterFunc(mFromFileType, mKeyData, _filterFuncBase);
            }

            DataGridViewForFilterFunc.Rows.Add(
                ((FilterCompareValueType)_filterFuncBase.GetIntCompareType()).ToString(),
                _filterFuncBase.GetCompareWayName(),
                _filterFuncBase.GetCompareValue(),
                "移除"
            );
        }

        private const int mRemoveIndex = 3; // 移除按钮下标
        private const int mCompareDataColumIndex = 0; // 比较类型
        private const int mCompareValueColumIndex = 1; // 比较值

        private void DataGridViewForFilterFunc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }

            if (e.ColumnIndex == mRemoveIndex)
            {
                // 点击移除按钮
                var _funcList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, mKeyData);
                if (_funcList != null)
                {
                    _funcList.RemoveAt(e.RowIndex);

                    // 刷新一下
                    DataGridViewForFilterFunc.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void DataGridViewForFilterFunc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (mKeyData == null)
            {
                return;
            }

            var _funcList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, mKeyData);
            if (_funcList == null)
            {
                CommonUtil.ShowError("GetSourceFileDataFilterFuncByKey 结果为空");
                return;
            }

            var _cell = this.DataGridViewForFilterFunc.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (_cell == null || _cell.EditedFormattedValue == null)
            {
                return;
            }

            switch (e.ColumnIndex)
            {
                case mCompareValueColumIndex:
                {
                    if (_cell.EditedFormattedValue != null)
                    {
                        var _compareValue = _cell.EditedFormattedValue.ToString();
                        if (_compareValue == null)
                        {
                            _compareValue = string.Empty;
                        }
                        _funcList[e.RowIndex].SetCompareValue(_funcList[e.RowIndex].CompareWayIntValue, _compareValue);
                    }

                    break;
                }
            }
        }

        private void FilterConfigForm_Load(object sender, EventArgs e)
        {
            if (mKeyData == null)
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
            }

            {
                ComboBoxForCompareType.BeginUpdate();
                ComboBoxForCompareType.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayTypeForNumber));
                ComboBoxForCompareType.SelectedItem = Enum.GetName(
                    typeof(MainTypeDefine.FilterCompareWayTypeForNumber),
                    MainTypeDefine.FilterCompareWayTypeForNumber.Equal
                );

                ComboBoxForCompareType.EndUpdate();
            }

            var _filterFuncList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, mKeyData);

            if (_filterFuncList != null)
            {
                for (int i = 0; i < _filterFuncList.Count; ++i)
                {
                    DataGridViewForFilterFunc.Rows.Add(
                        ((FilterCompareValueType)_filterFuncList[i].GetIntCompareType()).ToString(),
                        _filterFuncList[i].GetCompareWayName(),
                        _filterFuncList[i].GetCompareValue(),
                        "移除"
                    );
                }
            }
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private void ComboBoxForCompareType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void TextBoxForCompareValue_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
