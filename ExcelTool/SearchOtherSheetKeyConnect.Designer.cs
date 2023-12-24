namespace ExcelTool
{
    partial class SearchOtherSheetKeyConnect
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            BtnReset = new Button();
            BtnSearch = new Button();
            TextBoxForSearch = new TextBox();
            label5 = new Label();
            label3 = new Label();
            ComboBoxForWorkSheet = new ComboBox();
            LabelForFromTable = new Label();
            DataViewForKeyList = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
            IsDataSource = new DataGridViewCheckBoxColumn();
            label1 = new Label();
            ComboBoxForLoadedFile = new ComboBox();
            BtnLoadNewFile = new Button();
            BtnFinishConfig = new Button();
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyList).BeginInit();
            SuspendLayout();
            // 
            // BtnReset
            // 
            BtnReset.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReset.Location = new Point(490, 48);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(79, 30);
            BtnReset.TabIndex = 54;
            BtnReset.Text = "重置";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // BtnSearch
            // 
            BtnSearch.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSearch.Location = new Point(404, 48);
            BtnSearch.Name = "BtnSearch";
            BtnSearch.Size = new Size(78, 30);
            BtnSearch.TabIndex = 53;
            BtnSearch.Text = "查找";
            BtnSearch.UseVisualStyleBackColor = true;
            BtnSearch.Click += BtnSearch_Click;
            // 
            // TextBoxForSearch
            // 
            TextBoxForSearch.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForSearch.Location = new Point(101, 50);
            TextBoxForSearch.Name = "TextBoxForSearch";
            TextBoxForSearch.Size = new Size(292, 26);
            TextBoxForSearch.TabIndex = 52;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 52);
            label5.Name = "label5";
            label5.Size = new Size(69, 21);
            label5.TabIndex = 51;
            label5.Text = "Key筛选";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(637, 14);
            label3.Name = "label3";
            label3.Size = new Size(85, 21);
            label3.TabIndex = 50;
            label3.Text = "选择Sheet";
            // 
            // ComboBoxForWorkSheet
            // 
            ComboBoxForWorkSheet.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForWorkSheet.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForWorkSheet.FormattingEnabled = true;
            ComboBoxForWorkSheet.Location = new Point(729, 11);
            ComboBoxForWorkSheet.Name = "ComboBoxForWorkSheet";
            ComboBoxForWorkSheet.Size = new Size(130, 29);
            ComboBoxForWorkSheet.TabIndex = 49;
            ComboBoxForWorkSheet.SelectedIndexChanged += ComboBoxForWorkSheet_SelectedIndexChanged;
            // 
            // LabelForFromTable
            // 
            LabelForFromTable.AutoSize = true;
            LabelForFromTable.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelForFromTable.Location = new Point(12, 14);
            LabelForFromTable.Name = "LabelForFromTable";
            LabelForFromTable.Size = new Size(90, 21);
            LabelForFromTable.TabIndex = 48;
            LabelForFromTable.Text = "目标文件：";
            // 
            // DataViewForKeyList
            // 
            DataViewForKeyList.BackgroundColor = SystemColors.ControlLight;
            DataViewForKeyList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewForKeyList.Columns.AddRange(new DataGridViewColumn[] { Key, KeyName, IsDataSource });
            DataViewForKeyList.Location = new Point(11, 82);
            DataViewForKeyList.Name = "DataViewForKeyList";
            DataViewForKeyList.RowTemplate.Height = 25;
            DataViewForKeyList.Size = new Size(996, 279);
            DataViewForKeyList.TabIndex = 47;
            DataViewForKeyList.CellContentClick += DataViewForKeyList_CellContentClick;
            // 
            // Key
            // 
            Key.HeaderText = "Index";
            Key.Name = "Key";
            Key.Width = 60;
            // 
            // KeyName
            // 
            KeyName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle1;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "Key名字";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            // 
            // IsDataSource
            // 
            IsDataSource.HeaderText = "选择数据列";
            IsDataSource.Name = "IsDataSource";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(367, 14);
            label1.Name = "label1";
            label1.Size = new Size(90, 21);
            label1.TabIndex = 46;
            label1.Text = "已加载表格";
            // 
            // ComboBoxForLoadedFile
            // 
            ComboBoxForLoadedFile.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForLoadedFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForLoadedFile.FormattingEnabled = true;
            ComboBoxForLoadedFile.Location = new Point(468, 11);
            ComboBoxForLoadedFile.Name = "ComboBoxForLoadedFile";
            ComboBoxForLoadedFile.Size = new Size(130, 29);
            ComboBoxForLoadedFile.TabIndex = 45;
            ComboBoxForLoadedFile.SelectedIndexChanged += ComboBoxForLoadedFile_SelectedIndexChanged;
            // 
            // BtnLoadNewFile
            // 
            BtnLoadNewFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLoadNewFile.Location = new Point(877, 9);
            BtnLoadNewFile.Name = "BtnLoadNewFile";
            BtnLoadNewFile.Size = new Size(130, 30);
            BtnLoadNewFile.TabIndex = 44;
            BtnLoadNewFile.Text = "加载新表格";
            BtnLoadNewFile.UseVisualStyleBackColor = true;
            BtnLoadNewFile.Click += BtnLoadNewFile_Click;
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(877, 380);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 55;
            BtnFinishConfig.Text = "设置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // SearchOtherSheetKeyConnect
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 419);
            Controls.Add(BtnFinishConfig);
            Controls.Add(BtnReset);
            Controls.Add(BtnSearch);
            Controls.Add(TextBoxForSearch);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(ComboBoxForWorkSheet);
            Controls.Add(LabelForFromTable);
            Controls.Add(DataViewForKeyList);
            Controls.Add(label1);
            Controls.Add(ComboBoxForLoadedFile);
            Controls.Add(BtnLoadNewFile);
            Name = "SearchOtherSheetKeyConnect";
            Text = "SearchOtherSheetKeyConnect";
            Load += SearchOtherSheetKeyConnect_Load;
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnReset;
        private Button BtnSearch;
        private TextBox TextBoxForSearch;
        private Label label5;
        private Label label3;
        private ComboBox ComboBoxForWorkSheet;
        private Label LabelForFromTable;
        private DataGridView DataViewForKeyList;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewCheckBoxColumn IsDataSource;
        private Label label1;
        private ComboBox ComboBoxForLoadedFile;
        private Button BtnLoadNewFile;
        private Button BtnFinishConfig;
    }
}