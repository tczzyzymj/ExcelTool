﻿using ExcelTool;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExcelConsole
{
    public class FatePopGroupNPCData
    {
        public int FateNpcID = 0;
        public int MaxNum = 0;
        public int MonsterID = 0;
    }

    /// <summary>
    /// 处理 Fate 相关的 FateNpc 转化后的 Monster 的等级
    /// </summary>
    public class ProcessForFateMonsterAndReference : ProcessBase
    {
        private ExcelFileData? mExcelFate = null;
        private ExcelSheetData? mExcelSheetFateGuardInFateExcel = null;
        private ExcelSheetData? mExcelSheetPopGroupInFateExcel = null;

        private ExcelFileData? mExcelFateNpc = null;
        private ExcelSheetData? mExcelSheetFateNpc = null;

        private ExcelFileData? mExcelMonster = null;
        private ExcelSheetData? mExcelSheetMonster = null;

        private CSVFileData? mCSVFateGuard = null;
        private CSVSheetData? mCSVSheetFateGuard = null;

        private CSVFileData? mCSVFatePopGroup = null;
        private CSVSheetData? mCSVSheetFatePopGroup = null;

        private ExcelFileData? mExcelLevelReference = null;
        private ExcelSheetData? mExcelSheetLevelReference = null;

        private static int mMonsterIDIndex = CommonUtil.GetIndexByZM("F") - 1;
        private static int mMonsterFateNpcIDIndex = CommonUtil.GetIndexByZM("I") - 1;

        private static int mFateNpcIDIndex = CommonUtil.GetIndexByZM("A") - 1;
        private static int mFateNpcBaseIDIndex = CommonUtil.GetIndexByZM("R") - 1;
        private static int mFateNpcLayoutIDIndex = CommonUtil.GetIndexByZM("D") - 1;
        private static int _fateNpcIdleRangeIndex = CommonUtil.GetIndexByZM("N") - 1;
        private static int _fateNpcDepopRangeIndex = CommonUtil.GetIndexByZM("O") - 1;

        private static int _fatePopGroupMonsterIDIndex = CommonUtil.GetIndexByZM("E") - 1; // MonsterID index
        private static int _fatePopGroupBaseIDIndex = _fatePopGroupMonsterIDIndex + 1; // BaseID index
        private static int _fatePopGroupMinNumIndex = _fatePopGroupBaseIDIndex + 1; // 最少数量 index
        private static int _fatePopGroupMaxNumIndex = _fatePopGroupMinNumIndex + 1; // 最少数量 index
        private static int _fatePopGroupPopRangeIndex = _fatePopGroupMaxNumIndex + 1;// 出现范围 index
        private static int _fatePopGroupIdleRangeIndex = _fatePopGroupPopRangeIndex + 1; // 移动目的地 index
        private static int _fatePopGroupDepopRangeIndex = _fatePopGroupIdleRangeIndex + 1; // 逃离目的地


        private string InternalTryParseLevelReferenceData(string levelReferenceStr)
        {
            int.TryParse(levelReferenceStr, out var _layouerID);
            if (_layouerID <= 0)
            {
                return string.Empty;
            }

            if (mExcelSheetLevelReference == null)
            {
                return string.Empty;
            }

            return CommonUtil.GetPosInfoByLevelReferenceID(
                _layouerID,
                mExcelSheetLevelReference,
                true,
                false,
                false
            );
        }

        private void InternalLoadFiles()
        {
            // 加载 FATE表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                // 重新加载一下 key
                {
                    mExcelSheetPopGroupInFateExcel = mExcelFate.GetWorkSheetByIndex(6) as ExcelSheetData;
                    if (mExcelSheetPopGroupInFateExcel == null)
                    {
                        throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                    }

                    {
                        var _keyListData = mExcelSheetPopGroupInFateExcel.GetKeyListData();
                        for (int i = 0; i < _keyListData.Count; ++i)
                        {
                            _keyListData[i].IsMainKey = false;
                        }

                        _keyListData[0].IsMainKey = true;
                    }
                    mExcelSheetPopGroupInFateExcel.LoadAllCellData(true);

                    mExcelSheetFateGuardInFateExcel = mExcelFate.GetWorkSheetByIndex(13) as ExcelSheetData;
                    if (mExcelSheetFateGuardInFateExcel == null)
                    {
                        throw new Exception("mFateFile.GetWorkSheetByIndex(13)");
                    }

                    {
                        var _keyList = mExcelSheetFateGuardInFateExcel.GetKeyListData();
                        for (int i = 0; i < _keyList.Count; ++i)
                        {
                            _keyList[i].IsMainKey = false;
                        }
                        _keyList[0].IsMainKey = true;
                    }
                    mExcelSheetFateGuardInFateExcel.LoadAllCellData(true);
                }
            }

            // 加载 FateGuard.csv
            {
                var _tempPath = Path.Combine(FolderPath, "FateGuard.csv");
                mCSVFateGuard = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCSVSheetFateGuard = mCSVFateGuard.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCSVSheetFateGuard == null)
                {
                    throw new Exception("无法获取 mFateGuardCSVFile.GetWorkSheetByIndex(0)");
                }
                mCSVSheetFateGuard.LoadAllCellData(true);
            }

            // 加载 LevelReference.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "LevelReference.xlsx");
                mExcelLevelReference = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetLevelReference = mExcelLevelReference.GetWorkSheetByIndex(0) as ExcelSheetData;
                if (mExcelSheetLevelReference == null)
                {
                    throw new Exception($"mExcelLevelReference.GetWorkSheetByIndex(0) 获取数据出错");
                }

                mExcelSheetLevelReference.SetKeyStartRowIndexInSheet(5);
                mExcelSheetLevelReference.SetKeyStartColmIndexInSheet(2);
                mExcelSheetLevelReference.SetContentStartRowIndexInSheet(10);

                mExcelSheetLevelReference.ReloadKey();
                mExcelSheetLevelReference.LoadAllCellData(true);
            }

            // 加载 G怪物表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                mExcelMonster = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetMonster = mExcelMonster.GetWorkSheetByIndex(1) as ExcelSheetData;
                if (mExcelSheetMonster == null)
                {
                    throw new Exception("获取数据失败， mFateMonsterExcelFile.GetWorkSheetByIndex(1)");
                }

                mExcelSheetMonster.SetKeyStartRowIndexInSheet(1);
                mExcelSheetMonster.SetKeyStartColmIndexInSheet(1);
                mExcelSheetMonster.SetContentStartRowIndexInSheet(4);

                mExcelSheetMonster.ReloadKey();
                mExcelSheetMonster.LoadAllCellData(true);
            }

            // 加载 FatePopGroup.csv
            {
                var _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                mCSVFatePopGroup = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCSVSheetFatePopGroup = mCSVFatePopGroup.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCSVSheetFatePopGroup == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }
                mCSVSheetFatePopGroup.LoadAllCellData(true);
            }

            // 加载 FateNpc.xlsx 文件
            {
                var _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                mExcelFateNpc = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mExcelSheetFateNpc = mExcelFateNpc.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mExcelSheetFateNpc == null)
                {
                    throw new Exception($"无法获取 mFateNpcExcelFile.GetWorkSheetByIndex(0)");
                }
                mExcelSheetFateNpc.SetKeyStartRowIndexInSheet(1);
                mExcelSheetFateNpc.SetKeyStartColmIndexInSheet(1);
                mExcelSheetFateNpc.SetContentStartRowIndexInSheet(4);
                mExcelSheetFateNpc.ReloadKey();
                mExcelSheetFateNpc.LoadAllCellData(true);
            }
        }

        public override bool Process()
        {
            InternalLoadFiles();

            // Key : fateID ; Value : List<FateGuardPopGroupData> FateNpcID ，List 的 index 是对应fate创建物的monster index
            Dictionary<int, List<FatePopGroupNPCData>> _fateMonsterToNpcDataMap = new Dictionary<int, List<FatePopGroupNPCData>>();

            // Key : FateID ; Value : List<List<int>> , 这里会去获取index
            Dictionary<int, List<List<int>>> _fateGuardDataMap = new Dictionary<int, List<List<int>>>();

            // 获取ID
            {
                var _guardIDIndex = CommonUtil.GetIndexByZM("C") - 1;
                var _fateIDIndex = CommonUtil.GetIndexByZM("A") - 1;
                var _fateExcelGuardSheetAllDataList = mExcelSheetFateGuardInFateExcel?.GetAllDataList();
                if (_fateExcelGuardSheetAllDataList == null)
                {
                    throw new Exception($"mFateGuardExcelSheet.GetAllDataList() 错误，无法获取数据");
                }
                var _fateGuardCSVKeyListData = mCSVSheetFateGuard?.GetKeyListData();
                if (_fateGuardCSVKeyListData == null)
                {
                    throw new Exception("mCSVSheetFateGuard?.GetKeyListData(); 为空，请检查!");
                }
                var _fatePopGroupCSVKeyListData = mCSVSheetFatePopGroup?.GetKeyListData();
                if (_fatePopGroupCSVKeyListData == null)
                {
                    throw new Exception("mCSVSheetFatePopGroup?.GetKeyListData(); 为空，请检查!");
                }
                foreach (var _singleRowDataForFateGuard in _fateExcelGuardSheetAllDataList)
                {
                    int.TryParse(_singleRowDataForFateGuard[_guardIDIndex].GetCellValue(), out var _guardID);

                    int.TryParse(_singleRowDataForFateGuard[_fateIDIndex].GetCellValue(), out var _fateID);

                    if (_fateID <= 0 || _guardID <= 0)
                    {
                        continue;
                    }

                    if (_fateID != 434)
                    {
                        continue;
                    }

                    if (_fateMonsterToNpcDataMap.ContainsKey(_fateID))
                    {
                        continue;
                    }

                    var _newFateNpcDataList = new List<FatePopGroupNPCData>();
                    _fateMonsterToNpcDataMap.Add(_fateID, _newFateNpcDataList);

                    var _fateGuardNPCDataList = new List<List<int>>();
                    _fateGuardDataMap.Add(_fateID, _fateGuardNPCDataList);

                    // 获取 FateGuard.csv 的目标数据
                    var _targetFateGuardRowData = mCSVSheetFateGuard?.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 0 },
                        new List<string> { _guardID.ToString() }
                    );

                    if (_targetFateGuardRowData == null)
                    {
                        throw new Exception($"错误 mFateGuardCSVSheet.GetRowCellDataByTargetKeysAndValus 为空 , _guardID 为 : [{_guardID}]");
                    }

                    for (int i = 4; i < _fateGuardCSVKeyListData.Count; i += 3)
                    {
                        var _newListInt = new List<int>();
                        _fateGuardNPCDataList.Add(_newListInt);

                        if (!int.TryParse(_targetFateGuardRowData[i].GetCellValue(), out var _popGroupID) || _popGroupID <= 0)
                        {
                            continue;
                        }

                        var _tempRowData = mCSVSheetFatePopGroup?.GetRowCellDataByTargetKeysAndValus(new List<int> { 0 }, new List<string> { _popGroupID.ToString() });
                        if (_tempRowData == null)
                        {
                            continue;
                        }

                        for (int _colIndex = 3; _colIndex < _fatePopGroupCSVKeyListData.Count; _colIndex += 2)
                        {
                            if (!int.TryParse(_tempRowData[_colIndex].GetCellValue(), out var _fateNPCID) || _fateNPCID <= 0)
                            {
                                continue;
                            }

                            int.TryParse(_tempRowData[_colIndex + 1].GetCellValue(), out var _numLimit);

                            if (!_newListInt.Contains(_fateNPCID))
                            {
                                _newListInt.Add(_fateNPCID);
                            }

                            bool _hasRecord = false;
                            foreach (var _pair in _newFateNpcDataList)
                            {
                                if (_pair.FateNpcID == _fateNPCID)
                                {
                                    _hasRecord = true;
                                    break;
                                }
                            }

                            if (!_hasRecord)
                            {
                                // 加入
                                var _tempNewData = new FatePopGroupNPCData();
                                _tempNewData.FateNpcID = _fateNPCID;
                                _tempNewData.MaxNum = _numLimit;
                                _newFateNpcDataList.Add(_tempNewData);
                            }
                        }
                    }
                }
            }

            //// 写入 Fate表的 Guard Sheet
            //{
            //    // 这里去获取 index
            //    Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();
            //    foreach (var _pair in _fateGuardDataMap)
            //    {
            //        var _targetRowCellData = mFateGuardExcelSheet.GetRowCellDataByTargetKeysAndValus(
            //            new List<int> { CommonUtil.GetIndexByZM("A") - 1 }, new List<string> { _pair.Key.ToString() }
            //        );

            //        if (_targetRowCellData == null || _targetRowCellData.Count < 1)
            //        {
            //            throw new Exception($"无法获取 Fate 数据，ID是：{_pair.Key}");
            //        }

            //        var _targetRowStringData = CommonUtil.ParsRowCellDataToRowStringData(_targetRowCellData);

            //        if (!_fateMonsterToNpcDataMap.TryGetValue(_pair.Key, out var _fateNPCIDList))
            //        {
            //            throw new Exception($"错误，无法获取  Fate ID 对应的数据,FATEID是 : [{_pair.Key}]");
            //        }

            //        for (int i = 0; i < _pair.Value.Count; ++i)
            //        {
            //            var _index = 4 + i * 4;
            //            if (_index >= _targetRowStringData.Count)
            //            {
            //                continue;
            //            }

            //            List<int> _indexList = new List<int>();
            //            foreach (var _tempNPCID in _pair.Value[i])
            //            {
            //                var _targetIndex = _fateNPCIDList.FindIndex(x => x.FateNpcID == _tempNPCID);
            //                if (_targetIndex < 0)
            //                {
            //                    throw new Exception("无法找到 FATE NPC ID ， ID是： " + _tempNPCID);
            //                }
            //                if (!_indexList.Contains(_targetIndex))
            //                {
            //                    _indexList.Add(_targetIndex);
            //                }
            //            }
            //            var _tempStr = CommonUtil.ConverListIntToString(_indexList, ",");
            //            if (string.IsNullOrEmpty(_tempStr))
            //            {
            //                _tempStr = "空";
            //            }
            //            _targetRowStringData[_index] = _tempStr;
            //        }

            //        _writeDataMap.Add(_targetRowCellData[0].GetCellRowIndexInSheet(), _targetRowStringData);
            //    }

            //    // 这里写入
            //    foreach (var _pair in _writeDataMap)
            //    {
            //        mFateGuardExcelSheet.WriteOneData(_pair.Key, _pair.Value, true);
            //    }
            //}

            // 这里写入创建物
            {
                Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();

                foreach (var _pair in _fateMonsterToNpcDataMap)
                {
                    var _fatePopGroupCellDataList = mExcelSheetPopGroupInFateExcel?.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { CommonUtil.GetIndexByZM("A") - 1 },
                        new List<string> { _pair.Key.ToString() }
                    );

                    if (_pair.Value.Count > 16)
                    {
                        Console.WriteLine($"FateNPC 的数量超过了16个，FATE ID是：{_pair.Key} ，请检查");
                        continue;
                    }

                    if (_fatePopGroupCellDataList == null || _fatePopGroupCellDataList.Count < 1)
                    {
                        throw new Exception($"获取数据失败，mFatePopGroupExcelSheet.GetRowCellDataByTargetKeysAndValus 请检查");
                    }

                    var _fatePopGroupStringDataList = CommonUtil.ParsRowCellDataToRowStringData(_fatePopGroupCellDataList);

                    for (int i = 0; i < _pair.Value.Count; ++i)
                    {
                        var _fateNPCID = _pair.Value[i].FateNpcID;

                        var _fateNpcRowCellData = mExcelSheetFateNpc?.GetRowCellDataByTargetKeysAndValus(
                             new List<int> { mFateNpcIDIndex },
                             new List<string> { _fateNPCID.ToString() }
                        );

                        if (_fateNpcRowCellData == null)
                        {
                            throw new Exception($"无法获取 FateNPC 数据，ID是：{_fateNPCID}");
                        }

                        // 通过 FateNpcID 查找 Monter 表
                        var _monsterData = mExcelSheetMonster?.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { mMonsterFateNpcIDIndex },
                            new List<string> { _fateNPCID.ToString() }
                        );

                        if (_monsterData == null)
                        {
                            throw new Exception($"无法通过 FateNpcID 查找到 Monster 数据，FateNpcID 是 : [{_fateNPCID}]");
                        }

                        var _monsterIDStr = _monsterData[mMonsterIDIndex].GetCellValue();

                        _fatePopGroupStringDataList[_fatePopGroupMonsterIDIndex + i * 7] = _monsterIDStr;
                        _fatePopGroupStringDataList[_fatePopGroupBaseIDIndex + i * 7] = _fateNpcRowCellData[mFateNpcBaseIDIndex].GetCellValue();
                        if (_pair.Value[i].MaxNum < 1)
                        {
                            _fatePopGroupStringDataList[_fatePopGroupMinNumIndex + i * 7] = "0";
                        }
                        else
                        {
                            _fatePopGroupStringDataList[_fatePopGroupMinNumIndex + i * 7] = "1";
                        }

                        _fatePopGroupStringDataList[_fatePopGroupMaxNumIndex + i * 7] = _pair.Value[i].MaxNum.ToString();

                        // 获取 PopRange
                        InternalTryProcessForPosInfo(_fateNpcRowCellData, _fatePopGroupStringDataList, i);

                        // 获取 IdleRange
                        {
                            int.TryParse(_fateNpcRowCellData[_fateNpcIdleRangeIndex].GetCellValue(), out var _idleRangeID);
                            if (_idleRangeID > 0)
                            {
                                _fatePopGroupStringDataList[_fatePopGroupIdleRangeIndex + i * 7] = CommonUtil.GetPosInfoByLevelReferenceID(
                                    _idleRangeID,
                                    mExcelSheetLevelReference,
                                    true,
                                    false,
                                    true
                                );
                            }
                            else
                            {
                                _fatePopGroupStringDataList[_fatePopGroupIdleRangeIndex + i * 7] = string.Empty;
                            }
                        }

                        // 获取逃跑
                        {
                            int.TryParse(_fateNpcRowCellData[_fateNpcDepopRangeIndex].GetCellValue(), out var _depopRangeID);
                            if (_depopRangeID > 0)
                            {
                                _fatePopGroupStringDataList[_fatePopGroupDepopRangeIndex + i * 7] = CommonUtil.GetPosInfoByLevelReferenceID(
                                    _depopRangeID,
                                    mExcelSheetLevelReference,
                                    true,
                                    false,
                                    true
                                );
                            }
                            else
                            {
                                _fatePopGroupStringDataList[_fatePopGroupDepopRangeIndex + i * 7] = string.Empty;
                            }
                        }
                    }

                    _writeDataMap.Add(_fatePopGroupCellDataList[0].GetCellRowIndexInSheet(), _fatePopGroupStringDataList);
                }

                foreach (var _pair in _writeDataMap)
                {
                    mExcelSheetPopGroupInFateExcel?.WriteOneData(_pair.Key, _pair.Value, true);
                }
            }

            mExcelFate?.SaveFile();

            return true;
        }

        private void InternalTryProcessForPosInfo(List<CellValueData> _fateNpcRowCellData, List<string> _fatePopGroupStringDataList, int index)
        {
            for (int i = 0; i < 10; ++i)
            {
                int.TryParse(_fateNpcRowCellData[mFateNpcLayoutIDIndex + i].GetCellValue(), out var _targetID);
                if (_targetID > 0)
                {
                    _fatePopGroupStringDataList[_fatePopGroupPopRangeIndex + index * 7] = CommonUtil.GetPosInfoByLevelReferenceID(
                        _targetID,
                        mExcelSheetLevelReference,
                        true,
                        false,
                        true
                    );

                    return;
                }
            }

            _fatePopGroupStringDataList[_fatePopGroupPopRangeIndex + index * 7] = string.Empty;
        }
    }
}
