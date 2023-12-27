using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class KeyData
    {
        // 下标为0，在 worksheet 的 keydataList 里面的下标
        private int mKeyIndexInDatList = 0;

        public string KeyName
        {
            get;
            private set;
        } = string.Empty;

        public string KeyNameWithIndex
        {
            get;
            private set;
        } = string.Empty;

        public int KeyIndexInList
        {
            get;
            private set;
        } = 0;

        // 注意，这个是内部用的，因为 excel 的数据是从下标1开始的
        private int mKeyColumIndexInSheetData = 1;

        public int mKeyColumIndexForShow = 0;

        private WeakReference<CommonWorkSheetData>? mOwnerSheet = null;

        /// <summary>
        /// 导出的时候，是否忽略
        /// </summary>
        public bool IsIgnore
        {
            get;
            set;
        } = true; // TODO，记得删除，测试用，默认是不忽略的

        /// <summary>
        /// 是否为主KEY，如果是主KEY，并且没有指定关联数据，那么ID会以最大ID为基准+1，并且会作为判断是否冲突的
        /// </summary>
        public bool IsMainKey
        {
            get;
            set;
        }

        public void Init(
            int indexInList,
            int indexInSheetData,
            string nameValue,
            WeakReference<CommonWorkSheetData> ownerSheet
        )
        {
            mKeyIndexInDatList = indexInList;
            KeyName = nameValue;
            KeyIndexInList = indexInList;

            mKeyColumIndexInSheetData = indexInSheetData;
            mKeyColumIndexForShow = mKeyIndexInDatList + 1;

            KeyNameWithIndex = $"{CommonUtil.GetZM(mKeyColumIndexForShow)} : {KeyName}";

            mOwnerSheet = ownerSheet;
        }

        public CommonWorkSheetData? GetOwnerSheet()
        {
            if (mOwnerSheet == null)
            {
                MessageBox.Show($"Key :{KeyName} 无法获取 Sheet，请检查", "错误");
                return null;
            }

            mOwnerSheet.TryGetTarget(out var _result);

            return _result;
        }

        public int GetKeyIndexInDataList()
        {
            return mKeyIndexInDatList;
        }

        public int GetKeyIndexForShow()
        {
            return mKeyColumIndexForShow;
        }

        public int GetKeyIndexInSheetData()
        {
            return mKeyColumIndexInSheetData;
        }

        public FileDataBase? GetOwnerTable()
        {
            if (mOwnerSheet == null || !mOwnerSheet.TryGetTarget(out CommonWorkSheetData? _targetSheet))
            {
                return null;
            }

            var _ownerTable = _targetSheet.GetOwnerTable();

            return _ownerTable;
        }

        public string GetOwnerTableName(bool isFullName)
        {
            var _ownerTable = GetOwnerTable();

            if (_ownerTable == null)
            {
                return string.Empty;
            }

            return _ownerTable.GetFileName(isFullName);
        }

        public string GetOwnerSheetName()
        {
            if (mOwnerSheet == null)
            {
                return string.Empty;
            }

            if (mOwnerSheet.TryGetTarget(out CommonWorkSheetData? _targetSheet))
            {
                return _targetSheet.DisplayName;
            }

            return string.Empty;
        }
    }
}
