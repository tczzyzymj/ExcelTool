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
    public partial class SourceFileConfigForm : FormBase
    {
        public SourceFileConfigForm()
        {
            InitializeComponent();
            this.DataGridViewForKeyFilter.AllowUserToAddRows = false;
        }

        protected override void OnProcessEvent(params object[] args)
        {
            base.OnProcessEvent(args);

            if (args == null || args.Length < 1)
            {
                return;
            }
        }

        private void BtnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openfileDialog = new OpenFileDialog();
            _openfileDialog.Filter = "csv|*.csv|New excel|*.xlsx|Old excel|*.xls";
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

                var _sourceFile = TableDataManager.Instance().TryChooseSourceFile(_openfileDialog.FileName);
                if (_sourceFile == null)
                {
                    MessageBox.Show($"加载目标文件：{_openfileDialog.FileName} 出错，请检查!", "错误");
                    return;
                }

                TextForFilePath.Text = _openfileDialog.FileName;
                var _workSheetList = _sourceFile.GetWorkSheetList();
                if (_workSheetList == null || _workSheetList.Count < 1)
                {
                    return;
                }

                PanelForConfigs.Visible = true;

                if (_sourceFile is ExcelFileData)
                {
                    LableForSplitSymbol.Visible = false;
                    this.TextBoxSplitSymbol.Visible = false;

                    TextBoxForKeyStartRow.Text = "2";
                    TextBoxForKeyStartColm.Text = "1";
                    TextBoxForContentStartRow.Text = "4";
                    TextBoxForIDColumIndex.Text = "1";
                }
                else if (_sourceFile is CSVFileData)
                {
                    LableForSplitSymbol.Visible = true;
                    this.TextBoxSplitSymbol.Visible = true;

                    TextBoxForKeyStartRow.Text = "0";
                    TextBoxForKeyStartColm.Text = "0";
                    TextBoxForContentStartRow.Text = "3";
                    TextBoxForIDColumIndex.Text = "0";

                    TextBoxSplitSymbol.Text = ",";
                }

                TextBoxForKeyStartRow_TextChanged(null, null);
                TextBoxForKeyStartColm_TextChanged(null, null);
                TextBoxForContentStartRow_TextChanged(null, null);
                TextBoxForIDColumIndex_TextChanged(null, null);

                ComboBoxForSelectSheet.BeginUpdate();
                ComboBoxForSelectSheet.DataSource = _workSheetList;
                ComboBoxForSelectSheet.ValueMember = "IndexInListForShow";
                ComboBoxForSelectSheet.DisplayMember = "DisplayName";
                ComboBoxForSelectSheet.SelectedIndex = 0;
                ComboBoxForSelectSheet.EndUpdate();
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
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartRow.Text, out var _value);
            _sourceFile.SetKeyStartRowIndex(_value);
        }

        private void TextBoxForKeyStartColm_TextChanged(object sender, EventArgs e)
        {
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartColm.Text, out var _value);
            _sourceFile.SetKeyStartColmIndex(_value);
        }

        private void TextBoxForContentStartRow_TextChanged(object sender, EventArgs e)
        {
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForContentStartRow.Text, out var _value);
            _sourceFile.SetContentStartRowIndex(_value);
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            var _selectItem = ComboBoxForSelectSheet.SelectedItem as CommonWorkSheetData;
            if (_selectItem == null || _sourceFile == null)
            {
                MessageBox.Show("当前的 LoadFile 为空，请检查", "错误");
                return;
            }

            if (!_sourceFile.TryChooseSheet(_selectItem))
            {
                MessageBox.Show("选择 Sheet 数据失败，请检查文件，看所选 Sheet 是否有数据", "错误");
                return;
            }

            DataGridViewForKeyFilter.Rows.Clear();

            // 这里重置一下数据
            // 这里导出 key 供选择
            var _currentSheet = _sourceFile.GetCurrentWorkSheet();
            if (_currentSheet == null)
            {
                MessageBox.Show("当前的 Sheet 数据为空，请检查文件", "错误 ");
                return;
            }

            var _keyListData = _currentSheet.GetKeyListData();
            if (_keyListData != null)
            {
                for (int i = 0; i < _keyListData.Count; i++)
                {
                    DataGridViewForKeyFilter.Rows.Add(
                        CommonUtil.GetZM(_keyListData[i].GetKeyIndexForShow()),
                        _keyListData[i].GetKeyName(),
                        _keyListData[i].GetFilterFuncList().Count > 0,
                        "设置"
                    );
                }
            }
        }

        private void DataGridViewForKeyFilter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null)
            {
                return;
            }

            switch (e.ColumnIndex)
            {
                case 2:
                {
                    // 确认选择成为过滤KEY
                    var _targetGridView = sender as DataGridView;
                    if (_targetGridView != null)
                    {
                        var _cell = _targetGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        var _isSelect = (bool)(_cell.EditedFormattedValue);
                    }

                    break;
                }
                case 3:
                {
                    // 编辑过滤类型
                    var _currentSheet = _sourceFile.GetCurrentWorkSheet();
                    if (_currentSheet != null)
                    {
                        var _keyListData = _currentSheet.GetKeyListData();
                        FilterConfigForm _form = new FilterConfigForm();
                        _form.InitInfo(e.RowIndex, e.ColumnIndex, _keyListData[e.RowIndex]);
                        if (_form.ShowDialog(this) == DialogResult.OK)
                        {
                            var _row = DataGridViewForKeyFilter.Rows;
                            var _cell = _row[e.RowIndex].Cells[2] as DataGridViewCheckBoxCell;
                            _cell.Value = true;
                            DataGridViewForKeyFilter.UpdateCellValue(e.ColumnIndex, e.RowIndex);
                        }
                    }

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
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile != null)
            {
                PanelForConfigs.Visible = true;
                TextBoxForKeyStartRow.Text = _sourceFile.GetKeyStartRowIndex().ToString();
                TextBoxForKeyStartColm.Text = _sourceFile.GetKeyStartColmIndex().ToString();
                TextBoxForContentStartRow.Text = _sourceFile.GetContentStartRowIndex().ToString();
                TextBoxForIDColumIndex.Text = _sourceFile.IDIndex.ToString();

                if (_sourceFile is CSVFileData _csvFile)
                {
                    LableForSplitSymbol.Visible = true;
                    TextBoxSplitSymbol.Visible = true;
                    TextBoxSplitSymbol.Text = _csvFile.SplitSymbol;
                }
                else
                {
                    LableForSplitSymbol.Visible = false;
                    TextBoxSplitSymbol.Visible = false;
                }

                TextForFilePath.Text = _sourceFile.GetFilePath();
            }
            else
            {
                PanelForConfigs.Visible = false;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null)
            {
                return;
            }

            // 这里导出 key 供选择
            var _currentSheet = _sourceFile.GetCurrentWorkSheet();
            if (_currentSheet == null)
            {
                MessageBox.Show("没有 workSheet 数据，请检查", "错误");
                return;
            }

            var _keyListData = _currentSheet.GetKeyListData();
            if (_keyListData == null)
            {
                MessageBox.Show("没有 Keylist，请检查", "错误");
                return;
            }
            var _serachContent = this.TextBoxForSearch.Text.ToLower();

            for (int i = 0; i < this.DataGridViewForKeyFilter.Rows.Count; ++i)
            {
                var _keyName = DataGridViewForKeyFilter.Rows[i].Cells[1].Value as string;
                if (!string.IsNullOrEmpty(_keyName) && _keyName.ToLower().Contains(_serachContent))
                {
                    DataGridViewForKeyFilter.Rows[i].Visible = true;
                }
                else
                {
                    DataGridViewForKeyFilter.Rows[i].Visible = false;
                }
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.DataGridViewForKeyFilter.Rows.Count; ++i)
            {
                DataGridViewForKeyFilter.Rows[i].Visible = true;
            }
        }

        private void BtnFinishConfig_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void TextBoxSplitSymbol_TextChanged(object sender, EventArgs e)
        {
            var _fileData = TableDataManager.Instance().GetSourceFileData() as CSVFileData;
            if (_fileData != null)
            {
                _fileData.SplitSymbol = TextBoxSplitSymbol.Text;
            }
        }

        private void TextBoxForIDColumIndex_TextChanged(object sender, EventArgs e)
        {
            var _sourceFile = TableDataManager.Instance().GetSourceFileData();
            if (_sourceFile == null)
            {
                MessageBox.Show("无法获取数据， source File 未加载，请检查!", "错误");
                return;
            }

            if (int.TryParse(TextBoxForIDColumIndex.Text, out var _targetValue))
            {
                _sourceFile.IDIndex = _targetValue;
            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }
    }
}
