using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            #region CreateCodeText

            if (cbDBType.Text == "ORACLE")
            {
                string sql = @"select a.TABLE_NAME,c.COMMENTS as table_comments, a.column_name,a.DATA_TYPE,b.Comments as column_comments,a.NULLABLE
                            from user_tab_columns a 
                            left join user_col_comments b on a.TABLE_NAME=b.TABLE_NAME and a.COLUMN_NAME = b.column_name
                            left join user_tab_comments c on a.TABLE_NAME=c.TABLE_NAME
                            where a.table_name ='{0}'
                            order by a.COLUMN_ID ";

                var dtData = DBHepler.OracleHelper.ExecuteDataTable(string.Format(sql, cbTableName.Text.Trim()));
                var list = UtilHelper.ModelFromDBHelper.GetFieldsFormDB(dtData);
                var code = UtilHelper.ModelFromDBHelper.GenerateCode(list, cbLineDeal.Checked);
                this.CodeText = code;
            }
            else if (cbDBType.Text == "SQLSERVER")
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

                var dtData = DBHepler.SQLHelper.ExecuteDataTable(string.Format(sql, cbTableName.Text.Trim()));
                var list = UtilHelper.ModelFromDBHelper.GetFieldsFormDB(dtData);
                var code = UtilHelper.ModelFromDBHelper.GenerateCode(list, cbLineDeal.Checked, cbDisplayName.Checked);
                this.CodeText = code;
            }

            #endregion

            this.DialogResult = DialogResult.OK;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DBTaleForm_Load(object sender, EventArgs e)
        {
            cbDBType.SelectedIndex = 0;
        }

        private void cbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
