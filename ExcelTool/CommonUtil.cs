using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public static class CommonUtil
    {
        public static string GetZM(int indexValue)
        {
            string _columName = string.Empty;
            while (indexValue > 0)
            {
                var _module = (indexValue - 1) % 26;
                _columName = Convert.ToChar(65 + _module) + _columName;
                indexValue = (indexValue - _module) / 26;
            }
            return _columName;
        }

        // 这里要做下循环检测
        //// 检测看下是否安全，没有循环引用，主要是对比表格和sheet的名字
        //public static bool IsSafeNoCycleReferenceForKey(ChaineKeyData? keyChaine)
        //{
        //    if (keyChaine == null)
        //    {
        //        return false;
        //    }

        //    return false;
        //}

        //public static string GetKeyConnectFullInfo(ChaineKeyData? targetData)
        //{
        //    if (targetData == null)
        //    {
        //        return string.Empty;
        //    }

        //    var _result = string.Empty;

        //    return _result;
        //}
    }
}
