using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class KeyData
    {
        private int mKeyIndexForShow = 0; // 外部用的

        private string mKeyName = string.Empty;

        private int mKeyIndexInSheetData = 1; // 注意，这个是内部用的，因为 excel 的数据是从下标1开始的

        public void Init(int indexValue, int indexInSheetData, string nameValue)
        {
            mKeyIndexForShow = indexValue;
            mKeyName = nameValue;
            mKeyIndexInSheetData = indexInSheetData;
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
    }
}
