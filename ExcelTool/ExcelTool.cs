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
        /// <summary>
        /// �������ݰ�ť
        /// </summary>
        private const int mColumIndexForConfigDataSource = 3;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private const int mColumIndexForSetIgnore = 4;

        /// <summary>
        /// �Ƿ�Ϊ��KEY
        /// </summary>
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
            var mExportTargetFile = TableDataManager.Ins().GetExportFileData();
            if (mExportTargetFile == null)
            {
                MessageBox.Show("û�пɵ���������", "��ʾ");
                return;
            }

            //var _serializeContent = JsonConvert.SerializeObject(mExportTargetFile, Formatting.None);

            // ����������ʾ
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".json";
            var _timeformat = DateTime.Now.GetDateTimeFormats()[17].Replace("/", "_").Replace(" ", "_").Replace(":", "_");
            _saveFileDialog.FileName = $"{_timeformat}_ExportSetting.json";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //File.WriteAllText(_saveFileDialog.FileName, _serializeContent);

                MessageBox.Show("����ɹ�", " ��ʾ");
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("����������", "��ʾ");
            return;
            var mExportTargetFile = TableDataManager.Ins().GetExportFileData();
            // ��������
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                var _loadedFile = JsonConvert.DeserializeObject<FileDataBase>(_openfileDialog.FileName);
                if (_loadedFile != null)
                {
                    TableDataManager.Ins().TrySetExportTargetFile(_loadedFile);
                }
            }
        }

        private void BntChooseExportFile_Click(object sender, EventArgs e)
        {
            ChooseFileConfigForm _exportConfigForm = new ChooseFileConfigForm();
            _exportConfigForm.SetInitData(LoadFileType.ExportFile);
            if (_exportConfigForm.ShowDialog(this) == DialogResult.OK)
            {
                InternalAnalysisKey();
            }
        }

        private void InternalAnalysisKey()
        {
            var _exportFile = TableDataManager.Ins().GetExportFileData();

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
                    _keyList[i].KeyName,
                    "",
                    "��������",
                    _keyList[i].IsIgnore,
                    _keyList[i].IsMainKey
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
            ChooseFileConfigForm _form = new ChooseFileConfigForm();
            _form.SetInitData(LoadFileType.SourceFile);
            if (_form.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void StartExportBtn_Click(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Ins().GetExportFileData();
            if (_exportFile == null || !_exportFile.GetHasInit())
            {
                MessageBox.Show("����Ŀ���ļ�δ׼���ã������õ���Ŀ���ļ���", "����");
                return;
            }
            var _sourceFile = TableDataManager.Ins().GetSourceFileData();
            if (_sourceFile == null || !_sourceFile.GetHasInit())
            {
                MessageBox.Show("Դ�ļ�δ׼���� ��������Դ�ļ���", "����");
                return;
            }

            int _startRowIndex = 0;

            switch (TableDataManager.Ins().ExportWriteWayType)
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
                    MessageBox.Show($"����д���У�����δ��������ͣ�{TableDataManager.Ins().ExportWriteWayType}������");
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

            TableDataManager.Ins().ExportWriteWayType = (MainTypeDefine.ExportWriteWayType)_selectValue.RealValue;
        }

        private void ComboBoxForExportConfigDealWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectValue = this.ComboBoxForExportWriteWay.SelectedValue as CommonDataForComboBox;
            if (_selectValue == null)
            {
                return;
            }

            TableDataManager.Ins().ExportConfigDealWayType = (MainTypeDefine.ExportConflictDealWayType)_selectValue.RealValue;
        }

        private void DataViewConfigForExportFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // ����˱༭��ť�������༭���
            if (mKeyList == null || mKeyList.Count < 1 || e.RowIndex >= mKeyList.Count)
            {
                MessageBox.Show("ColumIndexForSetConnect �±���Ч������", "����");
                return;
            }

            var _columIndex = e.ColumnIndex;
            var _fromKey = mKeyList[e.RowIndex];
            switch (_columIndex)
            {
                case mColumIndexForConfigDataSource:
                {
                    var _sourceFile = TableDataManager.Ins().GetSourceFileData();
                    if (_sourceFile == null)
                    {
                        MessageBox.Show("������������Դ�ļ���", "����");
                        return;
                    }

                    KeyConnectEditForm _form = new KeyConnectEditForm();
                    if (!TableDataManager.Ins().ExportKeyActionMap.TryGetValue(_fromKey, out var _action))
                    {
                        _action = new SourceAction();
                        TableDataManager.Ins().ExportKeyActionMap.Add(_fromKey, _action);
                    }
                    _form.InitData(_action);
                    if (_form.ShowDialog() == DialogResult.OK)
                    {
                        // TODO ����ȥ���һ�£����Ƿ���ѭ������
                        //if (!CommonUtil.IsSafeNoCycleReferenceForKey(_targetKey))
                        //{
                        //    _targetKey.ClearNextConnectKey();
                        //    return;
                        //}
                    }

                    break;
                }
                case mColumIndexForSetIgnore:
                {
                    var _value = (bool)DataViewConfigForExportFile.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                    _fromKey.IsIgnore = _value;
                    if (_value)
                    {
                        _fromKey.IsMainKey = false;
                        DataViewConfigForExportFile.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = false;
                    }

                    break;
                }
                case mColumIndexForIsMainKey:
                {
                    // Ŀǰ��KEYֻ��һ������������Ϊ��
                    var _value = (bool)DataViewConfigForExportFile.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                    _fromKey.IsMainKey = _value;
                    if (_value)
                    {
                        _fromKey.IsIgnore = false;
                        DataViewConfigForExportFile.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = false;
                    }

                    for (int i = 0; i < this.DataViewConfigForExportFile.RowCount; ++i)
                    {
                        if (i == e.RowIndex)
                        {
                            continue;
                        }

                        this.DataViewConfigForExportFile.Rows[i].Cells[e.ColumnIndex].Value = false;
                    }

                    break;
                }
            }
        }
    }
}