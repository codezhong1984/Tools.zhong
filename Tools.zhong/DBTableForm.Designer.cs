
namespace Tools.zhong
{
    partial class DbTableForm
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
            this.cbDBType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLineDeal = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDisplayName = new System.Windows.Forms.CheckBox();
            this.cbTableName = new System.Windows.Forms.CheckedListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNameSpace = new System.Windows.Forms.TextBox();
            this.btnOpenPath2 = new System.Windows.Forms.Button();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.cbCreateTbName = new System.Windows.Forms.CheckBox();
            this.cbFullProp = new System.Windows.Forms.CheckBox();
            this.cbView = new System.Windows.Forms.CheckBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.Blue;
            this.btnOk.Location = new System.Drawing.Point(438, 593);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(330, 593);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "关 闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbDBType
            // 
            this.cbDBType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDBType.FormattingEnabled = true;
            this.cbDBType.Location = new System.Drawing.Point(92, 20);
            this.cbDBType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDBType.Name = "cbDBType";
            this.cbDBType.Size = new System.Drawing.Size(423, 23);
            this.cbDBType.TabIndex = 22;
            this.cbDBType.SelectedIndexChanged += new System.EventHandler(this.cbDBType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "DB类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "表或视图：";
            // 
            // cbLineDeal
            // 
            this.cbLineDeal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLineDeal.AutoSize = true;
            this.cbLineDeal.Location = new System.Drawing.Point(90, 453);
            this.cbLineDeal.Name = "cbLineDeal";
            this.cbLineDeal.Size = new System.Drawing.Size(134, 19);
            this.cbLineDeal.TabIndex = 26;
            this.cbLineDeal.Text = "是否处理下划线";
            this.cbLineDeal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(230, 453);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "注：首字母大写，下划线后接字母大写";
            // 
            // cbDisplayName
            // 
            this.cbDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDisplayName.AutoSize = true;
            this.cbDisplayName.Location = new System.Drawing.Point(90, 479);
            this.cbDisplayName.Name = "cbDisplayName";
            this.cbDisplayName.Size = new System.Drawing.Size(177, 19);
            this.cbDisplayName.TabIndex = 28;
            this.cbDisplayName.Text = "是否生成DisplayName";
            this.cbDisplayName.UseVisualStyleBackColor = true;
            // 
            // cbTableName
            // 
            this.cbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTableName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cbTableName.CheckOnClick = true;
            this.cbTableName.FormattingEnabled = true;
            this.cbTableName.Location = new System.Drawing.Point(92, 126);
            this.cbTableName.Name = "cbTableName";
            this.cbTableName.Size = new System.Drawing.Size(424, 280);
            this.cbTableName.TabIndex = 29;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(22, 593);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 27);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "保存为文件";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 31;
            this.label3.Text = "命名空间：";
            // 
            // tbNameSpace
            // 
            this.tbNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNameSpace.Location = new System.Drawing.Point(92, 54);
            this.tbNameSpace.Name = "tbNameSpace";
            this.tbNameSpace.Size = new System.Drawing.Size(424, 25);
            this.tbNameSpace.TabIndex = 32;
            this.tbNameSpace.Text = "DBModel";
            // 
            // btnOpenPath2
            // 
            this.btnOpenPath2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOpenPath2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPath2.ForeColor = System.Drawing.Color.Blue;
            this.btnOpenPath2.Location = new System.Drawing.Point(173, 593);
            this.btnOpenPath2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenPath2.Name = "btnOpenPath2";
            this.btnOpenPath2.Size = new System.Drawing.Size(124, 26);
            this.btnOpenPath2.TabIndex = 33;
            this.btnOpenPath2.Text = "打开生成目录";
            this.btnOpenPath2.UseVisualStyleBackColor = true;
            this.btnOpenPath2.Click += new System.EventHandler(this.btnOpenPath2_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(454, 420);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(59, 19);
            this.cbSelectAll.TabIndex = 34;
            this.cbSelectAll.Text = "全选";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // cbCreateTbName
            // 
            this.cbCreateTbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCreateTbName.AutoSize = true;
            this.cbCreateTbName.Checked = true;
            this.cbCreateTbName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateTbName.Location = new System.Drawing.Point(90, 507);
            this.cbCreateTbName.Name = "cbCreateTbName";
            this.cbCreateTbName.Size = new System.Drawing.Size(383, 19);
            this.cbCreateTbName.TabIndex = 35;
            this.cbCreateTbName.Text = "是否映射DB表类名，代码中包含：[Table(\"TANEM\")]";
            this.cbCreateTbName.UseVisualStyleBackColor = true;
            this.cbCreateTbName.CheckedChanged += new System.EventHandler(this.cbCreateTbName_CheckedChanged);
            // 
            // cbFullProp
            // 
            this.cbFullProp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFullProp.AutoSize = true;
            this.cbFullProp.Location = new System.Drawing.Point(90, 532);
            this.cbFullProp.Name = "cbFullProp";
            this.cbFullProp.Size = new System.Drawing.Size(205, 19);
            this.cbFullProp.TabIndex = 36;
            this.cbFullProp.Text = "是否生成完整GET|SET方法";
            this.cbFullProp.UseVisualStyleBackColor = true;
            // 
            // cbView
            // 
            this.cbView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbView.AutoSize = true;
            this.cbView.Location = new System.Drawing.Point(328, 93);
            this.cbView.Name = "cbView";
            this.cbView.Size = new System.Drawing.Size(104, 19);
            this.cbView.TabIndex = 37;
            this.cbView.Text = "仅加载视图";
            this.cbView.UseVisualStyleBackColor = true;
            this.cbView.CheckedChanged += new System.EventHandler(this.cbView_CheckedChanged);
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(92, 90);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(230, 25);
            this.tbFilter.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "过滤名称：";
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLoad.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoad.ForeColor = System.Drawing.Color.Blue;
            this.btnLoad.Location = new System.Drawing.Point(432, 89);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(84, 27);
            this.btnLoad.TabIndex = 40;
            this.btnLoad.Text = "加载";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // DbTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 648);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbView);
            this.Controls.Add(this.cbFullProp);
            this.Controls.Add(this.cbCreateTbName);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.btnOpenPath2);
            this.Controls.Add(this.tbNameSpace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbTableName);
            this.Controls.Add(this.cbDisplayName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLineDeal);
            this.Controls.Add(this.cbDBType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "DbTableForm";
            this.ShowIcon = false;
            this.Text = "选择数据库表";
            this.Load += new System.EventHandler(this.DBTaleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbDBType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbLineDeal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbDisplayName;
        private System.Windows.Forms.CheckedListBox cbTableName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNameSpace;
        private System.Windows.Forms.Button btnOpenPath2;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.CheckBox cbCreateTbName;
        private System.Windows.Forms.CheckBox cbFullProp;
        private System.Windows.Forms.CheckBox cbView;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoad;
    }
}