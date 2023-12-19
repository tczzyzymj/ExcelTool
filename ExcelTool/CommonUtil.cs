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

        // 检测看下是否安全，没有循环引用，主要是对比表格和sheet的名字
        public static bool IsSafeNoCycleReferenceForKey(KeyData targetData)
        {
            if (targetData == null)
            {
                return false;
            }

            List<KeyData> _list = new List<KeyData>()
            {
                targetData
            };

            var _nextKey = targetData.GetNextConnectKey();

            // 这里不用 while 是怕已经形成循环引用了
            for (int i = 0; i < 100; ++i)
            {
                if (_nextKey != null)
                {
                    _list.Add(_nextKey);
                }
                else
                {
                    break;
                }

                _nextKey = _nextKey.GetNextConnectKey();
            }

            for (int i = 0; i < _list.Count; ++i)
            {
                var _curTableName = _list[i].GetOwnerTableName(true);
                var _curSheetName = _list[i].GetOwnerSheetName();

                for (int j = i + 1; j < _list.Count; ++j)
                {
                    var _itTableName = _list[j].GetOwnerTableName(true);
                    var _itSheetName = _list[j].GetOwnerSheetName();

                    if ((!string.IsNullOrEmpty(_curTableName) && _curTableName.Equals(_itTableName)) &&
                        (!string.IsNullOrEmpty(_curSheetName) && _curSheetName.Equals(_itSheetName)))
                    {
                        var _content = string.Format(
                            "有循环引用，头Key:[{0}]>表：[{1}]>sheet[{2}] ， 出错位置1：头Key:[{3}]>表：[{4}]>sheet[{5}] ; 出错位置2:头Key:[{6}]>表：[{7}]>sheet[{8}]",
                            targetData.GetKeyName(),
                            targetData.GetOwnerTableName(false),
                            targetData.GetOwnerSheetName(),
                            _list[i].GetKeyName(),
                            _list[i].GetOwnerTableName(false),
                            _list[i].GetOwnerSheetName(),
                            _list[j].GetKeyName(),
                            _list[j].GetOwnerTableName(false),
                            _list[j].GetOwnerSheetName()
                        );

                        MessageBox.Show(_content, "错误");

                        return false;
                    }
                }
            }

            return true;
        }

        public static string GetKeyConnectFullInfo(KeyData targetData)
        {
            if (targetData == null)
            {
                return string.Empty;
            }

            var _result = targetData.GetConnectInfo();

            for (int i = 0; i < 1000; ++i)
            {

            }

            return _result;
        }
    }
}
