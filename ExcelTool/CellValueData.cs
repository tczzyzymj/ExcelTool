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

        private string? mCellValue = string.Empty;

        // 认为表格的内容就是可以为空的，并且只保存为文本
        public void Init(string? content, int rowIndex, int colmIndex)
        {
            mCellValue = content;
            mRowIndex = rowIndex;
            mColumnIndex = colmIndex;
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
