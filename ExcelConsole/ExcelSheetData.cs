using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExcelTool
{
    public class ExcelSheetData : CommonWorkSheetData
    {
        public ExcelSheetData()
        {
            mKeyStartRowIndexInSheet = 2; //  行，其开始的行下标，从1开始，不是0
            mKeyStartColmIndexInSheet = 1; // 列，其开始的列下标，从1开始，不是0
            mContentStartRowIndexInSheet = 4; // 内容选中的行下标，从1开始，不是0
        }

        protected ExcelWorksheet? mOriginSheetData = null; // 原始数据

        public override void ReloadKey()
        {
            base.ReloadKey();
            InternalInitWithKey(mOriginSheetData, true);
        }

        public override void CleanAllContent()
        {
            base.CleanAllContent();
            if (mOriginSheetData == null)
            {
                throw new Exception($"{CleanAllContent} 出错，mOriginSheetData 为空");
            }
            var _ownerTable = GetOwnerTable();
            if (_ownerTable == null)
            {
                throw new Exception($"{CleanAllContent} 出错，_ownerTable 为空");
            }

            var _contentStartIndex = GetContentStartRowIndexInSheet();

            mOriginSheetData.DeleteRow(_contentStartIndex, mOriginSheetData.Dimension.Rows);
        }

        public override bool SaveSheet()
        {
            if (mCellData2DList == null || mCellData2DList.Count < 1)
            {
                return false;
            }

            if (mOriginSheetData == null)
            {
                CommonUtil.ShowError("ExcelSheetData.WriteData ，但是 ExcelWorksheet 数据为空，请检查");
                return false;
            }

            foreach (var _rowData in mCellData2DList)
            {
                for (int i = 0; i < _rowData.Count; ++i)
                {
                    mOriginSheetData.Cells[
                        _rowData[0].GetCellRowIndexInSheet(),
                        _rowData[i].GetCellColumnIndexInSheet()
                    ].Value = _rowData[i].GetCellValue();
                }
            }

            return true;
        }
        public override bool WriteOneData(int rowIndexInSheet, List<string> inValueList, bool skipEmptyData)
        {
            if (!base.WriteOneData(rowIndexInSheet, inValueList, skipEmptyData))
            {
                return false;
            }

            if (mOriginSheetData == null)
            {
                CommonUtil.ShowError("ExcelSheetData.WriteData ，但是 ExcelWorksheet 数据为空，请检查");
                return false;
            }

            var _keyList = GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                throw new Exception($"{nameof(ExcelSheetData)} : {WriteOneData} 出错，_keyList 无效");
            }

            var _targetRowIndex = rowIndexInSheet;
            if (_targetRowIndex < 0)
            {
                _targetRowIndex = mOriginSheetData.Dimension.Rows + 1;
            }
            else
            {
                // 这里去检测一下看下标是否对
                if (rowIndexInSheet < 1 || rowIndexInSheet > mOriginSheetData.Dimension.Rows)
                {
                    throw new Exception($"{nameof(ExcelSheetData)} : {WriteOneData} 出错，rowIndexInSheet :{rowIndexInSheet} 无效");
                }
            }

            for (int _i = 0; _i < _keyList.Count; ++_i)
            {
                if (_i >= inValueList.Count)
                {
                    break;
                }

                var _tempContent = inValueList[_i];

                if (skipEmptyData)
                {
                    if (!string.IsNullOrEmpty(_tempContent))
                    {
                        mOriginSheetData.Cells[_targetRowIndex, _keyList[_i].GetKeyIndexInSheetData()].Value = _tempContent;
                    }
                }
                else
                {
                    mOriginSheetData.Cells[_targetRowIndex, _keyList[_i].GetKeyIndexInSheetData()].Value = _tempContent;
                }
            }

            return true;
        }

        protected override bool InternalInitWithKey(object? sheetData, bool isForce)
        {
            if (mHasInitKey && !isForce)
            {
                return true;
            }

            if (mOwnerTable == null)
            {
                CommonUtil.ShowError("传入的 ExcelFileBase 为空，无法初始化，请检查！");
                return false;
            }

            if (sheetData == null)
            {
                CommonUtil.ShowError("传入的 ExcelWorksheet 为空，无法初始化，请检查！");
                return false;
            }
            var _finalData = sheetData as ExcelWorksheet;
            mOriginSheetData = _finalData;
            if (mOriginSheetData == null)
            {
                CommonUtil.ShowError("传入的 Data 数据无法解析为 ExcelWorksheet，请检查");
                return false;
            }

            if (mOriginSheetData.Dimension == null)
            {
                // 这个 sheet 里面的内容为空，还是要记录一下，所以返回true
                return true;
            }

            if (!mOwnerTable.TryGetTarget(out FileDataBase? _ownerExcel))
            {
                return false;
            }

            var _rowIndex = GetKeyStartRowIndexInSheet();

            for (int _colm = GetKeyStartColmIndexInSheet(); _colm <= mOriginSheetData.Dimension.Columns; ++_colm)
            {
                var _tempValue = mOriginSheetData.Cells[_rowIndex, _colm].Value;
                if (_tempValue == null)
                {
                    continue;
                }
                string? _finalStr = _tempValue as string;
                if (string.IsNullOrEmpty(_finalStr))
                {
                    continue;
                }

                AddNewKeyData(_colm - GetKeyStartColmIndexInSheet(), _colm, _finalStr);
            }

            mHasInitKey = true;

            return true;
        }

        protected override bool InternalLoadAllCellData(bool forceLoad)
        {
            if (mHasLoadAllCellData && !forceLoad)
            {
                return true;
            }
            var _ownerTable = GetOwnerTable();
            if (_ownerTable == null)
            {
                CommonUtil.ShowError("InternalLoadAllCellData 无法获取父 Table ，请检查！");
                return false;
            }

            if (mOriginSheetData == null)
            {
                CommonUtil.ShowError("InternalLoadAllCellData 无法获取 SheetData，请检查！");
                return false;
            }

            mCellData2DList?.Clear();

            var _contentStartRow = GetContentStartRowIndexInSheet();
            var _keyStartColum = GetKeyStartColmIndexInSheet();


            var _capcity = 0;
            if (mOriginSheetData.Dimension != null)
            {
                _capcity = mOriginSheetData.Dimension.Rows - 4;
            }

            if (_capcity > 3)
            {
                mCellData2DList = new List<List<CellValueData>>(_capcity);
            }
            else
            {
                mCellData2DList = new List<List<CellValueData>>();
            }

            StringBuilder _errorMsgBuilder = new StringBuilder();

            int _maxRowCount = 1;

            if (mOriginSheetData.Dimension != null)
            {
                _maxRowCount = mOriginSheetData.Dimension.Rows;
            }

            int _maxColumCount = 1;
            if (mOriginSheetData.Dimension != null)
            {
                _maxColumCount = mOriginSheetData.Dimension.Columns;
            }

            for (int _row = _contentStartRow; _row <= _maxRowCount; ++_row)
            {
                var _newList = new List<CellValueData>();
                mCellData2DList.Add(_newList);

                for (int _colum = _keyStartColum; _colum <= _maxColumCount; ++_colum)
                {
                    var _newCellData = new CellValueData();
                    _newList.Add(_newCellData);
                    var _tempCell = mOriginSheetData.Cells[_row, _colum];
                    if (_tempCell == null)
                    {
                        throw new Exception($"row : [{_row}] , col : [{_colum}] 为空，请检查!");
                    }
                    var _value = _tempCell.Value;
                    var _stringValue = _value == null ? string.Empty : _value.ToString();
                    if (_stringValue == null)
                    {
                        _stringValue = string.Empty;
                    }
                    var _targetIndex = _colum - _keyStartColum;
                    if (_targetIndex < 0)
                    {
                        throw new Exception(
                            $"_colum : [{_colum}] - _keyStartColum : [{_keyStartColum}] < 0 , 请检查,sheet:{DisplayName} ， 表格 : {GetOwnerTable()?.GetFileName(false)}"
                        );
                    }
                    if (_targetIndex >= mKeyDataList.Count)
                    {
                        throw new Exception(
                            $"_colum : [{_colum}] - _keyStartColum : [{_keyStartColum}] >= mKeyDataList.Count : [{mKeyDataList.Count}], 请检查，sheet:{DisplayName} ， 表格 : {GetOwnerTable()?.GetFileName(false)}"
                        );
                    }
                    _newCellData.Init(
                        _stringValue,
                        _row,
                        _colum,
                        _row - _contentStartRow,
                        _colum - _keyStartColum,
                        mKeyDataList[_targetIndex]
                    );
                }
            }

            var _errorMsg = _errorMsgBuilder.ToString();
            if (!string.IsNullOrEmpty(_errorMsg))
            {
                CommonUtil.ShowError(_errorMsg);
            }

            mHasLoadAllCellData = true;

            return true;
        }
    }
}
