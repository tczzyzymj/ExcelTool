﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ExcelTool
{
    public partial class ChooseFileConfigForm : Form
    {
        private LoadFileType mFileType = 0;

        public ChooseFileConfigForm()
        {
            InitializeComponent();
            this.DataGridViewForKeyFilter.AllowUserToAddRows = false;
        }

        private FileDataBase? mChooseFile = null;

        private CommonWorkSheetData mChooseSheet = null;

        private List<KeyData> mKeyDataList = new List<KeyData>();


        public string LastChooseFileAbsolutePath
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType">1表示读取的是exportfile , 2 表示读取的是 sourcefile，3表示普通文件加载</param>
        public void SetInitData(LoadFileType fileType)
        {
            mFileType = fileType;
        }

        private FileDataBase? InternalLoadFile(string absolutePath)
        {
            switch (mFileType)
            {
                case LoadFileType.ExportFile:
                {
                    mChooseFile = TableDataManager.Ins().TryLoadExportFile(absolutePath);
                    return mChooseFile;
                }
                case LoadFileType.SourceFile:
                {
                    mChooseFile = TableDataManager.Ins().TryLoadExportFile(absolutePath);
                    return mChooseFile;
                }
                case LoadFileType.NormalFile:
                {

                    mChooseFile = TableDataManager.Ins().TryLoadNormalFile(absolutePath);
                    return mChooseFile;
                }
            }

            return null;
        }

        private string InternalGetFileFilterStr()
        {
            switch (mFileType)
            {
                case LoadFileType.ExportFile:
                {
                    return "New excel|*.xlsx|Old excel|*.xls|csv|*.csv";
                }
                case LoadFileType.SourceFile:
                case LoadFileType.NormalFile:
                {
                    return "csv|*.csv|New excel|*.xlsx|Old excel|*.xls";
                }
            }

            return "所有文件|*.*";
        }

        private void InternalChangeNotice()
        {
            var _targetFile = mChooseFile;
            if (_targetFile is ExcelFileData)
            {
                LabelForNotice.Text = "注意，所选文件为 Excel 文件，下标是从 【 1 】开始！！！";
            }
            else if (_targetFile is CSVFileData)
            {
                LabelForNotice.Text = "注意，所选文件为 CSV 文件，下标是从 【 0 】开始！！！";
            }
        }

        private void BtnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openfileDialog = new OpenFileDialog();

            _openfileDialog.Filter = InternalGetFileFilterStr();
            _openfileDialog.Multiselect = false;
            if (_openfileDialog.ShowDialog() == DialogResult.OK)
            {
                InternalLoadFile(_openfileDialog.FileName);
                if (mChooseFile == null)
                {
                    MessageBox.Show($"加载目标文件：{_openfileDialog.FileName} 出错，请检查!", "错误");
                    return;
                }

                TextForFilePath.Text = _openfileDialog.FileName;
                var _workSheetList = mChooseFile.GetWorkSheetList();
                if (_workSheetList == null || _workSheetList.Count < 1)
                {
                    return;
                }
                LastChooseFileAbsolutePath = mChooseFile.GetFilePath();
                InternalChangeNotice();

                TextForFilePath.Text = _openfileDialog.FileName;

                PanelForConfigs.Enabled = true;

                TextBoxForKeyStartRow.Text = mChooseFile.GetKeyStartRowIndex().ToString();
                TextBoxForKeyStartColm.Text = mChooseFile.GetKeyStartColmIndex().ToString();
                TextBoxForContentStartRow.Text = mChooseFile.GetContentStartRowIndex().ToString();

                if (mChooseFile is ExcelFileData)
                {
                    LableForSplitSymbol.Visible = false;
                    this.TextBoxSplitSymbol.Visible = false;
                }
                else if (mChooseFile is CSVFileData)
                {
                    LableForSplitSymbol.Visible = true;
                    this.TextBoxSplitSymbol.Visible = true;

                    TextBoxSplitSymbol.Text = ",";
                }

                InternalInitForSheetComboBox(mChooseFile);
            }
        }

        private void InternalInitForSheetComboBox(FileDataBase targetFile)
        {
            var _targetFile = targetFile;
            if (_targetFile == null)
            {
                return;
            }

            var _workSheetList = _targetFile.GetWorkSheetList();
            if (_workSheetList == null || _workSheetList.Count < 1)
            {
                return;
            }

            ComboBoxForSelectSheet.BeginUpdate();
            ComboBoxForSelectSheet.DataSource = _workSheetList;
            ComboBoxForSelectSheet.ValueMember = "IndexInListForShow";
            ComboBoxForSelectSheet.DisplayMember = "DisplayName";
            ComboBoxForSelectSheet.SelectedIndex = 0;
            ComboBoxForSelectSheet.EndUpdate();
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
            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartRow.Text, out var _value);
            _targetFile.SetKeyStartRowIndex(_value);
        }

        private void TextBoxForKeyStartColm_TextChanged(object sender, EventArgs e)
        {
            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartColm.Text, out var _value);
            _targetFile.SetKeyStartColmIndex(_value);
        }

        private void TextBoxForContentStartRow_TextChanged(object sender, EventArgs e)
        {
            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                return;
            }
            int.TryParse(TextBoxForContentStartRow.Text, out var _value);
            _targetFile.SetContentStartRowIndex(_value);
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            InternalRefreshDataView();
        }

        private void InternalRefreshDataView()
        {
            var _targetFile = mChooseFile;
            var _selectItem = ComboBoxForSelectSheet.SelectedItem as CommonWorkSheetData;
            if (_selectItem == null || _targetFile == null)
            {
                MessageBox.Show("当前的 LoadFile 为空，请检查", "错误");
                return;
            }

            mChooseSheet = _selectItem;
            // 这里重置一下数据
            // 这里导出 key 供选择
            var _currentSheet = mChooseSheet;
            if (_currentSheet == null)
            {
                MessageBox.Show("当前的 Sheet 数据为空，请检查文件", "错误 ");
                return;
            }
            mKeyDataList = _currentSheet.GetKeyListData();

            DataGridViewForKeyFilter.Rows.Clear();

            for (int i = 0; i < mKeyDataList.Count; i++)
            {
                var _filter = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mKeyDataList[i]);
                DataGridViewForKeyFilter.Rows.Add(
                    CommonUtil.GetZM(mKeyDataList[i].GetKeyIndexForShow()),
                    mKeyDataList[i].KeyName,
                    _filter != null && _filter.Count > 0,
                    "设置"
                );
            }
        }

        private const int mInexForHasSetFilter = 2;

        private const int mIndexForSetButton = 3;
        private void DataGridViewForKeyFilter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                MessageBox.Show($"{DataGridViewForKeyFilter_CellContentClick} 错误，mChooseFile 为空");
                return;
            }

            switch (e.ColumnIndex)
            {
                case mInexForHasSetFilter:
                {
                    break;
                }
                case mIndexForSetButton:
                {
                    // 编辑过滤类型
                    FilterConfigForm _form = new FilterConfigForm();
                    var _targetKey = mKeyDataList[e.RowIndex];
                    _form.InitInfo(e.RowIndex, e.ColumnIndex, _targetKey);
                    if (_form.ShowDialog(this) == DialogResult.OK)
                    {
                        var _row = DataGridViewForKeyFilter.Rows;
                        var _cell = _row[e.RowIndex].Cells[mInexForHasSetFilter];
                        var _filterList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(_targetKey);
                        _cell.Value = _filterList != null && _filterList.Count > 0;
                    }

                    break;
                }
            }
        }

        private void ChooseFileConfigForm_Load(object sender, EventArgs e)
        {
            switch (mFileType)
            {
                case LoadFileType.ExportFile:
                {
                    mChooseFile = TableDataManager.Ins().GetExportFileData();
                    this.DataGridViewForKeyFilter.Columns[mInexForHasSetFilter].Visible = false;
                    this.DataGridViewForKeyFilter.Columns[mIndexForSetButton].Visible = false;
                    break;
                }
                case LoadFileType.SourceFile:
                {
                    mChooseFile = TableDataManager.Ins().GetSourceFileData();
                    break;
                }
                case LoadFileType.NormalFile:
                {
                    this.DataGridViewForKeyFilter.Columns[mInexForHasSetFilter].Visible = false;
                    this.DataGridViewForKeyFilter.Columns[mIndexForSetButton].Visible = false;
                    break;
                }
            }

            var _targetFile = mChooseFile;
            if (_targetFile != null)
            {
                InternalChangeNotice();

                PanelForConfigs.Enabled = true;

                TextBoxForKeyStartRow.Text = _targetFile.GetKeyStartRowIndex().ToString();
                TextBoxForKeyStartColm.Text = _targetFile.GetKeyStartColmIndex().ToString();
                TextBoxForContentStartRow.Text = _targetFile.GetContentStartRowIndex().ToString();

                if (_targetFile is CSVFileData _csvFile)
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

                TextForFilePath.Text = _targetFile.GetFilePath();

                InternalInitForSheetComboBox(_targetFile);
            }
            else
            {
                PanelForConfigs.Enabled = false;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                return;
            }

            // 这里导出 key 供选择
            var _currentSheet = _targetFile.GetCurrentWorkSheet();
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
            var _searchContentStr = this.TextBoxForSearch.Text.ToLower();

            for (int i = 0; i < this.DataGridViewForKeyFilter.Rows.Count; ++i)
            {
                var _keyName = DataGridViewForKeyFilter.Rows[i].Cells[1].Value as string;
                if (!string.IsNullOrEmpty(_keyName) && _keyName.ToLower().Contains(_searchContentStr))
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
            var _fileData = mChooseFile as CSVFileData;
            if (_fileData != null)
            {
                _fileData.SplitSymbol = TextBoxSplitSymbol.Text;
            }
        }

        private void BtnReloadKey_Click(object sender, EventArgs e)
        {
            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                MessageBox.Show("当前未加载文件，请检查！", "错误");
                return;
            }

            InternalRefreshDataView();
        }

        private void BtnShowHasSetFilter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.DataGridViewForKeyFilter.Rows.Count; ++i)
            {
                bool _show = false;
                if (_show)
                {
                    DataGridViewForKeyFilter.Rows[i].Visible = true;
                }
                else
                {
                    DataGridViewForKeyFilter.Rows[i].Visible = false;
                }
            }
        }
    }
}
