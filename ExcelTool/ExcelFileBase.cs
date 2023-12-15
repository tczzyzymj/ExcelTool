using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExcelTool
{
    internal class ExcelFileBase
    {
        private ExcelPackage mExcelPackage = null; // 原始数据

        [JsonProperty]
        private string mExcelAbsolutePath = string.Empty;

        private List<WorkSheetData> mWorkSheetList = new List<WorkSheetData>();

        [JsonProperty]
        private int mKeyStartRowIndex = 1; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0

        [JsonProperty]
        private int mKeyStartColmIndex = 1; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0

        [JsonProperty]
        private int mContentStartRowIndex = 2; // 内容选中的行下标，从2开始，认为1是KEY不能小于2

        [JsonProperty]
        private int mContentStartColmIndex = 1; // 内容开始的列下标，从1开始

        private WorkSheetData mChooseWorkSheet = null; // 当前选中的目标 WorkSheet

        [JsonProperty]
        private int mChooseWorkSheetIndex = 1; // 选中的workSheet需要处理的 workdsheet

        public ExcelFileBase()
        {

        }

        ~ExcelFileBase()
        {
            CloseFile();
        }

        public bool LoadFile(string absolutePath)
        {
            FileInfo _info = new FileInfo(absolutePath);
            if (!_info.Exists)
            {
                MessageBox.Show($"错误，路径 【{absolutePath}】不存在，请检查！");
                return false;
            }

            bool _result = false;
            try
            {
                mExcelPackage = new ExcelPackage(_info);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                mExcelAbsolutePath = absolutePath;

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

                _result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return _result;
        }

        public void ChooseWorkSheet(int indexValue)
        {
        }

        public int GetKeyStartRowIndex()
        {
            return mKeyStartRowIndex;
        }

        public void SetKeyStartRowIndex(int targetValue)
        {
            mKeyStartRowIndex = targetValue;
        }

        public int GetKeyStartColmIndex()
        {
            return mKeyStartColmIndex;
        }

        public void SetKeyStartColmIndex(int targetValue)
        {
            mKeyStartColmIndex = targetValue;
        }

        public int GetContentStartRowIndex()
        {
            return mContentStartRowIndex;
        }

        public void SetContentStartRowIndex(int targetValue)
        {
            mContentStartRowIndex = targetValue;
        }

        public int GetContentStartColmIndex()
        {
            return mContentStartColmIndex;
        }

        public void SetContentStartColmIndex(int targetValue)
        {
            mContentStartColmIndex = targetValue;
        }

        public void CloseFile()
        {
            mExcelPackage.Dispose();
        }

        public void SaveFile()
        {
            mExcelPackage.Save();
        }

        public string GetFileName(bool isFull)
        {
            if (isFull)
            {
                return mExcelPackage.File.FullName;
            }

            return mExcelPackage.File.Name;
        }
    }
}
