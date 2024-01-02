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

namespace ExcelTool
{
    public partial class FilterConfigForm : Form
    {
        private int mRowIndex = 0;
        private int mColumnIndex = 0;
        private KeyData? mKeyData = null;

        private const int mCompareDataColumIndex = 0;

        private const int mCompareValueColumIndex = 1;

        public FilterConfigForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.DataGridViewForFilterFunc.AllowUserToAddRows = false;
        }

        public void InitInfo(int rowIndex, int columIndex, KeyData targetData)
        {
            this.mRowIndex = rowIndex;
            this.mColumnIndex = columIndex;
            mKeyData = targetData;
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


            if (!Enum.TryParse(typeof(MainTypeDefine.FilterCompareValueType), this.ComboBoxForValueType.SelectedIndex.ToString(), out var _tempCompareValueType))
            {
                throw new Exception($"比较值类型：{ComboBoxForValueType.SelectedIndex} 解析失败，无法解析为：FilterCompareValueType 请检查！");
            }
            if (!Enum.TryParse(typeof(MainTypeDefine.FilterCompareWayType), this.ComboBoxForCompareType.SelectedIndex.ToString(), out var _tempCompareWayType))
            {
                throw new Exception($"比较方式类型：{this.ComboBoxForCompareType.SelectedIndex} 解析失败，无法解析为：FilterCompareWayType 请检查！");
            }

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

            if (!_filterFuncBase.SetCompareValue(this.TextBoxForCompareValue.Text))
            {
                return;
            }

            if (_tempCompareWayType != null)
            {
                _filterFuncBase.CompareWay = (MainTypeDefine.FilterCompareWayType)_tempCompareWayType;
            }

            var _funcList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mKeyData);
            if (_funcList != null)
            {
                _funcList.Add(_filterFuncBase);
            }
            else
            {
                TableDataManager.Ins().AddSourceFileDataFilterFunc(mKeyData, _filterFuncBase);
            }

            var _addInex = DataGridViewForFilterFunc.Rows.Add(
                null,
                _filterFuncBase.GetCompareValue(),
                "移除"
            );

            var _tempCellBox = DataGridViewForFilterFunc.Rows[_addInex].Cells[mCompareDataColumIndex] as DataGridViewComboBoxCell;
            if (_tempCellBox != null)
            {
                _tempCellBox.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayType));
                _tempCellBox.Value = Enum.GetName(
                    typeof(MainTypeDefine.FilterCompareWayType),
                    _filterFuncBase.CompareWay
                );
            }
        }

        private void DataGridViewForFilterFunc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }

            if (e.ColumnIndex == 2)
            {
                // 点击移除按钮
                var _funcList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mKeyData);
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

            var _funcList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mKeyData);
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
                case mCompareDataColumIndex:
                {
                    if (Enum.TryParse(typeof(MainTypeDefine.FilterCompareWayType), _cell.EditedFormattedValue.ToString(), out var _enumValue) && _enumValue != null)
                    {
                        _funcList[e.RowIndex].CompareWay = (MainTypeDefine.FilterCompareWayType)_enumValue;
                    }

                    break;
                }
                case mCompareValueColumIndex:
                {
                    if (_cell.EditedFormattedValue != null)
                    {
                        var _compareValue = _cell.EditedFormattedValue.ToString();
                        if (_compareValue == null)
                        {
                            _compareValue = string.Empty;
                        }
                        _funcList[e.RowIndex].SetCompareValue(_compareValue);
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
                ComboBoxForCompareType.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayType));
                ComboBoxForCompareType.SelectedItem = Enum.GetName(
                    typeof(MainTypeDefine.FilterCompareWayType),
                    MainTypeDefine.FilterCompareWayType.Equal
                );

                ComboBoxForCompareType.EndUpdate();
            }

            var _filterFuncList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mKeyData);

            if (_filterFuncList != null)
            {
                for (int i = 0; i < _filterFuncList.Count; ++i)
                {
                    ComboBox _newBox = new ComboBox();
                    _newBox.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayType));
                    _newBox.SelectedItem = Enum.GetName(
                        typeof(MainTypeDefine.FilterCompareWayType),
                        _filterFuncList[i].CompareWay
                    );

                    DataGridViewForFilterFunc.Rows.Add(
                        null,
                        _filterFuncList[i].GetCompareValue(),
                        "移除"
                    );
                }

                for (int i = 0; i < DataGridViewForFilterFunc.Rows.Count; ++i)
                {
                    var _cell = DataGridViewForFilterFunc.Rows[i].Cells[mCompareDataColumIndex] as DataGridViewComboBoxCell;
                    if (_cell == null)
                    {
                        continue;
                    }
                    _cell.DataSource = Enum.GetNames(typeof(MainTypeDefine.FilterCompareWayType));
                    _cell.Value = Enum.GetName(
                        typeof(MainTypeDefine.FilterCompareWayType),
                        _filterFuncList[i].CompareWay
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

        }
    }
}
