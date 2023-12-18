﻿using System;
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
            UseOldData = 0, // 使用旧数据
            UseNewData, // 使用新数据
        }

        public enum FilterCompareValueType
        {
            IntValue,
            StringValue,
        }

        public enum FilterCompareWayType
        {
            Equal = 0,
            Greater,
            Less,
            GreaterAndEqual,
            LessAndQual,
            NotEqual,
        }
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
    }

    public enum EventTypeEnum
    {
        SetFilterFinished = 1,
    }
}