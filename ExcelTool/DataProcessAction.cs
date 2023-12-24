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

        public abstract List<string> FindTargetValueAndProcess(List<CellValueData> matchValueList);
    }

    public class DataProcessActionForFindRowData : DataProcessActionBase
    {
        public List<DataProcessActionBase> ActionListAfterFindValues = new List<DataProcessActionBase>();

        public override List<string> FindTargetValueAndProcess(List<CellValueData> matchValueList)
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

            List<string> _result = new List<string>();

            var _rowData = BindSheet.GetRowDataByTargetKeysAndValus(MatchKeyList, matchValueList);
            if (_rowData == null)
            {
                return _result;
            }

            for (int i = 0; i < ActionListAfterFindValues.Count; ++i)
            {
                var _tempValue = ActionListAfterFindValues[i].FindTargetValueAndProcess(_rowData);

                _result.AddRange(_tempValue);
            }

            return _result;
        }
    }

    /// <summary>
    /// 找其他的然后代替
    /// </summary>
    public class DataProcessActionForConnectReplace : DataProcessActionBase
    {
        public DataProcessActionForFindRowData TargetProcess = new DataProcessActionForFindRowData();

        public override List<string> FindTargetValueAndProcess(List<CellValueData> rowData)
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
        public override List<string> FindTargetValueAndProcess(List<CellValueData> rowData)
        {
            List<string> _result = new List<string>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                var _cell = rowData[MatchKeyList[i].GetKeyColumIndexInList()];
                _result.Add(_cell.GetCellValue());
            }
            return _result;
        }
    }

    public class DataProcessActionForReturnAsUEPos : DataProcessActionBase
    {
        public override List<string> FindTargetValueAndProcess(List<CellValueData> rowData)
        {
            List<string> _result = new List<string>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                var _cell = rowData[MatchKeyList[i].GetKeyColumIndexInList()];
                var _tempCellValue = _cell.GetCellValue();
                float.TryParse(_tempCellValue, out var _floatValue);
                var _finalStr = ((int)(_floatValue * 100)).ToString();
                _result.Add(_finalStr);
            }
            return _result;
        }
    }
}

