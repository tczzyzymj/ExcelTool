using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    // 这里是最基本的类，后续自己分一下
    public abstract class TableBaseData
    {
        [JsonProperty]
        protected string mExcelAbsolutePath = string.Empty;

        [JsonProperty]
        protected int mKeyStartRowIndex = 2; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0

        [JsonProperty]
        protected int mKeyStartColmIndex = 1; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0

        [JsonProperty]
        protected int mContentStartRowIndex = 4; // 内容选中的行下标，从2开始，认为1是KEY不能小于2

        [JsonProperty]
        private int mChooseWorkSheetIndexInList = 0; // 选中的workSheet需要处理的 workdsheet

        protected List<CommonWorkSheetData> mWorkSheetList = new List<CommonWorkSheetData>();

        protected CommonWorkSheetData? mChooseWorkSheet = null; // 当前选中的目标 WorkSheet

        protected int mChooseSheetIndex = 0;

        protected string mChooseSheetName = string.Empty;

        protected bool mHasInit = false;

        public bool DoLoadFile(string absolutePath)
        {
            if (!File.Exists(absolutePath))
            {
                return false;
            }

            if (!InternalLoadFile(absolutePath))
            {
                return false;
            }

            mExcelAbsolutePath = absolutePath;
            mHasInit = true;
            return true;
        }

        public bool GetHasInit()
        {
            return mHasInit;
        }

        public string? GetFilePath()
        {
            return mExcelAbsolutePath;
        }

        public CommonWorkSheetData? GetCurrentWorkSheet()
        {
            return mChooseWorkSheet;
        }

        public bool TryChooseSheet(CommonWorkSheetData targetData)
        {
            if (targetData == null)
            {
                MessageBox.Show("尝试选中需要处理的 Sheet 表，但传入的内容为空，请检查!");
                return false;
            }

            var _targetIndex = mWorkSheetList.IndexOf(targetData);
            if (_targetIndex < 0)
            {
                MessageBox.Show($"尝试选中需要处理的 Sheet 表，但是没有已加载的数据中找到，Sheet 名字是:{targetData.DisplayName}，请检查!", "错误");
                return false;
            }

            var _keyListData = targetData.GetKeyListData();
            if (_keyListData == null || _keyListData.Count < 1)
            {
                MessageBox.Show("选中的 Sheet 没有 key 数据，请检查！", "错误");
                return false;
            }

            mChooseWorkSheetIndexInList = targetData.IndexInList;
            mChooseWorkSheet = targetData;

            return true;
        }

        public bool IsCurrentSheetValid()
        {
            if (mChooseWorkSheet == null)
            {
                return false;
            }

            var _keyList = mChooseWorkSheet.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                return false;
            }

            return true;
        }

        public List<CommonWorkSheetData> GetWorkSheetList()
        {
            return mWorkSheetList;
        }

        public virtual string GetFileName(bool isFull)
        {
            return Path.GetFileName(mExcelAbsolutePath);
        }

        public bool AnalysCellData()
        {
            return InternalAnalysCellData();
        }

        protected abstract bool InternalAnalysCellData(); // 解析内容数据

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

        public abstract bool InternalLoadFile(string absolutePath);
    }
}
