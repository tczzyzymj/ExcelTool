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
        public Type? TargetType = null;

        public int Index
        {
            get; set;
        }

        public string Name
        {
            get; set;
        } = string.Empty;
    }


    public static class CommonUtil
    {
        public static ActionCore? GetNewDataForChooseAction(CommonDataForClass mChooseActionType)
        {
            ActionCore? _result = null;

            if (mChooseActionType == null)
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，mChooseActionType为空，请检查");
                return null;
            }

            if (mChooseActionType.TargetType == null)
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，mChooseActionType.TargetType 为空，请检查");
                return null;
            }

            var _className = mChooseActionType.TargetType.FullName;

            if (string.IsNullOrEmpty(_className))
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，mChooseActionType.TargetType.FullName 为空，请检查");
                return null;
            }

            _result = Assembly.GetExecutingAssembly().CreateInstance(_className, true) as ActionCore;

            if (_result == null)
            {
                MessageBox.Show($"{GetNewDataForChooseAction} 错误，实例化失败，请检查");

                return null;
            }

            return _result;
        }

        public static List<string> ParsRowCellDataToRowStringData(List<CellValueData> inDataList)
        {
            List<string> _result = new List<string>();
            if (inDataList != null && _result.Count > 0)
            {
                foreach (var _cell in inDataList)
                {
                    _result.Add(_cell.GetCellValue());
                }
            }

            return _result;
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
                MessageBox.Show(content, "错误");
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

        public static List<CommonDataForClass> CreateComboBoxDataForType<T>() where T : class
        {
            List<CommonDataForClass> _result = new List<CommonDataForClass>();

            var _types = Assembly.GetExecutingAssembly().GetTypes();
            List<Type> _recordList = new List<Type>();

            var _targetType = typeof(T);
            var _ignoreType = typeof(ActionCore);
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
