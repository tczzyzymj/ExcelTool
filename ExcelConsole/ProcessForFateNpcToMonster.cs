using ExcelTool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ExcelConsole
{
    public class ProcessForFateNpcToMonster : ProcessBase
    {
        private ExcelFileData mExcelFateNpc = null;
        private ExcelSheetData mExcelSheetFateNpc = null;

        private ExcelFileData mExcelMonster = null;
        private ExcelSheetData mExcelSheetMonster = null;

        private ExcelFileData mExcelLevelReference = null;
        private ExcelSheetData mExcelSheetLevelReference = null;

        private ExcelFileData mExcelMap = null;
        private ExcelSheetData mExcelSheetMap = null;

        private ExcelFileData mExcelMapBNpcID = null;
        private ExcelSheetData mExcelSheetMapBNpcID = null;

        private ExcelFileData mExcelBNpcName = null;
        private ExcelSheetData mExcelSheetBNpcName = null;

        private ExcelFileData mExcelBNpcBase = null;
        private ExcelSheetData mExcelSheetBNpcBase = null;

        private ExcelFileData mExcelEObj = null;
        private ExcelSheetData mExcelSheetEObj = null;

        private class FateNpcToMonsterData
        {
            public int LevelReferenceID = 0;
            public int MapID = 0;
            public int BNpcID = 0;
            public string SEMapID = string.Empty;
            public string BigMapName = string.Empty;
            public string SmallMapName = string.Empty;
            public string MonsterIDKeyStr = string.Empty; // FM地图大区 + FM地图子区
            public string FateNpcName = string.Empty;
            public string CampStr = string.Empty; // 阵营字符串

            public int OverrideBNpcID = 0; // 继承的 BNpcID

            public bool IsEObj = false;
        }

        private void InternalLoadFile()
        {
            {
                string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
                var _tempPath = Path.Combine(currentPath, "../FileFolder");
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
                var _tempPath = Path.Combine(FolderPath, "EObj.xlsx");
                mExcelEObj = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetEObj = mExcelEObj.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetEObj == null)
                {
                    throw new Exception($"mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetEObj.SetKeyStartRowIndex(5);
                mExcelSheetEObj.SetKeyStartColmIndex(2);
                mExcelSheetEObj.SetContentStartRowIndex(10);

                mExcelSheetEObj.ReloadKey();
                var _allKeyDataList = mExcelSheetEObj.GetKeyListData();
                _allKeyDataList[0].IsMainKey = true;

                mExcelSheetEObj.LoadAllCellData(true);
            }

            // 加载 FateNpc
            {
                var _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                mExcelFateNpc = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetFateNpc = mExcelFateNpc.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetFateNpc == null)
                {
                    throw new Exception($"mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetFateNpc.SetKeyStartRowIndex(5);
                mExcelSheetFateNpc.SetKeyStartColmIndex(2);
                mExcelSheetFateNpc.SetContentStartRowIndex(11);

                mExcelSheetFateNpc.ReloadKey();
                var _allKeyDataList = mExcelSheetFateNpc.GetKeyListData();
                _allKeyDataList[0].IsMainKey = true;

                mExcelSheetFateNpc.LoadAllCellData(true);
            }

            // 加载 BNpcBase 表
            {
                var _tempPath = Path.Combine(FolderPath, "BNpcBase.xlsx");
                mExcelBNpcBase = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetBNpcBase = mExcelBNpcBase.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetBNpcBase == null)
                {
                    throw new Exception($"mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetBNpcBase.SetKeyStartRowIndex(5);
                mExcelSheetBNpcBase.SetKeyStartColmIndex(2);
                mExcelSheetBNpcBase.SetContentStartRowIndex(11);

                mExcelSheetBNpcBase.ReloadKey();
                var _allKeyDataList = mExcelSheetBNpcBase.GetKeyListData();
                _allKeyDataList[0].IsMainKey = true;

                mExcelSheetBNpcBase.LoadAllCellData(true);
            }

            // 加载 Monster
            {
                var _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                mExcelMonster = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetMonster = mExcelMonster.GetWorkSheetByIndex(1) as ExcelSheetData;
                if (mExcelSheetMonster == null)
                {
                    throw new Exception($"mExcelMonster.GetWorkSheetByIndex(1) 获取数据出错");
                }

                mExcelSheetMonster.SetKeyStartRowIndex(1);
                mExcelSheetMonster.ReloadKey();

                var _allKeyDataList = mExcelSheetMonster.GetKeyListData();
                _allKeyDataList[CommonUtil.GetIndexByZM("I") - 1].IsMainKey = true;

                mExcelSheetMonster.LoadAllCellData(true);
            }

            // 加载 LevelReference
            {
                var _tempPath = Path.Combine(FolderPath, "LevelReference.xlsx");
                mExcelLevelReference = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetLevelReference = mExcelLevelReference.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetLevelReference == null)
                {
                    throw new Exception($"mExcelLevelReference.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetLevelReference.SetKeyStartRowIndex(5);
                mExcelSheetLevelReference.SetKeyStartColmIndex(2);
                mExcelSheetLevelReference.SetContentStartRowIndex(10);

                mExcelSheetLevelReference.ReloadKey();
                mExcelSheetLevelReference.LoadAllCellData(true);
            }

            // 加载 Map
            {
                var _tempPath = Path.Combine(FolderPath, "Map.xlsx");
                mExcelMap = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetMap = mExcelMap.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetMap == null)
                {
                    throw new Exception($"mExcelMap.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetMap.SetKeyStartRowIndex(5);
                mExcelSheetMap.SetKeyStartColmIndex(2);
                mExcelSheetMap.SetContentStartRowIndex(10);

                mExcelSheetMap.ReloadKey();
                mExcelSheetMap.LoadAllCellData(true);
            }

            // 加载 MapBNpcID
            {
                var _tempPath = Path.Combine(FolderPath, "MapBnpcid.xlsx");
                mExcelMapBNpcID = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetMapBNpcID = mExcelMapBNpcID.GetWorkSheetByIndex(1) as ExcelSheetData;
                if (mExcelSheetMapBNpcID == null)
                {
                    throw new Exception($"mExcelFateNpc.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetMapBNpcID.SetKeyStartRowIndex(1);
                mExcelSheetMapBNpcID.SetKeyStartColmIndex(1);
                mExcelSheetMapBNpcID.SetContentStartRowIndex(2);

                mExcelSheetMapBNpcID.ReloadKey();
                mExcelSheetMapBNpcID.LoadAllCellData(true);
            }

            // 加载 BNpcName
            {
                var _tempPath = Path.Combine(FolderPath, "BNpcName.xlsx");
                mExcelBNpcName = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetBNpcName = mExcelBNpcName.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetBNpcName == null)
                {
                    throw new Exception($"mExcelBNpcName.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetBNpcName.SetKeyStartRowIndex(5);
                mExcelSheetBNpcName.SetKeyStartColmIndex(2);
                mExcelSheetBNpcName.SetContentStartRowIndex(11);

                mExcelSheetBNpcName.ReloadKey();
                mExcelSheetBNpcName.LoadAllCellData(true);
            }
        }

        private static int mIndexOfMonsterCampID = CommonUtil.GetIndexByZM("AS") - 1;

        private static int mIndexOffset = 2;

        private static int mIndexOfFateNpcID = CommonUtil.GetIndexByZM("B") - mIndexOffset; // -2 是因为 KEY 是从 B 列开始的
        private static int mIndexOfFateNpcBaseID = CommonUtil.GetIndexByZM("J") - mIndexOffset;
        private static int mIndexOfFateNpcLayoutID = CommonUtil.GetIndexByZM("K") - mIndexOffset;
        private static int mIndexOfFateNpcPopRange = CommonUtil.GetIndexByZM("AA") - mIndexOffset;
        private static int mIndexOfFateNpcBaseNpc = CommonUtil.GetIndexByZM("F") - mIndexOffset;
        private static int mIndexOfFatenpcKeyDevCode = CommonUtil.GetIndexByZM("D") - mIndexOffset;

        private static int mIndexOfLevelRefKeyDev = CommonUtil.GetIndexByZM("D") - mIndexOffset;
        private static int mIndexOfLevelRefKeyAlias = CommonUtil.GetIndexByZM("C") - mIndexOffset;
        private static int mIndexOfLevelRefKey = CommonUtil.GetIndexByZM("B") - mIndexOffset;
        private static int mIndexOfLevelRefIsEObj = CommonUtil.GetIndexByZM("M") - mIndexOffset;
        private static int mIndexOfLevelRefBaseID = CommonUtil.GetIndexByZM("N") - mIndexOffset;
        private static int mIndexOfLevelRefMapID = CommonUtil.GetIndexByZM("O") - mIndexOffset;

        private static int mIndexOfMapPath = CommonUtil.GetIndexByZM("L") - mIndexOffset;

        private static int _fateNpcIndexOfName = CommonUtil.GetIndexByZM("I") - mIndexOffset;
        private static int _fateNpcTextIndex = CommonUtil.GetIndexByZM("L") - mIndexOffset;
        private static int _eobjNameIndex = CommonUtil.GetIndexByZM("AD") - mIndexOffset;

        private static int mIndexOfFateNpcEntrType = CommonUtil.GetIndexByZM("H") - mIndexOffset;

        // G怪物表
        private static int mIndexOfMonsterFateNpcID = CommonUtil.GetIndexByZM("I") - 1;

        public override bool Process()
        {
            InternalLoadFile();

            var _allDataList = mExcelSheetFateNpc.GetAllDataList();

            if (_allDataList == null || _allDataList.Count == 0)
            {
                throw new Exception($"mExcelSheetFateNpc.GetAllDataList() 出错，数量无效");
            }

            Dictionary<int, FateNpcToMonsterData> _fateNpcDataMap = new Dictionary<int, FateNpcToMonsterData>();

            foreach (var _singleFateNpcDataList in _allDataList)
            {
                if (!int.TryParse(_singleFateNpcDataList[mIndexOfFateNpcID].GetCellValue(), out var _fateNpcID) || _fateNpcID <= 0)
                {
                    continue;
                }

                // 先找一下LayoutID
                var _levelRefStr = InternalGetLevelReferenceID(_singleFateNpcDataList);
                if (string.IsNullOrEmpty(_levelRefStr))
                {
                    // 这里去看一下，是否有继承的NPC
                    var _tempBaseNpcStr = _singleFateNpcDataList[mIndexOfFateNpcBaseNpc].GetCellValue();
                    if (string.IsNullOrEmpty(_tempBaseNpcStr))
                    {
                        CommonUtil.ShowError($"FateNpc : [{_fateNpcID}] 无法找到出生点, LevelReference 为空，且没有继承 FateNpc , 请检查");
                        continue;
                    }

                    var _targetFateNpcRowDataList = mExcelSheetFateNpc.GetRowCellDataByTargetKeysAndValus(
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

                var _targetLevelRefRowDataList = mExcelSheetLevelReference.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { mIndexOfLevelRefKeyDev },
                    new List<string> { _levelRefStr }
                );

                if (_targetLevelRefRowDataList == null || _targetLevelRefRowDataList.Count < 1)
                {
                    _targetLevelRefRowDataList = mExcelSheetLevelReference.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { mIndexOfLevelRefKeyAlias },
                        new List<string> { _levelRefStr }
                    );

                    if (_targetLevelRefRowDataList == null || _targetLevelRefRowDataList.Count < 1)
                    {
                        _targetLevelRefRowDataList = mExcelSheetLevelReference.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { mIndexOfLevelRefKeyAlias },
                            new List<string> { _levelRefStr }
                        );

                        throw new Exception($"FateNpc : [{_fateNpcID}] 找不到 LevelRefStr 数据, Mapping 数据是：[{_levelRefStr}]请检查");
                    }
                }

                // 获取 level reference id 
                var _levelRefIDStr = _targetLevelRefRowDataList[mIndexOfLevelRefKey].GetCellValue();
                if (!int.TryParse(_levelRefIDStr, out var _levelRefID) || _levelRefID <= 0)
                {
                    throw new Exception($"无法获取 LevelReferenceID , keyDev 是：[{_levelRefStr}]");
                }

                var _newFateNpcToMonsterData = new FateNpcToMonsterData();
                _fateNpcDataMap.Add(_fateNpcID, _newFateNpcToMonsterData);

                // 写入阵营相关  敵

                // 看是不是 eobj
                var _typeNameStr = _targetLevelRefRowDataList[mIndexOfLevelRefIsEObj].GetCellValue();
                if (!string.IsNullOrEmpty(_typeNameStr) && _typeNameStr.ToLower().Contains("eventobj"))
                {
                    _newFateNpcToMonsterData.IsEObj = true;
                }

                _newFateNpcToMonsterData.LevelReferenceID = _levelRefID;

                var _tempTypeStr = _singleFateNpcDataList[mIndexOfFateNpcEntrType].GetCellValue();
                if (!string.IsNullOrEmpty(_tempTypeStr) && _tempTypeStr.ToLower().Contains("eobj"))
                {
                    _newFateNpcToMonsterData.IsEObj = true;
                }

                var _overrideFateNpcStr = _singleFateNpcDataList[4].GetCellValue();
                if (!string.IsNullOrEmpty(_overrideFateNpcStr))
                {
                    var _tempOverrideFateNpcDataList = mExcelSheetFateNpc.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 2 },
                        new List<string> { _overrideFateNpcStr }
                    );

                    if (_tempOverrideFateNpcDataList == null || _tempOverrideFateNpcDataList.Count < 1)
                    {
                        throw new Exception($"无法获取 FateNpc 数据，KeyDev 是 : [{_overrideFateNpcStr}]");
                    }

                    int.TryParse(_tempOverrideFateNpcDataList[0].GetCellValue(), out _newFateNpcToMonsterData.OverrideBNpcID);
                }

                // 这里写入一下 BaseID
                {
                    // 先在 FateNpc 表里面看看 J 列的BaseID 是否有
                    var _tempBaseIDStr = _singleFateNpcDataList[mIndexOfFateNpcBaseID].GetCellValue();
                    if (!string.IsNullOrEmpty(_tempBaseIDStr))
                    {
                        if (_newFateNpcToMonsterData.IsEObj)
                        {
                            var _targetEObjDataList = mExcelSheetEObj.GetRowCellDataByTargetKeysAndValus(
                                new List<int> { 4 },
                                new List<string> { _tempBaseIDStr }
                            );
                            if (_targetEObjDataList == null || _targetEObjDataList.Count < 1)
                            {
                                throw new Exception($"无法获取 EObj 数据，IDMappingStr 是 : [{_tempBaseIDStr}] , FateNpc : [{_fateNpcID}]");
                            }

                            int.TryParse(_targetEObjDataList[0].GetCellValue(), out _newFateNpcToMonsterData.BNpcID);
                        }
                        else
                        {
                            var _tempBNpcBaseDataList = mExcelSheetBNpcBase.GetRowCellDataByTargetKeysAndValus(
                                new List<int> { 3 },
                                new List<string> { _tempBaseIDStr }
                            );

                            if (_tempBNpcBaseDataList == null || _tempBNpcBaseDataList.Count < 1)
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
                            if (int.TryParse(_targetLevelRefRowDataList[mIndexOfLevelRefBaseID].GetCellValue(), out var _baseID) && _baseID > 0)
                            {
                                _newFateNpcToMonsterData.BNpcID = _baseID;
                            }
                        }
                    }
                }

                // 获取 map id
                var _mapIDStr = _targetLevelRefRowDataList[mIndexOfLevelRefMapID].GetCellValue();
                if (!int.TryParse(_mapIDStr, out var _mapID) || _mapID <= 0)
                {
                    Console.WriteLine($"FateNpc: [{_fateNpcID}] -> LevelReference : [{_levelRefID}] , 没有地图ID");
                    _fateNpcDataMap.Remove(_fateNpcID);
                    continue;
                }

                _newFateNpcToMonsterData.MapID = _mapID;

                // 去 Map.xlsx 查找一下
                var _targetMapRowDataList = mExcelSheetMap.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 0 },
                    new List<string> { _mapIDStr }
                );

                if (_targetMapRowDataList == null)
                {
                    throw new Exception($"无法找到 Map 数据，ID是：[{_mapIDStr}]");
                }

                var _pathStr = _targetMapRowDataList[mIndexOfMapPath].GetCellValue();
                if (string.IsNullOrEmpty(_pathStr))
                {
                    throw new Exception($"Map 数据出错 , Path 数据为空");
                }

                // 去 MapBNpcID 表 查找一下
                var _targetMapBNpcIDRowDataList = mExcelSheetMapBNpcID.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 0 },
                    new List<string> { _pathStr }
                );

                if (_targetMapBNpcIDRowDataList == null || _targetMapBNpcIDRowDataList.Count < 1)
                {
                    throw new Exception(
                        $"mExcelSheetMapBNpcID.GetRowCellDataByTargetKeysAndValus 错误，SE地图ID : {_pathStr}"
                    );
                }
                _newFateNpcToMonsterData.SEMapID = _pathStr;
                _newFateNpcToMonsterData.BigMapName = _targetMapBNpcIDRowDataList[1].GetCellValue();
                _newFateNpcToMonsterData.SmallMapName = _targetMapBNpcIDRowDataList[3].GetCellValue();
                _newFateNpcToMonsterData.MonsterIDKeyStr = _targetMapBNpcIDRowDataList[2].GetCellValue() +
                    _targetMapBNpcIDRowDataList[4].GetCellValue();
            }

            // 这里再去获取一下 FateNpcName :  Name , FateNpcBaseID
            foreach (var _pair in _fateNpcDataMap)
            {
                InternalLoadFateNpcBaseIDAndName(_pair.Key, _pair.Value, _fateNpcDataMap);
            }

            Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();

            int _newIndex = -1;

            var _templateRowData = mExcelSheetMonster.GetAllDataList()[0];

            List<string> _templateDataList = new List<string>();

            for (int i = 0; i < _templateRowData.Count; ++i)
            {
                _templateDataList.Add(string.Empty);
            }

            var _profileIndex = CommonUtil.GetIndexByZM("N") - 1;
            var _nameIndex = CommonUtil.GetIndexByZM("G") - 1;
            var _synacTarget = CommonUtil.GetIndexByZM("AT") - 1;
            var _keepBodyIndex = CommonUtil.GetIndexByZM("AX") - 1;
            var _rotateSpeedIndex = CommonUtil.GetIndexByZM("BA") - 1;
            var _minRoateIndex = CommonUtil.GetIndexByZM("BB") - 1;

            var _visionRadius = CommonUtil.GetIndexByZM("BF") - 1;

            var _ownerShipIndex = CommonUtil.GetIndexByZM("BU") - 1;

            var _distributeIndex = CommonUtil.GetIndexByZM("BX") - 1;

            // 准备写入信息
            foreach (var _pair in _fateNpcDataMap)
            {
                int _targetIndex = 0;
                List<string> _finalStrList = null;
                var _targetRowDataList = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());
                if (_targetRowDataList == null || _targetRowDataList.Count < 1)
                {
                    // 新数据
                    _targetIndex = _newIndex;
                    --_newIndex;
                    _finalStrList = new List<string>();
                    _finalStrList.AddRange(_templateDataList);

                    var _newMonsterID = GetNextMonsterID(_pair.Value.MonsterIDKeyStr, 5);
                    _finalStrList[5] = _newMonsterID.ToString();

                    Console.WriteLine($"新写入数据 -> FateNpc : [{_pair.Key}] , MonsterID : [{_newMonsterID}]");
                }
                else
                {
                    // 旧数据
                    _targetIndex = _targetRowDataList[0].GetCellRowIndexInSheet();
                    _finalStrList = CommonUtil.ParsRowCellDataToRowStringData(_targetRowDataList);
                }

                if (_finalStrList == null)
                {
                    throw new Exception($"错误，没有数据，请检查！");
                }

                _finalStrList[0] = _pair.Value.SmallMapName + "FATE";
                _finalStrList[2] = _pair.Value.MapID.ToString();
                _finalStrList[3] = _pair.Value.BNpcID.ToString();
                _finalStrList[_nameIndex] = _pair.Value.FateNpcName;
                _finalStrList[8] = _pair.Key.ToString();
                _finalStrList[_profileIndex] = _pair.Value.BNpcID.ToString();
                _finalStrList[_synacTarget] = "1";
                _finalStrList[_keepBodyIndex] = "4000";
                _finalStrList[_rotateSpeedIndex] = "180";
                _finalStrList[_minRoateIndex] = "15";

                _finalStrList[_visionRadius] = "300";
                _finalStrList[_visionRadius + 1] = "200";
                _finalStrList[_visionRadius + 2] = "400";
                _finalStrList[_visionRadius + 3] = "5000";
                _finalStrList[_visionRadius + 4] = "5";

                _finalStrList[_ownerShipIndex] = "伤害占比";
                _finalStrList[_ownerShipIndex + 1] = "5000";

                _finalStrList[_distributeIndex] = "随机掉落";

                _finalStrList[mIndexOfMonsterCampID] = _pair.Value.CampStr;

                _writeDataMap.Add(_targetIndex, _finalStrList);
            }

            foreach (var _pair in _writeDataMap)
            {
                mExcelSheetMonster.WriteOneData(_pair.Key, _pair.Value, true);
            }

            mExcelMonster.SaveFile();

            return true;
        }

        // 写入名字
        private void InternalLoadFateNpcBaseIDAndName(int fateNpcKey, FateNpcToMonsterData targetData, Dictionary<int, FateNpcToMonsterData> totalData)
        {
            var _cachedFateNpcData = mExcelSheetFateNpc.GetCacheRowDataListByKeyStr(fateNpcKey.ToString());
            if (_cachedFateNpcData == null || _cachedFateNpcData.Count < 1)
            {
                throw new Exception($"无法获取缓存的数据，FateNpc : [{fateNpcKey}]");
            }

            // 如果有继承的表，那么去获取一下继承的数据
            if (targetData.OverrideBNpcID > 0)
            {
                // 获取一下父类
                var _overrideFateData = totalData[targetData.OverrideBNpcID];
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
                targetData.BNpcID = _overrideFateData.BNpcID;

                return;
            }

            if (targetData.IsEObj)
            {
                var _targetEObjDataList = mExcelSheetEObj.GetCacheRowDataListByKeyStr(targetData.BNpcID.ToString());
                if (_targetEObjDataList == null || _targetEObjDataList.Count < 1)
                {
                    throw new Exception($"无法获取  Eobj 数据，ID是：[{targetData.BNpcID}] , FateNpc : [{fateNpcKey}]");
                }

                targetData.FateNpcName = _targetEObjDataList[_eobjNameIndex].GetCellValue();

                return;
            }

            var _bNPCNameMappingStr = _cachedFateNpcData[_fateNpcIndexOfName].GetCellValue();
            if (!string.IsNullOrEmpty(_bNPCNameMappingStr))
            {
                // 这里看下 BaseID 是否已经有了，如果有了， 那么去 BaseID 看看
                var _targetRowDataList = mExcelSheetBNpcName.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 4 },
                    new List<string> { _bNPCNameMappingStr }
                );

                if (_targetRowDataList == null || _targetRowDataList.Count < 1)
                {
                    throw new Exception($"mExcelSheetBNpcName.GetRowCellDataByTargetKeysAndValus 错误，无法匹配，Str = [{_bNPCNameMappingStr}]");
                }

                targetData.FateNpcName = _targetRowDataList[_fateNpcTextIndex].GetCellValue();
                return;
            }

            // 获取 BNpcID
            if (targetData.BNpcID <= 0)
            {
                CommonUtil.ShowError($"数据出错，没有 BNpcID ，FateNpc ID  是 ： [{fateNpcKey}]");
                return;
            }

            var _cacheBNpcDataList = mExcelSheetBNpcBase.GetCacheRowDataListByKeyStr(targetData.BNpcID.ToString());
            if (_cacheBNpcDataList == null || _cacheBNpcDataList.Count < 1)
            {
                throw new Exception($"无法获取 BNpcBase 数据，ID是  : [{targetData.BNpcID}] , FateNpc : [{fateNpcKey}]");
            }

            var _tempBNpcNameStr = _cacheBNpcDataList[4].GetCellValue();

            if (!string.IsNullOrEmpty(_tempBNpcNameStr))
            {
                var _targetRowDataList = mExcelSheetBNpcName.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 4 },
                    new List<string> { _tempBNpcNameStr }
                );

                if (_targetRowDataList == null || _targetRowDataList.Count < 1)
                {
                    throw new Exception(
                        $"mExcelSheetBNpcName.GetRowCellDataByTargetKeysAndValus 错误，BaseID : [{targetData.BNpcID}] , FateNpc : [{fateNpcKey}] , 匹配Str = [{_tempBNpcNameStr}]"
                    );
                }

                targetData.FateNpcName = _targetRowDataList[_fateNpcTextIndex].GetCellValue();
            }
        }

        private string InternalGetLevelReferenceID(List<CellValueData> singleRowData)
        {
            // layoutID
            var _tempStr = singleRowData[mIndexOfFateNpcLayoutID].GetCellValue();
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
            var _exportSheet = mExcelSheetMonster;
            if (_exportSheet == null)
            {
                throw new Exception($"{GetNextMonsterID} 出错，无法获取 mExcelSheetMonster");
            }

            if (!int.TryParse(mapIDStr, out var _tempCheckValue))
            {
                throw new Exception($"{GetNextMonsterID} 出错，传入的值 : {mapIDStr} ，无法解析为 int，请检查!");
            }

            _exportSheet.LoadAllCellData(false);

            var _keyList = _exportSheet.GetKeyListData();
            KeyData _targetKey = _keyList[monsterIDIndex];

            if (_targetKey == null)
            {
                throw new Exception($"没有找到主 key，请检查");
            }

            // 如果已经有存储的，那么+1后返回，并再次存储
            {
                if (MonsterExportIDMap.TryGetValue(mapIDStr, out var _tempID))
                {
                    var _result = _tempID + 1;
                    MonsterExportIDMap[mapIDStr] = _result;
                    return _result;
                }
            }

            var _allDataList = _exportSheet.GetAllDataList();
            if (_allDataList == null || _allDataList.Count < 1)
            {
                throw new Exception($"导出的总数据为空，请检查");
            }

            // 没有找到，那么就去遍历一下，获取最大的ID
            {
                foreach (var _singleRow in _allDataList)
                {
                    // 这里去截取前5位，获得到ID
                    var _cellStr = _singleRow[_targetKey.KeyIndexInList].GetCellValue();
                    if (string.IsNullOrEmpty(_cellStr) || _cellStr.Length < 5)
                    {
                        continue;
                    }

                    var _finalStr = _cellStr.Substring(0, 5);
                    if (!string.Equals(_finalStr, mapIDStr))
                    {
                        continue;
                    }

                    if (int.TryParse(_cellStr, out var _monsterID))
                    {
                        if (!MonsterExportIDMap.TryGetValue(_finalStr, out var _currentStoreID))
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
                if (MonsterExportIDMap.TryGetValue(mapIDStr, out var _tempID))
                {
                    var _result = _tempID + 1;
                    MonsterExportIDMap[mapIDStr] = _result;
                    return _result;
                }
                else
                {
                    // 没有找到，那么组装一个新的放进去
                    var _newValue = string.Format("{0}001", mapIDStr);
                    int.TryParse(_newValue, out var _newIntValue);
                    MonsterExportIDMap[mapIDStr] = _newIntValue;

                    return _newIntValue;
                }
            }

            throw new Exception($"{GetNextMonsterID} 未找到数据，请检查!");
        }
    }
}
