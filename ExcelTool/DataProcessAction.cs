using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public abstract class DataProcessActionBase
    {
        public CommonWorkSheetData? BindSheet = null;

        public List<KeyData> MatchKeyList = new List<KeyData>();

        public abstract string FindTargetValueAndProcess(List<CellValueData> matchValueList);

        public string MultiValueSplitSymbol = ",";
    }

    public class DataProcessActionForFindRowData : DataProcessActionBase
    {
        public List<DataProcessActionBase> ActionListAfterFindValues = new List<DataProcessActionBase>();

        public override string FindTargetValueAndProcess(List<CellValueData> matchValueList)
        {
            if (BindSheet == null)
            {
                throw new Exception($"{FindTargetValueAndProcess} 出错， BindSheet 为空");
            }

            if (ActionListAfterFindValues.Count < 1)
            {
                throw new Exception($"{FindTargetValueAndProcess} 出错， ActionListAfterFindValues 数量小于1");
            }

            if (MatchKeyList.Count < 1)
            {
                throw new Exception($"{FindTargetValueAndProcess} 出错， MatchKeyList 数量小于1");
            }

            if (MatchKeyList.Count != matchValueList.Count)
            {
                throw new Exception($"{FindTargetValueAndProcess} 出错，传入的 matchValueList 和 MatchKeyList 数量不匹配，请检查");
            }

            List<string> _resultList = new List<string>();

            var _rowData = BindSheet.GetRowDataByTargetKeysAndValus(MatchKeyList, matchValueList);
            if (_rowData == null)
            {
                return string.Empty;
            }

            for (int i = 0; i < ActionListAfterFindValues.Count; ++i)
            {
                var _tempValue = ActionListAfterFindValues[i].FindTargetValueAndProcess(_rowData);

                _resultList.Add(_tempValue);
            }

            if (_resultList.Count > 1)
            {
                StringBuilder _builder = new StringBuilder();

                for (int i = 0; i < _resultList.Count; ++i)
                {
                    _builder.Append(_resultList[i]);
                    if (i < _resultList.Count - 1)
                    {
                        _builder.Append(MultiValueSplitSymbol);
                    }
                }

                return _builder.ToString();
            }
            else
            {
                return _resultList[0];
            }
        }
    }

    /// <summary>
    /// 找其他的表然后使用其他 ACTION 替换的数据
    /// </summary>
    public class DataProcessActionForConnectReplace : DataProcessActionBase
    {
        public DataProcessActionForFindRowData TargetProcess = new DataProcessActionForFindRowData();

        public override string FindTargetValueAndProcess(List<CellValueData> rowData)
        {
            List<CellValueData> _matchValueList = new List<CellValueData>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                _matchValueList.Add(rowData[MatchKeyList[i].GetKeyColumIndexInList()]);
            }

            return TargetProcess.FindTargetValueAndProcess(_matchValueList);
        }
    }

    /// <summary>
    /// 直接返回值
    /// </summary>
    public class DataProcessActionForDirectReturn : DataProcessActionBase
    {
        public override string FindTargetValueAndProcess(List<CellValueData> rowData)
        {
            List<string> _resultList = new List<string>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                var _cell = rowData[MatchKeyList[i].GetKeyColumIndexInList()];
                _resultList.Add(_cell.GetCellValue());
            }

            if (_resultList.Count > 1)
            {
                StringBuilder _builder = new StringBuilder();

                for (int i = 0; i < _resultList.Count; ++i)
                {
                    _builder.Append(_resultList[i]);
                    if (i < _resultList.Count - 1)
                    {
                        _builder.Append(MultiValueSplitSymbol);
                    }
                }

                return _builder.ToString();
            }
            else
            {
                return _resultList[0];
            }
        }
    }

    public class DataProcessActionForReturnAsUEPos : DataProcessActionBase
    {
        public override string FindTargetValueAndProcess(List<CellValueData> rowData)
        {
            List<string> _resultList = new List<string>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                var _cell = rowData[MatchKeyList[i].GetKeyColumIndexInList()];
                var _tempCellValue = _cell.GetCellValue();
                float.TryParse(_tempCellValue, out var _floatValue);
                var _finalStr = ((int)(_floatValue * 100)).ToString();
                _resultList.Add(_finalStr);
            }

            if (_resultList.Count > 1)
            {
                StringBuilder _builder = new StringBuilder();

                for (int i = 0; i < _resultList.Count; ++i)
                {
                    _builder.Append(_resultList[i]);
                    if (i < _resultList.Count - 1)
                    {
                        _builder.Append(MultiValueSplitSymbol);
                    }
                }

                return _builder.ToString();
            }
            else
            {
                return _resultList[0];
            }
        }
    }

    public class DataProcessActionForReturnAsUERotateY : DataProcessActionBase
    {
        public override string FindTargetValueAndProcess(List<CellValueData> rowData)
        {
            List<string> _resultList = new List<string>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                var _cell = rowData[MatchKeyList[i].GetKeyColumIndexInList()];
                var _tempCellValue = _cell.GetCellValue();
                float.TryParse(_tempCellValue, out var _floatValue);
                var _finalStr = ((int)(180 / 3.1415926 * _floatValue)).ToString();
                _resultList.Add(_finalStr);
            }

            if (_resultList.Count > 1)
            {
                StringBuilder _builder = new StringBuilder();

                for (int i = 0; i < _resultList.Count; ++i)
                {
                    _builder.Append(_resultList[i]);
                    if (i < _resultList.Count - 1)
                    {
                        _builder.Append(MultiValueSplitSymbol);
                    }
                }

                return _builder.ToString();
            }
            else
            {
                return _resultList[0];
            }
        }
    }
}

