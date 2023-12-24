namespace ExcelTool
{
    partial class KeyConnectEditForm
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
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
            BtnLoadNewFile = new Button();
            ComboBoxForLoadedFile = new ComboBox();
            label1 = new Label();
            DataViewForKeyConfig = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
            RelatInfo = new DataGridViewTextBoxColumn();
            EditRelateBtnColum = new DataGridViewButtonColumn();
            IsDataSource = new DataGridViewCheckBoxColumn();
            LabelForFromTable = new Label();
            BtnFinishConfig = new Button();
            label3 = new Label();
            ComboBoxForWorkSheet = new ComboBox();
            BtnReset = new Button();
            BtnSearch = new Button();
            TextBoxForSearch = new TextBox();
            label5 = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewButtonColumn1 = new DataGridViewButtonColumn();
            dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
            button1 = new Button();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label6 = new Label();
            comboBox2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyConfig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // BtnLoadNewFile
            // 
            BtnLoadNewFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLoadNewFile.Location = new Point(878, 17);
            BtnLoadNewFile.Name = "BtnLoadNewFile";
            BtnLoadNewFile.Size = new Size(130, 30);
            BtnLoadNewFile.TabIndex = 17;
            BtnLoadNewFile.Text = "加载新表格";
            BtnLoadNewFile.UseVisualStyleBackColor = true;
            BtnLoadNewFile.Click += BtnLoadNewFile_Click;
            // 
            // ComboBoxForLoadedFile
            // 
            ComboBoxForLoadedFile.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForLoadedFile.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForLoadedFile.FormattingEnabled = true;
            ComboBoxForLoadedFile.Location = new Point(469, 19);
            ComboBoxForLoadedFile.Name = "ComboBoxForLoadedFile";
            ComboBoxForLoadedFile.Size = new Size(130, 29);
            ComboBoxForLoadedFile.TabIndex = 18;
            ComboBoxForLoadedFile.SelectedIndexChanged += ComboBoxForLoadedFile_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(368, 22);
            label1.Name = "label1";
            label1.Size = new Size(90, 21);
            label1.TabIndex = 19;
            label1.Text = "已加载表格";
            // 
            // DataViewForKeyConfig
            // 
            DataViewForKeyConfig.BackgroundColor = SystemColors.ControlLight;
            DataViewForKeyConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewForKeyConfig.Columns.AddRange(new DataGridViewColumn[] { Key, KeyName, RelatInfo, EditRelateBtnColum, IsDataSource });
            DataViewForKeyConfig.Location = new Point(12, 90);
            DataViewForKeyConfig.Name = "DataViewForKeyConfig";
            DataViewForKeyConfig.RowTemplate.Height = 25;
            DataViewForKeyConfig.Size = new Size(996, 279);
            DataViewForKeyConfig.TabIndex = 20;
            DataViewForKeyConfig.CellContentClick += DataViewForKeyConfig_CellContentClick;
            DataViewForKeyConfig.CellValueChanged += DataViewForKeyConfig_CellValueChanged;
            // 
            // Key
            // 
            Key.HeaderText = "Index";
            Key.Name = "Key";
            Key.Width = 60;
            // 
            // KeyName
            // 
            dataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle21;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "Key名字";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            KeyName.Width = 200;
            // 
            // RelatInfo
            // 
            RelatInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RelatInfo.DefaultCellStyle = dataGridViewCellStyle22;
            RelatInfo.FillWeight = 160F;
            RelatInfo.HeaderText = "关联信息";
            RelatInfo.Name = "RelatInfo";
            RelatInfo.ReadOnly = true;
            // 
            // EditRelateBtnColum
            // 
            EditRelateBtnColum.FillWeight = 160F;
            EditRelateBtnColum.HeaderText = "设置关联";
            EditRelateBtnColum.Name = "EditRelateBtnColum";
            EditRelateBtnColum.Text = "编辑";
            EditRelateBtnColum.ToolTipText = "编辑";
            // 
            // IsDataSource
            // 
            IsDataSource.HeaderText = "选为数据源";
            IsDataSource.Name = "IsDataSource";
            // 
            // LabelForFromTable
            // 
            LabelForFromTable.AutoSize = true;
            LabelForFromTable.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelForFromTable.Location = new Point(13, 22);
            LabelForFromTable.Name = "LabelForFromTable";
            LabelForFromTable.Size = new Size(90, 21);
            LabelForFromTable.TabIndex = 21;
            LabelForFromTable.Text = "目标文件：";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(879, 774);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 23;
            BtnFinishConfig.Text = "设置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(638, 22);
            label3.Name = "label3";
            label3.Size = new Size(85, 21);
            label3.TabIndex = 25;
            label3.Text = "选择Sheet";
            // 
            // ComboBoxForWorkSheet
            // 
            ComboBoxForWorkSheet.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForWorkSheet.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForWorkSheet.FormattingEnabled = true;
            ComboBoxForWorkSheet.Location = new Point(730, 19);
            ComboBoxForWorkSheet.Name = "ComboBoxForWorkSheet";
            ComboBoxForWorkSheet.Size = new Size(130, 29);
            ComboBoxForWorkSheet.TabIndex = 24;
            ComboBoxForWorkSheet.SelectedIndexChanged += ComboBoxForWorkSheet_SelectedIndexChanged;
            // 
            // BtnReset
            // 
            BtnReset.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReset.Location = new Point(491, 56);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(79, 30);
            BtnReset.TabIndex = 43;
            BtnReset.Text = "重置";
            BtnReset.UseVisualStyleBackColor = true;
            // 
            // BtnSearch
            // 
            BtnSearch.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSearch.Location = new Point(405, 56);
            BtnSearch.Name = "BtnSearch";
            BtnSearch.Size = new Size(78, 30);
            BtnSearch.TabIndex = 42;
            BtnSearch.Text = "查找";
            BtnSearch.UseVisualStyleBackColor = true;
            // 
            // TextBoxForSearch
            // 
            TextBoxForSearch.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxForSearch.Location = new Point(102, 58);
            TextBoxForSearch.Name = "TextBoxForSearch";
            TextBoxForSearch.Size = new Size(292, 26);
            TextBoxForSearch.TabIndex = 41;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(13, 60);
            label5.Name = "label5";
            label5.Size = new Size(69, 21);
            label5.TabIndex = 40;
            label5.Text = "Key筛选";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 468);
            label2.Name = "label2";
            label2.Size = new Size(90, 21);
            label2.TabIndex = 44;
            label2.Text = "已配置行为";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewButtonColumn1, dataGridViewCheckBoxColumn1 });
            dataGridView1.Location = new Point(14, 492);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(995, 263);
            dataGridView1.TabIndex = 45;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Index";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle23;
            dataGridViewTextBoxColumn2.FillWeight = 160F;
            dataGridViewTextBoxColumn2.HeaderText = "Key名字";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle24;
            dataGridViewTextBoxColumn3.FillWeight = 160F;
            dataGridViewTextBoxColumn3.HeaderText = "关联信息";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewButtonColumn1.FillWeight = 160F;
            dataGridViewButtonColumn1.HeaderText = "设置关联";
            dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            dataGridViewButtonColumn1.Text = "编辑";
            dataGridViewButtonColumn1.ToolTipText = "编辑";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCheckBoxColumn1.HeaderText = "选为数据源";
            dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(878, 403);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 46;
            button1.Text = "加载新表格";
            button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 403);
            label4.Name = "label4";
            label4.Size = new Size(170, 21);
            label4.TabIndex = 48;
            label4.Text = "已选数据列，注意顺序";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(188, 400);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(130, 29);
            comboBox1.TabIndex = 47;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(377, 403);
            label6.Name = "label6";
            label6.Size = new Size(42, 21);
            label6.TabIndex = 50;
            label6.Text = "行为";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(420, 400);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(130, 29);
            comboBox2.TabIndex = 49;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // KeyConnectEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 816);
            Controls.Add(label6);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(BtnReset);
            Controls.Add(BtnSearch);
            Controls.Add(TextBoxForSearch);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(ComboBoxForWorkSheet);
            Controls.Add(BtnFinishConfig);
            Controls.Add(LabelForFromTable);
            Controls.Add(DataViewForKeyConfig);
            Controls.Add(label1);
            Controls.Add(ComboBoxForLoadedFile);
            Controls.Add(BtnLoadNewFile);
            Name = "KeyConnectEditForm";
            Text = "KeyConnectEditForm";
            Load += KeyConnectEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyConfig).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BtnLoadNewFile;
        private ComboBox ComboBoxForLoadedFile;
        private Label label1;
        private DataGridView DataViewForKeyConfig;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewTextBoxColumn RelatInfo;
        private DataGridViewButtonColumn EditRelateBtnColum;
        private DataGridViewCheckBoxColumn IsDataSource;
        private Label LabelForFromTable;
        private Button BtnFinishConfig;
        private Label label3;
        private ComboBox ComboBoxForWorkSheet;
        private Button BtnReset;
        private Button BtnSearch;
        private TextBox TextBoxForSearch;
        private Label label5;
        private Label label2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewButtonColumn dataGridViewButtonColumn1;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private Button button1;
        private Label label4;
        private ComboBox comboBox1;
        private Label label6;
        private ComboBox comboBox2;
    }
}