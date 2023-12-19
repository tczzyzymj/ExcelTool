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

            var _keyListData = _exportFile.GetCurrentWorkSheet()?.GetKeyListData();
            if (_keyListData == null)
            {
                MessageBox.Show("无法获取 _exportFile 当前 Sheet 的 Key数据，请检查", "错误");
                return;
            }

            bool _anyConnect = false;

            // 这里检查一下，看是否至少有一个是设置关联了的
            for (int i = 0; i < _keyListData.Count; ++i)
            {
                if (_keyListData[i].GetNextConnectKey() != null)
                {
                    _anyConnect = true;
                    break;
                }
            }

            if (!_anyConnect)
            {
                MessageBox.Show("导出文件至少要设置一个关联数据!", "错误");
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

            _exportFile.WriteData(_inDataList);
        }

        public void TrySetExportTargetFile(TableBaseData targetData)
        {
            mExportTargetFile = targetData;
        }

        public void TrySetSourceTargetFile(TableBaseData targetData)
        {
            mSourceFile = targetData;
        }

        public TableBaseData? TryChooseExportFile(string absoluteFilePath)
        {
            mExportTargetFile = TryLoadFile(absoluteFilePath, false);

            return mExportTargetFile;
        }

        public TableBaseData? TryChooseSourceFile(string absoluteFilePath)
        {
            var _tempFile = TryLoadFile(absoluteFilePath, true);
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


        public TableBaseData? TryLoadFile(string absoluteFilePath, bool checkExistAndAdd)
        {
            TableBaseData? targetFile = null;

            try
            {
                if (checkExistAndAdd)
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

                if (checkExistAndAdd)
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
