using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    /// <summary>
    /// 这里注意 
    /// </summary>
    public class ChaineKeyData
    {
        public WeakReference<ChaineKeyData>? OwnerChaineKeyData = null;

        /// <summary>
        /// 当前KEY
        /// </summary>
        public KeyData? MainKey = null;

        /// <summary>
        /// 当前KEY所关联的内容
        /// </summary>
        public List<CellValueData> MainCellValue = new List<CellValueData>();

        /// <summary>
        /// 关联哪个表
        /// </summary>
        public CommonWorkSheetData? ConnectWorkSheet = null;

        /// <summary>
        /// 关联表里的哪些KEY
        /// </summary>
        public List<ChaineKeyData> ConnectSheetKeyList = new List<ChaineKeyData>();
    }
}
