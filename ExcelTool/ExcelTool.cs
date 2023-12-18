using ExcelTool;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class ExcelTool : Form
    {
        private TableBaseData? mExportTargetFile = null; // ����Ŀ���ļ������������������

        private TableBaseData? mSourceFile = null; // ����Դ�ļ�

        public ExcelTool()
        {
            InitializeComponent();
            DataViewConfigForExportFile.AllowUserToAddRows = false;
        }

        public TableBaseData? TryChooseExportFile(string absoluteFilePath)
        {
            InternalChooseTargetFile(ref mExportTargetFile, absoluteFilePath);
            return mExportTargetFile;
        }

        public TableBaseData? TryChooseSourceFile(string absoluteFilePath)
        {
            InternalChooseTargetFile(ref mSourceFile, absoluteFilePath);
            return mSourceFile;
        }

        private bool InternalChooseTargetFile(ref TableBaseData targetFile, string absoluteFilePath)
        {
            bool _result = false;
            try
            {
                var _extension = Path.GetExtension(absoluteFilePath).ToLower();
                if (_extension.Equals(".xls") || _extension.Equals(".xlsx"))
                {
                    targetFile = new ExcelFileData();
                }
                else if (_extension.Equals(".csv"))
                {
                    targetFile = new CSVFileData();
                }
                else
                {
                    throw new Exception($"�ļ����Ͳ�ƥ�䣬�����ļ���Ŀ���ļ�·��Ϊ��{absoluteFilePath}");
                }

                if (!targetFile.DoLoadFile(absoluteFilePath))
                {
                    return false;
                }

                _result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "����");
            }

            return _result;
        }

        public TableBaseData? GetExportFileData()
        {
            return mExportTargetFile;
        }

        public TableBaseData? GetSourceFileData()
        {
            return mSourceFile;
        }

        private void BtnStartExport_Click(object sender, EventArgs e)
        {

        }

        private void BtnExportSetting_Click(object sender, EventArgs e)
        {
            MessageBox.Show("����������", "��ʾ");
            return;
            // ��������
            if (mExportTargetFile == null)
            {
                MessageBox.Show("û�пɵ���������", "��ʾ");
                return;
            }

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
            MessageBox.Show("����������", "��ʾ");
            return;
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
            ExportFileConfigForm _exportConfigForm = new ExportFileConfigForm();
            _exportConfigForm.ShowDialog(this);
        }

        private void BtnAnalysis_Click(object sender, EventArgs e)
        {
            // ����ֻ����һ������
            if (this.mExportTargetFile == null)
            {
                MessageBox.Show("��ǰδѡ����Ҫ������Ŀ���ļ�", "����");
                return;
            }
            if (mExportTargetFile.GetCurrentWorkSheet() == null)
            {
                MessageBox.Show("��ǰδѡ����Ҫ������Ŀ���ļ� Sheet", "����");
                return;
            }
            var _workSheet = mExportTargetFile.GetCurrentWorkSheet();
            var _keyList = _workSheet?.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                MessageBox.Show("��ǰѡ����Ҫ������Ŀ���ļ� Sheet��δ�ܽ����� Key���������û��ߺ��г���Ա!", "����");
                return;
            }

            for (int i = 0; i < _keyList.Count; ++i)
            {
                this.DataViewConfigForExportFile.Rows.Add(_keyList[i].GetKeyName(), string.Empty, "�༭");
            }

            //for (int i = 0; i < _keyList.Count; ++i)
            //{
            //    this.DataViewConfigForExportFile.Rows.Add(_keyList[i].GetKeyName(), string.Empty, null);
            //}
        }

        private void ExcelTool_Load(object sender, EventArgs e)
        {

        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataVewConfigForExportFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var _columIndex = e.ColumnIndex;
            if (_columIndex == 2)
            {
                // ����˱༭��ť�������༭���
                var _rowIndex = e.RowIndex;
                ProcessForClickEditBtn(_rowIndex);
            }
        }

        private void ProcessForClickEditBtn(int rowIndex)
        {

        }

        private void BtnChooseSourceFile_Click(object sender, EventArgs e)
        {
            SourceFileConfigForm _form = new SourceFileConfigForm();
            _form.ShowDialog(this);
        }

        private void StartExportBtn_Click(object sender, EventArgs e)
        {
            if (mSourceFile == null || !mSourceFile.GetHasInit())
            {
                MessageBox.Show("Դ�ļ�δ׼���ã����飡", "����");
                return;
            }
        }
    }
}