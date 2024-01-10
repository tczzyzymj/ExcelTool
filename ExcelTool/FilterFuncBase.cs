using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExcelTool.MainTypeDefine;

namespace ExcelTool
{
    // 过滤方法后续可以添加的
    public abstract class FilterFuncBase
    {
        public int CompareWayIntValue = 0;

        // 都以 string 的方式保存，后续自己去解析
        public string CompareValue
        {
            get;
            protected set;
        } = string.Empty;

        /// <summary>
        /// 和下一个连接的判断类型，是 and 或者 or
        /// </summary>
        public MultiConditionJudgeType FilterConnectType = MultiConditionJudgeType.None;

        public abstract bool IsMatchFilter(string? content);

        public abstract string GetCompareValue();

        public virtual bool SetCompareValue(int compareWayIntvalue, string targetValue)
        {
            if (string.IsNullOrEmpty(targetValue))
            {
                CommonUtil.ShowError($"{SetCompareValue} 比较的内容为空，请检查");
                return false;
            }

            CompareWayIntValue = compareWayIntvalue;
            CompareValue = targetValue;
            return true;
        }

        public abstract int GetIntCompareType();

        public abstract string GetCompareWayName();
    }

    // 对比 int 数值
    public class FilterForCompareIntValue : FilterFuncBase
    {
        private MainTypeDefine.FilterCompareWayTypeForNumber CompareWay
        {
            get;
            set;
        } = MainTypeDefine.FilterCompareWayTypeForNumber.Equal;

        private int mTargetValue = 0;

        public override bool SetCompareValue(int compareWayIntvalue, string targetValue)
        {
            if (!int.TryParse(targetValue, out var _newvalue))
            {
                MessageBox.Show($"传入的值：{targetValue} 无法解析为 int, 请检查", "错误");
                return false;
            }

            CompareWay = (FilterCompareWayTypeForNumber)compareWayIntvalue;

            CompareValue = targetValue;

            mTargetValue = _newvalue;

            return true;
        }

        public override string GetCompareWayName()
        {
            return CompareWay.ToString();
        }

        public override int GetIntCompareType()
        {
            return (int)(FilterCompareValueType.IntValue);
        }

        public override string GetCompareValue()
        {
            return CompareValue.ToString();
        }

        public override bool IsMatchFilter(string? inValueStr)
        {
            if (string.IsNullOrEmpty(inValueStr))
            {
                return false;
            }

            if (!int.TryParse(inValueStr, out var _parsedValue))
            {
                return false;
            }

            switch (CompareWay)
            {
                case MainTypeDefine.FilterCompareWayTypeForNumber.Equal:
                {
                    return _parsedValue == mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayTypeForNumber.Greater:
                {
                    return _parsedValue > mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayTypeForNumber.Less:
                {
                    return _parsedValue < mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayTypeForNumber.GreaterAndEqual:
                {
                    return _parsedValue >= mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayTypeForNumber.LessAndQual:
                {
                    return _parsedValue <= mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayTypeForNumber.NotEqual:
                {
                    return _parsedValue != mTargetValue;
                }
                default:
                {
                    // 这里没有报错，直接抛异常吧
                    throw new Exception($"未处理的类型：{CompareWay}");
                }
            }
        }
    }

    public class FilterForCompareStringValue : FilterFuncBase
    {
        private MainTypeDefine.FilterCompareWayForString CompareWay
        {
            get;
            set;
        } = MainTypeDefine.FilterCompareWayForString.ContainsIgnoreCase;

        public override string GetCompareValue()
        {
            return CompareValue;
        }

        public override string GetCompareWayName()
        {
            return CompareWay.ToString();
        }

        public override int GetIntCompareType()
        {
            return (int)(FilterCompareValueType.StringValue);
        }

        public override bool SetCompareValue(int compareWayIntvalue, string targetValue)
        {
            if (!base.SetCompareValue(compareWayIntvalue, targetValue))
            {
                return false;
            }

            CompareValue = targetValue;
            CompareWay = (MainTypeDefine.FilterCompareWayForString)compareWayIntvalue;
            return true;
        }

        // 注意，如果是字符串比较，那么就认为，只能是equal
        public override bool IsMatchFilter(string? content)
        {
            if (string.IsNullOrEmpty(CompareValue))
            {
                throw new Exception($"{IsMatchFilter} 比较的内容为空，请检查");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new Exception($"{IsMatchFilter} 传入的 content 为空，请检查");
            }

            switch (CompareWay)
            {
                case MainTypeDefine.FilterCompareWayForString.ContainsIgnoreCase:
                {
                    return CompareValue.ToLower().Contains(content.ToLower());
                }
                case MainTypeDefine.FilterCompareWayForString.ContainsNoIgnoreCase:
                {
                    return CompareValue.Contains(content);
                }
                case MainTypeDefine.FilterCompareWayForString.CompleteEqual:
                {
                    return string.Equals(CompareValue, content);
                }
                default:
                {
                    throw new Exception("未处理的字符串比较类型：" + CompareWay);
                }
            }
        }
    }
}
