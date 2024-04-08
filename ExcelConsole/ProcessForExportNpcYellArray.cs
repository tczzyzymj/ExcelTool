using ExcelTool;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConsole
{
    public class ProcessForExportNpcYellArray : ProcessBase
    {
        private class FateArrayNpcYellData
        {
            public int ID;
            public int YellInterval; // 最小间隔
            public int YellIntervalMax; // 最大间隔
            public List<int> NpcYellIDList = new List<int>();
        }

        private ExcelFileData? mExcelFateArrayNpcYell;
        private ExcelSheetData? mExcelSheetFateArrayNpcYell;

        private ExcelFileData? mExcelNpcYell;
        private ExcelSheetData? mExcelSheetNpcYell;

        private ExcelFileData? mExporFile = null;

        public static int mMaxYellCount = 8;

        private List<FateArrayNpcYellData> mWriteDataList = new List<FateArrayNpcYellData>();

        private static int mIDIndex = CommonUtil.GetIndexByZM("B") - 2;
        private static int mYellIntervalIndex = CommonUtil.GetIndexByZM("E") - 2;
        private static int mYellIntervalMaxIndex = CommonUtil.GetIndexByZM("F") - 2;
        private static int mYellFirstIndex = CommonUtil.GetIndexByZM("G") - 2;

        public override bool Process()
        {
            // 加载文件
            {
                // FateArrayNpcYell 端游表 数据加载
                {
                    var _tempPath = Path.Combine(FolderPath, "FateArrayNpcYell.xlsx");
                    mExcelFateArrayNpcYell = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetFateArrayNpcYell = mExcelFateArrayNpcYell.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetFateArrayNpcYell == null)
                    {
                        throw new Exception("无法获取 sheet 3");
                    }

                    mExcelSheetFateArrayNpcYell.SetKeyStartRowIndexInSheet(5);
                    mExcelSheetFateArrayNpcYell.SetKeyStartColmIndexInSheet(2);
                    mExcelSheetFateArrayNpcYell.SetContentStartRowIndexInSheet(11);

                    mExcelSheetFateArrayNpcYell.LoadAllCellData(true);
                }
                // end

                // NpcYell 端游表 数据加载
                {
                    var _tempPath = Path.Combine(FolderPath, "NpcYell.xlsx");
                    mExcelNpcYell = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                    mExcelSheetNpcYell = mExcelNpcYell.GetWorkSheetByIndex(0) as ExcelSheetData;

                    if (mExcelSheetNpcYell == null)
                    {
                        throw new Exception("无法获取 sheet 3");
                    }

                    var _keyListData = mExcelSheetNpcYell.GetKeyListData();
                    _keyListData[4].IsMainKey = true;

                    mExcelSheetNpcYell.SetKeyStartRowIndexInSheet(5);
                    mExcelSheetNpcYell.SetKeyStartColmIndexInSheet(2);
                    mExcelSheetNpcYell.SetContentStartRowIndexInSheet(11);

                    mExcelSheetNpcYell.LoadAllCellData(true);
                }
                //end 

                // 导出目标表
                {
                    var _tempPath = Path.Combine(FolderPath, "Q气泡对话组.xlsx");
                    mExporFile = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
                }
            }

            var _allDataList = mExcelSheetFateArrayNpcYell.GetAllDataList();
            if (_allDataList == null || _allDataList.Count < 1)
            {
                throw new Exception($"mExcelSheetFateArrayNpcYell.GetAllDataList(); 获取数据出错");
            }

            foreach (var _data in _allDataList)
            {
                int.TryParse(_data[mIDIndex].GetCellValue(), out var _id);
                if (_id <= 0)
                {
                    continue;
                }

                var _newData = new FateArrayNpcYellData();
                mWriteDataList.Add(_newData);
                _newData.ID = _id;

                int.TryParse(_data[mYellIntervalIndex].GetCellValue(), out _newData.YellInterval);
                int.TryParse(_data[mYellIntervalMaxIndex].GetCellValue(), out _newData.YellIntervalMax);

                for (int i = 0; i < mMaxYellCount; ++i)
                {
                    var _searchStr = _data[mYellFirstIndex + i].GetCellValue();
                    if (string.IsNullOrEmpty(_searchStr))
                    {
                        continue;
                    }

                    var _seachRowData = mExcelSheetNpcYell.GetCacheRowDataListByKeyStr(_searchStr);
                    if (_seachRowData == null || _seachRowData.Count < 1)
                    {
                        throw new Exception($"获取数据错误，查找数据是:[{_searchStr}], FateArrayNpcYell_ID:[{_id}]");
                    }

                    int.TryParse(_seachRowData[0].GetCellValue(), out var _npcYellID);
                    if (_npcYellID > 0)
                    {
                        if (!_newData.NpcYellIDList.Contains(_npcYellID))
                        {
                            _newData.NpcYellIDList.Add(_npcYellID);
                        }
                    }
                }
            }

            // 写入
            {
                const int _rowOffset = 4;
                var _sheet = mExporFile.GetExcelPackage()?.Workbook.Worksheets[0];
                if (_sheet == null)
                {
                    throw new Exception($"错误，无法获取 WorkSheets");
                }

                _sheet.Cells[2, 1].Value = "ID";
                _sheet.Cells[3, 1].Value = "ID";
                _sheet.Cells[2, 2].Value = "YellInterval";
                _sheet.Cells[3, 2].Value = "最小间隔";
                _sheet.Cells[2, 3].Value = "YellIntervalMax";
                _sheet.Cells[3, 3].Value = "最大间隔";

                for (int i = 0; i < mMaxYellCount; ++i)
                {
                    _sheet.Cells[2, 4 + i].Value = $"NpcYellArray[{i}]";
                    _sheet.Cells[3, 4 + i].Value = $"去Q气泡表查找";
                }

                for (int i = 0; i < mWriteDataList.Count; ++i)
                {
                    _sheet.Cells[i + _rowOffset, 1].Value = mWriteDataList[i].ID.ToString();
                    _sheet.Cells[i + _rowOffset, 2].Value = mWriteDataList[i].YellInterval.ToString();
                    _sheet.Cells[i + _rowOffset, 3].Value = mWriteDataList[i].YellIntervalMax.ToString();

                    for (int j = 0; j < mWriteDataList[i].NpcYellIDList.Count; ++j)
                    {
                        _sheet.Cells[i + _rowOffset, 4 + j].Value = mWriteDataList[i].NpcYellIDList[j].ToString();
                    }
                }
            }

            mExporFile?.SaveFile();

            return true;
        }
    }
}
