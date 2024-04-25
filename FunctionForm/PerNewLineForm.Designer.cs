
namespace Tools.zhong
{
    partial class PerNewLineForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCols = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbChar = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbReserveSplitChar = new System.Windows.Forms.CheckBox();
            this.cbTrimEmptyLine = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(54, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 33);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "换行符：";
            // 
            // txtCols
            // 
            this.txtCols.Location = new System.Drawing.Point(123, 67);
            this.txtCols.Name = "txtCols";
            this.txtCols.Size = new System.Drawing.Size(121, 25);
            this.txtCols.TabIndex = 4;
            this.txtCols.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "每行列数：";
            // 
            // cbChar
            // 
            this.cbChar.FormattingEnabled = true;
            this.cbChar.Items.AddRange(new object[] {
            ",",
            ";"});
            this.cbChar.Location = new System.Drawing.Point(123, 24);
            this.cbChar.Name = "cbChar";
            this.cbChar.Size = new System.Drawing.Size(121, 23);
            this.cbChar.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.Blue;
            this.btnOk.Location = new System.Drawing.Point(186, 200);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 33);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbReserveSplitChar
            // 
            this.cbReserveSplitChar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbReserveSplitChar.AutoSize = true;
            this.cbReserveSplitChar.Checked = true;
            this.cbReserveSplitChar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReserveSplitChar.Location = new System.Drawing.Point(112, 114);
            this.cbReserveSplitChar.Name = "cbReserveSplitChar";
            this.cbReserveSplitChar.Size = new System.Drawing.Size(104, 19);
            this.cbReserveSplitChar.TabIndex = 7;
            this.cbReserveSplitChar.Text = "保留换行符";
            this.cbReserveSplitChar.UseVisualStyleBackColor = true;
            // 
            // cbTrimEmptyLine
            // 
            this.cbTrimEmptyLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbTrimEmptyLine.AutoSize = true;
            this.cbTrimEmptyLine.Checked = true;
            this.cbTrimEmptyLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTrimEmptyLine.Location = new System.Drawing.Point(112, 149);
            this.cbTrimEmptyLine.Name = "cbTrimEmptyLine";
            this.cbTrimEmptyLine.Size = new System.Drawing.Size(104, 19);
            this.cbTrimEmptyLine.TabIndex = 8;
            this.cbTrimEmptyLine.Text = "去除空白行";
            this.cbTrimEmptyLine.UseVisualStyleBackColor = true;
            // 
            // PerNewLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 259);
            this.Controls.Add(this.cbTrimEmptyLine);
            this.Controls.Add(this.cbReserveSplitChar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbChar);
            this.Controls.Add(this.txtCols);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PerNewLineForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "定制化换行";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCols;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbChar;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbReserveSplitChar;
        private System.Windows.Forms.CheckBox cbTrimEmptyLine;
    }
}