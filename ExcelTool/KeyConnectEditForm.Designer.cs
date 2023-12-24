﻿namespace ExcelTool
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            BtnLoadNewFile = new Button();
            ComboBoxForLoadedFile = new ComboBox();
            label1 = new Label();
            DataViewForKeyList = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            KeyName = new DataGridViewTextBoxColumn();
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
            DataViewForAction = new DataGridView();
            BtnAddAction = new Button();
            label4 = new Label();
            ComboBoxForSelectKeyList = new ComboBox();
            label6 = new Label();
            ComboBoxForActionTypeList = new ComboBox();
            ActionName = new DataGridViewTextBoxColumn();
            BindKeyList = new DataGridViewComboBoxColumn();
            dataGridViewButtonColumn1 = new DataGridViewButtonColumn();
            ConnectKeyActionsColum = new DataGridViewButtonColumn();
            ConnectInfoColum = new DataGridViewTextBoxColumn();
            RemoveBtnColum = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataViewForAction).BeginInit();
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
            // DataViewForKeyList
            // 
            DataViewForKeyList.BackgroundColor = SystemColors.ControlLight;
            DataViewForKeyList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewForKeyList.Columns.AddRange(new DataGridViewColumn[] { Key, KeyName, IsDataSource });
            DataViewForKeyList.Location = new Point(12, 90);
            DataViewForKeyList.Name = "DataViewForKeyList";
            DataViewForKeyList.RowTemplate.Height = 25;
            DataViewForKeyList.Size = new Size(996, 279);
            DataViewForKeyList.TabIndex = 20;
            DataViewForKeyList.CellContentClick += DataViewForKeyConfig_CellContentClick;
            // 
            // Key
            // 
            Key.HeaderText = "Index";
            Key.Name = "Key";
            Key.Width = 60;
            // 
            // KeyName
            // 
            KeyName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            KeyName.DefaultCellStyle = dataGridViewCellStyle5;
            KeyName.FillWeight = 160F;
            KeyName.HeaderText = "Key名字";
            KeyName.Name = "KeyName";
            KeyName.ReadOnly = true;
            // 
            // IsDataSource
            // 
            IsDataSource.HeaderText = "选择数据列";
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
            BtnReset.Click += BtnReset_Click;
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
            BtnSearch.Click += BtnSearch_Click;
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
            // DataViewForAction
            // 
            DataViewForAction.BackgroundColor = SystemColors.ControlLight;
            DataViewForAction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewForAction.Columns.AddRange(new DataGridViewColumn[] { ActionName, BindKeyList, dataGridViewButtonColumn1, ConnectKeyActionsColum, ConnectInfoColum, RemoveBtnColum });
            DataViewForAction.Location = new Point(14, 492);
            DataViewForAction.Name = "DataViewForAction";
            DataViewForAction.RowTemplate.Height = 25;
            DataViewForAction.Size = new Size(995, 263);
            DataViewForAction.TabIndex = 45;
            DataViewForAction.CellContentClick += DataViewForAction_CellContentClick;
            // 
            // BtnAddAction
            // 
            BtnAddAction.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAddAction.Location = new Point(878, 403);
            BtnAddAction.Name = "BtnAddAction";
            BtnAddAction.Size = new Size(130, 30);
            BtnAddAction.TabIndex = 46;
            BtnAddAction.Text = "添加行为";
            BtnAddAction.UseVisualStyleBackColor = true;
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
            // ComboBoxForSelectKeyList
            // 
            ComboBoxForSelectKeyList.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForSelectKeyList.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForSelectKeyList.FormattingEnabled = true;
            ComboBoxForSelectKeyList.Location = new Point(188, 400);
            ComboBoxForSelectKeyList.Name = "ComboBoxForSelectKeyList";
            ComboBoxForSelectKeyList.Size = new Size(130, 29);
            ComboBoxForSelectKeyList.TabIndex = 47;
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
            // ComboBoxForActionTypeList
            // 
            ComboBoxForActionTypeList.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForActionTypeList.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForActionTypeList.FormattingEnabled = true;
            ComboBoxForActionTypeList.Location = new Point(420, 400);
            ComboBoxForActionTypeList.Name = "ComboBoxForActionTypeList";
            ComboBoxForActionTypeList.Size = new Size(130, 29);
            ComboBoxForActionTypeList.TabIndex = 49;
            // 
            // ActionName
            // 
            ActionName.HeaderText = "行为名字";
            ActionName.Name = "ActionName";
            ActionName.ReadOnly = true;
            ActionName.Width = 120;
            // 
            // BindKeyList
            // 
            BindKeyList.HeaderText = "绑定Key";
            BindKeyList.Name = "BindKeyList";
            BindKeyList.ReadOnly = true;
            BindKeyList.Width = 180;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewButtonColumn1.FillWeight = 160F;
            dataGridViewButtonColumn1.HeaderText = "关联其他表Key";
            dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            dataGridViewButtonColumn1.Text = "关联其他表Key";
            dataGridViewButtonColumn1.ToolTipText = "关联其他表Key";
            // 
            // ConnectKeyActionsColum
            // 
            ConnectKeyActionsColum.HeaderText = "关联其他表行为";
            ConnectKeyActionsColum.Name = "ConnectKeyActionsColum";
            ConnectKeyActionsColum.Text = "关联其他表行为";
            // 
            // ConnectInfoColum
            // 
            ConnectInfoColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ConnectInfoColum.HeaderText = "关联信息";
            ConnectInfoColum.Name = "ConnectInfoColum";
            ConnectInfoColum.ReadOnly = true;
            // 
            // RemoveBtnColum
            // 
            RemoveBtnColum.HeaderText = "移除按钮";
            RemoveBtnColum.Name = "RemoveBtnColum";
            // 
            // KeyConnectEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 816);
            Controls.Add(label6);
            Controls.Add(ComboBoxForActionTypeList);
            Controls.Add(label4);
            Controls.Add(ComboBoxForSelectKeyList);
            Controls.Add(BtnAddAction);
            Controls.Add(DataViewForAction);
            Controls.Add(label2);
            Controls.Add(BtnReset);
            Controls.Add(BtnSearch);
            Controls.Add(TextBoxForSearch);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(ComboBoxForWorkSheet);
            Controls.Add(BtnFinishConfig);
            Controls.Add(LabelForFromTable);
            Controls.Add(DataViewForKeyList);
            Controls.Add(label1);
            Controls.Add(ComboBoxForLoadedFile);
            Controls.Add(BtnLoadNewFile);
            Name = "KeyConnectEditForm";
            Text = "KeyConnectEditForm";
            Load += KeyConnectEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataViewForKeyList).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataViewForAction).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BtnLoadNewFile;
        private ComboBox ComboBoxForLoadedFile;
        private Label label1;
        private DataGridView DataViewForKeyList;
        private Label LabelForFromTable;
        private Button BtnFinishConfig;
        private Label label3;
        private ComboBox ComboBoxForWorkSheet;
        private Button BtnReset;
        private Button BtnSearch;
        private TextBox TextBoxForSearch;
        private Label label5;
        private Label label2;
        private DataGridView DataViewForAction;
        private Button BtnAddAction;
        private Label label4;
        private ComboBox ComboBoxForSelectKeyList;
        private Label label6;
        private ComboBox ComboBoxForActionTypeList;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn KeyName;
        private DataGridViewCheckBoxColumn IsDataSource;
        private DataGridViewTextBoxColumn ActionName;
        private DataGridViewComboBoxColumn BindKeyList;
        private DataGridViewButtonColumn dataGridViewButtonColumn1;
        private DataGridViewButtonColumn ConnectKeyActionsColum;
        private DataGridViewTextBoxColumn ConnectInfoColum;
        private DataGridViewButtonColumn RemoveBtnColum;
    }
}