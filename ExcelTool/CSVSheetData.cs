using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    internal class CSVSheetData : CommonWorkSheetData
    {
        private string[]? mSheetData = null;

        public override void ReloadKey()
        {
            base.ReloadKey();
            InternalInitWithKey(mSheetData, true);
        }

        protected override bool InternalInitWithKey(object? sheetData, bool isForce)
        {
            if (mHasInitKey && !isForce)
            {
                return true;
            }

            mSheetData = sheetData as string[];

            if (mSheetData == null)
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
                var _splitArray = mSheetData[_ownerTable.GetKeyStartRowIndex()].Split(_ownerTable.SplitSymbol);

                for (int _i = _ownerTable.GetKeyStartColmIndex(); _i < _splitArray.Length; ++_i)
                {
                    AddNewKeyData(_i, _i, _splitArray[_i]);
                }
            }

            mHasInitKey = true;

            return true;
        }

        protected override bool InternalLoadAllCellData()
        {
            if (mHasLoadAllCellData)
            {
                return true;
            }

            var _ownerTable = GetOwnerTable();
            if (_ownerTable == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取父 Table ，请检查！", "错误");
                return false;
            }

            if (mSheetData == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取 SheetData，请检查！", "错误");
                return false;
            }

            var _contentStartRow = _ownerTable.GetContentStartRowIndex();
            var _keyStartColum = _ownerTable.GetKeyStartColmIndex();

            mCellData2DList = new List<List<CellValueData>>(mSheetData.Length - 4);

            for (int _row = _contentStartRow; _row < mSheetData.Length; ++_row)
            {
                var _newList = new List<CellValueData>();
                mCellData2DList.Add(_newList);

                var _rowArray = mSheetData[_row].Split(',');

                for (int _colum = _keyStartColum; _colum <= _rowArray.Length; ++_colum)
                {
                    var _newCellData = new CellValueData();
                    _newList.Add(_newCellData);
                    var _value = _rowArray[_colum];
                    _newCellData.Init(_value == null ? string.Empty : _value.ToString(), _row, _colum);
                }
            }


            mHasLoadAllCellData = true;

            return true;
        }
    }
}
