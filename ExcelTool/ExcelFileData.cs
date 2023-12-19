using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class ExcelFileData : TableBaseData
    {
        private ExcelPackage? mExcelPackage = null; // 原始数据如果是

        public ExcelFileData()
        {

        }

        ~ExcelFileData()
        {
            CloseFile();
        }

        // 这里只先加载一下 sheet
        public override bool InternalLoadFile(string absolutePath)
        {
            FileInfo _info = new FileInfo(absolutePath);
            if (!_info.Exists)
            {
                MessageBox.Show($"错误，路径 【{absolutePath}】不存在，请检查！");
                return false;
            }

            mExcelPackage = new ExcelPackage(_info);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 这里要注意，里面说了和 .net 的版本有关，具体请跳转进去看一下
            var _allSheets = mExcelPackage.Workbook.Worksheets;
            int _startIndex = 0;
            if (mExcelPackage.Compatibility.IsWorksheets1Based)
            {
                // 从1开始
                _startIndex = 1;
            }
            else
            {
                // 从0开始
                _startIndex = 0;
            }

            for (int i = _startIndex; i < _allSheets.Count; ++i)
            {
                var sheet = _allSheets[i];
                var _newSheetData = new ExcelSheetData();
                if (!_newSheetData.Init(new WeakReference<TableBaseData>(this), sheet, i - _startIndex, i, sheet.Name))
                {
                    continue;
                }

                mWorkSheetList.Add(_newSheetData);
            }

            return true;
        }

        protected override bool InternalAnalysCellData()
        {
            return true;
        }

        public void ChooseWorkSheet(int indexValue)
        {
        }

        public void CloseFile()
        {
            mExcelPackage?.Dispose();
        }

        public void SaveFile()
        {
            mExcelPackage?.Save();
        }

        public override string GetFileName(bool isFull)
        {
            if (mExcelPackage == null)
            {
                return string.Empty;
            }
            if (isFull)
            {
                return mExcelPackage.File.FullName;
            }

            return mExcelPackage.File.Name;
        }
    }
}
