using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExcelTool
{
    internal class ExcelSheetData : CommonWorkSheetData
    {
        protected ExcelWorksheet? mOriginSheetData = null; // 原始数据

        public override void ReloadKey()
        {
            base.ReloadKey();
            InternalInitWithKey(mOriginSheetData, true);
        }

        public override bool WriteData(List<List<CellValueData>> sourceFilteredDataList)
        {
            if (!base.WriteData(sourceFilteredDataList))
            {
                return false;
            }

            if (mOriginSheetData == null)
            {
                MessageBox.Show("ExcelSheetData.WriteData ，但是 ExcelWorksheet 数据为空，请检查", "错误");
                return false;
            }

            var _keyListData = GetKeyListData();
            if (_keyListData == null)
            {
                MessageBox.Show("ExcelSheetData.WriteData ，但是 KeyList 为空，请检查", "错误");

                return false;
            }

            var _ownerTable = GetOwnerTable();
            if (_ownerTable == null)
            {
                MessageBox.Show("ExcelSheetData.WriteData 时，GetOwnerTable 为空，请检查", "错误");
                return false;
            }

            int _contentStartRow = _ownerTable.GetContentStartRowIndex();

            var _writeType = TableDataManager.Instance().ExportWriteWayType;
            switch (_writeType)
            {
                case MainTypeDefine.ExportWriteWayType.Append:
                {
                    _contentStartRow = mOriginSheetData.Dimension.Rows + 1;
                    break;
                }
                case MainTypeDefine.ExportWriteWayType.OverWriteAll:
                {
                    break;
                }
                default:
                {
                    MessageBox.Show($"WriteData 时，选择了 ExportWriteWayType 未实现的类型：{_writeType}", "错误");
                    return false;
                }
            }

            for (int i = 0; i < sourceFilteredDataList.Count; ++i)
            {
                for (int j = 0; j < _keyListData.Count; ++j)
                {

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
                MessageBox.Show("传入的 ExcelFileBase 为空，无法初始化，请检查！", "错误");
                return false;
            }

            if (sheetData == null)
            {
                MessageBox.Show("传入的 ExcelWorksheet 为空，无法初始化，请检查！", "错误");
                return false;
            }
            var _finalData = sheetData as ExcelWorksheet;
            mOriginSheetData = _finalData;
            if (mOriginSheetData == null)
            {
                MessageBox.Show("传入的 Data 数据无法解析为 ExcelWorksheet，请检查", "错误");
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

            var _rowIndex = _ownerExcel.GetKeyStartRowIndex();

            for (int _colm = _ownerExcel.GetKeyStartColmIndex(); _colm <= mOriginSheetData.Dimension.Columns; ++_colm)
            {
                var _tempValue = mOriginSheetData.Cells[_rowIndex, _colm].Value;
                if (_tempValue == null)
                {
                    MessageBox.Show($"表格的 key 有空列，请检查，文件：{_ownerExcel.GetFileName(true)}, sheet:{mOriginSheetData.Name}，行：{_rowIndex}, 列：{_colm}", "错误");
                    return false;
                }
                string? _finalStr = _tempValue as string;
                if (string.IsNullOrEmpty(_finalStr))
                {
                    MessageBox.Show($"表格的 key 有空列，请检查，文件：{_ownerExcel.GetFileName(true)}, sheet:{mOriginSheetData.Name}", "错误");
                    return false;
                }

                AddNewKeyData(_colm - _ownerExcel.GetKeyStartColmIndex(), _colm, _finalStr);
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

            if (mOriginSheetData == null)
            {
                MessageBox.Show("InternalLoadAllCellData 无法获取 SheetData，请检查！", "错误");
                return false;
            }

            mAllDataMap.Clear();
            mCellData2DList?.Clear();

            var _contentStartRow = _ownerTable.GetContentStartRowIndex();
            var _keyStartColum = _ownerTable.GetKeyStartColmIndex();

            mCellData2DList = new List<List<CellValueData>>(mOriginSheetData.Dimension.Rows - 4);

            var _IDIndex = _ownerTable.IDIndex;

            StringBuilder _errorMsgBuilder = new StringBuilder();

            for (int _row = _contentStartRow; _row <= mOriginSheetData.Dimension.Rows; ++_row)
            {
                var _newList = new List<CellValueData>();
                mCellData2DList.Add(_newList);

                for (int _colum = _keyStartColum; _colum <= mOriginSheetData.Dimension.Columns; ++_colum)
                {
                    var _newCellData = new CellValueData();
                    _newList.Add(_newCellData);
                    var _value = mOriginSheetData.Cells[_row, _colum].Value;
                    var _stringValue = _value == null ? string.Empty : _value.ToString();
                    _newCellData.Init(
                        _stringValue,
                        _row,
                        _colum,
                        _row - _contentStartRow,
                        _colum - _keyStartColum,
                        mKeyDataList[_colum - _keyStartColum]
                    );

                    if (_colum == _IDIndex)
                    {
                        if (string.IsNullOrEmpty(_stringValue))
                        {
                            _errorMsgBuilder.Append($"尝试添加Key，但 Key 为空，请检查， Sheet 名字：[{this.DisplayName}], 表名:[{GetOwnerTable()?.DisplayName}]");
                        }
                        else
                        {
                            var _keyStr = _stringValue.Trim();
                            if (mAllDataMap.ContainsKey(_keyStr))
                            {
                                _errorMsgBuilder.Append($"存在相同的KEY：{_stringValue} 请检查");
                                _errorMsgBuilder.Append("\r\n");
                            }
                            else
                            {
                                mAllDataMap.Add(_keyStr, _newList);
                            }
                        }
                    }
                }
            }

            var _errorMsg = _errorMsgBuilder.ToString();
            if (!string.IsNullOrEmpty(_errorMsg))
            {
                MessageBox.Show(_errorMsg, "加载有错");
            }

            mHasLoadAllCellData = true;

            return true;
        }
    }
}
