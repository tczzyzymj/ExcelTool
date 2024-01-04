namespace ExcelTool
{
    partial class ChooseFileConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnFinishConfig = new Button();
            TextBoxForContentStartRow = new TextBox();
            label3 = new Label();
            TextBoxForKeyStartColm = new TextBox();
            label2 = new Label();
            TextBoxForKeyStartRow = new TextBox();
            LableKeyStartRowIndex = new Label();
            BtnChooseFile = new Button();
            label1 = new Label();
            TextForFilePath = new TextBox();
            label5 = new Label();
            ComboBoxForSelectSheet = new ComboBox();
            label4 = new Label();
            DataGridViewForKeyFilter = new DataGridView();
            KeyIndex = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
            CheckColum = new DataGridViewCheckBoxColumn();
            EditFilterBtnColum = new DataGridViewButtonColumn();
            ChooseSearchKey = new DataGridViewCheckBoxColumn();
            TextBoxForSearch = new TextBox();
            BtnSearch = new Button();
            BtnReset = new Button();
            TextBoxSplitSymbol = new TextBox();
            LableForSplitSymbol = new Label();
            PanelForConfigs = new Panel();
            LabelForNotice = new Label();
            BtnReloadKey = new Button();
            BtnShowHasSetFilter = new Button();
            LabelLoadedFiles = new Label();
            ComboBoxForLoadedFile = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)DataGridViewForKeyFilter).BeginInit();
            PanelForConfigs.SuspendLayout();
            SuspendLayout();
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(653, 520);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 34;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // TextBoxForContentStartRow
            // 
            TextBoxForContentStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForContentStartRow.Location = new Point(408, 52);
            TextBoxForContentStartRow.Name = "TextBoxForContentStartRow";
            TextBoxForContentStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForContentStartRow.Size = new Size(39, 28);
            TextBoxForContentStartRow.TabIndex = 33;
            TextBoxForContentStartRow.Text = "1";
            TextBoxForContentStartRow.TextChanged += TextBoxForContentStartRow_TextChanged;
            TextBoxForContentStartRow.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(316, 55);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 32;
            label3.Text = "内容开始行";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(248, 52);
            TextBoxForKeyStartColm.Name = "TextBoxForKeyStartColm";
            TextBoxForKeyStartColm.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartColm.Size = new Size(39, 28);
            TextBoxForKeyStartColm.TabIndex = 31;
            TextBoxForKeyStartColm.Text = "0";
            TextBoxForKeyStartColm.TextChanged += TextBoxForKeyStartColm_TextChanged;
            TextBoxForKeyStartColm.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(161, 55);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 30;
            label2.Text = "Key开始列";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(91, 52);
            TextBoxForKeyStartRow.Name = "TextBoxForKeyStartRow";
            TextBoxForKeyStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartRow.Size = new Size(39, 28);
            TextBoxForKeyStartRow.TabIndex = 29;
            TextBoxForKeyStartRow.Text = "0";
            TextBoxForKeyStartRow.TextChanged += TextBoxForKeyStartRow_TextChanged;
            TextBoxForKeyStartRow.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // LableKeyStartRowIndex
            // 
            LableKeyStartRowIndex.AutoSize = true;
            LableKeyStartRowIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableKeyStartRowIndex.Location = new Point(4, 55);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 28;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // BtnChooseFile
            // 
            BtnChooseFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseFile.Location = new Point(653, 21);
            BtnChooseFile.Name = "BtnChooseFile";
            BtnChooseFile.Size = new Size(130, 30);
            BtnChooseFile.TabIndex = 26;
            BtnChooseFile.Text = "加载文件";
            BtnChooseFile.UseVisualStyleBackColor = true;
            BtnChooseFile.Click += BtnChooseFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(15, 24);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 25;
            label1.Text = "目标文件";
            // 
            // TextForFilePath
            // 
            TextForFilePath.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextForFilePath.Location = new Point(104, 23);
            TextForFilePath.Name = "TextForFilePath";
            TextForFilePath.ReadOnly = true;
            TextForFilePath.Size = new Size(529, 26);
            TextForFilePath.TabIndex = 24;
            TextForFilePath.Text = "请点击选择";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(15, 191);
            label5.Name = "label5";
            label5.Size = new Size(69, 21);
            label5.TabIndex = 35;
            label5.Text = "Key筛选";
            // 
            // ComboBoxForSelectSheet
            // 
            ComboBoxForSelectSheet.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForSelectSheet.FormattingEnabled = true;
            ComboBoxForSelectSheet.Location = new Point(104, 63);
            ComboBoxForSelectSheet.Name = "ComboBoxForSelectSheet";
            ComboBoxForSelectSheet.Size = new Size(121, 25);
            ComboBoxForSelectSheet.TabIndex = 14;
            ComboBoxForSelectSheet.SelectedIndexChanged += ComboBoxForSelectSheet_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(15, 65);
            label4.Name = "label4";
            label4.Size = new Size(83, 21);
            label4.TabIndex = 13;
            label4.Text = "选择sheet";
            // 
            // DataGridViewForKeyFilter
            // 
            DataGridViewForKeyFilter.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewForKeyFilter.Columns.AddRange(new DataGridViewColumn[] { KeyIndex, KeyName, CheckColum, EditFilterBtnColum, ChooseSearchKey });
            DataGridViewForKeyFilter.Location = new Point(15, 225);
            DataGridViewForKeyFilter.Name = "DataGridViewForKeyFilter";
            DataGridViewForKeyFilter.RowTemplate.Height = 25;
            DataGridViewForKeyFilter.Size = new Size(768, 283);
            DataGridViewForKeyFilter.TabIndex = 36;
            DataGridViewForKeyFilter.CellContentClick += DataGridViewForKeyFilter_CellContentClick;
            // 
            // KeyIndex
            // 
            KeyIndex.HeaderText = "Key下标";
            KeyIndex.Name = "KeyIndex";
            KeyIndex.ReadOnly = true;
            KeyIndex.Width = 80;
            // 
            // KeyName
            // 
            KeyName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            KeyName.HeaderText = "Key名字";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            // 
            // CheckColum
            // 
            CheckColum.HeaderText = "已设置筛选";
            CheckColum.Name = "CheckColum";
            CheckColum.ReadOnly = true;
            CheckColum.Width = 80;
            // 
            // EditFilterBtnColum
            // 
            EditFilterBtnColum.HeaderText = "设置过滤方法";
            EditFilterBtnColum.Name = "EditFilterBtnColum";
            EditFilterBtnColum.Text = "设置";
            // 
            // ChooseSearchKey
            // 
            ChooseSearchKey.HeaderText = "选择";
            ChooseSearchKey.Name = "ChooseSearchKey";
            ChooseSearchKey.Resizable = DataGridViewTriState.True;
            ChooseSearchKey.SortMode = DataGridViewColumnSortMode.Automatic;
            ChooseSearchKey.Width = 60;
            // 
            // TextBoxForSearch
            // 
            TextBoxForSearch.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForSearch.Location = new Point(104, 188);
            TextBoxForSearch.Name = "TextBoxForSearch";
            TextBoxForSearch.Size = new Size(154, 26);
            TextBoxForSearch.TabIndex = 37;
            // 
            // BtnSearch
            // 
            BtnSearch.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSearch.Location = new Point(264, 186);
            BtnSearch.Name = "BtnSearch";
            BtnSearch.Size = new Size(78, 30);
            BtnSearch.TabIndex = 38;
            BtnSearch.Text = "查找";
            BtnSearch.UseVisualStyleBackColor = true;
            BtnSearch.Click += BtnSearch_Click;
            // 
            // BtnReset
            // 
            BtnReset.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReset.Location = new Point(348, 186);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(79, 30);
            BtnReset.TabIndex = 39;
            BtnReset.Text = "重置";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // TextBoxSplitSymbol
            // 
            TextBoxSplitSymbol.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxSplitSymbol.Location = new Point(726, 52);
            TextBoxSplitSymbol.Name = "TextBoxSplitSymbol";
            TextBoxSplitSymbol.RightToLeft = RightToLeft.Yes;
            TextBoxSplitSymbol.Size = new Size(39, 28);
            TextBoxSplitSymbol.TabIndex = 43;
            TextBoxSplitSymbol.Text = ",";
            TextBoxSplitSymbol.TextChanged += TextBoxSplitSymbol_TextChanged;
            // 
            // LableForSplitSymbol
            // 
            LableForSplitSymbol.AutoSize = true;
            LableForSplitSymbol.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableForSplitSymbol.Location = new Point(662, 56);
            LableForSplitSymbol.Name = "LableForSplitSymbol";
            LableForSplitSymbol.Size = new Size(58, 21);
            LableForSplitSymbol.TabIndex = 42;
            LableForSplitSymbol.Text = "分隔符";
            // 
            // PanelForConfigs
            // 
            PanelForConfigs.Controls.Add(LabelForNotice);
            PanelForConfigs.Controls.Add(TextBoxSplitSymbol);
            PanelForConfigs.Controls.Add(LableKeyStartRowIndex);
            PanelForConfigs.Controls.Add(LableForSplitSymbol);
            PanelForConfigs.Controls.Add(TextBoxForKeyStartRow);
            PanelForConfigs.Controls.Add(label2);
            PanelForConfigs.Controls.Add(TextBoxForKeyStartColm);
            PanelForConfigs.Controls.Add(label3);
            PanelForConfigs.Controls.Add(TextBoxForContentStartRow);
            PanelForConfigs.Location = new Point(12, 93);
            PanelForConfigs.Name = "PanelForConfigs";
            PanelForConfigs.Size = new Size(772, 89);
            PanelForConfigs.TabIndex = 44;
            // 
            // LabelForNotice
            // 
            LabelForNotice.AutoSize = true;
            LabelForNotice.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelForNotice.Location = new Point(4, 20);
            LabelForNotice.Name = "LabelForNotice";
            LabelForNotice.Size = new Size(314, 21);
            LabelForNotice.TabIndex = 46;
            LabelForNotice.Text = "注意，所选文件不同，下标开始数字也不同";
            // 
            // BtnReloadKey
            // 
            BtnReloadKey.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReloadKey.Location = new Point(628, 186);
            BtnReloadKey.Name = "BtnReloadKey";
            BtnReloadKey.Size = new Size(155, 30);
            BtnReloadKey.TabIndex = 45;
            BtnReloadKey.Text = "重新加载Key数据";
            BtnReloadKey.UseVisualStyleBackColor = true;
            BtnReloadKey.Click += BtnReloadKey_Click;
            // 
            // BtnShowHasSetFilter
            // 
            BtnShowHasSetFilter.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnShowHasSetFilter.Location = new Point(433, 186);
            BtnShowHasSetFilter.Name = "BtnShowHasSetFilter";
            BtnShowHasSetFilter.Size = new Size(110, 30);
            BtnShowHasSetFilter.TabIndex = 46;
            BtnShowHasSetFilter.Text = "显示已筛选";
            BtnShowHasSetFilter.UseVisualStyleBackColor = true;
            BtnShowHasSetFilter.Click += BtnShowHasSetFilter_Click;
            // 
            // LabelLoadedFiles
            // 
            LabelLoadedFiles.AutoSize = true;
            LabelLoadedFiles.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelLoadedFiles.Location = new Point(252, 65);
            LabelLoadedFiles.Name = "LabelLoadedFiles";
            LabelLoadedFiles.Size = new Size(90, 21);
            LabelLoadedFiles.TabIndex = 48;
            LabelLoadedFiles.Text = "已加载文件";
            // 
            // ComboBoxForLoadedFile
            // 
            ComboBoxForLoadedFile.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForLoadedFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForLoadedFile.FormattingEnabled = true;
            ComboBoxForLoadedFile.Location = new Point(353, 60);
            ComboBoxForLoadedFile.Name = "ComboBoxForLoadedFile";
            ComboBoxForLoadedFile.Size = new Size(130, 29);
            ComboBoxForLoadedFile.TabIndex = 47;
            ComboBoxForLoadedFile.SelectedIndexChanged += ComboBoxForLoadedFile_SelectedIndexChanged;
            // 
            // ChooseFileConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 568);
            Controls.Add(LabelLoadedFiles);
            Controls.Add(ComboBoxForLoadedFile);
            Controls.Add(BtnShowHasSetFilter);
            Controls.Add(BtnReloadKey);
            Controls.Add(PanelForConfigs);
            Controls.Add(BtnReset);
            Controls.Add(BtnSearch);
            Controls.Add(TextBoxForSearch);
            Controls.Add(DataGridViewForKeyFilter);
            Controls.Add(label4);
            Controls.Add(ComboBoxForSelectSheet);
            Controls.Add(label5);
            Controls.Add(BtnFinishConfig);
            Controls.Add(BtnChooseFile);
            Controls.Add(label1);
            Controls.Add(TextForFilePath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ChooseFileConfigForm";
            Text = "ChooseFileConfigForm";
            Load += ChooseFileConfigForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewForKeyFilter).EndInit();
            PanelForConfigs.ResumeLayout(false);
            PanelForConfigs.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnFinishConfig;
        private TextBox TextBoxForContentStartRow;
        private Label label3;
        private TextBox TextBoxForKeyStartColm;
        private Label label2;
        private TextBox TextBoxForKeyStartRow;
        private Label LableKeyStartRowIndex;
        private Button BtnChooseFile;
        private Label label1;
        private TextBox TextForFilePath;
        private Label label5;
        private ComboBox ComboBoxForSelectSheet;
        private Label label4;
        private DataGridView DataGridViewForKeyFilter;
        private TextBox TextBoxForSearch;
        private Button BtnSearch;
        private Button BtnReset;
        private TextBox TextBoxSplitSymbol;
        private Label LableForSplitSymbol;
        private Panel PanelForConfigs;
        private Button BtnReloadKey;
        private Label LabelForNotice;
        private Button BtnShowHasSetFilter;
        private Label LabelLoadedFiles;
        private ComboBox ComboBoxForLoadedFile;
        private DataGridViewTextBoxColumn KeyIndex;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewCheckBoxColumn CheckColum;
        private DataGridViewButtonColumn EditFilterBtnColum;
        private DataGridViewCheckBoxColumn ChooseSearchKey;
    }
}