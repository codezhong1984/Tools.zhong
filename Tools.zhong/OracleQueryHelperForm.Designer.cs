
namespace Tools.zhong
{
    partial class OracleQueryHelperForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnParam = new System.Windows.Forms.Button();
            this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnOk.Location = new System.Drawing.Point(1011, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "执行（F5)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "执行结果：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 31;
            this.label3.Text = "SQL：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamName,
            this.ParamValue,
            this.ParamType});
            this.dataGridView1.Location = new System.Drawing.Point(0, 1);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1137, 108);
            this.dataGridView1.TabIndex = 32;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToDeleteRows = false;
            this.dataGridViewResult.AllowUserToOrderColumns = true;
            this.dataGridViewResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResult.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewResult.CausesValidation = false;
            this.dataGridViewResult.ColumnHeadersHeight = 29;
            this.dataGridViewResult.Location = new System.Drawing.Point(6, 40);
            this.dataGridViewResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.ReadOnly = true;
            this.dataGridViewResult.RowHeadersWidth = 100;
            this.dataGridViewResult.RowTemplate.Height = 27;
            this.dataGridViewResult.Size = new System.Drawing.Size(1120, 229);
            this.dataGridViewResult.TabIndex = 33;
            // 
            // txtSQL
            // 
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL.Size = new System.Drawing.Size(1137, 260);
            this.txtSQL.TabIndex = 34;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 132);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSQL);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnParam);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewResult);
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Size = new System.Drawing.Size(1137, 537);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 35;
            // 
            // btnParam
            // 
            this.btnParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParam.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnParam.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnParam.Location = new System.Drawing.Point(883, 7);
            this.btnParam.Name = "btnParam";
            this.btnParam.Size = new System.Drawing.Size(106, 30);
            this.btnParam.TabIndex = 34;
            this.btnParam.Text = "参数化语句";
            this.btnParam.UseVisualStyleBackColor = true;
            this.btnParam.Click += new System.EventHandler(this.btnParam_Click);
            // 
            // ParamName
            // 
            this.ParamName.HeaderText = "参数名";
            this.ParamName.MinimumWidth = 6;
            this.ParamName.Name = "ParamName";
            // 
            // ParamValue
            // 
            this.ParamValue.HeaderText = "参数值";
            this.ParamValue.MinimumWidth = 6;
            this.ParamValue.Name = "ParamValue";
            // 
            // ParamType
            // 
            this.ParamType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ParamType.HeaderText = "参数类型";
            this.ParamType.Items.AddRange(new object[] {
            "DateTime",
            "Varchar",
            "Varchar2",
            "Number"});
            this.ParamType.MinimumWidth = 6;
            this.ParamType.Name = "ParamType";
            // 
            // OracleQueryHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 673);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "OracleQueryHelperForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oracel参数化查询辅助";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OracleQueryHelperForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValue;
        private System.Windows.Forms.DataGridViewComboBoxColumn ParamType;
    }
}