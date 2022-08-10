using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.IO;

namespace Tools.zhong
{
    public partial class MainForm : Form
    {
        #region 属性变量

        private const string SELECT_TEMPLATE = @"SELECT {#COLUMNS} {#LINE_SPLIT}FROM {#TABLE_NAME} {#LINE_SPLIT}WHERE 1=1 ";

        private const string INSERT_TEMPLATE = @"INSERT INTO {#TABLE_NAME} {#LINE_SPLIT}({#COLUMNS}){#LINE_SPLIT}VALUES{#LINE_SPLIT}({#IPARAMS})";

        private const string UPDATE_TEMPLATE = @"UPDATE {#TABLE_NAME} {#LINE_SPLIT}SET {#UPARAMS} {#LINE_SPLIT}WHERE {#KEYPARAMS}";

        private const string DELETE_TEMPLATE = @"DELETE FROM {#TABLE_NAME} WHERE {#KEYPARAMS}";

        //SQL脚本参数前辍
        private string SQL_PARAM_PREFIX = "@";

        private DataTable dt;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        #region 代码转换工具

        private void btnOutput_Click(object sender, EventArgs e)
        {
            var templ = txtTempl.Text.Trim();
            StringBuilder sbOutput = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                var outputLine = templ;
                int i = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    outputLine = outputLine.Replace($"${i}", item[column.ColumnName].ToString());
                    i++;
                }
                sbOutput.AppendLine(outputLine);
            }

            txtOutput.Text = sbOutput.ToString();
            tabControl1.SelectedIndex = 1;
        }

        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            var cols = dt.Columns.Count;
            dt.Columns.Add($"col{cols}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (dt == null)
            {
                dt = new DataTable();
            }
            dataGridView1.DataSource = dt;

            if (cbDBType.Items.Count > 0)
            {
                cbDBType.SelectedIndex = 0;
            }

            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTempl.Clear();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            dataGridView1.DataSource = dt;
        }

        private void btnRemoveCol_Click(object sender, EventArgs e)
        {
            if (dt.Columns.Count > 0)
            {
                dt.Columns.RemoveAt(dt.Columns.Count - 1);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportForm importForm = new ImportForm();
            if (importForm.ShowDialog() == DialogResult.OK)
            {
                string code = importForm.CodeText;
                Regex regex = new Regex(@"\s*public\s+\w+\s(\w+)\w*\s+\{");

                var matches = regex.Matches(code);
                var cols = new List<string>();
                if (matches != null && matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        var gls = matches[i].Groups;
                        if (gls != null && gls.Count > 0)
                        {
                            cols.Add(gls[1].Value);
                        }
                    }
                }

                if (cols.Count > 0)
                {
                    var colName = $"col{dt.Columns.Count}";
                    dt.Columns.Add(colName);
                    int j = 0;
                    int i = dt.Rows.Count;
                    foreach (var colItem in cols)
                    {
                        DataRow dr = j < i ? dt.Rows[j] : dt.NewRow();
                        dr[colName] = colItem;
                        if (j >= i)
                        {
                            dt.Rows.Add(dr);
                        }
                        j++;
                    }
                }

            }
        }

        private void btnCreateModelFromDBScript_Click(object sender, EventArgs e)
        {
            DBTaleForm subForm = new DBTaleForm();
            if (subForm.ShowDialog() == DialogResult.OK)
            {
                txtOutput.Text = subForm.CodeText;
                tabControl1.SelectedIndex = 1;
            }
        }

        private void btnExportToFile2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = saveFileDialog1.FileName;
                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(txtOutput.Text.Trim());
                }
            }
        }

        private void btnOpenPath2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/root," + saveFileDialog1.InitialDirectory;
            System.Diagnostics.Process.Start(psi);
        }
        #endregion

        #region SQL辅助工具

        private void btnInput3_Click(object sender, EventArgs e)
        {
            txtTableName3.Text = "";
            txtInput3.Text = "";
        }
        private void btnOutput3_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
        }

        private void btnCreateSelect_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            string inputText = txtInput3.Text.Trim();
            if (inputText.IndexOf(",") == -1 && inputText.IndexOf(System.Environment.NewLine) > 0)
            {
                inputText = inputText.Replace(System.Environment.NewLine, ",");
            }
            string tableName = txtTableName3.Text.Trim();
            string key = txtKey3.Text.Trim();
            int rowsPerCount = int.Parse(txtPerColNum.Text.Trim());
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("表名未填写！");
                txtTableName3.Focus();
                return;
            }
            StringBuilder sbColumns = new StringBuilder();

            string[] inputVals = inputText.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "").Replace(",", "").Replace("，", "").Replace(";", "");
                sbColumns.Append(string.Concat(i == 0 ? "" : ", ", inputItem,
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine + "\t" : ""));
            }

            var outText = SELECT_TEMPLATE.Replace("{#TABLE_NAME}", tableName)
                .Replace("{#COLUMNS}", sbColumns.ToString())
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void btnCreateInsert_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            string inputText = txtInput3.Text.Trim();
            if (inputText.IndexOf(",") == -1 && inputText.IndexOf(System.Environment.NewLine) > 0)
            {
                inputText = inputText.Replace(System.Environment.NewLine, ",");
            }
            string tableName = txtTableName3.Text.Trim();
            string key = txtKey3.Text.Trim();
            int rowsPerCount = int.Parse(txtPerColNum.Text.Trim());
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("表名未填写！");
                txtTableName3.Focus();
                return;
            }
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbInsertParamColumns = new StringBuilder();

            string[] inputVals = inputText.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "").Replace(",", "").Replace("，", "").Replace(";", "");
                sbColumns.Append(string.Concat(i == 0 ? "" : ", ", inputItem,
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine : ""));

                sbInsertParamColumns.Append(string.Concat(i == 0 ? "" : ", ", SQL_PARAM_PREFIX, inputItem.TrimStart(),
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine : ""));
            }

            var outText = INSERT_TEMPLATE.Replace("{#TABLE_NAME}", tableName)
                .Replace("{#COLUMNS}", sbColumns.ToString())
                .Replace("{#IPARAMS}", sbInsertParamColumns.ToString())
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void btnCreateUpdate_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            string inputText = txtInput3.Text.Trim();
            if (inputText.IndexOf(",") == -1 && inputText.IndexOf(System.Environment.NewLine) > 0)
            {
                inputText = inputText.Replace(System.Environment.NewLine, ",");
            }
            string tableName = txtTableName3.Text.Trim();
            string key = txtKey3.Text.Trim();
            int rowsPerCount = int.Parse(txtPerColNum.Text.Trim());
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("表名未填写！");
                txtTableName3.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("主键未填写！");
                txtKey3.Focus();
                return;
            }
            StringBuilder sbUpdateColumns = new StringBuilder();
            StringBuilder sbKeyColumns = new StringBuilder();

            string[] inputVals = inputText.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "").Replace(",", "").Replace("，", "").Replace(";", "");
                sbUpdateColumns.Append(string.Concat(i == 0 ? "" : ", ",
                    inputItem, " = ", SQL_PARAM_PREFIX,
                    inputItem.TrimStart(), (i + 1) % rowsPerCount == 0 ? System.Environment.NewLine + "\t" : ""));
            }

            string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < keys.Length; i++)
            {
                var inputItem = keys[i].Replace(System.Environment.NewLine, "").Replace(",", "").Replace("，", "").Replace(";", "");

                sbKeyColumns.Append(string.Concat(i == 0 ? "" : string.Concat(System.Environment.NewLine, "\tAND "),
                    inputItem, " = ", SQL_PARAM_PREFIX,
                    inputItem.TrimStart()));
            }

            var outText = UPDATE_TEMPLATE.Replace("{#TABLE_NAME}", tableName)
                .Replace("{#UPARAMS}", sbUpdateColumns.ToString())
                .Replace("{#KEYPARAMS}", sbKeyColumns.ToString())
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void btnCreateDelete_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            string tableName = txtTableName3.Text.Trim();
            string key = txtKey3.Text.Trim();
            int rowsPerCount = int.Parse(txtPerColNum.Text.Trim());
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("表名未填写！");
                txtTableName3.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("主键未填写！");
                txtKey3.Focus();
                return;
            }
            StringBuilder sbKeyColumns = new StringBuilder();

            string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < keys.Length; i++)
            {
                var inputItem = keys[i].Replace(System.Environment.NewLine, "").Replace(",", "").Replace("，", "").Replace(";", "");
                sbKeyColumns.Append(string.Concat(i == 0 ? "" : string.Concat(System.Environment.NewLine, "\tAND "),
                    inputItem, " = ", SQL_PARAM_PREFIX,
                    inputItem.TrimStart()));
            }

            var outText = DELETE_TEMPLATE.Replace("{#TABLE_NAME}", tableName)
                .Replace("{#KEYPARAMS}", sbKeyColumns.ToString())
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void txtPerColNum_TextChanged(object sender, EventArgs e)
        {
            int PerColNum = 8;
            if (string.IsNullOrWhiteSpace(txtPerColNum.Text) || !int.TryParse(txtPerColNum.Text, out PerColNum))
            {
                txtPerColNum.Text = PerColNum.ToString();
            }
        }

        private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDBType.Text == "ORACLE")
            {
                SQL_PARAM_PREFIX = ":";
            }
            else
            {
                SQL_PARAM_PREFIX = "@";
            }
        }

        private void btnLoadFromDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDBType.Text == "ORACLE")
                {
                    string sql = "select table_name from user_tables ";
                    var dtData = DBHepler.OracleHelper.ExecuteDataTable(sql);
                    txtTableName3.DataSource = dtData;
                    txtTableName3.DisplayMember = "table_name";
                    txtTableName3.ValueMember = "table_name";
                }
                else if (cbDBType.Text == "SQLSERVER")
                {
                    string sql = "select name table_name from sys.tables ";
                    var dtData = DBHepler.SQLHelper.ExecuteDataTable(sql);
                    txtTableName3.DataSource = dtData;
                    txtTableName3.DisplayMember = "table_name";
                    txtTableName3.ValueMember = "table_name";
                }
                btnLoadFromDB.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCopy3_Click(object sender, EventArgs e)
        {
            if (txtOuput3.SelectedText != "")
            {
                Clipboard.SetDataObject(txtOuput3.SelectedText);
                MessageBox.Show("复制成功");
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                txtInput3.Text = (String)iData.GetData(DataFormats.Text);
            }
        }

        private void txtTableName3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTableName3.Items.Count == 0)
            {
                return;
            }
            try
            {
                if (cbDBType.Text == "ORACLE")
                {
                    string sql = $"select column_name from user_tab_columns where table_name='{txtTableName3.Text.Trim()}' ";
                    var dtData = DBHepler.OracleHelper.ExecuteDataTable(sql);
                    if (dtData != null)
                    {
                        StringBuilder sbColumns = new StringBuilder();
                        foreach (DataRow item in dtData.Rows)
                        {
                            sbColumns.AppendLine(item["column_name"]?.ToString() + ",");
                        }
                        txtInput3.Text = sbColumns.ToString();
                    }

                    string sqlkey = $"select column_name from user_cons_columns cu, user_constraints au " +
                                    $"where cu.constraint_name = au.constraint_name and au.constraint_type = 'P' and au.table_name = '{txtTableName3.Text.Trim()}'";
                    dtData = DBHepler.OracleHelper.ExecuteDataTable(sqlkey);
                    if (dtData != null)
                    {
                        StringBuilder sbColumns = new StringBuilder();
                        foreach (DataRow item in dtData.Rows)
                        {
                            sbColumns.AppendLine(item["column_name"]?.ToString() + ",");
                        }
                        txtKey3.Text = sbColumns.ToString();
                    }
                }
                if (cbDBType.Text == "SQLSERVER")
                {
                    string sql = string.Format(@"select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{0}'", txtTableName3.Text.Trim());
                    var dtData = DBHepler.SQLHelper.ExecuteDataTable(sql);
                    if (dtData != null)
                    {
                        StringBuilder sbColumns = new StringBuilder();
                        foreach (DataRow item in dtData.Rows)
                        {
                            sbColumns.AppendLine(item["column_name"]?.ToString() + ",");
                        }
                        txtInput3.Text = sbColumns.ToString();
                    }

                    string sqlkey = string.Format(@"SELECT cols.name column_name
                                    FROM sys.index_columns indexCols
                                    INNER JOIN sys.columns cols ON indexCols.object_id = cols.object_id AND indexCols.column_id = cols.column_id
                                    INNER JOIN sys.indexes inds ON indexCols.object_id = inds.object_id AND indexCols.index_id = inds.index_id
                                    WHERE indexCols.object_id = OBJECT_ID('{0}', 'u') AND inds.is_primary_key = 1",
                                    txtTableName3.Text.Trim());

                    dtData = DBHepler.SQLHelper.ExecuteDataTable(sqlkey);
                    if (dtData != null)
                    {
                        StringBuilder sbColumns = new StringBuilder();
                        foreach (DataRow item in dtData.Rows)
                        {
                            sbColumns.AppendLine(item["column_name"]?.ToString() + ",");
                        }
                        txtKey3.Text = sbColumns.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnBlankToComma_Click(object sender, EventArgs e)
        {
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, " ").Replace("\t", " ");
            var inputTexts = templ.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            txtOutput.Text = string.Join(",\t", inputTexts);
            tabControl1.SelectedIndex = 1;
        }

        private void btnCommaToBlank_Click(object sender, EventArgs e)
        {
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            txtOutput.Text = string.Join(System.Environment.NewLine, inputTexts);
            tabControl1.SelectedIndex = 1;
        }


        #endregion
       
    }
}
