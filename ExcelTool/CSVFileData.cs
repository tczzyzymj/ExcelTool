using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class CSVFileData : TableBaseData
    {
        private DataTable? mOriginDataTable = null;


        public override bool InternalLoadFile(string absolutePath)
        {
            var _allLines = File.ReadAllLines(absolutePath);
            if (_allLines == null || _allLines.Length < 1)
            {
                MessageBox.Show($"文件：{absolutePath} ，内容不正确，请检查");
                return false;
            }

            mOriginDataTable = new DataTable();

            for (int i = 0; i < _allLines.Length; ++i)
            {
                var _splitArray = _allLines[i].Split(',');
                if (i == 0)
                {
                    // 这里是 key
                    for (int j = 0; j < _splitArray.Length; ++j)
                    {
                        mOriginDataTable.Columns.Add(_splitArray[j]);
                    }
                }
                else
                {
                    // 这里是 数据
                    mOriginDataTable.Rows.Add(_splitArray);
                }
            }

            return true;
        }
    }
}
