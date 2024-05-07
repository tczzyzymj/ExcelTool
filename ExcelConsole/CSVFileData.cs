using System.Globalization;
using CsvHelper;

namespace ExcelTool
{
    public class CSVFileData : FileDataBase
    {
        public CSVFileData()
        {
        }

        public CSVFileData(string absolutePath, LoadFileType fileType) : base(absolutePath, fileType)
        {
        }

        public string SplitSymbol
        {
            get;
            set;
        } = ",";

        public override bool WriteData(List<List<string>> filteredInData, int workSheetIndex)
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

        protected override bool InternalLoadFile(string absolutePath)
        {
            string[]? _keyArray = null;

            using (StreamReader _sr = new StreamReader(absolutePath))
            {
                using (CsvReader _csvReader = new CsvReader(_sr, CultureInfo.InvariantCulture))
                {
                    _csvReader.Read();
                    _csvReader.ReadHeader();

                    if (_csvReader.HeaderRecord != null)
                    {
                        _keyArray = _csvReader.HeaderRecord;
                    }
                }
            }

            if ((_keyArray == null) || (_keyArray.Length < 1))
            {
                CommonUtil.ShowError($"文件：{absolutePath} ，内容不正确，请检查");

                return false;
            }

            CSVSheetData _newSheetData = new CSVSheetData();

            bool _bInit = _newSheetData.Init(
                new WeakReference<FileDataBase>(this),
                _keyArray,
                0,
                0,
                "Sheet"
            );

            if (!_bInit)
            {
                return false;
            }

            mWorkSheetList.Add(_newSheetData);

            return true;
        }
    }
}
