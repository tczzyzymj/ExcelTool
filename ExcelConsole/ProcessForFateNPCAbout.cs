using ExcelTool;

namespace ExcelConsole
{
    public class ProcessForFateNPCAbout : ProcessBase
    {
        private static readonly int             mFatePopGroupDepopRangeIndex = CommonUtil.GetIndexByZm("DR", 1);
        private static readonly int             mFatePopGroupIdleRangeIndex  = CommonUtil.GetIndexByZm("DQ", 1);
        private static readonly int             mFatePopGroupPopRangeIndex   = CommonUtil.GetIndexByZm("DP", 1);
        private static          int             mNpcResID                    = CommonUtil.GetIndexByZm("DM", 1);
        private                 ExcelFileData?  mExcelLevelReference;
        private                 ExcelSheetData? mExcelSheetLevelReference;
        private                 CSVFileData?    mFateCSVFile;
        private                 CSVSheetData?   mFateCSVSheet;
        private                 ExcelFileData?  mFateExcelFile;
        private                 ExcelSheetData? mFateGuardExcelSheet;
        private                 ExcelFileData?  mFateMonsterExcelFile;
        private                 ExcelSheetData? mFateMonsterExcelSheet;
        private                 ExcelFileData?  mFateNpcExcelFile;
        private                 ExcelSheetData? mFateNpcExcelSheet;
        private                 CSVFileData?    mFatePopGroupCSVFile;
        private                 CSVSheetData?   mFatePopGroupCSVSheet;
        private                 ExcelSheetData? mFatePopGroupExcelSheet;

        public override bool Process()
        {
            InternalLoadFile();

            List<List<CellValueData>>? _allFateCSVDataList = mFateGuardExcelSheet?.GetAllDataList();

            if ((_allFateCSVDataList == null) || (_allFateCSVDataList.Count < 1))
            {
                throw new Exception("mFateCSVSheet.GetAllDataList() 错误，请检查");
            }

            List<int> _fateIDList = new List<int>();

            foreach (List<CellValueData> _singeRowData in _allFateCSVDataList)
            {
                if (int.TryParse(_singeRowData[0].GetCellValue(), out int _fateID) && (_fateID > 0))
                {
                    _fateIDList.Add(_fateID);
                }
            }

            int _starterGroupIndex = CommonUtil.GetIndexByZm("FD", 1);
            int _guardParamsIndex  = CommonUtil.GetIndexByZm("HD", 1);

            int _fateNpcIndex = CommonUtil.GetIndexByZm("D", 1);

            // key : fate id ; value : FatePopGroupNPCData
            Dictionary<int, FatePopGroupNPCData> _fateNpcDataMap = new Dictionary<int, FatePopGroupNPCData>();

            foreach (int _fateID in _fateIDList)
            {
                List<CellValueData>? _targetFateRowDataList = mFateCSVSheet?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 0 },
                    new List<string> { _fateID.ToString() }
                );

                if ((_targetFateRowDataList == null) || (_targetFateRowDataList.Count < 1))
                {
                    throw new Exception($"无法获取 Fate 数据，FateID 是 : [{_fateID}]");
                }

                if (!int.TryParse(_targetFateRowDataList[_guardParamsIndex].GetCellValue(), out int _guardParamsID) || (_guardParamsID <= 0))
                {
                    continue;
                }

                if (!int.TryParse(_targetFateRowDataList[_starterGroupIndex].GetCellValue(), out int _starterGroupID) || (_starterGroupID <= 0))
                {
                    continue;
                }

                List<CellValueData>? _fatePopGroupRowDataList = mFatePopGroupCSVSheet?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 0 },
                    new List<string> { _starterGroupID.ToString() }
                );

                if ((_fatePopGroupRowDataList == null) || (_fatePopGroupRowDataList.Count < 1))
                {
                    throw new Exception($"无法获取 FatePopGroup 数据，ID是：[{_starterGroupID}]");
                }

                if (!int.TryParse(_fatePopGroupRowDataList[_fateNpcIndex].GetCellValue(), out int _fateNpcID) || (_fateNpcID <= 0))
                {
                    throw new Exception($"FatePopGroup : [{_starterGroupID}] , 的第一个友方 FateNpc 为空，请检查");
                }

                int.TryParse(_fatePopGroupRowDataList[_fateNpcIndex + 1].GetCellValue(), out int _maxCount);

                if (_maxCount < 1)
                {
                    _maxCount = 1;
                }

                FatePopGroupNPCData _fateNpcData = new FatePopGroupNPCData();
                _fateNpcData.FateNpcID = _fateNpcID;
                _fateNpcData.MaxNum    = _maxCount;
                _fateNpcDataMap.Add(_fateID, _fateNpcData);
            }

            foreach (KeyValuePair<int, FatePopGroupNPCData> _pair in _fateNpcDataMap)
            {
                List<CellValueData>? _targetRowDataList = mFateMonsterExcelSheet?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 8 },
                    new List<string> { _pair.Value.FateNpcID.ToString() }
                );

                if ((_targetRowDataList == null) || (_targetRowDataList.Count < 1))
                {
                    throw new Exception($"无法从 Monster 获取数据，FateNpc ID 是 : [{_pair.Value.FateNpcID}]");
                }

                if (!int.TryParse(_targetRowDataList[5].GetCellValue(), out int _monsterID))
                {
                    throw new Exception($"从 Monster 表获取数据错误，其MonsterID无法转化为int，FateNpcID  : [{_pair.Value.FateNpcID}]");
                }

                _pair.Value.MonsterID = _monsterID;
            }

            int _npcResIDIndex        = CommonUtil.GetIndexByZm("DM", 1);
            int _fateNpcLayoutIDIndex = CommonUtil.GetIndexByZm("D", 1);
            int _idleRangeIndex       = CommonUtil.GetIndexByZm("N", 1);
            int _depopRangeIndex      = CommonUtil.GetIndexByZm("O", 1);

            Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();

            foreach (KeyValuePair<int, FatePopGroupNPCData> _pair in _fateNpcDataMap)
            {
                List<CellValueData>? _fatePopGroupRowDataList = mFatePopGroupExcelSheet?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 0 },
                    new List<string> { _pair.Key.ToString() }
                );

                if ((_fatePopGroupRowDataList == null) || (_fatePopGroupRowDataList.Count < 1))
                {
                    throw new Exception($"无法获取 Fate 的 创建物数据，Fate ID : [{_pair.Key}]");
                }

                List<string> _fatePopGroupStringDataList = CommonUtil.ParsRowCellDataToRowStringData(_fatePopGroupRowDataList);

                _fatePopGroupStringDataList[_npcResIDIndex]     = _pair.Value.MonsterID.ToString();
                _fatePopGroupStringDataList[_npcResIDIndex + 1] = "1";                           // 最小数量
                _fatePopGroupStringDataList[_npcResIDIndex + 2] = _pair.Value.MaxNum.ToString(); // 最大数量

                List<CellValueData>? _fateNpcRowDataList = mFateNpcExcelSheet?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 0 },
                    new List<string> { _pair.Value.FateNpcID.ToString() }
                );

                if ((_fateNpcRowDataList == null) || (_fateNpcRowDataList.Count < 1))
                {
                    throw new Exception($"无法获取 FateNPC，ID是 : [{_pair.Value.FateNpcID}]");
                }

                InternalProcessForFateNpcPosInfo(_fateNpcRowDataList, _fatePopGroupStringDataList, _fateNpcLayoutIDIndex);

                // IdleRange
                int.TryParse(_fateNpcRowDataList[_idleRangeIndex].GetCellValue(), out int _idleRange);

                if (_idleRange > 0)
                {
                    _fatePopGroupStringDataList[mFatePopGroupIdleRangeIndex] = CommonUtil.GetPosInfoByLevelReferenceId(
                        _idleRange,
                        mExcelSheetLevelReference,
                        true,
                        false,
                        true
                    );
                }
                else
                {
                    _fatePopGroupStringDataList[mFatePopGroupIdleRangeIndex] = string.Empty;
                }

                // DepopRange
                int.TryParse(_fateNpcRowDataList[_depopRangeIndex].GetCellValue(), out int _depopRange);

                if (_idleRange > 0)
                {
                    _fatePopGroupStringDataList[mFatePopGroupDepopRangeIndex] = CommonUtil.GetPosInfoByLevelReferenceId(
                        _depopRange,
                        mExcelSheetLevelReference,
                        true,
                        false,
                        true
                    );
                }
                else
                {
                    _fatePopGroupStringDataList[mFatePopGroupDepopRangeIndex] = string.Empty;
                }

                _writeDataMap.Add(_fatePopGroupRowDataList[0].GetCellRowIndexInSheet(), _fatePopGroupStringDataList);
            }

            foreach (KeyValuePair<int, List<string>> _pair in _writeDataMap)
            {
                mFatePopGroupExcelSheet?.WriteOneData(_pair.Key, _pair.Value, true);
            }

            mFateExcelFile?.SaveFile();

            return true;
        }

        private void InternalLoadFile()
        {
            // 加载 FATE表.xlsx
            {
                string _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mFateExcelFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                mFatePopGroupExcelSheet = mFateExcelFile.GetWorkSheetByIndex(6) as ExcelSheetData;

                if (mFatePopGroupExcelSheet == null)
                {
                    throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                }

                {
                    List<KeyData> _keyListData = mFatePopGroupExcelSheet.GetKeyListData();

                    for (int i = 0; i < _keyListData.Count; ++i)
                    {
                        _keyListData[i].IsMainKey = false;
                    }

                    _keyListData[0].IsMainKey = true;
                }

                mFatePopGroupExcelSheet.LoadAllCellData(true);

                mFateGuardExcelSheet = mFateExcelFile.GetWorkSheetByIndex(13) as ExcelSheetData;

                if (mFateGuardExcelSheet == null)
                {
                    throw new Exception("mFateFile.GetWorkSheetByIndex(13)");
                }

                {
                    List<KeyData> _keyList = mFateGuardExcelSheet.GetKeyListData();

                    for (int i = 0; i < _keyList.Count; ++i)
                    {
                        _keyList[i].IsMainKey = false;
                    }

                    _keyList[0].IsMainKey = true;
                }

                mFateGuardExcelSheet.LoadAllCellData(true);
            }

            // 加载 FatePopGroup.csv
            {
                string _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                mFatePopGroupCSVFile  = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mFatePopGroupCSVSheet = mFatePopGroupCSVFile.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mFatePopGroupCSVSheet == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }

                mFatePopGroupCSVSheet.LoadAllCellData(true);
            }

            // 加载 FateNpc.xlsx 文件
            {
                string _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                mFateNpcExcelFile  = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mFateNpcExcelSheet = mFateNpcExcelFile.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mFateNpcExcelSheet == null)
                {
                    throw new Exception("无法获取 mFateNpcExcelFile.GetWorkSheetByIndex(0)");
                }

                mFateNpcExcelSheet.LoadAllCellData(true);
            }

            // 加载 LevelReference.csv
            {
                string _tempPath = Path.Combine(FolderPath, "LevelReference.xlsx");
                mExcelLevelReference      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetLevelReference = mExcelLevelReference.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetLevelReference == null)
                {
                    throw new Exception("mExcelLevelReference.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetLevelReference.SetKeyStartRowIndexInSheet(5);
                mExcelSheetLevelReference.SetKeyStartColmIndexInSheet(2);
                mExcelSheetLevelReference.SetContentStartRowIndexInSheet(10);

                mExcelSheetLevelReference.ReloadKey();
                mExcelSheetLevelReference.LoadAllCellData(true);
            }

            // 加载 G怪物表.xlsx
            {
                string _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                mFateMonsterExcelFile  = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mFateMonsterExcelSheet = mFateMonsterExcelFile.GetWorkSheetByIndex(1) as ExcelSheetData;

                if (mFateMonsterExcelSheet == null)
                {
                    throw new Exception("获取数据失败， mFateMonsterExcelFile.GetWorkSheetByIndex(1)");
                }

                mFateMonsterExcelSheet.SetKeyStartRowIndexInSheet(1);
                mFateMonsterExcelSheet.SetKeyStartColmIndexInSheet(1);
                mFateMonsterExcelSheet.SetContentStartRowIndexInSheet(4);

                mFateMonsterExcelSheet.ReloadKey();
                mFateMonsterExcelSheet.LoadAllCellData(true);
            }

            // 加载 FateCSV
            {
                string _tempPath = Path.Combine(FolderPath, "Fate.csv");
                mFateCSVFile  = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mFateCSVSheet = mFateCSVFile.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mFateCSVSheet == null)
                {
                    throw new Exception(" mFateCSVFile.GetWorkSheetByIndex(0) 获取数据错误");
                }

                mFateCSVSheet.LoadAllCellData(true);
            }
        }

        private void InternalProcessForFateNpcPosInfo(
            List<CellValueData> _fateNpcRowDataList,
            List<string>        _fatePopGroupStringDataList,
            int                 _fateNpcLayoutIDIndex
        )
        {
            int.TryParse(_fateNpcRowDataList[_fateNpcLayoutIDIndex].GetCellValue(), out int _layoutID);

            if (_layoutID > 0)
            {
                string _tempStr = CommonUtil.GetPosInfoByLevelReferenceId(
                    _layoutID,
                    mExcelSheetLevelReference,
                    true,
                    true,
                    false
                );

                if (!string.IsNullOrEmpty(_tempStr))
                {
                    _fatePopGroupStringDataList[mFatePopGroupPopRangeIndex] = _tempStr;

                    return;
                }
            }

            int.TryParse(_fateNpcRowDataList[_fateNpcLayoutIDIndex].GetCellValue(), out int _popRange);

            if (_popRange > 0)
            {
                string _tempStr = CommonUtil.GetPosInfoByLevelReferenceId(
                    _popRange,
                    mExcelSheetLevelReference,
                    true,
                    true,
                    false
                );

                if (!string.IsNullOrEmpty(_tempStr))
                {
                    _fatePopGroupStringDataList[mFatePopGroupPopRangeIndex] = _tempStr;

                    return;
                }
            }

            throw new Exception($"无法获取坐标数据,FateNPCID:{_fateNpcRowDataList[0].GetCellValue()}");
        }
    }
}
