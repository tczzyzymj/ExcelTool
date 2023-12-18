using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class KeyData
    {
        private int mKeyIndexInList = 0; // 外部用的

        private string mKeyName = string.Empty;

        private int mKeyIndexInSheetData = 1; // 注意，这个是内部用的，因为 excel 的数据是从下标1开始的

        private int mKeyIndexForShow = 0;

        private List<FilterFuncBase> mFilterFuncList = new List<FilterFuncBase>(); // 其实这个是给源文件用的，其他的目前用不到

        public void Init(int indexForShow, int indexInSheetData, string nameValue)
        {
            mKeyIndexInList = indexForShow;
            mKeyName = nameValue;
            mKeyIndexInSheetData = indexInSheetData;
            mKeyIndexForShow = mKeyIndexInList + 1;
        }

        public int GetKeyIndexInList()
        {
            return mKeyIndexInList;
        }

        public int GetKeyIndexForShow()
        {
            return mKeyIndexForShow;
        }

        public string GetKeyName()
        {
            return mKeyName;
        }

        public int GetKeyIndexInSheetData()
        {
            return mKeyIndexInSheetData;
        }

        public List<FilterFuncBase> GetFilterFuncList()
        {
            return mFilterFuncList;
        }
    }
}
