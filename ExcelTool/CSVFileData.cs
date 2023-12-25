using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class CSVFileData : FileDataBase
    {
        public CSVFileData()
        {

        }

        public string SplitSymbol
        {
            get;
            set;
        } = ",";

        public override bool WriteData(List<List<CellValueData>> filteredInData, int workSheetIndex)
        {
            if (!base.WriteData(filteredInData, workSheetIndex))
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
            string[] _keyArray = null;
            using (StreamReader sr = new StreamReader(absolutePath))
            {
                using (CsvReader _csvReader = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    _csvReader.Read();
                    _csvReader.ReadHeader();
                    if (_csvReader.HeaderRecord != null)
                    {
                        _keyArray = _csvReader.HeaderRecord;
                    }
                }
            }

            if (_keyArray == null || _keyArray.Length < 1)
            {
                MessageBox.Show($"文件：{absolutePath} ，内容不正确，请检查");
                return false;
            }

            var _newSheetData = new CSVSheetData();
            if (!_newSheetData.Init(new WeakReference<FileDataBase>(this), _keyArray, 0, 0, "Sheet"))
            {
                return false;
            }

            mWorkSheetList.Add(_newSheetData);

            return true;
        }
    }
}
