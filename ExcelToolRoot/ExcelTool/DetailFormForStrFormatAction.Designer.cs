namespace ExcelTool
{
    partial class DetailFormForStrFormatAction
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
            TextBoxForFormat = new TextBox();
            label5 = new Label();
            BtnFinishConfig = new Button();
            SuspendLayout();
            // 
            // TextBoxForFormat
            // 
            TextBoxForFormat.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForFormat.Location = new Point(132, 21);
            TextBoxForFormat.Name = "TextBoxForFormat";
            TextBoxForFormat.Size = new Size(154, 26);
            TextBoxForFormat.TabIndex = 38;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 23);
            label5.Name = "label5";
            label5.Size = new Size(90, 21);
            label5.TabIndex = 39;
            label5.Text = "格式化内容";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(156, 89);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 40;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // DetailFormForStrFormatAction
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 142);
            Controls.Add(BtnFinishConfig);
            Controls.Add(label5);
            Controls.Add(TextBoxForFormat);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "DetailFormForStrFormatAction";
            Text = "DetailFormForStrFormatAction";
            Load += DetailFormForStrFormatAction_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextBoxForFormat;
        private Label label5;
        private Button BtnFinishConfig;
    }
}