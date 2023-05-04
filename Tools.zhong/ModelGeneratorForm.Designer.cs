
namespace Tools.zhong
{
    partial class ModelGeneratorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNullable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbNameSpace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPreCreate = new System.Windows.Forms.Button();
            this.cbSplitChar = new System.Windows.Forms.ComboBox();
            this.cbFullProp = new System.Windows.Forms.CheckBox();
            this.cbCreateTbName = new System.Windows.Forms.CheckBox();
            this.cbDisplayName = new System.Windows.Forms.CheckBox();
            this.cbLineDeal = new System.Windows.Forms.CheckBox();
            this.cbIfTrim = new System.Windows.Forms.CheckBox();
            this.cbFieldType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRequired = new System.Windows.Forms.CheckBox();
            this.btnDelColumn = new System.Windows.Forms.Button();
            this.cbCol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRequired = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入字段属性：";
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Location = new System.Drawing.Point(12, 31);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(1072, 104);
            this.txtCode.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnOk.Location = new System.Drawing.Point(983, 566);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 33);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(877, 567);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "退 出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(606, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "字段分隔符：";
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClassName.Location = new System.Drawing.Point(267, 143);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(126, 25);
            this.txtClassName.TabIndex = 25;
            this.txtClassName.Text = "Model";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "类名:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TableName,
            this.DataLength,
            this.TableComment,
            this.FieldName,
            this.FieldRemarks,
            this.DataType,
            this.IsNullable});
            this.dataGridView1.Location = new System.Drawing.Point(17, 182);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1067, 368);
            this.dataGridView1.TabIndex = 26;
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "TableName";
            this.TableName.HeaderText = "对象名称";
            this.TableName.MinimumWidth = 6;
            this.TableName.Name = "TableName";
            this.TableName.Visible = false;
            // 
            // DataLength
            // 
            this.DataLength.DataPropertyName = "DataLength";
            this.DataLength.HeaderText = "数据长度";
            this.DataLength.MinimumWidth = 6;
            this.DataLength.Name = "DataLength";
            this.DataLength.Visible = false;
            // 
            // TableComment
            // 
            this.TableComment.DataPropertyName = "TableComment";
            this.TableComment.HeaderText = "对象描述";
            this.TableComment.MinimumWidth = 6;
            this.TableComment.Name = "TableComment";
            this.TableComment.Visible = false;
            // 
            // FieldName
            // 
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "字段名称";
            this.FieldName.MinimumWidth = 6;
            this.FieldName.Name = "FieldName";
            // 
            // FieldRemarks
            // 
            this.FieldRemarks.DataPropertyName = "FieldRemarks";
            this.FieldRemarks.HeaderText = "字段描述";
            this.FieldRemarks.MinimumWidth = 6;
            this.FieldRemarks.Name = "FieldRemarks";
            // 
            // DataType
            // 
            this.DataType.DataPropertyName = "DataType";
            this.DataType.HeaderText = "字段类型";
            this.DataType.MinimumWidth = 6;
            this.DataType.Name = "DataType";
            // 
            // IsNullable
            // 
            this.IsNullable.DataPropertyName = "IsNullable";
            this.IsNullable.HeaderText = "可否为空";
            this.IsNullable.MinimumWidth = 6;
            this.IsNullable.Name = "IsNullable";
            // 
            // tbNameSpace
            // 
            this.tbNameSpace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbNameSpace.Location = new System.Drawing.Point(103, 143);
            this.tbNameSpace.Name = "tbNameSpace";
            this.tbNameSpace.Size = new System.Drawing.Size(109, 25);
            this.tbNameSpace.TabIndex = 34;
            this.tbNameSpace.Text = "DBModel";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 33;
            this.label3.Text = "命名空间：";
            // 
            // btnPreCreate
            // 
            this.btnPreCreate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPreCreate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreCreate.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPreCreate.Location = new System.Drawing.Point(983, 143);
            this.btnPreCreate.Name = "btnPreCreate";
            this.btnPreCreate.Size = new System.Drawing.Size(101, 27);
            this.btnPreCreate.TabIndex = 35;
            this.btnPreCreate.Text = "导入字段";
            this.btnPreCreate.UseVisualStyleBackColor = true;
            this.btnPreCreate.Click += new System.EventHandler(this.btnPreCreate_Click);
            // 
            // cbSplitChar
            // 
            this.cbSplitChar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbSplitChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSplitChar.FormattingEnabled = true;
            this.cbSplitChar.Items.AddRange(new object[] {
            "Tab",
            "空格",
            "逗号",
            "回车换行"});
            this.cbSplitChar.Location = new System.Drawing.Point(708, 145);
            this.cbSplitChar.Name = "cbSplitChar";
            this.cbSplitChar.Size = new System.Drawing.Size(94, 23);
            this.cbSplitChar.TabIndex = 38;
            this.cbSplitChar.SelectedIndexChanged += new System.EventHandler(this.cbSplitChar_SelectedIndexChanged);
            // 
            // cbFullProp
            // 
            this.cbFullProp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFullProp.AutoSize = true;
            this.cbFullProp.Location = new System.Drawing.Point(208, 557);
            this.cbFullProp.Name = "cbFullProp";
            this.cbFullProp.Size = new System.Drawing.Size(205, 19);
            this.cbFullProp.TabIndex = 39;
            this.cbFullProp.Text = "是否生成完整GET|SET方法";
            this.cbFullProp.UseVisualStyleBackColor = true;
            // 
            // cbCreateTbName
            // 
            this.cbCreateTbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCreateTbName.AutoSize = true;
            this.cbCreateTbName.Location = new System.Drawing.Point(208, 589);
            this.cbCreateTbName.Name = "cbCreateTbName";
            this.cbCreateTbName.Size = new System.Drawing.Size(383, 19);
            this.cbCreateTbName.TabIndex = 42;
            this.cbCreateTbName.Text = "是否映射DB表类名，代码中包含：[Table(\"TANEM\")]";
            this.cbCreateTbName.UseVisualStyleBackColor = true;
            // 
            // cbDisplayName
            // 
            this.cbDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDisplayName.AutoSize = true;
            this.cbDisplayName.Location = new System.Drawing.Point(19, 589);
            this.cbDisplayName.Name = "cbDisplayName";
            this.cbDisplayName.Size = new System.Drawing.Size(177, 19);
            this.cbDisplayName.TabIndex = 41;
            this.cbDisplayName.Text = "是否生成DisplayName";
            this.cbDisplayName.UseVisualStyleBackColor = true;
            // 
            // cbLineDeal
            // 
            this.cbLineDeal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLineDeal.AutoSize = true;
            this.cbLineDeal.Location = new System.Drawing.Point(19, 558);
            this.cbLineDeal.Name = "cbLineDeal";
            this.cbLineDeal.Size = new System.Drawing.Size(134, 19);
            this.cbLineDeal.TabIndex = 40;
            this.cbLineDeal.Text = "是否处理下划线";
            this.cbLineDeal.UseVisualStyleBackColor = true;
            // 
            // cbIfTrim
            // 
            this.cbIfTrim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbIfTrim.AutoSize = true;
            this.cbIfTrim.Checked = true;
            this.cbIfTrim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIfTrim.Location = new System.Drawing.Point(446, 558);
            this.cbIfTrim.Name = "cbIfTrim";
            this.cbIfTrim.Size = new System.Drawing.Size(173, 19);
            this.cbIfTrim.TabIndex = 43;
            this.cbIfTrim.Text = "是否对SET方法去空格";
            this.cbIfTrim.UseVisualStyleBackColor = true;
            // 
            // cbFieldType
            // 
            this.cbFieldType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFieldType.FormattingEnabled = true;
            this.cbFieldType.Location = new System.Drawing.Point(741, 584);
            this.cbFieldType.Name = "cbFieldType";
            this.cbFieldType.Size = new System.Drawing.Size(94, 23);
            this.cbFieldType.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(627, 588);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 44;
            this.label5.Text = "默认字段类型：";
            // 
            // cbRequired
            // 
            this.cbRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRequired.AutoSize = true;
            this.cbRequired.Location = new System.Drawing.Point(630, 558);
            this.cbRequired.Name = "cbRequired";
            this.cbRequired.Size = new System.Drawing.Size(149, 19);
            this.cbRequired.TabIndex = 46;
            this.cbRequired.Text = "是否生成必填属性";
            this.cbRequired.UseVisualStyleBackColor = true;
            // 
            // btnDelColumn
            // 
            this.btnDelColumn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelColumn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelColumn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnDelColumn.Location = new System.Drawing.Point(917, 143);
            this.btnDelColumn.Name = "btnDelColumn";
            this.btnDelColumn.Size = new System.Drawing.Size(61, 27);
            this.btnDelColumn.TabIndex = 47;
            this.btnDelColumn.Text = "重置";
            this.btnDelColumn.UseVisualStyleBackColor = true;
            this.btnDelColumn.Click += new System.EventHandler(this.btnDelColumn_Click);
            // 
            // cbCol
            // 
            this.cbCol.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCol.FormattingEnabled = true;
            this.cbCol.Items.AddRange(new object[] {
            "FieldName",
            "FieldRemarks",
            "DataType",
            "IsNullable"});
            this.cbCol.Location = new System.Drawing.Point(469, 145);
            this.cbCol.Name = "cbCol";
            this.cbCol.Size = new System.Drawing.Size(132, 23);
            this.cbCol.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(397, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "导入列：";
            // 
            // btnRequired
            // 
            this.btnRequired.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRequired.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRequired.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnRequired.Location = new System.Drawing.Point(808, 143);
            this.btnRequired.Name = "btnRequired";
            this.btnRequired.Size = new System.Drawing.Size(104, 27);
            this.btnRequired.TabIndex = 50;
            this.btnRequired.Text = "全部非必填";
            this.btnRequired.UseVisualStyleBackColor = true;
            this.btnRequired.Click += new System.EventHandler(this.btnRequired_Click);
            // 
            // ModelGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 621);
            this.Controls.Add(this.btnRequired);
            this.Controls.Add(this.cbCol);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDelColumn);
            this.Controls.Add(this.cbRequired);
            this.Controls.Add(this.cbFieldType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbIfTrim);
            this.Controls.Add(this.cbCreateTbName);
            this.Controls.Add(this.cbDisplayName);
            this.Controls.Add(this.cbLineDeal);
            this.Controls.Add(this.cbFullProp);
            this.Controls.Add(this.cbSplitChar);
            this.Controls.Add(this.btnPreCreate);
            this.Controls.Add(this.tbNameSpace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label1);
            this.Name = "ModelGeneratorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "根据输入字符串自动生成类对象";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNameSpace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPreCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldRemarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsNullable;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbSplitChar;
        private System.Windows.Forms.CheckBox cbFullProp;
        private System.Windows.Forms.CheckBox cbCreateTbName;
        private System.Windows.Forms.CheckBox cbDisplayName;
        private System.Windows.Forms.CheckBox cbLineDeal;
        private System.Windows.Forms.CheckBox cbIfTrim;
        private System.Windows.Forms.ComboBox cbFieldType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbRequired;
        private System.Windows.Forms.Button btnDelColumn;
        private System.Windows.Forms.ComboBox cbCol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRequired;
    }
}