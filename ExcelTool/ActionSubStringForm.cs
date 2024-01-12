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
    public partial class ActionSubStringForm : Form
    {
        private ActionSubString? mFromAction = null;
        public ActionSubStringForm()
        {
            InitializeComponent();
        }

        public bool InitData(ActionSubString targetValue)
        {
            if (targetValue == null)
            {
                CommonUtil.ShowError("错误，传入的 ActionSubString 为空!");
                return false;
            }

            mFromAction = targetValue;
            return true;
        }

        private void ActionSubStringForm_Load(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                return;
            }

            this.TextBoxForBeginIndex.Text = mFromAction.BeginIndex.ToString();
            this.TextBoxForSubLength.Text = mFromAction.SubLength.ToString();
            CheckBoxForThrow.Checked = mFromAction.ThrowExceptionIfError;
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            if (mFromAction == null)
            {
                CommonUtil.ShowError("错误，FromAction 为空，请检查!");
                return;
            }

            if (!int.TryParse(TextBoxForBeginIndex.Text, out mFromAction.BeginIndex))
            {
                CommonUtil.ShowError("开始下标的数据无法转化为int,请检查!");
            }

            if (!int.TryParse(TextBoxForSubLength.Text, out mFromAction.SubLength))
            {
                CommonUtil.ShowError("截取长度无法转化为int,请检查!");
            }

            this.DialogResult = DialogResult.OK;
        }

        private void CheckBoxForThrow_CheckedChanged(object sender, EventArgs e)
        {
            if (mFromAction != null)
            {
                mFromAction.ThrowExceptionIfError = CheckBoxForThrow.Checked;
            }
        }
    }
}
