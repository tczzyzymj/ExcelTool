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
        private const int mColumIndexForSetConnect = 3;

        private const int mColumIndexForSetIgnore = 4;

        private const int mColumIndexForIsMainKey = 5;

        private List<KeyData>? mKeyList = null;

        public ExcelTool()
        {
            InitializeComponent();
            DataViewConfigForExportFile.AllowUserToAddRows = false;
        }

        private void BtnStartExport_Click(object sender, EventArgs e)
        {

        }

        private void BtnExportSetting_Click(object sender, EventArgs e)
        {
            MessageBox.Show("����������", "��ʾ");
            return;
            // ��������
            var mExportTargetFile = TableDataManager.Instance().GetExportFileData();
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
            var mExportTargetFile = TableDataManager.Instance().GetExportFileData();
            // ��������
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                var _loadedFile = JsonConvert.DeserializeObject<TableBaseData>(_openfileDialog.FileName);
                if (_loadedFile != null)
                {
                    TableDataManager.Instance().TrySetExportTargetFile(_loadedFile);
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
            var _exportFile = TableDataManager.Instance().GetExportFileData();

            // ����ֻ����һ������
            if (_exportFile == null)
            {
                MessageBox.Show("��ǰδѡ����Ҫ������Ŀ���ļ�", "����");
                return;
            }

            var _workSheet = _exportFile.GetCurrentWorkSheet();
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

            mKeyList = _keyList;

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
                ComboBoxForExportWriteWay.BeginUpdate();
                ComboBoxForExportWriteWay.DataSource = _dataForExportWay;
                ComboBoxForExportWriteWay.ValueMember = "RealValue";
                ComboBoxForExportWriteWay.DisplayMember = "DisplayName";
                ComboBoxForExportWriteWay.EndUpdate();
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
                ComboBoxForExportConflictDealWay.BeginUpdate();
                ComboBoxForExportConflictDealWay.DataSource = _dataForExportWay;
                ComboBoxForExportConflictDealWay.ValueMember = "RealValue";
                ComboBoxForExportConflictDealWay.DisplayMember = "DisplayName";
                ComboBoxForExportConflictDealWay.EndUpdate();
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
            var _exportFile = TableDataManager.Instance().GetExportFileData();
            if (_exportFile == null || !_exportFile.GetHasInit())
            {
                MessageBox.Show("����Ŀ���ļ�δ׼���ã������õ���Ŀ���ļ���", "����");
                return;
            }
            var _sourceFile = TableDataManager.Instance().GetExportFileData();
            if (_sourceFile == null || !_sourceFile.GetHasInit())
            {
                MessageBox.Show("Դ�ļ�δ׼���� ��������Դ�ļ���", "����");
                return;
            }

            int _startRowIndex = 0;

            switch (TableDataManager.Instance().ExportWriteWayType)
            {
                case MainTypeDefine.ExportWriteWayType.OverWriteAll:
                {
                    _startRowIndex = _exportFile.GetContentStartRowIndex();
                    break;
                }
                case MainTypeDefine.ExportWriteWayType.Append:
                {
                    break;
                }
                default:
                {
                    MessageBox.Show($"����д���У�����δ��������ͣ�{TableDataManager.Instance().ExportWriteWayType}������");
                    break;
                }
            }
        }

        private void ComboBoxForExportWriteWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectValue = this.ComboBoxForExportWriteWay.SelectedValue as CommonDataForComboBox;
            if (_selectValue == null)
            {
                return;
            }

            TableDataManager.Instance().ExportWriteWayType = (MainTypeDefine.ExportWriteWayType)_selectValue.RealValue;
        }

        private void ComboBoxForExportConfigDealWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectValue = this.ComboBoxForExportWriteWay.SelectedValue as CommonDataForComboBox;
            if (_selectValue == null)
            {
                return;
            }

            TableDataManager.Instance().ExportConfigDealWayType = (MainTypeDefine.ExportConflictDealWayType)_selectValue.RealValue;
        }

        private void DataViewConfigForExportFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ����˱༭��ť�������༭���
            if (mKeyList == null || mKeyList.Count < 1 || e.RowIndex >= mKeyList.Count)
            {
                MessageBox.Show("ColumIndexForSetConnect �±���Ч������", "����");
                return;
            }

            var _columIndex = e.ColumnIndex;
            var _targetKey = mKeyList[e.RowIndex];
            switch (_columIndex)
            {
                case mColumIndexForSetConnect:
                {
                    KeyConnectEditForm _form = new KeyConnectEditForm();
                    _form.InitData(_targetKey);
                    if (_form.ShowDialog() == DialogResult.OK)
                    {
                        // ����ȥ���һ�£��� key �������Ƿ�
                        if (!CommonUtil.IsSafeNoCycleReferenceForKey(_targetKey))
                        {
                            return;
                        }
                    }

                    break;
                }
                case mColumIndexForSetIgnore:
                {
                    var _value = (bool)DataViewConfigForExportFile.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                    _targetKey.IsIgnore = _value;
                    break;
                }
                case mColumIndexForIsMainKey:
                {
                    var _value = (bool)DataViewConfigForExportFile.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                    _targetKey.IsMainKey = _value;
                    break;
                }
            }
        }
    }
}