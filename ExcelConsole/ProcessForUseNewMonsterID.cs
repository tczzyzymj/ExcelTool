using ExcelTool;
using OfficeOpenXml;

namespace ExcelConsole
{
    public class FateNpcData
    {
        public int    BaseID;
        public string DepopRangePosInfo;
        public int    FateNpcID;
        public string IdleRangePosInfo;
        public string LogInfo;
        public int    MaxCount = 1;
        public int    MinCount = 1;
        public int    MonsterID;
        public string PopRangePosInfo;
    }

    public class ProcessForUseNewMonsterID : ProcessBase
    {
        private static readonly int             mFateNpcDepopRangeIndex                     = CommonUtil.GetIndexByZm("O", 1);
        private static readonly int             mFateNpcIdleRangeIndex                      = CommonUtil.GetIndexByZm("N", 1);
        private static readonly int             mFateNpcLayoutIDIndex                       = CommonUtil.GetIndexByZm("D", 1);
        private static readonly int             mFateNpcPopRangeIndex                       = CommonUtil.GetIndexByZm("E", 1);
        private static readonly int             mFateNpcRandomPopRange00Index               = CommonUtil.GetIndexByZm("F", 1);
        private static readonly int             mFateOriginAllyIndex                        = CommonUtil.GetIndexByZm("BQ", 2);
        private static readonly int             mFateOriginPopRange00Index                  = CommonUtil.GetIndexByZm("BE", 2);
        private static          int             mFatePopGroupFirstMonsteMinrCountIndex      = CommonUtil.GetIndexByZm("G", 1);
        private static          int             mFatePopGroupFirstMonsterBaseIDIndex        = CommonUtil.GetIndexByZm("F", 1);
        private static readonly int             mFatePopGroupFirstMonsterDepopRangeIndex    = CommonUtil.GetIndexByZm("S", 1);
        private static          int             mFatePopGroupFirstMonsterFirstMaxCountIndex = CommonUtil.GetIndexByZm("H", 1);
        private static readonly int             mFatePopGroupFirstMonsterIdleRangeIndex     = CommonUtil.GetIndexByZm("R", 1);
        private static readonly int             mFatePopGroupFirstMonsterIDStartIndex       = CommonUtil.GetIndexByZm("E", 1);
        private static readonly int             mFatePopGroupFirstMonsterPosStartIndex      = CommonUtil.GetIndexByZm("I", 1);
        private static readonly int             mFatePopGroupFirstNpcDepopRangeIndex        = CommonUtil.GetIndexByZm("IP", 1);
        private static readonly int             mFatePopGroupFirstNpcIDIndex                = CommonUtil.GetIndexByZm("IK", 1);
        private static readonly int             mFatePopGroupFirstNpcIdleRangeIndex         = CommonUtil.GetIndexByZm("IO", 1);
        private static          int             mFatePopGroupFirstNpcMaxNumIndex            = CommonUtil.GetIndexByZm("IM", 1);
        private static          int             mFatePopGroupFirstNpcMinNumIndex            = CommonUtil.GetIndexByZm("IL", 1);
        private static readonly int             mFatePopGroupFirstNpcPopRangeIndex          = CommonUtil.GetIndexByZm("IN", 1);
        private static readonly int             mGuardParamIndex                            = CommonUtil.GetIndexByZm("BX", 1);
        private static          int             mMonsterAITableIDIndex                      = CommonUtil.GetIndexByZm("BC", 1);
        private static          int             mMonsterAttrIDColumIndex                    = CommonUtil.GetIndexByZm("AR", 1);
        private static          int             mMonsterBaseIDIndex                         = CommonUtil.GetIndexByZm("D", 1);
        private static          int             mMonsterFateIDIndex                         = CommonUtil.GetIndexByZm("B", 1);
        private static readonly int             mMonsterFateNpcIdIndex                      = CommonUtil.GetIndexByZm("I", 1);
        private static          int             mMonsterIDIndex                             = CommonUtil.GetIndexByZm("F", 1);
        private static          int             mMonsterLevelColumIndex                     = CommonUtil.GetIndexByZm("E", 1);
        private static          int             mMonsterNameIndex                           = CommonUtil.GetIndexByZm("G", 1);
        private readonly        bool            mTest                                       = false;
        private                 CSVFileData?    mCSVFatePopGroup;
        private                 CSVSheetData?   mCSVSheetFatePopGroup;
        private                 ExcelFileData?  mExcelCompare;
        private                 ExcelFileData?  mExcelFate;
        private                 ExcelFileData?  mExcelFateNpc;
        private                 ExcelFileData?  mExcelFateOrigin;
        private                 ExcelFileData?  mExcelFatePopGropOrigin;
        private                 ExcelFileData?  mExcelLevelReference;
        private                 ExcelFileData?  mExcelMonster;
        private                 ExcelSheetData? mExcelSheetFateNpc;
        private                 ExcelSheetData? mExcelSheetFateOrigin;
        private                 ExcelSheetData? mExcelSheetFatePopGroupOrigin;
        private                 ExcelSheetData? mExcelSheetLevelReference;
        private                 ExcelSheetData? mExcelSheetMonster;
        private                 ExcelSheetData? mExcelSheetPopGroupInFateExcel;

        public override bool Process()
        {
            // 加载文件相关
            {
                if (mTest)
                {
                    string _tempPath = Path.Combine(FolderPath, "Compare.xlsx");
                    mExcelCompare = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                }

                // 加载 FATE表.xlsx
                {
                    string _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                    mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                    if (mExcelFate == null)
                    {
                        throw new Exception("错误，加载 FATE表.xlsx 数据出错");
                    }

                    // 重新加载一下 key

                    mExcelSheetPopGroupInFateExcel = mExcelFate?.GetWorkSheetByIndex(6) as ExcelSheetData;

                    if (mExcelSheetPopGroupInFateExcel == null)
                    {
                        throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                    }

                    List<KeyData> _keyListData = mExcelSheetPopGroupInFateExcel.GetKeyListData();

                    for (int i = 0; i < _keyListData.Count; ++i)
                    {
                        _keyListData[i].IsMainKey = false;
                    }

                    _keyListData[0].IsMainKey = true;

                    mExcelSheetPopGroupInFateExcel.LoadAllCellData(true);
                }

                // fate 端游表 数据加载
                {
                    string _tempPath = Path.Combine(FolderPath, "Fate.xlsx");
                    mExcelFateOrigin      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetFateOrigin = mExcelFateOrigin.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetFateOrigin == null)
                    {
                        throw new Exception("无法获取 sheet 3");
                    }

                    mExcelSheetFateOrigin.SetKeyStartRowIndexInSheet(5);
                    mExcelSheetFateOrigin.SetKeyStartColmIndexInSheet(2);
                    mExcelSheetFateOrigin.SetContentStartRowIndexInSheet(11);

                    mExcelSheetFateOrigin.LoadAllCellData(true);
                }
                // end

                // Fate PopGroup 端游数据加载
                {
                    string _tempPath = Path.Combine(FolderPath, "FatePopGroup.xlsx");
                    mExcelFatePopGropOrigin       = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetFatePopGroupOrigin = mExcelFatePopGropOrigin.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetFatePopGroupOrigin == null)
                    {
                        throw new Exception("无法获取 sheet 3");
                    }

                    mExcelSheetFatePopGroupOrigin.SetKeyStartRowIndexInSheet(5);
                    mExcelSheetFatePopGroupOrigin.SetKeyStartColmIndexInSheet(2);
                    mExcelSheetFatePopGroupOrigin.SetContentStartRowIndexInSheet(11);

                    mExcelSheetFatePopGroupOrigin.LoadAllCellData(true);
                }
                // end

                // 加载 Monster
                {
                    string _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                    mExcelMonster      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetMonster = mExcelMonster.GetWorkSheetByIndex(1) as ExcelSheetData;

                    if (mExcelSheetMonster == null)
                    {
                        throw new Exception("mExcelMonster.GetWorkSheetByIndex(1) 获取数据出错");
                    }

                    mExcelSheetMonster.SetKeyStartRowIndexInSheet(1);
                    mExcelSheetMonster.ReloadKey();

                    List<KeyData> _allKeyDataList = mExcelSheetMonster.GetKeyListData();
                    _allKeyDataList[CommonUtil.GetIndexByZm("F", 1)].IsMainKey = true;

                    mExcelSheetMonster.LoadAllCellData(true);
                }

                // 加载 FateNpc
                {
                    string _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                    mExcelFateNpc      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetFateNpc = mExcelFateNpc.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetFateNpc == null)
                    {
                        throw new Exception("mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                    }

                    mExcelSheetFateNpc.SetKeyStartRowIndexInSheet(1);
                    mExcelSheetFateNpc.SetKeyStartColmIndexInSheet(1);
                    mExcelSheetFateNpc.SetContentStartRowIndexInSheet(4);

                    mExcelSheetFateNpc.ReloadKey();
                    List<KeyData> _allKeyDataList = mExcelSheetFateNpc.GetKeyListData();
                    _allKeyDataList[0].IsMainKey = true;

                    mExcelSheetFateNpc.LoadAllCellData(true);
                }

                // fatepopgroup csv表 数据加载
                {
                    string _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                    mCSVFatePopGroup      = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                    mCSVSheetFatePopGroup = mCSVFatePopGroup.GetWorkSheetByIndex(0) as CSVSheetData;

                    if (mCSVSheetFatePopGroup == null)
                    {
                        CommonUtil.ShowError("无法获取 mCSVSheetFatePopGroup", true);

                        return false;
                    }

                    mCSVSheetFatePopGroup.LoadAllCellData(true);
                }
                // end

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
                // end
            }

            // key : OldMonsterID ; value : FateNpcID
            Dictionary<int, int> _oldDataMonsterIDToFateNpcID = new Dictionary<int, int>();

            // key : OldNpcMonster ; value : FateNpcID
            Dictionary<int, int> _oldDataNpcMonsterIDToFateNpcID = new Dictionary<int, int>();

            // key : fateID ; value : List<FateNpcID>
            Dictionary<int, List<FateNpcData>> _writeDataForFatePopGroupMonster = new Dictionary<int, List<FateNpcData>>();

            // key : fateID ; value : List<FateNpcID>
            Dictionary<int, List<FateNpcData>> _writeDataForFatePopGroupNpcMonster = new Dictionary<int, List<FateNpcData>>();

            // 解析数据中
            List<List<CellValueData>>? _allFateDataList = mExcelSheetPopGroupInFateExcel.GetAllDataList();

            if ((_allFateDataList == null) || (_allFateDataList.Count < 1))
            {
                throw new Exception("无法获取所有数据，请检查!");
            }

            for (int i = 0; i < _allFateDataList.Count; ++i)
            {
                Dictionary<int, List<FateNpcData>> _oldMonsterListDataMap    = new Dictionary<int, List<FateNpcData>>();
                Dictionary<int, List<FateNpcData>> _oldNpcMonsterListDataMap = new Dictionary<int, List<FateNpcData>>();

                Dictionary<int, List<FateNpcData>> _newMonsterListDataMap    = new Dictionary<int, List<FateNpcData>>();
                Dictionary<int, List<FateNpcData>> _newNpcMonsterListDataMap = new Dictionary<int, List<FateNpcData>>();

                int.TryParse(_allFateDataList[i][0].GetCellValue(), out int _targetFateID);

                if (_targetFateID <= 0)
                {
                    throw new Exception($"错误，无法获取 创建物的 FateID ,index 是：[{i}]");
                }

                // 解析一下旧数据
                {
                    List<CellValueData> _targetRowData = _allFateDataList[i];

                    List<FateNpcData> tempMonsterList = new List<FateNpcData>();
                    _oldMonsterListDataMap.Add(_targetFateID, tempMonsterList);

                    List<FateNpcData> _tempNpcMonsterList = new List<FateNpcData>();
                    _oldNpcMonsterListDataMap.Add(_targetFateID, _tempNpcMonsterList);

                    //  monster 的
                    for (int _tempIndex = 0; _tempIndex <= 15; ++_tempIndex)
                    {
                        int _offset = _tempIndex * 15;

                        int.TryParse(_targetRowData[mFatePopGroupFirstMonsterIDStartIndex + _offset].GetCellValue(), out int _tempMonsterId);

                        if (_tempMonsterId <= 0)
                        {
                            continue;
                        }

                        string _popRangePosInfo = _targetRowData[mFatePopGroupFirstMonsterPosStartIndex + _offset].GetCellValue();

                        FateNpcData _newData = new FateNpcData();

                        // IdleRange
                        {
                            string _idlePosInfo = _targetRowData[mFatePopGroupFirstMonsterIdleRangeIndex + _offset].GetCellValue();

                            if (!string.IsNullOrEmpty(_idlePosInfo))
                            {
                                _newData.IdleRangePosInfo = _idlePosInfo;
                            }
                        }

                        // DepopRange
                        {
                            string _depopRangePosInfo = _targetRowData[mFatePopGroupFirstMonsterDepopRangeIndex + _offset].GetCellValue();

                            if (!string.IsNullOrEmpty(_depopRangePosInfo))
                            {
                                _newData.DepopRangePosInfo = _depopRangePosInfo;
                            }
                        }

                        _newData.MonsterID       = _tempMonsterId;
                        _newData.PopRangePosInfo = _popRangePosInfo;

                        _newData.LogInfo =
                        $"MonsterID:[{_tempMonsterId}] ; pos:[{_popRangePosInfo}],IdlePos:[{_newData.IdleRangePosInfo}],DepopRange:[{_newData.DepopRangePosInfo}]";

                        tempMonsterList.Add(_newData);
                    }

                    // npc的
                    for (int _tempIndex = 0; _tempIndex <= 15; ++_tempIndex)
                    {
                        int _offset = _tempIndex * 6;

                        int.TryParse(_targetRowData[mFatePopGroupFirstNpcIDIndex + _offset].GetCellValue(), out int _npcMonsterID);

                        if (_npcMonsterID <= 0)
                        {
                            continue;
                        }

                        string _tempPosInfo = _targetRowData[mFatePopGroupFirstNpcPopRangeIndex + _offset].GetCellValue();

                        FateNpcData _newData = new FateNpcData();

                        // IdleRange
                        {
                            string _idlePosInfo = _targetRowData[mFatePopGroupFirstNpcIdleRangeIndex + _offset].GetCellValue();

                            if (!string.IsNullOrEmpty(_idlePosInfo))
                            {
                                _newData.IdleRangePosInfo = _idlePosInfo;
                            }
                        }

                        // DepopRange
                        {
                            string _depopRangePosInfo = _targetRowData[mFatePopGroupFirstNpcDepopRangeIndex + _offset].GetCellValue();

                            if (!string.IsNullOrEmpty(_depopRangePosInfo))
                            {
                                _newData.DepopRangePosInfo = _depopRangePosInfo;
                            }
                        }

                        _newData.MonsterID       = _npcMonsterID;
                        _newData.PopRangePosInfo = _tempPosInfo;

                        _newData.LogInfo =
                        $"NpcMonsterID:[{_npcMonsterID}] ; pos:[{_tempPosInfo}] ; IdlePos:[{_newData.IdleRangePosInfo}], DepopPos:[{_newData.DepopRangePosInfo}]";

                        _tempNpcMonsterList.Add(_newData);
                    }
                }

                // 解析端游数据
                {
                    List<CellValueData>? _targetRowData = mExcelSheetFateOrigin.GetRowCellDataByTargetKeysAndValues(
                        new List<int> { 0 },
                        new List<string> { $"{_targetFateID}" }
                    );

                    if ((_targetRowData == null) || (_targetRowData.Count < 1))
                    {
                        throw new Exception($"数据无效，无法找到 fate :[{_targetFateID}] 数据，请检查");
                    }

                    // 护送的不管
                    int.TryParse(_targetRowData[mGuardParamIndex].GetCellValue(), out int _guardParam);

                    if (_guardParam > 0)
                    {
                        continue;
                    }

                    List<FateNpcData> _tempMonsterList = new List<FateNpcData>();
                    _newMonsterListDataMap.Add(_targetFateID, _tempMonsterList);

                    List<FateNpcData> _tempNpcMonsterList = new List<FateNpcData>();
                    _newNpcMonsterListDataMap.Add(_targetFateID, _tempNpcMonsterList);

                    // 这里是 怪物的
                    for (int _tempIndex = 0; _tempIndex < 12; ++_tempIndex)
                    {
                        string _tempFatePopGroupStr = _targetRowData[mFateOriginPopRange00Index + _tempIndex].GetCellValue();

                        if (string.IsNullOrEmpty(_tempFatePopGroupStr))
                        {
                            continue;
                        }

                        List<CellValueData>? _originRowData = mExcelSheetFatePopGroupOrigin.GetRowCellDataByTargetKeysAndValues(
                            new List<int> { 2 },
                            new List<string> { _tempFatePopGroupStr }
                        );

                        if ((_originRowData == null) || (_originRowData.Count < 1))
                        {
                            throw new Exception(
                                $"1 无法在端游元表 FatePopGroup 中找到数据，KEYDevCode是：[{_tempFatePopGroupStr}], ColumIndex:[{_tempIndex}],Row:[{_targetRowData[0].GetCellValue()}]"
                            );
                        }

                        int.TryParse(_originRowData[0].GetCellValue(), out int _fatePopGroupID);

                        if (_fatePopGroupID <= 0)
                        {
                            throw new Exception(
                                $"2 无法在端游元表 FatePopGroup 中找到数据，KEYDevCode是：[{_tempFatePopGroupStr}], ColumIndex:[{_tempIndex}],Row:[{_targetRowData[0].GetCellValue()}]"
                            );
                        }

                        List<CellValueData>? _fatePopGroupData = mCSVSheetFatePopGroup.GetRowCellDataByTargetKeysAndValues(
                            new List<int> { 0 },
                            new List<string> { _fatePopGroupID.ToString() }
                        );

                        if ((_fatePopGroupData == null) || (_fatePopGroupData.Count < 1))
                        {
                            throw new Exception($"无法在 FatePopGroup.csv 中找到ID: {_fatePopGroupID}，请检查");
                        }

                        for (int j = 0; j <= 15; ++j)
                        {
                            int.TryParse(_fatePopGroupData[3 + (j * 2)].GetCellValue(), out int _fateNpcID);

                            if (_fateNpcID <= 0)
                            {
                                continue;
                            }

                            int.TryParse(_fatePopGroupData[4 + (j * 2)].GetCellValue(), out int _maxCount);

                            List<CellValueData>? _fateNpcRowData = mExcelSheetFateNpc.GetRowCellDataByTargetKeysAndValues(
                                new List<int> { 0 },
                                new List<string> { _fateNpcID.ToString() }
                            );

                            if ((_fateNpcRowData == null) || (_fateNpcRowData.Count < 1))
                            {
                                throw new Exception($"无法找到 FateNpc数据,ID是：{_fateNpcID}");
                            }

                            int _levelReferenceID = 0;

                            do
                            {
                                int.TryParse(_fateNpcRowData[3].GetCellValue(), out _levelReferenceID);

                                if (_levelReferenceID > 0)
                                {
                                    break;
                                }

                                int.TryParse(_fateNpcRowData[4].GetCellValue(), out _levelReferenceID);

                                if (_levelReferenceID > 0)
                                {
                                    break;
                                }

                                int.TryParse(_fateNpcRowData[5].GetCellValue(), out _levelReferenceID);

                                if (_levelReferenceID > 0)
                                {
                                    break;
                                }
                            }
                            while (false);

                            if (_levelReferenceID <= 0)
                            {
                                Console.WriteLine($"无法找到LevelReferenceID, FateNpcID : {_fateNpcID}，请检查");

                                continue;
                            }

                            int _findIndex = _tempMonsterList.FindIndex(x => x.FateNpcID == _fateNpcID);

                            if (_findIndex < 0)
                            {
                                string _popRangePosInfo = CommonUtil.GetPosInfoByLevelReferenceId(
                                    _levelReferenceID,
                                    mExcelSheetLevelReference,
                                    true,
                                    false,
                                    true
                                );

                                if (!_writeDataForFatePopGroupMonster.TryGetValue(_targetFateID, out List<FateNpcData>? _targetFateNpcDataList))
                                {
                                    _targetFateNpcDataList                          = new List<FateNpcData>();
                                    _writeDataForFatePopGroupMonster[_targetFateID] = _targetFateNpcDataList;
                                }

                                FateNpcData _newData = new FateNpcData();

                                int.TryParse(_fateNpcRowData[mFateNpcIdleRangeIndex].GetCellValue(), out int _idleRangeLevelRefID);

                                if (_idleRangeLevelRefID > 0)
                                {
                                    string _idleRangePosInfo = CommonUtil.GetPosInfoByLevelReferenceId(
                                        _idleRangeLevelRefID,
                                        mExcelSheetLevelReference,
                                        true,
                                        false,
                                        true
                                    );

                                    _newData.IdleRangePosInfo = _idleRangePosInfo;
                                }
                                else
                                {
                                    _newData.IdleRangePosInfo = _popRangePosInfo;
                                }

                                int.TryParse(_fateNpcRowData[mFateNpcDepopRangeIndex].GetCellValue(), out int _depopRangeLevelRefID);

                                if (_depopRangeLevelRefID > 0)
                                {
                                    string _depopRangePosInfo = CommonUtil.GetPosInfoByLevelReferenceId(
                                        _depopRangeLevelRefID,
                                        mExcelSheetLevelReference,
                                        true,
                                        false,
                                        true
                                    );

                                    _newData.DepopRangePosInfo = _depopRangePosInfo;
                                }

                                string _msg =
                                $"FateNpcID:[{_fateNpcID}] ; Pos:[{_popRangePosInfo}], IdlePos:[{_newData.IdleRangePosInfo}], DepopPos:[{_newData.DepopRangePosInfo}]";

                                _newData.FateNpcID       = _fateNpcID;
                                _newData.PopRangePosInfo = _popRangePosInfo;
                                _newData.LogInfo         = _msg;
                                _newData.MaxCount        = _maxCount;
                                _tempMonsterList.Add(_newData);
                                _targetFateNpcDataList.Add(_newData);
                            }
                            else
                            {
                                _tempMonsterList[_findIndex].MinCount++;

                                if (_tempMonsterList[_findIndex].MinCount > _tempMonsterList[_findIndex].MaxCount)
                                {
                                    _tempMonsterList[_findIndex].MaxCount = _tempMonsterList[_findIndex].MinCount;
                                }
                            }
                        }
                    }

                    // 这里是NPC的
                    do
                    {
                        string _tempFatePopGroupStr = _targetRowData[mFateOriginAllyIndex].GetCellValue();

                        if (string.IsNullOrEmpty(_tempFatePopGroupStr))
                        {
                            break;
                        }

                        List<CellValueData>? _originRowData = mExcelSheetFatePopGroupOrigin.GetRowCellDataByTargetKeysAndValues(
                            new List<int> { 2 },
                            new List<string> { _tempFatePopGroupStr }
                        );

                        if ((_originRowData == null) || (_originRowData.Count < 1))
                        {
                            throw new Exception(
                                $"1 无法在端游元表 FatePopGroup 中找到数据，KEYDevCode是：[{_tempFatePopGroupStr}], ColumIndex:[{mFateOriginPopRange00Index}],Row:[{_targetRowData[0].GetCellValue()}]"
                            );
                        }

                        int.TryParse(_originRowData[0].GetCellValue(), out int _fatePopGroupID);

                        if (_fatePopGroupID <= 0)
                        {
                            throw new Exception(
                                $"2 无法在端游元表 FatePopGroup 中找到数据，KEYDevCode是：[{_tempFatePopGroupStr}], ColumIndex:[{mFateOriginPopRange00Index}],Row:[{_targetRowData[0].GetCellValue()}]"
                            );
                        }

                        List<CellValueData>? _fatePopGroupData = mCSVSheetFatePopGroup.GetRowCellDataByTargetKeysAndValues(
                            new List<int> { 0 },
                            new List<string> { _fatePopGroupID.ToString() }
                        );

                        if ((_fatePopGroupData == null) || (_fatePopGroupData.Count < 1))
                        {
                            throw new Exception($"无法在 FatePopGroup.csv 中找到ID: {_fatePopGroupID}，请检查");
                        }

                        for (int j = 0; j <= 15; ++j)
                        {
                            int.TryParse(_fatePopGroupData[3 + (j * 2)].GetCellValue(), out int _fateNpcID);

                            if (_fateNpcID <= 0)
                            {
                                continue;
                            }

                            int.TryParse(_fatePopGroupData[4 + (j * 2)].GetCellValue(), out int _maxCount);

                            List<CellValueData>? _fateNpcRowData = mExcelSheetFateNpc.GetRowCellDataByTargetKeysAndValues(
                                new List<int> { 0 },
                                new List<string> { _fateNpcID.ToString() }
                            );

                            if ((_fateNpcRowData == null) || (_fateNpcRowData.Count < 1))
                            {
                                throw new Exception($"无法找到 FateNpc数据,ID是：{_fateNpcID}");
                            }

                            int _popLevelRefID = 0;

                            do
                            {
                                int.TryParse(_fateNpcRowData[mFateNpcLayoutIDIndex].GetCellValue(), out _popLevelRefID);

                                if (_popLevelRefID > 0)
                                {
                                    break;
                                }

                                int.TryParse(_fateNpcRowData[mFateNpcPopRangeIndex].GetCellValue(), out _popLevelRefID);

                                if (_popLevelRefID > 0)
                                {
                                    break;
                                }

                                int.TryParse(_fateNpcRowData[mFateNpcRandomPopRange00Index].GetCellValue(), out _popLevelRefID);

                                if (_popLevelRefID > 0)
                                {
                                    break;
                                }
                            }
                            while (false);

                            if (_popLevelRefID <= 0)
                            {
                                Console.WriteLine($"无法找到LevelReferenceID, FateNpcID : {_fateNpcID}，请检查");

                                continue;
                            }

                            int _findIndex = _tempNpcMonsterList.FindIndex(x => x.FateNpcID == _fateNpcID);

                            if (_findIndex < 0)
                            {
                                string _popRangePosInfo = CommonUtil.GetPosInfoByLevelReferenceId(
                                    _popLevelRefID,
                                    mExcelSheetLevelReference,
                                    true,
                                    false,
                                    true
                                );

                                if (!_writeDataForFatePopGroupNpcMonster.TryGetValue(
                                        _targetFateID,
                                        out List<FateNpcData>? _targetFateNpcDataList
                                    ))
                                {
                                    _targetFateNpcDataList                             = new List<FateNpcData>();
                                    _writeDataForFatePopGroupNpcMonster[_targetFateID] = _targetFateNpcDataList;
                                }

                                FateNpcData _newData = new FateNpcData();

                                int.TryParse(_fateNpcRowData[mFateNpcIdleRangeIndex].GetCellValue(), out int _idleRangeLevelRefID);

                                if (_idleRangeLevelRefID > 0)
                                {
                                    string _idleRangePosInfo = CommonUtil.GetPosInfoByLevelReferenceId(
                                        _idleRangeLevelRefID,
                                        mExcelSheetLevelReference,
                                        true,
                                        false,
                                        true
                                    );

                                    _newData.IdleRangePosInfo = _idleRangePosInfo;
                                }
                                else
                                {
                                    _newData.IdleRangePosInfo = _popRangePosInfo;
                                }

                                int.TryParse(_fateNpcRowData[mFateNpcDepopRangeIndex].GetCellValue(), out int _depopRangeLevelRefID);

                                if (_depopRangeLevelRefID > 0)
                                {
                                    string _depopRangePosInfo = CommonUtil.GetPosInfoByLevelReferenceId(
                                        _depopRangeLevelRefID,
                                        mExcelSheetLevelReference,
                                        true,
                                        false,
                                        true
                                    );

                                    _newData.DepopRangePosInfo = _depopRangePosInfo;
                                }

                                string _msg =
                                $"FateNpcID:[{_fateNpcID}] ; Pos:[{_popRangePosInfo}], IdlePos:[{_newData.IdleRangePosInfo}], DepopPos:[{_newData.DepopRangePosInfo}]";

                                _newData.FateNpcID       = _fateNpcID;
                                _newData.PopRangePosInfo = _popRangePosInfo;
                                _newData.LogInfo         = _msg;
                                _newData.MaxCount        = _maxCount;

                                _targetFateNpcDataList.Add(_newData);
                                _tempNpcMonsterList.Add(_newData);
                            }
                            else
                            {
                                _tempNpcMonsterList[_findIndex].MinCount++;

                                if (_tempNpcMonsterList[_findIndex].MinCount > _tempNpcMonsterList[_findIndex].MaxCount)
                                {
                                    _tempNpcMonsterList[_findIndex].MaxCount = _tempNpcMonsterList[_findIndex].MinCount;
                                }
                            }
                        }
                    }
                    while (false);
                }

                // 写入 Compare
                if (mTest)
                {
                    ExcelPackage? _package = mExcelCompare?.GetExcelPackage();

                    if (_package == null)
                    {
                        throw new Exception("获取 Package 为空，请检查");
                    }

                    ExcelWorksheet? _sheet = _package.Workbook.Worksheets[0];

                    //// monster 的
                    //{
                    //foreach (var _pair in _oldMonsterListDataMap)
                    //{
                    //    _newMonsterListDataMap.TryGetValue(_pair.Key, out var _newInfoList);

                    //    _sheet.Cells[1 + i * 2, 1].Value = _pair.Key.ToString();
                    //    for (int j = 0; j < _pair.Value.Count; ++j)
                    //    {
                    //        if (_newInfoList != null && j < _newInfoList.Count)
                    //        {
                    //            _pair.Value[j].FateNpcID = _newInfoList[j].FateNpcID;
                    //        }

                    //        _sheet.Cells[1 + i * 2, j + 2].Value = $"FateNpcID:[{_pair.Value[j].FateNpcID}] ; " +
                    //            _pair.Value[j].LogInfo;
                    //    }
                    //}

                    //foreach (var _pair in _newMonsterListDataMap)
                    //{
                    //    _sheet.Cells[2 + i * 2, 1].Value = _pair.Key.ToString();
                    //    for (int j = 0; j < _pair.Value.Count; ++j)
                    //    {
                    //        _sheet.Cells[2 + i * 2, j + 2].Value = _pair.Value[j].LogInfo;
                    //    }
                    //}
                    //}

                    // NPC的
                    {
                        foreach (KeyValuePair<int, List<FateNpcData>> _pair in _oldNpcMonsterListDataMap)
                        {
                            _newNpcMonsterListDataMap.TryGetValue(_pair.Key, out List<FateNpcData>? _newInfoList);

                            _sheet.Cells[1 + (i * 2), 1].Value = _pair.Key.ToString();

                            for (int j = 0; j < _pair.Value.Count; ++j)
                            {
                                if ((_newInfoList != null) && (j < _newInfoList.Count))
                                {
                                    _pair.Value[j].FateNpcID = _newInfoList[j].FateNpcID;
                                }

                                _sheet.Cells[1 + (i * 2), j + 2].Value = $"FateNpcID:[{_pair.Value[j].FateNpcID}] ; " + _pair.Value[j].LogInfo;
                            }
                        }

                        foreach (KeyValuePair<int, List<FateNpcData>> _pair in _newNpcMonsterListDataMap)
                        {
                            _sheet.Cells[2 + (i * 2), 1].Value = _pair.Key.ToString();

                            for (int j = 0; j < _pair.Value.Count; ++j)
                            {
                                _sheet.Cells[2 + (i * 2), j + 2].Value = _pair.Value[j].LogInfo;
                            }
                        }
                    }
                }
                else
                {
                    // 这里要做旧数据映射，但是不要写入到 sheet里面
                    foreach (KeyValuePair<int, List<FateNpcData>> _pair in _oldMonsterListDataMap)
                    {
                        _newMonsterListDataMap.TryGetValue(_pair.Key, out List<FateNpcData>? _newInfoList);

                        for (int j = 0; j < _pair.Value.Count; ++j)
                        {
                            if ((_newInfoList != null) && (j < _newInfoList.Count))
                            {
                                _pair.Value[j].FateNpcID = _newInfoList[j].FateNpcID;

                                _oldDataMonsterIDToFateNpcID[_pair.Value[j].MonsterID] = _newInfoList[j].FateNpcID;
                            }
                        }
                    }

                    foreach (KeyValuePair<int, List<FateNpcData>> _pair in _oldNpcMonsterListDataMap)
                    {
                        _newNpcMonsterListDataMap.TryGetValue(_pair.Key, out List<FateNpcData>? _newInfoList);

                        for (int j = 0; j < _pair.Value.Count; ++j)
                        {
                            if ((_newInfoList != null) && (j < _newInfoList.Count))
                            {
                                _pair.Value[j].FateNpcID = _newInfoList[j].FateNpcID;

                                _oldDataNpcMonsterIDToFateNpcID[_pair.Value[j].MonsterID] = _newInfoList[j].FateNpcID;
                            }
                        }
                    }
                }
            }

            if (mTest)
            {
                mExcelCompare?.SaveFile();
            }
            else
            {
                //// 先处理一下 怪物表
                //{
                //    Dictionary<int, List<CellValueData>> _cacheMonsterDataMap = new Dictionary<int, List<CellValueData>>();
                //    {
                //        var _allDataList = mExcelSheetMonster.GetAllDataList();
                //        if (_allDataList == null || _allDataList.Count < 1)
                //        {
                //            throw new Exception($"无法获取所有的 怪物表 数据，请检查!");
                //        }

                //        foreach (var _dataList in _allDataList)
                //        {
                //            int.TryParse(_dataList[mMonsterFateNpcIdIndex].GetCellValue(), out var _fateNpcID);
                //            if (_fateNpcID > 0)
                //            {
                //                _cacheMonsterDataMap[_fateNpcID] = _dataList;
                //            }
                //        }
                //    }

                //    // 这里将新数据的 MonsterID 设置进去
                //    {
                //        foreach (var _pair in _writeDataForFatePopGroupMonster)
                //        {
                //            foreach (var _data in _pair.Value)
                //            {
                //                if (!_cacheMonsterDataMap.TryGetValue(_data.FateNpcID, out var _rowDataList))
                //                {
                //                    throw new Exception($"无法在Monster中找到数据，基于 FateNPCID，请检查");
                //                }

                //                int.TryParse(_rowDataList[mMonsterIDIndex].GetCellValue(), out var _monsterID);
                //                _data.MonsterID = _monsterID;
                //                int.TryParse(_rowDataList[mMonsterBaseIDIndex].GetCellValue(), out var _baseID);
                //                _data.BaseID = _baseID;
                //            }
                //        }

                //        foreach (var _pair in _writeDataForFatePopGroupNpcMonster)
                //        {
                //            foreach (var _data in _pair.Value)
                //            {
                //                if (!_cacheMonsterDataMap.TryGetValue(_data.FateNpcID, out var _rowDataList))
                //                {
                //                    throw new Exception($"无法在Monster中找到数据，基于 FateNPCID，请检查");
                //                }

                //                int.TryParse(_rowDataList[mMonsterIDIndex].GetCellValue(), out var _monsterID);
                //                _data.MonsterID = _monsterID;
                //                int.TryParse(_rowDataList[mMonsterBaseIDIndex].GetCellValue(), out var _baseID);
                //                _data.BaseID = _baseID;
                //            }
                //        }
                //    }

                //    //// 处理旧数据中关于 Monster的
                //    //{
                //    //    // 写入怪物表相关数据
                //    //    foreach (var _pair in _oldDataMonsterIDToFateNpcID)
                //    //    {
                //    //        var _oldRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());
                //    //        if (_oldRowData == null || _oldRowData.Count < 1)
                //    //        {
                //    //            Console.WriteLine($"错误，无法在 G怪物表 中获取数据，MonsterID是：{_pair.Key}");
                //    //            continue;
                //    //        }

                //    //        var _newRowData = mExcelSheetMonster.GetRowCellDataByTargetKeysAndValues(
                //    //            new List<int> { mMonsterFateNpcIdIndex },
                //    //            new List<string> { _pair.Value.ToString() }
                //    //        );

                //    //        if (_newRowData == null || _newRowData.Count < 1)
                //    //        {
                //    //            throw new Exception($"错误,无法在怪物表中找到数据，基于Fate关联表，ID 是：[{_pair.Value}]");
                //    //        }

                //    //        _newRowData[mMonsterLevelColumIndex].SetCellValue(
                //    //            _oldRowData[mMonsterLevelColumIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterAttrIDColumIndex].SetCellValue(
                //    //            _oldRowData[mMonsterAttrIDColumIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterAITableIDIndex].SetCellValue(
                //    //            _oldRowData[mMonsterAITableIDIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterFateIDIndex].SetCellValue(
                //    //            _oldRowData[mMonsterFateIDIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterNameIndex].SetCellValue(
                //    //            _oldRowData[mMonsterNameIndex].GetCellValue()
                //    //        );
                //    //    }
                //    //}

                //    //// 处理旧数据中，关于NpcMonster 的
                //    //{
                //    //    // 写入怪物表相关数据
                //    //    foreach (var _pair in _oldDataNpcMonsterIDToFateNpcID)
                //    //    {
                //    //        var _oldRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());
                //    //        if (_oldRowData == null || _oldRowData.Count < 1)
                //    //        {
                //    //            Console.WriteLine($"错误，无法在 G怪物表 中获取数据，MonsterID是：{_pair.Key}");
                //    //            continue;
                //    //        }

                //    //        var _newRowData = mExcelSheetMonster.GetRowCellDataByTargetKeysAndValues(
                //    //            new List<int> { mMonsterFateNpcIdIndex },
                //    //            new List<string> { _pair.Value.ToString() }
                //    //        );

                //    //        if (_newRowData == null || _newRowData.Count < 1)
                //    //        {
                //    //            throw new Exception($"错误,无法在怪物表中找到数据，基于Fate关联表，ID 是：[{_pair.Value}]");
                //    //        }

                //    //        _newRowData[mMonsterLevelColumIndex].SetCellValue(
                //    //            _oldRowData[mMonsterLevelColumIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterAttrIDColumIndex].SetCellValue(
                //    //            _oldRowData[mMonsterAttrIDColumIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterAITableIDIndex].SetCellValue(
                //    //            _oldRowData[mMonsterAITableIDIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterFateIDIndex].SetCellValue(
                //    //            _oldRowData[mMonsterFateIDIndex].GetCellValue()
                //    //        );
                //    //        _newRowData[mMonsterNameIndex].SetCellValue(
                //    //            _oldRowData[mMonsterNameIndex].GetCellValue()
                //    //        );
                //    //    }
                //    //}

                //    //// 写入 Fate 创建表相关数据
                //    //{
                //    //    // monster id 相关
                //    //    {
                //    //        foreach (var _pair in _writeDataForFatePopGroupMonster)
                //    //        {
                //    //            var _targetRowData = mExcelSheetPopGroupInFateExcel.GetCacheRowDataListByKeyStr(
                //    //                _pair.Key.ToString()
                //    //            );

                //    //            if (_targetRowData == null || _targetRowData.Count < 1)
                //    //            {
                //    //                throw new Exception($"错误，无法获取 Fate 创建物表数据，FateID 是 : [{_pair.Key}]");
                //    //            }

                //    //            for (int i = 0; i < _pair.Value.Count; ++i)
                //    //            {
                //    //                var _data = _pair.Value[i];
                //    //                // 这里写入新的 MonsterID ，数量，POS数据
                //    //                if (_data.MonsterID <= 0)
                //    //                {
                //    //                    throw new Exception($"错误，Monster数据无效，FATEID:[{_pair.Key}], index:[]");
                //    //                }

                //    //                if (i > 15)
                //    //                {
                //    //                    Console.WriteLine($"错误，怪物的数量超出了范围，请检查! FateID : [{_pair.Key}]");
                //    //                    break;
                //    //                }

                //    //                var _indexOffset = i * 15;

                //    //                _targetRowData[mFatePopGroupFirstMonsterIDStartIndex + _indexOffset].SetCellValue(_data.MonsterID.ToString());
                //    //                _targetRowData[mFatePopGroupFirstMonsterPosStartIndex + _indexOffset].SetCellValue(_data.PopRangePosInfo);
                //    //                _targetRowData[mFatePopGroupFirstMonsterBaseIDIndex + _indexOffset].SetCellValue(_data.BaseID.ToString());
                //    //                if (_data.MaxCount > 0)
                //    //                {
                //    //                    _targetRowData[mFatePopGroupFirstMonsteMinrCountIndex + _indexOffset].SetCellValue(_data.MinCount.ToString());
                //    //                    _targetRowData[mFatePopGroupFirstMonsterFirstMaxCountIndex + _indexOffset].SetCellValue(_data.MaxCount.ToString());
                //    //                }
                //    //                else
                //    //                {
                //    //                    _targetRowData[mFatePopGroupFirstMonsteMinrCountIndex + _indexOffset].SetCellValue("0");
                //    //                    _targetRowData[mFatePopGroupFirstMonsterFirstMaxCountIndex + _indexOffset].SetCellValue("0");
                //    //                }
                //    //                _targetRowData[mFatePopGroupFirstMonsterIdleRangeIndex + _indexOffset].SetCellValue(_data.IdleRangePosInfo);
                //    //                _targetRowData[mFatePopGroupFirstMonsterDepopRangeIndex + _indexOffset].SetCellValue(_data.DepopRangePosInfo);
                //    //            }
                //    //        }
                //    //    }

                //    //    // NPC 相关
                //    //    {
                //    //        foreach (var _pair in _writeDataForFatePopGroupNpcMonster)
                //    //        {
                //    //            var _targetRowData = mExcelSheetPopGroupInFateExcel.GetCacheRowDataListByKeyStr(
                //    //                _pair.Key.ToString()
                //    //            );

                //    //            if (_targetRowData == null || _targetRowData.Count < 1)
                //    //            {
                //    //                throw new Exception($"错误，无法获取 Fate 创建物表数据，FateID 是 : [{_pair.Key}]");
                //    //            }

                //    //            for (int tempIndex = 0; tempIndex < _pair.Value.Count; ++tempIndex)
                //    //            {
                //    //                var _data = _pair.Value[tempIndex];
                //    //                // 这里写入新的 MonsterID ，数量，POS数据
                //    //                if (_data.MonsterID <= 0)
                //    //                {
                //    //                    throw new Exception($"错误，Monster数据无效，FATEID:[{_pair.Key}], index:[]");
                //    //                }

                //    //                if (tempIndex > 15)
                //    //                {
                //    //                    Console.WriteLine($"错误，怪物的数量超出了范围，请检查! FateID : [{_pair.Key}]");
                //    //                    break;
                //    //                }

                //    //                var _indexOffset = tempIndex * 6;

                //    //                _targetRowData[mFatePopGroupFirstNpcIDIndex + _indexOffset].SetCellValue(_data.MonsterID.ToString());
                //    //                _targetRowData[mFatePopGroupFirstNpcPopRangeIndex + _indexOffset].SetCellValue(_data.PopRangePosInfo);

                //    //                if (_data.MaxCount > 0)
                //    //                {
                //    //                    _targetRowData[mFatePopGroupFirstNpcMinNumIndex + _indexOffset].SetCellValue(_data.MinCount.ToString());
                //    //                    _targetRowData[mFatePopGroupFirstNpcMaxNumIndex + _indexOffset].SetCellValue(_data.MaxCount.ToString());
                //    //                }
                //    //                else
                //    //                {
                //    //                    _targetRowData[mFatePopGroupFirstNpcMinNumIndex + _indexOffset].SetCellValue("0");
                //    //                    _targetRowData[mFatePopGroupFirstNpcMaxNumIndex + _indexOffset].SetCellValue("0");
                //    //                }

                //    //                _targetRowData[mFatePopGroupFirstNpcIdleRangeIndex + _indexOffset].SetCellValue(_data.IdleRangePosInfo);
                //    //                _targetRowData[mFatePopGroupFirstNpcDepopRangeIndex + _indexOffset].SetCellValue(_data.DepopRangePosInfo);
                //    //            }
                //    //        }
                //    //    }
                //    //}
                //}

                // Key : OldMonsterID ; value : FateNpcID
                foreach (KeyValuePair<int, int> _pair in _oldDataMonsterIDToFateNpcID)
                {
                    List<CellValueData>? _rowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());

                    if ((_rowData == null) || (_rowData.Count < 1))
                    {
                        Console.WriteLine($"错误，有无法获取数据的 MonsterID : [{_pair.Key}]");

                        continue;
                    }

                    _rowData[mMonsterFateNpcIdIndex].SetCellValue(_pair.Value.ToString());
                }

                // key : OldNpcMonster ; value : FateNpcID
                foreach (KeyValuePair<int, int> _pair in _oldDataNpcMonsterIDToFateNpcID)
                {
                    List<CellValueData>? _rowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());

                    if ((_rowData == null) || (_rowData.Count < 1))
                    {
                        Console.WriteLine($"错误，有无法获取数据的 MonsterID : [{_pair.Key}]");

                        continue;
                    }

                    _rowData[mMonsterFateNpcIdIndex].SetCellValue(_pair.Value.ToString());
                }

                // 写入数据
                mExcelSheetMonster.SaveSheet();
                mExcelMonster.SaveFile();
                //mExcelSheetPopGroupInFateExcel.SaveSheet();
                //mExcelFate.SaveFile();
            }

            return true;
        }
    }
}
