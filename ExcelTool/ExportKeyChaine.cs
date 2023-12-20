using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class ExportKeyChaine
    {
        public KeyData ExportKey;

        public SourceSheetData SourceSheet;
    }

    public class SourceSheetData
    {
        public CommonWorkSheetData CurrentSheet;

        public ChaineKeyData ConnectKey;
    }

    public class ChaineSheetData
    {
        public CommonWorkSheetData CurrentSheet;

        public List<ChaineKeyData> ConnectKeyDatList = new List<ChaineKeyData>();
    }

    public class ChaineKeyData
    {
        public KeyData CurrentKey;

        public ChaineSheetData ChaineSheet;
    }
}
