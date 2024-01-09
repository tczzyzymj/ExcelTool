using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class MainTypeDefine
    {
        public enum ExportWriteWayType
        {
            Append = 0, // 以追加的方式
            OverWriteAll, // 新写入的方式，旧数据全部放弃
        }

        public enum ExportConflictDealWayType
        {
            [DisplayName("使用旧数据")]
            UseOldData = 0, // KEY有冲突，使用旧数据

            [DisplayName("使用新数据，空数据跳过")]
            UseNewDataSkipEmptyData, // KEY有冲突，使用新数据，空数据跳过

            [DisplayName("使用新数据全覆盖")]
            UseNewDataOverwriteAll, // KEY有冲突，使用新数据全覆盖
        }

        public enum FilterCompareValueType
        {
            [DisplayName("整数")]
            IntValue,

            [DisplayName("字符串")]
            StringValue,
        }

        public enum FilterCompareWayType
        {
            [DisplayName("等于")]
            Equal = 0,

            [DisplayName("大于")]
            Greater,

            [DisplayName("小于")]
            Less,

            [DisplayName("大于等于")]
            GreaterAndEqual,

            [DisplayName("小于等于")]
            LessAndQual,

            [DisplayName("不等于")]
            NotEqual,
        }
    }

    public enum MultiResultReturnType
    {
        [DisplayName("单值返回")]
        SingleString,

        [DisplayName("数组返回")]
        ListString,
    }

    public enum LoadFileType
    {
        // 普通文件
        NormalFile,

        // 导出目标文件
        ExportFile,

        // 数据源文件
        SourceFile,

        // 设置查找KEY
        SetSearchKey,
    }

    public enum MultiConditionJudgeType
    {
        [DisplayName("无")]
        None,

        [DisplayName("或")]
        Or,

        [DisplayName("并且")]
        And
    }

    public class CommonDataForComboBox
    {
        public int RealValue
        {
            get;
            set;
        } = 0;

        public string DisplayName
        {
            get;
            set;
        } = string.Empty;

        public Type? TargetType
        {
            get;
            set;
        } = null;

        public int Index
        {
            get;
            set;
        } = 0;
    }

    public enum EventTypeEnum
    {
        SetFilterFinished = 1,
    }
}
