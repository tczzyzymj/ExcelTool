namespace ExcelTool
{
    partial class ActionSubStringForm
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
            label7 = new Label();
            label1 = new Label();
            BtnFinishConfig = new Button();
            TextBoxForBeginIndex = new TextBox();
            TextBoxForSubLength = new TextBox();
            label2 = new Label();
            CheckBoxForThrow = new CheckBox();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(28, 15);
            label7.Name = "label7";
            label7.Size = new Size(189, 21);
            label7.TabIndex = 60;
            label7.Text = "开始下标(大于等于0整数)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(28, 77);
            label1.Name = "label1";
            label1.Size = new Size(189, 21);
            label1.TabIndex = 62;
            label1.Text = "截取长度(0表示剩下全截)";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(133, 184);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 64;
            BtnFinishConfig.Text = "设置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // TextBoxForBeginIndex
            // 
            TextBoxForBeginIndex.Location = new Point(28, 39);
            TextBoxForBeginIndex.Name = "TextBoxForBeginIndex";
            TextBoxForBeginIndex.Size = new Size(100, 23);
            TextBoxForBeginIndex.TabIndex = 65;
            // 
            // TextBoxForSubLength
            // 
            TextBoxForSubLength.Location = new Point(28, 102);
            TextBoxForSubLength.Name = "TextBoxForSubLength";
            TextBoxForSubLength.Size = new Size(100, 23);
            TextBoxForSubLength.TabIndex = 66;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(28, 141);
            label2.Name = "label2";
            label2.Size = new Size(122, 21);
            label2.TabIndex = 67;
            label2.Text = "出错是否抛异常";
            // 
            // CheckBoxForThrow
            // 
            CheckBoxForThrow.AutoSize = true;
            CheckBoxForThrow.Location = new Point(156, 144);
            CheckBoxForThrow.Name = "CheckBoxForThrow";
            CheckBoxForThrow.Size = new Size(15, 14);
            CheckBoxForThrow.TabIndex = 68;
            CheckBoxForThrow.UseVisualStyleBackColor = true;
            CheckBoxForThrow.CheckedChanged += CheckBoxForThrow_CheckedChanged;
            // 
            // ActionSubStringForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(275, 227);
            Controls.Add(CheckBoxForThrow);
            Controls.Add(label2);
            Controls.Add(TextBoxForSubLength);
            Controls.Add(TextBoxForBeginIndex);
            Controls.Add(BtnFinishConfig);
            Controls.Add(label1);
            Controls.Add(label7);
            Name = "ActionSubStringForm";
            Text = "ActionSubStringForm";
            Load += ActionSubStringForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label7;
        private Label label1;
        private Button BtnFinishConfig;
        private TextBox TextBoxForBeginIndex;
        private TextBox TextBoxForSubLength;
        private Label label2;
        private CheckBox CheckBoxForThrow;
    }
}