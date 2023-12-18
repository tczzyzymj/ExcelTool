using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTool
{
    public class FormBase : Form
    {
        public void SendEvent(params object[] args)
        {
            OnProcessEvent(args);
        }

        protected virtual void OnProcessEvent(params object[] args)
        {
        }
    }
}
