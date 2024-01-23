using ExcelTool;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConsole
{
    public class FateGuardPopGroupData
    {
        public int PopGroupID = 0;

        public List<int> FateNPCIDList = new List<int>();
    }


    /// <summary>
    /// 处理 Fate 相关的 FateNpc 转化后的 Monster 的等级
    /// </summary>
    public class ProcessForFateMonsterAndReference : ProcessBase
    {
        private ExcelFileData mFateExcelFile = null;
        private ExcelSheetData mFateGuardExcelSheet = null;
        private ExcelSheetData mFatePopGroupExcelSheet = null;

        private ExcelFileData mFateNpcExcelFile = null;
        private ExcelSheetData mFateNpcExcelSheet = null;

        private ExcelFileData mFateMonsterExcelFile = null;
        private ExcelSheetData mFateMonsterExcelSheet = null;

        private CSVFileData mFateGuardCSVFile = null;
        private CSVSheetData mFateGuardCSVSheet = null;
        private CSVFileData mFatePopGroupCSVFile = null;
        private CSVSheetData mFatePopGroupCSVSheet = null;

        public override bool Process()
        {
            // 加载 FATE表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mFateExcelFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
            }

            // 加载 FateGuard.csv
            {
                var _tempPath = Path.Combine(FolderPath, "FateGuard.csv");
                mFateGuardCSVFile = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mFateGuardCSVSheet = mFateGuardCSVFile.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mFateGuardCSVSheet == null)
                {
                    throw new Exception("无法获取 mFateGuardCSVFile.GetWorkSheetByIndex(0)");
                }
                mFateGuardCSVSheet.LoadAllCellData(true);
            }

            // 加载 G怪物表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                mFateMonsterExcelFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mFateMonsterExcelSheet = mFateMonsterExcelFile.GetWorkSheetByIndex(1) as ExcelSheetData;
                if (mFateMonsterExcelSheet == null)
                {
                    throw new Exception("获取数据失败， mFateMonsterExcelFile.GetWorkSheetByIndex(1)");
                }

                mFateMonsterExcelSheet.SetKeyStartRowIndex(1);
                mFateMonsterExcelSheet.SetKeyStartColmIndex(6);
                mFateMonsterExcelSheet.SetContentStartRowIndex(4);

                mFateMonsterExcelSheet.ReloadKey();
                mFateMonsterExcelSheet.LoadAllCellData(true);
            }

            // 加载 FatePopGroup.csv
            {
                var _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                mFatePopGroupCSVFile = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mFatePopGroupCSVSheet = mFatePopGroupCSVFile.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mFatePopGroupCSVSheet == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }
                mFatePopGroupCSVSheet.LoadAllCellData(true);
            }

            // 加载 FateNpc.xlsx 文件
            {
                var _tempPath = Path.Combine(FolderPath, "FateNpc.xlsx");
                mFateNpcExcelFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mFateNpcExcelSheet = mFateNpcExcelFile.GetWorkSheetByIndex(0) as ExcelSheetData;

                if (mFateNpcExcelSheet == null)
                {
                    throw new Exception($"无法获取 mFateNpcExcelFile.GetWorkSheetByIndex(0)");
                }
                mFateNpcExcelSheet.LoadAllCellData(true);
            }

            // 重新加载一下 key
            {
                mFatePopGroupExcelSheet = mFateExcelFile.GetWorkSheetByIndex(6) as ExcelSheetData;
                if (mFatePopGroupExcelSheet == null)
                {
                    throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                }

                {
                    var _keyListData = mFatePopGroupExcelSheet.GetKeyListData();
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
                    var _keyList = mFateGuardExcelSheet.GetKeyListData();
                    for (int i = 0; i < _keyList.Count; ++i)
                    {
                        _keyList[i].IsMainKey = false;
                    }
                    _keyList[0].IsMainKey = true;
                }
                mFateGuardExcelSheet.LoadAllCellData(true);
            }

            // Key : fateID ; Value : List<int> FateNpcID ，List 的 index 是对应fate创建物的monster index
            Dictionary<int, List<int>> _fateIDAndFateNPCIDDataMap = new Dictionary<int, List<int>>();

            // Key : FateID ; Value : List<FateGuardPopGroupData>
            Dictionary<int, List<FateGuardPopGroupData>> _fateGuardDataMap = new Dictionary<int, List<FateGuardPopGroupData>>();

            // 获取ID
            {
                var _guardIDIndex = CommonUtil.GetIndexByZM("C") - 1;
                var _fateIDIndex = CommonUtil.GetIndexByZM("A") - 1;
                var _fateExcelGuardSheetAllDataList = mFateGuardExcelSheet.GetAllDataList();
                var _fateGuardCSVKeyListData = mFateGuardCSVSheet.GetKeyListData();
                var _fatePopGroupCSVKeyListData = mFatePopGroupCSVSheet.GetKeyListData();
                foreach (var _singleRowDataForFateGuard in _fateExcelGuardSheetAllDataList)
                {
                    int.TryParse(_singleRowDataForFateGuard[_guardIDIndex].GetCellValue(), out var _guardID);

                    int.TryParse(_singleRowDataForFateGuard[_fateIDIndex].GetCellValue(), out var _fateID);

                    if (_fateID <= 0 || _guardID <= 0)
                    {
                        continue;
                    }

                    if (_fateIDAndFateNPCIDDataMap.ContainsKey(_fateID))
                    {
                        continue;
                    }

                    var _fateNPCIDList = new List<int>();
                    _fateIDAndFateNPCIDDataMap.Add(_fateID, _fateNPCIDList);

                    var _fateGuardData = new List<FateGuardPopGroupData>();
                    _fateGuardDataMap.Add(_fateID, _fateGuardData);

                    // 获取 FateGuard.csv 的目标数据
                    var _targetFateGuardRowData = mFateGuardCSVSheet.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 0 },
                        new List<string> { _guardID.ToString() }
                    );

                    if (_targetFateGuardRowData == null)
                    {
                        throw new Exception($"错误 mFateGuardCSVSheet.GetRowCellDataByTargetKeysAndValus 为空");
                    }

                    for (int i = 4; i < _fateGuardCSVKeyListData.Count; i += 3)
                    {
                        var _newPopGroupData = new FateGuardPopGroupData();
                        _fateGuardData.Add(_newPopGroupData);

                        if (!int.TryParse(_targetFateGuardRowData[i].GetCellValue(), out var _popGroupID) || _popGroupID <= 0)
                        {
                            continue;
                        }

                        var _tempRowData = mFatePopGroupCSVSheet.GetRowCellDataByTargetKeysAndValus(new List<int> { 0 }, new List<string> { _popGroupID.ToString() });
                        if (_tempRowData == null)
                        {
                            continue;
                        }

                        _newPopGroupData.PopGroupID = _popGroupID;

                        for (int _colIndex = 3; _colIndex < _fatePopGroupCSVKeyListData.Count; _colIndex += 2)
                        {
                            if (!int.TryParse(_tempRowData[_colIndex].GetCellValue(), out var _fateNPCID) || _fateNPCID <= 0)
                            {
                                continue;
                            }

                            if (!_fateNPCIDList.Contains(_fateNPCID))
                            {
                                _fateNPCIDList.Add(_fateNPCID);
                            }

                            if (!_newPopGroupData.FateNPCIDList.Contains(_fateNPCID))
                            {
                                _newPopGroupData.FateNPCIDList.Add(_fateNPCID);
                            }
                        }
                    }
                }
            }

            // 写入 Fate表的 Guard Sheet
            {
                // 这里去获取 index
                Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();
                foreach (var _pair in _fateGuardDataMap)
                {
                    var _targetRowCellData = mFateGuardExcelSheet.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { CommonUtil.GetIndexByZM("A") - 1 }, new List<string> { _pair.Key.ToString() }
                    );

                    if (_targetRowCellData == null || _targetRowCellData.Count < 1)
                    {
                        throw new Exception($"无法获取数据，ID是：{_pair.Key}");
                    }

                    var _targetRowStringData = CommonUtil.ParsRowCellDataToRowStringData(_targetRowCellData);

                    if (!_fateIDAndFateNPCIDDataMap.TryGetValue(_pair.Key, out var _fateNPCIDList))
                    {
                        throw new Exception($"错误，无法获取  Fate ID 对应的数据,FATEID是 : [{_pair.Key}]");
                    }

                    for (int i = 0; i < _pair.Value.Count; ++i)
                    {
                        var _index = 4 + i * 3;
                        List<int> _indexList = new List<int>();
                        foreach (var _fateNPCID in _pair.Value[i].FateNPCIDList)
                        {
                            var _targetIndex = _fateNPCIDList.IndexOf(_fateNPCID);
                            if (_targetIndex < 0)
                            {
                                throw new Exception("无法找到 FATE NPC ID ， ID是： " + _fateNPCID);
                            }
                            if (!_indexList.Contains(_targetIndex))
                            {
                                _indexList.Add(_targetIndex);
                            }
                        }

                        _targetRowStringData[_index] = CommonUtil.ConverListIntToString(_indexList, ",");
                    }

                    _writeDataMap.Add(_targetRowCellData[0].GetCellRowIndexInSheet(), _targetRowStringData);
                }

                // 这里写入
                foreach (var _pair in _writeDataMap)
                {
                    mFateGuardExcelSheet.WriteOneData(_pair.Key, _pair.Value, true);
                }
            }

            // 这里写入创建物
            {
                Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();

                foreach (var _pair in _fateIDAndFateNPCIDDataMap)
                {
                    var _fatePopGroupCellDataList = mFatePopGroupExcelSheet.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { CommonUtil.GetIndexByZM("A") - 1 },
                        new List<string> { _pair.Key.ToString() }
                    );

                    if (_fatePopGroupCellDataList == null || _fatePopGroupCellDataList.Count < 1)
                    {
                        throw new Exception($"获取数据失败，mFatePopGroupExcelSheet.GetRowCellDataByTargetKeysAndValus 请检查");
                    }

                    var _fatePopGroupStringDataList = CommonUtil.ParsRowCellDataToRowStringData(_fatePopGroupCellDataList);

                    for (int i = 0; i < _pair.Value.Count; ++i)
                    {
                        var _fateNPCID = _pair.Value[i];

                        var _fateNpcRowCellData = mFateNpcExcelSheet.GetRowCellDataByTargetKeysAndValus(
                             new List<int> { CommonUtil.GetIndexByZM("A") - 1 },
                             new List<string> { _fateNPCID.ToString() }
                        );

                        if (_fateNpcRowCellData == null)
                        {
                            throw new Exception($"无法获取 FateNPC 数据，ID是：{_fateNPCID}");
                        }
                    }

                    _writeDataMap.Add(_fatePopGroupCellDataList[0].GetCellRowIndexInSheet(), _fatePopGroupStringDataList);
                }

                foreach (var _pair in _writeDataMap)
                {
                    mFatePopGroupExcelSheet.WriteOneData(_pair.Key, _pair.Value, true);
                }
            }

            return true;
        }
    }
}
