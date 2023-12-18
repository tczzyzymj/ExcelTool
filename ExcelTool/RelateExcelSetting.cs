using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class RelateExcelSetting : FormBase
    {
        public RelateExcelSetting()
        {
            InitializeComponent();
        }

        public bool InitData(TableBaseData fromData)
        {
            if (fromData == null)
            {
                MessageBox.Show("传入的 ExcelFileBase 为空，请检查！", "错误");
                return false;
            }

            return true;
        }
    }
}
