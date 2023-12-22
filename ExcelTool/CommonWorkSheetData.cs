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

        protected WeakReference<TableBaseData>? mOwnerTable = null;

        protected List<KeyData> mKeyDataList = new List<KeyData>();

        protected List<List<CellValueData>>? mCellData2DList = null; // 1维 是行， 2维是列


        /// <summary>
        /// 这里是通过 KEY 可以索引的，原始数据里面都是 string，就直接保存string了
        /// </summary>
        protected Dictionary<string, List<CellValueData>> mAllDataMap = new Dictionary<string, List<CellValueData>>();

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
        /// 
        /// </summary>
        /// <param name="IDStr"></param>
        /// <returns></returns>
        public virtual List<CellValueData>? GetListCellDataByID(string IDStr)
        {
            mAllDataMap.TryGetValue(IDStr, out var cellData);
            return cellData;
        }

        public int GetIndexInFileData()
        {
            return mIndexInFileData;
        }

        /// <summary>
        /// 从key 里面移除一个 筛选方法
        /// </summary>
        /// <param name="keyIndex"></param>
        /// <returns></returns>
        public bool RemoveFilterFromKey(int keyIndex, int filterIndex)
        {
            if (keyIndex < 0 || keyIndex >= mFilterDataMap.Count)
            {
                return false;
            }

            mFilterDataMap[keyIndex].RemoveAt(filterIndex);

            return true;
        }

        public List<List<CellValueData>>? GetFilteredDataList()
        {
            if (mCellData2DList == null)
            {
                return null;
            }
            List<int> _checkColumIndexList = new List<int>();

            foreach (var _pair in mFilterDataMap)
            {
                _checkColumIndexList.Add(_pair.Key);
            }

            List<List<CellValueData>> _result = new List<List<CellValueData>>(mCellData2DList.Count);
            for (int _row = 0; _row < mCellData2DList.Count; ++_row)
            {
                var _rowData = mCellData2DList[_row];
                bool _match = true;
                foreach (var _columIndex in _checkColumIndexList)
                {
                    foreach (var _filterFunc in mFilterDataMap[_columIndex])
                    {
                        if (!_filterFunc.IsMatchFilter(_rowData[_columIndex].GetCellValue()))
                        {
                            _match = false;
                            break;
                        }
                    }

                    if (!_match)
                    {
                        break;
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

        public TableBaseData? GetOwnerTable()
        {
            if (mOwnerTable == null || !mOwnerTable.TryGetTarget(out var _result))
            {
                return null;
            }

            return _result;
        }

        // 注意，这里只初始化 Key
        public bool Init(WeakReference<TableBaseData> ownerExcelFile, object sheetData, int indexInListValue, int indexInFileData, string name)
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
                MessageBox.Show($"已经存在相同的 indexInList : {indexInList}, 内容分别是：{_existData.GetKeyName()} {nameValue}");
                return false;
            }

            _existData = mKeyDataList.Find((x) => x.GetKeyIndexInSheetData() == indexInSheetData);
            if (_existData != null)
            {
                MessageBox.Show($"已经存在相同的 indexInSheetData : {indexInSheetData}, 内容分别是：{_existData.GetKeyName()} {nameValue}");
                return false;
            }

            var _newData = new KeyData();
            _newData.Init(indexInList, indexInSheetData, nameValue, new WeakReference<CommonWorkSheetData>(this));
            mKeyDataList.Add(_newData);
            return true;
        }
    }
}
