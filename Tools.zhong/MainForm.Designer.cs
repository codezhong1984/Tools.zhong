
namespace Tools.zhong
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCreateModelByInput = new System.Windows.Forms.Button();
            this.btnCreateModelFromDBScript = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemoveCol = new System.Windows.Forms.Button();
            this.txtTempl = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmKg2Dh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDh2Hh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddDyh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddSyh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelDyh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelSyh = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTitle = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnCustom = new System.Windows.Forms.Button();
            this.txtCustom = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnOpenPath2 = new System.Windows.Forms.Button();
            this.btnExportToFile2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtTableName3 = new System.Windows.Forms.ComboBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy3 = new System.Windows.Forms.Button();
            this.btnLoadFromDB = new System.Windows.Forms.Button();
            this.cbDBType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKey3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPerColNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreateSelect = new System.Windows.Forms.Button();
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.btnCreateUpdate = new System.Windows.Forms.Button();
            this.btnOutput3 = new System.Windows.Forms.Button();
            this.btnInput3 = new System.Windows.Forms.Button();
            this.txtOuput3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateInsert = new System.Windows.Forms.Button();
            this.txtInput3 = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tsmReplaceLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-3, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1144, 637);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCreateModelByInput);
            this.tabPage1.Controls.Add(this.btnCreateModelFromDBScript);
            this.tabPage1.Controls.Add(this.btnImport);
            this.tabPage1.Controls.Add(this.btnRemoveAll);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.btnRemoveCol);
            this.tabPage1.Controls.Add(this.txtTempl);
            this.tabPage1.Controls.Add(this.txtTitle);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.btnOutput);
            this.tabPage1.Controls.Add(this.btnCustom);
            this.tabPage1.Controls.Add(this.txtCustom);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1136, 608);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "代码生成主功能";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCreateModelByInput
            // 
            this.btnCreateModelByInput.AutoEllipsis = true;
            this.btnCreateModelByInput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateModelByInput.ForeColor = System.Drawing.Color.Blue;
            this.btnCreateModelByInput.Location = new System.Drawing.Point(439, 189);
            this.btnCreateModelByInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateModelByInput.Name = "btnCreateModelByInput";
            this.btnCreateModelByInput.Size = new System.Drawing.Size(161, 29);
            this.btnCreateModelByInput.TabIndex = 19;
            this.btnCreateModelByInput.Text = "字符串生成Model类";
            this.btnCreateModelByInput.UseVisualStyleBackColor = true;
            this.btnCreateModelByInput.Click += new System.EventHandler(this.btnCreateModelByInput_Click);
            // 
            // btnCreateModelFromDBScript
            // 
            this.btnCreateModelFromDBScript.AutoEllipsis = true;
            this.btnCreateModelFromDBScript.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateModelFromDBScript.ForeColor = System.Drawing.Color.Blue;
            this.btnCreateModelFromDBScript.Location = new System.Drawing.Point(279, 189);
            this.btnCreateModelFromDBScript.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateModelFromDBScript.Name = "btnCreateModelFromDBScript";
            this.btnCreateModelFromDBScript.Size = new System.Drawing.Size(149, 29);
            this.btnCreateModelFromDBScript.TabIndex = 18;
            this.btnCreateModelFromDBScript.Text = "DB表生成Model类";
            this.btnCreateModelFromDBScript.UseVisualStyleBackColor = true;
            this.btnCreateModelFromDBScript.Click += new System.EventHandler(this.btnCreateModelFromDBScript_Click);
            // 
            // btnImport
            // 
            this.btnImport.AutoEllipsis = true;
            this.btnImport.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImport.ForeColor = System.Drawing.Color.Blue;
            this.btnImport.Location = new System.Drawing.Point(120, 189);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(148, 29);
            this.btnImport.TabIndex = 15;
            this.btnImport.Text = "类代码属性中提取";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.AutoEllipsis = true;
            this.btnRemoveAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveAll.ForeColor = System.Drawing.Color.Blue;
            this.btnRemoveAll.Location = new System.Drawing.Point(9, 189);
            this.btnRemoveAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(100, 29);
            this.btnRemoveAll.TabIndex = 14;
            this.btnRemoveAll.Text = "删除所有列";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.AutoEllipsis = true;
            this.btnClear.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Blue;
            this.btnClear.Location = new System.Drawing.Point(1001, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(127, 29);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "清空模板文本";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemoveCol
            // 
            this.btnRemoveCol.AutoEllipsis = true;
            this.btnRemoveCol.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveCol.ForeColor = System.Drawing.Color.Blue;
            this.btnRemoveCol.Location = new System.Drawing.Point(201, 232);
            this.btnRemoveCol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveCol.Name = "btnRemoveCol";
            this.btnRemoveCol.Size = new System.Drawing.Size(127, 29);
            this.btnRemoveCol.TabIndex = 12;
            this.btnRemoveCol.Text = "删除最后一列";
            this.btnRemoveCol.UseVisualStyleBackColor = true;
            this.btnRemoveCol.Click += new System.EventHandler(this.btnRemoveCol_Click);
            // 
            // txtTempl
            // 
            this.txtTempl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTempl.ContextMenuStrip = this.contextMenuStrip1;
            this.txtTempl.Location = new System.Drawing.Point(6, 41);
            this.txtTempl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTempl.Multiline = true;
            this.txtTempl.Name = "txtTempl";
            this.txtTempl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTempl.Size = new System.Drawing.Size(1122, 130);
            this.txtTempl.TabIndex = 7;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmKg2Dh,
            this.tsmDh2Hh,
            this.tsmAddDyh,
            this.tsmAddSyh,
            this.tsmDelDyh,
            this.tsmDelSyh,
            this.tsmReplaceLine});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 200);
            // 
            // tsmKg2Dh
            // 
            this.tsmKg2Dh.Name = "tsmKg2Dh";
            this.tsmKg2Dh.Size = new System.Drawing.Size(210, 24);
            this.tsmKg2Dh.Text = "空格替换为逗号";
            this.tsmKg2Dh.Click += new System.EventHandler(this.tsmKg2Dh_Click);
            // 
            // tsmDh2Hh
            // 
            this.tsmDh2Hh.Name = "tsmDh2Hh";
            this.tsmDh2Hh.Size = new System.Drawing.Size(210, 24);
            this.tsmDh2Hh.Text = "逗号替换为换行";
            this.tsmDh2Hh.Click += new System.EventHandler(this.tsmDh2Hh_Click);
            // 
            // tsmAddDyh
            // 
            this.tsmAddDyh.Name = "tsmAddDyh";
            this.tsmAddDyh.Size = new System.Drawing.Size(210, 24);
            this.tsmAddDyh.Text = "添加单引号";
            this.tsmAddDyh.Click += new System.EventHandler(this.tsmAddDyh_Click);
            // 
            // tsmAddSyh
            // 
            this.tsmAddSyh.Name = "tsmAddSyh";
            this.tsmAddSyh.Size = new System.Drawing.Size(210, 24);
            this.tsmAddSyh.Text = "添加双引号";
            this.tsmAddSyh.Click += new System.EventHandler(this.tsmAddSyh_Click);
            // 
            // tsmDelDyh
            // 
            this.tsmDelDyh.Name = "tsmDelDyh";
            this.tsmDelDyh.Size = new System.Drawing.Size(210, 24);
            this.tsmDelDyh.Text = "删除单引号";
            this.tsmDelDyh.Click += new System.EventHandler(this.tsmDelDyh_Click);
            // 
            // tsmDelSyh
            // 
            this.tsmDelSyh.Name = "tsmDelSyh";
            this.tsmDelSyh.Size = new System.Drawing.Size(210, 24);
            this.tsmDelSyh.Text = "删除双引号";
            this.tsmDelSyh.Click += new System.EventHandler(this.tsmDelSyh_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.AutoSize = true;
            this.txtTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTitle.ForeColor = System.Drawing.Color.Blue;
            this.txtTitle.Location = new System.Drawing.Point(3, 9);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(282, 15);
            this.txtTitle.TabIndex = 6;
            this.txtTitle.Text = "模板字符(需要被替换部分使用$0,$1...)";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 271);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1121, 333);
            this.dataGridView1.TabIndex = 11;
            // 
            // btnOutput
            // 
            this.btnOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutput.AutoEllipsis = true;
            this.btnOutput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOutput.ForeColor = System.Drawing.Color.Blue;
            this.btnOutput.Location = new System.Drawing.Point(1023, 231);
            this.btnOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(100, 29);
            this.btnOutput.TabIndex = 10;
            this.btnOutput.Text = "生成结果";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnCustom
            // 
            this.btnCustom.AutoEllipsis = true;
            this.btnCustom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCustom.ForeColor = System.Drawing.Color.Blue;
            this.btnCustom.Location = new System.Drawing.Point(117, 232);
            this.btnCustom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(74, 29);
            this.btnCustom.TabIndex = 9;
            this.btnCustom.Text = "添加列";
            this.btnCustom.UseVisualStyleBackColor = true;
            this.btnCustom.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // txtCustom
            // 
            this.txtCustom.AutoSize = true;
            this.txtCustom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustom.ForeColor = System.Drawing.Color.Blue;
            this.txtCustom.Location = new System.Drawing.Point(11, 238);
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.Size = new System.Drawing.Size(82, 15);
            this.txtCustom.TabIndex = 8;
            this.txtCustom.Text = "定制化列表";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnOpenPath2);
            this.tabPage2.Controls.Add(this.btnExportToFile2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtOutput);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(1136, 608);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "结果输出";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnOpenPath2
            // 
            this.btnOpenPath2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPath2.ForeColor = System.Drawing.Color.Blue;
            this.btnOpenPath2.Location = new System.Drawing.Point(781, 6);
            this.btnOpenPath2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenPath2.Name = "btnOpenPath2";
            this.btnOpenPath2.Size = new System.Drawing.Size(124, 26);
            this.btnOpenPath2.TabIndex = 8;
            this.btnOpenPath2.Text = "打开生成目录";
            this.btnOpenPath2.UseVisualStyleBackColor = true;
            this.btnOpenPath2.Click += new System.EventHandler(this.btnOpenPath2_Click);
            // 
            // btnExportToFile2
            // 
            this.btnExportToFile2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExportToFile2.ForeColor = System.Drawing.Color.Blue;
            this.btnExportToFile2.Location = new System.Drawing.Point(919, 5);
            this.btnExportToFile2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportToFile2.Name = "btnExportToFile2";
            this.btnExportToFile2.Size = new System.Drawing.Size(103, 26);
            this.btnExportToFile2.TabIndex = 7;
            this.btnExportToFile2.Text = "生成文件";
            this.btnExportToFile2.UseVisualStyleBackColor = true;
            this.btnExportToFile2.Click += new System.EventHandler(this.btnExportToFile2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(1036, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "返 回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "输出结果：";
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(7, 36);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(1123, 564);
            this.txtOutput.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtTableName3);
            this.tabPage3.Controls.Add(this.btnPaste);
            this.tabPage3.Controls.Add(this.btnCopy3);
            this.tabPage3.Controls.Add(this.btnLoadFromDB);
            this.tabPage3.Controls.Add(this.cbDBType);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.txtKey3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.txtPerColNum);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.btnCreateSelect);
            this.tabPage3.Controls.Add(this.btnCreateDelete);
            this.tabPage3.Controls.Add(this.btnCreateUpdate);
            this.tabPage3.Controls.Add(this.btnOutput3);
            this.tabPage3.Controls.Add(this.btnInput3);
            this.tabPage3.Controls.Add(this.txtOuput3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.btnCreateInsert);
            this.tabPage3.Controls.Add(this.txtInput3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(1136, 608);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SQL脚本生成辅助工具";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtTableName3
            // 
            this.txtTableName3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTableName3.FormattingEnabled = true;
            this.txtTableName3.Location = new System.Drawing.Point(328, 5);
            this.txtTableName3.Margin = new System.Windows.Forms.Padding(4);
            this.txtTableName3.Name = "txtTableName3";
            this.txtTableName3.Size = new System.Drawing.Size(207, 23);
            this.txtTableName3.TabIndex = 20;
            this.txtTableName3.SelectedIndexChanged += new System.EventHandler(this.txtTableName3_SelectedIndexChanged);
            // 
            // btnPaste
            // 
            this.btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaste.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPaste.ForeColor = System.Drawing.Color.Black;
            this.btnPaste.Location = new System.Drawing.Point(998, 2);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(60, 31);
            this.btnPaste.TabIndex = 19;
            this.btnPaste.Text = "粘贴";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy3
            // 
            this.btnCopy3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy3.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCopy3.ForeColor = System.Drawing.Color.Black;
            this.btnCopy3.Location = new System.Drawing.Point(998, 223);
            this.btnCopy3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopy3.Name = "btnCopy3";
            this.btnCopy3.Size = new System.Drawing.Size(60, 31);
            this.btnCopy3.TabIndex = 18;
            this.btnCopy3.Text = "复制";
            this.btnCopy3.UseVisualStyleBackColor = true;
            this.btnCopy3.Click += new System.EventHandler(this.btnCopy3_Click);
            // 
            // btnLoadFromDB
            // 
            this.btnLoadFromDB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLoadFromDB.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoadFromDB.Location = new System.Drawing.Point(889, 3);
            this.btnLoadFromDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadFromDB.Name = "btnLoadFromDB";
            this.btnLoadFromDB.Size = new System.Drawing.Size(103, 29);
            this.btnLoadFromDB.TabIndex = 17;
            this.btnLoadFromDB.Text = "DB加载";
            this.btnLoadFromDB.UseVisualStyleBackColor = true;
            this.btnLoadFromDB.Click += new System.EventHandler(this.btnLoadFromDB_Click);
            // 
            // cbDBType
            // 
            this.cbDBType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbDBType.FormattingEnabled = true;
            this.cbDBType.Items.AddRange(new object[] {
            "SQLSERVER",
            "ORACLE",
            "MySQL"});
            this.cbDBType.Location = new System.Drawing.Point(164, 5);
            this.cbDBType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDBType.Name = "cbDBType";
            this.cbDBType.Size = new System.Drawing.Size(108, 23);
            this.cbDBType.TabIndex = 10;
            this.cbDBType.SelectedIndexChanged += new System.EventHandler(this.cbDBType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "数据库类型：";
            // 
            // txtKey3
            // 
            this.txtKey3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtKey3.Location = new System.Drawing.Point(591, 5);
            this.txtKey3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtKey3.Multiline = true;
            this.txtKey3.Name = "txtKey3";
            this.txtKey3.Size = new System.Drawing.Size(292, 25);
            this.txtKey3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(539, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "主键：";
            // 
            // txtPerColNum
            // 
            this.txtPerColNum.Location = new System.Drawing.Point(581, 226);
            this.txtPerColNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPerColNum.Name = "txtPerColNum";
            this.txtPerColNum.Size = new System.Drawing.Size(40, 25);
            this.txtPerColNum.TabIndex = 12;
            this.txtPerColNum.Text = "8";
            this.txtPerColNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPerColNum.TextChanged += new System.EventHandler(this.txtPerColNum_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(464, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "每行几个字段：";
            // 
            // btnCreateSelect
            // 
            this.btnCreateSelect.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateSelect.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCreateSelect.Location = new System.Drawing.Point(8, 222);
            this.btnCreateSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateSelect.Name = "btnCreateSelect";
            this.btnCreateSelect.Size = new System.Drawing.Size(107, 31);
            this.btnCreateSelect.TabIndex = 11;
            this.btnCreateSelect.Text = "SELECT";
            this.btnCreateSelect.UseVisualStyleBackColor = true;
            this.btnCreateSelect.Click += new System.EventHandler(this.btnCreateSelect_Click);
            // 
            // btnCreateDelete
            // 
            this.btnCreateDelete.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateDelete.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCreateDelete.Location = new System.Drawing.Point(341, 222);
            this.btnCreateDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(107, 31);
            this.btnCreateDelete.TabIndex = 8;
            this.btnCreateDelete.Text = "DELETE";
            this.btnCreateDelete.UseVisualStyleBackColor = true;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            // 
            // btnCreateUpdate
            // 
            this.btnCreateUpdate.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateUpdate.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCreateUpdate.Location = new System.Drawing.Point(230, 222);
            this.btnCreateUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateUpdate.Name = "btnCreateUpdate";
            this.btnCreateUpdate.Size = new System.Drawing.Size(107, 31);
            this.btnCreateUpdate.TabIndex = 7;
            this.btnCreateUpdate.Text = "UPDATE";
            this.btnCreateUpdate.UseVisualStyleBackColor = true;
            this.btnCreateUpdate.Click += new System.EventHandler(this.btnCreateUpdate_Click);
            // 
            // btnOutput3
            // 
            this.btnOutput3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutput3.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnOutput3.ForeColor = System.Drawing.Color.Black;
            this.btnOutput3.Location = new System.Drawing.Point(1067, 223);
            this.btnOutput3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOutput3.Name = "btnOutput3";
            this.btnOutput3.Size = new System.Drawing.Size(60, 31);
            this.btnOutput3.TabIndex = 6;
            this.btnOutput3.Text = "清空";
            this.btnOutput3.UseVisualStyleBackColor = true;
            this.btnOutput3.Click += new System.EventHandler(this.btnOutput3_Click);
            // 
            // btnInput3
            // 
            this.btnInput3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInput3.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnInput3.ForeColor = System.Drawing.Color.Black;
            this.btnInput3.Location = new System.Drawing.Point(1067, 2);
            this.btnInput3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInput3.Name = "btnInput3";
            this.btnInput3.Size = new System.Drawing.Size(60, 31);
            this.btnInput3.TabIndex = 5;
            this.btnInput3.Text = "清空";
            this.btnInput3.UseVisualStyleBackColor = true;
            this.btnInput3.Click += new System.EventHandler(this.btnInput3_Click);
            // 
            // txtOuput3
            // 
            this.txtOuput3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOuput3.Location = new System.Drawing.Point(5, 264);
            this.txtOuput3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOuput3.Multiline = true;
            this.txtOuput3.Name = "txtOuput3";
            this.txtOuput3.Size = new System.Drawing.Size(1128, 345);
            this.txtOuput3.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "表名：";
            // 
            // btnCreateInsert
            // 
            this.btnCreateInsert.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateInsert.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCreateInsert.Location = new System.Drawing.Point(119, 222);
            this.btnCreateInsert.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateInsert.Name = "btnCreateInsert";
            this.btnCreateInsert.Size = new System.Drawing.Size(107, 31);
            this.btnCreateInsert.TabIndex = 1;
            this.btnCreateInsert.Text = "INSERT";
            this.btnCreateInsert.UseVisualStyleBackColor = true;
            this.btnCreateInsert.Click += new System.EventHandler(this.btnCreateInsert_Click);
            // 
            // txtInput3
            // 
            this.txtInput3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput3.Location = new System.Drawing.Point(5, 35);
            this.txtInput3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInput3.Multiline = true;
            this.txtInput3.Name = "txtInput3";
            this.txtInput3.Size = new System.Drawing.Size(1125, 175);
            this.txtInput3.TabIndex = 0;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "All files(*.*)|*.*\";";
            this.saveFileDialog1.Title = "保存文件";
            // 
            // tsmReplaceLine
            // 
            this.tsmReplaceLine.Name = "tsmReplaceLine";
            this.tsmReplaceLine.Size = new System.Drawing.Size(210, 24);
            this.tsmReplaceLine.Text = "替换回车换行";
            this.tsmReplaceLine.Click += new System.EventHandler(this.tsmReplaceLine_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 648);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码生成辅助工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox txtTempl;
        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemoveCol;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnOutput3;
        private System.Windows.Forms.Button btnInput3;
        private System.Windows.Forms.TextBox txtOuput3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateInsert;
        private System.Windows.Forms.TextBox txtInput3;
        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.Button btnCreateUpdate;
        private System.Windows.Forms.ComboBox cbDBType;
        private System.Windows.Forms.Button btnCreateSelect;
        private System.Windows.Forms.TextBox txtPerColNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKey3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadFromDB;
        private System.Windows.Forms.Button btnCopy3;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.ComboBox txtTableName3;
        private System.Windows.Forms.Button btnCreateModelFromDBScript;
        private System.Windows.Forms.Label txtCustom;
        private System.Windows.Forms.Button btnExportToFile2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnOpenPath2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmKg2Dh;
        private System.Windows.Forms.ToolStripMenuItem tsmDh2Hh;
        private System.Windows.Forms.ToolStripMenuItem tsmAddDyh;
        private System.Windows.Forms.ToolStripMenuItem tsmAddSyh;
        private System.Windows.Forms.ToolStripMenuItem tsmDelDyh;
        private System.Windows.Forms.ToolStripMenuItem tsmDelSyh;
        private System.Windows.Forms.Button btnCreateModelByInput;
        private System.Windows.Forms.ToolStripMenuItem tsmReplaceLine;
    }
}

