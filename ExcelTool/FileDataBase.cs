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

            mExcelAbsolutePath = absolutePath; //后面有用到，这里还是先设置一下

            if (!InternalLoadFile(absolutePath))
            {
                mExcelAbsolutePath = string.Empty;
                return false;
            }

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
            var _currentSheet = mWorkSheetList[workSheetIndex];
            var _currentKeyList = _currentSheet.GetKeyListData();
            if (_currentKeyList == null)
            {
                throw new Exception($"{WriteData} 出错，无法获取当前数据表格的 KeyList，请检查!");
            }

            _currentSheet.LoadAllCellData(false);

            List<string> _theKeyCompareValue = new List<string>();
            List<KeyData> _theKeyList = new List<KeyData>();
            for (int i = 0; i < _currentKeyList.Count; ++i)
            {
                if (_currentKeyList[i].IsMainKey)
                {
                    _theKeyList.Add(_currentKeyList[i]);
                }
            }

            List<int> _theKeyInListIndexList = new List<int>(); //在 List 中的index ，不是 sheet 里面的index

            Dictionary<KeyData, string> _writeDataMap = new Dictionary<KeyData, string>();

            switch (TableDataManager.Ins().ExportWriteWayType)
            {
                case MainTypeDefine.ExportWriteWayType.Append:
                {
                    switch (TableDataManager.Ins().ExportConfigDealWayType)
                    {
                        case MainTypeDefine.ExportConflictDealWayType.UseNewData:
                        {
                            foreach (var _singleRow in inRowDataList)
                            {
                                _writeDataMap.Clear();
                                _theKeyCompareValue.Clear();
                                _theKeyInListIndexList.Clear();

                                foreach (var _singleKey in _currentKeyList)
                                {
                                    if (_singleKey.IsIgnore)
                                    {
                                        _writeDataMap.Add(_singleKey, string.Empty);
                                        continue;
                                    }

                                    if (!_keyActionMap.TryGetValue(_singleKey, out var _action))
                                    {
                                        throw new Exception($" Key : [{_singleKey.KeyName}] 没有忽略，并且也没有指定数据请检查");
                                    }

                                    var _dataAfterAction = _action.TryProcessData(_singleRow);

                                    _writeDataMap.Add(_singleKey, _dataAfterAction);
                                }

                                // 检测冲突
                                {
                                    foreach (var _theKey in _theKeyList)
                                    {
                                        _theKeyInListIndexList.Add(_theKey.GetKeyColumIndexInList());
                                        if (_writeDataMap.TryGetValue(_theKey, out var _tempValue))
                                        {
                                            _theKeyCompareValue.Add(_tempValue);
                                        }
                                    }

                                    if (_theKeyCompareValue.Count > 0)
                                    {
                                        var _rowData = _currentSheet.GetRowDataByTargetKeysAndValus(_theKeyInListIndexList, _theKeyCompareValue);
                                        if (_rowData != null && _rowData.Count > 0)
                                        {
                                            // 有冲突
                                            _currentSheet.WriteOneData(_rowData[0].GetCellRowIndexInSheet(), _writeDataMap, false);
                                        }
                                        else
                                        {
                                            // 没有冲突
                                            _currentSheet.WriteOneData(-1, _writeDataMap, true);
                                        }
                                    }
                                }
                            }

                            break;
                        }
                        case MainTypeDefine.ExportConflictDealWayType.UseOldData:
                        {
                            foreach (var _singleRow in inRowDataList)
                            {
                                _writeDataMap.Clear();
                                _theKeyCompareValue.Clear();
                                _theKeyInListIndexList.Clear();

                                foreach (var _singleKey in _currentKeyList)
                                {
                                    if (_singleKey.IsIgnore)
                                    {
                                        _writeDataMap.Add(_singleKey, string.Empty);
                                        continue;
                                    }

                                    if (!_keyActionMap.TryGetValue(_singleKey, out var _action))
                                    {
                                        throw new Exception($" Key : [{_singleKey.KeyName}] 没有忽略，并且也没有指定数据请检查");
                                    }

                                    var _dataAfterAction = _action.TryProcessData(_singleRow);

                                    _writeDataMap.Add(_singleKey, _dataAfterAction);
                                }

                                // 检测冲突
                                {
                                    foreach (var _theKey in _theKeyList)
                                    {
                                        _theKeyInListIndexList.Add(_theKey.GetKeyColumIndexInList());
                                        if (_writeDataMap.TryGetValue(_theKey, out var _tempValue))
                                        {
                                            _theKeyCompareValue.Add(_tempValue);
                                        }
                                    }

                                    if (_theKeyCompareValue.Count > 0)
                                    {
                                        var _rowData = _currentSheet.GetRowDataByTargetKeysAndValus(_theKeyInListIndexList, _theKeyCompareValue);
                                        if (_rowData == null)
                                        {
                                            // 没有冲突
                                            _currentSheet.WriteOneData(-1, _writeDataMap, true);
                                        }
                                    }
                                }
                            }

                            break;
                        }
                    }
                    break;
                }
                case MainTypeDefine.ExportWriteWayType.OverWriteAll:
                {
                    _currentSheet.CleanAllContent();

                    foreach (var _singleRow in inRowDataList)
                    {
                        _writeDataMap.Clear();
                        _theKeyCompareValue.Clear();
                        _theKeyInListIndexList.Clear();

                        foreach (var _singleKey in _currentKeyList)
                        {
                            if (_singleKey.IsIgnore)
                            {
                                _writeDataMap.Add(_singleKey, string.Empty);
                                continue;
                            }

                            if (!_keyActionMap.TryGetValue(_singleKey, out var _action))
                            {
                                throw new Exception($" Key : [{_singleKey.KeyName}] 没有忽略，并且也没有指定数据请检查");
                            }

                            var _dataAfterAction = _action.TryProcessData(_singleRow);

                            _writeDataMap.Add(_singleKey, _dataAfterAction);
                        }

                        _currentSheet.WriteOneData(-1, _writeDataMap, true);
                    }

                    break;
                }
            }

            return true;
        }

        public abstract void SaveFile();

        public bool GetHasInit()
        {
            return mHasInit;
        }

        public List<List<CellValueData>>? GetFilteredDataList(Dictionary<KeyData, List<FilterFuncBase>> filterDataMap, int workSheet)
        {
            var _currentSheet = GetWorkSheetList()[workSheet];

            return _currentSheet?.GetFilteredDataList(filterDataMap);
        }

        public string GetFileAbsulotePath()
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

        public abstract bool InternalLoadFile(string absolutePath);
    }
}
