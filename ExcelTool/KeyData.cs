using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class KeyData
    {
        private int mKeyIndex = 1;

        private string mKeyName = string.Empty;

        public void Init(int indexValue, string nameValue)
        {
            mKeyIndex = indexValue;
            mKeyName = nameValue;
        }

        public int GetKeyIndex()
        {
            return mKeyIndex;
        }

        public string GetKeyName()
        {
            return mKeyName;
        }
    }
}
