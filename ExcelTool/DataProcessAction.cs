using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class DisplayNameAttribute : Attribute
    {
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName
        {
            get;
            set;
        } = string.Empty;
    }

    public abstract class ActionCore
    {
        public MultiResultReturnType ResultReturnType = MultiResultReturnType.SingleString;

        public string MultiValueSplitSymbol = ",";

        public virtual List<string> ProcessData(List<string> inDataList)
        {
            var _resultList = OnProcessData(inDataList);

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
                case MultiResultReturnType.ListString:
                {
                    return _resultList;
                }
                default:
                {
                    throw new Exception($"未处理的枚举:{ResultReturnType}");
                }
            }
        }

        protected abstract List<string> OnProcessData(List<string> inDataList);

        public abstract bool HaveDetailEdit();

        public virtual void OpenDetailEditForm()
        {
        }
    }

    /// <summary>
    /// 对数据，自身不做任何执行
    /// </summary>
    public abstract class ActionNoSelfProcess : ActionCore
    {
    }

    /// <summary>
    /// 对数据，自己是要执行的
    /// </summary>
    public abstract class ActionWithSelfProcess : ActionCore
    {
        protected override List<string> OnProcessData(List<string> inDataList)
        {
            return OnSelfProcessData(inDataList);
        }

        protected abstract List<string> OnSelfProcessData(List<string> inDataList);
    }

    public class SequenceAction : ActionNoSelfProcess
    {
        public List<NormalActionBase> ActionSequence = new List<NormalActionBase>();

        public CommonWorkSheetData? WorkSheetData = null;

        protected override List<string> OnProcessData(List<string> inDataList)
        {
            List<string> _resultList = new List<string>();

            for (int i = 0; i < ActionSequence.Count; ++i)
            {
                var _tempResult = ActionSequence[i].ProcessData(inDataList);
                _resultList.AddRange(_tempResult);
            }

            return _resultList;
        }

        public override bool HaveDetailEdit()
        {
            return false;
        }
    }

    public abstract class NormalActionBase : ActionWithSelfProcess
    {
        public List<int> MatchKeyIndexList = new List<int>();

        public List<string> MatchKeyNameList = new List<string>(); // 给外面显示用的

        public SequenceAction FollowSequenceAction = new SequenceAction();

        protected override List<string> OnSelfProcessData(List<string> inDataList)
        {
            if (MatchKeyIndexList.Count > 0)
            {
                List<string> _tempInDataList = new List<string>();

                for (int i = 0; i < MatchKeyIndexList.Count; ++i)
                {
                    if (MatchKeyIndexList[i] < 0 || MatchKeyIndexList[i] > inDataList.Count)
                    {
                        throw new Exception(
                            $"错误，下标越界了，传入的数据数量：[{inDataList.Count}] , 记录的下标 Index : [{i}],数值 : [{MatchKeyIndexList[i]}]"
                        );
                    }

                    _tempInDataList.Add(inDataList[MatchKeyIndexList[i]]);
                }

                return InternalSelfProcessData(_tempInDataList);
            }
            else
            {
                return InternalSelfProcessData(inDataList);
            }
        }

        protected abstract List<string> InternalSelfProcessData(List<string> inDataList);
    }

    /// <summary>
    /// 直接返回值
    /// </summary>
    [DisplayName("直接返回")]
    public class ActionDirectReturn : NormalActionBase
    {
        protected override List<string> InternalSelfProcessData(List<string> inDataList)
        {
            List<string> _resultList = new List<string>();
            if (inDataList == null || inDataList.Count < 1)
            {
                return _resultList;
            }

            for (int i = 0; i < MatchKeyIndexList.Count; ++i)
            {
                var _cell = inDataList[MatchKeyIndexList[i]];
                _resultList.Add(_cell);
            }

            return _resultList;
        }

        public override bool HaveDetailEdit()
        {
            return false;
        }
    }

    [DisplayName("查找行为")]
    public class FindAction : NormalActionBase
    {
        protected CommonWorkSheetData? SearchWorkSheet = null;

        public List<int> SearchKeyIndexList = new List<int>();

        protected override List<string> InternalSelfProcessData(List<string> inDataList)
        {
            if (SearchWorkSheet == null)
            {
                throw new Exception($"错误，查找的目标表格为空，请检查");
            }

            if (SearchKeyIndexList.Count < 1)
            {
                throw new Exception($"错误，查找的 KeyIndexList 为空，请检查！");
            }

            var _rowData = SearchWorkSheet.GetRowStringDataByTargetKeysAndValus(SearchKeyIndexList, inDataList);

            return _rowData;
        }

        public void SetSearchWorkSheet(CommonWorkSheetData targetSheet)
        {
            SearchWorkSheet = targetSheet;

            FollowSequenceAction.WorkSheetData = SearchWorkSheet;
        }

        public CommonWorkSheetData? GetSearchWorkSheetData()
        {
            return SearchWorkSheet;
        }

        public override bool HaveDetailEdit()
        {
            return true;
        }

        public override void OpenDetailEditForm()
        {
            ChooseFileConfigForm _form = new ChooseFileConfigForm();
            _form.SetFindAction(this);
            if (_form.ShowDialog() == DialogResult.OK)
            {
                var _chooseSheet = _form.GetChooseSheet();
                if (_chooseSheet == null)
                {
                    CommonUtil.ShowError("FindAction -> OpenDetailEditForm , 没有选择正确的 WorkSheet ");
                    return;
                }

                var _selectKeyIndexList = _form.GetSelectKeyIndexList();
                if (_selectKeyIndexList == null || _selectKeyIndexList.Count < 1)
                {
                    CommonUtil.ShowError("FindAction -> OpenDetailEditForm , 没有选择匹配 Key ");

                    return;
                }

                SetSearchWorkSheet(_chooseSheet);
                SearchKeyIndexList.Clear();
                SearchKeyIndexList.AddRange(_selectKeyIndexList);
            }
        }
    }

    [DisplayName("返回为UEPos")]
    public class ActionReturnAsUEPos : NormalActionBase
    {
        protected override List<string> InternalSelfProcessData(List<string> rowData)
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

        public override bool HaveDetailEdit()
        {
            return false;
        }
    }

    [DisplayName("返回为UE旋转")]
    public class ActionReturnAsUERotateY : NormalActionBase
    {
        protected override List<string> InternalSelfProcessData(List<string> rowData)
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

        public override bool HaveDetailEdit()
        {
            return false;
        }
    }

    [DisplayName("格式化后返回")]
    public class ActionReturnAfterFormat : NormalActionBase
    {
        public string FormatStr = string.Empty;

        protected override List<string> InternalSelfProcessData(List<string> rowData)
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

        public override void OpenDetailEditForm()
        {
            base.OpenDetailEditForm();
            DetailFormForStrFormatAction _form = new DetailFormForStrFormatAction();
            _form.Init(this);
            _form.ShowDialog();
        }
    }

    [DisplayName("返回匹配列名中下标")]
    public class ActionReturnMatchKeyContensIndex : NormalActionBase
    {
        public int IndexChangeValue = 0;

        protected override List<string> InternalSelfProcessData(List<string> inDataList)
        {
            if (MatchKeyNameList.Count != inDataList.Count)
            {
                throw new Exception(
                    $"错误，匹配 KEY名字的数量：[{MatchKeyNameList.Count}] 与 数据数量: [{inDataList.Count}] 不匹配"
                );
            }

            var _result = new List<string>();

            Regex regex = new Regex(@"\[(\d+)\]"); // 定义正则表达式模式

            for (int i = 0; i < MatchKeyNameList.Count; ++i)
            {
                Match match = regex.Match(MatchKeyNameList[i]); // 进行匹配操作

                if (match.Success)
                {
                    if (int.TryParse(match.Groups[1].Value, out var _tempValue))
                    {
                        _result.Add((_tempValue + IndexChangeValue).ToString());
                    }
                }
            }

            return _result;
        }

        public override bool HaveDetailEdit()
        {
            return true;
        }

        public override void OpenDetailEditForm()
        {
            base.OpenDetailEditForm();

            ActionReturnMatchKeyIndexDetail _form = new ActionReturnMatchKeyIndexDetail();
            _form.Init(this);
            _form.ShowDialog();
        }
    }

    [DisplayName("返回指定字符串")]
    public class ActionReturnSpecificValue : NormalActionBase
    {
        public string TargetValue;

        protected override List<string> InternalSelfProcessData(List<string> inDataList)
        {
            return new List<string>() { TargetValue };
        }

        public override bool HaveDetailEdit()
        {
            return true;
        }

        public override void OpenDetailEditForm()
        {
            base.OpenDetailEditForm();

            ReturnSpecificValueForm _form = new ReturnSpecificValueForm();
            _form.Init(this);
            _form.ShowDialog();
        }
    }
}

