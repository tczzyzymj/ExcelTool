namespace ExcelTool
{
    partial class ActionReturnMatchKeyIndexDetail
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
            BtnFinishConfig = new Button();
            label5 = new Label();
            TextBoxForFormat = new TextBox();
            SuspendLayout();
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(155, 70);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 43;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 19);
            label5.Name = "label5";
            label5.Size = new Size(132, 21);
            label5.TabIndex = 42;
            label5.Text = "下标变化值(加法)";
            // 
            // TextBoxForFormat
            // 
            TextBoxForFormat.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForFormat.Location = new Point(150, 17);
            TextBoxForFormat.Name = "TextBoxForFormat";
            TextBoxForFormat.Size = new Size(135, 26);
            TextBoxForFormat.TabIndex = 41;
            // 
            // ActionReturnMatchKeyIndexDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 117);
            Controls.Add(BtnFinishConfig);
            Controls.Add(label5);
            Controls.Add(TextBoxForFormat);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ActionReturnMatchKeyIndexDetail";
            Text = "ActionReturnMatchKeyIndexDetail";
            Load += ActionReturnMatchKeyIndexDetail_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnFinishConfig;
        private Label label5;
        private TextBox TextBoxForFormat;
    }
}