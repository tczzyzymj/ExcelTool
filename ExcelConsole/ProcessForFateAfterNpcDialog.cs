using ExcelTool;

namespace ExcelConsole
{
    public class ProcessForFateAfterNpcDialog : ProcessBase
    {
        private static readonly int mFateTypeIndex          = CommonUtil.GetIndexByZm("F", 1);
        private static readonly int mFateTriggerNpcIndex    = CommonUtil.GetIndexByZm("N", 1);
        private static readonly int mFatePopGroupNpcIndex   = CommonUtil.GetIndexByZm("D", 1);
        private static readonly int mFateIdIndex            = CommonUtil.GetIndexByZm("A", 1);
        private static readonly int mFateNpcNormalTalkIndex = CommonUtil.GetIndexByZm("AH", 1);
        private static readonly int mFateNpcDialog00        = CommonUtil.GetIndexByZm("CN", 1);
        private static readonly int mNpcDialogIdIndex       = CommonUtil.GetIndexByZm("A", 1);
        private static readonly int mNpcDialogLibIdIndex    = CommonUtil.GetIndexByZm("B", 1);
        private static readonly int mFateNpcImportIdIndex   = CommonUtil.GetIndexByZm("J", 1);
        private static readonly int mFateCsvStarterIndex    = CommonUtil.GetIndexByZm("FD", 1);

        private readonly int mDefenceFatePrefix = 22;
        private readonly int mKillFatePrefix    = 23;

        // Fate.csv 相关
        private CSVFileData?  mCsvFileFate;
        private CSVSheetData? mCsvSheetFate;
        // end

        // FateEvent.csv 相关
        private CSVSheetData? mCsvSheetFateEvent;
        private CSVFileData?  mCsvFileFateEvent;
        // end

        // Fate总表 相关
        private ExcelFileData?  mExcelFileFate;
        private ExcelSheetData? mExcelSheetFate;
        private ExcelSheetData? mExcelSheetFateNpcDialogImport;
        // end

        // FateNpc.csv 相关
        private CSVFileData?  mCsvFileFateNpc;
        private CSVSheetData? mCsvSheetFateNpc;
        // end

        // FatePopGroup.csv 相关
        private CSVFileData?  mCsvFileFatePopGroup;
        private CSVSheetData? mCsvSheetFatePopGroup;
        // End

        private int mMaxDefenceDialogId;
        private int mMaxDefenceLibId;

        private int mMaxKillFateDialogId;
        private int mMaxKillFateLibId;

        private ExcelFileData?  mNpcDialogExcelFile;
        private ExcelSheetData? mNpcDialogExcelSheet;

        public override bool Process()
        {
            if (!InternalLoadFile())
            {
                return false;
            }

            if (mExcelSheetFate == null)
            {
                throw new Exception("错误，mExcelSheetFate 为空，请检查");
            }

            if (mNpcDialogExcelSheet == null)
            {
                throw new Exception("mNpcDialogExcelSheet 为空");
            }

            List<List<CellValueData>>? _allFateDatalist = mExcelSheetFate.GetAllDataList();

            if ((_allFateDatalist == null) || (_allFateDatalist.Count < 1))
            {
                throw new Exception("mExcelSheetFate 数据错误，请检查");
            }

            // 这里先去遍历一下，获取最大的讨伐是23开头， 防守是22开头
            List<List<CellValueData>>? _allDataList = mNpcDialogExcelSheet.GetAllDataList();

            if ((_allDataList == null) || (_allDataList.Count < 1))
            {
                throw new Exception("NPC对话表的数据为空，请检查!");
            }

            if (mExcelSheetFateNpcDialogImport == null)
            {
                throw new Exception("mExcelSheetFateNpcDialogImport 为空，请检查");
            }

            if (mExcelFileFate == null)
            {
                throw new Exception("mExcelFileFate 为空，请检查");
            }

            if (mNpcDialogExcelFile == null)
            {
                throw new Exception("mNpcDialogExcelFile 为空，请检查");
            }

            // 这里去找一下最大ID
            foreach (List<CellValueData> _tempRowData in _allDataList)
            {
                string _tempDialogIdStr = _tempRowData[mNpcDialogIdIndex].GetCellValue();

                string _tempLibIdStr = _tempRowData[mNpcDialogLibIdIndex].GetCellValue();

                if (!int.TryParse(_tempLibIdStr, out int _dialogLibId))
                {
                    continue;
                }

                if (!int.TryParse(_tempDialogIdStr, out int _dialogId))
                {
                    continue;
                }

                int _libIdPreFix = 0;

                if (_dialogLibId > 99999)
                {
                    _libIdPreFix = _dialogLibId / 10000;
                }
                else if (_dialogLibId > 9999)
                {
                    _libIdPreFix = _dialogLibId / 1000;
                }
                else
                {
                    throw new Exception($"错误，DialogLib : {_dialogLibId} 的数量级无效，请检查");
                }

                if (_libIdPreFix == mKillFatePrefix)
                {
                    // 这里记录一下最大的
                    if (mMaxKillFateDialogId < _dialogId)
                    {
                        mMaxKillFateDialogId = _dialogId;
                    }

                    if (mMaxKillFateLibId < _dialogLibId)
                    {
                        mMaxKillFateLibId = _dialogLibId;
                    }
                }
                else if (_libIdPreFix == mDefenceFatePrefix)
                {
                    // 这里记录一下最大的
                    if (mMaxDefenceDialogId < _dialogId)
                    {
                        mMaxDefenceDialogId = _dialogId;
                    }

                    if (mMaxDefenceLibId < _dialogLibId)
                    {
                        mMaxDefenceLibId = _dialogLibId;
                    }
                }
            }

            mMaxKillFateDialogId++;
            mMaxKillFateLibId++;
            mMaxDefenceDialogId++;
            mMaxDefenceLibId++;

            if (mCsvSheetFate == null)
            {
                throw new Exception("mCsvSheetFate 为空，请检查");
            }

            if (mCsvSheetFatePopGroup == null)
            {
                throw new Exception("mCsvSheetFatePopGroup 为空，请检查");
            }

            Dictionary<int, NewDialogData> _newDataMap = new Dictionary<int, NewDialogData>();

            int _count = 0;

            for (int _i = 0; _i < _allFateDatalist.Count; ++_i)
            {
                List<CellValueData> _tempFateRowData = _allFateDatalist[_i];
                string              _tempFateIdStr   = _tempFateRowData[mFateIdIndex].GetCellValue();
                int.TryParse(_tempFateIdStr, out int _fateId);

                if (_fateId <= 0)
                {
                    throw new Exception("FateId无效，请检查，尝试解析数据为 : " + _tempFateIdStr);
                }

                var _triggerNpcStr = _tempFateRowData[mFateTriggerNpcIndex].GetCellValue();

                if (string.IsNullOrEmpty(_triggerNpcStr))
                {
                    continue;
                }

                string _tempCellValue = _tempFateRowData[mFateTypeIndex].GetCellValue();

                if (string.IsNullOrEmpty(_tempCellValue))
                {
                    throw new Exception($"FATE类型为空，ID是：{_fateId}");
                }

                bool _isKillFate    = _tempCellValue.Contains("讨伐");
                bool _isDefenceFate = _tempCellValue.Contains("防守");

                if (!_isKillFate && !_isDefenceFate)
                {
                    continue;
                }

                ++_count;
                Console.WriteLine("数量为 : " + _count);

                List<CellValueData>? _csvFateData = mCsvSheetFate.GetCacheRowDataListByKeyStr(_tempFateIdStr);

                if ((_csvFateData == null) || (_csvFateData.Count < 1))
                {
                    throw new Exception($"无法在 Fate.csv 中找到数据，ID是：{_tempFateIdStr}");
                }

                string _startGroupStr = _csvFateData[mFateCsvStarterIndex].GetCellValue();
                int.TryParse(_startGroupStr, out int _startGroupId);

                if (_startGroupId <= 0)
                {
                    continue;
                }

                List<CellValueData>? _startGroupData = mCsvSheetFatePopGroup.GetCacheRowDataListByKeyStr(_startGroupStr);

                if ((_startGroupData == null) || (_startGroupData.Count < 1))
                {
                    throw new Exception("无法获取 FatePopGroup 数据，Id是:" + _startGroupStr);
                }

                int.TryParse(_startGroupData[mFatePopGroupNpcIndex].GetCellValue(), out int _finalNpcId);

                if (_finalNpcId <= 0)
                {
                    throw new Exception($"出错了，FATE：{_fateId} , _startGroupStr :{_startGroupStr} 的 TriggerNpcId 有误，请检查");
                }

                List<CellValueData>? _fateNpcRowData = mCsvSheetFateNpc?.GetCacheRowDataListByKeyStr(_finalNpcId.ToString());

                if (_fateNpcRowData == null)
                {
                    throw new Exception($"无法获取 FateNpc, ID 是 ： {_finalNpcId}");
                }

                string _normalTalkEventStr = _fateNpcRowData[mFateNpcNormalTalkIndex].GetCellValue();

                if (!int.TryParse(_normalTalkEventStr, out int _fateEventId))
                {
                    throw new Exception($"FateNpc : {_fateNpcRowData[0].GetCellValue()} 的 NormalTalkEvent 无法转化为 INT，请检查");
                }

                if (_fateEventId <= 0)
                {
                    Console.WriteLine($"Fate : {_fateId} , Npc : {_finalNpcId} 没有 NormalTalkEventId");

                    continue;
                }

                List<CellValueData>? _fateEventRowData = mCsvSheetFateEvent?.GetCacheRowDataListByKeyStr(_normalTalkEventStr);

                if (_fateEventRowData == null)
                {
                    throw new Exception($"无法在 FateEvent 中找到数据，ID是：{_normalTalkEventStr}");
                }

                int _newLibId = 0;

                if (_isDefenceFate)
                {
                    _newLibId = mMaxDefenceLibId;
                    Console.WriteLine($"添加 防守 Fate 对话， FateId : {_fateId}, LibId : {_newLibId}");
                    mMaxDefenceLibId++;
                }
                else if (_isKillFate)
                {
                    _newLibId = mMaxKillFateLibId;
                    Console.WriteLine($"添加 讨伐 Fate 对话， FateId : {_fateId}, LibId : {_newLibId}");
                    mMaxKillFateLibId++;
                }

                NewDialogData _newData = new NewDialogData();
                _newData.NpcId       = _finalNpcId;
                _newData.FateEventId = _fateEventId;
                _newData.FateId      = _fateId;

                _newDataMap.Add(_newLibId, _newData);

                for (int _j = 0; _j < 4; ++_j)
                {
                    string _tempContentStr = _fateEventRowData[mFateNpcDialog00 + _j].GetCellValue();

                    if (string.IsNullOrEmpty(_tempContentStr))
                    {
                        break;
                    }

                    int _newDialogId = 0;

                    if (_isDefenceFate)
                    {
                        _newDialogId = mMaxDefenceDialogId;
                        mMaxDefenceDialogId++;
                    }
                    else if (_isKillFate)
                    {
                        _newDialogId = mMaxKillFateDialogId;
                        mMaxKillFateDialogId++;
                    }

                    _newData.DialogIDList.Add(_newDialogId);
                    _newData.DialogContentList.Add(_tempContentStr);
                }
            }

            bool _existSame = false;

            // 这里先对NpcDialog进行检测，看是否有重复的
            foreach (KeyValuePair<int, NewDialogData> _pair in _newDataMap)
            {
                foreach (int _existDialogId in _pair.Value.DialogIDList)
                {
                    List<CellValueData>? _tempData = mNpcDialogExcelSheet.GetCacheRowDataListByKeyStr(_existDialogId.ToString());

                    if ((_tempData != null) && (_tempData.Count > 0))
                    {
                        _existSame = true;

                        Console.WriteLine("存在相同的DialogId:" + _existDialogId);
                    }
                }
            }

            if (_existSame)
            {
                throw new Exception("存在相同的NpcDialogId");
            }

            // 写入NPC对话表
            foreach (KeyValuePair<int, NewDialogData> _pair in _newDataMap)
            {
                for (int _j = 0; _j < _pair.Value.DialogIDList.Count; ++_j)
                {
                    List<string> _newData = new List<string>
                    {
                        _pair.Value.DialogIDList[_j].ToString(),
                        _pair.Key.ToString(),
                        "任务",
                        "1",
                        _pair.Value.NpcId.ToString(),
                        string.Empty,
                        _pair.Value.DialogContentList[_j],
                        string.Empty,
                        string.Empty,
                        $"{_pair.Value.FateId}_{_pair.Value.NpcId}_{_pair.Value.FateEventId}_{_j}" // 关联用的
                    };

                    mNpcDialogExcelSheet.WriteOneData(-1, _newData, false);
                }
            }

            // 写入FATE的NPC对话导入表
            foreach (KeyValuePair<int, NewDialogData> _pair in _newDataMap)
            {
                List<CellValueData>? _rowData = mExcelSheetFateNpcDialogImport.GetCacheRowDataListByKeyStr(_pair.Value.FateId.ToString());

                if ((_rowData == null) || (_rowData.Count < 1))
                {
                    throw new Exception($"Fate ID : {_pair.Value.FateId}, 无法在 NPC对话表中找到");
                }

                _rowData[mFateNpcImportIdIndex].SetCellValue(_pair.Key.ToString());
            }

            mNpcDialogExcelSheet.SaveSheet();
            mExcelSheetFateNpcDialogImport.SaveSheet();
            mExcelFileFate.SaveFile();
            mNpcDialogExcelFile.SaveFile();

            return true;
        }

        private bool InternalLoadFile()
        {
            // 加载 FATE表.xlsx
            {
                // 主流程
                {
                    string _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                    mExcelFileFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                    mExcelSheetFate = mExcelFileFate.GetWorkSheetByIndex(2) as ExcelSheetData;

                    if (mExcelSheetFate == null)
                    {
                        throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                    }

                    mExcelSheetFate.SetContentStartRowIndexInSheet(3);

                    List<KeyData> _keyListData = mExcelSheetFate.GetKeyListData();

                    for (int _i = 0; _i < _keyListData.Count; ++_i)
                    {
                        _keyListData[_i].IsMainKey = false;
                    }

                    _keyListData[0].IsMainKey = true;

                    mExcelSheetFate.LoadAllCellData(true);
                }

                // NPC对话导入sheet
                {
                    mExcelSheetFateNpcDialogImport = mExcelFileFate.GetWorkSheetByIndex(19) as ExcelSheetData;

                    if (mExcelSheetFateNpcDialogImport == null)
                    {
                        throw new Exception("mFateFile.GetWorkSheetByIndex(13)");
                    }

                    mExcelSheetFateNpcDialogImport.SetContentStartRowIndexInSheet(3);

                    List<KeyData> _keyList = mExcelSheetFateNpcDialogImport.GetKeyListData();

                    for (int _i = 0; _i < _keyList.Count; ++_i)
                    {
                        _keyList[_i].IsMainKey = false;
                    }

                    _keyList[0].IsMainKey = true;

                    mExcelSheetFateNpcDialogImport.LoadAllCellData(true);
                }
            }

            // 加载 NPC对话表.xlsx
            {
                string _tempPath = Path.Combine(FolderPath, "NPC对话表.xlsx");
                mNpcDialogExcelFile  = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                mNpcDialogExcelSheet = mNpcDialogExcelFile.GetWorkSheetByIndex(1) as ExcelSheetData;

                if (mNpcDialogExcelSheet == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }

                mNpcDialogExcelSheet.SetContentStartRowIndexInSheet(3);

                List<KeyData> _keyList = mNpcDialogExcelSheet.GetKeyListData();

                for (int _i = 0; _i < _keyList.Count; ++_i)
                {
                    _keyList[_i].IsMainKey = false;
                }

                _keyList[0].IsMainKey = true;

                mNpcDialogExcelSheet.LoadAllCellData(true);
            }

            // 加载 FateNpc.csv
            {
                string _tempPath = Path.Combine(FolderPath, "FateNpc.csv");
                mCsvFileFateNpc  = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCsvSheetFateNpc = mCsvFileFateNpc.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCsvSheetFateNpc == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }

                List<KeyData> _keyList = mCsvSheetFateNpc.GetKeyListData();

                for (int _i = 0; _i < _keyList.Count; ++_i)
                {
                    _keyList[_i].IsMainKey = false;
                }

                _keyList[0].IsMainKey = true;

                mCsvSheetFateNpc.LoadAllCellData(true);

                List<CellValueData>? _targetRow = mCsvSheetFateNpc.GetCacheRowDataListByKeyStr("2139");

                if (_targetRow == null)
                {
                    throw new Exception("no");
                }
            }

            // 加载 FateEvent.csv
            {
                string _tempPath = Path.Combine(FolderPath, "FateEvent.csv");
                mCsvFileFateEvent  = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCsvSheetFateEvent = mCsvFileFateEvent.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCsvSheetFateEvent == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }

                List<KeyData> _keyList = mCsvSheetFateEvent.GetKeyListData();

                for (int _i = 0; _i < _keyList.Count; ++_i)
                {
                    _keyList[_i].IsMainKey = false;
                }

                _keyList[0].IsMainKey = true;

                mCsvSheetFateEvent.LoadAllCellData(true);
            }

            // 加载 Fate.csv
            {
                string _tempPath = Path.Combine(FolderPath, "Fate.csv");
                mCsvFileFate  = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCsvSheetFate = mCsvFileFate.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCsvSheetFate == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }

                List<KeyData> _keyList = mCsvSheetFate.GetKeyListData();

                for (int _i = 0; _i < _keyList.Count; ++_i)
                {
                    _keyList[_i].IsMainKey = false;
                }

                _keyList[0].IsMainKey = true;

                mCsvSheetFate.LoadAllCellData(true);
            }

            // 加载 FatePopRange.csv
            {
                string _tempPath = Path.Combine(FolderPath, "FatePopGroup.csv");
                mCsvFileFatePopGroup  = new CSVFileData(_tempPath, LoadFileType.NormalFile);
                mCsvSheetFatePopGroup = mCsvFileFatePopGroup.GetWorkSheetByIndex(0) as CSVSheetData;

                if (mCsvSheetFatePopGroup == null)
                {
                    throw new Exception("无法获取 mFatePopGroupCSVFile.GetWorkSheetByIndex(0)");
                }

                List<KeyData> _keyList = mCsvSheetFatePopGroup.GetKeyListData();

                for (int _i = 0; _i < _keyList.Count; ++_i)
                {
                    _keyList[_i].IsMainKey = false;
                }

                _keyList[0].IsMainKey = true;

                mCsvSheetFatePopGroup.LoadAllCellData(true);
            }

            return true;
        }

        private class NewDialogData
        {
            public readonly List<string> DialogContentList = new List<string>();

            public readonly List<int> DialogIDList = new List<int>();

            public int FateEventId;
            public int FateId;

            public int NpcId;
        }
    }
}
