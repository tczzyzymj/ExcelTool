using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    internal class CSVSheetData : CommonWorkSheetData
    {
        public CSVSheetData()
        {
            mKeyStartRowIndex = 0; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0
            mKeyStartColmIndex = 0; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0
            mContentStartRowIndex = 3; // 内容选中的行下标，从2开始，认为1是KEY不能小于2
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
                        for (int i = 0; i <= mKeyStartRowIndex; ++i)
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
                    MessageBox.Show($"文件：{absolutePath} ，内容不正确，请检查");
                    return;
                }

                InternalInitWithKey(_keyArray, true);
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public override bool WriteOneData(int rowIndexInSheet, List<string> valueList, bool skipEmptyData)
        {
            throw new Exception("CSV写入功能完善中");
            if (!base.WriteOneData(rowIndexInSheet, valueList, skipEmptyData))
            {
                return false;
            }

            var _keyListData = GetKeyListData();
            if (_keyListData == null)
            {
                MessageBox.Show("WriteData ，但是 KeyList 为空，请检查", "错误");

                return false;
            }

            return true;
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
                MessageBox.Show("传入的数据为空，请检查!", "错误");
                return false;
            }

            var _ownerTable = GetOwnerTable() as CSVFileData;
            if (_ownerTable == null)
            {
                MessageBox.Show("表格数据为空，请检查!", "错误");
                return false;
            }

            // 这里是 key，初始化的时候只初始化一下 key，内容放到后面的解析去做
            {
                var _keyLine = _keyArray;

                for (int _i = GetKeyStartColmIndex(); _i < _keyLine.Length; ++_i)
                {
                    AddNewKeyData(_i - GetKeyStartColmIndex(), _i, _keyLine[_i]);
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
                MessageBox.Show("InternalLoadAllCellData 无法获取父 Table ，请检查！", "错误");
                return false;
            }

            var _contentStartRow = GetContentStartRowIndex();
            var _keyStartColum = GetKeyStartColmIndex();

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
                MessageBox.Show("InternalLoadAllCellData 无法获取 SheetData，请检查！", "错误");
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
