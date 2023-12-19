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
            TextBoxForIDColumIndex = new TextBox();
            label5 = new Label();
            PanelForConfigs = new Panel();
            TextBoxSplitSymbol = new TextBox();
            LableForSplitSymbol = new Label();
            PanelForConfigs.SuspendLayout();
            SuspendLayout();
            // 
            // BtnChooseExportFile
            // 
            BtnChooseExportFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseExportFile.Location = new Point(556, 22);
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
            label1.Location = new Point(13, 28);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 7;
            label1.Text = "目标文件";
            // 
            // TextForExportFilePath
            // 
            TextForExportFilePath.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextForExportFilePath.Location = new Point(102, 26);
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
            label4.Location = new Point(13, 78);
            label4.Name = "label4";
            label4.Size = new Size(83, 21);
            label4.TabIndex = 13;
            label4.Text = "选择sheet";
            // 
            // ComboBoxForSelectSheet
            // 
            ComboBoxForSelectSheet.FormattingEnabled = true;
            ComboBoxForSelectSheet.Location = new Point(102, 76);
            ComboBoxForSelectSheet.Name = "ComboBoxForSelectSheet";
            ComboBoxForSelectSheet.Size = new Size(121, 25);
            ComboBoxForSelectSheet.TabIndex = 14;
            ComboBoxForSelectSheet.SelectedIndexChanged += ComboBoxForSelectSheet_SelectedIndexChanged;
            // 
            // TextBoxForContentStartRow
            // 
            TextBoxForContentStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForContentStartRow.Location = new Point(509, 20);
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
            label3.Location = new Point(417, 23);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 21;
            label3.Text = "内容开始行";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(347, 20);
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
            label2.Location = new Point(261, 23);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 19;
            label2.Text = "Key开始列";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(196, 20);
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
            LableKeyStartRowIndex.Location = new Point(110, 23);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 17;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(556, 182);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 23;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // TextBoxForIDColumIndex
            // 
            TextBoxForIDColumIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForIDColumIndex.Location = new Point(48, 20);
            TextBoxForIDColumIndex.Name = "TextBoxForIDColumIndex";
            TextBoxForIDColumIndex.RightToLeft = RightToLeft.Yes;
            TextBoxForIDColumIndex.Size = new Size(39, 28);
            TextBoxForIDColumIndex.TabIndex = 25;
            TextBoxForIDColumIndex.Text = "1";
            TextBoxForIDColumIndex.TextChanged += TextBoxForIDColumIndex_TextChanged;
            TextBoxForIDColumIndex.KeyPress += TextBoxCommonProcess_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(6, 23);
            label5.Name = "label5";
            label5.Size = new Size(43, 21);
            label5.TabIndex = 24;
            label5.Text = "ID列";
            // 
            // PanelForConfigs
            // 
            PanelForConfigs.Controls.Add(TextBoxSplitSymbol);
            PanelForConfigs.Controls.Add(LableForSplitSymbol);
            PanelForConfigs.Controls.Add(LableKeyStartRowIndex);
            PanelForConfigs.Controls.Add(TextBoxForIDColumIndex);
            PanelForConfigs.Controls.Add(TextBoxForKeyStartRow);
            PanelForConfigs.Controls.Add(label5);
            PanelForConfigs.Controls.Add(label2);
            PanelForConfigs.Controls.Add(TextBoxForKeyStartColm);
            PanelForConfigs.Controls.Add(label3);
            PanelForConfigs.Controls.Add(TextBoxForContentStartRow);
            PanelForConfigs.Location = new Point(13, 111);
            PanelForConfigs.Name = "PanelForConfigs";
            PanelForConfigs.Size = new Size(673, 65);
            PanelForConfigs.TabIndex = 26;
            // 
            // TextBoxSplitSymbol
            // 
            TextBoxSplitSymbol.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxSplitSymbol.Location = new Point(632, 20);
            TextBoxSplitSymbol.Name = "TextBoxSplitSymbol";
            TextBoxSplitSymbol.RightToLeft = RightToLeft.Yes;
            TextBoxSplitSymbol.Size = new Size(39, 28);
            TextBoxSplitSymbol.TabIndex = 27;
            TextBoxSplitSymbol.Text = ",";
            TextBoxSplitSymbol.TextChanged += TextBoxSplitSymbol_TextChanged;
            // 
            // LableForSplitSymbol
            // 
            LableForSplitSymbol.AutoSize = true;
            LableForSplitSymbol.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableForSplitSymbol.Location = new Point(572, 24);
            LableForSplitSymbol.Name = "LableForSplitSymbol";
            LableForSplitSymbol.Size = new Size(58, 21);
            LableForSplitSymbol.TabIndex = 26;
            LableForSplitSymbol.Text = "分隔符";
            // 
            // ExportFileConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 227);
            Controls.Add(PanelForConfigs);
            Controls.Add(label4);
            Controls.Add(ComboBoxForSelectSheet);
            Controls.Add(BtnFinishConfig);
            Controls.Add(BtnChooseExportFile);
            Controls.Add(label1);
            Controls.Add(TextForExportFilePath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ExportFileConfigForm";
            Text = "ExportFileConfigForm";
            Load += ExportFileConfigForm_Load;
            PanelForConfigs.ResumeLayout(false);
            PanelForConfigs.PerformLayout();
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
        private TextBox TextBoxForIDColumIndex;
        private Label label5;
        private Panel PanelForConfigs;
        private TextBox TextBoxSplitSymbol;
        private Label LableForSplitSymbol;
    }
}