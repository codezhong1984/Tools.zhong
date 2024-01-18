using DBHepler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Tools.zhong.Component;
using Tools.zhong.Model;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public partial class DBTransferDataForm : Form
    {
        private int _TotalCount = 0;
        private int _DoneCount = 0;
        private int _PageSize = 0;
        private DateTime _StartDate;

        private string _SourceConnectionString = ConfigHelper.GetConnectionString("TransferSrcDB");
        private string _DescConnectionString = ConfigHelper.GetConnectionString("TransferDesDB");

        private DataBaseType _DataBaseType = DataBaseType.ORACLE;

        public DBTransferDataForm()
        {
            InitializeComponent();
        }


        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;
                int[] args = (int[])e.Argument;
                int pageSize = args[0];
                int totalCount = args[1];
                int doneCount = args[2];

                string sqlTemplate = @"select * 
                        from(select a.*, rownum rindex from {0} a #FILTER order by 1) t
                        where t.rindex > {1} and t.rindex <= {2}";

                sqlTemplate = sqlTemplate.Replace("#FILTER", !string.IsNullOrWhiteSpace(txtFilter.Text) ? "where " + txtFilter.Text : "");

                while (doneCount <= totalCount)
                {
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    //查询
                    var sql = string.Format(sqlTemplate, tbSrcTableName.Text, doneCount, pageSize + doneCount);

                    var dbType = _DataBaseType;
                    if (dbType == DataBaseType.ORACLE)
                    {
                        //查询数据
                        DataTable dt = OracleHelper.GetDataTable(_SourceConnectionString, sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dt.Columns.Remove("rindex");
                            //导入数据
                            OracleHelper.BatchInsertDataTable(_DescConnectionString, tbDescTableName.Text.Trim(), dt, 10 * 60);
                        }
                    }
                    else if (dbType == DataBaseType.MySQL)
                    {
                        //查询数据
                        DataTable dt = MySQLHelper.GetDataTable(_SourceConnectionString, sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dt.Columns.Remove("rindex");
                            //导入数据
                            MySQLHelper.BatchInsertDataTable(_DescConnectionString, tbDescTableName.Text.Trim(), dt);
                        }
                    }
                    else if (dbType == DataBaseType.SQLSERVER)
                    {
                        //查询数据
                        DataTable dt = SQLHelper.GetDataTable(_SourceConnectionString, sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dt.Columns.Remove("rindex");
                            //导入数据
                            SQLHelper.BatchInsertDataTable(_DescConnectionString, tbDescTableName.Text.Trim(), dt, 10 * 60);
                        }
                    }
                    doneCount += pageSize;
                    bw.ReportProgress(doneCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+$" 详细信息：{ex.StackTrace}");
            }          

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tbDescTableName.Text.Trim().Length == 0 || tbSrcTableName.Text.Trim().Length == 0)
            {
                tbDescTableName.Focus();
                return;
            }
            _PageSize = (int)this.tbPerRows.Value;
            _DoneCount = 0;
            _TotalCount = 0;

            //加载数据总数
            string sql = "select count(1) from {0} ";
            if (!string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                sql += "where " + txtFilter.Text;
            }
            //查询表信息
            object obj = null;
            var dbType = _DataBaseType;
            if (dbType == DataBaseType.ORACLE)
            {
                obj = OracleHelper.GetSingle(_SourceConnectionString, string.Format(sql, tbSrcTableName.Text));
            }
            else if (dbType == DataBaseType.MySQL)
            {
                obj = MySQLHelper.GetSingle(_SourceConnectionString, string.Format(sql, tbSrcTableName.Text));
            }
            else if (dbType == DataBaseType.SQLSERVER)
            {
                obj = SQLHelper.GetSingle(_SourceConnectionString, string.Format(sql, tbSrcTableName.Text));
            }

            _TotalCount = int.Parse(obj.ToString());

            progressBar1.Maximum = _TotalCount;
            progressBar1.Step = _PageSize;
            lblTotal.Text = _TotalCount.ToString();
            _StartDate = DateTime.Now;
            this.backgroundWorker1.RunWorkerAsync(new int[] { _PageSize, _TotalCount, _DoneCount });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > _TotalCount)
            {
                progressBar1.Value = progressBar1.Maximum;
                return;
            }
            progressBar1.Value = e.ProgressPercentage;
            lblDone.Text = e.ProgressPercentage.ToString();
            lblSpent.Text = $"{Math.Round((DateTime.Now - _StartDate).TotalSeconds, 2)}秒";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //若没有完全执行结束，则报错
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
                return;
            }
            if (!e.Cancelled)
                MessageBox.Show("处理完毕");
            else
                MessageBox.Show("处理终止");
        }

        private void DBTransferDataForm_Load(object sender, EventArgs e)
        {
            txtSrcConn.Text = _SourceConnectionString;
            txtDescConn.Text = _DescConnectionString;
            //加载数据库类型
            ComboBoxHelper.BindDBTypeComboBox(cbDBType);
        }

        private void btnSaveSrcConn_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.ExecutablePath + ".config");//获取当前配置文件

                var ParamName = "TransferSrcDB";
                var ParamValue = txtSrcConn.Text.Trim();

                XmlNode node = null;
                node = doc.SelectSingleNode(@"//add[@name='" + ParamName + "']");
                XmlElement ele = (XmlElement)node;
                ele.SetAttribute("connectionString", ParamValue);

                doc.Save(Application.ExecutablePath + ".config");
                ConfigurationManager.RefreshSection("connectionStrings");
                MessageBox.Show("保存成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常，" + ex.Message);
                return;
            }
        }

        private void btnSaveDescConn_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.ExecutablePath + ".config");//获取当前配置文件

                var ParamName = "TransferDesDB";
                var ParamValue = txtDescConn.Text.Trim();

                XmlNode node = null;
                node = doc.SelectSingleNode(@"//add[@name='" + ParamName + "']");
                XmlElement ele = (XmlElement)node;
                ele.SetAttribute("connectionString", ParamValue);

                doc.Save(Application.ExecutablePath + ".config");
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("保存成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常，" + ex.Message);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DataBaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
        }
    }
}
