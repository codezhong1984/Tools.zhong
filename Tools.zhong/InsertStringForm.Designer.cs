
namespace Tools.zhong
{
    partial class InsertStringForm
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
            this.txtInsertString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSplitChar = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbBeforeOrAfter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "分隔符：";
            // 
            // txtInsertString
            // 
            this.txtInsertString.Location = new System.Drawing.Point(15, 121);
            this.txtInsertString.Multiline = true;
            this.txtInsertString.Name = "txtInsertString";
            this.txtInsertString.Size = new System.Drawing.Size(286, 72);
            this.txtInsertString.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "添加字符串：";
            // 
            // cbSplitChar
            // 
            this.cbSplitChar.FormattingEnabled = true;
            this.cbSplitChar.Items.AddRange(new object[] {
            ",",
            ";"});
            this.cbSplitChar.Location = new System.Drawing.Point(70, 20);
            this.cbSplitChar.Name = "cbSplitChar";
            this.cbSplitChar.Size = new System.Drawing.Size(231, 23);
            this.cbSplitChar.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.Blue;
            this.btnOk.Location = new System.Drawing.Point(213, 203);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 33);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbBeforeOrAfter
            // 
            this.cbBeforeOrAfter.FormattingEnabled = true;
            this.cbBeforeOrAfter.Location = new System.Drawing.Point(70, 64);
            this.cbBeforeOrAfter.Name = "cbBeforeOrAfter";
            this.cbBeforeOrAfter.Size = new System.Drawing.Size(231, 23);
            this.cbBeforeOrAfter.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "位置：";
            // 
            // InsertStringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 244);
            this.Controls.Add(this.cbBeforeOrAfter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbSplitChar);
            this.Controls.Add(this.txtInsertString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertStringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置按分隔符添加字符串";
            this.Load += new System.EventHandler(this.InsertStringForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInsertString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSplitChar;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cbBeforeOrAfter;
        private System.Windows.Forms.Label label3;
    }
}