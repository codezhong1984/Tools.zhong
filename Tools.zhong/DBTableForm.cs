using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.zhong
{
    public partial class DBTaleForm : Form
    {
        public string CodeText { get; set; }
        public DBTaleForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbDBType.SelectedIndex <= 0)
            {
                cbDBType.Focus();
                MessageBox.Show("数据库类型未选择！");
            }
            if (cbTableName.SelectedIndex < 0)
            {
                cbTableName.Focus();
                MessageBox.Show("数据库表未选择！");
            }

            List<string> listTables = new List<string>();
            foreach (var item in cbTableName.CheckedItems)
            {
                listTables.Add(((DataRowView)item)["table_name"]?.ToString());
            }

            string tableName = string.Empty;
            StringBuilder sbCodes = new StringBuilder();
            #region CreateCodeText

            if (cbDBType.Text == "ORACLE")
            {
                foreach (var item in listTables)
                {
                    if (listTables.Count > 1)
                    {
                        sbCodes.AppendLine();
                        sbCodes.AppendLine($"//**************************** {item}  *******************************");
                        sbCodes.AppendLine();
                    }
                    var codeOracle = GetCodeForOracle(item);
                    sbCodes.AppendLine(codeOracle);
                }

                this.CodeText = sbCodes.ToString();
            }
            else if (cbDBType.Text == "SQLSERVER")
            {
                foreach (var item in listTables)
                {
                    if (listTables.Count > 1)
                    {
                        sbCodes.AppendLine();
                        sbCodes.AppendLine($"//**************************** {item}  *******************************");
                        sbCodes.AppendLine();
                    }
                    var codeSqlServer = GetCodeForSqlServer(item);
                    sbCodes.AppendLine(codeSqlServer);
                }

                this.CodeText = sbCodes.ToString();
            }
            else if (cbDBType.Text == "MySQL")
            {
                foreach (var item in listTables)
                {
                    if (listTables.Count > 1)
                    {
                        sbCodes.AppendLine();
                        sbCodes.AppendLine($"//**************************** {item}  *******************************");
                        sbCodes.AppendLine();
                    }
                    var codeMySQL = GetCodeForMySQL(item);
                    sbCodes.AppendLine(codeMySQL);
                }

                this.CodeText = sbCodes.ToString();
            }

            #endregion

            this.DialogResult = DialogResult.OK;
        }

        private string GetCodeForOracle(string tableName)
        {
            string sql = @"select a.TABLE_NAME,c.COMMENTS as table_comments, a.column_name,a.DATA_TYPE,b.Comments as column_comments,a.NULLABLE
                            from user_tab_columns a 
                            left join user_col_comments b on a.TABLE_NAME=b.TABLE_NAME and a.COLUMN_NAME = b.column_name
                            left join user_tab_comments c on a.TABLE_NAME=c.TABLE_NAME
                            where a.table_name ='{0}'
                            order by a.COLUMN_ID ";

            var dtData = DBHepler.OracleHelper.ExecuteDataTable(string.Format(sql, tableName.Trim()));
            var list = UtilHelper.ModelFromDBHelper.GetFieldsFormDB(dtData);
            var code = UtilHelper.ModelFromDBHelper.GenerateCode(list, cbLineDeal.Checked);
            return code;
        }

        private string GetCodeForSqlServer(string tableName)
        {
            string sql = @" select a.name table_name,b.value table_comments,c.name column_name,e.name data_type,d.value column_comments,
                                    IIF(c.is_nullable=1,'Y','N') nullable
                                from sys.tables a 
                                left join sys.extended_properties b on a.object_id=b.major_id and b.minor_id=0
                                left join sys.columns c on a.object_id=c.object_id
                                left join sys.extended_properties d on d.major_id=c.object_id and d.minor_id=c.column_id
                                left join sys.systypes e on c.system_type_id=e.xtype and e.xtype=e.xusertype
                                where a.name='{0}'
                                order by c.column_id ";

            var dtData = DBHepler.SQLHelper.ExecuteDataTable(string.Format(sql, tableName));
            var list = UtilHelper.ModelFromDBHelper.GetFieldsFormDB(dtData);
            var code = UtilHelper.ModelFromDBHelper.GenerateCode(list, cbLineDeal.Checked, cbDisplayName.Checked, tbNameSpace.Text.Trim());
            return code;
        }


        private string GetCodeForMySQL(string tableName)
        {
            string sql = @" select b.table_name,b.table_comment table_comments,a.column_name,a.data_type,a.column_comment column_comments,if(a.is_Nullable='YES','Y','N') nullable
                            from information_schema.columns a inner join information_schema.tables b on a.table_name=b.table_name and a.table_schema=b.table_schema
                            where b.table_schema=@DataBase and b.table_name='{0}'
                            order by a.ORDINAL_POSITION ";

            var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(string.Format(sql, tableName));
            var list = UtilHelper.ModelFromDBHelper.GetFieldsFormDB(dtData);
            var code = UtilHelper.ModelFromDBHelper.GenerateCode(list, cbLineDeal.Checked, cbDisplayName.Checked, tbNameSpace.Text.Trim());
            return code;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbDBType.Text == "ORACLE")
                {
                    string sql = "select table_name from user_tables ";
                    var dtData = DBHepler.OracleHelper.ExecuteDataTable(sql);
                    cbTableName.DataSource = dtData;
                    cbTableName.DisplayMember = "table_name";
                    cbTableName.ValueMember = "table_name";


                }
                else if (cbDBType.Text == "SQLSERVER")
                {
                    string sql = "select name table_name from sys.tables ";
                    var dtData = DBHepler.SQLHelper.ExecuteDataTable(sql);
                    cbTableName.DataSource = dtData;
                    cbTableName.DisplayMember = "table_name";
                    cbTableName.ValueMember = "table_name";
                }
                else if (cbDBType.Text == "MySQL")
                {
                    string sql = "select table_name from information_schema.tables where table_schema=@DataBase ";
                    var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(sql);
                    cbTableName.DataSource = dtData;
                    cbTableName.DisplayMember = "table_name";
                    cbTableName.ValueMember = "table_name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DBTaleForm_Load(object sender, EventArgs e)
        {
            cbDBType.SelectedIndex = 0;
            folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void cbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbDBType.SelectedIndex <= 0)
            {
                cbDBType.Focus();
                MessageBox.Show("数据库类型未选择！");
            }
            if (cbTableName.SelectedIndex < 0)
            {
                cbTableName.Focus();
                MessageBox.Show("数据库表未选择！");
            }

            string dirPath = string.Empty;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                dirPath = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                return;
            }

            List<string> listTables = new List<string>();
            foreach (var item in cbTableName.CheckedItems)
            {
                listTables.Add(((DataRowView)item)["table_name"]?.ToString());
            }

            string tableName = string.Empty;
            #region CreateCodeText

            if (cbDBType.Text == "ORACLE")
            {
                foreach (var item in listTables)
                {
                    var codeOracle = GetCodeForOracle(item);
                    using (StreamWriter sw = new StreamWriter($"{dirPath}\\{item}.cs", false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(codeOracle);
                    }
                }
            }
            else if (cbDBType.Text == "SQLSERVER")
            {
                foreach (var item in listTables)
                {
                    var codeSqlServer = GetCodeForSqlServer(item);
                    using (StreamWriter sw = new StreamWriter($"{dirPath}\\{item}.cs", false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(codeSqlServer);
                    }
                }
            }
            else if (cbDBType.Text == "MySQL")
            {
                foreach (var item in listTables)
                {
                    var codeMySQL = GetCodeForMySQL(item);
                    using (StreamWriter sw = new StreamWriter($"{dirPath}\\{item}.cs", false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(codeMySQL);
                    }
                }
            }
            #endregion

            MessageBox.Show("文件保存成功！");
        }

        private void btnOpenPath2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/root," + folderBrowserDialog1.SelectedPath;
            System.Diagnostics.Process.Start(psi);
        }


        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTableName.Items.Count == 0)
            {
                return;
            }
            for (int i = 0; i < cbTableName.Items.Count; i++)
            {
                cbTableName.SetItemChecked(i, cbSelectAll.Checked);
            }
        }
    }
}
