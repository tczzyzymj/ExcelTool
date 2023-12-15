using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;

namespace MyExcelTool
{
    public partial class ExcelTool : Form
    {
        private ExcelFileBase mExportTargetFile = null; // 导出目标文件，其关联在里面设置

        public ExcelTool()
        {
            InitializeComponent();
        }

        private void BtnStartExport_Click(object sender, EventArgs e)
        {

        }


        private void InternalExportSetting()
        {
            //if (mExportTargetFile == null)
            //{
            //    MessageBox.Show("没有可导出的配置", "提示");
            //    return;
            //}

            var _serializeContent = JsonConvert.SerializeObject(mExportTargetFile, Formatting.None);

            // 弹出保存提示
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Filter = "*.txt|txt file";
            _saveFileDialog.DefaultExt = ".json";
            _saveFileDialog.FileName = $"{DateTime.Now.ToShortDateString}_ExportSetting.json";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(_saveFileDialog.FileName, _serializeContent);

                MessageBox.Show("保存成功", " 提示");
            }
        }

        private void ExportSettingBtn_Click(object sender, EventArgs e)
        {
            InternalExportSetting();
        }
    }
}