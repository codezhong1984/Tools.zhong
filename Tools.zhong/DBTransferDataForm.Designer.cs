
namespace Tools.zhong
{
    partial class DBTransferDataForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.tbPerRows = new System.Windows.Forms.NumericUpDown();
            this.lblDone = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSrcTableName = new System.Windows.Forms.TextBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDescTableName = new System.Windows.Forms.TextBox();
            this.lblDesTableName = new System.Windows.Forms.Label();
            this.lblSpent = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSrcConn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescConn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSaveDescConn = new System.Windows.Forms.Button();
            this.btnSaveSrcConn = new System.Windows.Forms.Button();
            this.cbDBType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbPerRows)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(270, 297);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "每次处理：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 351);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "总共：";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Location = new System.Drawing.Point(129, 351);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(2, 17);
            this.lblTotal.TabIndex = 4;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(355, 297);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tbPerRows
            // 
            this.tbPerRows.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tbPerRows.Location = new System.Drawing.Point(126, 296);
            this.tbPerRows.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.tbPerRows.Name = "tbPerRows";
            this.tbPerRows.Size = new System.Drawing.Size(89, 25);
            this.tbPerRows.TabIndex = 6;
            this.tbPerRows.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDone.Location = new System.Drawing.Point(273, 351);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(2, 17);
            this.lblDone.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "已处理：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(44, 377);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(417, 19);
            this.progressBar1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "源表表名：";
            // 
            // tbSrcTableName
            // 
            this.tbSrcTableName.Location = new System.Drawing.Point(126, 158);
            this.tbSrcTableName.Name = "tbSrcTableName";
            this.tbSrcTableName.Size = new System.Drawing.Size(304, 25);
            this.tbSrcTableName.TabIndex = 11;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(126, 202);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(304, 25);
            this.txtFilter.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "条件：";
            // 
            // tbDescTableName
            // 
            this.tbDescTableName.Location = new System.Drawing.Point(126, 246);
            this.tbDescTableName.Name = "tbDescTableName";
            this.tbDescTableName.Size = new System.Drawing.Size(304, 25);
            this.tbDescTableName.TabIndex = 15;
            // 
            // lblDesTableName
            // 
            this.lblDesTableName.AutoSize = true;
            this.lblDesTableName.Location = new System.Drawing.Point(46, 251);
            this.lblDesTableName.Name = "lblDesTableName";
            this.lblDesTableName.Size = new System.Drawing.Size(82, 15);
            this.lblDesTableName.TabIndex = 14;
            this.lblDesTableName.Text = "目标表名：";
            // 
            // lblSpent
            // 
            this.lblSpent.AutoSize = true;
            this.lblSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSpent.Location = new System.Drawing.Point(399, 351);
            this.lblSpent.Name = "lblSpent";
            this.lblSpent.Size = new System.Drawing.Size(2, 17);
            this.lblSpent.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(354, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "用时：";
            // 
            // txtSrcConn
            // 
            this.txtSrcConn.Location = new System.Drawing.Point(126, 70);
            this.txtSrcConn.Name = "txtSrcConn";
            this.txtSrcConn.Size = new System.Drawing.Size(304, 25);
            this.txtSrcConn.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(16, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "源数据库连接：";
            // 
            // txtDescConn
            // 
            this.txtDescConn.Location = new System.Drawing.Point(126, 114);
            this.txtDescConn.Name = "txtDescConn";
            this.txtDescConn.Size = new System.Drawing.Size(304, 25);
            this.txtDescConn.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(1, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "目标数据库连接：";
            // 
            // btnSaveDescConn
            // 
            this.btnSaveDescConn.Location = new System.Drawing.Point(432, 117);
            this.btnSaveDescConn.Name = "btnSaveDescConn";
            this.btnSaveDescConn.Size = new System.Drawing.Size(77, 23);
            this.btnSaveDescConn.TabIndex = 22;
            this.btnSaveDescConn.Text = "保存连接";
            this.btnSaveDescConn.UseVisualStyleBackColor = true;
            this.btnSaveDescConn.Click += new System.EventHandler(this.btnSaveDescConn_Click);
            // 
            // btnSaveSrcConn
            // 
            this.btnSaveSrcConn.Location = new System.Drawing.Point(432, 69);
            this.btnSaveSrcConn.Name = "btnSaveSrcConn";
            this.btnSaveSrcConn.Size = new System.Drawing.Size(77, 23);
            this.btnSaveSrcConn.TabIndex = 23;
            this.btnSaveSrcConn.Text = "保存连接";
            this.btnSaveSrcConn.UseVisualStyleBackColor = true;
            this.btnSaveSrcConn.Click += new System.EventHandler(this.btnSaveSrcConn_Click);
            // 
            // cbDBType
            // 
            this.cbDBType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDBType.FormattingEnabled = true;
            this.cbDBType.Location = new System.Drawing.Point(126, 27);
            this.cbDBType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDBType.Name = "cbDBType";
            this.cbDBType.Size = new System.Drawing.Size(304, 23);
            this.cbDBType.TabIndex = 24;
            this.cbDBType.SelectedIndexChanged += new System.EventHandler(this.cbDBType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "DB类型：";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnExit.Location = new System.Drawing.Point(415, 415);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 32);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(432, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "如：R_KPSN_T";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(432, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 12);
            this.label11.TabIndex = 28;
            this.label11.Text = "如：Site=\'85\'";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(432, 254);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 29;
            this.label12.Text = "如：R_KPSN_T";
            // 
            // DBTransferDataForm
            // 
            this.ClientSize = new System.Drawing.Size(512, 454);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbDBType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSaveSrcConn);
            this.Controls.Add(this.btnSaveDescConn);
            this.Controls.Add(this.txtDescConn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSrcConn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.lblSpent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDescTableName);
            this.Controls.Add(this.lblDesTableName);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSrcTableName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPerRows);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBTransferDataForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库导入工具";
            this.Load += new System.EventHandler(this.DBTransferDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbPerRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NumericUpDown tbPerRows;
        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSrcTableName;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDescTableName;
        private System.Windows.Forms.Label lblDesTableName;
        private System.Windows.Forms.Label lblSpent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSrcConn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescConn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSaveDescConn;
        private System.Windows.Forms.Button btnSaveSrcConn;
        private System.Windows.Forms.ComboBox cbDBType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}