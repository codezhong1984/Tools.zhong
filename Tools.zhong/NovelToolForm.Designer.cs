
namespace Tools.zhong
{
    partial class NovelToolForm
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
            this.btnReadOrginalFile = new System.Windows.Forms.Button();
            this.btnOutDir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.btnTempDir = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTemplDir = new System.Windows.Forms.TextBox();
            this.btnProccess = new System.Windows.Forms.Button();
            this.txtFileContent = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnReadOrginalFile
            // 
            this.btnReadOrginalFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReadOrginalFile.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadOrginalFile.ForeColor = System.Drawing.Color.Blue;
            this.btnReadOrginalFile.Location = new System.Drawing.Point(727, 19);
            this.btnReadOrginalFile.Name = "btnReadOrginalFile";
            this.btnReadOrginalFile.Size = new System.Drawing.Size(106, 33);
            this.btnReadOrginalFile.TabIndex = 6;
            this.btnReadOrginalFile.Text = "读取原文";
            this.btnReadOrginalFile.UseVisualStyleBackColor = true;
            // 
            // btnOutDir
            // 
            this.btnOutDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOutDir.Location = new System.Drawing.Point(668, 73);
            this.btnOutDir.Name = "btnOutDir";
            this.btnOutDir.Size = new System.Drawing.Size(53, 33);
            this.btnOutDir.TabIndex = 21;
            this.btnOutDir.Text = "...";
            this.btnOutDir.UseVisualStyleBackColor = true;
            this.btnOutDir.Click += new System.EventHandler(this.btnOutDir_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "输出目录：";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtOutputDir.Location = new System.Drawing.Point(12, 76);
            this.txtOutputDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutputDir.MaxLength = 8;
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(656, 25);
            this.txtOutputDir.TabIndex = 19;
            this.txtOutputDir.Text = "D:\\小说\\武炼巅峰（上）_out.txt";
            // 
            // btnTempDir
            // 
            this.btnTempDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTempDir.Location = new System.Drawing.Point(668, 18);
            this.btnTempDir.Name = "btnTempDir";
            this.btnTempDir.Size = new System.Drawing.Size(53, 33);
            this.btnTempDir.TabIndex = 18;
            this.btnTempDir.Text = "...";
            this.btnTempDir.UseVisualStyleBackColor = true;
            this.btnTempDir.Click += new System.EventHandler(this.btnTempDir_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "模板目录：";
            // 
            // txtTemplDir
            // 
            this.txtTemplDir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTemplDir.Location = new System.Drawing.Point(12, 22);
            this.txtTemplDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTemplDir.MaxLength = 8;
            this.txtTemplDir.Name = "txtTemplDir";
            this.txtTemplDir.Size = new System.Drawing.Size(656, 25);
            this.txtTemplDir.TabIndex = 16;
            this.txtTemplDir.Text = "D:\\小说\\武炼巅峰（上）.txt";
            // 
            // btnProccess
            // 
            this.btnProccess.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnProccess.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnProccess.ForeColor = System.Drawing.Color.Blue;
            this.btnProccess.Location = new System.Drawing.Point(727, 71);
            this.btnProccess.Name = "btnProccess";
            this.btnProccess.Size = new System.Drawing.Size(106, 33);
            this.btnProccess.TabIndex = 22;
            this.btnProccess.Text = "处理文字";
            this.btnProccess.UseVisualStyleBackColor = true;
            this.btnProccess.Click += new System.EventHandler(this.btnProccess_Click);
            // 
            // txtFileContent
            // 
            this.txtFileContent.Location = new System.Drawing.Point(12, 122);
            this.txtFileContent.Multiline = true;
            this.txtFileContent.Name = "txtFileContent";
            this.txtFileContent.Size = new System.Drawing.Size(837, 377);
            this.txtFileContent.TabIndex = 23;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            // 
            // NovelToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 520);
            this.Controls.Add(this.txtFileContent);
            this.Controls.Add(this.btnProccess);
            this.Controls.Add(this.btnOutDir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOutputDir);
            this.Controls.Add(this.btnTempDir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTemplDir);
            this.Controls.Add(this.btnReadOrginalFile);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NovelToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小说文字处理器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReadOrginalFile;
        private System.Windows.Forms.Button btnOutDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button btnTempDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTemplDir;
        private System.Windows.Forms.Button btnProccess;
        private System.Windows.Forms.TextBox txtFileContent;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}