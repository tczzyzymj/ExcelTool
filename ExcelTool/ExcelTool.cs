using ExcelTool;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;

namespace ExcelTool
{
    public partial class ExcelTool : Form
    {
        private TableBaseData? mExportTargetFile = null; // ����Ŀ���ļ������������������

        public ExcelTool()
        {
            InitializeComponent();
        }

        private void BtnStartExport_Click(object sender, EventArgs e)
        {

        }

        private void BtnExportSetting_Click(object sender, EventArgs e)
        {
            // ��������
            //if (mExportTargetFile == null)
            //{
            //    MessageBox.Show("û�пɵ���������", "��ʾ");
            //    return;
            //}

            var _serializeContent = JsonConvert.SerializeObject(mExportTargetFile, Formatting.None);

            // ����������ʾ
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".json";
            var _timeformat = DateTime.Now.GetDateTimeFormats();
            _saveFileDialog.FileName = $"{DateTime.Now.ToShortDateString()}_ExportSetting.json";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(_saveFileDialog.FileName, _serializeContent);

                MessageBox.Show("����ɹ�", " ��ʾ");
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            // ��������
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                var _loadedFile = JsonConvert.DeserializeObject<TableBaseData>(_openfileDialog.FileName);
                if (_loadedFile != null)
                {
                    mExportTargetFile = _loadedFile;
                }
            }
        }

        private void BntChooseExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            _openfileDialog.Filter = "*.xls|*.csv";
            _openfileDialog.Multiselect = false;
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var _extension = Path.GetExtension(_openfileDialog.FileName).ToLower();
                    if (_extension.Equals("xls") || _extension.Equals("xlsx"))
                    {
                        mExportTargetFile = new ExcelFileData();
                    }
                    else
                    {
                        mExportTargetFile = new CSVFileData();
                    }

                    if (!mExportTargetFile.DoLoadFile(_openfileDialog.FileName))
                    {
                        return;
                    }

                    TextForExportFilePath.Text = _openfileDialog.FileName;
                    PanelForChooseSheet.Visible = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "����");
                }
            }
        }

        private void TextBoxCommonProcess_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) || e.KeyChar == 8) // e.KeyChar == 8 ���˸��
            {
                e.Handled = true;
            }
        }

        private void BtnAnalysis_Click(object sender, EventArgs e)
        {

        }

        private void ExcelTool_Load(object sender, EventArgs e)
        {
            PanelForChooseSheet.Visible = false;
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}