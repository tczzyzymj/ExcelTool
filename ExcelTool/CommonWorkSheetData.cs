using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public abstract class CommonWorkSheetData
    {
        protected bool mHasLoadAllCellData = false;

        protected bool mHasInitKey = false;

        protected WeakReference<FileDataBase>? mOwnerTable = null;

        protected List<KeyData> mKeyDataList = new List<KeyData>();

        protected List<List<CellValueData>>? mCellData2DList = null; // 1维 是行， 2维是列

        protected string mSheetName = string.Empty;

        public int IndexInListForShow
        {
            get;
            protected set;
        } = 0;

        protected int mIndexInFileData = 1;

        public string DisplayName
        {
            get;
            protected set;
        } = string.Empty;

        /// <summary>
        /// 通过对比指定的列的值
        /// </summary>
        /// <param name="matchKeyList"></param>
        /// <param name="matchValueList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual List<CellValueData>? GetRowDataByTargetKeysAndValus(List<KeyData> matchKeyList, List<CellValueData> matchValueList)
        {
            if (mCellData2DList == null || mCellData2DList.Count < 1)
            {
                throw new Exception("GetListCellDataBySpecificKeysAndValues, mCellData2DList 为空");
            }

            if (matchKeyList == null || matchKeyList.Count < 1)
            {
                throw new Exception("GetListCellDataBySpecificKeysAndValues, 传入的 List<KeyData> matchKeyList 数据为空");
            }

            if (matchValueList == null || matchValueList.Count < 1)
            {
                throw new Exception("GetListCellDataBySpecificKeysAndValues, 传入的 List<CellValueData> matchValueList 数据为空");
            }

            if (matchValueList.Count != matchValueList.Count)
            {
                throw new Exception("GetListCellDataBySpecificKeysAndValues, 传入的数据数量不一致，请检查");
            }

            List<CellValueData> _result = null;

            for (int _rowIndex = 0; _rowIndex < mCellData2DList.Count; ++_rowIndex)
            {
                var _rowData = mCellData2DList[_rowIndex];

                bool _isMatch = true;
                foreach (var _matchKey in matchKeyList)
                {
                    for (int j = 0; j < matchValueList.Count; ++j)
                    {
                        if (!string.Equals(
                                _rowData[_matchKey.GetKeyColumIndexInList()].GetCellValue(),
                                matchValueList[j].GetCellValue())
                            )
                        {
                            _isMatch = false;
                            break;
                        }
                    }

                    if (!_isMatch)
                    {
                        break;
                    }
                }

                if (_isMatch)
                {
                    _result = _rowData;
                    break;
                }
            }

            return _result;
        }

        public int GetIndexInFileData()
        {
            return mIndexInFileData;
        }

        public List<List<CellValueData>>? GetFilteredDataList(Dictionary<KeyData, List<FilterFuncBase>> filterDataMap)
        {
            if (mCellData2DList == null)
            {
                return null;
            }

            if (filterDataMap == null || filterDataMap.Count < 1)
            {
                return mCellData2DList;
            }

            List<List<CellValueData>> _result = new List<List<CellValueData>>(mCellData2DList.Count);

            var _keyDataList = GetKeyListData();

            Dictionary<KeyData, List<FilterFuncBase>> _tempFilterMap = new Dictionary<KeyData, List<FilterFuncBase>>();

            for (int i = 0; i < _keyDataList.Count; ++i)
            {
                if (filterDataMap.TryGetValue(_keyDataList[i], out var _tempValue))
                {
                    _tempFilterMap.Add(_keyDataList[i], _tempValue);
                }
            }

            for (int _row = 0; _row < mCellData2DList.Count; ++_row)
            {
                var _rowData = mCellData2DList[_row];
                bool _match = true;
                foreach (var _pair in _tempFilterMap)
                {
                    foreach (var _filterFunc in _pair.Value)
                    {
                        if (!_filterFunc.IsMatchFilter(_rowData[_pair.Key.GetKeyColumIndexInList()].GetCellValue()))
                        {
                            _match = false;

                            break;
                        }
                    }
                }

                if (_match)
                {
                    _result.Add(_rowData);
                }
            }

            return _result;
        }

        public virtual bool WriteData(List<List<CellValueData>> filteredInDataList)
        {
            var _keyListData = GetKeyListData();
            if (_keyListData == null)
            {
                MessageBox.Show("WriteData ，但是 KeyList 为空，请检查", "错误");

                return false;
            }

            // 这里检查一下，看是否至少有一个是设置关联了的

            return true;
        }

        public virtual void ReloadKey()
        {
            mKeyDataList.Clear();
        }

        protected abstract bool InternalInitWithKey(object sheetData, bool isForce);

        public FileDataBase? GetOwnerTable()
        {
            if (mOwnerTable == null || !mOwnerTable.TryGetTarget(out var _result))
            {
                return null;
            }

            return _result;
        }

        // 注意，这里只初始化 Key
        public bool Init(WeakReference<FileDataBase> ownerExcelFile, object sheetData, int indexInListValue, int indexInFileData, string name)
        {
            mIndexInFileData = indexInFileData;
            mOwnerTable = ownerExcelFile;
            mSheetName = name;
            IndexInListForShow = indexInListValue + 1;
            DisplayName = $"{IndexInListForShow} : {name}";
            mKeyDataList.Clear();
            return InternalInitWithKey(sheetData, false);
        }

        public List<KeyData> GetKeyListData()
        {
            return mKeyDataList;
        }

        public bool LoadAllCellData()
        {
            if (!mHasLoadAllCellData)
            {
                return false;
            }

            return InternalLoadAllCellData();
        }

        protected abstract bool InternalLoadAllCellData();

        protected virtual bool AddNewKeyData(int indexInList, int indexInSheetData, string nameValue)
        {
            var _existData = mKeyDataList.Find((x) => x.GetKeyColumIndexInList() == indexInList);
            if (_existData != null)
            {
                MessageBox.Show($"已经存在相同的 indexInList : {indexInList}, 内容分别是：{_existData.KeyName} {nameValue}");
                return false;
            }

            _existData = mKeyDataList.Find((x) => x.GetKeyIndexInSheetData() == indexInSheetData);
            if (_existData != null)
            {
                MessageBox.Show($"已经存在相同的 indexInSheetData : {indexInSheetData}, 内容分别是：{_existData.KeyName} {nameValue}");
                return false;
            }

            var _newData = new KeyData();
            _newData.Init(indexInList, indexInSheetData, nameValue, new WeakReference<CommonWorkSheetData>(this));
            mKeyDataList.Add(_newData);
            return true;
        }
    }
}
