using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class ProcessActionAttribute : Attribute
    {
        public ProcessActionAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName
        {
            get;
            set;
        }
    }

    public abstract class DataProcessActionBase
    {
        public List<KeyData> MatchKeyList = new List<KeyData>();

        public abstract string TryProcessData(List<CellValueData> inRowData);

        public string MultiValueSplitSymbol = ",";
    }

    [ProcessAction("源行为")]
    public class SourceAction : DataProcessActionBase
    {
        public List<DataProcessActionBase> ActionList = new List<DataProcessActionBase>();

        public override string TryProcessData(List<CellValueData> rowValue)
        {
            List<string> _resultList = new List<string>();

            for (int i = 0; i < ActionList.Count; ++i)
            {
                _resultList.Add(ActionList[i].TryProcessData(rowValue));
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
    /// 直接返回值
    /// </summary>
    [ProcessAction("直接返回")]
    public class DataProcessActionForDirectReturn : DataProcessActionBase
    {
        public override string TryProcessData(List<CellValueData> rowData)
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

    [ProcessAction("跨表查找")]
    public class DataProcessActionForFindRowDataInOtherSheet : DataProcessActionBase
    {
        public CommonWorkSheetData SearchTargetSheet;

        public List<DataProcessActionBase> ActionListAfterFindValues = new List<DataProcessActionBase>();

        public List<KeyData> SearchKeyList = new List<KeyData>();

        public override string TryProcessData(List<CellValueData> inRowData)
        {
            if (ActionListAfterFindValues.Count < 1)
            {
                throw new Exception($"{TryProcessData} 出错， ActionListAfterFindValues 数量小于1");
            }

            if (MatchKeyList.Count < 1)
            {
                throw new Exception($"{TryProcessData} 出错， MatchKeyList 数量小于1");
            }
            if (SearchKeyList.Count < 1)
            {
                throw new Exception($"{TryProcessData} 出错，查找表格的列没有数据，请检查");
            }

            List<CellValueData> _searchInCellList = new List<CellValueData>();

            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                _searchInCellList.Add(inRowData[MatchKeyList[i].GetKeyColumIndexInList()]);
            }

            List<string> _resultList = new List<string>();

            var _rowData = SearchTargetSheet.GetRowDataByTargetKeysAndValus(MatchKeyList, _searchInCellList);
            if (_rowData == null)
            {
                return string.Empty;
            }

            for (int i = 0; i < ActionListAfterFindValues.Count; ++i)
            {
                var _tempValue = ActionListAfterFindValues[i].TryProcessData(_rowData);

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

    [ProcessAction("返回为UEPos")]
    public class DataProcessActionForReturnAsUEPos : DataProcessActionBase
    {
        public override string TryProcessData(List<CellValueData> rowData)
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

    [ProcessAction("返回为UE旋转")]
    public class DataProcessActionForReturnAsUERotateY : DataProcessActionBase
    {
        public override string TryProcessData(List<CellValueData> rowData)
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

