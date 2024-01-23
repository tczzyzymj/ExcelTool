using ExcelTool;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class ExcelTool : Form
    {
        private List<KeyData> mKeyList = new List<KeyData>();

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
#pragma warning disable CS0162 // Unreachable code detected
            var mExportTargetFile = TableDataManager.Ins().GetExportFileData();

            if (mExportTargetFile == null)
            {
                MessageBox.Show("没有可导出的配置", "提示");
                return;
            }

            //var _serializeContent = JsonConvert.SerializeObject(mExportTargetFile, Formatting.None);

            // 弹出保存提示
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".json";
            var _timeformat = DateTime.Now.GetDateTimeFormats()[17].Replace("/", "_").Replace(" ", "_").Replace(":", "_");
            _saveFileDialog.FileName = $"{_timeformat}_ExportSetting.json";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //File.WriteAllText(_saveFileDialog.FileName, _serializeContent);

                MessageBox.Show("保存成功", " 提示");
            }
#pragma warning restore CS0162 // Unreachable code detected
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能制作中", "提示");
            return;
#pragma warning disable CS0162 // Unreachable code detected
            var mExportTargetFile = TableDataManager.Ins().GetExportFileData();

            // 导入配置
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                var _loadedFile = JsonConvert.DeserializeObject<FileDataBase>(_openfileDialog.FileName);
                if (_loadedFile != null)
                {

                }
            }
#pragma warning restore CS0162 // Unreachable code detected
        }

        private void BntChooseExportFile_Click(object sender, EventArgs e)
        {
            ChooseFileConfigForm _exportConfigForm = new ChooseFileConfigForm();
            _exportConfigForm.SetInitData(LoadFileType.ExportFile, TableDataManager.Ins().GetExportSheet());
            if (_exportConfigForm.ShowDialog(this) == DialogResult.OK)
            {
                InternalAnalysisKey();
            }
        }

        private void InternalAnalysisKey()
        {
            var _exportFile = TableDataManager.Ins().GetExportFileData();

            // 这里只分析一下数据
            if (_exportFile == null)
            {
                MessageBox.Show("当前未选中需要导出的目标文件", "错误");
                return;
            }

            var _workSheet = TableDataManager.Ins().GetExportSheet();
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
                    _keyList[i].KeyName,
                    "",
                    false,
                    "配置数据",
                    _keyList[i].IsIgnore,
                    _keyList[i].IsMainKey
                );
            }
            InternalRefreshForHasConfigKey();
        }

        private void InternalRefreshForHasConfigKey()
        {
            var _rows = DataViewConfigForExportFile.Rows;
            var _actionMap = TableDataManager.Ins().ExportKeyActionMap;
            for (int i = 0; i < _rows.Count; ++i)
            {
                _actionMap.TryGetValue(mKeyList[i], out var _sequenceAction);
                bool _hasConfig = _sequenceAction != null && _sequenceAction.ActionSequence.Count > 0;
                _rows[i].Cells[mColumIndexForHasConfigKey].Value = _hasConfig;
                _rows[i].Cells[mColumIndexForSetIgnore].Value = !_hasConfig;
                if (_hasConfig)
                {
                    mKeyList[i].IsIgnore = false;
                }
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
                    _tempone.DisplayName = "用旧数据";
                    _dataForExportWay.Add(_tempone);
                }

                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 1;
                    _tempone.DisplayName = "用新数据(空数据跳过)";
                    _dataForExportWay.Add(_tempone);
                }

                {
                    CommonDataForComboBox _tempone = new CommonDataForComboBox();
                    _tempone.RealValue = 2;
                    _tempone.DisplayName = "用新数据(空数据覆盖)";
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
            ChooseFileConfigForm _sourceFileChooseForm = new ChooseFileConfigForm();
            _sourceFileChooseForm.SetInitData(LoadFileType.SourceFile, TableDataManager.Ins().GetSourceSheet());
            if (_sourceFileChooseForm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void StartExportBtn_Click(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Ins().GetExportFileData();
            if (_exportFile == null || !_exportFile.GetHasInit())
            {
                MessageBox.Show("导出目标文件未准备好，请配置导出目标文件！", "错误");
                return;
            }
            var _sourceFile = TableDataManager.Ins().GetSourceFileData();
            if (_sourceFile == null || !_sourceFile.GetHasInit())
            {
                MessageBox.Show("源文件未准备好 ，请配置源文件！", "错误");
                return;
            }

            try
            {
                TableDataManager.Ins().StartExportData();
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }
        }

        private void ComboBoxForExportWriteWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectValue = this.ComboBoxForExportWriteWay.SelectedItem as CommonDataForComboBox;
            if (_selectValue == null)
            {
                return;
            }

            TableDataManager.Ins().ExportWriteWayType = (MainTypeDefine.ExportWriteWayType)_selectValue.RealValue;
        }

        private void ComboBoxForExportConflictDealWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectValue = this.ComboBoxForExportConflictDealWay.SelectedItem as CommonDataForComboBox;
            if (_selectValue == null)
            {
                return;
            }

            TableDataManager.Ins().ExportConfigDealWayType = (MainTypeDefine.ExportConflictDealWayType)_selectValue.RealValue;
        }

        /// <summary>
        /// 是否已经配置key
        /// </summary>
        private const int mColumIndexForHasConfigKey = 3;
        /// <summary>
        /// 配置数据按钮
        /// </summary>
        private const int mColumIndexForConfigDataSource = 4;
        /// <summary>
        /// 是否忽略
        /// </summary>
        private const int mColumIndexForSetIgnore = 5;

        /// <summary>
        /// 是否为主KEY
        /// </summary>
        private const int mColumIndexForIsMainKey = 6;

        private void DataViewConfigForExportFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }

            // 点击了编辑按钮，弹出编辑相关
            if (mKeyList == null || mKeyList.Count < 1 || e.RowIndex >= mKeyList.Count)
            {
                MessageBox.Show("ColumIndexForSetConnect 下标无效，请检查", "错误");
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
                        MessageBox.Show("请先配置数据源文件！", "错误");
                        return;
                    }

                    var _sourceSheet = TableDataManager.Ins().GetSourceSheet();
                    if (_sourceSheet == null)
                    {
                        MessageBox.Show("请先选择源文件 Sheet", "错误");
                        return;
                    }

                    SequenceActionConnectEditForm _form = new SequenceActionConnectEditForm();
                    if (!TableDataManager.Ins().ExportKeyActionMap.TryGetValue(_fromKey, out var _action))
                    {
                        _action = new SequenceAction();
                        TableDataManager.Ins().ExportKeyActionMap.Add(_fromKey, _action);
                    }
                    _action.WorkSheetData = TableDataManager.Ins().GetSourceSheet();
                    _form.InitData(_action);
                    if (_form.ShowDialog() == DialogResult.OK)
                    {
                        InternalRefreshForHasConfigKey();
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
                    // 目前主KEY只有一个其他的设置为空
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