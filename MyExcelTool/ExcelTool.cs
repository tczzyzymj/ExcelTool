using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;

namespace MyExcelTool
{
    public partial class ExcelTool : Form
    {
        private ExcelFileBase mExportTargetFile = null; // ����Ŀ���ļ������������������

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
            //    MessageBox.Show("û�пɵ���������", "��ʾ");
            //    return;
            //}

            var _serializeContent = JsonConvert.SerializeObject(mExportTargetFile, Formatting.None);

            // ����������ʾ
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Filter = "*.txt|txt file";
            _saveFileDialog.DefaultExt = ".json";
            _saveFileDialog.FileName = $"{DateTime.Now.ToShortDateString}_ExportSetting.json";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(_saveFileDialog.FileName, _serializeContent);

                MessageBox.Show("����ɹ�", " ��ʾ");
            }
        }

        private void ExportSettingBtn_Click(object sender, EventArgs e)
        {
            InternalExportSetting();
        }
    }
}