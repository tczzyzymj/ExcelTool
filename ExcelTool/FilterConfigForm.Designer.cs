namespace ExcelTool
{
    partial class FilterConfigForm
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
            label1 = new Label();
            DataGridViewForFilterFunc = new DataGridView();
            BtnFinishConfig = new Button();
            ComboBoxForCompareType = new ComboBox();
            label2 = new Label();
            BtnAddFilterFunc = new Button();
            TextBoxForCompareValue = new TextBox();
            label3 = new Label();
            ComboBoxForValueType = new ComboBox();
            label4 = new Label();
            CompareType = new DataGridViewComboBoxColumn();
            CompareValue = new DataGridViewTextBoxColumn();
            RemoveBtnColum = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)DataGridViewForFilterFunc).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(336, 34);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 8;
            label1.Text = "目标值";
            // 
            // DataGridViewForFilterFunc
            // 
            DataGridViewForFilterFunc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewForFilterFunc.Columns.AddRange(new DataGridViewColumn[] { CompareType, CompareValue, RemoveBtnColum });
            DataGridViewForFilterFunc.Location = new Point(12, 74);
            DataGridViewForFilterFunc.Name = "DataGridViewForFilterFunc";
            DataGridViewForFilterFunc.RowTemplate.Height = 25;
            DataGridViewForFilterFunc.Size = new Size(987, 315);
            DataGridViewForFilterFunc.TabIndex = 16;
            DataGridViewForFilterFunc.CellClick += DataGridViewForFilterFunc_CellClick;
            DataGridViewForFilterFunc.CellValueChanged += DataGridViewForFilterFunc_CellValueChanged;
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(869, 408);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 24;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // ComboBoxForCompareType
            // 
            ComboBoxForCompareType.FormattingEnabled = true;
            ComboBoxForCompareType.Location = new Point(172, 33);
            ComboBoxForCompareType.Name = "ComboBoxForCompareType";
            ComboBoxForCompareType.Size = new Size(121, 25);
            ComboBoxForCompareType.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(99, 34);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 25;
            label2.Text = "比较方式";
            // 
            // BtnAddFilterFunc
            // 
            BtnAddFilterFunc.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAddFilterFunc.Location = new Point(869, 29);
            BtnAddFilterFunc.Name = "BtnAddFilterFunc";
            BtnAddFilterFunc.Size = new Size(130, 30);
            BtnAddFilterFunc.TabIndex = 27;
            BtnAddFilterFunc.Text = "添加";
            BtnAddFilterFunc.UseVisualStyleBackColor = true;
            BtnAddFilterFunc.Click += BtnAddFilterFunc_Click;
            // 
            // TextBoxForCompareValue
            // 
            TextBoxForCompareValue.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForCompareValue.Location = new Point(394, 31);
            TextBoxForCompareValue.Name = "TextBoxForCompareValue";
            TextBoxForCompareValue.Size = new Size(194, 26);
            TextBoxForCompareValue.TabIndex = 28;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(11, 34);
            label3.Name = "label3";
            label3.Size = new Size(74, 21);
            label3.TabIndex = 29;
            label3.Text = "表格内值";
            // 
            // ComboBoxForValueType
            // 
            ComboBoxForValueType.FormattingEnabled = true;
            ComboBoxForValueType.Location = new Point(696, 31);
            ComboBoxForValueType.Name = "ComboBoxForValueType";
            ComboBoxForValueType.Size = new Size(121, 25);
            ComboBoxForValueType.TabIndex = 31;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(632, 33);
            label4.Name = "label4";
            label4.Size = new Size(58, 21);
            label4.TabIndex = 30;
            label4.Text = "值类型";
            // 
            // CompareType
            // 
            CompareType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CompareType.HeaderText = "比较类型";
            CompareType.Name = "CompareType";
            // 
            // CompareValue
            // 
            CompareValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CompareValue.HeaderText = "对比值";
            CompareValue.Name = "CompareValue";
            // 
            // RemoveBtnColum
            // 
            RemoveBtnColum.HeaderText = "移除按钮";
            RemoveBtnColum.Name = "RemoveBtnColum";
            // 
            // FilterConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 450);
            Controls.Add(ComboBoxForValueType);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(TextBoxForCompareValue);
            Controls.Add(BtnAddFilterFunc);
            Controls.Add(ComboBoxForCompareType);
            Controls.Add(label2);
            Controls.Add(BtnFinishConfig);
            Controls.Add(DataGridViewForFilterFunc);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FilterConfigForm";
            Text = "FilterConfigForm";
            Load += FilterConfigForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewForFilterFunc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView DataGridViewForFilterFunc;
        private Button BtnFinishConfig;
        private ComboBox ComboBoxForCompareType;
        private Label label2;
        private Button BtnAddFilterFunc;
        private TextBox TextBoxForCompareValue;
        private Label label3;
        private ComboBox ComboBoxForValueType;
        private Label label4;
        private DataGridViewComboBoxColumn CompareType;
        private DataGridViewTextBoxColumn CompareValue;
        private DataGridViewButtonColumn RemoveBtnColum;
    }
}