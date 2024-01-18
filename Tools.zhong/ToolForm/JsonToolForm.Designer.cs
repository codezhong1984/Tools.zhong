
namespace Tools.zhong
{
    partial class JsonToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonToolForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClear2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblExample = new System.Windows.Forms.Label();
            this.lblMsg2 = new System.Windows.Forms.Label();
            this.cbSplitChar = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this.txtOutput2 = new System.Windows.Forms.TextBox();
            this.btnProccess = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNode = new System.Windows.Forms.TextBox();
            this.txtFormat = new System.Windows.Forms.Button();
            this.btnShowNode = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1037, 641);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(852, 491);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "JSON对象生成类";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnShowNode);
            this.tabPage2.Controls.Add(this.txtFormat);
            this.tabPage2.Controls.Add(this.txtNode);
            this.tabPage2.Controls.Add(this.btnClear2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.lblExample);
            this.tabPage2.Controls.Add(this.lblMsg2);
            this.tabPage2.Controls.Add(this.cbSplitChar);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtInput2);
            this.tabPage2.Controls.Add(this.txtOutput2);
            this.tabPage2.Controls.Add(this.btnProccess);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1029, 612);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "JSON节点提取";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClear2
            // 
            this.btnClear2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear2.Location = new System.Drawing.Point(961, 2);
            this.btnClear2.Name = "btnClear2";
            this.btnClear2.Size = new System.Drawing.Size(56, 23);
            this.btnClear2.TabIndex = 46;
            this.btnClear2.Text = "清空";
            this.btnClear2.UseVisualStyleBackColor = true;
            this.btnClear2.Click += new System.EventHandler(this.btnClear2_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 45;
            this.label3.Text = "示例：";
            // 
            // lblExample
            // 
            this.lblExample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExample.AutoSize = true;
            this.lblExample.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExample.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblExample.Location = new System.Drawing.Point(189, 295);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(231, 15);
            this.lblExample.TabIndex = 44;
            this.lblExample.Text = "ReqBody\\.SNList\\[\\d+\\]\\.FGSN";
            this.lblExample.Click += new System.EventHandler(this.lblExample_Click);
            // 
            // lblMsg2
            // 
            this.lblMsg2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblMsg2.Location = new System.Drawing.Point(8, 586);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(1018, 21);
            this.lblMsg2.TabIndex = 43;
            // 
            // cbSplitChar
            // 
            this.cbSplitChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSplitChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSplitChar.FormattingEnabled = true;
            this.cbSplitChar.Items.AddRange(new object[] {
            "回车换行",
            "逗号",
            "空格",
            "Tab",
            "单引号",
            "双引号",
            "冒号"});
            this.cbSplitChar.Location = new System.Drawing.Point(641, 268);
            this.cbSplitChar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSplitChar.Name = "cbSplitChar";
            this.cbSplitChar.Size = new System.Drawing.Size(113, 23);
            this.cbSplitChar.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(578, 272);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 41;
            this.label12.Text = "分隔符：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "输出结果：";
            // 
            // txtInput2
            // 
            this.txtInput2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput2.Location = new System.Drawing.Point(9, 29);
            this.txtInput2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInput2.MaxLength = 0;
            this.txtInput2.Multiline = true;
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput2.Size = new System.Drawing.Size(1010, 227);
            this.txtInput2.TabIndex = 25;
            // 
            // txtOutput2
            // 
            this.txtOutput2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput2.Location = new System.Drawing.Point(11, 324);
            this.txtOutput2.MaxLength = 0;
            this.txtOutput2.Multiline = true;
            this.txtOutput2.Name = "txtOutput2";
            this.txtOutput2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput2.Size = new System.Drawing.Size(1012, 259);
            this.txtOutput2.TabIndex = 30;
            // 
            // btnProccess
            // 
            this.btnProccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProccess.Font = new System.Drawing.Font("宋体", 9F);
            this.btnProccess.ForeColor = System.Drawing.Color.Blue;
            this.btnProccess.Location = new System.Drawing.Point(942, 263);
            this.btnProccess.Name = "btnProccess";
            this.btnProccess.Size = new System.Drawing.Size(74, 29);
            this.btnProccess.TabIndex = 29;
            this.btnProccess.Text = "提取";
            this.btnProccess.UseVisualStyleBackColor = true;
            this.btnProccess.Click += new System.EventHandler(this.btnProccess_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 15);
            this.label5.TabIndex = 26;
            this.label5.Text = "JSON文本：";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "提取正则表达式：";
            // 
            // txtNode
            // 
            this.txtNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNode.Location = new System.Drawing.Point(141, 267);
            this.txtNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNode.MaxLength = 0;
            this.txtNode.Name = "txtNode";
            this.txtNode.Size = new System.Drawing.Size(424, 25);
            this.txtNode.TabIndex = 27;
            // 
            // txtFormat
            // 
            this.txtFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFormat.Font = new System.Drawing.Font("宋体", 9F);
            this.txtFormat.ForeColor = System.Drawing.Color.Blue;
            this.txtFormat.Location = new System.Drawing.Point(850, 264);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(84, 29);
            this.txtFormat.TabIndex = 47;
            this.txtFormat.Text = "格式化";
            this.txtFormat.UseVisualStyleBackColor = true;
            this.txtFormat.Click += new System.EventHandler(this.txtFormat_Click);
            // 
            // btnShowNode
            // 
            this.btnShowNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowNode.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShowNode.ForeColor = System.Drawing.Color.Blue;
            this.btnShowNode.Location = new System.Drawing.Point(760, 264);
            this.btnShowNode.Name = "btnShowNode";
            this.btnShowNode.Size = new System.Drawing.Size(84, 29);
            this.btnShowNode.TabIndex = 48;
            this.btnShowNode.Text = "显示节点";
            this.btnShowNode.UseVisualStyleBackColor = true;
            this.btnShowNode.Click += new System.EventHandler(this.btnShowNode_Click);
            // 
            // JsonToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 641);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JsonToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSON数据处理工具";
            this.Load += new System.EventHandler(this.JsonToolForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtInput2;
        private System.Windows.Forms.TextBox txtOutput2;
        private System.Windows.Forms.Button btnProccess;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSplitChar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblMsg2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.Button btnClear2;
        private System.Windows.Forms.Button txtFormat;
        private System.Windows.Forms.Button btnShowNode;
    }
}