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
        private ExcelFileData mMonsterFile = null;
        private ExcelFileData mFateNpcFile = null;
        public override bool Process()
        {
            // monster 表 数据加载
            var _exportFilePath = Path.Combine(FolderPath, "G怪物表.xlsx");
            ExcelFileData _monsterFile = new ExcelFileData(_exportFilePath, LoadFileType.ExportFile);
            mMonsterFile = _monsterFile;
            var _monsterSheet = _monsterFile.GetWorkSheetByIndex(1);
            if (_monsterSheet == null)
            {
                CommonUtil.ShowError("无法获取 sheet 1", true);
                return false;
            }

            _monsterSheet.SetKeyStartColmIndex(1);
            _monsterSheet.SetKeyStartRowIndex(1);
            _monsterSheet.SetContentStartRowIndex(4);
            _monsterSheet.ReloadKey();
            _monsterSheet.LoadAllCellData(true);
            // end

            // fate csv 表 数据加载
            var _fateCSVPath = Path.Combine(FolderPath, "Fate.csv");
            CSVFileData _fateCSVData = new CSVFileData(_fateCSVPath, LoadFileType.NormalFile);
            var _fateCSVSheet = _fateCSVData.GetWorkSheetByIndex(0);

            if (_fateCSVSheet == null)
            {
                CommonUtil.ShowError("无法获取 sheet 3", true);
                return false;
            }
            _fateCSVSheet.LoadAllCellData(true);
            // end

            // fate npc 表 数据加载
            var _fateNPCExcelPath = Path.Combine(FolderPath, "FateNpc.xlsx");
            ExcelFileData _fateNPCExcelFileData = new ExcelFileData(_fateNPCExcelPath, LoadFileType.NormalFile);
            mFateNpcFile = _fateNPCExcelFileData;
            var _fateNPCSheet = _fateNPCExcelFileData.GetWorkSheetByIndex(0);
            if (_fateNPCSheet == null)
            {
                CommonUtil.ShowError("无法获取 sheet 2", true);
                return false;
            }
            _fateNPCSheet.SetKeyStartRowIndex(1);
            _fateNPCSheet.SetKeyStartColmIndex(1);
            _fateNPCSheet.SetContentStartRowIndex(4);
            _fateNPCSheet.ReloadKey();
            _fateNPCSheet.LoadAllCellData(true);
            // end

            // fate guard csv 表数据加载
            var _fateGuardCSVPath = Path.Combine(FolderPath, "FateGuard.csv");
            CSVFileData _fateGuardCSV = new CSVFileData(_fateGuardCSVPath, LoadFileType.NormalFile);
            var _fateGuardCSVSheet = _fateCSVData.GetWorkSheetByIndex(0);

            if (_fateGuardCSVSheet == null)
            {
                CommonUtil.ShowError("无法获取 sheet 3", true);
                return false;
            }
            _fateGuardCSVSheet.LoadAllCellData(true);
            //end

            // fatepopgroup csv表 数据加载
            var _fatepopGroupCSVPath = Path.Combine(FolderPath, "FatePopGroup.csv");
            CSVFileData _fatepopGroupCSV = new CSVFileData(_fatepopGroupCSVPath, LoadFileType.NormalFile);
            var _fatepopGroupCSVSheet = _fatepopGroupCSV.GetWorkSheetByIndex(0);

            if (_fatepopGroupCSVSheet == null)
            {
                CommonUtil.ShowError("无法获取 sheet 3", true);
                return false;
            }
            _fatepopGroupCSVSheet.LoadAllCellData(true);
            // end

            var _allDataForFate = _fateCSVSheet.GetAllDataList();
            if (_allDataForFate == null || _allDataForFate.Count < 1)
            {
                throw new Exception("_allDataForFata 为空");
            }

            var _allDataForFatePopGroup = _fatepopGroupCSVSheet.GetAllDataList();
            if (_allDataForFatePopGroup == null || _allDataForFatePopGroup.Count < 1)
            {
                throw new Exception($"_allDataForFatePopGroup 为空");
            }

            var _fateGuardIDIndex = CommonUtil.GetIndexByZM("HD") - 1;
            var _starterGroupIndex = CommonUtil.GetIndexByZM("FD") - 1;
            var _fateEnemyLevelIndex = CommonUtil.GetIndexByZM("GX") - 1;
            var _fateAllyLevelIndex = CommonUtil.GetIndexByZM("GY") - 1;
            var _fateStartNPCIndex = CommonUtil.GetIndexByZM("BT") - 1; // 开始的NPC 相关
            var _fateCSVKeyIndex = CommonUtil.GetIndexByZM("A") - 1;

            Dictionary<int, FateLevelData> _fateLevelDataMap = new Dictionary<int, FateLevelData>(); // key : FateID , value : FateLevelData
            Dictionary<int, FateNPCData> _fateNpcDataMap = new Dictionary<int, FateNPCData>(); // key: FateNPCID , value : FateNPCData

            List<List<CellValueData>> _fateCSVContainsGuardDataList = new List<List<CellValueData>>();
            for (int i = 0; i < _allDataForFate.Count; ++i)
            {
                // 找 fateguard 相关的
                {
                    var _fateGuardIDStr = _allDataForFate[i][_fateGuardIDIndex].GetCellValue();
                    if (!int.TryParse(_fateGuardIDStr, out var _fateGuardID))
                    {
                        continue;
                    }

                    if (_fateGuardID > 0)
                    {
                        _fateCSVContainsGuardDataList.Add(_allDataForFate[i]);
                    }
                }
            }

            var _fatepopGroupSheetKeyList = _fatepopGroupCSVSheet.GetKeyListData();
            if (_fatepopGroupSheetKeyList == null)
            {
                throw new Exception("_fatepopGroupSheetKeyList 为空");
            }

            foreach (var _dataList in _fateCSVContainsGuardDataList)
            {
                if (!int.TryParse(_dataList[_fateCSVKeyIndex].GetCellValue(), out var _fateID))
                {
                    throw new Exception("无法解析为 fateid");
                }

                var _fateLevelData = new FateLevelData();
                _fateLevelDataMap[_fateID] = _fateLevelData;

                var _allyLevel = 1;
                int.TryParse(_dataList[_fateAllyLevelIndex].GetCellValue(), out _allyLevel);
                _fateLevelData.AllyLevel = _allyLevel;

                var _enemyLevel = 1;
                int.TryParse(_dataList[_fateEnemyLevelIndex].GetCellValue(), out _enemyLevel);
                _fateLevelData.EnemyLevel = _enemyLevel;
            }

            foreach (var _dataList in _fateCSVContainsGuardDataList)
            {
                if (!int.TryParse(_dataList[_fateCSVKeyIndex].GetCellValue(), out var _fateID))
                {
                    throw new Exception("无法解析为 fateid");
                }

                // 这里先检查一下 _fateStartNPCIndex 是否有直接的NPCID
                {
                    var _beginNPCStr = _dataList[_fateStartNPCIndex].GetCellValue();
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
                        var _fatePopGroupRowData = _fatepopGroupCSVSheet.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { 0 },
                            new List<string> { _dataList[_starterGroupIndex].GetCellValue() }
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
                    var _fateGuardRowData = _fateGuardCSVSheet.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 0 },
                        new List<string> { _dataList[_fateGuardIDIndex].GetCellValue() }
                    );

                    var _fateGuardKeyList = _fateGuardCSVSheet.GetKeyListData();
                    var _fatepopgroupKeyList = _fatepopGroupCSVSheet.GetKeyListData();
                    if (_fateGuardRowData != null && _fateGuardRowData.Count > 0)
                    {
                        for (int i = 4; i < _fateGuardKeyList.Count; i += 3)
                        {
                            if (!int.TryParse(_fateGuardRowData[i].GetCellValue(), out var _fatepopGroupID) || _fatepopGroupID <= 0)
                            {
                                continue;
                            }

                            // 通过key 去 fatepopgroup csv 表里面找一下
                            var _fatePopGroupRowData = _fatepopGroupCSVSheet.GetRowCellDataByTargetKeysAndValus(
                                new List<int> { 0 },
                                new List<string> { _fateGuardRowData[i].GetCellValue() }
                            );

                            if (_fatePopGroupRowData == null || _fatePopGroupRowData.Count < 1)
                            {
                                continue;
                            }

                            for (int j = 3; j < _fatepopgroupKeyList.Count; j += 2)
                            {
                                if (int.TryParse(_fateGuardRowData[j].GetCellValue(), out var _npcID) && _npcID > 0)
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

            var _monsterAllDataList = _monsterSheet.GetAllDataList();
            if (_monsterAllDataList == null || _monsterAllDataList.Count < 1)
            {
                throw new Exception("_monsterAllDataList 为空，请检查");
            }

            var _monsterKeyDataList = _monsterSheet.GetKeyListData();
            var _monsterLevelIndex = CommonUtil.GetIndexByZM("CA") - 1;
            var _monsterFateNPCIDIndex = CommonUtil.GetIndexByZM("I") - 1;
            Dictionary<int, List<string>> _newDataMap = new Dictionary<int, List<string>>();
            foreach (var _pair in _fateNpcDataMap)
            {
                var _rowData = _monsterSheet.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { _monsterFateNPCIDIndex },
                    new List<string> { _pair.Key.ToString() }
                );

                if (_rowData == null || _rowData.Count < 1)
                {
                    continue;
                }

                var _listStr = CommonUtil.ParsRowCellDataToRowStringData(_rowData);
                _newDataMap.Add(_rowData[0].GetCellRowIndexInSheet(), _listStr);

                if (!_fateLevelDataMap.TryGetValue(_pair.Value.FateID, out var _targetFateData))
                {
                    throw new Exception("无法获取FateID, ID是：" + _pair.Value.FateID);
                }

                if (_pair.Value.IsAlly)
                {
                    _listStr[_monsterLevelIndex] = _targetFateData.AllyLevel.ToString();
                }
                else
                {
                    _listStr[_monsterLevelIndex] = _targetFateData.EnemyLevel.ToString();
                }
            }

            foreach (var _pair in _newDataMap)
            {
                _monsterSheet.WriteOneData(_pair.Key, _pair.Value, true);
            }

            _monsterFile.SaveFile();

            return true;
        }
    }
}
