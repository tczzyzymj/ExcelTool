using ExcelTool;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConsole
{
    public class FateLevelData
    {
        public int AllyLevel = 1;
        public int EnemyLevel = 1;
    }

    public class FateNPCData
    {
        public int FateID;
        public bool IsAlly = false;
    }

    /// <summary>
    /// 处理 Fate 相关的 FateNpc 转化后的 Monster 的等级
    /// </summary>
    public class ProcessForFateGuardNpcToMonsterLevel : ProcessBase
    {
        private ExcelFileData? mExceLMonster = null;
        private ExcelSheetData? mExcelSheetMonster = null;

        private ExcelFileData? mExcelFateNPC = null;
        private ExcelSheetData? mExcelSheetFateNPC = null;

        private CSVFileData? mCSVFate = null;
        private CSVSheetData? mCSVSheetFate = null;

        private CSVFileData? mCVSFateGuard = null;
        private CSVSheetData? mCSVSheetFateGuard = null;

        private CSVFileData? mCSVFatePopGroup = null;
        private CSVSheetData? mCSVSheetFatePopGroup = null;

        private static int mFateGuardIDIndex = CommonUtil.GetIndexByZM("HD") - 1;
        private static int mStarterGroupIndex = CommonUtil.GetIndexByZM("FD") - 1;
        private static int mFateEnemyLevelIndex = CommonUtil.GetIndexByZM("GX") - 1;
        private static int mFateAllyLevelIndex = CommonUtil.GetIndexByZM("GY") - 1;
        private static int mFateStartNPCIndex = CommonUtil.GetIndexByZM("BT") - 1; // 开始的NPC 相关
        private static int mFateCSVKeyIndex = CommonUtil.GetIndexByZM("A") - 1;
        private static int mMonsterLevelIndex = CommonUtil.GetIndexByZM("CA") - 1;
        private static int mMonsterFateNPCIDIndex = CommonUtil.GetIndexByZM("I") - 1;
        private static int mMonsterLevelFirstIndex = CommonUtil.GetIndexByZM("E") - 1;

        private bool InternalLoadFiles()
        {
            // monster 表 数据加载
            {
                var _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                mExceLMonster = new ExcelFileData(_tempPath, LoadFileType.ExportFile);
                mExcelSheetMonster = mExceLMonster?.GetWorkSheetByIndex(1) as ExcelSheetData;
                if (mExcelSheetMonster == null)
                {
                    CommonUtil.ShowError("无法获取 sheet 1", true);
                    return false;
                }

                mExcelSheetMonster.SetKeyStartColmIndexInSheet(1);
                mExcelSheetMonster.SetKeyStartRowIndexInSheet(1);
                mExcelSheetMonster.SetContentStartRowIndexInSheet(4);
                mExcelSheetMonster.ReloadKey();
                mExcelSheetMonster.LoadAllCellData(true);
            }
            // end

            // fate csv 表 数据加载
            {
                var _tempPath = Path.Combine(FolderPath, "Fate.csv");
                mCSVFate = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCSVSheetFate = mCSVFate.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCSVSheetFate == null)
                {
                    CommonUtil.ShowError("无法获取 sheet 3", true);
                    return false;
                }
                mCSVSheetFate.LoadAllCellData(true);
            }
            // end

            // fate npc 表 数据加载
            {
                var _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                mExcelFateNPC = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetFateNPC = mExcelFateNPC.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetFateNPC == null)
                {
                    CommonUtil.ShowError("无法获取 sheet 2", true);
                    return false;
                }

                mExcelSheetFateNPC.SetKeyStartRowIndexInSheet(1);
                mExcelSheetFateNPC.SetKeyStartColmIndexInSheet(1);
                mExcelSheetFateNPC.SetContentStartRowIndexInSheet(4);
                mExcelSheetFateNPC.ReloadKey();
                mExcelSheetFateNPC.LoadAllCellData(true);
            }
            // end

            // fate guard csv 表数据加载
            {
                var _tempPath = Path.Combine(FolderPath, "FateGuard.csv");
                mCVSFateGuard = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCSVSheetFateGuard = mCVSFateGuard.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCSVSheetFateGuard == null)
                {
                    CommonUtil.ShowError("无法获取 sheet 3", true);
                    return false;
                }
                mCSVSheetFateGuard.LoadAllCellData(true);
            }
            //end

            // fatepopgroup csv表 数据加载
            {
                var _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                mCSVFatePopGroup = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCSVSheetFatePopGroup = mCSVFatePopGroup.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCSVSheetFatePopGroup == null)
                {
                    CommonUtil.ShowError("无法获取 sheet 3", true);
                    return false;
                }
                mCSVSheetFatePopGroup.LoadAllCellData(true);
            }
            // end

            return true;
        }

        public override bool Process()
        {
            if (!InternalLoadFiles())
            {
                return false;
            }

            var _allDataForFate = mCSVSheetFate?.GetAllDataList();
            if (_allDataForFate == null || _allDataForFate.Count < 1)
            {
                throw new Exception("_allDataForFata 为空");
            }

            var _allDataForFatePopGroup = mCSVSheetFatePopGroup?.GetAllDataList();
            if (_allDataForFatePopGroup == null || _allDataForFatePopGroup.Count < 1)
            {
                throw new Exception($"_allDataForFatePopGroup 为空");
            }

            Dictionary<int, FateLevelData> _fateLevelDataMap = new Dictionary<int, FateLevelData>(); // key : FateID , value : FateLevelData
            Dictionary<int, FateNPCData> _fateNpcDataMap = new Dictionary<int, FateNPCData>(); // key: FateNPCID , value : FateNPCData

            List<List<CellValueData>> _fateCSVContainsGuardDataList = new List<List<CellValueData>>();
            for (int i = 0; i < _allDataForFate.Count; ++i)
            {
                // 找 fateguard 相关的
                {
                    var _fateGuardIDStr = _allDataForFate[i][mFateGuardIDIndex].GetCellValue();
                    if (!int.TryParse(_fateGuardIDStr, out var _fateGuardID))
                    {
                        continue;
                    }

                    if (_fateGuardID != 3)
                    {
                        continue;
                    }

                    if (_fateGuardID > 0)
                    {
                        _fateCSVContainsGuardDataList.Add(_allDataForFate[i]);
                    }
                }
            }

            foreach (var _dataList in _fateCSVContainsGuardDataList)
            {
                if (!int.TryParse(_dataList[mFateCSVKeyIndex].GetCellValue(), out var _fateID))
                {
                    throw new Exception("无法解析为 fateid");
                }

                var _fateLevelData = new FateLevelData();
                _fateLevelDataMap[_fateID] = _fateLevelData;

                var _allyLevel = 1;
                int.TryParse(_dataList[mFateAllyLevelIndex].GetCellValue(), out _allyLevel);
                _fateLevelData.AllyLevel = _allyLevel;

                var _enemyLevel = 1;
                int.TryParse(_dataList[mFateEnemyLevelIndex].GetCellValue(), out _enemyLevel);
                _fateLevelData.EnemyLevel = _enemyLevel;
            }

            foreach (var _dataList in _fateCSVContainsGuardDataList)
            {
                if (!int.TryParse(_dataList[mFateCSVKeyIndex].GetCellValue(), out var _fateID))
                {
                    throw new Exception("无法解析为 fateid");
                }

                // 这里先检查一下 _fateStartNPCIndex 是否有直接的NPCID
                {
                    var _beginNPCStr = _dataList[mFateStartNPCIndex].GetCellValue();
                    if (int.TryParse(_beginNPCStr, out var _beginNPCID) && _beginNPCID > 0)
                    {
                        if (!_fateNpcDataMap.TryGetValue(_beginNPCID, out var _npcData))
                        {
                            _npcData = new FateNPCData();
                            _fateNpcDataMap.Add(_beginNPCID, _npcData);
                        }

                        _npcData.FateID = _fateID;
                        _npcData.IsAlly = true;
                    }
                    else
                    {
                        // 通过key 去 fatepopgroup csv 表里面找一下
                        var _fatePopGroupRowData = mCSVSheetFatePopGroup?.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { 0 },
                            new List<string> { _dataList[mStarterGroupIndex].GetCellValue() }
                        );

                        if (_fatePopGroupRowData != null && _fatePopGroupRowData.Count > 0)
                        {
                            // 因为是开始的NPC，一定是友方
                            var _starterNPCIndex = CommonUtil.GetIndexByZM("D") - 1;
                            if (int.TryParse(_fatePopGroupRowData[_starterNPCIndex].GetCellValue(), out var _allyNPCID))
                            {
                                if (!_fateNpcDataMap.TryGetValue(_allyNPCID, out var _npcData))
                                {
                                    _npcData = new FateNPCData();
                                    _fateNpcDataMap.Add(_allyNPCID, _npcData);
                                }
                                _npcData.FateID = _fateID;
                                _npcData.IsAlly = true;
                            }
                        }
                    }
                }

                // 这里去找一下 fateguard，应该都是enemy，但还是要看下 jlabl，看是否有包含 enemy或者 敌
                {
                    var _fateGuardRowData = mCSVSheetFateGuard?.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 0 },
                        new List<string> { _dataList[mFateGuardIDIndex].GetCellValue() }
                    );

                    var _fateGuardKeyList = mCSVSheetFateGuard?.GetKeyListData();
                    var _fatepopgroupKeyList = mCSVSheetFatePopGroup?.GetKeyListData();
                    if (_fateGuardKeyList != null && _fatepopgroupKeyList != null && _fateGuardRowData != null && _fateGuardRowData.Count > 0)
                    {
                        for (int i = 4; i < _fateGuardKeyList.Count; i += 3)
                        {
                            if (!int.TryParse(_fateGuardRowData[i].GetCellValue(), out var _fatepopGroupID) || _fatepopGroupID <= 0)
                            {
                                continue;
                            }

                            // 通过key 去 fatepopgroup csv 表里面找一下
                            var _fatePopGroupRowData = mCSVSheetFatePopGroup?.GetRowCellDataByTargetKeysAndValus(
                                new List<int> { 0 },
                                new List<string> { _fatepopGroupID.ToString() }
                            );

                            if (_fatePopGroupRowData == null || _fatePopGroupRowData.Count < 1)
                            {
                                continue;
                            }

                            for (int j = 3; j < _fatepopgroupKeyList.Count; j += 2)
                            {
                                if (int.TryParse(_fatePopGroupRowData[j].GetCellValue(), out var _npcID) && _npcID > 0)
                                {
                                    if (!_fateNpcDataMap.TryGetValue(_npcID, out var _data))
                                    {
                                        _data = new FateNPCData();
                                        _fateNpcDataMap.Add(_npcID, _data);
                                    }

                                    _data.FateID = _fateID;
                                    _data.IsAlly = false;
                                }
                            }
                        }
                    }
                }
            }

            var _monsterAllDataList = mExcelSheetMonster?.GetAllDataList();
            if (_monsterAllDataList == null || _monsterAllDataList.Count < 1)
            {
                throw new Exception("_monsterAllDataList 为空，请检查");
            }

            Dictionary<int, List<string>> _newDataMapToWrite = new Dictionary<int, List<string>>();
            foreach (var _pair in _fateNpcDataMap)
            {
                var _rowData = mExcelSheetMonster?.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { mMonsterFateNPCIDIndex },
                    new List<string> { _pair.Key.ToString() }
                );

                if (_rowData == null || _rowData.Count < 1)
                {
                    continue;
                }

                var _listStr = CommonUtil.ParsRowCellDataToRowStringData(_rowData);
                _newDataMapToWrite.Add(_rowData[0].GetCellRowIndexInSheet(), _listStr);

                if (!_fateLevelDataMap.TryGetValue(_pair.Value.FateID, out var _targetFateData))
                {
                    throw new Exception("无法获取FateID, ID是：" + _pair.Value.FateID);
                }

                if (_pair.Value.IsAlly)
                {
                    _listStr[mMonsterLevelIndex] = _targetFateData.AllyLevel.ToString();
                    _listStr[mMonsterLevelFirstIndex] = _targetFateData.AllyLevel.ToString();
                }
                else
                {
                    _listStr[mMonsterLevelIndex] = _targetFateData.EnemyLevel.ToString();
                    _listStr[mMonsterLevelFirstIndex] = _targetFateData.EnemyLevel.ToString();
                }
            }

            foreach (var _pair in _newDataMapToWrite)
            {
                mExcelSheetMonster?.WriteOneData(_pair.Key, _pair.Value, true);
            }

            mExceLMonster?.SaveFile();

            return true;
        }
    }
}
