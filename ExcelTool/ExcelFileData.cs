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

        private List<WorkSheetData> mWorkSheetList = new List<WorkSheetData>();

        private WorkSheetData? mChooseWorkSheet = null; // 当前选中的目标 WorkSheet

        [JsonProperty]
        private int mChooseWorkSheetIndex = 1; // 选中的workSheet需要处理的 workdsheet

        public ExcelFileData()
        {

        }

        ~ExcelFileData()
        {
            CloseFile();
        }

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

            var _allSheets = mExcelPackage.Workbook.Worksheets; // 这里要注意，里面说了和 .net 的版本有关，具体请跳转进去看一下
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
                var _newSheetData = new WorkSheetData();
                if (!_newSheetData.Init(this, sheet, i))
                {
                    return false;
                }

                mWorkSheetList.Add(_newSheetData);
            }

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

        public string GetFileName(bool isFull)
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
