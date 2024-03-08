using ExcelTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConsole
{
    public class FateNpcData
    {
        public int FateNpcID;

        public int MonsterID;

        public int BaseID;

        public string PosInfo;

        public string LogInfo;

        public int MinCount = 0;
        public int MaxCount = 1;
    }

    public class ProcessForUseNewMonsterID : ProcessBase
    {
        private ExcelFileData? mExcelFateNpc = null;
        private ExcelSheetData? mExcelSheetFateNpc = null;

        private ExcelFileData? mExcelMonster = null;
        private ExcelSheetData? mExcelSheetMonster = null;

        private ExcelFileData? mExcelFate = null;
        private ExcelSheetData? mExcelSheetPopGroupInFateExcel = null;

        private ExcelFileData? mExcelFateOrigin = null;
        private ExcelSheetData? mExcelSheetFateOrigin = null;

        private ExcelFileData? mExcelFatePopGropOrigin = null;
        private ExcelSheetData? mExcelSheetFatePopGroupOrigin = null;

        private ExcelFileData? mExcelLevelReference = null;
        private ExcelSheetData? mExcelSheetLevelReference = null;

        private CSVFileData? mCSVFatePopGroup = null;
        private CSVSheetData? mCSVSheetFatePopGroup = null;


        private ExcelFileData? mExcelCompare = null;

        private static int mFateMonsterIDStartIndex = CommonUtil.GetIndexByZM("E") - 1;
        private static int mFateMonsterPosStartIndex = CommonUtil.GetIndexByZM("I") - 1;
        private static int mFateMonsterBaseIDIndex = CommonUtil.GetIndexByZM("F") - 1;
        private static int mFateMonsteMinrCountIndex = CommonUtil.GetIndexByZM("G") - 1;

        private static int mFateOriginPopRange00Index = CommonUtil.GetIndexByZM("BE") - 2;

        private static int mMonsterFateNpcIdIndex = CommonUtil.GetIndexByZM("I") - 1;
        private static int mMonsterLevelColumIndex = CommonUtil.GetIndexByZM("E") - 1;
        private static int mMonsterAttrIDColumIndex = CommonUtil.GetIndexByZM("AS") - 1;
        private static int mMonsterAITableIDIndex = CommonUtil.GetIndexByZM("BD") - 1;
        private static int mMonsterNameIndex = CommonUtil.GetIndexByZM("G") - 1;
        private static int mMonsterIDIndex = CommonUtil.GetIndexByZM("F") - 1;
        private static int mMonsterBaseIDIndex = CommonUtil.GetIndexByZM("D") - 1;

        private static int mGuardParamIndex = CommonUtil.GetIndexByZM("BX") - 1;

        private bool mTest = false;

        public override bool Process()
        {
            // 加载文件相关
            {
                if (mTest)
                {
                    var _tempPath = Path.Combine(FolderPath, "Compare.xlsx");
                    mExcelCompare = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                }

                // 加载 FATE表.xlsx
                {
                    var _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                    mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    if (mExcelFate == null)
                    {
                        throw new Exception($"错误，加载 FATE表.xlsx 数据出错");
                    }

                    // 重新加载一下 key

                    mExcelSheetPopGroupInFateExcel = mExcelFate?.GetWorkSheetByIndex(6) as ExcelSheetData;
                    if (mExcelSheetPopGroupInFateExcel == null)
                    {
                        throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                    }

                    var _keyListData = mExcelSheetPopGroupInFateExcel.GetKeyListData();
                    for (int i = 0; i < _keyListData.Count; ++i)
                    {
                        _keyListData[i].IsMainKey = false;
                    }

                    _keyListData[0].IsMainKey = true;

                    mExcelSheetPopGroupInFateExcel.LoadAllCellData(true);
                }

                // fate 端游表 数据加载
                {
                    var _tempPath = Path.Combine(FolderPath, "Fate.xlsx");
                    mExcelFateOrigin = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
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
                    var _tempPath = Path.Combine(FolderPath, "FatePopGroup.xlsx");
                    mExcelFatePopGropOrigin = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
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
                    var _tempPath = Path.Combine(FolderPath, "G怪物表.xlsx");
                    mExcelMonster = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetMonster = mExcelMonster.GetWorkSheetByIndex(1) as ExcelSheetData;
                    if (mExcelSheetMonster == null)
                    {
                        throw new Exception($"mExcelMonster.GetWorkSheetByIndex(1) 获取数据出错");
                    }

                    mExcelSheetMonster.SetKeyStartRowIndexInSheet(1);
                    mExcelSheetMonster.ReloadKey();

                    var _allKeyDataList = mExcelSheetMonster.GetKeyListData();
                    _allKeyDataList[CommonUtil.GetIndexByZM("F") - 1].IsMainKey = true;

                    mExcelSheetMonster.LoadAllCellData(true);
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

                    mExcelSheetFateNpc.SetKeyStartRowIndexInSheet(1);
                    mExcelSheetFateNpc.SetKeyStartColmIndexInSheet(1);
                    mExcelSheetFateNpc.SetContentStartRowIndexInSheet(4);

                    mExcelSheetFateNpc.ReloadKey();
                    var _allKeyDataList = mExcelSheetFateNpc.GetKeyListData();
                    _allKeyDataList[0].IsMainKey = true;

                    mExcelSheetFateNpc.LoadAllCellData(true);
                }

                // fatepopgroup csv表 数据加载
                {
                    var _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                    mCSVFatePopGroup = new CSVFileData(_tempPath, LoadFileType.NormalFile);
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
                // end
            }

            // key : OldMonsterID ; value : FateNpcID
            Dictionary<int, int> _oldDataMonsterIDToFateNpcID = new Dictionary<int, int>();

            // key : fateID ; value : List<FateNpcID>
            Dictionary<int, List<FateNpcData>> _writeDataForFatePopGroupMonster = new Dictionary<int, List<FateNpcData>>();

            // 解析数据中
            var _allFateDataList = mExcelSheetPopGroupInFateExcel.GetAllDataList();
            if (_allFateDataList == null || _allFateDataList.Count < 1)
            {
                throw new Exception($"无法获取所有数据，请检查!");
            }
            for (int i = 0; i < _allFateDataList.Count; ++i)
            {
                Dictionary<int, List<FateNpcData>> _oldListDataMap = new Dictionary<int, List<FateNpcData>>();
                Dictionary<int, List<FateNpcData>> _newListDataMap = new Dictionary<int, List<FateNpcData>>();

                int.TryParse(_allFateDataList[i][0].GetCellValue(), out var _targetFateID);
                if (_targetFateID <= 0)
                {
                    throw new Exception($"错误，无法获取 创建物的 FateID ,index 是：[{i}]");
                }

                // 解析一下旧数据
                {
                    var _targetRowData = _allFateDataList[i];

                    var _tempList = new List<FateNpcData>();
                    _oldListDataMap.Add(_targetFateID, _tempList);

                    for (int _tempIndex = 0; _tempIndex <= 15; ++_tempIndex)
                    {
                        int.TryParse(
                            _targetRowData[mFateMonsterIDStartIndex + _tempIndex * 15].GetCellValue(),
                            out var _tempMonsterId
                        );

                        if (_tempMonsterId <= 0)
                        {
                            continue;
                        }

                        var _tempPosInfo = _targetRowData[mFateMonsterPosStartIndex + _tempIndex * 15].GetCellValue();
                        if (string.IsNullOrEmpty(_tempPosInfo))
                        {
                            continue;
                        }

                        var _newData = new FateNpcData();
                        _newData.MonsterID = _tempMonsterId;
                        _newData.PosInfo = _tempPosInfo;
                        _newData.LogInfo = $"MonsterID:[{_tempMonsterId}] ; pos:[{_tempPosInfo}] ";
                        _tempList.Add(_newData);
                    }
                }

                // 解析端游数据
                {
                    var _targetRowData = mExcelSheetFateOrigin.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 0 },
                        new List<string> { $"{_targetFateID}" }
                    );

                    if (_targetRowData == null || _targetRowData.Count < 1)
                    {
                        throw new Exception($"数据无效，无法找到 fate :[{_targetFateID}] 数据，请检查");
                    }

                    // 护送的不管
                    int.TryParse(_targetRowData[mGuardParamIndex].GetCellValue(), out var _guardParam);
                    if (_guardParam > 0)
                    {
                        continue;
                    }

                    var _tempList = new List<FateNpcData>();
                    _newListDataMap.Add(_targetFateID, _tempList);

                    // 这里是 怪物的
                    for (int _tempIndex = 0; _tempIndex < 12; ++_tempIndex)
                    {
                        var _tempFatePopGroupStr = _targetRowData[mFateOriginPopRange00Index + _tempIndex].GetCellValue();
                        if (string.IsNullOrEmpty(_tempFatePopGroupStr))
                        {
                            continue;
                        }

                        var _originRowData = mExcelSheetFatePopGroupOrigin.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { 2 },
                            new List<string> { _tempFatePopGroupStr }
                        );

                        if (_originRowData == null || _originRowData.Count < 1)
                        {
                            throw new Exception(
                                $"1 无法在端游元表 FatePopGroup 中找到数据，KEYDevCode是：[{_tempFatePopGroupStr}], ColumIndex:[{_tempIndex}],Row:[{_targetRowData[0].GetCellValue()}]"
                            );
                        }

                        int.TryParse(_originRowData[0].GetCellValue(), out var _fatePopGroupID);
                        if (_fatePopGroupID <= 0)
                        {
                            throw new Exception(
                                $"2 无法在端游元表 FatePopGroup 中找到数据，KEYDevCode是：[{_tempFatePopGroupStr}], ColumIndex:[{_tempIndex}],Row:[{_targetRowData[0].GetCellValue()}]"
                            );
                        }

                        var _fatePopGroupData = mCSVSheetFatePopGroup.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { 0 },
                            new List<string> { _fatePopGroupID.ToString() }
                        );

                        if (_fatePopGroupData == null || _fatePopGroupData.Count < 1)
                        {
                            throw new Exception($"无法在 FatePopGroup.csv 中找到ID: {_fatePopGroupID}，请检查");
                        }

                        for (int j = 0; j <= 15; ++j)
                        {
                            int.TryParse(_fatePopGroupData[3 + j * 2].GetCellValue(), out var _fateNpcID);
                            if (_fateNpcID <= 0)
                            {
                                continue;
                            }

                            int.TryParse(_fatePopGroupData[4 + j * 2].GetCellValue(), out var _maxCount);

                            var _fateNpcRowData = mExcelSheetFateNpc.GetRowCellDataByTargetKeysAndValus(
                                new List<int> { 0 },
                                new List<string> { _fateNpcID.ToString() }
                            );

                            if (_fateNpcRowData == null || _fateNpcRowData.Count < 1)
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

                            } while (false);

                            if (_levelReferenceID <= 0)
                            {
                                Console.WriteLine($"无法找到LevelReferenceID, FateNpcID : {_fateNpcID}，请检查");
                                continue;
                            }

                            var _posInfo = CommonUtil.GetPosInfoByLevelReferenceID(
                                _levelReferenceID, mExcelSheetLevelReference, true, false, true
                            );

                            var _findIndex = _tempList.FindIndex(x => x.FateNpcID == _fateNpcID);
                            if (_findIndex < 0)
                            {
                                if (!_writeDataForFatePopGroupMonster.TryGetValue(_targetFateID, out var _targetFateNpcDataList))
                                {
                                    _targetFateNpcDataList = new List<FateNpcData>();
                                    _writeDataForFatePopGroupMonster[_targetFateID] = _targetFateNpcDataList;
                                }

                                var _newData = new FateNpcData();
                                _targetFateNpcDataList.Add(_newData);
                                var _msg = $"FateNpcID:[{_fateNpcID}] ; Pos:[{_posInfo}]";
                                _newData.FateNpcID = _fateNpcID;
                                _newData.PosInfo = _posInfo;
                                _newData.LogInfo = _msg;
                                _newData.MaxCount = _maxCount;
                                _tempList.Add(_newData);
                            }
                            else
                            {
                                _tempList[_findIndex].MinCount++;
                            }
                        }
                    }
                }

                // 写入 Compare
                if (mTest)
                {
                    var _package = mExcelCompare?.GetExcelPackage();
                    if (_package == null)
                    {
                        throw new Exception("获取 Package 为空，请检查");
                    }

                    var _sheet = _package.Workbook.Worksheets[0];

                    foreach (var _pair in _oldListDataMap)
                    {
                        _newListDataMap.TryGetValue(_pair.Key, out var _newInfoList);


                        _sheet.Cells[1 + i * 2, 1].Value = _pair.Key.ToString();
                        for (int j = 0; j < _pair.Value.Count; ++j)
                        {
                            if (_newInfoList != null && j < _newInfoList.Count)
                            {
                                _pair.Value[j].FateNpcID = _newInfoList[j].FateNpcID;
                            }

                            _sheet.Cells[1 + i * 2, j + 2].Value = $"FateNpcID:[{_pair.Value[j].FateNpcID}] ; " +
                                _pair.Value[j].LogInfo;
                        }
                    }

                    foreach (var _pair in _newListDataMap)
                    {
                        _sheet.Cells[2 + i * 2, 1].Value = _pair.Key.ToString();
                        for (int j = 0; j < _pair.Value.Count; ++j)
                        {
                            _sheet.Cells[2 + i * 2, j + 2].Value = _pair.Value[j].LogInfo;
                        }
                    }
                }
                else
                {
                    // 这里对原有的数据做一下排列
                    foreach (var _pair in _oldListDataMap)
                    {
                        _newListDataMap.TryGetValue(_pair.Key, out var _newInfoList);

                        for (int j = 0; j < _pair.Value.Count; ++j)
                        {
                            if (_newInfoList != null && j < _newInfoList.Count)
                            {
                                _pair.Value[j].FateNpcID = _newInfoList[j].FateNpcID;

                                _oldDataMonsterIDToFateNpcID[_pair.Value[j].MonsterID] = _newInfoList[j].FateNpcID;
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
                // 先处理一下 怪物表
                {
                    Dictionary<int, List<CellValueData>> _cacheMonsterDataMap = new Dictionary<int, List<CellValueData>>();
                    {
                        var _allDataList = mExcelSheetMonster.GetAllDataList();
                        if (_allDataList == null || _allDataList.Count < 1)
                        {
                            throw new Exception($"无法获取所有的 怪物表 数据，请检查!");
                        }

                        foreach (var _dataList in _allDataList)
                        {
                            int.TryParse(_dataList[mMonsterFateNpcIdIndex].GetCellValue(), out var _fateNpcID);
                            if (_fateNpcID > 0)
                            {
                                _cacheMonsterDataMap[_fateNpcID] = _dataList;
                            }
                        }
                    }

                    // 这里将新数据的 MonsterID 设置进去
                    {
                        foreach (var _pair in _writeDataForFatePopGroupMonster)
                        {
                            foreach (var _data in _pair.Value)
                            {
                                if (!_cacheMonsterDataMap.TryGetValue(_data.FateNpcID, out var _rowDataList))
                                {
                                    throw new Exception($"无法在Monster中找到数据，基于 FateNPCID，请检查");
                                }

                                int.TryParse(_rowDataList[mMonsterIDIndex].GetCellValue(), out var _monsterID);
                                _data.MonsterID = _monsterID;
                                int.TryParse(_rowDataList[mMonsterBaseIDIndex].GetCellValue(), out var _baseID);
                                _data.BaseID = _baseID;
                            }
                        }
                    }

                    // 写入怪物表相关数据
                    foreach (var _pair in _oldDataMonsterIDToFateNpcID)
                    {
                        var _oldRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());
                        if (_oldRowData == null || _oldRowData.Count < 1)
                        {
                            Console.WriteLine($"错误，无法在 G怪物表 中获取数据，MonsterID是：{_pair.Key}");
                            continue;
                        }

                        var _newRowData = mExcelSheetMonster.GetRowCellDataByTargetKeysAndValus(
                            new List<int> { mMonsterFateNpcIdIndex },
                            new List<string> { _pair.Value.ToString() }
                        );

                        if (_newRowData == null || _newRowData.Count < 1)
                        {
                            throw new Exception($"错误,无法在怪物表中找到数据，基于Fate关联表，ID 是：[{_pair.Value}]");
                        }

                        _newRowData[mMonsterLevelColumIndex].WriteCellValue(
                            _oldRowData[mMonsterLevelColumIndex].GetCellValue()
                        );
                        _newRowData[mMonsterAttrIDColumIndex].WriteCellValue(
                            _oldRowData[mMonsterAttrIDColumIndex].GetCellValue()
                        );
                        _newRowData[mMonsterAITableIDIndex].WriteCellValue(
                            _oldRowData[mMonsterAITableIDIndex].GetCellValue()
                        );

                        var _newDataMonsterName = _newRowData[mMonsterNameIndex].GetCellValue();
                        if (!string.IsNullOrEmpty(_newDataMonsterName))
                        {
                            _newRowData[mMonsterNameIndex].WriteCellValue(
                                _oldRowData[mMonsterNameIndex].GetCellValue()
                            );
                        }
                    }

                    // 写入怪物表相关数据
                    foreach (var _pair in _oldDataMonsterIDToFateNpcID)
                    {
                        var _oldRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());
                        if (_oldRowData == null || _oldRowData.Count < 1)
                        {
                            Console.WriteLine($"错误，无法在 G怪物表 中获取数据，MonsterID是：{_pair.Key}");
                            continue;
                        }

                        // 这里将 FATENPCID 写入到旧数据
                        _oldRowData[mMonsterFateNpcIdIndex].WriteCellValue(_pair.Value.ToString());
                    }

                    // 写入 Fate 创建表相关数据
                    {
                        foreach (var _pair in _writeDataForFatePopGroupMonster)
                        {
                            var _targetRowData = mExcelSheetPopGroupInFateExcel.GetCacheRowDataListByKeyStr(
                                _pair.Key.ToString()
                            );

                            if (_targetRowData == null || _targetRowData.Count < 1)
                            {
                                throw new Exception($"错误，无法获取 Fate 创建物表数据，FateID 是 : [{_pair.Key}]");
                            }

                            for (int i = 0; i < _pair.Value.Count; ++i)
                            {
                                var _data = _pair.Value[i];
                                // 这里写入新的 MonsterID ，数量，POS数据
                                if (_data.MonsterID <= 0)
                                {
                                    throw new Exception($"错误，Monster数据无效，FATEID:[{_pair.Key}], index:[]");
                                }

                                if (i > 15)
                                {
                                    Console.WriteLine($"错误，怪物的数量超出了范围，请检查! FateID : [{_pair.Key}]");
                                    break;
                                }

                                var _indexOffset = i * 15;

                                _targetRowData[mFateMonsterIDStartIndex + _indexOffset].WriteCellValue(_data.MonsterID.ToString());
                                _targetRowData[mFateMonsterPosStartIndex + _indexOffset].WriteCellValue(_data.PosInfo);
                                _targetRowData[mFateMonsterBaseIDIndex + _indexOffset].WriteCellValue(_data.BaseID.ToString());
                                if (_data.MaxCount > 0)
                                {
                                    _targetRowData[mFateMonsteMinrCountIndex + _indexOffset].WriteCellValue("1");
                                    _targetRowData[mFateMonsteMinrCountIndex + 1 + _indexOffset].WriteCellValue(_data.MaxCount.ToString());
                                }
                                else
                                {
                                    _targetRowData[mFateMonsteMinrCountIndex + _indexOffset].WriteCellValue("0");
                                    _targetRowData[mFateMonsteMinrCountIndex + 1 + _indexOffset].WriteCellValue("0");
                                }
                            }
                        }
                    }
                }

                // 写入数据
                mExcelSheetMonster.SaveSheet();
                mExcelMonster.SaveFile();
                mExcelSheetPopGroupInFateExcel.SaveSheet();
                mExcelFate.SaveFile();
            }

            return true;
        }
    }
}
