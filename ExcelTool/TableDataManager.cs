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

        public static TableDataManager Instance()
        {
            if (mIns == null)
            {
                mIns = new TableDataManager();
            }

            return mIns;
        }

        private static TableDataManager mIns = null;

        private static List<TableBaseData> mDataList = new List<TableBaseData>();

        private TableBaseData? mExportTargetFile = null; // 导出目标文件，其关联在里面设置

        private TableBaseData? mSourceFile = null; // 数据源文件

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

        public void RemoveData(TableBaseData targetData)
        {
            if (mDataList.Remove(targetData))
            {
                InternalSetSortIndex();
            }
        }

        private void InternalSetSortIndex()
        {
            for (int i = 0; i < mDataList.Count; i++)
            {
                mDataList[i].DisplayIndex = i;
            }
        }

        public List<TableBaseData> GetDataList()
        {
            return mDataList;
        }

        public TableBaseData? IsFileExist(string absoluteFilePath)
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

            var _inDataList = _sourceFile.GetFilteredDataList();
            if (_inDataList == null || _inDataList.Count < 1)
            {
                MessageBox.Show("过滤后的源数据为空，没有写入的必要，请检查一下", "提示");
                return;
            }

            if (!_exportFile.WriteData(_inDataList))
            {
                MessageBox.Show("数据写入出错，请检查", "错误");
                return;
            }

            _exportFile.SaveFile();

            MessageBox.Show("数据导出完成", "提示");
        }

        public void TrySetExportTargetFile(TableBaseData targetData)
        {
            mExportTargetFile = targetData;
        }

        public void TrySetSourceTargetFile(TableBaseData targetData)
        {
            mSourceFile = targetData;
        }

        public TableBaseData? TryLoadExportFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFilePath().Equals(absoluteFilePath))
                {
                    return mExportTargetFile;
                }
            }

            mExportTargetFile = InternalLoadFile(absoluteFilePath, true, false);

            return mExportTargetFile;
        }

        public TableBaseData? TryLoadNormalFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFilePath().Equals(absoluteFilePath))
                {
                    MessageBox.Show("导出目标文件已经加载，不要重复加载", "提示");
                    return null;
                }
            }

            var _tempFile = InternalLoadFile(absoluteFilePath, true, true);

            return _tempFile;
        }

        public TableBaseData? TryLoadSourceFile(string absoluteFilePath)
        {
            if (mExportTargetFile != null)
            {
                if (mExportTargetFile.GetFilePath().Equals(absoluteFilePath))
                {
                    MessageBox.Show("导出目标文件已经加载，不要重复加载", "提示");
                    return null;
                }
            }

            var _tempFile = InternalLoadFile(absoluteFilePath, true, true);
            if (_tempFile == null)
            {
                return null;
            }

            // 这里是确保 SourceFile 一定在第一个
            if (mSourceFile != null)
            {
                // 如果原来已经有了
                mDataList.Remove(_tempFile);
                List<TableBaseData> _tempList = new List<TableBaseData>()
                {
                    _tempFile
                };

                _tempList.AddRange(mDataList);
                mDataList = _tempList;
            }

            mSourceFile = _tempFile;

            return mSourceFile;
        }

        public TableBaseData? GetExportFileData()
        {
            return mExportTargetFile;
        }

        public TableBaseData? GetSourceFileData()
        {
            return mSourceFile;
        }

        private TableBaseData? InternalLoadFile(string absoluteFilePath, bool checkExist, bool addList)
        {
            TableBaseData? targetFile = null;

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

                TableBaseData? _tempFile = null;
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

                if (!_tempFile.DoLoadFile(absoluteFilePath))
                {
                    return null;
                }

                targetFile = _tempFile;

                if (addList)
                {
                    mDataList.Add(_tempFile);
                    _tempFile.DisplayName = _tempFile.GetFileName(false);
                    InternalSetSortIndex();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "报错");
            }

            return targetFile;
        }
    }
}
