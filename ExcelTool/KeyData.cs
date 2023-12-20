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
        private int mKeyColumIndexInList = 0;

        private string mKeyName = string.Empty;

        // 注意，这个是内部用的，因为 excel 的数据是从下标1开始的
        private int mKeyColumIndexInSheetData = 1;

        private int mKeyColumIndexForShow = 0;

        // 目前源文件 source file 用，其他的目前用不到，用来筛选数据
        private List<FilterFuncBase> mFilterFuncList = new List<FilterFuncBase>();

        private WeakReference<KeyData>? mNextConnectKey = null; // 要避免循环引用，是基于表格的，每次设置关联之后，会检测一次

        private WeakReference<CommonWorkSheetData>? mOwnerSheet = null;

        /// <summary>
        /// 导出的时候，是否忽略
        /// </summary>
        public bool IsIgnore
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为主KEY，如果是主KEY，并且没有指定关联数据，那么ID会以最大ID为基准+1
        /// </summary>
        public bool IsMainKey
        {
            get;
            set;
        }

        /// <summary>
        /// 关联的表格的相对路径，用于序列化
        /// </summary>
        public string ConnectTableRelativePath
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// 关联的 Key 下标，用于序列化
        /// </summary>
        public int ConnectKeyIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 关联的 Key 名字，用于序列化
        /// </summary>
        public string ConnectKeyName
        {
            get;
            set;
        } = string.Empty;

        public void Init(
            int indexForShow,
            int indexInSheetData,
            string nameValue,
            WeakReference<CommonWorkSheetData> ownerSheet
        )
        {
            mKeyColumIndexInList = indexForShow;
            mKeyName = nameValue;
            mKeyColumIndexInSheetData = indexInSheetData;
            mKeyColumIndexForShow = mKeyColumIndexInList + 1;
            mOwnerSheet = ownerSheet;
        }

        public CommonWorkSheetData? GetOwnerSheet()
        {
            if (mOwnerSheet == null)
            {
                MessageBox.Show($"Key :{GetKeyName()} 无法获取 Sheet，请检查", "错误");
                return null;
            }

            mOwnerSheet.TryGetTarget(out var _result);

            return _result;
        }

        public int GetKeyColumIndexInList()
        {
            return mKeyColumIndexInList;
        }

        public bool IsMatchFilter(string? content)
        {
            if (mFilterFuncList.Count < 1)
            {
                return true;
            }

            for (int i = 0; i < mFilterFuncList.Count; ++i)
            {
                if (!mFilterFuncList[i].IsMatchFilter(content))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetKeyIndexForShow()
        {
            return mKeyColumIndexForShow;
        }

        public string GetKeyName()
        {
            return mKeyName;
        }

        public int GetKeyIndexInSheetData()
        {
            return mKeyColumIndexInSheetData;
        }

        public List<FilterFuncBase> GetFilterFuncList()
        {
            return mFilterFuncList;
        }

        public TableBaseData? GetOwnerTable()
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

        public string GetConnectInfo()
        {
            if (mNextConnectKey == null)
            {
                return string.Empty;
            }

            if (!mNextConnectKey.TryGetTarget(out KeyData? _targetKey))
            {
                return string.Empty;
            }

            return $"{_targetKey.GetOwnerTableName(false)}[{_targetKey.GetOwnerSheetName()}][{_targetKey.GetKeyName()}]";
        }

        // 避免循环引用，是基于表格的
        public bool SetNextConnectKey(WeakReference<KeyData> connectKey)
        {
            if (connectKey == null || !connectKey.TryGetTarget(out var _tempConnectKey))
            {
                MessageBox.Show("尝试 SetNextConnectKey，但传入的数据为空，请检查!", "错误");
                return false;
            }

            mNextConnectKey = connectKey;

            return true;
        }

        public void ClearNextConnectKey()
        {
            mNextConnectKey = null;
        }

        public KeyData? GetNextConnectKey()
        {
            if (mNextConnectKey == null)
            {
                return null;
            }

            mNextConnectKey.TryGetTarget(out var _nextKey);
            return _nextKey;
        }
    }
}
