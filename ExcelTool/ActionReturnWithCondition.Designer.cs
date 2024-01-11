namespace ExcelTool
{
    partial class ActionReturnWithCondition
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
            ComboBoxForValueType = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            TextBoxForCompareValue = new TextBox();
            BtnAddFilterFunc = new Button();
            ComboBoxForCompareType = new ComboBox();
            label2 = new Label();
            BtnFinishConfig = new Button();
            DataGridViewForFilterFunc = new DataGridView();
            CompareTypeColum = new DataGridViewTextBoxColumn();
            CompareType = new DataGridViewTextBoxColumn();
            CompareValue = new DataGridViewTextBoxColumn();
            RemoveBtnColum = new DataGridViewButtonColumn();
            label1 = new Label();
            TextBoxForReturnString = new TextBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)DataGridViewForFilterFunc).BeginInit();
            SuspendLayout();
            // 
            // ComboBoxForValueType
            // 
            ComboBoxForValueType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForValueType.FormattingEnabled = true;
            ComboBoxForValueType.Location = new Point(197, 19);
            ComboBoxForValueType.Name = "ComboBoxForValueType";
            ComboBoxForValueType.Size = new Size(121, 25);
            ComboBoxForValueType.TabIndex = 41;
            ComboBoxForValueType.SelectedIndexChanged += ComboBoxForValueType_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(133, 21);
            label4.Name = "label4";
            label4.Size = new Size(58, 21);
            label4.TabIndex = 40;
            label4.Text = "值类型";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(17, 21);
            label3.Name = "label3";
            label3.Size = new Size(74, 21);
            label3.TabIndex = 39;
            label3.Text = "表格内值";
            // 
            // TextBoxForCompareValue
            // 
            TextBoxForCompareValue.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForCompareValue.Location = new Point(626, 18);
            TextBoxForCompareValue.Name = "TextBoxForCompareValue";
            TextBoxForCompareValue.Size = new Size(194, 26);
            TextBoxForCompareValue.TabIndex = 38;
            // 
            // BtnAddFilterFunc
            // 
            BtnAddFilterFunc.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAddFilterFunc.Location = new Point(875, 16);
            BtnAddFilterFunc.Name = "BtnAddFilterFunc";
            BtnAddFilterFunc.Size = new Size(130, 30);
            BtnAddFilterFunc.TabIndex = 37;
            BtnAddFilterFunc.Text = "添加";
            BtnAddFilterFunc.UseVisualStyleBackColor = true;
            BtnAddFilterFunc.Click += BtnAddFilterFunc_Click;
            // 
            // ComboBoxForCompareType
            // 
            ComboBoxForCompareType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForCompareType.FormattingEnabled = true;
            ComboBoxForCompareType.Location = new Point(415, 20);
            ComboBoxForCompareType.Name = "ComboBoxForCompareType";
            ComboBoxForCompareType.Size = new Size(121, 25);
            ComboBoxForCompareType.TabIndex = 36;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(342, 21);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 35;
            label2.Text = "比较方式";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(875, 393);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 34;
            BtnFinishConfig.Text = "配置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // DataGridViewForFilterFunc
            // 
            DataGridViewForFilterFunc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewForFilterFunc.Columns.AddRange(new DataGridViewColumn[] { CompareTypeColum, CompareType, CompareValue, RemoveBtnColum });
            DataGridViewForFilterFunc.Location = new Point(18, 61);
            DataGridViewForFilterFunc.Name = "DataGridViewForFilterFunc";
            DataGridViewForFilterFunc.RowTemplate.Height = 25;
            DataGridViewForFilterFunc.Size = new Size(987, 315);
            DataGridViewForFilterFunc.TabIndex = 33;
            DataGridViewForFilterFunc.CellContentClick += DataGridViewForFilterFunc_CellContentClick;
            // 
            // CompareTypeColum
            // 
            CompareTypeColum.HeaderText = "值类型";
            CompareTypeColum.Name = "CompareTypeColum";
            CompareTypeColum.Resizable = DataGridViewTriState.True;
            CompareTypeColum.SortMode = DataGridViewColumnSortMode.NotSortable;
            CompareTypeColum.Width = 200;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(568, 21);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 32;
            label1.Text = "目标值";
            // 
            // TextBoxForReturnString
            // 
            TextBoxForReturnString.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForReturnString.Location = new Point(98, 395);
            TextBoxForReturnString.Name = "TextBoxForReturnString";
            TextBoxForReturnString.Size = new Size(194, 26);
            TextBoxForReturnString.TabIndex = 43;
            TextBoxForReturnString.TextChanged += TextBoxForReturnString_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(21, 398);
            label5.Name = "label5";
            label5.Size = new Size(58, 21);
            label5.TabIndex = 42;
            label5.Text = "返回值";
            // 
            // ActionReturnWithCondition
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 435);
            Controls.Add(TextBoxForReturnString);
            Controls.Add(label5);
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
            Name = "ActionReturnWithCondition";
            Text = "ActionReturnWithCondition";
            Load += ActionReturnWithCondition_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewForFilterFunc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ComboBoxForValueType;
        private Label label4;
        private Label label3;
        private TextBox TextBoxForCompareValue;
        private Button BtnAddFilterFunc;
        private ComboBox ComboBoxForCompareType;
        private Label label2;
        private Button BtnFinishConfig;
        private DataGridView DataGridViewForFilterFunc;
        private Label label1;
        private TextBox TextBoxForReturnString;
        private Label label5;
        private DataGridViewTextBoxColumn CompareTypeColum;
        private DataGridViewTextBoxColumn CompareType;
        private DataGridViewTextBoxColumn CompareValue;
        private DataGridViewButtonColumn RemoveBtnColum;
    }
}