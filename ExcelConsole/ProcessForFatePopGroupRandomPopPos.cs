using ExcelTool;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConsole
{
    public class ProcessForFatePopGroupRandomPopPos : ProcessBase
    {
        private ExcelFileData? mExcelFateNpc = null;
        private ExcelSheetData? mExcelSheetFateNpc = null;

        private ExcelFileData? mExcelMonster = null;
        private ExcelSheetData? mExcelSheetMonster = null;

        private ExcelFileData? mExcelLevelReference = null;
        private ExcelSheetData? mExcelSheetLevelReference = null;

        private ExcelFileData? mExcelFate = null;
        private ExcelSheetData? mExcelSheetPopGroupInFateExcel = null;

        private bool InternalLoadFiles()
        {
            // 加载 FATE表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);

                // 重新加载一下 key
                {
                    mExcelSheetPopGroupInFateExcel = mExcelFate?.GetWorkSheetByIndex(6) as ExcelSheetData;
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
                }
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

            // 加载 LevelReference.xlsx 日文原本数据
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

            return true;
        }

        // 怪物表
        private static int mMonsterFateNpcIDIndex = CommonUtil.GetIndexByZM("I") - 1;
        private static int mMonsterIDIndex = CommonUtil.GetIndexByZM("F") - 1;

        // FATE创建物表
        private static int mFatePopGroupMonsterIDIndex = CommonUtil.GetIndexByZM("E") - 1;
        private static int mFatePopGroupRandomPopRange0Index = CommonUtil.GetIndexByZM("J") - 1;

        private static int mMonsterIDOffset = 15;

        // FateNpc随机位置 Index
        private static int mFateNpcRandomPopRangeIndex = CommonUtil.GetIndexByZM("F") - 1;

        public override bool Process()
        {
            if (!InternalLoadFiles())
            {
                return false;
            }

            // 遍历Fate创建物里面的所有
            var _fatePopGroupDataList = mExcelSheetPopGroupInFateExcel?.GetAllDataList();
            if (_fatePopGroupDataList == null || _fatePopGroupDataList.Count < 1)
            {
                throw new Exception("数据出错，mExcelSheetPopGroupInFateExcel.GetAllDataList()");
            }

            foreach (var fatePopGroupRowData in _fatePopGroupDataList)
            {
                int.TryParse(fatePopGroupRowData[0].GetCellValue(), out var _fateID);

                for (int i = 0; i <= mMonsterIDOffset; ++i)
                {
                    if (!int.TryParse(fatePopGroupRowData[mFatePopGroupMonsterIDIndex + i * mMonsterIDOffset].GetCellValue(), out var _monsterID))
                    {
                        continue;
                    }

                    // 这里是去找，看对应的 FateNpc 数据是否有随机位置

                    // 获取到 _monsterID 去 Monster表里面查找  FateNpcID
                    var _monsterRowData = mExcelSheetMonster?.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { mMonsterIDIndex },
                        new List<string> { _monsterID.ToString() }
                    );

                    if (_monsterRowData == null || _monsterRowData.Count < 1)
                    {
                        continue;
                    }

                    if (!int.TryParse(_monsterRowData[mMonsterFateNpcIDIndex].GetCellValue(), out var _fateNpcID))
                    {
                        continue;
                    }

                    if (_fateNpcID <= 0)
                    {
                        continue;
                    }

                    var _fateNpcRowData = mExcelSheetFateNpc?.GetRowCellDataByTargetKeysAndValus(
                        new List<int> { 0 },
                        new List<string> { _fateNpcID.ToString() }
                    );

                    if (_fateNpcRowData == null || _fateNpcRowData.Count < 1)
                    {
                        throw new Exception($"无法找到 FateNpc,ID是：{_fateNpcID}");
                    }

                    //if (_fateID == 434)
                    //{
                    //    Console.WriteLine($"查找 MonsterID : [{_monsterID}] , FateNpcID : [{_fateNpcID}]");
                    //}

                    for (int z = 0; z <= 7; ++z)
                    {
                        if (!int.TryParse(
                            _fateNpcRowData[mFateNpcRandomPopRangeIndex + z].GetCellValue(),
                            out var _levelReferenceID)
                        )
                        {
                            break;
                        }

                        if (_levelReferenceID <= 0)
                        {
                            continue;
                        }

                        var _posContent = CommonUtil.GetPosInfoByLevelReferenceID(
                            _levelReferenceID, mExcelSheetLevelReference, true, false, true
                        );
                        var _targetIndex = mFatePopGroupRandomPopRange0Index + z + i * mMonsterIDOffset;
                        //if (_fateID == 434)
                        //{
                        //    Console.WriteLine($"\t RefID : [{_levelReferenceID}] , 写入 index : [{_targetIndex}], 列名:[{CommonUtil.GetZM(_targetIndex)}]  数据：{_posContent}");
                        //}
                        fatePopGroupRowData[_targetIndex].WriteCellValue(_posContent);
                    }
                }
            }

            mExcelSheetPopGroupInFateExcel?.SaveSheet();

            mExcelFate?.SaveFile();

            return true;
        }
    }
}
