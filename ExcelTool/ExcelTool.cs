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
        private TableBaseData? mExportTargetFile = null; // 导出目标文件，其关联在里面设置

        public ExcelTool()
        {
            InitializeComponent();
        }

        public TableBaseData? TryChooseExportFile(string absoluteFilePath)
        {
            try
            {
                var _extension = Path.GetExtension(absoluteFilePath).ToLower();
                if (_extension.Equals(".xls") || _extension.Equals(".xlsx"))
                {

                    mExportTargetFile = new ExcelFileData();
                }
                else if (_extension.Equals(".csv"))
                {
                    mExportTargetFile = new CSVFileData();
                }
                else
                {
                    throw new Exception($"文件类型不匹配，请检查文件，目标文件路径为：{absoluteFilePath}");
                }

                if (!mExportTargetFile.DoLoadFile(absoluteFilePath))
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "报错");
            }

            return mExportTargetFile;
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
            _exportConfigForm.ShowDialog(this);
        }

        private void BtnAnalysis_Click(object sender, EventArgs e)
        {
            // 这里只分析一下数据
            if (this.mExportTargetFile == null)
            {
                MessageBox.Show("当前未选中需要导出的目标文件", "错误");
                return;
            }
            if (mExportTargetFile.GetWorkdSheet() == null)
            {
                MessageBox.Show("当前未选中需要导出的目标文件 Sheet", "错误");
                return;
            }
            var _workSheet = mExportTargetFile.GetWorkdSheet();
            var _keyList = _workSheet?.GetKeyListData();
            if (_keyList == null || _keyList.Count < 1)
            {
                MessageBox.Show("当前选中需要导出的目标文件 Sheet，未能解析出 Key，请检查配置或者呼叫程序员!", "错误");
                return;
            }
            for (int i = 0; i < _keyList.Count; ++i)
            {
                this.DataVewConfigForExportFile.Rows.Add(_keyList[i].GetKeyName(), string.Empty, null);
            }
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
    }
}