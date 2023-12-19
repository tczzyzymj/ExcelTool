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
            MessageBox.Show("功能制作中", "提示");
            return;
            // 导出配置
            var mExportTargetFile = TableDataManager.Instance().GetExportFileData();
            if (mExportTargetFile == null)
            {
                MessageBox.Show("没有可导出的配置", "提示");
                return;
            }

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
            MessageBox.Show("功能制作中", "提示");
            return;
            var mExportTargetFile = TableDataManager.Instance().GetExportFileData();
            // 导入配置
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
            ChooseFileConfigForm _exportConfigForm = new ChooseFileConfigForm();
            _exportConfigForm.SetInitData(1);
            if (_exportConfigForm.ShowDialog(this) == DialogResult.OK)
            {
                InternalAnalysisKey();
            }
        }

        private void InternalAnalysisKey()
        {
            var _exportFile = TableDataManager.Instance().GetExportFileData();

            // 这里只分析一下数据
            if (_exportFile == null)
            {
                MessageBox.Show("当前未选中需要导出的目标文件", "错误");
                return;
            }

            var _workSheet = _exportFile.GetCurrentWorkSheet();
            if (_workSheet == null)
            {
                MessageBox.Show("当前未选中需要导出的目标文件 Sheet", "错误");
                return;
            }

            var _keyList = _workSheet.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                MessageBox.Show("当前选中需要导出的目标文件 Sheet，未能解析出 Key，请检查配置或者呼叫程序员!", "错误");
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
                    "设置"
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
                    _tempone.DisplayName = "尾部添加";
                    _dataForExportWay.Add(_tempone);
                }

                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 1;
                    _tempone.DisplayName = "全部重写";
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
                    _tempone.DisplayName = "使用旧数据";
                    _dataForExportWay.Add(_tempone);
                }

                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 1;
                    _tempone.DisplayName = "使用新数据";
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
            _form.SetInitData(2);
            if (_form.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void StartExportBtn_Click(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Instance().GetExportFileData();
            if (_exportFile == null || !_exportFile.GetHasInit())
            {
                MessageBox.Show("导出目标文件未准备好，请配置导出目标文件！", "错误");
                return;
            }
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null || !_sourceFile.GetHasInit())
            {
                MessageBox.Show("源文件未准备好 ，请配置源文件！", "错误");
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
                    MessageBox.Show($"导出写入中，存在未处理的类型：{TableDataManager.Instance().ExportWriteWayType}，请检查");
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
            // 点击了编辑按钮，弹出编辑相关
            if (mKeyList == null || mKeyList.Count < 1 || e.RowIndex >= mKeyList.Count)
            {
                MessageBox.Show("ColumIndexForSetConnect 下标无效，请检查", "错误");
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
                        // 这里去检测一下，看 key 的引用是否
                        if (!CommonUtil.IsSafeNoCycleReferenceForKey(_targetKey))
                        {
                            _targetKey.ClearNextConnectKey();
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