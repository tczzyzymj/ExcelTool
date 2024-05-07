using OfficeOpenXml;

namespace ExcelTool
{
    public class ExcelFileData : FileDataBase
    {
        private ExcelPackage? mExcelPackage; // 原始数据如果是

        public ExcelFileData()
        {
        }

        public ExcelFileData(string absolutePath, LoadFileType fileType) : base(absolutePath, fileType)
        {
        }

        ~ExcelFileData()
        {
            CloseFile();
        }

        public ExcelPackage? GetExcelPackage()
        {
            return mExcelPackage;
        }

        public override void SaveFile()
        {
            mExcelPackage?.Save();
        }

        // 这里只先加载一下 sheet
        protected override bool InternalLoadFile(string absolutePath)
        {
            FileInfo _info = new FileInfo(absolutePath);

            if (!_info.Exists)
            {
                CommonUtil.ShowError($"路径 【{absolutePath}】不存在，请检查！");

                return false;
            }

            mExcelPackage               = new ExcelPackage(_info);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 这里要注意，里面说了和 .net 的版本有关，具体请跳转进去看一下
            ExcelWorksheets? _allSheets  = mExcelPackage.Workbook.Worksheets;
            int              _startIndex = 0;

            _startIndex = mExcelPackage.Compatibility.IsWorksheets1Based ? 1 : 0;

            for (int _i = _startIndex; _i < _allSheets.Count; ++_i)
            {
                ExcelWorksheet? _sheet        = _allSheets[_i];
                ExcelSheetData  _newSheetData = new ExcelSheetData();

                if (!_newSheetData.Init(new WeakReference<FileDataBase>(this),
                                        _sheet,
                                        _i - _startIndex,
                                        _i,
                                        _sheet.Name))
                {
                    continue;
                }

                mWorkSheetList.Add(_newSheetData);
            }

            return true;
        }

        public void ChooseWorkSheet(int indexValue)
        {
        }

        public void CloseFile()
        {
            Console.WriteLine("关闭了文件：" + GetFileAbsulotePath());
            mExcelPackage?.Dispose();
        }

        public override string GetFileName(bool isFull)
        {
            if (mExcelPackage == null)
            {
                return string.Empty;
            }

            if (isFull)
            {
                return mExcelPackage.File.FullName;
            }

            return mExcelPackage.File.Name;
        }
    }
}
