using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class WorkSheetData
    {
        private bool mHasInit = false;

        private WeakReference<ExcelFileData>? mExcelFileBase = null;

        private ExcelWorksheet? mData = null;

        private List<KeyData> mKeyDataList = new List<KeyData>();

        private int mSheetIndex = 1; // 这里要特别注意，根据 .net版本不同有不同的下标开始，从0，或者1都有可能，这里直接记录结果

        public bool Init(ExcelFileData ownerExcelFile, ExcelWorksheet targetWorkSheet, int index)
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

            if (targetWorkSheet == null)
            {
                MessageBox.Show("传入的 ExcelWorksheet 为空，无法初始化，请检查！", "错误");
                return false;
            }
            mSheetIndex = index;
            mData = targetWorkSheet;

            mExcelFileBase = new WeakReference<ExcelFileData>(ownerExcelFile);

            if (!mExcelFileBase.TryGetTarget(out ExcelFileData? _ownerExcel))
            {
                return false;
            }

            var _rowIndex = _ownerExcel.GetKeyStartRowIndex();

            for (int _colm = _ownerExcel.GetKeyStartColmIndex(); _colm <= mData.Dimension.Columns; ++_colm)
            {
                KeyData keyData = new KeyData();
                mKeyDataList.Add(keyData);
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

                keyData.Init(_colm, _finalStr);
            }

            mHasInit = true;

            return true;
        }
    }
}
