using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelTool
{
    public partial class ExportFileConfigForm : FormBase
    {
        public ExportFileConfigForm()
        {
            InitializeComponent();
        }

        private void BtnChooseExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            _openfileDialog.Filter = "New excel|*.xlsx|csv|*.csv|Old excel|*.xls";
            _openfileDialog.Multiselect = false;
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                var _owner = this.Owner as ExcelTool;
                if (_owner == null)
                {
                    this.Close();
                    MessageBox.Show("父窗口不是 ExcelTool ，请检查!", "错误");
                    return;
                }

                var _exportFile = TableDataManager.Instance().TryChooseExportFile(_openfileDialog.FileName);
                if (_exportFile == null)
                {
                    MessageBox.Show($"加载目标文件：{_openfileDialog.FileName} 出错，请检查!", "错误");
                    return;
                }

                TextBoxForKeyStartRow_TextChanged(null, null);
                TextBoxForKeyStartColm_TextChanged(null, null);
                TextBoxForContentStartRow_TextChanged(null, null);

                TextForExportFilePath.Text = _openfileDialog.FileName;
                var _workSheetList = _exportFile.GetWorkSheetList();
                if (_workSheetList == null || _workSheetList.Count < 1)
                {
                    return;
                }

                ComboBoxForSelectSheet.BeginUpdate();
                this.ComboBoxForSelectSheet.DataSource = _workSheetList;
                this.ComboBoxForSelectSheet.ValueMember = "IndexInListForShow";
                this.ComboBoxForSelectSheet.DisplayMember = "DisplayName";
                this.ComboBoxForSelectSheet.SelectedIndex = 0;
                ComboBoxForSelectSheet.EndUpdate();
                ComboBoxForSelectSheet_SelectedIndexChanged(null, null);
            }
        }

        private void TextBoxForKeyStartRow_TextChanged(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Instance().GetExportFileData();
            if (_exportFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartRow.Text, out var _value);
            _exportFile.SetKeyStartRowIndex(_value);
        }

        private void TextBoxCommonProcess_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) || e.KeyChar == 8) // e.KeyChar == 8 是退格键
            {
                e.Handled = true;
            }
        }

        private void TextBoxForKeyStartColm_TextChanged(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Instance().GetExportFileData();
            if (_exportFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartColm.Text, out var _value);
            _exportFile.SetKeyStartColmIndex(_value);
        }

        private void TextBoxForContentStartRow_TextChanged(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Instance().GetExportFileData();
            if (_exportFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForContentStartRow.Text, out var _value);
            _exportFile.SetContentStartRowIndex(_value);
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectItem = this.ComboBoxForSelectSheet.SelectedItem as CommonWorkSheetData;
            if (_selectItem != null)
            {
                var _exportFile = TableDataManager.Instance().GetExportFileData();
                _exportFile?.TryChooseSheet(_selectItem);
            }
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            var _exportFile = TableDataManager.Instance().GetExportFileData();
            // 检查一下有效性
            if (_exportFile != null)
            {
                if (!_exportFile.IsCurrentSheetValid())
                {
                    MessageBox.Show("当前选中 Sheet 无效，请重新配置", "错误");
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ExportFileConfigForm_Load(object sender, EventArgs e)
        {
            var _owner = this.Owner as ExcelTool;
            if (_owner == null)
            {
                this.Close();
                MessageBox.Show("父窗口不是 ExcelTool ，请检查!", "错误");
                return;
            }
            var _exportFIle = TableDataManager.Instance().GetExportFileData();
            _exportFIle = TableDataManager.Instance().GetExportFileData();
            if (_exportFIle != null)
            {
                TextBoxForKeyStartRow.Text = _exportFIle.GetKeyStartRowIndex().ToString();
                TextBoxForKeyStartColm.Text = _exportFIle.GetKeyStartColmIndex().ToString();
                TextBoxForContentStartRow.Text = _exportFIle.GetContentStartRowIndex().ToString();
                TextForExportFilePath.Text = _exportFIle.GetFilePath();
            }
        }
    }
}
