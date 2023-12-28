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
        } = string.Empty;
    }

    public abstract class DataProcessActionBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inRowData"></param>
        /// <returns>返回值有可能是作为行为序列中，查找行为的传入值，可能由多个value组成一个需要查找的值，因此不能返回 CellData</returns>
        /// <returns>同理， CellData 也不能作为输入值</returns>
        /// <exception cref="Exception"></exception>
        public virtual List<string> TryProcessData(List<string> inRowData)
        {
            if (inRowData == null || inRowData.Count < 1)
            {
                return new List<string>();
            }

            var _resultList = InternalProcessData(inRowData);

            switch (ResultReturnType)
            {
                case MultiResultReturnType.SingleString:
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

                    return new List<string>() { _builder.ToString() };
                }
                case MultiResultReturnType.StringList:
                {
                    return _resultList;
                }
                default:
                {
                    throw new Exception($"未处理的枚举:{ResultReturnType}");
                }
            }
        }

        public string MultiValueSplitSymbol = ",";

        public MultiResultReturnType ResultReturnType = MultiResultReturnType.SingleString;

        public List<int> MatchKeyIndexList = new List<int>(); // 对于源行为来说，需要在在配置源文件的时候，去指定 ID key 是哪个

        protected abstract List<string> InternalProcessData(List<string> inRowData);

        public virtual bool HaveDetailEdit()
        {
            return false;
        }
    }

    /// <summary>
    /// 作为导出表的 key 映射的第一个行为
    /// </summary>
    [ProcessAction("输入源数据")]
    public class SourceAction : DataProcessActionBase
    {
        public ActionForFindValue FindAction = new ActionForFindValue();

        protected override List<string> InternalProcessData(List<string> inRowData)
        {
            return FindAction.TryProcessData(inRowData);
        }
    }

    /// <summary>
    /// 只管找数据，找到数据以后返回交给后续处理
    /// </summary>
    [ProcessAction("查找行为")]
    public class ActionForFindValue : DataProcessActionBase
    {
        public CommonWorkSheetData? SearchTargetSheet = null; // 去哪个 sheet 查找

        public List<int> SearchKeyIndexList = new List<int>(); // 去 sheet 的那个位置找，注意 index 是 index in row ， 不是 index in sheet

        /// <summary>
        /// 注意，这里是给输入源数据用的
        /// </summary>
        public bool SkipSearch
        {
            get;
            set;
        }

        /// <summary>
        /// 找到数值后操作行为序列
        /// </summary>
        public List<DataProcessActionBase> FollowActionList = new List<DataProcessActionBase>();

        protected override List<string> InternalProcessData(List<string> inData)
        {
            if (SearchTargetSheet == null)
            {
                throw new Exception($" ActionForFindValue 出错，TryProcessData ， SearchTargetSheet 为空");
            }

            if (inData == null || inData.Count < 1)
            {
                throw new Exception($" ActionForFindValue 出错，TryProcessData ， inData 为空");
            }

            List<string> _findRowData;
            if (SkipSearch)
            {
                _findRowData = inData;
            }
            else
            {
                List<string> _matchValue = new List<string>();
                if (MatchKeyIndexList.Count > 0)
                {
                    foreach (var _index in MatchKeyIndexList)
                    {
                        _matchValue.Add(inData[_index]);
                    }
                }
                else
                {
                    _matchValue.AddRange(inData);
                }

                _findRowData = SearchTargetSheet.GetRowStringDataByTargetKeysAndValus(SearchKeyIndexList, _matchValue);
            }

            if (_findRowData == null)
            {
                return new List<string>();
            }

            List<string> _resultList = new List<string>();

            for (int i = 0; i < FollowActionList.Count; ++i)
            {
                var _result = FollowActionList[i].TryProcessData(_findRowData);
                if (_result == null || _result.Count == 0)
                {
                    return new List<string>();
                }
                else
                {
                    _resultList.AddRange(_result);
                }
            }

            return new List<string>();
        }

        public override bool HaveDetailEdit()
        {
            return true;
        }
    }

    /// <summary>
    /// 直接返回值
    /// </summary>
    [ProcessAction("直接返回")]
    public class ActionDirectReturn : DataProcessActionBase
    {
        protected override List<string> InternalProcessData(List<string> rowData)
        {
            List<string> _resultList = new List<string>();
            if (rowData == null || rowData.Count < 1)
            {
                return _resultList;
            }

            for (int i = 0; i < MatchKeyIndexList.Count; ++i)
            {
                var _cell = rowData[MatchKeyIndexList[i]];
                _resultList.Add(_cell);
            }

            return _resultList;
        }
    }

    [ProcessAction("返回为UEPos")]
    public class ActionReturnAsUEPos : DataProcessActionBase
    {
        protected override List<string> InternalProcessData(List<string> rowData)
        {
            List<string> _resultList = new List<string>();
            if (rowData == null || rowData.Count < 1)
            {
                return _resultList;
            }

            for (int i = 0; i < MatchKeyIndexList.Count; ++i)
            {
                var _cellValue = rowData[MatchKeyIndexList[i]];
                float.TryParse(_cellValue, out var _floatValue);
                var _finalStr = ((int)(_floatValue * 100)).ToString();
                _resultList.Add(_finalStr);
            }
            return _resultList;
        }
    }

    [ProcessAction("返回为UE旋转")]
    public class ActionReturnAsUERotateY : DataProcessActionBase
    {
        protected override List<string> InternalProcessData(List<string> rowData)
        {
            List<string> _resultList = new List<string>();
            if (rowData == null || rowData.Count < 1)
            {
                return _resultList;
            }

            for (int i = 0; i < MatchKeyIndexList.Count; ++i)
            {
                var _tempCellValue = rowData[MatchKeyIndexList[i]];
                float.TryParse(_tempCellValue, out var _floatValue);
                var _finalStr = ((int)(180 / 3.1415926 * _floatValue)).ToString();
                _resultList.Add(_finalStr);
            }

            return _resultList;
        }
    }

    [ProcessAction("格式化后返回")]
    public class ActionReturnAfterFormat : DataProcessActionBase
    {
        public string FormatStr = string.Empty;

        protected override List<string> InternalProcessData(List<string> rowData)
        {
            List<string> _resultList = new List<string>();
            if (rowData == null || rowData.Count < 1)
            {
                return _resultList;
            }

            for (int i = 0; i < rowData.Count; ++i)
            {
                bool _success = true;
                try
                {
                    _resultList.Add(string.Format(FormatStr, rowData[i]));
                }
                catch (Exception)
                {
                    _success = false;
                }

                if (!_success)
                {
                    var _builder = new StringBuilder();
                    for (int j = 0; j < rowData.Count; ++j)
                    {
                        _builder.Append(rowData[j]);

                        if (j < rowData.Count - 1)
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

            return _resultList;
        }

        public override bool HaveDetailEdit()
        {
            return true;
        }
    }
}

