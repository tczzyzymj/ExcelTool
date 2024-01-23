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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            StartExportBtn = new Button();
            BtnExportSetting = new Button();
            BtnImport = new Button();
            BtnChooseExportFile = new Button();
            BtnChooseSourceFile = new Button();
            DataViewConfigForExportFile = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
            RelatInfo = new DataGridViewTextBoxColumn();
            HasConfigAction = new DataGridViewCheckBoxColumn();
            EditRelateBtnColum = new DataGridViewButtonColumn();
            LeaveEmptyColum = new DataGridViewCheckBoxColumn();
            IsMainKey = new DataGridViewCheckBoxColumn();
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
            StartExportBtn.Location = new Point(1594, 1033);
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
            BtnExportSetting.Location = new Point(1230, 16);
            BtnExportSetting.Name = "BtnExportSetting";
            BtnExportSetting.Size = new Size(130, 30);
            BtnExportSetting.TabIndex = 1;
            BtnExportSetting.Text = "导出配置";
            BtnExportSetting.UseVisualStyleBackColor = true;
            BtnExportSetting.Visible = false;
            BtnExportSetting.Click += BtnExportSetting_Click;
            // 
            // BtnImport
            // 
            BtnImport.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnImport.Location = new Point(1394, 16);
            BtnImport.Name = "BtnImport";
            BtnImport.Size = new Size(130, 30);
            BtnImport.TabIndex = 2;
            BtnImport.Text = "导入配置";
            BtnImport.UseVisualStyleBackColor = true;
            BtnImport.Visible = false;
            BtnImport.Click += BtnImport_Click;
            // 
            // BtnChooseExportFile
            // 
            BtnChooseExportFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseExportFile.Location = new Point(12, 16);
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
            BtnChooseSourceFile.Location = new Point(167, 16);
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
            DataViewConfigForExportFile.Columns.AddRange(new DataGridViewColumn[] { Key, KeyName, RelatInfo, HasConfigAction, EditRelateBtnColum, LeaveEmptyColum, IsMainKey });
            DataViewConfigForExportFile.Location = new Point(12, 72);
            DataViewConfigForExportFile.Name = "DataViewConfigForExportFile";
            DataViewConfigForExportFile.RowTemplate.Height = 25;
            DataViewConfigForExportFile.Size = new Size(1712, 936);
            DataViewConfigForExportFile.TabIndex = 14;
            DataViewConfigForExportFile.CellContentClick += DataViewConfigForExportFile_CellContentClick;
            // 
            // Key
            // 
            Key.HeaderText = "Index";
            Key.Name = "Key";
            Key.ToolTipText = "下标，以表格为基准";
            Key.Width = 60;
            // 
            // KeyName
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle5;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "KeyName";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            KeyName.ToolTipText = "选择的导出文件的列名";
            KeyName.Width = 200;
            // 
            // RelatInfo
            // 
            RelatInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RelatInfo.DefaultCellStyle = dataGridViewCellStyle6;
            RelatInfo.FillWeight = 160F;
            RelatInfo.HeaderText = "关联信息";
            RelatInfo.Name = "RelatInfo";
            RelatInfo.ReadOnly = true;
            RelatInfo.ToolTipText = "Key的关联信息";
            // 
            // HasConfigAction
            // 
            HasConfigAction.HeaderText = "是否已配置";
            HasConfigAction.Name = "HasConfigAction";
            HasConfigAction.ReadOnly = true;
            // 
            // EditRelateBtnColum
            // 
            EditRelateBtnColum.FillWeight = 160F;
            EditRelateBtnColum.HeaderText = "配置数据";
            EditRelateBtnColum.Name = "EditRelateBtnColum";
            EditRelateBtnColum.Text = "配置数据";
            EditRelateBtnColum.ToolTipText = "配置数据";
            // 
            // LeaveEmptyColum
            // 
            LeaveEmptyColum.HeaderText = "是否忽略";
            LeaveEmptyColum.Name = "LeaveEmptyColum";
            LeaveEmptyColum.ToolTipText = "如果忽略，那么导出的时候不会写入内容";
            // 
            // IsMainKey
            // 
            IsMainKey.HeaderText = "是否为主Key";
            IsMainKey.Name = "IsMainKey";
            IsMainKey.ToolTipText = "当设置为主key，并且没有关联数据的时候，会自动以最大ID为基准+1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(341, 21);
            label4.Name = "label4";
            label4.Size = new Size(138, 21);
            label4.TabIndex = 15;
            label4.Text = "数据导出添加方式";
            // 
            // ComboBoxForExportWriteWay
            // 
            ComboBoxForExportWriteWay.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForExportWriteWay.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForExportWriteWay.FormattingEnabled = true;
            ComboBoxForExportWriteWay.Location = new Point(485, 19);
            ComboBoxForExportWriteWay.Name = "ComboBoxForExportWriteWay";
            ComboBoxForExportWriteWay.Size = new Size(165, 29);
            ComboBoxForExportWriteWay.TabIndex = 16;
            ComboBoxForExportWriteWay.SelectedIndexChanged += ComboBoxForExportWriteWay_SelectedIndexChanged;
            // 
            // ComboBoxForExportConflictDealWay
            // 
            ComboBoxForExportConflictDealWay.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForExportConflictDealWay.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForExportConflictDealWay.FormattingEnabled = true;
            ComboBoxForExportConflictDealWay.Location = new Point(870, 19);
            ComboBoxForExportConflictDealWay.Name = "ComboBoxForExportConflictDealWay";
            ComboBoxForExportConflictDealWay.Size = new Size(327, 29);
            ComboBoxForExportConflictDealWay.TabIndex = 18;
            ComboBoxForExportConflictDealWay.SelectedIndexChanged += ComboBoxForExportConflictDealWay_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(699, 21);
            label1.Name = "label1";
            label1.Size = new Size(165, 21);
            label1.TabIndex = 17;
            label1.Text = "导出Key重复处理方式";
            // 
            // ExcelTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1736, 1079);
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
        private DataGridViewCheckBoxColumn HasConfigAction;
        private DataGridViewButtonColumn EditRelateBtnColum;
        private DataGridViewCheckBoxColumn LeaveEmptyColum;
        private DataGridViewCheckBoxColumn IsMainKey;
    }
}