using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    // 这里是最基本的类，后续自己分一下
    public abstract class FileDataBase
    {
        [JsonProperty]
        protected string mExcelAbsolutePath = string.Empty;

        [JsonProperty]
        protected int mKeyStartRowIndex = 2; // Key 的概念认为是数据列的名字，其开始的行下标，从1开始，不是0

        [JsonProperty]
        protected int mKeyStartColmIndex = 1; // Key 的概念认为是数据列的名字，其开始的列下标，从1开始，不是0

        [JsonProperty]
        protected int mContentStartRowIndex = 4; // 内容选中的行下标，从2开始，认为1是KEY不能小于2

        public LoadFileType FileType
        {
            get;
            private set;
        } = LoadFileType.NormalFile;

        [JsonProperty]
        private int mChooseWorkSheetIndexInList = 0; // 选中的workSheet需要处理的 workdsheet

        protected List<CommonWorkSheetData> mWorkSheetList = new List<CommonWorkSheetData>();

        protected int mChooseSheetIndex = 0;

        protected string mChooseSheetName = string.Empty;

        // combobox显示用的index
        public int DisplayIndex
        {
            get;
            set;
        }

        // combobox显示用的名字
        public string DisplayName
        {
            get;
            set;
        } = string.Empty;

        protected bool mHasInit = false;

        public bool DoLoadFile(string absolutePath, LoadFileType fileType)
        {
            if (!File.Exists(absolutePath))
            {
                return false;
            }

            if (!InternalLoadFile(absolutePath))
            {
                return false;
            }
            FileType = fileType;
            mExcelAbsolutePath = absolutePath;
            mHasInit = true;
            return true;
        }

        public virtual bool WriteData(List<List<CellValueData>> inRowDataList, int workSheetIndex)
        {
            if (inRowDataList == null || inRowDataList.Count < 1)
            {
                throw new Exception($"{WriteData} 数据无效，请检查");
            }
            if (workSheetIndex < 0 || workSheetIndex >= mWorkSheetList.Count)
            {
                throw new Exception($"{WriteData}下标无效");
            }

            var _keyActionMap = TableDataManager.Ins().ExportKeyActionMap;
            if (_keyActionMap.Count < 1)
            {
                throw new Exception($"{WriteData} 出错，未配置 ExportKeyActionMap，请检查");
            }

            var _currentKeyList = mWorkSheetList[workSheetIndex].GetKeyListData();
            if (_currentKeyList == null)
            {
                throw new Exception($"{WriteData} 出错，无法获取当前数据表格的 KeyList，请检查!");
            }

            List<string> _writeRowData = new List<string>(_currentKeyList.Count);
            List<CellValueData> _tempMatchValueList = new List<CellValueData>();
            foreach (var _singleRow in inRowDataList)
            {
                _writeRowData.Clear();

                foreach (var _singleKey in _currentKeyList)
                {
                    if (!_keyActionMap.TryGetValue(_singleKey, out var _action))
                    {
                        _writeRowData.Add(string.Empty);
                        continue;
                    }

                    _tempMatchValueList.Add(_singleRow[_singleKey.GetKeyColumIndexInList()]);

                    _writeRowData.Add(_action.TryProcessData(_tempMatchValueList));
                }

                // 这里直接去写入一个新的
            }

            return true;
        }

        public abstract void SaveFile();

        public bool GetHasInit()
        {
            return mHasInit;
        }

        public List<List<CellValueData>>? GetFilteredDataList(Dictionary<KeyData, List<FilterFuncBase>> filterDataMap)
        {
            var _currentSheet = GetCurrentWorkSheet();

            return _currentSheet?.GetFilteredDataList(filterDataMap);
        }

        public string GetFilePath()
        {
            return mExcelAbsolutePath;
        }

        /// <summary>
        /// 注意，下标是从0开始
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CommonWorkSheetData? GetWorkSheetByIndex(int index)
        {
            if (index < 0 || index >= mWorkSheetList.Count)
            {
                index = 0;
            }

            return mWorkSheetList[index];
        }

        public List<CommonWorkSheetData> GetWorkSheetList()
        {
            return mWorkSheetList;
        }

        public virtual string GetFileName(bool isFull)
        {
            if (isFull)
            {
                return mExcelAbsolutePath;
            }
            else
            {
                return Path.GetFileName(mExcelAbsolutePath);
            }
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

        public void LoadAllCellDataForCurrentSheet()
        {
            if (mCurrentWorkSheet == null)
            {
                MessageBox.Show("当前未选中 WorkSheet ，请检查！");
                return;
            }

            mCurrentWorkSheet.LoadAllCellData();
        }

        public abstract bool InternalLoadFile(string absolutePath);
    }
}
