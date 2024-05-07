using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelTool;

namespace ExcelConsole
{
    public class ProcessForCreateNewColumForFatePopGroup : ProcessBase
    {
        private ExcelFileData? mExcelFate = null;
        private static int mPopRangeIndex = CommonUtil.GetIndexByZm("J", 0); // 这里是sheetindex 不要 -1

        private bool InternalLoadFiles()
        {
            // 加载 FATE表.xlsx
            {
                var _tempPath = Path.Combine(FolderPath, "FATE表.xlsx");
                mExcelFate = new ExcelFileData(_tempPath, LoadFileType.NormalFile);
            }

            return true;
        }

        public override bool Process()
        {
            if (!InternalLoadFiles())
            {
                return false;
            }

            var _package = mExcelFate?.GetExcelPackage();
            if (_package == null)
            {
                throw new Exception("获取 Package 为空，请检查");
            }

            var _sheet = _package.Workbook.Worksheets[6];

            // 这里是怪物的
            for (int j = 0; j <= 15; ++j)
            {
                var _startIndex = mPopRangeIndex + j * 15;
                for (int i = 0; i < 8; ++i)
                {
                    _sheet.Cells[2, _startIndex + i].Value = $"Monsters[{j}].RandomPopRange[{i}].Range";
                }
            }

            mExcelFate?.SaveFile();

            return true;
        }
    }
}
