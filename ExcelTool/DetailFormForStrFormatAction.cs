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
    public partial class DetailFormForStrFormatAction : Form
    {
        public DetailFormForStrFormatAction()
        {
            InitializeComponent();
        }
        private ActionReturnAfterFormat? mFromAction = null;

        public bool Init(ActionReturnAfterFormat fromAction)
        {
            if (fromAction == null)
            {
                CommonUtil.ShowError("错误，传入的 ActionReturnAfterFormat 参数为空");
                return false;
            }

            mFromAction = fromAction;

            return true;
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                CommonUtil.ShowError("mFromAction 为空");
                return;
            }

            mFromAction.FormatStr = this.TextBoxForFormat.Text;
        }

        private void DetailFormForStrFormatAction_Load(object sender, EventArgs e)
        {
            if (mFromAction != null)
            {
                this.TextBoxForFormat.Text = mFromAction.FormatStr;
            }
        }
    }
}
