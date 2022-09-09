﻿using DBHepler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public partial class OracleQueryHelperForm : Form
    {
        public string CodeText { get; set; }
        public OracleQueryHelperForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0 || txtSQL.Text.Trim().Length == 0)
                {
                    return;
                }
                var sql = txtSQL.Text.Trim().Trim(';');
                List<OracleParameter> paramCols = new List<OracleParameter>();
                foreach (DataGridViewRow drItem in dataGridView1.Rows)
                {
                    if (string.IsNullOrWhiteSpace(drItem.Cells["ParamName"].Value?.ToString()))
                    {
                        continue;
                    }
                    var paramItem = new OracleParameter();
                    var dbTypeString = drItem.Cells["ParamType"].Value?.ToString();
                    paramItem.ParameterName = $":{drItem.Cells["ParamName"].Value}";
                    var val = drItem.Cells["ParamValue"].Value;
                    if (val == null)
                    {
                        paramItem.Value = DBNull.Value;
                        paramCols.Add(paramItem);
                        continue;
                    }
                    DbType dbType = DbType.String;
                    switch (dbTypeString)
                    {
                        case "DateTime":
                            dbType = DbType.DateTime;
                            paramItem.Value = Convert.ToDateTime(val?.ToString());
                            break;
                        case "Varchar":
                            dbType = DbType.String;
                            paramItem.Value = val?.ToString();
                            break;
                        case "Varchar2":
                            dbType = DbType.String;
                            paramItem.Value = val?.ToString();
                            break;
                        case "Number":
                            dbType = DbType.Int32;
                            paramItem.Value = Convert.ToInt32(val?.ToString());
                            break;
                        default:
                            break;
                    }
                    paramItem.DbType = dbType;
                    paramCols.Add(paramItem);
                }

                //OracleParameter[] paramCols = new OracleParameter[] {
                //    new OracleParameter(":param_StartDate",OracleType.DateTime),
                //    new OracleParameter(":param_EndDate",OracleType.DateTime)
                //};
                //paramCols[0].Value = new DateTime(2022, 9, 7, 0, 0, 0);
                //paramCols[1].Value = new DateTime(2022, 9, 7, 23, 59, 59);

                txtSQL.Text = sql;
                var dtData = OracleHelper.ExecuteDataTable(sql, paramCols.ToArray());
                dataGridViewResult.DataSource = dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void OracleQueryHelperForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnOk_Click(null, null);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataViewColumn = dataGridView1.Columns[e.ColumnIndex];
            if (dataViewColumn is DataGridViewButtonColumn)
            {
                var dataRow = dataGridView1.Rows[e.RowIndex];
                var paramName = dataRow.Cells["ParamName"].Value?.ToString();
                if (txtSQL.Text.Trim().Length > 0)
                {
                    txtSQL.Text = txtSQL.Text.Replace(paramName, ":" + paramName);
                }
            }
        }

        private void btnParam_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows == null || dataGridView1.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow dataRow in dataGridView1.Rows)
            {
                var paramName = dataRow.Cells["ParamName"].Value?.ToString();
                txtSQL.Text = txtSQL.Text.Replace(paramName, ":" + paramName);
            }
        }
    }
}
