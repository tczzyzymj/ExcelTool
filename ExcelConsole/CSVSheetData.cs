using CsvHelper;
using System.Globalization;

namespace ExcelTool
{
    public class CSVSheetData : CommonWorkSheetData
    {
        public CSVSheetData()
        {
            mKeyStartRowIndexInSheet = 0; // 行，其开始的行下标，下标从0开始
            mKeyStartColmIndexInSheet = 0; // 列，其开始的列下标，下标从0开始
            mContentStartRowIndexInSheet = 3; // 内容行下标，下标从0开始
        }

        private List<string[]> mAllSheetData = new List<string[]>();

        public override void ReloadKey()
        {
            base.ReloadKey();
            var absolutePath = GetOwnerTable()?.GetFileAbsulotePath();
            if (string.IsNullOrEmpty(absolutePath))
            {
                return;
            }

            try
            {
                string[]? _keyArray = null;
                using (StreamReader sr = new StreamReader(absolutePath))
                {
                    using (CsvReader _csvReader = new CsvReader(sr, CultureInfo.InvariantCulture))
                    {
                        for (int i = 0; i <= mKeyStartRowIndexInSheet; ++i)
                        {
                            _csvReader.Read();
                        }

                        if (_csvReader.Parser.Record != null)
                        {
                            _keyArray = _csvReader.Parser.Record;
                        }
                    }
                }

                if (_keyArray == null || _keyArray.Length < 1)
                {
                    CommonUtil.ShowError($"文件：{absolutePath} ，内容不正确，请检查");
                    return;
                }

                InternalInitWithKey(_keyArray, true);
            }
            catch (Exception ex)
            {
                CommonUtil.ShowError(ex.ToString());
            }
        }

        public override bool WriteOneData(int rowIndexInSheet, List<string> inValueList, bool skipEmptyData)
        {
            throw new Exception("CSV写入功能完善中");
            //if (!base.WriteOneData(rowIndexInSheet, inValueList, skipEmptyData))
            //{
            //    return false;
            //}

            //var _keyListData = GetKeyListData();
            //if (_keyListData == null)
            //{
            //    CommonUtil.ShowError("WriteData ，但是 KeyList 为空，请检查");

            //    return false;
            //}

            //return true;
        }

        protected override bool InternalInitWithKey(object? sheetData, bool isForce)
        {
            if (mHasInitKey && !isForce)
            {
                return true;
            }

            var _keyArray = sheetData as string[];

            if (_keyArray == null)
            {
                CommonUtil.ShowError("传入的数据为空，请检查!");
                return false;
            }

            var _ownerTable = GetOwnerTable() as CSVFileData;
            if (_ownerTable == null)
            {
                CommonUtil.ShowError("表格数据为空，请检查!");
                return false;
            }

            // 这里是 key，初始化的时候只初始化一下 key，内容放到后面的解析去做
            {
                var _keyLine = _keyArray;

                for (int _i = GetKeyStartColmIndexInSheet(); _i < _keyLine.Length; ++_i)
                {
                    AddNewKeyData(_i - GetKeyStartColmIndexInSheet(), _i, _keyLine[_i]);
                }
            }

            InternalLoadAllCellData(true);

            mHasInitKey = true;

            return true;
        }

        protected override bool InternalLoadAllCellData(bool forceLoad)
        {
            if (mHasLoadAllCellData && !forceLoad)
            {
                return true;
            }
            mAllSheetData.Clear();
            var _ownerTable = GetOwnerTable() as CSVFileData;
            if (_ownerTable == null)
            {
                CommonUtil.ShowError("InternalLoadAllCellData 无法获取父 Table ，请检查！");
                return false;
            }

            var _contentStartRow = GetContentStartRowIndexInSheet();
            var _keyStartColum = GetKeyStartColmIndexInSheet();

            var _filePath = _ownerTable.GetFileAbsulotePath();

            using (StreamReader sr = new StreamReader(_filePath))
            {
                using (CsvReader _csvReader = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    for (int i = 0; i < _contentStartRow; ++i)
                    {
                        _csvReader.Read();
                    }

                    while (_csvReader.Read())
                    {
                        if (_csvReader.Parser.Record != null)
                        {
                            mAllSheetData.Add(_csvReader.Parser.Record);
                        }
                    }
                }
            }

            if (mAllSheetData == null)
            {
                CommonUtil.ShowError("InternalLoadAllCellData 无法获取 SheetData，请检查！");
                return false;
            }

            mCellData2DList = new List<List<CellValueData>>(mAllSheetData.Count);

            for (int _row = 0; _row < mAllSheetData.Count; ++_row)
            {
                var _newList = new List<CellValueData>();
                mCellData2DList.Add(_newList);

                var _rowArray = mAllSheetData[_row];

                for (int _colum = _keyStartColum; _colum < _rowArray.Length; ++_colum)
                {
                    var _tempKeyDataIndex = _colum - _keyStartColum;
                    if (_tempKeyDataIndex >= mKeyDataList.Count)
                    {
                        break;
                    }
                    var _newCellData = new CellValueData();
                    _newList.Add(_newCellData);
                    var _value = _rowArray[_colum];
                    _newCellData.Init(
                        _value.ToString(),
                        _row,
                        _colum,
                        _row - _contentStartRow,
                        _colum - _keyStartColum,
                        mKeyDataList[_colum - _keyStartColum]
                    );
                }
            }

            mHasLoadAllCellData = true;

            return true;
        }
    }
}