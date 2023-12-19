namespace ExcelTool
{
    partial class SourceFileConfigForm
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
            TextBoxForSearch = new TextBox();
            BtnSearch = new Button();
            BtnReset = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridViewForKeyFilter).BeginInit();
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
            TextBoxForContentStartRow.Location = new Point(435, 131);
            TextBoxForContentStartRow.Name = "TextBoxForContentStartRow";
            TextBoxForContentStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForContentStartRow.Size = new Size(39, 28);
            TextBoxForContentStartRow.TabIndex = 33;
            TextBoxForContentStartRow.Text = "4";
            TextBoxForContentStartRow.TextChanged += TextBoxForContentStartRow_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(344, 134);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 32;
            label3.Text = "内容开始行";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(275, 131);
            TextBoxForKeyStartColm.Name = "TextBoxForKeyStartColm";
            TextBoxForKeyStartColm.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartColm.Size = new Size(39, 28);
            TextBoxForKeyStartColm.TabIndex = 31;
            TextBoxForKeyStartColm.Text = "1";
            TextBoxForKeyStartColm.TextChanged += TextBoxForKeyStartColm_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(184, 134);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 30;
            label2.Text = "Key开始列";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(118, 131);
            TextBoxForKeyStartRow.Name = "TextBoxForKeyStartRow";
            TextBoxForKeyStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartRow.Size = new Size(39, 28);
            TextBoxForKeyStartRow.TabIndex = 29;
            TextBoxForKeyStartRow.Text = "2";
            TextBoxForKeyStartRow.TextChanged += TextBoxForKeyStartRow_TextChanged;
            // 
            // LableKeyStartRowIndex
            // 
            LableKeyStartRowIndex.AutoSize = true;
            LableKeyStartRowIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableKeyStartRowIndex.Location = new Point(29, 134);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 28;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // BtnChooseFile
            // 
            BtnChooseFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseFile.Location = new Point(653, 20);
            BtnChooseFile.Name = "BtnChooseFile";
            BtnChooseFile.Size = new Size(130, 30);
            BtnChooseFile.TabIndex = 26;
            BtnChooseFile.Text = "选择文件";
            BtnChooseFile.UseVisualStyleBackColor = true;
            BtnChooseFile.Click += BtnChooseFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(29, 24);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 25;
            label1.Text = "目标文件";
            // 
            // TextForFilePath
            // 
            TextForFilePath.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextForFilePath.Location = new Point(118, 22);
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
            label5.Location = new Point(29, 188);
            label5.Name = "label5";
            label5.Size = new Size(69, 21);
            label5.TabIndex = 35;
            label5.Text = "Key筛选";
            // 
            // ComboBoxForSelectSheet
            // 
            ComboBoxForSelectSheet.FormattingEnabled = true;
            ComboBoxForSelectSheet.Location = new Point(118, 75);
            ComboBoxForSelectSheet.Name = "ComboBoxForSelectSheet";
            ComboBoxForSelectSheet.Size = new Size(121, 25);
            ComboBoxForSelectSheet.TabIndex = 14;
            ComboBoxForSelectSheet.SelectedIndexChanged += ComboBoxForSelectSheet_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(29, 77);
            label4.Name = "label4";
            label4.Size = new Size(83, 21);
            label4.TabIndex = 13;
            label4.Text = "选择sheet";
            // 
            // DataGridViewForKeyFilter
            // 
            DataGridViewForKeyFilter.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewForKeyFilter.Columns.AddRange(new DataGridViewColumn[] { KeyIndex, KeyName, CheckColum, EditFilterBtnColum });
            DataGridViewForKeyFilter.Location = new Point(29, 216);
            DataGridViewForKeyFilter.Name = "DataGridViewForKeyFilter";
            DataGridViewForKeyFilter.RowTemplate.Height = 25;
            DataGridViewForKeyFilter.Size = new Size(754, 283);
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
            CheckColum.HeaderText = "是否筛选";
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
            // TextBoxForSearch
            // 
            TextBoxForSearch.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForSearch.Location = new Point(109, 186);
            TextBoxForSearch.Name = "TextBoxForSearch";
            TextBoxForSearch.Size = new Size(292, 26);
            TextBoxForSearch.TabIndex = 37;
            // 
            // BtnSearch
            // 
            BtnSearch.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSearch.Location = new Point(407, 183);
            BtnSearch.Name = "BtnSearch";
            BtnSearch.Size = new Size(130, 30);
            BtnSearch.TabIndex = 38;
            BtnSearch.Text = "查找";
            BtnSearch.UseVisualStyleBackColor = true;
            BtnSearch.Click += BtnSearch_Click;
            // 
            // BtnReset
            // 
            BtnReset.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReset.Location = new Point(543, 183);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(130, 30);
            BtnReset.TabIndex = 39;
            BtnReset.Text = "重置";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // SourceFileConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 562);
            Controls.Add(BtnReset);
            Controls.Add(BtnSearch);
            Controls.Add(TextBoxForSearch);
            Controls.Add(DataGridViewForKeyFilter);
            Controls.Add(label4);
            Controls.Add(ComboBoxForSelectSheet);
            Controls.Add(label5);
            Controls.Add(BtnFinishConfig);
            Controls.Add(TextBoxForContentStartRow);
            Controls.Add(label3);
            Controls.Add(TextBoxForKeyStartColm);
            Controls.Add(label2);
            Controls.Add(TextBoxForKeyStartRow);
            Controls.Add(LableKeyStartRowIndex);
            Controls.Add(BtnChooseFile);
            Controls.Add(label1);
            Controls.Add(TextForFilePath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "SourceFileConfigForm";
            Text = "SourceFileConfigForm";
            Load += SourceFileConfigForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewForKeyFilter).EndInit();
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
        private DataGridViewTextBoxColumn KeyIndex;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewCheckBoxColumn CheckColum;
        private DataGridViewButtonColumn EditFilterBtnColum;
    }
}