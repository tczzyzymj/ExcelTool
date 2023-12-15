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
            SuspendLayout();
            // 
            // StartExportBtn
            // 
            StartExportBtn.Location = new Point(1581, 926);
            StartExportBtn.Name = "StartExportBtn";
            StartExportBtn.Size = new Size(130, 50);
            StartExportBtn.TabIndex = 0;
            StartExportBtn.Text = "开始导表";
            StartExportBtn.UseVisualStyleBackColor = true;
            // 
            // BtnExportSetting
            // 
            BtnExportSetting.Location = new Point(1427, 926);
            BtnExportSetting.Name = "BtnExportSetting";
            BtnExportSetting.Size = new Size(130, 50);
            BtnExportSetting.TabIndex = 1;
            BtnExportSetting.Text = "导出配置";
            BtnExportSetting.UseVisualStyleBackColor = true;
            BtnExportSetting.Click += BtnExportSetting_Click;
            // 
            // ExcelTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1736, 1017);
            Controls.Add(BtnExportSetting);
            Controls.Add(StartExportBtn);
            Name = "ExcelTool";
            Text = "ExcelTool";
            ResumeLayout(false);
        }

        #endregion

        private Button StartExportBtn;
        private Button BtnExportSetting;
    }
}