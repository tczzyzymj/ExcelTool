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
        private string[]? mAllDataArray = null;

        public override bool InternalLoadFile(string absolutePath)
        {
            mAllDataArray = File.ReadAllLines(absolutePath);
            if (mAllDataArray == null || mAllDataArray.Length < 1)
            {
                MessageBox.Show($"文件：{absolutePath} ，内容不正确，请检查");
                return false;
            }

            var _newSheetData = new CSVSheetData();
            mWorkSheetList.Add(_newSheetData);
            _newSheetData.Init(this, mAllDataArray, 0, 0, "Sheet");
            mChooseWorkSheet = _newSheetData;

            return true;
        }

        protected override bool InternalAnalysData()
        {
            if (mChooseWorkSheet == null || mAllDataArray == null)
            {
                MessageBox.Show("无法加载数据，读取文件没成功，请检查！", "错误");
                return false;
            }

            return true;
        }
    }
}
