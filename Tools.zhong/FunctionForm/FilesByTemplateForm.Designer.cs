
namespace Tools.zhong
{
    partial class FilesByTemplateForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTemplDir = new System.Windows.Forms.TextBox();
            this.btnTempDir = new System.Windows.Forms.Button();
            this.btnOutDir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFuncName = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnOk.Location = new System.Drawing.Point(429, 517);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 33);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(353, 517);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "退 出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "表名（如:C_Owner_T）：";
            // 
            // txtTableName
            // 
            this.txtTableName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTableName.Location = new System.Drawing.Point(53, 28);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTableName.MaxLength = 8;
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(429, 25);
            this.txtTableName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "类名表名（如:TnOwner）：";
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClassName.Location = new System.Drawing.Point(53, 82);
            this.txtClassName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtClassName.MaxLength = 8;
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(429, 25);
            this.txtClassName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "模板目录：";
            // 
            // txtTemplDir
            // 
            this.txtTemplDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTemplDir.Location = new System.Drawing.Point(53, 191);
            this.txtTemplDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTemplDir.MaxLength = 8;
            this.txtTemplDir.Name = "txtTemplDir";
            this.txtTemplDir.Size = new System.Drawing.Size(370, 25);
            this.txtTemplDir.TabIndex = 10;
            // 
            // btnTempDir
            // 
            this.btnTempDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTempDir.Location = new System.Drawing.Point(429, 184);
            this.btnTempDir.Name = "btnTempDir";
            this.btnTempDir.Size = new System.Drawing.Size(53, 33);
            this.btnTempDir.TabIndex = 12;
            this.btnTempDir.Text = "...";
            this.btnTempDir.UseVisualStyleBackColor = true;
            this.btnTempDir.Click += new System.EventHandler(this.btnTempDir_Click);
            // 
            // btnOutDir
            // 
            this.btnOutDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOutDir.Location = new System.Drawing.Point(429, 239);
            this.btnOutDir.Name = "btnOutDir";
            this.btnOutDir.Size = new System.Drawing.Size(53, 33);
            this.btnOutDir.TabIndex = 15;
            this.btnOutDir.Text = "...";
            this.btnOutDir.UseVisualStyleBackColor = true;
            this.btnOutDir.Click += new System.EventHandler(this.btnOutDir_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "输出目录：";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtOutputDir.Location = new System.Drawing.Point(53, 245);
            this.txtOutputDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutputDir.MaxLength = 8;
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(370, 25);
            this.txtOutputDir.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "功能描述：";
            // 
            // txtFuncName
            // 
            this.txtFuncName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFuncName.Location = new System.Drawing.Point(53, 138);
            this.txtFuncName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFuncName.MaxLength = 8;
            this.txtFuncName.Name = "txtFuncName";
            this.txtFuncName.Size = new System.Drawing.Size(429, 25);
            this.txtFuncName.TabIndex = 16;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "E:\\myProjects\\Tools.zhong(codeTempl)\\ProjTempl";
            // 
            // folderBrowserDialog2
            // 
            this.folderBrowserDialog2.SelectedPath = "E:\\myProjects\\Tools.zhong(codeTempl)\\ProjOutput";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(53, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(429, 189);
            this.label5.TabIndex = 18;
            this.label5.Text = "指令列表：%T% ---- 表名\r\n%C% ---- 类名\r\n%LP%--%ELP% 循环 $0--Field0 $Field1....\r\n$0 --- fiel" +
    "ds\r\n%FDES% ----- 功能描述名指令\r\n%SLP% ---- 循环分隔符指令\r\n%ESLP% ---- 循环分隔符结束指令\r\n%SECTION% -" +
    "--- 代码段\r\n%END_SECTION% ---- 代码段结束";
            // 
            // FilesByTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 567);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFuncName);
            this.Controls.Add(this.btnOutDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOutputDir);
            this.Controls.Add(this.btnTempDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTemplDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FilesByTemplateForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码模板参数设置";
            this.Load += new System.EventHandler(this.FilesByTemplateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTemplDir;
        private System.Windows.Forms.Button btnTempDir;
        private System.Windows.Forms.Button btnOutDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFuncName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label label5;
    }
}