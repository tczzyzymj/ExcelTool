using ExcelTool;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class ExcelTool : FormBase
    {
        private TableBaseData? mExportTargetFile = null; // ����Ŀ���ļ������������������

        private TableBaseData? mSourceFile = null; // ����Դ�ļ�

        protected MainTypeDefine.ExportWriteWayType mExportWriteWayType = MainTypeDefine.ExportWriteWayType.Append;

        protected MainTypeDefine.ExportConflictDealWayType mExportConfigDealWayType = MainTypeDefine.ExportConflictDealWayType.UseOldData;

        private bool mHasAnalysData = false;

        private List<TableBaseData> mLoadedTableData = new List<TableBaseData>();

        public ExcelTool()
        {
            InitializeComponent();
            DataViewConfigForExportFile.AllowUserToAddRows = false;
        }

        public TableBaseData? TryChooseExportFile(string absoluteFilePath)
        {
            mExportTargetFile = InternalLoadFile(absoluteFilePath, false);

            return mExportTargetFile;
        }

        public TableBaseData? TryChooseSourceFile(string absoluteFilePath)
        {
            mSourceFile = InternalLoadFile(absoluteFilePath, false);

            return mSourceFile;
        }

        private TableBaseData? InternalLoadFile(string absoluteFilePath, bool checkExistAndAdd)
        {
            TableBaseData? targetFile = null;

            try
            {
                if (checkExistAndAdd)
                {
                    var _exitItem = mLoadedTableData.Find(x => x.GetFilePath() == absoluteFilePath);
                    if (_exitItem != null)
                    {
                        return _exitItem;
                    }
                }

                TableBaseData? _tempFile = null;
                var _extension = Path.GetExtension(absoluteFilePath).ToLower();
                if (_extension.Equals(".xls") || _extension.Equals(".xlsx"))
                {
                    _tempFile = new ExcelFileData();
                }
                else if (_extension.Equals(".csv"))
                {
                    _tempFile = new CSVFileData();
                }
                else
                {
                    throw new Exception($"�ļ����Ͳ�ƥ�䣬�����ļ���Ŀ���ļ�·��Ϊ��{absoluteFilePath}");
                }

                if (!_tempFile.DoLoadFile(absoluteFilePath))
                {
                    return null;
                }

                targetFile = _tempFile;

                if (checkExistAndAdd)
                {
                    mLoadedTableData.Add(targetFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "����");
            }

            return targetFile;
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
            if (_exportConfigForm.ShowDialog(this) == DialogResult.OK)
            {
                InternalAnalysisKey();
            }
        }

        private void InternalAnalysisKey()
        {
            // ����ֻ����һ������
            if (this.mExportTargetFile == null)
            {
                MessageBox.Show("��ǰδѡ����Ҫ������Ŀ���ļ�", "����");
                return;
            }

            var _workSheet = mExportTargetFile.GetCurrentWorkSheet();
            if (_workSheet == null)
            {
                MessageBox.Show("��ǰδѡ����Ҫ������Ŀ���ļ� Sheet", "����");
                return;
            }

            var _keyList = _workSheet.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                MessageBox.Show("��ǰѡ����Ҫ������Ŀ���ļ� Sheet��δ�ܽ����� Key���������û��ߺ��г���Ա!", "����");
                return;
            }

            DataViewConfigForExportFile.Rows.Clear();

            for (int i = 0; i < _keyList.Count; ++i)
            {
                this.DataViewConfigForExportFile.Rows.Add(
                    CommonUtil.GetZM(_keyList[i].GetKeyIndexForShow()),
                    _keyList[i].GetKeyName(),
                    "",
                    "����"
                );
            }

            mHasAnalysData = true;
        }

        private void ExcelTool_Load(object sender, EventArgs e)
        {
            {
                List<CommonDataForComboBox> _dataForExportWay = new List<CommonDataForComboBox>();
                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 0;
                    _tempone.DisplayName = "β�����";
                    _dataForExportWay.Add(_tempone);
                }

                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 1;
                    _tempone.DisplayName = "ȫ����д";
                    _dataForExportWay.Add(_tempone);
                }
                ComboBoxForExportWriteWay.DataSource = _dataForExportWay;
                ComboBoxForExportWriteWay.ValueMember = "RealValue";
                ComboBoxForExportWriteWay.DisplayMember = "DisplayName";
            }

            {
                List<CommonDataForComboBox> _dataForExportWay = new List<CommonDataForComboBox>();
                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 0;
                    _tempone.DisplayName = "ʹ�þ�����";
                    _dataForExportWay.Add(_tempone);
                }

                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 1;
                    _tempone.DisplayName = "ʹ��������";
                    _dataForExportWay.Add(_tempone);
                }
                ComboBoxForExportConflictDealWay.DataSource = _dataForExportWay;
                ComboBoxForExportConflictDealWay.ValueMember = "RealValue";
                ComboBoxForExportConflictDealWay.DisplayMember = "DisplayName";
            }
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnChooseSourceFile_Click(object sender, EventArgs e)
        {
            SourceFileConfigForm _form = new SourceFileConfigForm();
            if (_form.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void StartExportBtn_Click(object sender, EventArgs e)
        {
            if (mExportTargetFile == null || !mExportTargetFile.GetHasInit())
            {
                MessageBox.Show("����Ŀ���ļ�δ׼���ã������õ���Ŀ���ļ���", "����");
                return;
            }
            if (mSourceFile == null || !mSourceFile.GetHasInit())
            {
                MessageBox.Show("Դ�ļ�δ׼���� ��������Դ�ļ���", "����");
                return;
            }
        }

        private void ComboBoxForExportWriteWay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxForExportConfigDealWay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataViewConfigForExportFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataViewConfigForExportFile_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DataViewConfigForExportFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var _columIndex = e.ColumnIndex;
            if (_columIndex == 2)
            {
                // ����˱༭��ť�������༭���
                var _rowIndex = e.RowIndex;
            }
        }
    }
}