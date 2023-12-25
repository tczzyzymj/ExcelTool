using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class CommonDataForClass
    {
        public Type TargetType;

        public int Index
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }

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

        public static List<CommonDataForClass> CreateComboBoxDataForType<T>() where T : class
        {
            List<CommonDataForClass> _result = new List<CommonDataForClass>();

            var _types = Assembly.GetExecutingAssembly().GetTypes();
            List<Type> _recordList = new List<Type>();

            var _targetType = typeof(T);
            var _ignoreType = typeof(SourceAction);
            foreach (var _pair in _types)
            {
                if (_pair == _ignoreType)
                {
                    continue;
                }
                if (_pair.BaseType == _targetType)
                {
                    var _newData = new CommonDataForClass();
                    _newData.Index = _recordList.Count;
                    _newData.TargetType = _pair;
                    var _attribute = _pair.GetCustomAttribute<ProcessActionAttribute>();
                    if (_attribute != null)
                    {
                        _newData.Name = _attribute.DisplayName;
                    }

                    _result.Add(_newData);
                }
            }

            return _result;
        }
    }
}
