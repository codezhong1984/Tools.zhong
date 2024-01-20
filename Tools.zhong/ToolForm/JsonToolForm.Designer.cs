
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
            this.btnShowNodeType1 = new System.Windows.Forms.Button();
            this.btnClearOutput1 = new System.Windows.Forms.Button();
            this.btnShowNode1 = new System.Windows.Forms.Button();
            this.btnFormat1 = new System.Windows.Forms.Button();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.btnClear1 = new System.Windows.Forms.Button();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInput1 = new System.Windows.Forms.TextBox();
            this.txtOutput1 = new System.Windows.Forms.TextBox();
            this.btnCreate1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClearOutput2 = new System.Windows.Forms.Button();
            this.btnShowNode = new System.Windows.Forms.Button();
            this.txtFormat = new System.Windows.Forms.Button();
            this.txtNode = new System.Windows.Forms.TextBox();
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
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbIfTrim = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(1082, 653);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbIfTrim);
            this.tabPage1.Controls.Add(this.txtNameSpace);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnShowNodeType1);
            this.tabPage1.Controls.Add(this.btnClearOutput1);
            this.tabPage1.Controls.Add(this.btnShowNode1);
            this.tabPage1.Controls.Add(this.btnFormat1);
            this.tabPage1.Controls.Add(this.txtClassName);
            this.tabPage1.Controls.Add(this.btnClear1);
            this.tabPage1.Controls.Add(this.lblMsg1);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtInput1);
            this.tabPage1.Controls.Add(this.txtOutput1);
            this.tabPage1.Controls.Add(this.btnCreate1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1074, 624);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "JSON对象生成类";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnShowNodeType1
            // 
            this.btnShowNodeType1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowNodeType1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShowNodeType1.ForeColor = System.Drawing.Color.Blue;
            this.btnShowNodeType1.Location = new System.Drawing.Point(766, 277);
            this.btnShowNodeType1.Name = "btnShowNodeType1";
            this.btnShowNodeType1.Size = new System.Drawing.Size(118, 29);
            this.btnShowNodeType1.TabIndex = 64;
            this.btnShowNodeType1.Text = "显示节点类型";
            this.btnShowNodeType1.UseVisualStyleBackColor = true;
            this.btnShowNodeType1.Click += new System.EventHandler(this.btnShowNodeType1_Click);
            // 
            // btnClearOutput1
            // 
            this.btnClearOutput1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearOutput1.Location = new System.Drawing.Point(1010, 309);
            this.btnClearOutput1.Name = "btnClearOutput1";
            this.btnClearOutput1.Size = new System.Drawing.Size(56, 23);
            this.btnClearOutput1.TabIndex = 63;
            this.btnClearOutput1.Text = "清空";
            this.btnClearOutput1.UseVisualStyleBackColor = true;
            this.btnClearOutput1.Click += new System.EventHandler(this.btnClearOutput1_Click);
            // 
            // btnShowNode1
            // 
            this.btnShowNode1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowNode1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShowNode1.ForeColor = System.Drawing.Color.Blue;
            this.btnShowNode1.Location = new System.Drawing.Point(676, 277);
            this.btnShowNode1.Name = "btnShowNode1";
            this.btnShowNode1.Size = new System.Drawing.Size(84, 29);
            this.btnShowNode1.TabIndex = 62;
            this.btnShowNode1.Text = "显示节点";
            this.btnShowNode1.UseVisualStyleBackColor = true;
            this.btnShowNode1.Click += new System.EventHandler(this.btnShowNode1_Click);
            // 
            // btnFormat1
            // 
            this.btnFormat1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormat1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnFormat1.ForeColor = System.Drawing.Color.Blue;
            this.btnFormat1.Location = new System.Drawing.Point(890, 277);
            this.btnFormat1.Name = "btnFormat1";
            this.btnFormat1.Size = new System.Drawing.Size(84, 29);
            this.btnFormat1.TabIndex = 61;
            this.btnFormat1.Text = "格式化";
            this.btnFormat1.UseVisualStyleBackColor = true;
            this.btnFormat1.Click += new System.EventHandler(this.btnFormat1_Click);
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtClassName.Location = new System.Drawing.Point(323, 279);
            this.txtClassName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtClassName.MaxLength = 0;
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(206, 25);
            this.txtClassName.TabIndex = 51;
            // 
            // btnClear1
            // 
            this.btnClear1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear1.Location = new System.Drawing.Point(1004, 3);
            this.btnClear1.Name = "btnClear1";
            this.btnClear1.Size = new System.Drawing.Size(56, 23);
            this.btnClear1.TabIndex = 60;
            this.btnClear1.Text = "清空";
            this.btnClear1.UseVisualStyleBackColor = true;
            this.btnClear1.Click += new System.EventHandler(this.btnClear1_Click);
            // 
            // lblMsg1
            // 
            this.lblMsg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblMsg1.Location = new System.Drawing.Point(6, 600);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(1063, 21);
            this.lblMsg1.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 320);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 55;
            this.label8.Text = "输出结果：";
            // 
            // txtInput1
            // 
            this.txtInput1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput1.Location = new System.Drawing.Point(7, 31);
            this.txtInput1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInput1.MaxLength = 0;
            this.txtInput1.Multiline = true;
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput1.Size = new System.Drawing.Size(1055, 239);
            this.txtInput1.TabIndex = 49;
            // 
            // txtOutput1
            // 
            this.txtOutput1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput1.Location = new System.Drawing.Point(9, 338);
            this.txtOutput1.MaxLength = 0;
            this.txtOutput1.Multiline = true;
            this.txtOutput1.Name = "txtOutput1";
            this.txtOutput1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput1.Size = new System.Drawing.Size(1057, 259);
            this.txtOutput1.TabIndex = 54;
            // 
            // btnCreate1
            // 
            this.btnCreate1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCreate1.ForeColor = System.Drawing.Color.Blue;
            this.btnCreate1.Location = new System.Drawing.Point(980, 277);
            this.btnCreate1.Name = "btnCreate1";
            this.btnCreate1.Size = new System.Drawing.Size(84, 29);
            this.btnCreate1.TabIndex = 53;
            this.btnCreate1.Text = "生成";
            this.btnCreate1.UseVisualStyleBackColor = true;
            this.btnCreate1.Click += new System.EventHandler(this.btnCreate1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 15);
            this.label9.TabIndex = 50;
            this.label9.Text = "JSON文本：";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(249, 284);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 52;
            this.label10.Text = "实体类名：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnClearOutput2);
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
            this.tabPage2.Size = new System.Drawing.Size(1074, 624);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "JSON节点提取";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClearOutput2
            // 
            this.btnClearOutput2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearOutput2.Location = new System.Drawing.Point(1006, 310);
            this.btnClearOutput2.Name = "btnClearOutput2";
            this.btnClearOutput2.Size = new System.Drawing.Size(56, 23);
            this.btnClearOutput2.TabIndex = 64;
            this.btnClearOutput2.Text = "清空";
            this.btnClearOutput2.UseVisualStyleBackColor = true;
            this.btnClearOutput2.Click += new System.EventHandler(this.btnClearOutput2_Click);
            // 
            // btnShowNode
            // 
            this.btnShowNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowNode.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShowNode.ForeColor = System.Drawing.Color.Blue;
            this.btnShowNode.Location = new System.Drawing.Point(805, 276);
            this.btnShowNode.Name = "btnShowNode";
            this.btnShowNode.Size = new System.Drawing.Size(84, 29);
            this.btnShowNode.TabIndex = 48;
            this.btnShowNode.Text = "显示节点";
            this.btnShowNode.UseVisualStyleBackColor = true;
            this.btnShowNode.Click += new System.EventHandler(this.btnShowNode_Click);
            // 
            // txtFormat
            // 
            this.txtFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFormat.Font = new System.Drawing.Font("宋体", 9F);
            this.txtFormat.ForeColor = System.Drawing.Color.Blue;
            this.txtFormat.Location = new System.Drawing.Point(895, 276);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(84, 29);
            this.txtFormat.TabIndex = 47;
            this.txtFormat.Text = "格式化";
            this.txtFormat.UseVisualStyleBackColor = true;
            this.txtFormat.Click += new System.EventHandler(this.txtFormat_Click);
            // 
            // txtNode
            // 
            this.txtNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNode.Location = new System.Drawing.Point(141, 279);
            this.txtNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNode.MaxLength = 0;
            this.txtNode.Name = "txtNode";
            this.txtNode.Size = new System.Drawing.Size(424, 25);
            this.txtNode.TabIndex = 27;
            // 
            // btnClear2
            // 
            this.btnClear2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear2.Location = new System.Drawing.Point(1006, 1);
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
            this.label3.Location = new System.Drawing.Point(134, 309);
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
            this.lblExample.Location = new System.Drawing.Point(189, 307);
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
            this.lblMsg2.Location = new System.Drawing.Point(8, 598);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(1063, 21);
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
            this.cbSplitChar.Location = new System.Drawing.Point(686, 280);
            this.cbSplitChar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSplitChar.Name = "cbSplitChar";
            this.cbSplitChar.Size = new System.Drawing.Size(113, 23);
            this.cbSplitChar.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(623, 284);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 41;
            this.label12.Text = "分隔符：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 318);
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
            this.txtInput2.Size = new System.Drawing.Size(1055, 239);
            this.txtInput2.TabIndex = 25;
            // 
            // txtOutput2
            // 
            this.txtOutput2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput2.Location = new System.Drawing.Point(11, 336);
            this.txtOutput2.MaxLength = 0;
            this.txtOutput2.Multiline = true;
            this.txtOutput2.Name = "txtOutput2";
            this.txtOutput2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput2.Size = new System.Drawing.Size(1057, 259);
            this.txtOutput2.TabIndex = 30;
            // 
            // btnProccess
            // 
            this.btnProccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProccess.Font = new System.Drawing.Font("宋体", 9F);
            this.btnProccess.ForeColor = System.Drawing.Color.Blue;
            this.btnProccess.Location = new System.Drawing.Point(985, 275);
            this.btnProccess.Name = "btnProccess";
            this.btnProccess.Size = new System.Drawing.Size(82, 29);
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
            this.label4.Location = new System.Drawing.Point(8, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "提取正则表达式：";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNameSpace.Location = new System.Drawing.Point(79, 279);
            this.txtNameSpace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameSpace.MaxLength = 0;
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(163, 25);
            this.txtNameSpace.TabIndex = 65;
            this.txtNameSpace.Text = "Model";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 66;
            this.label2.Text = "命名空间：";
            // 
            // cbIfTrim
            // 
            this.cbIfTrim.AutoSize = true;
            this.cbIfTrim.Checked = true;
            this.cbIfTrim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIfTrim.Location = new System.Drawing.Point(547, 282);
            this.cbIfTrim.Name = "cbIfTrim";
            this.cbIfTrim.Size = new System.Drawing.Size(104, 19);
            this.cbIfTrim.TabIndex = 67;
            this.cbIfTrim.Text = "自动去空格";
            this.cbIfTrim.UseVisualStyleBackColor = true;
            // 
            // JsonToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JsonToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSON数据处理工具";
            this.Load += new System.EventHandler(this.JsonToolForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Button btnClear1;
        private System.Windows.Forms.Label lblMsg1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInput1;
        private System.Windows.Forms.TextBox txtOutput1;
        private System.Windows.Forms.Button btnCreate1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnShowNode1;
        private System.Windows.Forms.Button btnFormat1;
        private System.Windows.Forms.Button btnClearOutput1;
        private System.Windows.Forms.Button btnClearOutput2;
        private System.Windows.Forms.Button btnShowNodeType1;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbIfTrim;
    }
}