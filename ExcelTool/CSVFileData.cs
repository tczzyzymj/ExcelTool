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
        public CSVFileData()
        {
            mKeyStartRowIndex = 0; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0
            mKeyStartColmIndex = 0; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0
            mContentStartRowIndex = 3; // 内容选中的行下标，从2开始，认为1是KEY不能小于2
            /// <summary>
            /// ID的列下标
            /// </summary>
            IDIndex = 0;
        }

        private string[]? mAllDataArray = null;

        public string SplitSymbol
        {
            get;
            set;
        } = ",";

        public override bool WriteData(List<List<CellValueData>> filteredInData)
        {
            if (!base.WriteData(filteredInData))
            {
                return false;
            }

            return true;
        }

        public override void SaveFile()
        {
        }

        public override bool InternalLoadFile(string absolutePath)
        {
            mAllDataArray = File.ReadAllLines(absolutePath);
            if (mAllDataArray == null || mAllDataArray.Length < 1)
            {
                MessageBox.Show($"文件：{absolutePath} ，内容不正确，请检查");
                return false;
            }

            var _newSheetData = new CSVSheetData();
            if (!_newSheetData.Init(new WeakReference<TableBaseData>(this), mAllDataArray, 0, 0, "Sheet"))
            {
                return false;
            }

            mWorkSheetList.Add(_newSheetData);

            mCurrentWorkSheet = _newSheetData;

            return true;
        }

        protected override bool InternalAnalysCellData()
        {
            if (mCurrentWorkSheet == null || mAllDataArray == null)
            {
                MessageBox.Show("无法加载数据，读取文件没成功，请检查！", "错误");
                return false;
            }

            return true;
        }
    }
}
