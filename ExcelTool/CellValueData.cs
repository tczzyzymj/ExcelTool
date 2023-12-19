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
        private int mRowIndex;

        private int mColumnIndex;

        private int mRowIndexInList;

        private int mColumnIndexInList;

        private string? mCellValue = string.Empty;

        private KeyData? mOwnerKey = null; // 不会循环引用，所以直接强引用了

        // 认为表格的内容就是可以为空的，并且只保存为文本
        public void Init(
            string? content, 
            int rowIndex, 
            int colmIndex, 
            int rowIndexInList,
            int colIndexInList,
            KeyData ownerKey
        )
        {
            mCellValue = content;
            mRowIndex = rowIndex;
            mColumnIndex = colmIndex;
            mRowIndexInList = rowIndexInList;
            mColumnIndexInList = colIndexInList;
            mOwnerKey = ownerKey;
        }

        public int GetRowIndexInList()
        {
            return mRowIndexInList;
        }

        public int GetColumnIndexInList()
        {
            return mColumnIndexInList;
        }

        public int GetRowIndex()
        {
            return mRowIndex;
        }

        public int GetColumnIndex()
        {
            return mColumnIndex;
        }

        public string? GetCellValue()
        {
            return mCellValue;
        }
    }
}
