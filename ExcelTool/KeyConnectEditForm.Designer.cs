namespace ExcelTool
{
    partial class KeyConnectEditForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            BtnLoadNewFile = new Button();
            ComboBoxForLoadedFile = new ComboBox();
            label1 = new Label();
            DataViewForKeyConfig = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
            RelatInfo = new DataGridViewTextBoxColumn();
            EditRelateBtnColum = new DataGridViewButtonColumn();
            IsDataSource = new DataGridViewCheckBoxColumn();
            label2 = new Label();
            LabelForFromTable = new Label();
            button1 = new Button();
            label3 = new Label();
            ComboBoxForWorkSheet = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyConfig).BeginInit();
            SuspendLayout();
            // 
            // BtnLoadNewFile
            // 
            BtnLoadNewFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLoadNewFile.Location = new Point(878, 17);
            BtnLoadNewFile.Name = "BtnLoadNewFile";
            BtnLoadNewFile.Size = new Size(130, 30);
            BtnLoadNewFile.TabIndex = 17;
            BtnLoadNewFile.Text = "加载新表格";
            BtnLoadNewFile.UseVisualStyleBackColor = true;
            BtnLoadNewFile.Click += BtnLoadNewFile_Click;
            // 
            // ComboBoxForLoadedFile
            // 
            ComboBoxForLoadedFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForLoadedFile.FormattingEnabled = true;
            ComboBoxForLoadedFile.Location = new Point(469, 19);
            ComboBoxForLoadedFile.Name = "ComboBoxForLoadedFile";
            ComboBoxForLoadedFile.Size = new Size(130, 29);
            ComboBoxForLoadedFile.TabIndex = 18;
            ComboBoxForLoadedFile.SelectedIndexChanged += ComboBoxForLoadedFile_SelectedIndexChanged;
            ComboBoxForLoadedFile.SelectionChangeCommitted += ComboBoxForLoadedFile_SelectionChangeCommitted;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(368, 22);
            label1.Name = "label1";
            label1.Size = new Size(90, 21);
            label1.TabIndex = 19;
            label1.Text = "已加载表格";
            // 
            // DataViewForKeyConfig
            // 
            DataViewForKeyConfig.BackgroundColor = SystemColors.ControlLight;
            DataViewForKeyConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewForKeyConfig.Columns.AddRange(new DataGridViewColumn[] { Key, KeyName, RelatInfo, EditRelateBtnColum, IsDataSource });
            DataViewForKeyConfig.Location = new Point(12, 64);
            DataViewForKeyConfig.Name = "DataViewForKeyConfig";
            DataViewForKeyConfig.RowTemplate.Height = 25;
            DataViewForKeyConfig.Size = new Size(996, 456);
            DataViewForKeyConfig.TabIndex = 20;
            DataViewForKeyConfig.CellContentClick += DataViewForKeyConfig_CellContentClick;
            DataViewForKeyConfig.CellValueChanged += DataViewForKeyConfig_CellValueChanged;
            // 
            // Key
            // 
            Key.HeaderText = "Index";
            Key.Name = "Key";
            Key.Width = 60;
            // 
            // KeyName
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle1;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "Key名字";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            KeyName.Width = 200;
            // 
            // RelatInfo
            // 
            RelatInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RelatInfo.DefaultCellStyle = dataGridViewCellStyle2;
            RelatInfo.FillWeight = 160F;
            RelatInfo.HeaderText = "关联信息";
            RelatInfo.Name = "RelatInfo";
            RelatInfo.ReadOnly = true;
            // 
            // EditRelateBtnColum
            // 
            EditRelateBtnColum.FillWeight = 160F;
            EditRelateBtnColum.HeaderText = "设置关联";
            EditRelateBtnColum.Name = "EditRelateBtnColum";
            EditRelateBtnColum.Text = "编辑";
            EditRelateBtnColum.ToolTipText = "编辑";
            // 
            // IsDataSource
            // 
            IsDataSource.HeaderText = "选为数据源";
            IsDataSource.Name = "IsDataSource";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(23, 22);
            label2.Name = "label2";
            label2.Size = new Size(90, 21);
            label2.TabIndex = 21;
            label2.Text = "目标文件：";
            // 
            // LabelForFromTable
            // 
            LabelForFromTable.AutoSize = true;
            LabelForFromTable.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelForFromTable.Location = new Point(124, 22);
            LabelForFromTable.Name = "LabelForFromTable";
            LabelForFromTable.Size = new Size(74, 21);
            LabelForFromTable.TabIndex = 22;
            LabelForFromTable.Text = "目标文件";
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(878, 542);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 23;
            button1.Text = "设置完成";
            button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(638, 22);
            label3.Name = "label3";
            label3.Size = new Size(85, 21);
            label3.TabIndex = 25;
            label3.Text = "选择Sheet";
            // 
            // ComboBoxForWorkSheet
            // 
            ComboBoxForWorkSheet.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForWorkSheet.FormattingEnabled = true;
            ComboBoxForWorkSheet.Location = new Point(730, 19);
            ComboBoxForWorkSheet.Name = "ComboBoxForWorkSheet";
            ComboBoxForWorkSheet.Size = new Size(130, 29);
            ComboBoxForWorkSheet.TabIndex = 24;
            ComboBoxForWorkSheet.SelectedIndexChanged += ComboBoxForWorkSheet_SelectedIndexChanged;
            ComboBoxForWorkSheet.SelectionChangeCommitted += ComboBoxForWorkSheet_SelectionChangeCommitted;
            // 
            // KeyConnectEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 584);
            Controls.Add(label3);
            Controls.Add(ComboBoxForWorkSheet);
            Controls.Add(button1);
            Controls.Add(LabelForFromTable);
            Controls.Add(label2);
            Controls.Add(DataViewForKeyConfig);
            Controls.Add(label1);
            Controls.Add(ComboBoxForLoadedFile);
            Controls.Add(BtnLoadNewFile);
            Name = "KeyConnectEditForm";
            Text = "EditRelateExcelSetting";
            Load += KeyConnectEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyConfig).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BtnLoadNewFile;
        private ComboBox ComboBoxForLoadedFile;
        private Label label1;
        private DataGridView DataViewForKeyConfig;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewTextBoxColumn RelatInfo;
        private DataGridViewButtonColumn EditRelateBtnColum;
        private DataGridViewCheckBoxColumn IsDataSource;
        private Label label2;
        private Label LabelForFromTable;
        private Button button1;
        private Label label3;
        private ComboBox ComboBoxForWorkSheet;
    }
}