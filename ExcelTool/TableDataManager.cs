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

        private static TableDataManager mIns = null;

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
        /// 导出目标文件的数据过滤器，考虑到可能是对原有数据的更新
        /// </summary>
        private Dictionary<KeyData, List<FilterFuncBase>> ExportDataFilterMap
        {
            get;
            set;
        } = new Dictionary<KeyData, List<FilterFuncBase>>();

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

        public List<FilterFuncBase>? GetSourceFileDataFilterFuncByKey(KeyData targetKey)
        {
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

        public List<FileDataBase> GetTableDataList()
        {
            return mDataList;
        }

        public FileDataBase? IsFileExist(string absoluteFilePath)
        {
            var _exitItem = mDataList.Find(x => x.GetFilePath() == absoluteFilePath);
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

            var _sourceFile = GetSourceFileData();
            if (_sourceFile == null)
            {
                MessageBox.Show("StartExportData 但是 _sourceFile 文件没有配置，请检查", "错误");
                return;
            }

            var _inRowDataList = _sourceFile.GetFilteredDataList(SourceDataFilterMap);
            if (_inRowDataList == null || _inRowDataList.Count < 1)
            {
                MessageBox.Show("过滤后的源数据为空，没有写入的必要，请检查一下", "提示");
                return;
            }

            if (!_exportFile.WriteData(_inRowDataList))
            {
                MessageBox.Show("数据写入出错，请检查", "错误");
                return;
            }

            _exportFile.SaveFile();

            MessageBox.Show("数据导出完成", "提示");
        }

        public void TrySetExportTargetFile(FileDataBase targetData)
        {
            mExportTargetFile = targetData;
        }

        public void TrySetSourceTargetFile(FileDataBase targetData)
        {
            mSourceFile = targetData;
        }

        public FileDataBase? TryLoadExportFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFilePath().Equals(absoluteFilePath))
                {
                    return mExportTargetFile;
                }
            }

            mExportTargetFile = InternalLoadFile(absoluteFilePath, true, LoadFileType.ExportFile);

            return mExportTargetFile;
        }

        public FileDataBase? TryGetTableByPath(string absolutePath)
        {
            return mDataList.Find(x => x.GetFilePath().Equals(absolutePath));
        }

        public int TryGetTableIndexByPath(string absolutePath)
        {
            return mDataList.FindIndex(x => x.GetFilePath().Equals(absolutePath));
        }

        public FileDataBase? TryLoadNormalFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFilePath().Equals(absoluteFilePath))
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
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFilePath().Equals(absoluteFilePath))
                {
                    MessageBox.Show("导出目标文件已经加载，不要重复加载", "提示");
                    return null;
                }
            }

            var _tempFile = InternalLoadFile(absoluteFilePath, true, LoadFileType.SourceFile);
            if (_tempFile == null)
            {
                return null;
            }

            // 这里是确保 SourceFile 一定在第一个
            if (mSourceFile != null)
            {
                // 如果原来已经有了
                mDataList.Remove(_tempFile);
                List<FileDataBase> _tempList = new List<FileDataBase>()
                {
                    _tempFile
                };

                _tempList.AddRange(mDataList);
                mDataList = _tempList;
            }

            mSourceFile = _tempFile;

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
