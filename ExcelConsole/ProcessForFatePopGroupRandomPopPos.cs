using ExcelTool;

namespace ExcelConsole
{
    public class ProcessForFatePopGroupRandomPopPos : ProcessBase
    {
        private ExcelFileData?  mExcelFateNpc;
        private ExcelSheetData? mExcelSheetFateNpc;

        private ExcelFileData?  mExcelMonster;
        private ExcelSheetData? mExcelSheetMonster;

        private ExcelFileData?  mExcelLevelReference;
        private ExcelSheetData? mExcelSheetLevelReference;

        private ExcelFileData?  mExcelFate;
        private ExcelSheetData? mExcelSheetPopGroupInFateExcel;

        private bool InternalLoadFiles()
        {
            // 加载 FATE表.xlsx
            {
                string _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                // 重新加载一下 key
                {
                    mExcelSheetPopGroupInFateExcel = mExcelFate?.GetWorkSheetByIndex(6) as ExcelSheetData;

                    if (mExcelSheetPopGroupInFateExcel == null)
                    {
                        throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                    }

                    {
                        List<KeyData> _keyListData = mExcelSheetPopGroupInFateExcel.GetKeyListData();

                        for (int i = 0; i < _keyListData.Count; ++i)
                        {
                            _keyListData[i].IsMainKey = false;
                        }

                        _keyListData[0].IsMainKey = true;
                    }

                    mExcelSheetPopGroupInFateExcel.LoadAllCellData(true);
                }
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

            // 加载 LevelReference.xlsx 日文原本数据
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

            return true;
        }

        // 怪物表
        private static readonly int mMonsterFateNpcIDIndex = CommonUtil.GetIndexByZm("I", 1);
        private static readonly int mMonsterIDIndex        = CommonUtil.GetIndexByZm("F", 1);

        // FATE创建物表
        private static readonly int mFatePopGroupMonsterIDIndex       = CommonUtil.GetIndexByZm("E", 1);
        private static readonly int mFatePopGroupRandomPopRange0Index = CommonUtil.GetIndexByZm("J", 1);

        private static readonly int mMonsterIDOffset = 15;

        // FateNpc随机位置 Index
        private static readonly int mFateNpcRandomPopRangeIndex = CommonUtil.GetIndexByZm("F", 1);

        public override bool Process()
        {
            if (!InternalLoadFiles())
            {
                return false;
            }

            // 遍历Fate创建物里面的所有
            List<List<CellValueData>>? _fatePopGroupDataList = mExcelSheetPopGroupInFateExcel?.GetAllDataList();

            if ((_fatePopGroupDataList == null) || (_fatePopGroupDataList.Count < 1))
            {
                throw new Exception("数据出错，mExcelSheetPopGroupInFateExcel.GetAllDataList()");
            }

            foreach (List<CellValueData> fatePopGroupRowData in _fatePopGroupDataList)
            {
                int.TryParse(fatePopGroupRowData[0].GetCellValue(), out int _fateID);

                for (int i = 0; i <= mMonsterIDOffset; ++i)
                {
                    if (!int.TryParse(
                            fatePopGroupRowData[mFatePopGroupMonsterIDIndex + (i * mMonsterIDOffset)].GetCellValue(),
                            out int _monsterID
                        ))
                    {
                        continue;
                    }

                    // 这里是去找，看对应的 FateNpc 数据是否有随机位置

                    // 获取到 _monsterID 去 Monster表里面查找  FateNpcID
                    List<CellValueData>? _monsterRowData = mExcelSheetMonster?.GetRowCellDataByTargetKeysAndValues(
                        new List<int> { mMonsterIDIndex },
                        new List<string> { _monsterID.ToString() }
                    );

                    if ((_monsterRowData == null) || (_monsterRowData.Count < 1))
                    {
                        continue;
                    }

                    if (!int.TryParse(_monsterRowData[mMonsterFateNpcIDIndex].GetCellValue(), out int _fateNpcID))
                    {
                        continue;
                    }

                    if (_fateNpcID <= 0)
                    {
                        continue;
                    }

                    List<CellValueData>? _fateNpcRowData = mExcelSheetFateNpc?.GetRowCellDataByTargetKeysAndValues(
                        new List<int> { 0 },
                        new List<string> { _fateNpcID.ToString() }
                    );

                    if ((_fateNpcRowData == null) || (_fateNpcRowData.Count < 1))
                    {
                        throw new Exception($"无法找到 FateNpc,ID是：{_fateNpcID}");
                    }

                    //if (_fateID == 434)
                    //{
                    //    Console.WriteLine($"查找 MonsterID : [{_monsterID}] , FateNpcID : [{_fateNpcID}]");
                    //}

                    for (int z = 0; z <= 7; ++z)
                    {
                        if (!int.TryParse(_fateNpcRowData[mFateNpcRandomPopRangeIndex + z].GetCellValue(), out int _levelReferenceID))
                        {
                            break;
                        }

                        if (_levelReferenceID <= 0)
                        {
                            continue;
                        }

                        string _posContent = CommonUtil.GetPosInfoByLevelReferenceId(
                            _levelReferenceID,
                            mExcelSheetLevelReference,
                            true,
                            false,
                            true
                        );

                        int _targetIndex = mFatePopGroupRandomPopRange0Index + z + (i * mMonsterIDOffset);
                        //if (_fateID == 434)
                        //{
                        //    Console.WriteLine($"\t RefID : [{_levelReferenceID}] , 写入 index : [{_targetIndex}], 列名:[{CommonUtil.GetZM(_targetIndex)}]  数据：{_posContent}");
                        //}
                        fatePopGroupRowData[_targetIndex].SetCellValue(_posContent);
                    }
                }
            }

            mExcelSheetPopGroupInFateExcel?.SaveSheet();

            mExcelFate?.SaveFile();

            return true;
        }
    }
}
