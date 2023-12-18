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
        protected ExcelWorksheet? mData = null; // 原始数据

        protected override bool InternalInitWithKey(TableBaseData ownerExcelFile, object sheetData, int IndexInListValue, int indexInFileData, string name)
        {
            if (mHasInit)
            {
                return true;
            }

            if (ownerExcelFile == null)
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
            mData = _finalData;
            if (mData == null)
            {
                MessageBox.Show("传入的 Data 数据无法解析为 ExcelWorksheet，请检查", "错误");
                return false;
            }

            if (mData.Dimension == null )
            {
                // 这个 sheet 里面的内容为空，还是要记录一下，所以返回true
                return true;
            }

            mExcelFileBase = new WeakReference<TableBaseData>(ownerExcelFile);

            if (!mExcelFileBase.TryGetTarget(out TableBaseData? _ownerExcel))
            {
                return false;
            }

            var _rowIndex = _ownerExcel.GetKeyStartRowIndex();

            for (int _colm = _ownerExcel.GetKeyStartColmIndex(); _colm <= mData.Dimension.Columns; ++_colm)
            {
                var _tempValue = mData.Cells[_rowIndex, _colm].Value;
                if (_tempValue == null)
                {
                    MessageBox.Show($"表格的 key 有空列，请检查，文件：{_ownerExcel.GetFileName(true)}, sheet:{mData.Name}，行：{_rowIndex}, 列：{_colm}", "错误");
                    return false;
                }
                string? _finalStr = _tempValue as string;
                if (string.IsNullOrEmpty(_finalStr))
                {
                    MessageBox.Show($"表格的 key 有空列，请检查，文件：{_ownerExcel.GetFileName(true)}, sheet:{mData.Name}", "错误");
                    return false;
                }

                AddNewKeyData(_colm - _ownerExcel.GetKeyStartColmIndex(), _colm, _finalStr);
            }

            mHasInit = true;

            return true;
        }

        protected override bool InternalLoadAllCellData()
        {
            return true;
        }
    }
}
