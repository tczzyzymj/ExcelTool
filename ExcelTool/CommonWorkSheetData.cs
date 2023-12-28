using Newtonsoft.Json;
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
        [JsonProperty]
        protected int mKeyStartRowIndex = 2; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0

        [JsonProperty]
        protected int mKeyStartColmIndex = 1; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0

        [JsonProperty]
        protected int mContentStartRowIndex = 4; // 内容选中的行下标，从2开始，认为1是KEY不能小于2

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
        public virtual List<CellValueData>? GetRowCellDataByTargetKeysAndValus(List<int> matchKeyList, List<string> matchValueList)
        {
            if (mCellData2DList == null)
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

            for (int _rowIndex = 0; _rowIndex < mCellData2DList.Count; ++_rowIndex)
            {
                var _rowData = mCellData2DList[_rowIndex];

                int _matchCount = 0;

                for (int i = 0; i < matchKeyList.Count; ++i)
                {
                    var _matchKeyIndex = matchKeyList[i];
                    var _matchKeyCellValue = _rowData[_matchKeyIndex].GetCellValue().Trim();

                    foreach (var _matchTargetStr in matchValueList)
                    {
                        if (string.Equals(_matchKeyCellValue, _matchTargetStr.Trim()))
                        {
                            ++_matchCount;
                            break;
                        }
                    }

                    if (_matchCount == matchValueList.Count)
                    {
                        break;
                    }
                }

                if (_matchCount == matchValueList.Count)
                {
                    return _rowData;
                }
            }

            return null;
        }

        /// <summary>
        /// 通过对比指定的列的值
        /// </summary>
        /// <param name="matchKeyList"></param>
        /// <param name="matchValueList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual List<string> GetRowStringDataByTargetKeysAndValus(List<int> matchKeyList, List<string> matchValueList)
        {
            List<string> _result = new List<string>();

            var _rowData = GetRowCellDataByTargetKeysAndValus(matchKeyList, matchValueList);
            if (_rowData != null)
            {
                foreach (var _singleData in _rowData)
                {
                    _result.Add(_singleData.GetCellValue());
                }
            }

            return _result;
        }

        public int GetIndexInFileData()
        {
            return mIndexInFileData;
        }

        public int GetKeyStartRowIndex()
        {
            return mKeyStartRowIndex;
        }

        public void SetKeyStartRowIndex(int targetValue)
        {
            mKeyStartRowIndex = targetValue;
        }

        public int GetKeyStartColmIndex()
        {
            return mKeyStartColmIndex;
        }

        public void SetKeyStartColmIndex(int targetValue)
        {
            mKeyStartColmIndex = targetValue;
        }

        public int GetContentStartRowIndex()
        {
            return mContentStartRowIndex;
        }

        public void SetContentStartRowIndex(int targetValue)
        {
            mContentStartRowIndex = targetValue;
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
                        if (!_filterFunc.IsMatchFilter(_rowData[_pair.Key.GetKeyIndexInDataList()].GetCellValue()))
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

        public virtual void CleanAllContent()
        {
        }

        public virtual bool WriteOneData(int rowIndexInSheet, Dictionary<KeyData, string> valueMap, bool newData, bool skipEmptyData)
        {
            if (mCellData2DList == null)
            {
                throw new Exception($"{WriteOneData} 出错，mCellData2DList 为空");
            }

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

        public bool LoadAllCellData(bool forceLoad)
        {
            if (mHasLoadAllCellData && !forceLoad)
            {
                return false;
            }

            if (InternalLoadAllCellData(forceLoad))
            {
                mHasLoadAllCellData = true;

                return true;
            }

            return false;
        }

        protected abstract bool InternalLoadAllCellData(bool forceLoad);

        protected virtual bool AddNewKeyData(int indexInList, int indexInSheetData, string nameValue)
        {
            var _existData = mKeyDataList.Find((x) => x.GetKeyIndexInDataList() == indexInList);
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
