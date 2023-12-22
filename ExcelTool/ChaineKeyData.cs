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

        public CellValueData? MainCellValue = null;

        /// <summary>
        /// 关联哪个表
        /// </summary>
        public CommonWorkSheetData? ConnectWorkSheet = null;

        /// <summary>
        /// 关联表里的哪些KEY，和下面的VALUE是匹配的
        /// </summary>
        public List<ChaineKeyData> ConnectDataSourceKeyList = new List<ChaineKeyData>();

        /// <summary>
        /// 关联KEY所指向的内容，和KEY是匹配的
        /// </summary>
        public List<CellValueData> ConnectCellValueList = new List<CellValueData>();

        /// <summary>
        /// 关联目标的过滤方法
        /// </summary>
        public Dictionary<int, List<FilterFuncBase>> ConnectWorkSheetFilterMap
        {
            get;
            set;
        } = new Dictionary<int, List<FilterFuncBase>>();
    }
}
