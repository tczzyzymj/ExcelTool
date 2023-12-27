using Microsoft.Extensions.Primitives;
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

        public abstract List<string>? TryProcessData(List<string> inRowData);

        public string MultiValueSplitSymbol = ",";
    }

    /// <summary>
    /// 有后续操作的行为
    /// </summary>
    public abstract class ActionWithFollowActions : DataProcessActionBase
    {
        /// <summary>
        /// 后续操作行为列表
        /// </summary>
        public List<DataProcessActionBase> FollowActionList = new List<DataProcessActionBase>();

        public override List<string> TryProcessData(List<string> inRowData)
        {
            var _resultList = OnProcessSelfAction(inRowData);
            if (FollowActionList.Count > 0)
            {
                return OnProcessFollowActions(_resultList);
            }

            return _resultList;
        }

        protected abstract List<string> OnProcessSelfAction(List<string> inRowData);

        protected abstract List<string> OnProcessFollowActions(List<string> inRowData);
    }

    /// <summary>
    /// 后续无操作的行为
    /// </summary>
    public abstract class ActionNoFollowActions : DataProcessActionBase
    {

    }

    [ProcessAction("源行为")]
    public class SourceAction : DataProcessActionBase
    {
        public List<DataProcessActionBase> ActionList = new List<DataProcessActionBase>();

        public override string TryProcessData(List<CellValueData> rowValue)
        {
            if (ActionList == null || ActionList.Count < 1)
            {
                return string.Empty;
            }

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
    [ProcessAction("返回单string")]
    public class ActionDirectReturn : ActionNoFollowActions
    {
        public override string TryProcessData(DataProcessActionBase preAction, List<string> rowData)
        {
            List<string> _resultList = new List<string>();
            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                var _cell = rowData[MatchKeyList[i].GetKeyColumIndexInList()];
                _resultList.Add(_cell);
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
    public class ActionFindRowDataInOtherSheet : ActionWithFollowActions
    {
        public CommonWorkSheetData? SearchTargetSheet = null;

        public List<KeyData> SearchKeyList = new List<KeyData>();

        protected override string OnProcessFollowActions()
        {
            
        }

        protected override string OnProcessSelfAction(List<CellValueData> inRowData)
        {
            
        }


        public override List<string> TryProcessData(List<CellValueData> inRowData)
        {
            if (FollowActionList.Count < 1)
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
            if (SearchTargetSheet == null)
            {
                throw new Exception($"{TryProcessData} 出错，SearchTargetSheet 为空");
            }

            SearchTargetSheet.LoadAllCellData(false);

            List<string> _searchInCellList = new List<string>();

            for (int i = 0; i < MatchKeyList.Count; ++i)
            {
                _searchInCellList.Add(inRowData[MatchKeyList[i].GetKeyColumIndexInList()].GetCellValue());
            }

            List<string> _resultList = new List<string>();

            List<int> _searchKeyIndexList = new List<int>();
            foreach (var _tempKey in SearchKeyList)
            {
                _searchKeyIndexList.Add(_tempKey.GetKeyColumIndexInList());
            }

            var searchMatchRowData = SearchTargetSheet.GetRowDataByTargetKeysAndValus(_searchKeyIndexList, _searchInCellList);
            if (searchMatchRowData == null)
            {
                return null;
            }

            for (int i = 0; i < FollowActionList.Count; ++i)
            {
                var _tempValue = FollowActionList[i].TryProcessData(searchMatchRowData);

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
    public class ActionReturnAsUEPos : ActionNoFollowActions
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
    public class ActionReturnAsUERotateY : ActionNoFollowActions
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

    [ProcessAction("格式化后返回")]
    public class ActionReturnAfterFormat : ActionNoFollowActions
    {
        public string FormatStr = string.Empty;

        public override string TryProcessData(List<CellValueData> inRowData)
        {
            if (inRowData == null || inRowData.Count < 1)
            {
                return string.Empty;
            }

            StringBuilder _builder = new StringBuilder();

            for (int i = 0; i < inRowData.Count; ++i)
            {
                bool _success = true;
                try
                {
                    _builder.Append(string.Format(FormatStr, inRowData[i].GetCellValue()));

                    if (i < inRowData.Count - 1)
                    {
                        _builder.Append(MultiValueSplitSymbol);
                    }
                }
                catch (Exception ex)
                {
                    _success = false;
                }

                if (!_success)
                {
                    _builder.Clear();
                    for (int j = 0; j < inRowData.Count; ++j)
                    {
                        _builder.Append(inRowData[j].GetCellValue());

                        if (j < inRowData.Count - 1)
                        {
                            _builder.Append(" , ");
                        }
                    }

                    // 外部自己接 catch 这里是为了防止继续写下去
                    throw new Exception(string.Format(
                            "格式化错误 ，格式化字符串:{0}, 传入内容 : {1} ",
                            FormatStr,
                            _builder.ToString()
                        )
                    );
                }
            }

            return _builder.ToString();
        }
    }
}

