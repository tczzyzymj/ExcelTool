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
        private TableBaseData? mExportTargetFile = null; // 导出目标文件，其关联在里面设置

        private TableBaseData? mSourceFile = null; // 数据源文件

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
                    throw new Exception($"文件类型不匹配，请检查文件，目标文件路径为：{absoluteFilePath}");
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
                MessageBox.Show(ex.ToString(), "报错");
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
            MessageBox.Show("功能制作中", "提示");
            return;
            // 导出配置
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
            // 导入配置
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
            // 这里只分析一下数据
            if (this.mExportTargetFile == null)
            {
                MessageBox.Show("当前未选中需要导出的目标文件", "错误");
                return;
            }

            var _workSheet = mExportTargetFile.GetCurrentWorkSheet();
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

            mHasAnalysData = true;
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
                ComboBoxForExportWriteWay.DataSource = _dataForExportWay;
                ComboBoxForExportWriteWay.ValueMember = "RealValue";
                ComboBoxForExportWriteWay.DisplayMember = "DisplayName";
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
                MessageBox.Show("导出目标文件未准备好，请配置导出目标文件！", "错误");
                return;
            }
            if (mSourceFile == null || !mSourceFile.GetHasInit())
            {
                MessageBox.Show("源文件未准备好 ，请配置源文件！", "错误");
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
                // 点击了编辑按钮，弹出编辑相关
                var _rowIndex = e.RowIndex;
            }
        }
    }
}