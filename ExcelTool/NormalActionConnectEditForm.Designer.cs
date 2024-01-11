namespace ExcelTool
{
    partial class NormalActionConnectEditForm
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
            DataViewForAction = new DataGridView();
            ActionName = new DataGridViewTextBoxColumn();
            dataGridViewButtonColumn1 = new DataGridViewButtonColumn();
            ConnectInfoColum = new DataGridViewTextBoxColumn();
            RemoveBtnColum = new DataGridViewButtonColumn();
            MoveUpColum = new DataGridViewButtonColumn();
            MoveDownColum = new DataGridViewButtonColumn();
            FollowActionColum = new DataGridViewButtonColumn();
            label2 = new Label();
            BtnFinishConfig = new Button();
            label6 = new Label();
            ComboBoxForActionTypeList = new ComboBox();
            BtnAddAction = new Button();
            label1 = new Label();
            ComboBoxForReturnType = new ComboBox();
            MultiDataSplitSymbol = new TextBox();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)DataViewForAction).BeginInit();
            SuspendLayout();
            // 
            // DataViewForAction
            // 
            DataViewForAction.BackgroundColor = SystemColors.ControlLight;
            DataViewForAction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataViewForAction.Columns.AddRange(new DataGridViewColumn[] { ActionName, dataGridViewButtonColumn1, ConnectInfoColum, RemoveBtnColum, MoveUpColum, MoveDownColum, FollowActionColum });
            DataViewForAction.Location = new Point(14, 92);
            DataViewForAction.Name = "DataViewForAction";
            DataViewForAction.RowTemplate.Height = 25;
            DataViewForAction.Size = new Size(995, 263);
            DataViewForAction.TabIndex = 48;
            DataViewForAction.CellContentClick += DataViewForAction_CellContentClick;
            // 
            // ActionName
            // 
            ActionName.HeaderText = "行为名字";
            ActionName.Name = "ActionName";
            ActionName.ReadOnly = true;
            ActionName.Width = 120;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewButtonColumn1.FillWeight = 160F;
            dataGridViewButtonColumn1.HeaderText = "详细配置";
            dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            dataGridViewButtonColumn1.Text = "详细配置";
            dataGridViewButtonColumn1.ToolTipText = "详细配置";
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
            RemoveBtnColum.HeaderText = "移除";
            RemoveBtnColum.Name = "RemoveBtnColum";
            RemoveBtnColum.Width = 50;
            // 
            // MoveUpColum
            // 
            MoveUpColum.HeaderText = "上移";
            MoveUpColum.Name = "MoveUpColum";
            MoveUpColum.Text = "上移";
            MoveUpColum.ToolTipText = "上移";
            MoveUpColum.Width = 40;
            // 
            // MoveDownColum
            // 
            MoveDownColum.HeaderText = "下移";
            MoveDownColum.Name = "MoveDownColum";
            MoveDownColum.Text = "下移";
            MoveDownColum.ToolTipText = "下移";
            MoveDownColum.Width = 40;
            // 
            // FollowActionColum
            // 
            FollowActionColum.HeaderText = "后续配置";
            FollowActionColum.Name = "FollowActionColum";
            FollowActionColum.Width = 80;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(14, 59);
            label2.Name = "label2";
            label2.Size = new Size(346, 21);
            label2.TabIndex = 47;
            label2.Text = "已配置行为序列，结果相互独立，最后统一返回";
            // 
            // BtnFinishConfig
            // 
            BtnFinishConfig.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFinishConfig.Location = new Point(879, 374);
            BtnFinishConfig.Name = "BtnFinishConfig";
            BtnFinishConfig.Size = new Size(130, 30);
            BtnFinishConfig.TabIndex = 46;
            BtnFinishConfig.Text = "设置完成";
            BtnFinishConfig.UseVisualStyleBackColor = true;
            BtnFinishConfig.Click += BtnFinishConfig_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(16, 21);
            label6.Name = "label6";
            label6.Size = new Size(42, 21);
            label6.TabIndex = 53;
            label6.Text = "行为";
            // 
            // ComboBoxForActionTypeList
            // 
            ComboBoxForActionTypeList.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForActionTypeList.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForActionTypeList.FormattingEnabled = true;
            ComboBoxForActionTypeList.Location = new Point(59, 18);
            ComboBoxForActionTypeList.Name = "ComboBoxForActionTypeList";
            ComboBoxForActionTypeList.Size = new Size(241, 29);
            ComboBoxForActionTypeList.TabIndex = 52;
            ComboBoxForActionTypeList.SelectedIndexChanged += ComboBoxForActionTypeList_SelectedIndexChanged;
            // 
            // BtnAddAction
            // 
            BtnAddAction.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAddAction.Location = new Point(765, 15);
            BtnAddAction.Name = "BtnAddAction";
            BtnAddAction.Size = new Size(130, 30);
            BtnAddAction.TabIndex = 51;
            BtnAddAction.Text = "添加行为";
            BtnAddAction.UseVisualStyleBackColor = true;
            BtnAddAction.Click += BtnAddAction_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(333, 21);
            label1.Name = "label1";
            label1.Size = new Size(90, 21);
            label1.TabIndex = 57;
            label1.Text = "多数据返回";
            // 
            // ComboBoxForReturnType
            // 
            ComboBoxForReturnType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForReturnType.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxForReturnType.FormattingEnabled = true;
            ComboBoxForReturnType.Location = new Point(426, 18);
            ComboBoxForReturnType.Name = "ComboBoxForReturnType";
            ComboBoxForReturnType.Size = new Size(110, 29);
            ComboBoxForReturnType.TabIndex = 56;
            ComboBoxForReturnType.SelectedIndexChanged += ComboBoxForReturnType_SelectedIndexChanged;
            // 
            // MultiDataSplitSymbol
            // 
            MultiDataSplitSymbol.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MultiDataSplitSymbol.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            MultiDataSplitSymbol.Location = new Point(677, 18);
            MultiDataSplitSymbol.Name = "MultiDataSplitSymbol";
            MultiDataSplitSymbol.Size = new Size(53, 26);
            MultiDataSplitSymbol.TabIndex = 59;
            MultiDataSplitSymbol.Text = ",";
            MultiDataSplitSymbol.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(560, 21);
            label7.Name = "label7";
            label7.Size = new Size(106, 21);
            label7.TabIndex = 58;
            label7.Text = "多数据分隔符";
            // 
            // NormalActionConnectEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1019, 413);
            Controls.Add(MultiDataSplitSymbol);
            Controls.Add(label7);
            Controls.Add(label1);
            Controls.Add(ComboBoxForReturnType);
            Controls.Add(label6);
            Controls.Add(ComboBoxForActionTypeList);
            Controls.Add(BtnAddAction);
            Controls.Add(DataViewForAction);
            Controls.Add(label2);
            Controls.Add(BtnFinishConfig);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "NormalActionConnectEditForm";
            Text = "NormalActionConnectEditForm";
            Load += NormalActionConnectEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataViewForAction).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DataViewForAction;
        private Label label2;
        private Button BtnFinishConfig;
        private Label label6;
        private ComboBox ComboBoxForActionTypeList;
        private Button BtnAddAction;
        private Label label1;
        private ComboBox ComboBoxForReturnType;
        private DataGridViewTextBoxColumn ActionName;
        private DataGridViewButtonColumn dataGridViewButtonColumn1;
        private DataGridViewTextBoxColumn ConnectInfoColum;
        private DataGridViewButtonColumn RemoveBtnColum;
        private DataGridViewButtonColumn MoveUpColum;
        private DataGridViewButtonColumn MoveDownColum;
        private DataGridViewButtonColumn FollowActionColum;
        private TextBox MultiDataSplitSymbol;
        private Label label7;
    }
}