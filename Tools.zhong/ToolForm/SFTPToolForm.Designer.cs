
namespace Tools.zhong
{
    partial class SFTPToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFTPToolForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lbPort = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemoteFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoacalFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDownLoad = new System.Windows.Forms.Button();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbFTPType = new System.Windows.Forms.ComboBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeViewFTPFolder = new System.Windows.Forms.TreeView();
            this.btnLoadFTPFolder = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "HOST:";
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpload.BackColor = System.Drawing.SystemColors.Control;
            this.btnUpload.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnUpload.Location = new System.Drawing.Point(324, 283);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(91, 27);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtHost
            // 
            this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHost.Location = new System.Drawing.Point(116, 53);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(788, 25);
            this.txtHost.TabIndex = 4;
            this.txtHost.Text = "HQASFTP.TPV-TECH.COM";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Location = new System.Drawing.Point(116, 92);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(788, 25);
            this.txtPort.TabIndex = 6;
            this.txtPort.Text = "22";
            // 
            // lbPort
            // 
            this.lbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(15, 99);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(47, 15);
            this.lbPort.TabIndex = 5;
            this.lbPort.Text = "PORT:";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(116, 131);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(788, 25);
            this.txtUserName.TabIndex = 8;
            this.txtUserName.Text = "Hub_Manage";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "USER NAME:";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(116, 170);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(787, 25);
            this.txtPassword.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "PASSWORD:";
            // 
            // txtRemoteFolder
            // 
            this.txtRemoteFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemoteFolder.Location = new System.Drawing.Point(116, 209);
            this.txtRemoteFolder.Name = "txtRemoteFolder";
            this.txtRemoteFolder.Size = new System.Drawing.Size(714, 25);
            this.txtRemoteFolder.TabIndex = 12;
            this.txtRemoteFolder.Text = "\\PTSOUTS";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "FTP目录:";
            // 
            // txtLoacalFolder
            // 
            this.txtLoacalFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoacalFolder.Location = new System.Drawing.Point(116, 248);
            this.txtLoacalFolder.Name = "txtLoacalFolder";
            this.txtLoacalFolder.Size = new System.Drawing.Size(714, 25);
            this.txtLoacalFolder.TabIndex = 14;
            this.txtLoacalFolder.Text = "D:\\Hub\\SFTP(CSDB)\\test.data";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "本地文件:";
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDownLoad.BackColor = System.Drawing.SystemColors.Control;
            this.btnDownLoad.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownLoad.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDownLoad.Location = new System.Drawing.Point(449, 283);
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(91, 27);
            this.btnDownLoad.TabIndex = 15;
            this.btnDownLoad.Text = "下载";
            this.btnDownLoad.UseVisualStyleBackColor = false;
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTestConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestConnect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestConnect.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTestConnect.Location = new System.Drawing.Point(199, 283);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(91, 27);
            this.btnTestConnect.TabIndex = 16;
            this.btnTestConnect.Text = "测试连接";
            this.btnTestConnect.UseVisualStyleBackColor = false;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClose.Location = new System.Drawing.Point(736, 283);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 27);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "退 出";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "FTP TYPE:";
            // 
            // cbFTPType
            // 
            this.cbFTPType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFTPType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFTPType.FormattingEnabled = true;
            this.cbFTPType.Items.AddRange(new object[] {
            "SFTP",
            "FTP"});
            this.cbFTPType.Location = new System.Drawing.Point(118, 16);
            this.cbFTPType.Name = "cbFTPType";
            this.cbFTPType.Size = new System.Drawing.Size(785, 23);
            this.cbFTPType.TabIndex = 21;
            this.cbFTPType.SelectedIndexChanged += new System.EventHandler(this.cbFTPType_SelectedIndexChanged);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Location = new System.Drawing.Point(831, 245);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(74, 29);
            this.btnChoose.TabIndex = 22;
            this.btnChoose.Text = "...";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.AutoSize = true;
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(12, 679);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(55, 15);
            this.lblResult.TabIndex = 23;
            this.lblResult.Text = "Result";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDelete.Location = new System.Drawing.Point(574, 283);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(128, 27);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "删除远程文件";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(187, 319);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(733, 350);
            this.dataGridView1.TabIndex = 25;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "FileName";
            this.Column1.HeaderText = "文件名";
            this.Column1.MinimumWidth = 100;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FileSize";
            this.Column2.HeaderText = "文件大小(B)";
            this.Column2.MinimumWidth = 100;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "LastWriteTime";
            this.Column4.HeaderText = "修改时间";
            this.Column4.MinimumWidth = 100;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "LastWriteTimeUTC";
            this.Column3.HeaderText = "修改时间UTC";
            this.Column3.MinimumWidth = 100;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // treeViewFTPFolder
            // 
            this.treeViewFTPFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewFTPFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewFTPFolder.FullRowSelect = true;
            this.treeViewFTPFolder.Location = new System.Drawing.Point(10, 319);
            this.treeViewFTPFolder.Name = "treeViewFTPFolder";
            this.treeViewFTPFolder.ShowNodeToolTips = true;
            this.treeViewFTPFolder.Size = new System.Drawing.Size(171, 350);
            this.treeViewFTPFolder.TabIndex = 26;
            this.treeViewFTPFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFTPFolder_AfterSelect);
            // 
            // btnLoadFTPFolder
            // 
            this.btnLoadFTPFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFTPFolder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadFTPFolder.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLoadFTPFolder.Location = new System.Drawing.Point(831, 207);
            this.btnLoadFTPFolder.Name = "btnLoadFTPFolder";
            this.btnLoadFTPFolder.Size = new System.Drawing.Size(74, 29);
            this.btnLoadFTPFolder.TabIndex = 27;
            this.btnLoadFTPFolder.Text = "加载";
            this.btnLoadFTPFolder.UseVisualStyleBackColor = true;
            this.btnLoadFTPFolder.Click += new System.EventHandler(this.btnLoadFTPFolder_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnRefresh.Location = new System.Drawing.Point(96, 283);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(69, 27);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // SFTPToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 703);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLoadFTPFolder);
            this.Controls.Add(this.treeViewFTPFolder);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.cbFTPType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTestConnect);
            this.Controls.Add(this.btnDownLoad);
            this.Controls.Add(this.txtLoacalFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRemoteFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SFTPToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP TOOL";
            this.Load += new System.EventHandler(this.SFTPToolForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRemoteFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoacalFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDownLoad;
        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbFTPType;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TreeView treeViewFTPFolder;
        private System.Windows.Forms.Button btnLoadFTPFolder;
        private System.Windows.Forms.Button btnRefresh;
    }
}