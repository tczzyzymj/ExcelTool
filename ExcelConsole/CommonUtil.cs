using System.Reflection;
using System.Text;

namespace ExcelTool
{
    public static class CommonUtil
    {
        private static readonly int mLevelReferenceTranXIndex   = GetIndexByZm("G", 2);
        private static readonly int mLevelReferenceTranYIndex   = mLevelReferenceTranXIndex   + 1;
        private static readonly int mLevelReferenceTranZIndex   = mLevelReferenceTranYIndex   + 1;
        private static readonly int mLevelReferenceRotateYIndex = mLevelReferenceTranZIndex   + 1;
        private static readonly int mLevelRangeOnMapIndex       = mLevelReferenceRotateYIndex + 1;

        public static List<string> ParsRowCellDataToRowStringData(List<CellValueData> inDataList)
        {
            List<string> _result = new List<string>();

            if ((inDataList != null) && (inDataList.Count > 0))
            {
                foreach (CellValueData _cell in inDataList)
                {
                    _result.Add(_cell.GetCellValue());
                }
            }

            return _result;
        }

        public static string GetPosInfoByLevelReferenceId(
            int             levelReferenceID,
            ExcelSheetData? fateLevelReferenceExcelSheet,
            bool            errorThrowException,
            bool            isTheFourthZero,
            bool            isTheFourthRange // 第四位是否为range，如果不是，那么取Rotate
        )
        {
            if (fateLevelReferenceExcelSheet == null)
            {
                throw new Exception("传入的 ExcelSheetData? fateLevelReferenceExcelSheet 为空，请检查!");
            }

            List<CellValueData>? _levelReferenceRowDataList = fateLevelReferenceExcelSheet.GetRowCellDataByTargetKeysAndValues(
                new List<int> { 0 },
                new List<string> { levelReferenceID.ToString() }
            );

            if (_levelReferenceRowDataList == null)
            {
                if (errorThrowException)
                {
                    throw new Exception($"错误，无法获取 LevelReference 数据，ID是：{levelReferenceID}，请检查");
                }

                return string.Empty;
            }

            return InternalGetPosByLevelReferenceRowData(_levelReferenceRowDataList, isTheFourthZero, isTheFourthRange);
        }

        private static string InternalGetPosByLevelReferenceRowData(List<CellValueData> rowData, bool isTheFourthZero, bool isTheFourthRange)
        {
            int _posX = ConvertToUEPos(rowData[mLevelReferenceTranXIndex].GetCellValue());
            int _posZ = ConvertToUEPos(rowData[mLevelReferenceTranZIndex].GetCellValue());
            int _posY = ConvertToUEPos(rowData[mLevelReferenceTranYIndex].GetCellValue());

            if (isTheFourthZero)
            {
                string _result = $"{_posX},{_posZ},{_posY},0";

                return _result;
            }

            if (isTheFourthRange)
            {
                int    _theFourthValue = ConvertToUEPos(rowData[mLevelRangeOnMapIndex].GetCellValue());
                string _result         = $"{_posX},{_posZ},{_posY},{_theFourthValue}";

                return _result;
            }
            else
            {
                int    _theFourthValue = ConvertToUERotate(rowData[mLevelReferenceRotateYIndex].GetCellValue());
                string _result         = $"{_posX},{_posZ},{_posY},{_theFourthValue}";

                return _result;
            }
        }

        public static int ConvertToUEPos(string strValue)
        {
            int _result = 0;

            if (!float.TryParse(strValue, out float _floatValue))
            {
                return _result;
            }

            _result = (int)(_floatValue * 100);

            return _result;
        }

        public static int ConvertToUERotate(string strValue)
        {
            int _result = 0;

            if (!float.TryParse(strValue, out float _floatValue))
            {
                return _result;
            }

            _result = (int)((180 / 3.1415926) * _floatValue);

            return _result;
        }

        public static string ConvertListIntToString(List<int> intList, string spliSymbol)
        {
            StringBuilder _sb = new StringBuilder();

            if ((intList == null) || (intList.Count < 1))
            {
                return string.Empty;
            }

            for (int i = 0; i < intList.Count; ++i)
            {
                _sb.Append(intList[i].ToString());

                if (i < (intList.Count - 1))
                {
                    _sb.Append(spliSymbol);
                }
            }

            return _sb.ToString();
        }

        public static List<int>? ParsKeyDataToInexInDataList(List<KeyData> inKeyDataList)
        {
            if ((inKeyDataList == null) || (inKeyDataList.Count < 1))
            {
                return null;
            }

            List<int> _result = new List<int>();

            foreach (KeyData _singleKey in inKeyDataList)
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

            Console.WriteLine(content);
        }

        public static string GetZM(int indexValue)
        {
            string _columName = string.Empty;

            while (indexValue > 0)
            {
                int _module = (indexValue      - 1) % 26;
                _columName = Convert.ToChar(65 + _module) + _columName;
                indexValue = (indexValue - _module) / 26;
            }

            return _columName;
        }

        /// <summary>
        /// </summary>
        /// <param name="inValue">传入如 "a" , 解析为1</param>
        /// <param name="indexOffset">下标的偏移,如A会被解析为1，但是在list里面其实是0，那么就传入1即可</param>
        /// <returns></returns>
        public static int GetIndexByZm(string inValue, int indexOffset)
        {
            if (string.IsNullOrEmpty(inValue))
            {
                return -1;
            }

            char[] _chrs   = inValue.ToUpper().ToCharArray(); // 转为大写字母组成的 char数组
            int    _length = _chrs.Length;
            int    _index  = 0;

            for (int _i = 0; _i < _length; _i++)
            {
                _index += ((_chrs[_i] - 'A') + 1) * (int)Math.Pow(26, _length - _i - 1); // 当做26进制来算 AAA=111 26^2+26^1+26^0
            }

            int _finalValue = _index - indexOffset;

            if (_finalValue < 0)
            {
                Console.WriteLine($"传入值 : {inValue} , 解析为 : {_index} , 偏移值 : {indexOffset} , 小于0，请检查");
                _finalValue = 0;
            }

            return _finalValue;
        }

        public static List<CommonDataForComboBox> CreateComboBoxDataForType<T>() where T : class
        {
            List<CommonDataForComboBox> _result = new List<CommonDataForComboBox>();

            Type[]     _types      = Assembly.GetExecutingAssembly().GetTypes();
            List<Type> _recordList = new List<Type>();

            Type _targetType = typeof(T);

            foreach (Type _pair in _types)
            {
                if (_pair.IsSubclassOf(_targetType))
                {
                    CommonDataForComboBox _newData = new CommonDataForComboBox();
                    _newData.Index      = _recordList.Count;
                    _newData.TargetType = _pair;
                    DisplayNameAttribute? _attribute = _pair.GetCustomAttribute<DisplayNameAttribute>();

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

            Type        _targetType = typeof(T);
            FieldInfo[] _allFiels   = _targetType.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo _singleEnum in _allFiels)
            {
                object? _tempValue = _singleEnum.GetValue(null);

                if (_tempValue == null)
                {
                    ShowError($"枚举获取值出错：[{_singleEnum}]");

                    continue;
                }

                CommonDataForComboBox _newData = new CommonDataForComboBox();
                _newData.Index      = _result.Count;
                _newData.RealValue  = (int)_tempValue;
                _newData.TargetType = typeof(T);
                DisplayNameAttribute? _attribute = _singleEnum.GetCustomAttribute<DisplayNameAttribute>();

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
