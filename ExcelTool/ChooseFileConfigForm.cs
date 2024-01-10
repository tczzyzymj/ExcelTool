using System;
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
        private LoadFileType mFromFileType = 0;

        public ChooseFileConfigForm()
        {
            InitializeComponent();
            this.DataGridViewForKeyFilter.AllowUserToAddRows = false;
        }

        private FileDataBase? mChooseFile = null;

        private CommonWorkSheetData? mChooseSheet = null;

        private List<KeyData> mKeyDataList = new List<KeyData>();

        public FileDataBase? GetChooseFile()
        {
            return mChooseFile;
        }

        public CommonWorkSheetData? GetChooseSheet()
        {
            return mChooseSheet;
        }

        public List<int> mSelectKeyIndexList = new List<int>();

        public List<int> GetSelectKeyIndexList()
        {
            return mSelectKeyIndexList;
        }

        private CommonWorkSheetData? mFromSheet = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType">1表示读取的是exportfile , 2 表示读取的是 sourcefile，3表示普通文件加载</param>
        public void SetInitData(LoadFileType fileType, CommonWorkSheetData? fromSheet)
        {
            mFromFileType = fileType;
            mFromSheet = fromSheet;
        }

        private FindAction? mFromAction = null;
        public void SetFindAction(FindAction targetAction)
        {
            mFromAction = targetAction;
            mFromFileType = LoadFileType.SetSearchKey;
            mFromSheet = targetAction.GetSearchWorkSheetData();
            mSelectKeyIndexList.Clear();
            mSelectKeyIndexList.AddRange(targetAction.SearchKeyIndexList);
        }

        private FileDataBase? InternalLoadFile(string absolutePath)
        {
            switch (mFromFileType)
            {
                case LoadFileType.ExportFile:
                {
                    return TableDataManager.Ins().TryLoadExportFile(absolutePath);

                }
                case LoadFileType.SourceFile:
                {
                    return TableDataManager.Ins().TryLoadSourceFile(absolutePath);
                }
                case LoadFileType.SourceFileFilterAction:
                {
                    return TableDataManager.Ins().TryLoadSourceFilterFile(absolutePath);
                }
                case LoadFileType.SetSearchKey:
                case LoadFileType.NormalFile:
                {
                    return TableDataManager.Ins().TryLoadNormalFile(absolutePath);
                }
                default:
                {
                    CommonUtil.ShowError("加载文件失败，未处理的类型 : " + mFromFileType.ToString());
                    break;
                }
            }

            return null;
        }

        private string InternalGetFileFilterStr()
        {
            switch (mFromFileType)
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
                var _tempFile = InternalLoadFile(_openfileDialog.FileName);
                if (_tempFile == null)
                {
                    MessageBox.Show($"加载目标文件：{_openfileDialog.FileName} 出错，请检查!", "错误");
                    return;
                }

                InternalChooseFile(_tempFile, true);

                if (mChooseFile == null)
                {
                    MessageBox.Show($"加载目标文件：{_openfileDialog.FileName} 出错，请检查!", "错误");
                    return;
                }

                TextForFilePath.Text = _openfileDialog.FileName;
                InternalChangeNotice();

                TextForFilePath.Text = _openfileDialog.FileName;

                PanelForConfigs.Enabled = true;

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
            }
        }

        private void InternalChooseFile(FileDataBase targetFile, bool refreshFileComboBox)
        {
            if (targetFile == null)
            {
                MessageBox.Show("InternalChooseFile， targetFile 为空", "错误");
                return;
            }

            if (mChooseFile != null && mChooseFile == targetFile)
            {
                return;
            }

            mChooseFile = targetFile;
            InternalInitForSheetComboBox();

            if (refreshFileComboBox)
            {
                if (mFromFileType != LoadFileType.ExportFile || mFromFileType != LoadFileType.SourceFile)
                {
                    InternalRefreshForLoadFiles();
                }
            }
        }

        private void InternalInitForSheetComboBox()
        {
            var _targetFile = mChooseFile;
            if (_targetFile != null)
            {
                var _workSheetList = _targetFile.GetWorkSheetList();
                if (_workSheetList != null && _workSheetList.Count > 0)
                {
                    ComboBoxForSelectSheet.BeginUpdate();
                    ComboBoxForSelectSheet.DataSource = null;
                    ComboBoxForSelectSheet.Items.Clear();
                    ComboBoxForSelectSheet.DataSource = _workSheetList;
                    ComboBoxForSelectSheet.ValueMember = "IndexInListForShow";
                    ComboBoxForSelectSheet.DisplayMember = "DisplayName";
                    if (mFromSheet != null)
                    {
                        var _index = _targetFile.GetWorkSheetList().IndexOf(mFromSheet);
                        if (_index >= 0 && _workSheetList != null && _workSheetList.Count > 0)
                        {
                            ComboBoxForSelectSheet.SelectedIndex = _index;
                        }
                    }
                    else
                    {
                        ComboBoxForSelectSheet.SelectedIndex = 0;
                    }

                    ComboBoxForSelectSheet.EndUpdate();

                    return;
                }
            }

            ComboBoxForSelectSheet.BeginUpdate();
            ComboBoxForSelectSheet.DataSource = null;
            ComboBoxForSelectSheet.Items?.Clear();
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
            var _targetSheet = mChooseSheet;
            if (_targetSheet == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartRow.Text, out var _value);
            _targetSheet.SetKeyStartRowIndex(_value);
        }

        private void TextBoxForKeyStartColm_TextChanged(object sender, EventArgs e)
        {
            var _targetSheet = mChooseSheet;
            if (_targetSheet == null)
            {
                return;
            }
            int.TryParse(TextBoxForKeyStartColm.Text, out var _value);
            _targetSheet.SetKeyStartColmIndex(_value);
        }

        private void TextBoxForContentStartRow_TextChanged(object sender, EventArgs e)
        {
            var _targetSheet = mChooseSheet;
            if (_targetSheet == null)
            {
                return;
            }
            int.TryParse(TextBoxForContentStartRow.Text, out var _value);
            _targetSheet.SetContentStartRowIndex(_value);
        }

        private void ComboBoxForSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selectItem = ComboBoxForSelectSheet.SelectedItem as CommonWorkSheetData;
            if (_selectItem == null)
            {
                return;
            }

            InternalChooseSheet(_selectItem);
        }

        private void InternalChooseSheet(CommonWorkSheetData targetSheet)
        {
            if (targetSheet == null)
            {
                CommonUtil.ShowError(" InternalChooseSheet 出错，targetSheet为空");
                return;
            }

            mChooseSheet = targetSheet;
            switch (mFromFileType)
            {
                case LoadFileType.NormalFile:
                {
                    break;
                }
                case LoadFileType.SourceFile:
                {
                    TableDataManager.Ins().SetSourceSheet(mChooseSheet);
                    break;
                }
                case LoadFileType.ExportFile:
                {
                    TableDataManager.Ins().SetExportSheet(mChooseSheet);
                    break;
                }
                case LoadFileType.SourceFileFilterAction:
                {
                    TableDataManager.Ins().SetSourceSheetForFiltAction(mChooseSheet);
                    break;
                }
                case LoadFileType.SetSearchKey:
                {
                    break;
                }
                default:
                {
                    CommonUtil.ShowError("未处理的类型：" + mFromFileType);
                    break;
                }
            }

            InternalRefreshDataViewForSheetKey();
        }

        private void InternalRefreshDataViewForSheetKey()
        {
            // 这里重置一下数据
            // 这里导出 key 供选择
            if (mChooseSheet == null)
            {
                MessageBox.Show("当前的 Sheet 数据为空，请检查文件", "错误 ");
                return;
            }

            TextBoxForKeyStartRow.Text = mChooseSheet.GetKeyStartRowIndex().ToString();
            TextBoxForKeyStartColm.Text = mChooseSheet.GetKeyStartColmIndex().ToString();
            TextBoxForContentStartRow.Text = mChooseSheet.GetContentStartRowIndex().ToString();

            mKeyDataList = mChooseSheet.GetKeyListData();

            DataGridViewForKeyFilter.Rows.Clear();


            if (mFromFileType == LoadFileType.SetSearchKey)
            {
                for (int i = 0; i < mKeyDataList.Count; i++)
                {
                    var _filter = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, mKeyDataList[i]);
                    bool _showSelect = false;
                    if (mFromAction != null)
                    {
                        _showSelect = mFromAction.SearchKeyIndexList.Contains(i);
                    }

                    DataGridViewForKeyFilter.Rows.Add(
                        CommonUtil.GetZM(mKeyDataList[i].GetKeyIndexForShow()),
                        mKeyDataList[i].KeyName,
                        _filter != null && _filter.Count > 0,
                        "设置",
                        _showSelect,
                        "配置行为"
                    );
                }
            }
            else
            {
                for (int i = 0; i < mKeyDataList.Count; i++)
                {
                    var _filter = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, mKeyDataList[i]);

                    DataGridViewForKeyFilter.Rows.Add(
                        CommonUtil.GetZM(mKeyDataList[i].GetKeyIndexForShow()),
                        mKeyDataList[i].KeyName,
                        _filter != null && _filter.Count > 0,
                        "设置",
                        false
                    );
                }
            }
        }

        private const int mInexForHasSetFilter = 2;
        private const int mIndexForSetButton = 3; // 设置过滤的按钮
        private const int mIndexForSelectSearchKey = 4; // 查找KEY用的，目前只有 设置跨表查找的时候用到
        private const int mIndexForConfigAction = 5; // 设置查找行为用

        private void DataGridViewForKeyFilter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)// 什么傻逼玩意，-1也发消息
            {
                return;
            }

            var _targetFile = mChooseFile;
            if (_targetFile == null)
            {
                MessageBox.Show($"{DataGridViewForKeyFilter_CellContentClick} 错误，mChooseFile 为空");
                return;
            }

            switch (e.ColumnIndex)
            {
                case mIndexForConfigAction:
                {
                    var _targetKey = mKeyDataList[e.RowIndex];
                    SequenceActionConnectEditForm _form = new SequenceActionConnectEditForm();
                    var _map = TableDataManager.Ins().SourceDataFiltActionMap;
                    if (!_map.TryGetValue(_targetKey, out var _action))
                    {
                        _action = new SequenceAction();
                        _action.WorkSheetData = TableDataManager.Ins().GetSourceSheetForFiltAction();
                        _map.Add(_targetKey, _action);
                    }

                    if (!_form.InitData(_action))
                    {
                        break;
                    }

                    _form.ShowDialog();

                    break;
                }
                case mInexForHasSetFilter:
                {
                    break;
                }
                case mIndexForSelectSearchKey:
                {
                    bool _isSelect = (bool)this.DataGridViewForKeyFilter.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                    if (mFromFileType == LoadFileType.SetSearchKey)
                    {
                        mSelectKeyIndexList.Remove(e.RowIndex);
                        if (_isSelect)
                        {
                            mSelectKeyIndexList.Add(e.RowIndex);
                        }
                    }
                    else
                    {
                        var _targetKey = mKeyDataList[e.RowIndex];

                        _targetKey.IsMainKey = _isSelect;
                    }

                    break;
                }
                case mIndexForSetButton:
                {
                    var _targetKey = mKeyDataList[e.RowIndex];

                    // 编辑过滤类型
                    FilterConfigForm _form = new FilterConfigForm();
                    _form.InitInfo(e.RowIndex, e.ColumnIndex, _targetKey, mFromFileType);
                    if (_form.ShowDialog(this) == DialogResult.OK)
                    {
                        var _row = DataGridViewForKeyFilter.Rows;
                        var _cell = _row[e.RowIndex].Cells[mInexForHasSetFilter];
                        var _filterList = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, _targetKey);
                        _cell.Value = _filterList != null && _filterList.Count > 0;
                    }

                    break;
                }
            }
        }

        private void InternalRefreshForLoadFiles()
        {
            // 这里为 file combobox 的已加载文件做显示
            var _choosedFile = mChooseFile;

            {
                ComboBoxForLoadedFile.DataSource = null;
                ComboBoxForLoadedFile.Items.Clear();
                ComboBoxForLoadedFile.BeginUpdate();
                var _dataList = TableDataManager.Ins().GetLoadedFileList();

                ComboBoxForLoadedFile.DataSource = _dataList;
                ComboBoxForLoadedFile.ValueMember = "DisplayIndex";
                ComboBoxForLoadedFile.DisplayMember = "DisplayName";
                var _fileIndex = _dataList.FindIndex(x => x.GetFileAbsulotePath() == _choosedFile?.GetFileAbsulotePath());
                if (_dataList != null && _dataList.Count > 0 && _fileIndex >= 0)
                {
                    ComboBoxForLoadedFile.SelectedIndex = _fileIndex;
                }

                ComboBoxForLoadedFile.EndUpdate();
            }
        }

        private void ChooseFileConfigForm_Load(object sender, EventArgs e)
        {
            FileDataBase? _targetFile = null;

            switch (mFromFileType)
            {
                case LoadFileType.ExportFile:
                {
                    _targetFile = TableDataManager.Ins().GetExportFileData();

                    DataGridViewForKeyFilter.Columns[mInexForHasSetFilter].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForSetButton].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForSelectSearchKey].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForConfigAction].Visible = false;
                    LabelLoadedFiles.Visible = false;
                    ComboBoxForLoadedFile.Visible = false;
                    BtnConfigFiltActions.Visible = false;
                    BtnShowHasSetFilter.Visible = false;

                    break;
                }
                case LoadFileType.SourceFile:
                {
                    LabelLoadedFiles.Visible = false;
                    ComboBoxForLoadedFile.Visible = false;
                    BtnShowHasSetFilter.Visible = true;
                    _targetFile = TableDataManager.Ins().GetSourceFileData();
                    DataGridViewForKeyFilter.Columns[mIndexForSelectSearchKey].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForConfigAction].Visible = false;
                    BtnConfigFiltActions.Visible = true;
                    break;
                }
                case LoadFileType.SourceFileFilterAction:
                {
                    LabelLoadedFiles.Visible = false;
                    ComboBoxForLoadedFile.Visible = false;
                    BtnShowHasSetFilter.Visible = true;
                    _targetFile = TableDataManager.Ins().GetSourceFilterActionFileData();
                    DataGridViewForKeyFilter.Columns[mIndexForSelectSearchKey].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForConfigAction].Visible = true;
                    BtnConfigFiltActions.Visible = true;
                    break;
                }
                case LoadFileType.NormalFile:
                {
                    if (mFromSheet != null)
                    {
                        _targetFile = mFromSheet.GetOwnerTable();
                    }
                    InternalRefreshForLoadFiles();
                    LabelLoadedFiles.Visible = true;
                    ComboBoxForLoadedFile.Visible = true;
                    BtnShowHasSetFilter.Visible = false;
                    DataGridViewForKeyFilter.Columns[mInexForHasSetFilter].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForSetButton].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForSelectSearchKey].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForConfigAction].Visible = false;
                    BtnConfigFiltActions.Visible = false;

                    break;
                }
                case LoadFileType.SetSearchKey:
                {
                    if (mFromSheet != null)
                    {
                        _targetFile = mFromSheet.GetOwnerTable();
                    }
                    InternalRefreshForLoadFiles();
                    LabelLoadedFiles.Visible = true;
                    ComboBoxForLoadedFile.Visible = true;
                    BtnShowHasSetFilter.Visible = false;
                    DataGridViewForKeyFilter.Columns[mInexForHasSetFilter].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForSetButton].Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForSelectSearchKey].Visible = true;
                    BtnConfigFiltActions.Visible = false;
                    DataGridViewForKeyFilter.Columns[mIndexForConfigAction].Visible = false;

                    break;
                }
                default:
                {
                    CommonUtil.ShowError("未处理的类型：" + mFromFileType);
                    break;
                }
            }

            if (_targetFile != null)
            {
                InternalChooseFile(_targetFile, true);

                InternalChangeNotice();

                PanelForConfigs.Enabled = true;

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

                TextForFilePath.Text = _targetFile.GetFileAbsulotePath();
            }
            else
            {
                PanelForConfigs.Enabled = false;
            }

            mFromSheet = null;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // 这里导出 key 供选择
            var _currentSheet = mChooseSheet;
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
            if (mChooseSheet == null)
            {
                MessageBox.Show("未选中 Sheet，请检查");
                return;
            }

            if (mChooseSheet == null)
            {
                CommonUtil.ShowError("mChooseSheet 为空，请检查");
                return;
            }

            DialogResult = DialogResult.OK;
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
            if (mChooseSheet == null)
            {
                MessageBox.Show("BtnReloadKey_Click 出错，mChooseSheet为空，请检查!");
                return;
            }
            mChooseSheet.ReloadKey();
            InternalRefreshDataViewForSheetKey();
        }

        private void BtnShowHasSetFilter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.DataGridViewForKeyFilter.Rows.Count; ++i)
            {
                var _key = mKeyDataList[i];
                var _filterFunc = TableDataManager.Ins().GetSourceFileDataFilterFuncByKey(mFromFileType, _key);
                bool _show = _filterFunc != null && _filterFunc.Count > 0;
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

        private void ComboBoxForLoadedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _dataList = TableDataManager.Ins().GetLoadedFileList();
            if (_dataList == null || _dataList.Count < 1)
            {
                MessageBox.Show("加载文件数量错误");
                return;
            }

            var _index = ComboBoxForLoadedFile.SelectedIndex;
            if (_index < 0)
            {
                return;
            }

            InternalChooseFile(_dataList[_index], false);
        }

        private void BtnConfigFiltActions_Click(object sender, EventArgs e)
        {
            ChooseFileConfigForm _form = new ChooseFileConfigForm();
            _form.SetInitData(LoadFileType.SourceFileFilterAction, TableDataManager.Ins().GetSourceSheetForFiltAction());

            _form.ShowDialog();
        }

        private void BtnClearSourceFilterAction_Click(object sender, EventArgs e)
        {
            TableDataManager.Ins().SetSourceSheetForFiltAction(null);
        }
    }
}
