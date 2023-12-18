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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            StartExportBtn = new Button();
            BtnExportSetting = new Button();
            BtnImport = new Button();
            BtnChooseExportFile = new Button();
            BtnAnalysis = new Button();
            BtnChooseSourceFile = new Button();
            DataViewConfigForExportFile = new DataGridView();
            KeyName = new DataGridViewTextBoxColumn();
            RelatInfo = new DataGridViewTextBoxColumn();
            EditRelateBtnColum = new DataGridViewButtonColumn();
            HasEdited = new DataGridViewCheckBoxColumn();
            label4 = new Label();
            ComboBoxForExportWriteWay = new ComboBox();
            ComboBoxForExportConfigDealWay = new ComboBox();
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
            BtnChooseExportFile.Text = "选择导出目标";
            BtnChooseExportFile.UseVisualStyleBackColor = true;
            BtnChooseExportFile.Click += BntChooseExportFile_Click;
            // 
            // BtnAnalysis
            // 
            BtnAnalysis.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAnalysis.Location = new Point(327, 60);
            BtnAnalysis.Name = "BtnAnalysis";
            BtnAnalysis.Size = new Size(130, 30);
            BtnAnalysis.TabIndex = 12;
            BtnAnalysis.Text = "解析导出Key";
            BtnAnalysis.UseVisualStyleBackColor = true;
            BtnAnalysis.Click += BtnAnalysis_Click;
            // 
            // BtnChooseSourceFile
            // 
            BtnChooseSourceFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseSourceFile.Location = new Point(167, 60);
            BtnChooseSourceFile.Name = "BtnChooseSourceFile";
            BtnChooseSourceFile.Size = new Size(130, 30);
            BtnChooseSourceFile.TabIndex = 13;
            BtnChooseSourceFile.Text = "选择数据源文件";
            BtnChooseSourceFile.UseVisualStyleBackColor = true;
            BtnChooseSourceFile.Click += BtnChooseSourceFile_Click;
            // 
            // DataViewConfigForExportFile
            // 
            DataViewConfigForExportFile.BackgroundColor = SystemColors.ControlLight;
            DataViewConfigForExportFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewConfigForExportFile.Columns.AddRange(new DataGridViewColumn[] { KeyName, RelatInfo, EditRelateBtnColum, HasEdited });
            DataViewConfigForExportFile.Location = new Point(12, 116);
            DataViewConfigForExportFile.Name = "DataViewConfigForExportFile";
            DataViewConfigForExportFile.RowTemplate.Height = 25;
            DataViewConfigForExportFile.Size = new Size(1712, 936);
            DataViewConfigForExportFile.TabIndex = 14;
            DataViewConfigForExportFile.CellContentClick += DataVewConfigForExportFile_CellContentClick;
            // 
            // KeyName
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle3;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "导出目标列名";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            KeyName.Width = 200;
            // 
            // RelatInfo
            // 
            RelatInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RelatInfo.DefaultCellStyle = dataGridViewCellStyle4;
            RelatInfo.FillWeight = 160F;
            RelatInfo.HeaderText = "关联信息";
            RelatInfo.Name = "RelatInfo";
            RelatInfo.ReadOnly = true;
            // 
            // EditRelateBtnColum
            // 
            EditRelateBtnColum.FillWeight = 160F;
            EditRelateBtnColum.HeaderText = "编辑按钮";
            EditRelateBtnColum.Name = "EditRelateBtnColum";
            EditRelateBtnColum.Text = "编辑";
            EditRelateBtnColum.ToolTipText = "编辑";
            // 
            // HasEdited
            // 
            HasEdited.FillWeight = 160F;
            HasEdited.HeaderText = "是否已编辑";
            HasEdited.Name = "HasEdited";
            HasEdited.ReadOnly = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(524, 65);
            label4.Name = "label4";
            label4.Size = new Size(138, 21);
            label4.TabIndex = 15;
            label4.Text = "数据导出添加方式";
            // 
            // ComboBoxForExportWriteWay
            // 
            ComboBoxForExportWriteWay.FormattingEnabled = true;
            ComboBoxForExportWriteWay.Location = new Point(668, 61);
            ComboBoxForExportWriteWay.Name = "ComboBoxForExportWriteWay";
            ComboBoxForExportWriteWay.Size = new Size(121, 25);
            ComboBoxForExportWriteWay.TabIndex = 16;
            // 
            // ComboBoxForExportConfigDealWay
            // 
            ComboBoxForExportConfigDealWay.FormattingEnabled = true;
            ComboBoxForExportConfigDealWay.Location = new Point(1029, 60);
            ComboBoxForExportConfigDealWay.Name = "ComboBoxForExportConfigDealWay";
            ComboBoxForExportConfigDealWay.Size = new Size(121, 25);
            ComboBoxForExportConfigDealWay.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(858, 65);
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
            Controls.Add(ComboBoxForExportConfigDealWay);
            Controls.Add(label1);
            Controls.Add(ComboBoxForExportWriteWay);
            Controls.Add(label4);
            Controls.Add(DataViewConfigForExportFile);
            Controls.Add(BtnChooseSourceFile);
            Controls.Add(BtnAnalysis);
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
        private Button BtnAnalysis;
        private Button BtnChooseSourceFile;
        private DataGridView DataViewConfigForExportFile;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewTextBoxColumn RelatInfo;
        private DataGridViewButtonColumn EditRelateBtnColum;
        private DataGridViewCheckBoxColumn HasEdited;
        private Label label4;
        private ComboBox ComboBoxForExportWriteWay;
        private ComboBox ComboBoxForExportConfigDealWay;
        private Label label1;
    }
}