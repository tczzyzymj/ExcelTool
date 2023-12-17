namespace ExcelTool
{
    partial class SourceFileConfigForm
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
            button1 = new Button();
            TextBoxForContentStartRow = new TextBox();
            label3 = new Label();
            TextBoxForKeyStartColm = new TextBox();
            label2 = new Label();
            TextBoxForKeyStartRow = new TextBox();
            LableKeyStartRowIndex = new Label();
            BtnChooseExportFile = new Button();
            label1 = new Label();
            TextForExportFilePath = new TextBox();
            label5 = new Label();
            ListBoxForKeyFiler = new ListBox();
            button2 = new Button();
            ComboBoxForSelectSheet = new ComboBox();
            label4 = new Label();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(545, 505);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 34;
            button1.Text = "配置完成";
            button1.UseVisualStyleBackColor = true;
            // 
            // TextBoxForContentStartRow
            // 
            TextBoxForContentStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForContentStartRow.Location = new Point(435, 131);
            TextBoxForContentStartRow.Name = "TextBoxForContentStartRow";
            TextBoxForContentStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForContentStartRow.Size = new Size(39, 28);
            TextBoxForContentStartRow.TabIndex = 33;
            TextBoxForContentStartRow.Text = "4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(344, 134);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 32;
            label3.Text = "内容开始行";
            // 
            // TextBoxForKeyStartColm
            // 
            TextBoxForKeyStartColm.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartColm.Location = new Point(275, 131);
            TextBoxForKeyStartColm.Name = "TextBoxForKeyStartColm";
            TextBoxForKeyStartColm.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartColm.Size = new Size(39, 28);
            TextBoxForKeyStartColm.TabIndex = 31;
            TextBoxForKeyStartColm.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(184, 134);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 30;
            label2.Text = "Key开始列";
            // 
            // TextBoxForKeyStartRow
            // 
            TextBoxForKeyStartRow.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForKeyStartRow.Location = new Point(118, 131);
            TextBoxForKeyStartRow.Name = "TextBoxForKeyStartRow";
            TextBoxForKeyStartRow.RightToLeft = RightToLeft.Yes;
            TextBoxForKeyStartRow.Size = new Size(39, 28);
            TextBoxForKeyStartRow.TabIndex = 29;
            TextBoxForKeyStartRow.Text = "2";
            // 
            // LableKeyStartRowIndex
            // 
            LableKeyStartRowIndex.AutoSize = true;
            LableKeyStartRowIndex.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LableKeyStartRowIndex.Location = new Point(29, 134);
            LableKeyStartRowIndex.Name = "LableKeyStartRowIndex";
            LableKeyStartRowIndex.Size = new Size(85, 21);
            LableKeyStartRowIndex.TabIndex = 28;
            LableKeyStartRowIndex.Text = "Key开始行";
            // 
            // BtnChooseExportFile
            // 
            BtnChooseExportFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnChooseExportFile.Location = new Point(545, 19);
            BtnChooseExportFile.Name = "BtnChooseExportFile";
            BtnChooseExportFile.Size = new Size(130, 30);
            BtnChooseExportFile.TabIndex = 26;
            BtnChooseExportFile.Text = "选择文件";
            BtnChooseExportFile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(29, 24);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 25;
            label1.Text = "目标文件";
            // 
            // TextForExportFilePath
            // 
            TextForExportFilePath.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextForExportFilePath.Location = new Point(118, 22);
            TextForExportFilePath.Name = "TextForExportFilePath";
            TextForExportFilePath.ReadOnly = true;
            TextForExportFilePath.Size = new Size(416, 26);
            TextForExportFilePath.TabIndex = 24;
            TextForExportFilePath.Text = "请点击选择";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(29, 192);
            label5.Name = "label5";
            label5.Size = new Size(74, 21);
            label5.TabIndex = 35;
            label5.Text = "内容筛选";
            // 
            // ListBoxForKeyFiler
            // 
            ListBoxForKeyFiler.FormattingEnabled = true;
            ListBoxForKeyFiler.ItemHeight = 17;
            ListBoxForKeyFiler.Location = new Point(29, 223);
            ListBoxForKeyFiler.Name = "ListBoxForKeyFiler";
            ListBoxForKeyFiler.Size = new Size(646, 259);
            ListBoxForKeyFiler.TabIndex = 36;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(545, 192);
            button2.Name = "button2";
            button2.Size = new Size(130, 30);
            button2.TabIndex = 37;
            button2.Text = "添加筛选Key";
            button2.UseVisualStyleBackColor = true;
            // 
            // ComboBoxForSelectSheet
            // 
            ComboBoxForSelectSheet.FormattingEnabled = true;
            ComboBoxForSelectSheet.Location = new Point(118, 75);
            ComboBoxForSelectSheet.Name = "ComboBoxForSelectSheet";
            ComboBoxForSelectSheet.Size = new Size(121, 25);
            ComboBoxForSelectSheet.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(29, 77);
            label4.Name = "label4";
            label4.Size = new Size(83, 21);
            label4.TabIndex = 13;
            label4.Text = "选择sheet";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(418, 193);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 25);
            comboBox1.TabIndex = 38;
            // 
            // SourceFileConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(706, 553);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(ComboBoxForSelectSheet);
            Controls.Add(button2);
            Controls.Add(ListBoxForKeyFiler);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(TextBoxForContentStartRow);
            Controls.Add(label3);
            Controls.Add(TextBoxForKeyStartColm);
            Controls.Add(label2);
            Controls.Add(TextBoxForKeyStartRow);
            Controls.Add(LableKeyStartRowIndex);
            Controls.Add(BtnChooseExportFile);
            Controls.Add(label1);
            Controls.Add(TextForExportFilePath);
            Name = "SourceFileConfigForm";
            Text = "SourceFileConfigForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox TextBoxForContentStartRow;
        private Label label3;
        private TextBox TextBoxForKeyStartColm;
        private Label label2;
        private TextBox TextBoxForKeyStartRow;
        private Label LableKeyStartRowIndex;
        private Button BtnChooseExportFile;
        private Label label1;
        private TextBox TextForExportFilePath;
        private Label label5;
        private ListBox ListBoxForKeyFiler;
        private Button button2;
        private ComboBox ComboBoxForSelectSheet;
        private Label label4;
        private ComboBox comboBox1;
    }
}