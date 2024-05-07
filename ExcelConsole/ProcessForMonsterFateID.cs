using ExcelTool;

namespace ExcelConsole
{
    public class ProcessForMonsterFateID : ProcessBase
    {
        private ExcelFileData?  mExcelFate;
        private ExcelSheetData? mExcelSheetPopGroupInFateExcel;

        private ExcelFileData?  mExcelMonster;
        private ExcelSheetData? mExcelSheetMonster;

        private static readonly int mFatePopGroupFirstMonsterIDIndex = CommonUtil.GetIndexByZm("E", 1);
        private static readonly int mFatePopGroupFirstNpcIDIndex     = CommonUtil.GetIndexByZm("IK", 1);

        public override bool Process()
        {
            // 加载文件
            {
                // 加载 FATE表.xlsx
                {
                    string _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                    mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                    if (mExcelFate == null)
                    {
                        throw new Exception("错误，加载 FATE表.xlsx 数据出错");
                    }

                    // 重新加载一下 key

                    mExcelSheetPopGroupInFateExcel = mExcelFate?.GetWorkSheetByIndex(6) as ExcelSheetData;

                    if (mExcelSheetPopGroupInFateExcel == null)
                    {
                        throw new Exception("无法获取 mFateFile.GetWorkSheetByIndex(6)");
                    }

                    List<KeyData> _keyListData = mExcelSheetPopGroupInFateExcel.GetKeyListData();

                    for (int i = 0; i < _keyListData.Count; ++i)
                    {
                        _keyListData[i].IsMainKey = false;
                    }

                    _keyListData[0].IsMainKey = true;

                    mExcelSheetPopGroupInFateExcel.LoadAllCellData(true);
                }
                // END

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
                //END
            }

            // key : monster id ; value fate id
            Dictionary<int, int> _monsterIDToFateIDMap = new Dictionary<int, int>();

            List<List<CellValueData>>? _allDataList = mExcelSheetPopGroupInFateExcel.GetAllDataList();

            if ((_allDataList == null) || (_allDataList.Count < 1))
            {
                throw new Exception("mExcelSheetPopGroupInFateExcel.GetAllDataList() 获取数据错误");
            }

            for (int i = 0; i < _allDataList.Count; ++i)
            {
                List<CellValueData> _rowData = _allDataList[i];

                // 这里是MONSTER的
                for (int _index = 0; _index <= 15; ++_index)
                {
                    int _offset = _index * 15;
                    int.TryParse(_rowData[mFatePopGroupFirstMonsterIDIndex + _offset].GetCellValue(), out int _monsterID);

                    if (_monsterID <= 0)
                    {
                        continue;
                    }

                    List<CellValueData>? _monsterRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_monsterID.ToString());

                    if ((_monsterRowData == null) || (_monsterRowData.Count < 1))
                    {
                        Console.WriteLine($"无法找到 Monster 数据，ID是：[{_monsterID}]");

                        continue;
                    }

                    int.TryParse(_rowData[0].GetCellValue(), out int _fateID);

                    _monsterIDToFateIDMap[_monsterID] = _fateID;
                }

                // 这里是 Npc 的
                for (int _index = 0; _index <= 15; ++_index)
                {
                    int _offset = _index * 6;
                    int.TryParse(_rowData[mFatePopGroupFirstNpcIDIndex + _offset].GetCellValue(), out int _monsterID);

                    if (_monsterID <= 0)
                    {
                        continue;
                    }

                    List<CellValueData>? _monsterRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_monsterID.ToString());

                    if ((_monsterRowData == null) || (_monsterRowData.Count < 1))
                    {
                        Console.WriteLine($"无法找到 Monster 数据，ID是：[{_monsterID}]");

                        continue;
                    }

                    int.TryParse(_rowData[0].GetCellValue(), out int _fateID);

                    _monsterIDToFateIDMap[_monsterID] = _fateID;
                }
            }

            foreach (KeyValuePair<int, int> _pair in _monsterIDToFateIDMap)
            {
                List<CellValueData>? _monsterRowData = mExcelSheetMonster.GetCacheRowDataListByKeyStr(_pair.Key.ToString());

                if ((_monsterRowData == null) || (_monsterRowData.Count < 1))
                {
                    Console.WriteLine($"无法找到 Monster 数据，ID是：[{_pair.Key}]");

                    continue;
                }

                _monsterRowData[1].SetCellValue(_pair.Value.ToString());
            }

            mExcelSheetMonster.SaveSheet();

            mExcelMonster.SaveFile();

            return true;
        }
    }
}
