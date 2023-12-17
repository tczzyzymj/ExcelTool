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
            LableKeyStartRowIndex = new Label();
            TextBoxForKeyStartRow = new TextBox();
            TextBoxForKeyStartColm = new TextBox();
            label2 = new Label();
            TextBoxForContentStartRow = new TextBox();
            label3 = new Label();
            panel1 = new Panel();
            ListViewMain = new ListView();
            BtnAnalysis = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
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
            BtnChooseExportFile.Text = "选择导出文件";
            BtnChooseExportFile.UseVisualStyleBackColor = true;
            BtnChooseExportFile.Click += BntChooseExportFile_Click;
            // 
            // LableKeyStartRowIndex
            // 
            LableKeyStartRowIndex.AutoSize = true;
            LableKeyStartRowIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableKeyStartRowIndex.Location = new Point(1069, 61);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 5;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(1160, 58);
            TextBoxForKeyStartRow.Name = "TextBoxForKeyStartRow";
            TextBoxForKeyStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartRow.Size = new Size(39, 28);
            TextBoxForKeyStartRow.TabIndex = 6;
            TextBoxForKeyStartRow.Text = "2";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(1315, 58);
            TextBoxForKeyStartColm.Name = "TextBoxForKeyStartColm";
            TextBoxForKeyStartColm.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartColm.Size = new Size(39, 28);
            TextBoxForKeyStartColm.TabIndex = 8;
            TextBoxForKeyStartColm.Text = "1";
            TextBoxForKeyStartColm.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(1224, 61);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 7;
            label2.Text = "Key开始列";
            // 
            // TextBoxForContentStartRow
            // 
            TextBoxForContentStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForContentStartRow.Location = new Point(1475, 58);
            TextBoxForContentStartRow.Name = "TextBoxForContentStartRow";
            TextBoxForContentStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForContentStartRow.Size = new Size(39, 28);
            TextBoxForContentStartRow.TabIndex = 10;
            TextBoxForContentStartRow.Text = "4";
            TextBoxForContentStartRow.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(1384, 61);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 9;
            label3.Text = "内容开始行";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ListViewMain);
            panel1.Location = new Point(12, 96);
            panel1.Name = "panel1";
            panel1.Size = new Size(1712, 975);
            panel1.TabIndex = 11;
            // 
            // ListViewMain
            // 
            ListViewMain.Location = new Point(-1, -1);
            ListViewMain.Name = "ListViewMain";
            ListViewMain.Size = new Size(1712, 975);
            ListViewMain.TabIndex = 1;
            ListViewMain.UseCompatibleStateImageBehavior = false;
            // 
            // BtnAnalysis
            // 
            BtnAnalysis.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAnalysis.Location = new Point(1594, 56);
            BtnAnalysis.Name = "BtnAnalysis";
            BtnAnalysis.Size = new Size(130, 30);
            BtnAnalysis.TabIndex = 12;
            BtnAnalysis.Text = "解析";
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
            button1.Text = "选择导出文件";
            button1.UseVisualStyleBackColor = true;
            // 
            // ExcelTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1736, 1122);
            Controls.Add(button1);
            Controls.Add(BtnAnalysis);
            Controls.Add(panel1);
            Controls.Add(TextBoxForContentStartRow);
            Controls.Add(label3);
            Controls.Add(TextBoxForKeyStartColm);
            Controls.Add(label2);
            Controls.Add(TextBoxForKeyStartRow);
            Controls.Add(LableKeyStartRowIndex);
            Controls.Add(BtnChooseExportFile);
            Controls.Add(BtnImport);
            Controls.Add(BtnExportSetting);
            Controls.Add(StartExportBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ExcelTool";
            Text = "ExcelTool";
            Load += ExcelTool_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartExportBtn;
        private Button BtnExportSetting;
        private Button BtnImport;
        private Button BtnChooseExportFile;
        private Label LableKeyStartRowIndex;
        private TextBox TextBoxForKeyStartRow;
        private TextBox TextBoxForKeyStartColm;
        private Label label2;
        private TextBox TextBoxForContentStartRow;
        private Label label3;
        private Panel panel1;
        private ListView ListViewMain;
        private Button BtnAnalysis;
        private Button button1;
    }
}