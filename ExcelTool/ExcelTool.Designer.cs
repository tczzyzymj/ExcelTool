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
            StartExportBtn = new Button();
            BtnExportSetting = new Button();
            BtnImport = new Button();
            BtnChooseExportFile = new Button();
            BtnAnalysis = new Button();
            button1 = new Button();
            DataVewConfigForExportFile = new DataGridView();
            KeyName = new DataGridViewTextBoxColumn();
            RelatInfo = new DataGridViewTextBoxColumn();
            EditRelateBtnColum = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)DataVewConfigForExportFile).BeginInit();
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
            BtnAnalysis.Text = "开始解析";
            BtnAnalysis.UseVisualStyleBackColor = true;
            BtnAnalysis.Click += BtnAnalysis_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(167, 60);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 13;
            button1.Text = "选择数据源文件";
            button1.UseVisualStyleBackColor = true;
            // 
            // DataVewConfigForExportFile
            // 
            DataVewConfigForExportFile.BackgroundColor = SystemColors.ControlLight;
            DataVewConfigForExportFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataVewConfigForExportFile.Columns.AddRange(new DataGridViewColumn[] { KeyName, RelatInfo, EditRelateBtnColum });
            DataVewConfigForExportFile.Location = new Point(12, 116);
            DataVewConfigForExportFile.Name = "DataVewConfigForExportFile";
            DataVewConfigForExportFile.RowTemplate.Height = 25;
            DataVewConfigForExportFile.Size = new Size(1712, 936);
            DataVewConfigForExportFile.TabIndex = 14;
            // 
            // KeyName
            // 
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "导出目标列名";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            KeyName.Width = 200;
            // 
            // RelatInfo
            // 
            RelatInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            EditRelateBtnColum.Width = 200;
            // 
            // ExcelTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1736, 1122);
            Controls.Add(DataVewConfigForExportFile);
            Controls.Add(button1);
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
            ((System.ComponentModel.ISupportInitialize)DataVewConfigForExportFile).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button StartExportBtn;
        private Button BtnExportSetting;
        private Button BtnImport;
        private Button BtnChooseExportFile;
        private Button BtnAnalysis;
        private Button button1;
        private DataGridView DataVewConfigForExportFile;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewTextBoxColumn RelatInfo;
        private DataGridViewButtonColumn EditRelateBtnColum;
    }
}