using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    internal class CSVSheetData : CommonWorkSheetData
    {
        protected override bool InternalInitWithKey(object sheetData)
        {
            var _allData = sheetData as string[];
            if (_allData == null)
            {
                MessageBox.Show("传入的数据为空，请检查！", "错误");
                return false;
            }

            // 这里是 key，初始化的时候只初始化一下 key，内容放到后面的解析去做
            {
                var _splitArray = _allData[0].Split(',');

                for (int _i = 0; _i < _splitArray.Length; ++_i)
                {
                    AddNewKeyData(_i, _i, _splitArray[_i]);
                }
            }

            return true;
        }

        protected override bool InternalLoadAllCellData()
        {
            return true;
        }
    }
}
