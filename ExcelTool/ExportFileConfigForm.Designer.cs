namespace ExcelTool
{
    partial class ExportFileConfigForm
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
            BtnChooseExportFile = new Button();
            label1 = new Label();
            TextForExportFilePath = new TextBox();
            label4 = new Label();
            ComboBoxForSelectSheet = new ComboBox();
            TextBoxForContentStartRow = new TextBox();
            label3 = new Label();
            TextBoxForKeyStartColm = new TextBox();
            label2 = new Label();
            TextBoxForKeyStartRow = new TextBox();
            LableKeyStartRowIndex = new Label();
            BtnFinishConfig = new Button();
            SuspendLayout();
            // 
            // BtnChooseExportFile
            // 
            BtnChooseExportFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseExportFile.Location = new Point(544, 22);
            BtnChooseExportFile.Name = "BtnChooseExportFile";
            BtnChooseExportFile.Size = new Size(130, 30);
            BtnChooseExportFile.TabIndex = 8;
            BtnChooseExportFile.Text = "选择文件";
            BtnChooseExportFile.UseVisualStyleBackColor = true;
            BtnChooseExportFile.Click += BtnChooseExportFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(28, 27);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 7;
            label1.Text = "目标文件";
            // 
            // TextForExportFilePath
            // 
            TextForExportFilePath.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextForExportFilePath.Location = new Point(117, 25);
            TextForExportFilePath.Name = "TextForExportFilePath";
            TextForExportFilePath.ReadOnly = true;
            TextForExportFilePath.Size = new Size(416, 26);
            TextForExportFilePath.TabIndex = 6;
            TextForExportFilePath.Text = "请点击选择";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(28, 82);
            label4.Name = "label4";
            label4.Size = new Size(83, 21);
            label4.TabIndex = 13;
            label4.Text = "选择sheet";
            // 
            // ComboBoxForSelectSheet
            // 
            ComboBoxForSelectSheet.FormattingEnabled = true;
            ComboBoxForSelectSheet.Location = new Point(117, 80);
            ComboBoxForSelectSheet.Name = "ComboBoxForSelectSheet";
            ComboBoxForSelectSheet.Size = new Size(121, 25);
            ComboBoxForSelectSheet.TabIndex = 14;
            ComboBoxForSelectSheet.SelectedIndexChanged += ComboBoxForSelectSheet_SelectedIndexChanged;
            // 
            // TextBoxForContentStartRow
            // 
            TextBoxForContentStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForContentStartRow.Location = new Point(434, 134);
            TextBoxForContentStartRow.Name = "TextBoxForContentStartRow";
            TextBoxForContentStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForContentStartRow.Size = new Size(39, 28);
            TextBoxForContentStartRow.TabIndex = 22;
            TextBoxForContentStartRow.Text = "4";
            TextBoxForContentStartRow.TextChanged += TextBoxForContentStartRow_TextChanged;
            TextBoxForContentStartRow.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(343, 137);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 21;
            label3.Text = "内容开始行";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(274, 134);
            TextBoxForKeyStartColm.Name = "TextBoxForKeyStartColm";
            TextBoxForKeyStartColm.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartColm.Size = new Size(39, 28);
            TextBoxForKeyStartColm.TabIndex = 20;
            TextBoxForKeyStartColm.Text = "1";
            TextBoxForKeyStartColm.TextChanged += TextBoxForKeyStartColm_TextChanged;
            TextBoxForKeyStartColm.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(183, 137);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 19;
            label2.Text = "Key开始列";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(117, 134);
            TextBoxForKeyStartRow.Name = "TextBoxForKeyStartRow";
            TextBoxForKeyStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartRow.Size = new Size(39, 28);
            TextBoxForKeyStartRow.TabIndex = 18;
            TextBoxForKeyStartRow.Text = "2";
            TextBoxForKeyStartRow.TextChanged += TextBoxForKeyStartRow_TextChanged;
            TextBoxForKeyStartRow.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // LableKeyStartRowIndex
            // 
            LableKeyStartRowIndex.AutoSize = true;
            LableKeyStartRowIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableKeyStartRowIndex.Location = new Point(28, 137);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 17;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(544, 182);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 23;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // ExportFileConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 236);
            Controls.Add(label4);
            Controls.Add(ComboBoxForSelectSheet);
            Controls.Add(BtnFinishConfig);
            Controls.Add(TextBoxForContentStartRow);
            Controls.Add(label3);
            Controls.Add(TextBoxForKeyStartColm);
            Controls.Add(label2);
            Controls.Add(TextBoxForKeyStartRow);
            Controls.Add(LableKeyStartRowIndex);
            Controls.Add(BtnChooseExportFile);
            Controls.Add(label1);
            Controls.Add(TextForExportFilePath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ExportFileConfigForm";
            Text = "ExportFileConfigForm";
            Load += ExportFileConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnChooseExportFile;
        private Label label1;
        private TextBox TextForExportFilePath;
        private Label label4;
        private ComboBox ComboBoxForSelectSheet;
        private TextBox TextBoxForContentStartRow;
        private Label label3;
        private TextBox TextBoxForKeyStartColm;
        private Label label2;
        private TextBox TextBoxForKeyStartRow;
        private Label LableKeyStartRowIndex;
        private Button BtnFinishConfig;
    }
}