using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public static class CommonUtil
    {
        public static List<string> ParsRowCellDataToRowStringData(List<CellValueData> inDataList)
        {
            List<string> _result = new List<string>();
            if (inDataList != null && inDataList.Count > 0)
            {
                foreach (var _cell in inDataList)
                {
                    _result.Add(_cell.GetCellValue());
                }
            }

            return _result;
        }

        public static string ConverListIntToString(List<int> intList, string spliSymbol)
        {
            StringBuilder _sb = new StringBuilder();

            if (intList == null || intList.Count < 1)
            {
                return string.Empty;
            }

            for (int i = 0; i < intList.Count; ++i)
            {
                _sb.Append(intList[i].ToString());

                if (i < intList.Count - 1)
                {
                    _sb.Append(spliSymbol);
                }
            }

            return _sb.ToString();
        }

        public static List<int>? ParsKeyDataToInexInDataList(List<KeyData> inKeyDataList)
        {
            if (inKeyDataList == null || inKeyDataList.Count < 1)
            {
                return null;
            }

            List<int> _result = new List<int>();
            foreach (var _singleKey in inKeyDataList)
            {
                _result.Add(_singleKey.KeyIndexInList);
            }

            return _result;
        }

        public static void ShowError(string content, bool throwException = false)
        {
            if (throwException)
            {
                throw new Exception(content);
            }
            else
            {
                Console.WriteLine(content);
            }
        }

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

        public static int GetIndexByZM(string col)
        {
            if (col == null)
            {
                return -1;
            }
            char[] chrs = col.ToUpper().ToCharArray(); // 转为大写字母组成的 char数组
            int length = chrs.Length;
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                index += (chrs[i] - 'A' + 1) * (int)(Math.Pow(26, length - i - 1)); // 当做26进制来算 AAA=111 26^2+26^1+26^0
            }
            return index;// 从0开始的下标
        }

        public static List<CommonDataForComboBox> CreateComboBoxDataForType<T>() where T : class
        {
            List<CommonDataForComboBox> _result = new List<CommonDataForComboBox>();

            var _types = Assembly.GetExecutingAssembly().GetTypes();
            List<Type> _recordList = new List<Type>();

            var _targetType = typeof(T);
            foreach (var _pair in _types)
            {
                if (_pair.IsSubclassOf(_targetType))
                {
                    var _newData = new CommonDataForComboBox();
                    _newData.Index = _recordList.Count;
                    _newData.TargetType = _pair;
                    var _attribute = _pair.GetCustomAttribute<DisplayNameAttribute>();
                    if (_attribute != null)
                    {
                        _newData.DisplayName = _attribute.DisplayName;
                        _result.Add(_newData);
                    }
                }
            }

            return _result;
        }

        public static List<CommonDataForComboBox> CreateComboBoxDataForEnum<T>() where T : Enum
        {
            List<CommonDataForComboBox> _result = new List<CommonDataForComboBox>();

            var _targetType = typeof(T);
            var _allFiels = _targetType.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var _singleEnum in _allFiels)
            {
                var _tempValue = _singleEnum.GetValue(null);
                if (_tempValue == null)
                {
                    CommonUtil.ShowError($"枚举获取值出错：[{_singleEnum}]");
                    continue;
                }

                var _newData = new CommonDataForComboBox();
                _newData.Index = _result.Count;
                _newData.RealValue = (int)(_tempValue);
                _newData.TargetType = typeof(T);
                var _attribute = _singleEnum.GetCustomAttribute<DisplayNameAttribute>();
                if (_attribute != null)
                {
                    _newData.DisplayName = _attribute.DisplayName;
                }

                _result.Add(_newData);
            }

            return _result;
        }
    }
}
