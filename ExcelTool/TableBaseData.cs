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

        public bool DoLoadFile(string absolutePath)
        {
            if (!File.Exists(absolutePath))
            {
                return false;
            }

            if (InternalLoadFile(absolutePath))
            {
                mExcelAbsolutePath = absolutePath;

                return true;
            }

            if (!AnalysData())
            {
                return false;
            }

            return false;
        }

        public CommonWorkSheetData? GetWorkdSheet()
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
                MessageBox.Show($"尝试选中需要处理的 Sheet 表，但是没有已加载的数据中找到，Sheet 名字是:{targetData.DisplayName}，请检查!","错误");
                return false;
            }
            mChooseWorkSheetIndexInList = targetData.IndexInList;
            mChooseWorkSheet = targetData;

            return true;
        }

        public List<CommonWorkSheetData> GetWorkSheet()
        {
            return mWorkSheetList;
        }

        public virtual string GetFileName(bool isFull)
        {
            return Path.GetFileName(mExcelAbsolutePath);
        }

        public bool AnalysData()
        {
            return InternalAnalysData();
        }

        protected abstract bool InternalAnalysData(); // 解析数据，主要是 key 或者 content 的数据改变了后的解析

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
