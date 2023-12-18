namespace ExcelTool
{
    partial class ExcelTool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            StartExportBtn = new Button();
            BtnExportSetting = new Button();
            BtnImport = new Button();
            BtnChooseExportFile = new Button();
            BtnChooseSourceFile = new Button();
            DataViewConfigForExportFile = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
            RelatInfo = new DataGridViewTextBoxColumn();
            EditRelateBtnColum = new DataGridViewButtonColumn();
            LeaveEmptyColum = new DataGridViewCheckBoxColumn();
            label4 = new Label();
            ComboBoxForExportWriteWay = new ComboBox();
            ComboBoxForExportConflictDealWay = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)DataViewConfigForExportFile).BeginInit();
            SuspendLayout();
            // 
            // StartExportBtn
            // 
            StartExportBtn.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            StartExportBtn.Location = new Point(1594, 1077);
            StartExportBtn.Name = "StartExportBtn";
            StartExportBtn.Size = new Size(130, 33);
            StartExportBtn.TabIndex = 0;
            StartExportBtn.Text = "开始导表";
            StartExportBtn.UseVisualStyleBackColor = true;
            StartExportBtn.Click += StartExportBtn_Click;
            // 
            // BtnExportSetting
            // 
            BtnExportSetting.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnExportSetting.Location = new Point(167, 12);
            BtnExportSetting.Name = "BtnExportSetting";
            BtnExportSetting.Size = new Size(130, 30);
            BtnExportSetting.TabIndex = 1;
            BtnExportSetting.Text = "导出配置";
            BtnExportSetting.UseVisualStyleBackColor = true;
            BtnExportSetting.Click += BtnExportSetting_Click;
            // 
            // BtnImport
            // 
            BtnImport.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnImport.Location = new Point(12, 12);
            BtnImport.Name = "BtnImport";
            BtnImport.Size = new Size(130, 30);
            BtnImport.TabIndex = 2;
            BtnImport.Text = "导入配置";
            BtnImport.UseVisualStyleBackColor = true;
            BtnImport.Click += BtnImport_Click;
            // 
            // BtnChooseExportFile
            // 
            BtnChooseExportFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseExportFile.Location = new Point(12, 60);
            BtnChooseExportFile.Name = "BtnChooseExportFile";
            BtnChooseExportFile.Size = new Size(130, 30);
            BtnChooseExportFile.TabIndex = 5;
            BtnChooseExportFile.Text = "配置导出目标";
            BtnChooseExportFile.UseVisualStyleBackColor = true;
            BtnChooseExportFile.Click += BntChooseExportFile_Click;
            // 
            // BtnChooseSourceFile
            // 
            BtnChooseSourceFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseSourceFile.Location = new Point(167, 60);
            BtnChooseSourceFile.Name = "BtnChooseSourceFile";
            BtnChooseSourceFile.Size = new Size(130, 30);
            BtnChooseSourceFile.TabIndex = 13;
            BtnChooseSourceFile.Text = "配置数据源";
            BtnChooseSourceFile.UseVisualStyleBackColor = true;
            BtnChooseSourceFile.Click += BtnChooseSourceFile_Click;
            // 
            // DataViewConfigForExportFile
            // 
            DataViewConfigForExportFile.BackgroundColor = SystemColors.ControlLight;
            DataViewConfigForExportFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewConfigForExportFile.Columns.AddRange(new DataGridViewColumn[] { Key, KeyName, RelatInfo, EditRelateBtnColum, LeaveEmptyColum });
            DataViewConfigForExportFile.Location = new Point(12, 116);
            DataViewConfigForExportFile.Name = "DataViewConfigForExportFile";
            DataViewConfigForExportFile.RowTemplate.Height = 25;
            DataViewConfigForExportFile.Size = new Size(1712, 936);
            DataViewConfigForExportFile.TabIndex = 14;
            DataViewConfigForExportFile.CellClick += DataViewConfigForExportFile_CellClick;
            DataViewConfigForExportFile.CellContentClick += DataViewConfigForExportFile_CellContentClick;
            DataViewConfigForExportFile.CellValueChanged += DataViewConfigForExportFile_CellValueChanged;
            // 
            // Key
            // 
            Key.HeaderText = "Key";
            Key.Name = "Key";
            Key.Width = 60;
            // 
            // KeyName
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle1;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "导出目标列名";
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
            // LeaveEmptyColum
            // 
            LeaveEmptyColum.HeaderText = "是否忽略";
            LeaveEmptyColum.Name = "LeaveEmptyColum";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(341, 65);
            label4.Name = "label4";
            label4.Size = new Size(138, 21);
            label4.TabIndex = 15;
            label4.Text = "数据导出添加方式";
            // 
            // ComboBoxForExportWriteWay
            // 
            ComboBoxForExportWriteWay.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForExportWriteWay.FormattingEnabled = true;
            ComboBoxForExportWriteWay.Location = new Point(485, 63);
            ComboBoxForExportWriteWay.Name = "ComboBoxForExportWriteWay";
            ComboBoxForExportWriteWay.Size = new Size(121, 29);
            ComboBoxForExportWriteWay.TabIndex = 16;
            ComboBoxForExportWriteWay.SelectedIndexChanged += ComboBoxForExportWriteWay_SelectedIndexChanged;
            // 
            // ComboBoxForExportConflictDealWay
            // 
            ComboBoxForExportConflictDealWay.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForExportConflictDealWay.FormattingEnabled = true;
            ComboBoxForExportConflictDealWay.Location = new Point(827, 63);
            ComboBoxForExportConflictDealWay.Name = "ComboBoxForExportConflictDealWay";
            ComboBoxForExportConflictDealWay.Size = new Size(121, 29);
            ComboBoxForExportConflictDealWay.TabIndex = 18;
            ComboBoxForExportConflictDealWay.SelectedIndexChanged += ComboBoxForExportConfigDealWay_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(656, 65);
            label1.Name = "label1";
            label1.Size = new Size(165, 21);
            label1.TabIndex = 17;
            label1.Text = "导出Key重复处理方式";
            // 
            // ExcelTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1736, 1122);
            Controls.Add(ComboBoxForExportConflictDealWay);
            Controls.Add(label1);
            Controls.Add(ComboBoxForExportWriteWay);
            Controls.Add(label4);
            Controls.Add(DataViewConfigForExportFile);
            Controls.Add(BtnChooseSourceFile);
            Controls.Add(BtnChooseExportFile);
            Controls.Add(BtnImport);
            Controls.Add(BtnExportSetting);
            Controls.Add(StartExportBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ExcelTool";
            Text = "ExcelTool";
            Load += ExcelTool_Load;
            ((System.ComponentModel.ISupportInitialize)DataViewConfigForExportFile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartExportBtn;
        private Button BtnExportSetting;
        private Button BtnImport;
        private Button BtnChooseExportFile;
        private Button BtnChooseSourceFile;
        private DataGridView DataViewConfigForExportFile;
        private Label label4;
        private ComboBox ComboBoxForExportWriteWay;
        private ComboBox ComboBoxForExportConflictDealWay;
        private Label label1;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewTextBoxColumn RelatInfo;
        private DataGridViewButtonColumn EditRelateBtnColum;
        private DataGridViewCheckBoxColumn LeaveEmptyColum;
    }
}