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
            CompareValueTypeColum = new DataGridViewTextBoxColumn();
            CompareType = new DataGridViewTextBoxColumn();
            CompareValue = new DataGridViewTextBoxColumn();
            RemoveBtnColum = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)DataGridViewForFilterFunc).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(593, 34);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 8;
            label1.Text = "目标值";
            // 
            // DataGridViewForFilterFunc
            // 
            DataGridViewForFilterFunc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewForFilterFunc.Columns.AddRange(new DataGridViewColumn[] { CompareValueTypeColum, CompareType, CompareValue, RemoveBtnColum });
            DataGridViewForFilterFunc.Location = new Point(12, 74);
            DataGridViewForFilterFunc.Name = "DataGridViewForFilterFunc";
            DataGridViewForFilterFunc.RowTemplate.Height = 25;
            DataGridViewForFilterFunc.Size = new Size(987, 315);
            DataGridViewForFilterFunc.TabIndex = 16;
            DataGridViewForFilterFunc.CellContentClick += DataGridViewForFilterFunc_CellContentClick;
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
            ComboBoxForCompareType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForCompareType.FormattingEnabled = true;
            ComboBoxForCompareType.Location = new Point(413, 32);
            ComboBoxForCompareType.Name = "ComboBoxForCompareType";
            ComboBoxForCompareType.Size = new Size(164, 25);
            ComboBoxForCompareType.TabIndex = 26;
            ComboBoxForCompareType.SelectedIndexChanged += ComboBoxForCompareType_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(333, 33);
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
            TextBoxForCompareValue.Location = new Point(651, 31);
            TextBoxForCompareValue.Name = "TextBoxForCompareValue";
            TextBoxForCompareValue.Size = new Size(194, 26);
            TextBoxForCompareValue.TabIndex = 28;
            TextBoxForCompareValue.TextChanged += TextBoxForCompareValue_TextChanged;
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
            ComboBoxForValueType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForValueType.FormattingEnabled = true;
            ComboBoxForValueType.Location = new Point(182, 31);
            ComboBoxForValueType.Name = "ComboBoxForValueType";
            ComboBoxForValueType.Size = new Size(121, 25);
            ComboBoxForValueType.TabIndex = 31;
            ComboBoxForValueType.SelectedIndexChanged += ComboBoxForValueType_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(118, 33);
            label4.Name = "label4";
            label4.Size = new Size(58, 21);
            label4.TabIndex = 30;
            label4.Text = "值类型";
            // 
            // CompareValueTypeColum
            // 
            CompareValueTypeColum.HeaderText = "值类型";
            CompareValueTypeColum.Name = "CompareValueTypeColum";
            CompareValueTypeColum.Resizable = DataGridViewTriState.True;
            CompareValueTypeColum.SortMode = DataGridViewColumnSortMode.NotSortable;
            CompareValueTypeColum.Width = 200;
            // 
            // CompareType
            // 
            CompareType.HeaderText = "比较方式";
            CompareType.Name = "CompareType";
            CompareType.Resizable = DataGridViewTriState.True;
            CompareType.SortMode = DataGridViewColumnSortMode.NotSortable;
            CompareType.Width = 200;
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
            MaximizeBox = false;
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
        private DataGridViewTextBoxColumn CompareValueTypeColum;
        private DataGridViewTextBoxColumn CompareType;
        private DataGridViewTextBoxColumn CompareValue;
        private DataGridViewButtonColumn RemoveBtnColum;
    }
}