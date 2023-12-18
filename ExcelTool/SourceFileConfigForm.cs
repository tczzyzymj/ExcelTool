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
    public partial class SourceFileConfigForm : Form
    {
        private TableBaseData? mLoadedFile = null;

        public SourceFileConfigForm()
        {
            InitializeComponent();
            this.DataGridViewForKeyFilter.AllowUserToOrderColumns = false;
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

                mLoadedFile = _owner.TryChooseExportFile(_openfileDialog.FileName);
                if (mLoadedFile == null)
                {
                    MessageBox.Show($"加载目标文件：{_openfileDialog.FileName} 出错，请检查!", "错误");
                    return;
                }

                TextBoxForKeyStartRow_TextChanged(null, null);
                TextBoxForKeyStartColm_TextChanged(null, null);
                TextBoxForContentStartRow_TextChanged(null, null);

                TextForExportFilePath.Text = _openfileDialog.FileName;
                var _workSheetList = mLoadedFile.GetWorkSheetList();
                if (_workSheetList == null || _workSheetList.Count < 1)
                {
                    return;
                }

                ComboBoxForSelectSheet.BeginUpdate();
                this.ComboBoxForSelectSheet.DataSource = _workSheetList;
                this.ComboBoxForSelectSheet.ValueMember = "IndexInList";
                this.ComboBoxForSelectSheet.DisplayMember = "DisplayName";
                this.ComboBoxForSelectSheet.SelectedIndex = 0;
                ComboBoxForSelectSheet.EndUpdate();
                ComboBoxForSelectSheet_SelectedIndexChanged(null, null);

                // 这里导出 key 供选择
                var _currentSheet = mLoadedFile.GetCurrentWorkSheet();
                if (_currentSheet != null)
                {
                    var _keyListData = _currentSheet.GetKeyListData();
                    if (_keyListData != null)
                    {
                        for (int i = 0; i < _keyListData.Count; i++)
                        {
                            this.DataGridViewForKeyFilter.Rows.Add(_keyListData[i].GetKeyName(), false, "编辑");
                        }
                    }
                }
            }
        }

        private void TextBoxCommonProcess_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) || e.KeyChar == 8) // e.KeyChar == 8 是退格键
            {
                e.Handled = true;
            }
        }

        private void TextBoxForKeyStartRow_TextChanged(object sender, EventArgs e)
        {
            if (mLoadedFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartRow.Text, out var _value);
            mLoadedFile.SetKeyStartRowIndex(_value);
        }

        private void TextBoxForKeyStartColm_TextChanged(object sender, EventArgs e)
        {
            if (mLoadedFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartColm.Text, out var _value);
            mLoadedFile.SetKeyStartColmIndex(_value);
        }

        private void TextBoxForContentStartRow_TextChanged(object sender, EventArgs e)
        {
            if (mLoadedFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForContentStartRow.Text, out var _value);
            mLoadedFile.SetContentStartRowIndex(_value);
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectItem = this.ComboBoxForSelectSheet.SelectedItem as CommonWorkSheetData;
            if (_selectItem != null)
            {
                mLoadedFile?.TryChooseSheet(_selectItem);
            }
        }

        private void DataGridViewForKeyFilter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                {
                    // 确认选择成为过滤KEY

                    break;
                }
                case 2:
                {
                    // 编辑过滤类型
                    break;
                }
            }
        }

        private void SourceFileConfigForm_Load(object sender, EventArgs e)
        {
            var _owner = this.Owner as ExcelTool;
            if (_owner == null)
            {
                this.Close();
                MessageBox.Show("父窗口不是 ExcelTool ，请检查!", "错误");
                return;
            }
            mLoadedFile = _owner.GetSourceFileData();
            if (mLoadedFile != null)
            {
                TextBoxForKeyStartRow.Text = mLoadedFile.GetKeyStartRowIndex().ToString();
                TextBoxForKeyStartColm.Text = mLoadedFile.GetKeyStartColmIndex().ToString();
                TextBoxForContentStartRow.Text = mLoadedFile.GetContentStartRowIndex().ToString();
                TextForExportFilePath.Text = mLoadedFile.GetFilePath();
            }
        }
    }
}
