using ExcelTool;

namespace ExcelConsole
{
    public class ProcessForFateNpcToMonster : ProcessBase
    {
        private ExcelFileData?  mExcelFateNpc;
        private ExcelSheetData? mExcelSheetFateNpc;

        private ExcelFileData?  mExcelMonster;
        private ExcelSheetData? mExcelSheetMonster;

        private ExcelFileData?  mExcelLevelReference;
        private ExcelSheetData? mExcelSheetLevelReference;

        private ExcelFileData?  mExcelMap;
        private ExcelSheetData? mExcelSheetMap;

        private ExcelFileData?  mExcelMapBNpcID;
        private ExcelSheetData? mExcelSheetMapBNpcID;

        private ExcelFileData?  mExcelBNpcName;
        private ExcelSheetData? mExcelSheetBNpcName;

        private ExcelFileData?  mExcelBNpcBase;
        private ExcelSheetData? mExcelSheetBNpcBase;

        private ExcelFileData?  mExcelEObj;
        private ExcelSheetData? mExcelSheetEObj;

        private class FateNpcToMonsterData
        {
            public          int    LevelReferenceID;
            public          int    MapID;
            public          int    BNpcID;
            public          string SEMapID         = string.Empty;
            public          string BigMapName      = string.Empty;
            public          string SmallMapName    = string.Empty;
            public          string MonsterIDKeyStr = string.Empty; // FM地图大区 + FM地图子区
            public          string FateNpcName     = string.Empty;
            public readonly string CampStr         = string.Empty; // 阵营字符串

            public int OverrideBNpcID; // 继承的 BNpcID

            public bool IsEObj;
        }

        private void InternalLoadFile()
        {
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
                string _tempPath   = Path.Combine(currentPath, "../FileFolder");
                FolderPath = Path.Combine(_tempPath, "ExportMonster");
                DirectoryInfo _tempFileInfo = new DirectoryInfo(FolderPath);

                if (!_tempFileInfo.Exists)
                {
                    Console.WriteLine($"错误，{_tempFileInfo} 路径不存在");

                    return;
                }
            }

            // 加载 EObj
            {
                string _tempPath = Path.Combine(FolderPath, "EObj.xlsx");
                mExcelEObj      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetEObj = mExcelEObj.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetEObj == null)
                {
                    throw new Exception("mExcelEObj.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetEObj.SetKeyStartRowIndexInSheet(5);
                mExcelSheetEObj.SetKeyStartColmIndexInSheet(2);
                mExcelSheetEObj.SetContentStartRowIndexInSheet(10);

                mExcelSheetEObj.ReloadKey();
                List<KeyData> _allKeyDataList = mExcelSheetEObj.GetKeyListData();
                _allKeyDataList[0].IsMainKey = true;

                mExcelSheetEObj.LoadAllCellData(true);
            }

            // 加载 FateNpc
            {
                string _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                mExcelFateNpc      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetFateNpc = mExcelFateNpc?.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetFateNpc == null)
                {
                    throw new Exception("mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetFateNpc.SetKeyStartRowIndexInSheet(5);
                mExcelSheetFateNpc.SetKeyStartColmIndexInSheet(2);
                mExcelSheetFateNpc.SetContentStartRowIndexInSheet(11);

                mExcelSheetFateNpc.ReloadKey();
                List<KeyData> _allKeyDataList = mExcelSheetFateNpc.GetKeyListData();
                _allKeyDataList[0].IsMainKey = true;

                mExcelSheetFateNpc.LoadAllCellData(true);
            }

            // 加载 BNpcBase 表
            {
                string _tempPath = Path.Combine(FolderPath, "BNpcBase.xlsx");
                mExcelBNpcBase      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetBNpcBase = mExcelBNpcBase.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetBNpcBase == null)
                {
                    throw new Exception("mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetBNpcBase.SetKeyStartRowIndexInSheet(5);
                mExcelSheetBNpcBase.SetKeyStartColmIndexInSheet(2);
                mExcelSheetBNpcBase.SetContentStartRowIndexInSheet(11);

                mExcelSheetBNpcBase.ReloadKey();
                List<KeyData> _allKeyDataList = mExcelSheetBNpcBase.GetKeyListData();
                _allKeyDataList[0].IsMainKey = true;

                mExcelSheetBNpcBase.LoadAllCellData(true);
            }

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
                _allKeyDataList[CommonUtil.GetIndexByZm("I", 1)].IsMainKey = true;

                mExcelSheetMonster.LoadAllCellData(true);
            }

            // 加载 LevelReference
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

            // 加载 Map
            {
                string _tempPath = Path.Combine(FolderPath, "Map.xlsx");
                mExcelMap      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetMap = mExcelMap.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetMap == null)
                {
                    throw new Exception("mExcelMap.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetMap.SetKeyStartRowIndexInSheet(5);
                mExcelSheetMap.SetKeyStartColmIndexInSheet(2);
                mExcelSheetMap.SetContentStartRowIndexInSheet(10);

                mExcelSheetMap.ReloadKey();
                mExcelSheetMap.LoadAllCellData(true);
            }

            // 加载 MapBNpcID
            {
                string _tempPath = Path.Combine(FolderPath, "MapBnpcid.xlsx");
                mExcelMapBNpcID      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetMapBNpcID = mExcelMapBNpcID.GetWorkSheetByIndex(1) as ExcelSheetData;

                if (mExcelSheetMapBNpcID == null)
                {
                    throw new Exception("mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetMapBNpcID.SetKeyStartRowIndexInSheet(1);
                mExcelSheetMapBNpcID.SetKeyStartColmIndexInSheet(1);
                mExcelSheetMapBNpcID.SetContentStartRowIndexInSheet(2);

                mExcelSheetMapBNpcID.ReloadKey();
                mExcelSheetMapBNpcID.LoadAllCellData(true);
            }

            // 加载 BNpcName
            {
                string _tempPath = Path.Combine(FolderPath, "BNpcName.xlsx");
                mExcelBNpcName      = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetBNpcName = mExcelBNpcName.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetBNpcName == null)
                {
                    throw new Exception("mExcelBNpcName.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetBNpcName.SetKeyStartRowIndexInSheet(5);
                mExcelSheetBNpcName.SetKeyStartColmIndexInSheet(2);
                mExcelSheetBNpcName.SetContentStartRowIndexInSheet(11);

                mExcelSheetBNpcName.ReloadKey();
                mExcelSheetBNpcName.LoadAllCellData(true);
            }
        }

        private static readonly int mIndexOfMonsterCampID = CommonUtil.GetIndexByZm("AS", 1);

        private static readonly int mIndexOffset = 2;

        private static readonly int mIndexOfFateNpcID         = CommonUtil.GetIndexByZm("B", mIndexOffset); // -2 是因为 KEY 是从 B 列开始的
        private static readonly int mIndexOfFateNpcBaseID     = CommonUtil.GetIndexByZm("J", mIndexOffset);
        private static readonly int mIndexOfFateNpcLayoutID   = CommonUtil.GetIndexByZm("K", mIndexOffset);
        private static readonly int mIndexOfFateNpcPopRange   = CommonUtil.GetIndexByZm("AA", mIndexOffset);
        private static readonly int mIndexOfFateNpcBaseNpc    = CommonUtil.GetIndexByZm("F", mIndexOffset);
        private static          int mIndexOfFatenpcKeyDevCode = CommonUtil.GetIndexByZm("D", mIndexOffset);
        private static readonly int mIndexOfLevelRefKeyDev    = CommonUtil.GetIndexByZm("D", mIndexOffset);
        private static readonly int mIndexOfLevelRefKeyAlias  = CommonUtil.GetIndexByZm("C", mIndexOffset);
        private static readonly int mIndexOfLevelRefKey       = CommonUtil.GetIndexByZm("B", mIndexOffset);
        private static readonly int mIndexOfLevelRefIsEObj    = CommonUtil.GetIndexByZm("M", mIndexOffset);
        private static readonly int mIndexOfLevelRefBaseID    = CommonUtil.GetIndexByZm("N", mIndexOffset);
        private static readonly int mIndexOfLevelRefMapID     = CommonUtil.GetIndexByZm("O", mIndexOffset);
        private static readonly int mIndexOfMapPath           = CommonUtil.GetIndexByZm("L", mIndexOffset);
        private static readonly int mFateNpcIndexOfName       = CommonUtil.GetIndexByZm("I", mIndexOffset);
        private static readonly int mFateNpcTextIndex         = CommonUtil.GetIndexByZm("L", mIndexOffset);
        private static readonly int mEobjNameIndex            = CommonUtil.GetIndexByZm("AD", mIndexOffset);
        private static readonly int mIndexOfFateNpcEntrType   = CommonUtil.GetIndexByZm("H", mIndexOffset);

        // G怪物表
        private static int mIndexOfMonsterFateNpcID = CommonUtil.GetIndexByZm("I", 1);

        public override bool Process()
        {
            InternalLoadFile();

            List<List<CellValueData>>? _allDataList = mExcelSheetFateNpc?.GetAllDataList();

            if ((mExcelSheetFateNpc == null) ||
                (mExcelSheetFateNpc == null) ||
                (mExcelSheetEObj    == null) ||
                (_allDataList       == null) ||
                (_allDataList.Count == 0))
            {
                throw new Exception("mExcelSheetFateNpc.GetAllDataList() 出错，数量无效");
            }

            Dictionary<int, FateNpcToMonsterData> _fateNpcDataMap = new Dictionary<int, FateNpcToMonsterData>();

            foreach (List<CellValueData> _singleFateNpcDataList in _allDataList)
            {
                if (!int.TryParse(_singleFateNpcDataList[mIndexOfFateNpcID].GetCellValue(), out int _fateNpcID) || (_fateNpcID <= 0))
                {
                    continue;
                }

                // 先找一下LayoutID
                string _levelRefStr = InternalGetLevelReferenceID(_singleFateNpcDataList);

                if (string.IsNullOrEmpty(_levelRefStr))
                {
                    // 这里去看一下，是否有继承的NPC
                    string _tempBaseNpcStr = _singleFateNpcDataList[mIndexOfFateNpcBaseNpc].GetCellValue();

                    if (string.IsNullOrEmpty(_tempBaseNpcStr))
                    {
                        CommonUtil.ShowError($"FateNpc : [{_fateNpcID}] 无法找到出生点, LevelReference 为空，且没有继承 FateNpc , 请检查");

                        continue;
                    }

                    List<CellValueData>? _targetFateNpcRowDataList = mExcelSheetFateNpc?.GetRowCellDataByTargetKeysAndValues(
                        new List<int> { 2 },
                        new List<string> { _tempBaseNpcStr }
                    );

                    if (_targetFateNpcRowDataList == null)
                    {
                        throw new Exception($"无法获取继承 FateNpc : [{_tempBaseNpcStr}], 原 FateNpcID 是 : [{_fateNpcID}]");
                    }

                    _levelRefStr = InternalGetLevelReferenceID(_targetFateNpcRowDataList);

                    if (string.IsNullOrEmpty(_levelRefStr))
                    {
                        CommonUtil.ShowError($"FateNpc : [{_fateNpcID}] 的继承 FateNpc : [{_tempBaseNpcStr}] 没有出生信息，请检查");

                        continue;
                    }
                }

                List<CellValueData>? _targetLevelRefRowDataList = mExcelSheetLevelReference?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { mIndexOfLevelRefKeyDev },
                    new List<string> { _levelRefStr }
                );

                if ((_targetLevelRefRowDataList == null) || (_targetLevelRefRowDataList.Count < 1))
                {
                    _targetLevelRefRowDataList = mExcelSheetLevelReference?.GetRowCellDataByTargetKeysAndValues(
                        new List<int> { mIndexOfLevelRefKeyAlias },
                        new List<string> { _levelRefStr }
                    );

                    if ((_targetLevelRefRowDataList == null) || (_targetLevelRefRowDataList.Count < 1))
                    {
                        _targetLevelRefRowDataList = mExcelSheetLevelReference?.GetRowCellDataByTargetKeysAndValues(
                            new List<int> { mIndexOfLevelRefKeyAlias },
                            new List<string> { _levelRefStr }
                        );

                        throw new Exception($"FateNpc : [{_fateNpcID}] 找不到 LevelRefStr 数据, Mapping 数据是：[{_levelRefStr}]请检查");
                    }
                }

                // 获取 level reference id 
                string _levelRefIDStr = _targetLevelRefRowDataList[mIndexOfLevelRefKey].GetCellValue();

                if (!int.TryParse(_levelRefIDStr, out int _levelRefID) || (_levelRefID <= 0))
                {
                    throw new Exception($"无法获取 LevelReferenceID , keyDev 是：[{_levelRefStr}]");
                }

                FateNpcToMonsterData _newFateNpcToMonsterData = new FateNpcToMonsterData();
                _fateNpcDataMap.Add(_fateNpcID, _newFateNpcToMonsterData);

                // 写入阵营相关  敵

                // 看是不是 eobj
                string _typeNameStr = _targetLevelRefRowDataList[mIndexOfLevelRefIsEObj].GetCellValue();

                if (!string.IsNullOrEmpty(_typeNameStr) && _typeNameStr.ToLower().Contains("eventobj"))
                {
                    _newFateNpcToMonsterData.IsEObj = true;
                }

                _newFateNpcToMonsterData.LevelReferenceID = _levelRefID;

                string _tempTypeStr = _singleFateNpcDataList[mIndexOfFateNpcEntrType].GetCellValue();

                if (!string.IsNullOrEmpty(_tempTypeStr) && _tempTypeStr.ToLower().Contains("eobj"))
                {
                    _newFateNpcToMonsterData.IsEObj = true;
                }

                string _overrideFateNpcStr = _singleFateNpcDataList[4].GetCellValue();

                if (!string.IsNullOrEmpty(_overrideFateNpcStr))
                {
                    List<CellValueData>? _tempOverrideFateNpcDataList = mExcelSheetFateNpc?.GetRowCellDataByTargetKeysAndValues(
                        new List<int> { 2 },
                        new List<string> { _overrideFateNpcStr }
                    );

                    if ((_tempOverrideFateNpcDataList == null) || (_tempOverrideFateNpcDataList.Count < 1))
                    {
                        throw new Exception($"无法获取 FateNpc 数据，KeyDev 是 : [{_overrideFateNpcStr}]");
                    }

                    int.TryParse(_tempOverrideFateNpcDataList[0].GetCellValue(), out _newFateNpcToMonsterData.OverrideBNpcID);
                }

                // 这里写入一下 BaseID
                {
                    // 先在 FateNpc 表里面看看 J 列的BaseID 是否有
                    string _tempBaseIDStr = _singleFateNpcDataList[mIndexOfFateNpcBaseID].GetCellValue();

                    if (!string.IsNullOrEmpty(_tempBaseIDStr))
                    {
                        if (_newFateNpcToMonsterData.IsEObj)
                        {
                            List<CellValueData>? _targetEObjDataList = mExcelSheetEObj.GetRowCellDataByTargetKeysAndValues(
                                new List<int> { 4 },
                                new List<string> { _tempBaseIDStr }
                            );

                            if ((_targetEObjDataList == null) || (_targetEObjDataList.Count < 1))
                            {
                                throw new Exception($"无法获取 EObj 数据，IDMappingStr 是 : [{_tempBaseIDStr}] , FateNpc : [{_fateNpcID}]");
                            }

                            int.TryParse(_targetEObjDataList[0].GetCellValue(), out _newFateNpcToMonsterData.BNpcID);
                        }
                        else
                        {
                            List<CellValueData>? _tempBNpcBaseDataList = mExcelSheetBNpcBase?.GetRowCellDataByTargetKeysAndValues(
                                new List<int> { 3 },
                                new List<string> { _tempBaseIDStr }
                            );

                            if ((_tempBNpcBaseDataList == null) || (_tempBNpcBaseDataList.Count < 1))
                            {
                                throw new Exception($"无法获取 BNpcBase 数据，ID是：[{_tempBaseIDStr}] , FateNpc : [{_fateNpcID}]");
                            }

                            int.TryParse(_tempBNpcBaseDataList[0].GetCellValue(), out _newFateNpcToMonsterData.BNpcID);
                        }
                    }
                    else
                    {
                        // 如果没有，那么去 LevelReference 表里面看 N 列是否有
                        if (_newFateNpcToMonsterData.BNpcID <= 0)
                        {
                            if (int.TryParse(_targetLevelRefRowDataList[mIndexOfLevelRefBaseID].GetCellValue(), out int _baseID) &&
                                (_baseID > 0))
                            {
                                _newFateNpcToMonsterData.BNpcID = _baseID;
                            }
                        }
                    }
                }

                // 获取 map id
                string _mapIDStr = _targetLevelRefRowDataList[mIndexOfLevelRefMapID].GetCellValue();

                if (!int.TryParse(_mapIDStr, out int _mapID) || (_mapID <= 0))
                {
                    Console.WriteLine($"FateNpc: [{_fateNpcID}] -> LevelReference : [{_levelRefID}] , 没有地图ID");
                    _fateNpcDataMap.Remove(_fateNpcID);

                    continue;
                }

                _newFateNpcToMonsterData.MapID = _mapID;

                // 去 Map.xlsx 查找一下
                List<CellValueData>? _targetMapRowDataList = mExcelSheetMap?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 0 },
                    new List<string> { _mapIDStr }
                );

                if (_targetMapRowDataList == null)
                {
                    throw new Exception($"无法找到 Map 数据，ID是：[{_mapIDStr}]");
                }

                string _pathStr = _targetMapRowDataList[mIndexOfMapPath].GetCellValue();

                if (string.IsNullOrEmpty(_pathStr))
                {
                    throw new Exception("Map 数据出错 , Path 数据为空");
                }

                // 去 MapBNpcID 表 查找一下
                List<CellValueData>? _targetMapBNpcIDRowDataList = mExcelSheetMapBNpcID?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 0 },
                    new List<string> { _pathStr }
                );

                if ((_targetMapBNpcIDRowDataList == null) || (_targetMapBNpcIDRowDataList.Count < 1))
                {
                    throw new Exception($"mExcelSheetMapBNpcID.GetRowCellDataByTargetKeysAndValues 错误，SE地图ID : {_pathStr}");
                }

                _newFateNpcToMonsterData.SEMapID      = _pathStr;
                _newFateNpcToMonsterData.BigMapName   = _targetMapBNpcIDRowDataList[1].GetCellValue();
                _newFateNpcToMonsterData.SmallMapName = _targetMapBNpcIDRowDataList[3].GetCellValue();

                _newFateNpcToMonsterData.MonsterIDKeyStr = _targetMapBNpcIDRowDataList[2].GetCellValue() +
                                                           _targetMapBNpcIDRowDataList[4].GetCellValue();
            }

            // 这里再去获取一下 FateNpcName :  Name , FateNpcBaseID
            foreach (KeyValuePair<int, FateNpcToMonsterData> _pair in _fateNpcDataMap)
            {
                InternalLoadFateNpcBaseIDAndName(_pair.Key, _pair.Value, _fateNpcDataMap);
            }

            Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();

            int                        _newIndex        = -1;
            List<List<CellValueData>>? _tempAllDataList = mExcelSheetMonster?.GetAllDataList();

            if (_tempAllDataList == null)
            {
                throw new Exception("mExcelSheetMonster?.GetAllDataList(); 为空，请检查!");
            }

            List<CellValueData> _templateRowData = _tempAllDataList[0];

            List<string> _templateDataList = new List<string>();

            for (int i = 0; i < _templateRowData.Count; ++i)
            {
                _templateDataList.Add(string.Empty);
            }

            int _profileIndex     = CommonUtil.GetIndexByZm("N", 1);
            int _nameIndex        = CommonUtil.GetIndexByZm("G", 1);
            int _synacTarget      = CommonUtil.GetIndexByZm("AT", 1);
            int _keepBodyIndex    = CommonUtil.GetIndexByZm("AX", 1);
            int _rotateSpeedIndex = CommonUtil.GetIndexByZm("BA", 1);
            int _minRoateIndex    = CommonUtil.GetIndexByZm("BB", 1);
            int _visionRadius     = CommonUtil.GetIndexByZm("BF", 1);
            int _ownerShipIndex   = CommonUtil.GetIndexByZm("BU", 1);
            int _distributeIndex  = CommonUtil.GetIndexByZm("BX", 1);

            // 准备写入信息
            foreach (KeyValuePair<int, FateNpcToMonsterData> _pair in _fateNpcDataMap)
            {
                int                  _targetIndex       = 0;
                List<string>         _finalStrList      = new List<string>();
                List<CellValueData>? _targetRowDataList = mExcelSheetMonster?.GetCacheRowDataListByKeyStr(_pair.Key.ToString());

                if ((_targetRowDataList == null) || (_targetRowDataList.Count < 1))
                {
                    // 新数据
                    _targetIndex = _newIndex;
                    --_newIndex;
                    _finalStrList = new List<string>();
                    _finalStrList.AddRange(_templateDataList);

                    int _newMonsterID = GetNextMonsterID(_pair.Value.MonsterIDKeyStr, 5);
                    _finalStrList[5] = _newMonsterID.ToString();

                    Console.WriteLine($"新写入数据 -> FateNpc : [{_pair.Key}] , MonsterID : [{_newMonsterID}]");
                }
                else
                {
                    // 旧数据
                    _targetIndex  = _targetRowDataList[0].GetCellRowIndexInSheet();
                    _finalStrList = CommonUtil.ParsRowCellDataToRowStringData(_targetRowDataList);
                }

                if (_finalStrList == null)
                {
                    throw new Exception("错误，没有数据，请检查！");
                }

                _finalStrList[0]                 = _pair.Value.SmallMapName + "FATE";
                _finalStrList[2]                 = _pair.Value.MapID.ToString();
                _finalStrList[3]                 = _pair.Value.BNpcID.ToString();
                _finalStrList[_nameIndex]        = _pair.Value.FateNpcName;
                _finalStrList[8]                 = _pair.Key.ToString();
                _finalStrList[_profileIndex]     = _pair.Value.BNpcID.ToString();
                _finalStrList[_synacTarget]      = "1";
                _finalStrList[_keepBodyIndex]    = "4000";
                _finalStrList[_rotateSpeedIndex] = "180";
                _finalStrList[_minRoateIndex]    = "15";

                _finalStrList[_visionRadius]     = "300";
                _finalStrList[_visionRadius + 1] = "200";
                _finalStrList[_visionRadius + 2] = "400";
                _finalStrList[_visionRadius + 3] = "5000";
                _finalStrList[_visionRadius + 4] = "5";

                _finalStrList[_ownerShipIndex]     = "伤害占比";
                _finalStrList[_ownerShipIndex + 1] = "5000";

                _finalStrList[_distributeIndex] = "随机掉落";

                _finalStrList[mIndexOfMonsterCampID] = _pair.Value.CampStr;

                _writeDataMap.Add(_targetIndex, _finalStrList);
            }

            foreach (KeyValuePair<int, List<string>> _pair in _writeDataMap)
            {
                mExcelSheetMonster?.WriteOneData(_pair.Key, _pair.Value, true);
            }

            mExcelMonster?.SaveFile();

            return true;
        }

        // 写入名字
        private void InternalLoadFateNpcBaseIDAndName(
            int                                   fateNpcKey,
            FateNpcToMonsterData                  targetData,
            Dictionary<int, FateNpcToMonsterData> totalData
        )
        {
            List<CellValueData>? _cachedFateNpcData = mExcelSheetFateNpc?.GetCacheRowDataListByKeyStr(fateNpcKey.ToString());

            if ((_cachedFateNpcData == null) || (_cachedFateNpcData.Count < 1))
            {
                throw new Exception($"无法获取缓存的数据，FateNpc : [{fateNpcKey}]");
            }

            // 如果有继承的表，那么去获取一下继承的数据
            if (targetData.OverrideBNpcID > 0)
            {
                // 获取一下父类
                FateNpcToMonsterData _overrideFateData = totalData[targetData.OverrideBNpcID];

                if (_overrideFateData.BNpcID <= 0)
                {
                    // 认为没加载，去加载一下
                    InternalLoadFateNpcBaseIDAndName(targetData.OverrideBNpcID, _overrideFateData, totalData);
                }

                if (_overrideFateData.BNpcID <= 0)
                {
                    // 认为没加载，去加载一下
                    throw new Exception($"FateNpc : [{targetData.OverrideBNpcID}] ，加载数据出错，请检查");
                }

                targetData.FateNpcName = _overrideFateData.FateNpcName;
                targetData.BNpcID      = _overrideFateData.BNpcID;

                return;
            }

            if (targetData.IsEObj)
            {
                List<CellValueData>? _targetEObjDataList = mExcelSheetEObj?.GetCacheRowDataListByKeyStr(targetData.BNpcID.ToString());

                if ((_targetEObjDataList == null) || (_targetEObjDataList.Count < 1))
                {
                    throw new Exception($"无法获取  Eobj 数据，ID是：[{targetData.BNpcID}] , FateNpc : [{fateNpcKey}]");
                }

                targetData.FateNpcName = _targetEObjDataList[mEobjNameIndex].GetCellValue();

                return;
            }

            string _bNPCNameMappingStr = _cachedFateNpcData[mFateNpcIndexOfName].GetCellValue();

            if (!string.IsNullOrEmpty(_bNPCNameMappingStr))
            {
                // 这里看下 BaseID 是否已经有了，如果有了， 那么去 BaseID 看看
                List<CellValueData>? _targetRowDataList = mExcelSheetBNpcName?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 4 },
                    new List<string> { _bNPCNameMappingStr }
                );

                if ((_targetRowDataList == null) || (_targetRowDataList.Count < 1))
                {
                    throw new Exception($"mExcelSheetBNpcName.GetRowCellDataByTargetKeysAndValues 错误，无法匹配，Str = [{_bNPCNameMappingStr}]");
                }

                targetData.FateNpcName = _targetRowDataList[mFateNpcTextIndex].GetCellValue();

                return;
            }

            // 获取 BNpcID
            if (targetData.BNpcID <= 0)
            {
                CommonUtil.ShowError($"数据出错，没有 BNpcID ，FateNpc ID  是 ： [{fateNpcKey}]");

                return;
            }

            List<CellValueData>? _cacheBNpcDataList = mExcelSheetBNpcBase?.GetCacheRowDataListByKeyStr(targetData.BNpcID.ToString());

            if ((_cacheBNpcDataList == null) || (_cacheBNpcDataList.Count < 1))
            {
                throw new Exception($"无法获取 BNpcBase 数据，ID是  : [{targetData.BNpcID}] , FateNpc : [{fateNpcKey}]");
            }

            string _tempBNpcNameStr = _cacheBNpcDataList[4].GetCellValue();

            if (!string.IsNullOrEmpty(_tempBNpcNameStr))
            {
                List<CellValueData>? _targetRowDataList = mExcelSheetBNpcName?.GetRowCellDataByTargetKeysAndValues(
                    new List<int> { 4 },
                    new List<string> { _tempBNpcNameStr }
                );

                if ((_targetRowDataList == null) || (_targetRowDataList.Count < 1))
                {
                    throw new Exception(
                        $"mExcelSheetBNpcName.GetRowCellDataByTargetKeysAndValues 错误，BaseID : [{targetData.BNpcID}] , FateNpc : [{fateNpcKey}] , 匹配Str = [{_tempBNpcNameStr}]"
                    );
                }

                targetData.FateNpcName = _targetRowDataList[mFateNpcTextIndex].GetCellValue();
            }
        }

        private string InternalGetLevelReferenceID(List<CellValueData> singleRowData)
        {
            // layoutID
            string _tempStr = singleRowData[mIndexOfFateNpcLayoutID].GetCellValue();

            if (!string.IsNullOrEmpty(_tempStr))
            {
                return _tempStr;
            }

            // PopRange
            _tempStr = singleRowData[mIndexOfFateNpcPopRange].GetCellValue();

            if (!string.IsNullOrEmpty(_tempStr))
            {
                return _tempStr;
            }

            // PopRange00
            _tempStr = singleRowData[mIndexOfFateNpcPopRange + 1].GetCellValue();

            if (!string.IsNullOrEmpty(_tempStr))
            {
                return _tempStr;
            }

            // PopRange01
            _tempStr = singleRowData[mIndexOfFateNpcPopRange + 2].GetCellValue();

            if (!string.IsNullOrEmpty(_tempStr))
            {
                return _tempStr;
            }

            // PopRange02
            _tempStr = singleRowData[mIndexOfFateNpcPopRange + 3].GetCellValue();

            if (!string.IsNullOrEmpty(_tempStr))
            {
                return _tempStr;
            }

            return string.Empty;
        }

        public Dictionary<string, int> MonsterExportIDMap = new Dictionary<string, int>(); // MonsterID专用的，保存的是地图相关的最大值

        public int GetNextMonsterID(string mapIDStr, int monsterIDIndex)
        {
            ExcelSheetData? _exportSheet = mExcelSheetMonster;

            if (_exportSheet == null)
            {
                throw new Exception($"{GetNextMonsterID} 出错，无法获取 mExcelSheetMonster");
            }

            if (!int.TryParse(mapIDStr, out int _tempCheckValue))
            {
                throw new Exception($"{GetNextMonsterID} 出错，传入的值 : {mapIDStr} ，无法解析为 int，请检查!");
            }

            _exportSheet.LoadAllCellData(false);

            List<KeyData> _keyList   = _exportSheet.GetKeyListData();
            KeyData       _targetKey = _keyList[monsterIDIndex];

            if (_targetKey == null)
            {
                throw new Exception("没有找到主 key，请检查");
            }

            // 如果已经有存储的，那么+1后返回，并再次存储
            {
                if (MonsterExportIDMap.TryGetValue(mapIDStr, out int _tempID))
                {
                    int _result = _tempID + 1;
                    MonsterExportIDMap[mapIDStr] = _result;

                    return _result;
                }
            }

            List<List<CellValueData>>? _allDataList = _exportSheet.GetAllDataList();

            if ((_allDataList == null) || (_allDataList.Count < 1))
            {
                throw new Exception("导出的总数据为空，请检查");
            }

            // 没有找到，那么就去遍历一下，获取最大的ID
            {
                foreach (List<CellValueData> _singleRow in _allDataList)
                {
                    // 这里去截取前5位，获得到ID
                    string _cellStr = _singleRow[_targetKey.KeyIndexInList].GetCellValue();

                    if (string.IsNullOrEmpty(_cellStr) || (_cellStr.Length < 5))
                    {
                        continue;
                    }

                    string _finalStr = _cellStr.Substring(0, 5);

                    if (!string.Equals(_finalStr, mapIDStr))
                    {
                        continue;
                    }

                    if (int.TryParse(_cellStr, out int _monsterID))
                    {
                        if (!MonsterExportIDMap.TryGetValue(_finalStr, out int _currentStoreID))
                        {
                            MonsterExportIDMap[_finalStr] = _monsterID;
                        }
                        else
                        {
                            if (_monsterID > _currentStoreID)
                            {
                                MonsterExportIDMap[_finalStr] = _monsterID;
                            }
                        }
                    }
                }

                // 从数据里面找到了
                if (MonsterExportIDMap.TryGetValue(mapIDStr, out int _tempID))
                {
                    int _result = _tempID + 1;
                    MonsterExportIDMap[mapIDStr] = _result;

                    return _result;
                }

                // 没有找到，那么组装一个新的放进去
                string _newValue = string.Format("{0}001", mapIDStr);
                int.TryParse(_newValue, out int _newIntValue);
                MonsterExportIDMap[mapIDStr] = _newIntValue;

                return _newIntValue;
            }

            throw new Exception($"{GetNextMonsterID} 未找到数据，请检查!");
        }
    }
}
