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
        private CSVFileData mFateCSVFile = null;
        private CSVSheetData mFateCSVSheet = null;

        private ExcelFileData mFateExcelFile = null;
        private ExcelSheetData mFatePopGroupExcelSheet = null;
        private ExcelSheetData mFateGuardExcelSheet = null;

        private static int mNpcResID = CommonUtil.GetIndexByZM("DM") - 1;

        public override bool Process()
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

            var _allFateCSVDataList = mFateGuardExcelSheet.GetAllDataList();
            if (_allFateCSVDataList == null || _allFateCSVDataList.Count < 1)
            {
                throw new Exception($"mFateCSVSheet.GetAllDataList() 错误，请检查");
            }

            List<int> _fateIDList = new List<int>();
            foreach (var _singeRowData in _allFateCSVDataList)
            {
                if (int.TryParse(_singeRowData[0].GetCellValue(), out var _fateID))
                {
                    _fateIDList.Add(_fateID);
                }
            }

            return true;
        }
    }
}
