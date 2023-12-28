using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class TableDataManager
    {
        public TableDataManager()
        {
        }

        public static TableDataManager Ins()
        {
            if (mIns == null)
            {
                mIns = new TableDataManager();
            }

            return mIns;
        }

        private static TableDataManager mIns = new TableDataManager();

        private static List<FileDataBase> mDataList = new List<FileDataBase>();

        private FileDataBase? mExportTargetFile = null; // 导出目标文件，其关联在里面设置

        private CommonWorkSheetData? mExportSheet = null;

        private FileDataBase? mSourceFile = null; // 数据源文件

        private CommonWorkSheetData? mSourceSheet = null;

        public Dictionary<KeyData, SourceAction> ExportKeyActionMap = new Dictionary<KeyData, SourceAction>();

        /// <summary>
        /// 源文件的数据过滤器
        /// </summary>
        private Dictionary<KeyData, List<FilterFuncBase>> SourceDataFilterMap
        {
            get;
            set;
        } = new Dictionary<KeyData, List<FilterFuncBase>>();

        /// <summary>
        /// 注意，这个INDEX是 IndexInRowData 不是 sheetData
        /// </summary>
        public int IDIndexForSourceData
        {
            get;
            set;
        }

        public MainTypeDefine.ExportWriteWayType ExportWriteWayType
        {
            get;
            set;
        } = MainTypeDefine.ExportWriteWayType.Append;

        public MainTypeDefine.ExportConflictDealWayType ExportConfigDealWayType
        {
            get;
            set;
        } = MainTypeDefine.ExportConflictDealWayType.UseOldData;

        public void RemoveData(FileDataBase targetData)
        {
            if (mDataList.Remove(targetData))
            {
                InternalSetSortIndex();
            }
        }

        public List<FilterFuncBase>? GetSourceFileDataFilterFuncByKey(KeyData? targetKey)
        {
            if (targetKey == null)
            {
                return null;
            }

            SourceDataFilterMap.TryGetValue(targetKey, out var _funcList);

            return _funcList;
        }

        public bool AddSourceFileDataFilterFunc(KeyData targetKey, FilterFuncBase targetFunc)
        {
            if (!SourceDataFilterMap.TryGetValue(targetKey, out var _funcList))
            {
                _funcList = new List<FilterFuncBase>();
                SourceDataFilterMap.Add(targetKey, _funcList);
            }

            if (_funcList.Contains(targetFunc))
            {
                return false;
            }

            _funcList.Add(targetFunc);

            return true;
        }

        private void InternalSetSortIndex()
        {
            for (int i = 0; i < mDataList.Count; i++)
            {
                mDataList[i].DisplayIndex = i;
            }
        }

        public List<FileDataBase> GetLoadedFileList()
        {
            return mDataList;
        }

        public FileDataBase? IsFileExist(string absoluteFilePath)
        {
            var _exitItem = mDataList.Find(x => x.GetFileAbsulotePath() == absoluteFilePath);
            if (_exitItem != null)
            {
                return _exitItem;
            }

            return null;
        }

        public void StartExportData()
        {
            var _exportFile = GetExportFileData();
            if (_exportFile == null)
            {
                MessageBox.Show("StartExportData 但是 _exportFile 文件没有配置，请检查", "错误");
                return;
            }

            var _exportSheet = TableDataManager.Ins().GetExportSheet();
            if (_exportSheet == null)
            {
                throw new Exception($"_exportSheet 为空");
            }

            _exportSheet.LoadAllCellData(true);
            var _exportSheetIndex = _exportFile.GetWorkSheetList().IndexOf(_exportSheet);
            if (_exportSheetIndex < 0)
            {
                throw new Exception("_exportSheetIndex 无效");
            }

            var _inRowDataList = InternalProcssExportData();

            if (!_exportFile.WriteData(_inRowDataList, _exportSheetIndex))
            {
                MessageBox.Show("数据写入出错，请检查", "错误");
                return;
            }

            _exportFile.SaveFile();

            MessageBox.Show("数据导出完成", "提示");
        }

        private List<List<string>> InternalProcssExportData()
        {
            var _sourceFile = GetSourceFileData();
            if (_sourceFile == null)
            {
                throw new Exception("StartExportData 但是 _sourceFile 文件没有配置，请检查");
            }

            var _sourceSheet = TableDataManager.Ins().GetSourceSheet();
            if (_sourceSheet == null)
            {
                throw new Exception("_sourceSheet 为空");
            }

            _sourceSheet.LoadAllCellData(true);
            var _sourceSheetIndex = _sourceFile.GetWorkSheetList().IndexOf(_sourceSheet);
            if (_sourceSheetIndex < 0)
            {
                throw new Exception("_sourceSheetIndex < 0 ，请检查");
            }

            var _sourceRowDataList = _sourceFile.GetFilteredDataList(SourceDataFilterMap, _sourceSheetIndex);
            if (_sourceRowDataList == null || _sourceRowDataList.Count < 1)
            {
                throw new Exception("过滤后的源数据为空，没有写入的必要，请检查一下");
            }

            var _keyActionMap = TableDataManager.Ins().ExportKeyActionMap;
            if (_keyActionMap.Count < 1)
            {
                throw new Exception($"{InternalProcssExportData} 出错，未配置 ExportKeyActionMap，请检查");
            }

            var _currentSheet = TableDataManager.Ins().GetExportSheet();
            if (_currentSheet == null)
            {
                throw new Exception($"_exportSheet 为空");
            }

            var _currentKeyList = _currentSheet.GetKeyListData();

            if (_currentKeyList == null)
            {
                throw new Exception($"{InternalProcssExportData} 出错，无法获取当前数据表格的 KeyList，请检查!");
            }

            _currentKeyList.Sort(
                (a, b) =>
                {
                    return (int)a.KeyIndexInList.CompareTo((int)b.KeyIndexInList);
                }
            );

            List<List<string>> _resultList = new List<List<string>>();

            foreach (var _sourceRowData in _sourceRowDataList)
            {
                List<string> _writeDataList = new List<string>();
                _resultList.Add(_writeDataList);

                foreach (var _singleKey in _currentKeyList)
                {
                    if (_singleKey.IsIgnore)
                    {
                        _writeDataList.Add(string.Empty);
                        continue;
                    }

                    if (!_keyActionMap.TryGetValue(_singleKey, out var _action))
                    {
                        throw new Exception($" Key : [{_singleKey.KeyName}] 没有忽略，并且也没有指定数据请检查");
                    }

                    var _listStringData = CommonUtil.ParsRowCellDataToRowStringData(_sourceRowData);

                    var _dataAfterAction = _action.TryProcessData(_listStringData);

                    if (_dataAfterAction == null || _dataAfterAction.Count < 1)
                    {
                        _writeDataList.Add(string.Empty);
                    }
                    else
                    {
                        if (_dataAfterAction.Count > 1)
                        {
                            throw new Exception($"错误，导出 Key:[{_singleKey.KeyName}] 绑定的行为居然返回多个数据，请检查!");
                        }

                        _writeDataList.Add(_dataAfterAction[0]);
                    }
                }
            }

            return _resultList;
        }

        public FileDataBase? TryLoadExportFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFileAbsulotePath().Equals(absoluteFilePath))
                {
                    return mExportTargetFile;
                }
            }

            mExportTargetFile = InternalLoadFile(absoluteFilePath, true, LoadFileType.ExportFile);
            if (mExportTargetFile != null)
            {
                this.SetExportSheet(mExportTargetFile.GetWorkSheetList()[0]);
            }

            return mExportTargetFile;
        }

        public FileDataBase? TryGetTableByPath(string absolutePath)
        {
            return mDataList.Find(x => x.GetFileAbsulotePath().Equals(absolutePath));
        }

        public int TryGetTableIndex(FileDataBase? targetFile)
        {
            if (targetFile == null)
            {
                return -1;
            }

            return mDataList.IndexOf(targetFile);
        }

        public int TryGetTableIndexByPath(string absolutePath)
        {
            return mDataList.FindIndex(x => x.GetFileAbsulotePath().Equals(absolutePath));
        }

        public FileDataBase? TryLoadNormalFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFileAbsulotePath().Equals(absoluteFilePath))
                {
                    MessageBox.Show("导出目标文件已经加载，不要重复加载", "提示");
                    return null;
                }
            }

            var _tempFile = InternalLoadFile(absoluteFilePath, true, LoadFileType.NormalFile);

            return _tempFile;
        }

        public FileDataBase? TryLoadSourceFile(string absoluteFilePath)
        {
            if (mSourceFile != null)
            {
                if (mSourceFile.GetFileAbsulotePath().Equals(absoluteFilePath))
                {
                    return mSourceFile;
                }
            }

            var _tempFile = InternalLoadFile(absoluteFilePath, true, LoadFileType.SourceFile);
            if (_tempFile == null)
            {
                return null;
            }

            mSourceFile = _tempFile;
            this.SetSourceSheet(mSourceFile.GetWorkSheetList()[0]);

            return mSourceFile;
        }

        public FileDataBase? GetExportFileData()
        {
            return mExportTargetFile;
        }

        public FileDataBase? GetSourceFileData()
        {
            return mSourceFile;
        }

        public CommonWorkSheetData? GetExportSheet()
        {
            return mExportSheet;
        }

        public void SetExportSheet(CommonWorkSheetData targetSheet)
        {
            mExportSheet = targetSheet;
        }

        public CommonWorkSheetData? GetSourceSheet()
        {
            return mSourceSheet;
        }

        public void SetSourceSheet(CommonWorkSheetData targetSheet)
        {
            mSourceSheet = targetSheet;
        }

        private FileDataBase? InternalLoadFile(string absoluteFilePath, bool checkExist, LoadFileType fileType)
        {
            FileDataBase? targetFile = null;

            try
            {
                if (checkExist)
                {
                    var _existFile = IsFileExist(absoluteFilePath);
                    if (_existFile != null)
                    {
                        return _existFile;
                    }
                }

                FileDataBase? _tempFile = null;
                var _extension = Path.GetExtension(absoluteFilePath).ToLower();
                if (_extension.Equals(".xls") || _extension.Equals(".xlsx"))
                {
                    _tempFile = new ExcelFileData();
                }
                else if (_extension.Equals(".csv"))
                {
                    _tempFile = new CSVFileData();
                }
                else
                {
                    throw new Exception($"文件类型不匹配，请检查文件，目标文件路径为：{absoluteFilePath}");
                }

                if (!_tempFile.DoLoadFile(absoluteFilePath, fileType))
                {
                    return null;
                }

                targetFile = _tempFile;

                mDataList.Add(_tempFile);
                _tempFile.DisplayName = _tempFile.GetFileName(false);
                InternalSetSortIndex();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "报错");
            }

            return targetFile;
        }
    }
}
