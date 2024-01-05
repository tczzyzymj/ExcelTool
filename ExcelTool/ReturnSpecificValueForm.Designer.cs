namespace ExcelTool
{
    partial class ReturnSpecificValueForm
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
            TextBoxForReturnValue = new TextBox();
            SuspendLayout();
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(156, 75);
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
            label5.Location = new Point(12, 17);
            label5.Name = "label5";
            label5.Size = new Size(74, 21);
            label5.TabIndex = 42;
            label5.Text = "指定内容";
            // 
            // TextBoxForReturnValue
            // 
            TextBoxForReturnValue.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForReturnValue.Location = new Point(132, 15);
            TextBoxForReturnValue.Name = "TextBoxForReturnValue";
            TextBoxForReturnValue.Size = new Size(154, 26);
            TextBoxForReturnValue.TabIndex = 41;
            // 
            // ReturnSpecificValueForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 125);
            Controls.Add(BtnFinishConfig);
            Controls.Add(label5);
            Controls.Add(TextBoxForReturnValue);
            Name = "ReturnSpecificValueForm";
            Text = "ReturnSpecificValueForm";
            Load += ReturnSpecificValueForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnFinishConfig;
        private Label label5;
        private TextBox TextBoxForReturnValue;
    }
}