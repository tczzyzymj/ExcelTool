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
        protected bool mHasInit = false;

        protected WeakReference<TableBaseData>? mExcelFileBase = null;

        protected List<KeyData> mKeyDataList = new List<KeyData>();

        protected List<List<KeyData>>? mCellData2DList = null;

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


        public int GetIndexInFileData()
        {
            return mIndexInFileData;
        }

        protected abstract bool InternalInitWithKey(TableBaseData ownerExcelFile, object sheetData, int IndexInListValue, int indexInFileData, string name);

        // 注意，这里只初始化 Key
        public bool Init(TableBaseData ownerExcelFile, object sheetData, int indexInListValue, int indexInFileData, string name)
        {
            mIndexInFileData = indexInFileData;
            mSheetName = name;
            IndexInListForShow = indexInListValue + 1;
            DisplayName = $"{IndexInListForShow} : {name}";
            mKeyDataList.Clear();
            return InternalInitWithKey(ownerExcelFile, sheetData, indexInListValue, indexInFileData, name);
        }

        public List<KeyData> GetKeyListData()
        {
            return mKeyDataList;
        }

        public bool LoadAllCellData()
        {
            if (!mHasInit)
            {
                return false;
            }

            return true;
        }

        protected abstract bool InternalLoadAllCellData();

        protected virtual bool AddNewKeyData(int indexInList, int indexInSheetData, string nameValue)
        {
            var _existData = mKeyDataList.Find((x) => x.GetKeyIndexInList() == indexInList);
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
            _newData.Init(indexInList, indexInSheetData, nameValue);
            mKeyDataList.Add(_newData);
            return true;
        }
    }
}
