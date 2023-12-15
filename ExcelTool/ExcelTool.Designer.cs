namespace MyExcelTool
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
            TextForExportFilePath = new TextBox();
            label1 = new Label();
            BtnChooseExportFile = new Button();
            LableKeyStartRowIndex = new Label();
            TextBoxForKeyStartRow = new TextBox();
            TextBoxForKeyStartColm = new TextBox();
            label2 = new Label();
            TextBoxForContentStartRow = new TextBox();
            label3 = new Label();
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
            // TextForExportFilePath
            // 
            TextForExportFilePath.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextForExportFilePath.Location = new Point(876, 62);
            TextForExportFilePath.Name = "TextForExportFilePath";
            TextForExportFilePath.ReadOnly = true;
            TextForExportFilePath.Size = new Size(712, 28);
            TextForExportFilePath.TabIndex = 3;
            TextForExportFilePath.Text = "请点击选择";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(801, 65);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 4;
            label1.Text = "目标文件";
            // 
            // BtnChooseExportFile
            // 
            BtnChooseExportFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseExportFile.Location = new Point(1594, 60);
            BtnChooseExportFile.Name = "BtnChooseExportFile";
            BtnChooseExportFile.Size = new Size(130, 30);
            BtnChooseExportFile.TabIndex = 5;
            BtnChooseExportFile.Text = "选择导出文件";
            BtnChooseExportFile.UseVisualStyleBackColor = true;
            BtnChooseExportFile.Click += BntChooseExportFile_Click;
            // 
            // LableKeyStartRowIndex
            // 
            LableKeyStartRowIndex.AutoSize = true;
            LableKeyStartRowIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableKeyStartRowIndex.Location = new Point(12, 62);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 5;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(103, 59);
            TextBoxForKeyStartRow.Name = "TextBoxForKeyStartRow";
            TextBoxForKeyStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartRow.Size = new Size(39, 28);
            TextBoxForKeyStartRow.TabIndex = 6;
            TextBoxForKeyStartRow.Text = "2";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(258, 59);
            TextBoxForKeyStartColm.Name = "TextBoxForKeyStartColm";
            TextBoxForKeyStartColm.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartColm.Size = new Size(39, 28);
            TextBoxForKeyStartColm.TabIndex = 8;
            TextBoxForKeyStartColm.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(167, 62);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 7;
            label2.Text = "Key开始列";
            // 
            // TextBoxForContentStartRow
            // 
            TextBoxForContentStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForContentStartRow.Location = new Point(418, 59);
            TextBoxForContentStartRow.Name = "TextBoxForContentStartRow";
            TextBoxForContentStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForContentStartRow.Size = new Size(39, 28);
            TextBoxForContentStartRow.TabIndex = 10;
            TextBoxForContentStartRow.Text = "4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(327, 62);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 9;
            label3.Text = "内容开始行";
            // 
            // ExcelTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1736, 1122);
            Controls.Add(TextBoxForContentStartRow);
            Controls.Add(label3);
            Controls.Add(TextBoxForKeyStartColm);
            Controls.Add(label2);
            Controls.Add(TextBoxForKeyStartRow);
            Controls.Add(LableKeyStartRowIndex);
            Controls.Add(BtnChooseExportFile);
            Controls.Add(label1);
            Controls.Add(TextForExportFilePath);
            Controls.Add(BtnImport);
            Controls.Add(BtnExportSetting);
            Controls.Add(StartExportBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ExcelTool";
            Text = "ExcelTool";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartExportBtn;
        private Button BtnExportSetting;
        private Button BtnImport;
        private TextBox TextForExportFilePath;
        private Label label1;
        private Button BtnChooseExportFile;
        private Label LableKeyStartRowIndex;
        private TextBox TextBoxForKeyStartRow;
        private TextBox TextBoxForKeyStartColm;
        private Label label2;
        private TextBox TextBoxForContentStartRow;
        private Label label3;
    }
}