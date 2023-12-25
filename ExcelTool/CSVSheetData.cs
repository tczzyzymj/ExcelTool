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
        private List<string[]> mAllSheetData;

        public override void ReloadKey()
        {
            base.ReloadKey();
            InternalInitWithKey(mAllSheetData, true);
        }

        public override bool WriteOneData(int rowIndexInSheet, Dictionary<KeyData, string> valueMap, bool isNewData)
        {
            if (!base.WriteOneData(rowIndexInSheet, valueMap, isNewData))
            {
                return false;
            }

            var _keyListData = GetKeyListData();
            if (_keyListData == null)
            {
                MessageBox.Show("WriteData ，但是 KeyList 为空，请检查", "错误");

                return false;
            }

            if (isNewData)
            {
            }
            else
            {
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

            mAllSheetData.Add(_keyArray);

            var _ownerTable = GetOwnerTable() as CSVFileData;
            if (_ownerTable == null)
            {
                MessageBox.Show("表格数据为空，请检查!", "错误");
                return false;
            }

            // 这里是 key，初始化的时候只初始化一下 key，内容放到后面的解析去做
            {
                var _keyLine = mAllSheetData[0];

                for (int _i = _ownerTable.GetKeyStartColmIndex(); _i < _keyLine.Length; ++_i)
                {
                    AddNewKeyData(_i - _ownerTable.GetKeyStartColmIndex(), _i, _keyLine[_i]);
                }
            }

            InternalLoadAllCellData();

            mHasInitKey = true;

            return true;
        }

        protected override bool InternalLoadAllCellData()
        {
            if (mHasLoadAllCellData)
            {
                return true;
            }

            var _filePath = GetOwnerTable().GetFileAbsulotePath();

            using (StreamReader sr = new StreamReader(_filePath))
            {
                using (CsvReader _csvReader = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    if (_csvReader.Read())
                    {
                        var _allArray = _csvReader.GetRecords<string>().ToArray();

                        int a = 0;
                    }
                }
            }

            var _ownerTable = GetOwnerTable() as CSVFileData;
            if (_ownerTable == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取父 Table ，请检查！", "错误");
                return false;
            }

            if (mAllSheetData == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取 SheetData，请检查！", "错误");
                return false;
            }

            var _contentStartRow = _ownerTable.GetContentStartRowIndex();
            var _keyStartColum = _ownerTable.GetKeyStartColmIndex();

            mCellData2DList?.Clear();

            mCellData2DList = new List<List<CellValueData>>(mAllSheetData.Count - _contentStartRow);

            for (int _row = _contentStartRow; _row < mAllSheetData.Count; ++_row)
            {
                var _newList = new List<CellValueData>();
                mCellData2DList.Add(_newList);

                var _rowArray = mAllSheetData[_row];

                for (int _colum = _keyStartColum; _colum < _rowArray.Length; ++_colum)
                {
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
