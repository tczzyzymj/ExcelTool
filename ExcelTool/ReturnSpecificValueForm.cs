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
    public partial class ReturnSpecificValueForm : Form
    {
        private ActionReturnSpecificValue mFromAction = null;

        public ReturnSpecificValueForm()
        {
            InitializeComponent();
        }

        public bool Init(ActionReturnSpecificValue targetAction)
        {
            if (targetAction == null)
            {
                CommonUtil.ShowError("错误，传入的 ActionReturnSpecificValue 为空");

                return false;
            }

            mFromAction = targetAction;

            return true;
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            mFromAction.TargetValue = this.TextBoxForReturnValue.Text;
        }

        private void ReturnSpecificValueForm_Load(object sender, EventArgs e)
        {
            this.TextBoxForReturnValue.Text = mFromAction.TargetValue;
        }
    }
}
