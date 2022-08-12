
namespace Tools.zhong
{
    partial class CreateModelBySplitStringForm
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
            this.txtTableDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSplitChar = new System.Windows.Forms.ComboBox();
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
            this.txtCode.Size = new System.Drawing.Size(1072, 65);
            this.txtCode.TabIndex = 1;
            this.txtCode.Text = "    AVGITEM1  AVGITEM2  AVGITEM3  AVG3  MAXITEM MINITEM";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Location = new System.Drawing.Point(563, 576);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 33);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(429, 576);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(769, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "字段分隔符：";
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClassName.Location = new System.Drawing.Point(322, 115);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(141, 25);
            this.txtClassName.TabIndex = 25;
            this.txtClassName.Text = "Model";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 120);
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
            this.dataGridView1.Location = new System.Drawing.Point(17, 152);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1067, 410);
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
            this.tbNameSpace.Location = new System.Drawing.Point(101, 115);
            this.tbNameSpace.Name = "tbNameSpace";
            this.tbNameSpace.Size = new System.Drawing.Size(170, 25);
            this.tbNameSpace.TabIndex = 34;
            this.tbNameSpace.Text = "DBModel";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 33;
            this.label3.Text = "命名空间：";
            // 
            // btnPreCreate
            // 
            this.btnPreCreate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPreCreate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreCreate.Location = new System.Drawing.Point(966, 113);
            this.btnPreCreate.Name = "btnPreCreate";
            this.btnPreCreate.Size = new System.Drawing.Size(117, 27);
            this.btnPreCreate.TabIndex = 35;
            this.btnPreCreate.Text = "预计字段列表";
            this.btnPreCreate.UseVisualStyleBackColor = true;
            this.btnPreCreate.Click += new System.EventHandler(this.btnPreCreate_Click);
            // 
            // txtTableDescription
            // 
            this.txtTableDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTableDescription.Location = new System.Drawing.Point(514, 113);
            this.txtTableDescription.Name = "txtTableDescription";
            this.txtTableDescription.Size = new System.Drawing.Size(252, 25);
            this.txtTableDescription.TabIndex = 37;
            this.txtTableDescription.Text = "Custom_Model";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(466, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 36;
            this.label4.Text = "描述:";
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
            this.cbSplitChar.Location = new System.Drawing.Point(869, 115);
            this.cbSplitChar.Name = "cbSplitChar";
            this.cbSplitChar.Size = new System.Drawing.Size(94, 23);
            this.cbSplitChar.TabIndex = 38;
            this.cbSplitChar.SelectedIndexChanged += new System.EventHandler(this.cbSplitChar_SelectedIndexChanged);
            // 
            // CreateModelBySplitStringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 621);
            this.Controls.Add(this.cbSplitChar);
            this.Controls.Add(this.txtTableDescription);
            this.Controls.Add(this.label4);
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
            this.Name = "CreateModelBySplitStringForm";
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
        private System.Windows.Forms.TextBox txtTableDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSplitChar;
    }
}