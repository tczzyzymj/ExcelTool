using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    internal class MainTypeDefine
    {
        public enum ExportWriteWayType
        {
            Append, // 以追加的方式
            OverWriteAll, // 新写入的方式，旧数据全部放弃
        }

        public enum ExportConfigDealWayType
        {
            UseOldData, // 使用旧数据
            UseNewData, // 使用新数据
        }
    }
}
