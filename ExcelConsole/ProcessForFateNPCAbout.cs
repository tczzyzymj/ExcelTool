using ExcelTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConsole
{
    public class ProcessForFateNPCAbout : ProcessBase
    {
        private CSVFileData? mFateCSVFile = null;
        private CSVSheetData? mFateCSVSheet = null;

        private ExcelFileData? mFateExcelFile = null;
        private ExcelSheetData? mFatePopGroupExcelSheet = null;
        private ExcelSheetData? mFateGuardExcelSheet = null;

        private ExcelFileData? mFateMonsterExcelFile = null;
        private ExcelSheetData? mFateMonsterExcelSheet = null;

        private ExcelFileData? mFateNpcExcelFile = null;
        private ExcelSheetData? mFateNpcExcelSheet = null;

        private ExcelFileData? mExcelLevelReference = null;
        private ExcelSheetData? mExcelSheetLevelReference = null;

        private CSVFileData? mFatePopGroupCSVFile = null;
        private CSVSheetData? mFatePopGroupCSVSheet = null;

        private static int mNpcResID = CommonUtil.GetIndexByZM("DM") - 1;

        private static int mFatePopGroupPopRangeIndex = CommonUtil.GetIndexByZM("DP") - 1;
        private static int mFatePopGroupIdleRangeIndex = CommonUtil.GetIndexByZM("DQ") - 1;
        private static int mFatePopGroupDepopRangeIndex = CommonUtil.GetIndexByZM("DR") - 1;

        private void InternalLoadFile()
        {
            // 加载 FATE表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mFateExcelFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

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

            // 加载 LevelReference.csv
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
                mFateMonsterExcelFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
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
                var _tempPath = Path.Combine(FolderPath, "Fate.csv");
                mFateCSVFile = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mFateCSVSheet = mFateCSVFile.GetWorkSheetByIndex(0) as CSVSheetData;
                if (mFateCSVSheet == null)
                {
                    throw new Exception($" mFateCSVFile.GetWorkSheetByIndex(0) 获取数据错误");
                }

                mFateCSVSheet.LoadAllCellData(true);
            }
        }

        public override bool Process()
        {
            InternalLoadFile();

            var _allFateCSVDataList = mFateGuardExcelSheet?.GetAllDataList();
            if (_allFateCSVDataList == null || _allFateCSVDataList.Count < 1)
            {
                throw new Exception($"mFateCSVSheet.GetAllDataList() 错误，请检查");
            }

            List<int> _fateIDList = new List<int>();
            foreach (var _singeRowData in _allFateCSVDataList)
            {
                if (int.TryParse(_singeRowData[0].GetCellValue(), out var _fateID) && _fateID > 0)
                {
                    _fateIDList.Add(_fateID);
                }
            }

            var _starterGroupIndex = CommonUtil.GetIndexByZM("FD") - 1;
            var _guardParamsIndex = CommonUtil.GetIndexByZM("HD") - 1;

            var _fateNpcIndex = CommonUtil.GetIndexByZM("D") - 1;

            // key : fate id ; value : FatePopGroupNPCData
            Dictionary<int, FatePopGroupNPCData> _fateNpcDataMap = new Dictionary<int, FatePopGroupNPCData>();

            foreach (var _fateID in _fateIDList)
            {
                var _targetFateRowDataList = mFateCSVSheet?.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 0 },
                    new List<string> { _fateID.ToString() }
                );

                if (_targetFateRowDataList == null || _targetFateRowDataList.Count < 1)
                {
                    throw new Exception($"无法获取 Fate 数据，FateID 是 : [{_fateID}]");
                }

                if (!int.TryParse(_targetFateRowDataList[_guardParamsIndex].GetCellValue(), out var _guardParamsID) ||
                    _guardParamsID <= 0)
                {
                    continue;
                }

                if (!int.TryParse(_targetFateRowDataList[_starterGroupIndex].GetCellValue(), out var _starterGroupID) ||
                    _starterGroupID <= 0)
                {
                    continue;
                }

                var _fatePopGroupRowDataList = mFatePopGroupCSVSheet?.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 0 },
                    new List<string> { _starterGroupID.ToString() }
                );

                if (_fatePopGroupRowDataList == null || _fatePopGroupRowDataList.Count < 1)
                {
                    throw new Exception($"无法获取 FatePopGroup 数据，ID是：[{_starterGroupID}]");
                }

                if (!int.TryParse(_fatePopGroupRowDataList[_fateNpcIndex].GetCellValue(), out var _fateNpcID) ||
                    _fateNpcID <= 0)
                {
                    throw new Exception($"FatePopGroup : [{_starterGroupID}] , 的第一个友方 FateNpc 为空，请检查");
                }

                int.TryParse(_fatePopGroupRowDataList[_fateNpcIndex + 1].GetCellValue(), out var _maxCount);
                if (_maxCount < 1)
                {
                    _maxCount = 1;
                }
                var _fateNpcData = new FatePopGroupNPCData();
                _fateNpcData.FateNpcID = _fateNpcID;
                _fateNpcData.MaxNum = _maxCount;
                _fateNpcDataMap.Add(_fateID, _fateNpcData);
            }

            foreach (var _pair in _fateNpcDataMap)
            {
                var _targetRowDataList = mFateMonsterExcelSheet?.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 8 },
                    new List<string> { _pair.Value.FateNpcID.ToString() }
                );

                if (_targetRowDataList == null || _targetRowDataList.Count < 1)
                {
                    throw new Exception($"无法从 Monster 获取数据，FateNpc ID 是 : [{_pair.Value.FateNpcID}]");
                }

                if (!int.TryParse(_targetRowDataList[5].GetCellValue(), out var _monsterID))
                {
                    throw new Exception($"从 Monster 表获取数据错误，其MonsterID无法转化为int，FateNpcID  : [{_pair.Value.FateNpcID}]");
                }

                _pair.Value.MonsterID = _monsterID;
            }

            var _npcResIDIndex = CommonUtil.GetIndexByZM("DM") - 1;
            var _fateNpcLayoutIDIndex = CommonUtil.GetIndexByZM("D") - 1;

            var _idleRangeIndex = CommonUtil.GetIndexByZM("N") - 1;
            var _depopRangeIndex = CommonUtil.GetIndexByZM("O") - 1;

            Dictionary<int, List<string>> _writeDataMap = new Dictionary<int, List<string>>();

            foreach (var _pair in _fateNpcDataMap)
            {
                var _fatePopGroupRowDataList = mFatePopGroupExcelSheet?.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 0 },
                    new List<string> { _pair.Key.ToString() }
                );

                if (_fatePopGroupRowDataList == null || _fatePopGroupRowDataList.Count < 1)
                {
                    throw new Exception($"无法获取 Fate 的 创建物数据，Fate ID : [{_pair.Key}]");
                }

                var _fatePopGroupStringDataList = CommonUtil.ParsRowCellDataToRowStringData(_fatePopGroupRowDataList);

                _fatePopGroupStringDataList[_npcResIDIndex] = _pair.Value.MonsterID.ToString();
                _fatePopGroupStringDataList[_npcResIDIndex + 1] = "1"; // 最小数量
                _fatePopGroupStringDataList[_npcResIDIndex + 2] = _pair.Value.MaxNum.ToString(); // 最大数量

                var _fateNpcRowDataList = mFateNpcExcelSheet?.GetRowCellDataByTargetKeysAndValus(
                    new List<int> { 0 },
                    new List<string> { _pair.Value.FateNpcID.ToString() }
                );

                if (_fateNpcRowDataList == null || _fateNpcRowDataList.Count < 1)
                {
                    throw new Exception($"无法获取 FateNPC，ID是 : [{_pair.Value.FateNpcID}]");
                }

                InternalProcessForFateNpcPosInfo(_fateNpcRowDataList, _fatePopGroupStringDataList, _fateNpcLayoutIDIndex);

                // IdleRange
                int.TryParse(_fateNpcRowDataList[_idleRangeIndex].GetCellValue(), out var _idleRange);
                if (_idleRange > 0)
                {
                    _fatePopGroupStringDataList[mFatePopGroupIdleRangeIndex] = CommonUtil.GetPosInfoByLevelReferenceID(
                        _idleRange, mExcelSheetLevelReference, true, false, true
                    );
                }
                else
                {
                    _fatePopGroupStringDataList[mFatePopGroupIdleRangeIndex] = string.Empty;
                }

                // DepopRange
                int.TryParse(_fateNpcRowDataList[_depopRangeIndex].GetCellValue(), out var _depopRange);
                if (_idleRange > 0)
                {
                    _fatePopGroupStringDataList[mFatePopGroupDepopRangeIndex] = CommonUtil.GetPosInfoByLevelReferenceID(
                        _depopRange, mExcelSheetLevelReference, true, false, true
                    );
                }
                else
                {
                    _fatePopGroupStringDataList[mFatePopGroupDepopRangeIndex] = string.Empty;
                }

                _writeDataMap.Add(_fatePopGroupRowDataList[0].GetCellRowIndexInSheet(), _fatePopGroupStringDataList);
            }

            foreach (var _pair in _writeDataMap)
            {
                mFatePopGroupExcelSheet?.WriteOneData(_pair.Key, _pair.Value, true);
            }

            mFateExcelFile?.SaveFile();

            return true;
        }

        private void InternalProcessForFateNpcPosInfo(List<CellValueData> _fateNpcRowDataList, List<string> _fatePopGroupStringDataList, int _fateNpcLayoutIDIndex)
        {
            int.TryParse(_fateNpcRowDataList[_fateNpcLayoutIDIndex].GetCellValue(), out var _layoutID);
            if (_layoutID > 0)
            {
                var _tempStr = CommonUtil.GetPosInfoByLevelReferenceID(_layoutID, mExcelSheetLevelReference, true, true, false);
                if (!string.IsNullOrEmpty(_tempStr))
                {
                    _fatePopGroupStringDataList[mFatePopGroupPopRangeIndex] = _tempStr;

                    return;
                }
            }

            int.TryParse(_fateNpcRowDataList[_fateNpcLayoutIDIndex].GetCellValue(), out var _popRange);
            if (_popRange > 0)
            {
                var _tempStr = CommonUtil.GetPosInfoByLevelReferenceID(_popRange, mExcelSheetLevelReference, true, true, false);
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
