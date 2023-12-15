using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;

namespace MyExcelTool
{
    public partial class ExcelTool : Form
    {
        private ExcelFileBase? mExportTargetFile = null; // 导出目标文件，其关联在里面设置

        public ExcelTool()
        {
            InitializeComponent();
        }

        private void BtnStartExport_Click(object sender, EventArgs e)
        {

        }

        private void BtnExportSetting_Click(object sender, EventArgs e)
        {
            // 导出配置
            //if (mExportTargetFile == null)
            //{
            //    MessageBox.Show("没有可导出的配置", "提示");
            //    return;
            //}

            var _serializeContent = JsonConvert.SerializeObject(mExportTargetFile, Formatting.None);

            // 弹出保存提示
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".json";
            var _timeformat = DateTime.Now.GetDateTimeFormats();
            _saveFileDialog.FileName = $"{DateTime.Now.ToShortDateString()}_ExportSetting.json";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(_saveFileDialog.FileName, _serializeContent);

                MessageBox.Show("保存成功", " 提示");
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            // 导入配置
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                var _loadedFile = JsonConvert.DeserializeObject<ExcelFileBase>(_openfileDialog.FileName);
                if (_loadedFile != null)
                {
                    mExportTargetFile = _loadedFile;
                }
            }
        }

        private void BntChooseExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            _openfileDialog.Filter = "*.xls|*.xlsx";
            _openfileDialog.Multiselect = false;
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                mExportTargetFile = new ExcelFileBase();
                if (mExportTargetFile.LoadFile(_openfileDialog.FileName))
                {
                    TextForExportFilePath.Text = _openfileDialog.FileName;
                }
            }
        }
    }
}