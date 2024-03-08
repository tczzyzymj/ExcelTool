using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    // 表格的内容
    public class CellValueData
    {
        private int mRowIndexInSheet;

        private int mColumnIndexInSheet;

        private int mRowIndexInList;

        private int mColumnIndexInList;

        private string mCellValue = string.Empty;

        private KeyData? mOwnerKey = null; // 不会循环引用，所以直接强引用了

        // 认为表格的内容就是可以为空的，并且只保存为文本
        public void Init(
            string content,
            int rowIndex,
            int colmIndex,
            int rowIndexInList,
            int colIndexInList,
            KeyData ownerKey
        )
        {
            mCellValue = content;
            mRowIndexInSheet = rowIndex;
            mColumnIndexInSheet = colmIndex;
            mRowIndexInList = rowIndexInList;
            mColumnIndexInList = colIndexInList;
            mOwnerKey = ownerKey;
        }

        public int GetCellRowIndexInList()
        {
            return mRowIndexInList;
        }

        public int GetCellColumnIndexInList()
        {
            return mColumnIndexInList;
        }

        public int GetCellRowIndexInSheet()
        {
            return mRowIndexInSheet;
        }

        public int GetCellColumnIndexInSheet()
        {
            return mColumnIndexInSheet;
        }

        public string GetCellValue()
        {
            if (string.IsNullOrEmpty(mCellValue))
            {
                return mCellValue;
            }
            return mCellValue.Replace("'", "");
        }

        public void WriteCellValue(string targetStr)
        {
            if (mCellValue == null)
            {
                return;
            }

            mCellValue = targetStr;
        }
    }
}
