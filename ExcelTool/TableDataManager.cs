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
            mSourceFile = TryLoadFile(absoluteFilePath, false);

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
