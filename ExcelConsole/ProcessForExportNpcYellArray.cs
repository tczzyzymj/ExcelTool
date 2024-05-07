using ExcelTool;
using OfficeOpenXml;

namespace ExcelConsole
{
    public class ProcessForExportNpcYellArray : ProcessBase
    {
        public static           int                        MaxYellCount          = 8;
        private static readonly int                        mIdIndex              = CommonUtil.GetIndexByZm("B", 2);
        private static readonly int                        mYellFirstIndex       = CommonUtil.GetIndexByZm("G", 2);
        private static readonly int                        mYellIntervalIndex    = CommonUtil.GetIndexByZm("E", 2);
        private static readonly int                        mYellIntervalMaxIndex = CommonUtil.GetIndexByZm("F", 2);
        private readonly        List<FateArrayNpcYellData> mWriteDataList        = new List<FateArrayNpcYellData>();
        private                 ExcelFileData?             mExcelFateArrayNpcYell;
        private                 ExcelFileData?             mExcelNpcYell;
        private                 ExcelSheetData?            mExcelSheetFateArrayNpcYell;
        private                 ExcelSheetData?            mExcelSheetNpcYell;
        private                 ExcelFileData?             mExportFile;

        public override bool Process()
        {
            // 加载文件
            {
                // FateArrayNpcYell 端游表 数据加载
                {
                    string _tempPath = Path.Combine(FolderPath, "FateArrayNpcYell.xlsx");
                    mExcelFateArrayNpcYell      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetFateArrayNpcYell = mExcelFateArrayNpcYell.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetFateArrayNpcYell == null)
                    {
                        throw new Exception("无法获取 sheet 3");
                    }

                    mExcelSheetFateArrayNpcYell.SetKeyStartRowIndexInSheet(5);
                    mExcelSheetFateArrayNpcYell.SetKeyStartColmIndexInSheet(2);
                    mExcelSheetFateArrayNpcYell.SetContentStartRowIndexInSheet(11);

                    mExcelSheetFateArrayNpcYell.LoadAllCellData(true);
                }
                // end

                // NpcYell 端游表 数据加载
                {
                    string _tempPath = Path.Combine(FolderPath, "NpcYell.xlsx");
                    mExcelNpcYell      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetNpcYell = mExcelNpcYell.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetNpcYell == null)
                    {
                        throw new Exception("无法获取 sheet 3");
                    }

                    List<KeyData> _keyListData = mExcelSheetNpcYell.GetKeyListData();
                    _keyListData[4].IsMainKey = true;

                    mExcelSheetNpcYell.SetKeyStartRowIndexInSheet(5);
                    mExcelSheetNpcYell.SetKeyStartColmIndexInSheet(2);
                    mExcelSheetNpcYell.SetContentStartRowIndexInSheet(11);

                    mExcelSheetNpcYell.LoadAllCellData(true);
                }
                //end

                // 导出目标表
                {
                    string _tempPath = Path.Combine(FolderPath, "Q气泡对话组.xlsx");
                    mExportFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                }
            }

            List<List<CellValueData>>? _allDataList = mExcelSheetFateArrayNpcYell.GetAllDataList();

            if ((_allDataList == null) || (_allDataList.Count < 1))
            {
                throw new Exception("mExcelSheetFateArrayNpcYell.GetAllDataList(); 获取数据出错");
            }

            foreach (List<CellValueData> _data in _allDataList)
            {
                int.TryParse(_data[mIdIndex].GetCellValue(), out int _id);

                if (_id <= 0)
                {
                    continue;
                }

                FateArrayNpcYellData _newData = new FateArrayNpcYellData();
                mWriteDataList.Add(_newData);
                _newData.Id = _id;

                int.TryParse(_data[mYellIntervalIndex].GetCellValue(), out _newData.YellInterval);
                int.TryParse(_data[mYellIntervalMaxIndex].GetCellValue(), out _newData.YellIntervalMax);

                for (int i = 0; i < MaxYellCount; ++i)
                {
                    string _searchStr = _data[mYellFirstIndex + i].GetCellValue();

                    if (string.IsNullOrEmpty(_searchStr))
                    {
                        continue;
                    }

                    List<CellValueData>? _seachRowData = mExcelSheetNpcYell.GetCacheRowDataListByKeyStr(_searchStr);

                    if ((_seachRowData == null) || (_seachRowData.Count < 1))
                    {
                        throw new Exception($"获取数据错误，查找数据是:[{_searchStr}], FateArrayNpcYell_ID:[{_id}]");
                    }

                    int.TryParse(_seachRowData[0].GetCellValue(), out int _npcYellID);

                    if (_npcYellID > 0)
                    {
                        if (!_newData.NpcYellIdList.Contains(_npcYellID))
                        {
                            _newData.NpcYellIdList.Add(_npcYellID);
                        }
                    }
                }
            }

            // 写入
            {
                const int       RowOffset = 4;
                ExcelWorksheet? _sheet    = mExportFile.GetExcelPackage()?.Workbook.Worksheets[0];

                if (_sheet == null)
                {
                    throw new Exception("错误，无法获取 WorkSheets");
                }

                _sheet.Cells[2, 1].Value = "ID";
                _sheet.Cells[3, 1].Value = "ID";
                _sheet.Cells[2, 2].Value = "YellInterval";
                _sheet.Cells[3, 2].Value = "最小间隔";
                _sheet.Cells[2, 3].Value = "YellIntervalMax";
                _sheet.Cells[3, 3].Value = "最大间隔";

                for (int _i = 0; _i < MaxYellCount; ++_i)
                {
                    _sheet.Cells[2, 4 + _i].Value = $"NpcYellArray[{_i}]";
                    _sheet.Cells[3, 4 + _i].Value = "去Q气泡表查找";
                }

                for (int _i = 0; _i < mWriteDataList.Count; ++_i)
                {
                    _sheet.Cells[_i + RowOffset, 1].Value = mWriteDataList[_i].Id.ToString();
                    _sheet.Cells[_i + RowOffset, 2].Value = mWriteDataList[_i].YellInterval.ToString();
                    _sheet.Cells[_i + RowOffset, 3].Value = mWriteDataList[_i].YellIntervalMax.ToString();

                    for (int j = 0; j < mWriteDataList[_i].NpcYellIdList.Count; ++j)
                    {
                        _sheet.Cells[_i + RowOffset, 4 + j].Value = mWriteDataList[_i].NpcYellIdList[j].ToString();
                    }
                }
            }

            mExportFile?.SaveFile();

            return true;
        }

        private class FateArrayNpcYellData
        {
            public readonly List<int> NpcYellIdList = new List<int>();
            public          int       Id;
            public          int       YellInterval;    // 最小间隔
            public          int       YellIntervalMax; // 最大间隔
        }
    }
}
