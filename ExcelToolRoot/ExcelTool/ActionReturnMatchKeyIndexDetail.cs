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
    public partial class ActionReturnMatchKeyIndexDetail : Form
    {
        private ActionReturnMatchKeyContensIndex? mFromAction = null;


        public ActionReturnMatchKeyIndexDetail()
        {
            InitializeComponent();
        }

        public bool Init(ActionReturnMatchKeyContensIndex fromAction)
        {
            if (fromAction == null)
            {
                CommonUtil.ShowError("错误，传入的 ActionReturnMatchKeyContensIndex 为空，请检查!");
                return false;
            }

            mFromAction = fromAction;

            return true;
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                CommonUtil.ShowError("错误,mFromAction 为空 ");
                return;
            }
            if (!int.TryParse(this.TextBoxForFormat.Text, out var _finalValue))
            {
                CommonUtil.ShowError("错误，无法转化为数字，请检查");
                return;
            }

            mFromAction.IndexChangeValue = _finalValue;
        }

        private void ActionReturnMatchKeyIndexDetail_Load(object sender, EventArgs e)
        {
            if (mFromAction != null)
            {
                this.TextBoxForFormat.Text = mFromAction.IndexChangeValue.ToString();
            }
        }
    }
}
