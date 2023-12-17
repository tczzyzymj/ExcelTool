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
            ListViewMain.BeginUpdate();
            //ListViewMain.View = View.List;
            //ListViewMain.Columns.Clear();
            ListViewMain.Columns.Add("Key", 120, HorizontalAlignment.Center);
            ListViewMain.Columns.Add("Content", 120, HorizontalAlignment.Center);

            //for (int i = 0; i < 10; ++i)
            //{
            //    var _item = new ListViewItem();
            //    _item.Text = i.ToString();
            //    //_item.SubItems.Add(i.ToString());
            //    //_item.SubItems.Add(i.ToString());
            //    ListViewMain.Items.Add(_item);
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    var _temButton = new Button();
            //    _temButton.Text = "Test" + i;
            //    _temButton.Name = "Test" + i;
            //    ListViewMain.Controls.Add(_temButton);
            //}

            ListViewMain.EndUpdate();
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}