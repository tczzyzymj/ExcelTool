using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    // 过滤方法后续可以添加的
    public abstract class FilterFuncBase
    {
        public MainTypeDefine.FilterCompareWayType CompareWay
        {
            get;
            set;
        } = MainTypeDefine.FilterCompareWayType.Equal;

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

        public abstract bool SetCompareValue(string targetValue);
    }

    // 对比 int 数值
    public class FilterForCompareIntValue : FilterFuncBase
    {
        private int mTargetValue = 0;

        public override bool SetCompareValue(string targetValue)
        {
            if (!int.TryParse(targetValue, out var _newvalue))
            {
                MessageBox.Show($"传入的值：{targetValue} 无法解析为 int, 请检查", "错误");
                return false;
            }

            CompareValue = targetValue;

            mTargetValue = _newvalue;

            return true;
        }

        public override string GetCompareValue()
        {
            return CompareValue.ToString();
        }

        public override bool IsMatchFilter(string? content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }

            if (!int.TryParse(content, out var _parsedValue))
            {
                return false;
            }

            switch (CompareWay)
            {
                case MainTypeDefine.FilterCompareWayType.Equal:
                {
                    return _parsedValue == mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayType.Greater:
                {
                    return _parsedValue > mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayType.Less:
                {
                    return _parsedValue > mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayType.GreaterAndEqual:
                {
                    return _parsedValue >= mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayType.LessAndQual:
                {
                    return _parsedValue <= mTargetValue;
                }
                case MainTypeDefine.FilterCompareWayType.NotEqual:
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
        public override string GetCompareValue()
        {
            return CompareValue;
        }

        public override bool SetCompareValue(string targetValue)
        {
            if (string.IsNullOrEmpty(targetValue))
            {
                return false;
            }

            CompareValue = targetValue;

            return true;
        }

        // 注意，如果是字符串比较，那么就认为，只能是equal
        public override bool IsMatchFilter(string? content)
        {
            return string.Equals(CompareValue, content);
        }
    }
}
