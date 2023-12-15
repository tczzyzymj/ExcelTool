using Newtonsoft.Json;
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
        protected int mKeyStartRowIndex = 1; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0

        [JsonProperty]
        protected int mKeyStartColmIndex = 1; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0

        [JsonProperty]
        protected int mContentStartRowIndex = 2; // 内容选中的行下标，从2开始，认为1是KEY不能小于2

        [JsonProperty]
        protected int mContentStartColmIndex = 1; // 内容开始的列下标，从1开始

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

            return false;
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

        public abstract bool InternalLoadFile(string absolutePath);
    }
}
