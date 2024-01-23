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
        public FileDataBase()
        {
        }

        public FileDataBase(string absolutePath, LoadFileType fileType)
        {
            if (!DoLoadFile(absolutePath, fileType))
            {
                throw new Exception($"加载文件错误，路径是：{absolutePath}");
            }
        }

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

        /// <summary>
        /// 注意，这里写入的数据一定是按下标来的
        /// </summary>
        /// <param name="inRowDataList"></param>
        /// <param name="workSheetIndex"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual bool WriteData(List<List<string>> inRowDataList, int workSheetIndex)
        {
            if (inRowDataList == null || inRowDataList.Count < 1)
            {
                throw new Exception($"{WriteData} 数据无效，请检查");
            }
            if (workSheetIndex < 0 || workSheetIndex >= mWorkSheetList.Count)
            {
                throw new Exception($"{WriteData}下标无效");
            }

            var _currentSheet = mWorkSheetList[workSheetIndex];
            var _currentKeyList = _currentSheet.GetKeyListData();
            if (_currentKeyList == null)
            {
                throw new Exception($"{WriteData} 出错，无法获取当前数据表格的 KeyList，请检查!");
            }

            List<int> _mainKeyIndexList = new List<int>();
            foreach (var _key in _currentKeyList)
            {
                if (_key.IsMainKey)
                {
                    _mainKeyIndexList.Add(_key.KeyIndexInList);
                }
            }

            if (_mainKeyIndexList.Count < 1)
            {
                throw new Exception($"导出目标 : {_currentSheet.DisplayName} 没有 主 KEY，请检查");
            }

            _currentSheet.LoadAllCellData(false);
            List<string> _theKeyCompareValue = new List<string>();
            var _exportWriteWayType = MainTypeDefine.ExportWriteWayType.Append;
            var _conflictDealWay = MainTypeDefine.ExportConflictDealWayType.UseNewDataSkipEmptyData;
            switch (_exportWriteWayType)
            {
                case MainTypeDefine.ExportWriteWayType.Append:
                {
                    switch (_conflictDealWay)
                    {
                        case MainTypeDefine.ExportConflictDealWayType.UseNewDataSkipEmptyData:
                        {
                            foreach (var _singleRow in inRowDataList)
                            {
                                _theKeyCompareValue.Clear();
                                foreach (var _keyIndex in _mainKeyIndexList)
                                {
                                    _theKeyCompareValue.Add(_singleRow[_keyIndex]);
                                }

                                // 检测冲突
                                var _rowData = _currentSheet.GetRowCellDataByTargetKeysAndValus(_mainKeyIndexList, _theKeyCompareValue);
                                if (_rowData != null && _rowData.Count > 0)
                                {
                                    // 有冲突
                                    _currentSheet.WriteOneData(_rowData[0].GetCellRowIndexInSheet(), _singleRow, true);
                                }
                                else
                                {
                                    // 没有冲突
                                    _currentSheet.WriteOneData(-1, _singleRow, false);
                                }
                            }

                            break;
                        }
                        case MainTypeDefine.ExportConflictDealWayType.UseNewDataOverwriteAll:
                        {
                            foreach (var _singleRow in inRowDataList)
                            {
                                _theKeyCompareValue.Clear();
                                foreach (var _keyIndex in _mainKeyIndexList)
                                {
                                    _theKeyCompareValue.Add(_singleRow[_keyIndex]);
                                }

                                // 检测冲突
                                var _rowData = _currentSheet.GetRowCellDataByTargetKeysAndValus(_mainKeyIndexList, _theKeyCompareValue);
                                if (_rowData != null && _rowData.Count > 0)
                                {
                                    // 有冲突
                                    _currentSheet.WriteOneData(_rowData[0].GetCellRowIndexInSheet(), _singleRow, false);
                                }
                                else
                                {
                                    // 没有冲突
                                    _currentSheet.WriteOneData(-1, _singleRow, false);
                                }
                            }

                            break;
                        }
                        case MainTypeDefine.ExportConflictDealWayType.UseOldData:
                        {
                            foreach (var _singleRow in inRowDataList)
                            {
                                _theKeyCompareValue.Clear();
                                foreach (var _keyIndex in _mainKeyIndexList)
                                {
                                    _theKeyCompareValue.Add(_singleRow[_keyIndex]);
                                }

                                // 检测冲突
                                var _rowData = _currentSheet.GetRowCellDataByTargetKeysAndValus(_mainKeyIndexList, _theKeyCompareValue);
                                if (_rowData != null && _rowData.Count > 0)
                                {
                                    continue;
                                }

                                // 没有冲突
                                _currentSheet.WriteOneData(-1, _singleRow, false);
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
                        _currentSheet.WriteOneData(-1, _singleRow, false);
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
