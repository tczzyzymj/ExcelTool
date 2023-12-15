using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    // 这里是最基本的类，后续自己分一下
    public abstract class TableBaseData
    {
        public bool DoLoadFile(string absolutePath)
        {
            return InternalLoadFile(absolutePath);
        }

        public abstract bool InternalLoadFile(string absolutePath);
    }
}
