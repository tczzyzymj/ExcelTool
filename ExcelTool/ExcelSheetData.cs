using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExcelTool
{
    internal class ExcelSheetData : CommonWorkSheetData
    {
        protected ExcelWorksheet? mSheetData = null; // 原始数据

        public override void ReloadKey()
        {
            base.ReloadKey();
            InternalInitWithKey(mSheetData, true);
        }

        protected override bool InternalInitWithKey(object? sheetData, bool isForce)
        {
            if (mHasInitKey && !isForce)
            {
                return true;
            }

            if (mOwnerTable == null)
            {
                MessageBox.Show("传入的 ExcelFileBase 为空，无法初始化，请检查！", "错误");
                return false;
            }

            if (sheetData == null)
            {
                MessageBox.Show("传入的 ExcelWorksheet 为空，无法初始化，请检查！", "错误");
                return false;
            }
            var _finalData = sheetData as ExcelWorksheet;
            mSheetData = _finalData;
            if (mSheetData == null)
            {
                MessageBox.Show("传入的 Data 数据无法解析为 ExcelWorksheet，请检查", "错误");
                return false;
            }

            if (mSheetData.Dimension == null)
            {
                // 这个 sheet 里面的内容为空，还是要记录一下，所以返回true
                return true;
            }

            if (!mOwnerTable.TryGetTarget(out TableBaseData? _ownerExcel))
            {
                return false;
            }

            var _rowIndex = _ownerExcel.GetKeyStartRowIndex();

            for (int _colm = _ownerExcel.GetKeyStartColmIndex(); _colm <= mSheetData.Dimension.Columns; ++_colm)
            {
                var _tempValue = mSheetData.Cells[_rowIndex, _colm].Value;
                if (_tempValue == null)
                {
                    MessageBox.Show($"表格的 key 有空列，请检查，文件：{_ownerExcel.GetFileName(true)}, sheet:{mSheetData.Name}，行：{_rowIndex}, 列：{_colm}", "错误");
                    return false;
                }
                string? _finalStr = _tempValue as string;
                if (string.IsNullOrEmpty(_finalStr))
                {
                    MessageBox.Show($"表格的 key 有空列，请检查，文件：{_ownerExcel.GetFileName(true)}, sheet:{mSheetData.Name}", "错误");
                    return false;
                }

                AddNewKeyData(_colm - _ownerExcel.GetKeyStartColmIndex(), _colm, _finalStr);
            }

            mHasInitKey = true;

            return true;
        }

        protected override bool InternalLoadAllCellData()
        {
            if (mHasLoadAllCellData)
            {
                return true;
            }
            var _ownerTable = GetOwnerTable();
            if (_ownerTable == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取父 Table ，请检查！", "错误");
                return false;
            }

            if (mSheetData == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取 SheetData，请检查！", "错误");
                return false;
            }

            var _contentStartRow = _ownerTable.GetContentStartRowIndex();
            var _keyStartColum = _ownerTable.GetKeyStartColmIndex();

            mCellData2DList = new List<List<CellValueData>>(mSheetData.Dimension.Rows - 4);

            for (int _row = _contentStartRow; _row <= mSheetData.Dimension.Rows; ++_row)
            {
                var _newList = new List<CellValueData>();
                mCellData2DList.Add(_newList);

                for (int _colum = _keyStartColum; _colum <= mSheetData.Dimension.Columns; ++_colum)
                {
                    var _newCellData = new CellValueData();
                    _newList.Add(_newCellData);
                    var _value = mSheetData.Cells[_row, _colum].Value;
                    _newCellData.Init(_value == null ? string.Empty : _value.ToString(), _row, _colum);
                }
            }

            mHasLoadAllCellData = true;

            return true;
        }
    }
}
