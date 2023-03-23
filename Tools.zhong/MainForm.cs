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
using Tools.zhong.UtilHelper;
using Tools.zhong.Model;
using DBHepler;

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

        private string lastText;

        private DbTableForm subForm;

        private ModelGeneratorForm mgForm;

        //    txtOutput.Text = subForm.CodeText;
        //    tabControl1.SelectedIndex = 4;

        public TextBox TextOutPut => txtOutput;
        public TabControl TabControl => tabControl1;

        /// <summary>
        /// 标记是否加载的视图
        /// </summary>
        public bool ViewFlag { get; set; }
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
            tabControl1.SelectedIndex = 4;
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

            var saveDefaultPath = ConfigHelper.GetValue("SaveDefaultPath");
            saveFileDialog1.InitialDirectory = string.IsNullOrWhiteSpace(saveDefaultPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : saveDefaultPath;
            cbEncodeType.SelectedIndex = 0;

            cbLikeType.Items.Add(new ListItem("LIKE", "LIKE"));
            cbLikeType.Items.Add(new ListItem("NOT LIKE", "NOT LIKE"));
            cbLikeType.Items.Add(new ListItem("=", "="));
            cbLikeType.DisplayMember = "Text";
            cbLikeType.ValueMember = "Value";
            cbLikeType.SelectedIndex = 0;

            //加载数据库类型
            string[] dbTypes = Enum.GetNames(typeof(DataBaseType));
            cbDBType.Items.Clear();
            cbDBType.DataSource = dbTypes;
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
            if (subForm != null)
            {
                if (subForm.IsDisposed)
                    subForm = new DbTableForm(this, cbLikeType.Text, txtTableFilter.Text);//如果已经销毁，则重新创建子窗口对象
                subForm.Show();
                subForm.Focus();
            }
            else
            {
                subForm = new DbTableForm(this, cbLikeType.Text, txtTableFilter.Text);
                subForm.Show();
                subForm.Focus();
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
            inputVals = inputVals.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "")
                    .Replace(",", "")
                    .Replace("，", "")
                    .Replace(";", "")
                    .Trim();
                if (string.IsNullOrWhiteSpace(inputItem))
                {
                    continue;
                }
                sbColumns.Append(string.Concat(i == 0 ? "" : " ,", inputItem,
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine + "" : ""));
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
            inputVals = inputVals.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "")
                    .Replace(",", "")
                    .Replace("，", "")
                    .Replace(";", "")
                    .Trim();
                if (string.IsNullOrWhiteSpace(inputItem))
                {
                    continue;
                }
                var newLineFlag = (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 && rowsPerCount != inputVals.Length;
                sbColumns.Append(string.Concat(i == 0 ? " " : " ,",
                    inputItem, newLineFlag ? System.Environment.NewLine : ""));

                sbInsertParamColumns.Append(string.Concat(i == 0 ? "" : ",", SQL_PARAM_PREFIX, inputItem.TrimStart(),
                    newLineFlag ? System.Environment.NewLine : ""));
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
            if (inputText.IndexOf(",") != -1 && inputText.IndexOf(System.Environment.NewLine) > 0)
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
            inputVals = inputVals.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "")
                    .Replace(",", "")
                    .Replace("，", "")
                    .Replace(";", "")
                    .Trim();
                if (string.IsNullOrWhiteSpace(inputItem))
                {
                    continue;
                }
                var newLineFlag = (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 && rowsPerCount != inputVals.Length;
                sbUpdateColumns.Append(string.Concat(i == 0 ? "" : ", ",
                    inputItem, " = ", SQL_PARAM_PREFIX,
                    inputItem.TrimStart(), newLineFlag ? System.Environment.NewLine + "\t" : ""));
            }

            string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            keys = keys.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                var inputItem = keys[i].Replace(System.Environment.NewLine, "")
                    .Replace(",", "")
                    .Replace("，", "")
                    .Replace(";", "")
                    .Trim();
                if (string.IsNullOrWhiteSpace(inputItem))
                {
                    continue;
                }
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
            keys = keys.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                var inputItem = keys[i].Replace(System.Environment.NewLine, "")
                    .Replace(",", "")
                    .Replace("，", "")
                    .Replace(";", "")
                    .Trim();
                if (string.IsNullOrWhiteSpace(inputItem))
                {
                    continue;
                }
                sbKeyColumns.Append(string.Concat(i == 0 ? "" : " AND ",
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
                ViewFlag = false;
                string tableFilter = txtTableFilter.Text.Trim();
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                var dtData = DbObjectHelper.GetDataBaseTables(dbType, tableFilter, cbLikeType.Text);
                txtTableName3.DataSource = dtData;
                txtTableName3.DisplayMember = "table_name";
                txtTableName3.ValueMember = "table_name";
                cblTableLists.Items.Clear();
                if (dtData != null)
                {
                    foreach (DataRow drItem in dtData.Rows)
                    {
                        cblTableLists.Items.Add(drItem["table_name"]);
                    }
                }

                btnLoadFromDB.ForeColor = Color.Red;
                btnLoadView.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnLoadView_Click(object sender, EventArgs e)
        {
            try
            {
                ViewFlag = true;
                string tableFilter = txtTableFilter.Text.Trim();
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                var dtData = DbObjectHelper.GetDataBaseViews(dbType, tableFilter);
                txtTableName3.DataSource = dtData;
                txtTableName3.DisplayMember = "table_name";
                txtTableName3.ValueMember = "table_name";
                cblTableLists.Items.Clear();
                if (dtData != null)
                {
                    foreach (DataRow drItem in dtData.Rows)
                    {
                        cblTableLists.Items.Add(drItem["table_name"]);
                    }
                }
                btnLoadFromDB.ForeColor = Color.Black;
                btnLoadView.ForeColor = Color.Red;
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
                string tableName = txtTableName3.Text.Trim();
                string tableInfoTmpl = "表名：{0} | 创建时间：{1} | 修改时间：{2}";
                var tableInfoModel = new TableModel();
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                if (dbType == DataBaseType.ORACLE)
                {
                    var colList = DbObjectHelper.GetColumnsForOracle(tableName);
                    txtInput3.Text = string.Join(",", colList.Select(i => i.FieldName));
                    txtKey3.Text = string.Join(",", DbObjectHelper.GetOracleTablePrimaryKey(tableName));
                    tableInfoModel = DbObjectHelper.GetOracleTableInfo(tableName);
                }
                if (dbType == DataBaseType.SQLSERVER)
                {
                    var colList = DbObjectHelper.GetColumnsForSqlServer(tableName, ViewFlag);
                    txtInput3.Text = string.Join(",", colList.Select(i => i.FieldName));
                    txtKey3.Text = string.Join(",", DbObjectHelper.GetSqlServerTablePrimaryKey(tableName));
                    tableInfoModel = DbObjectHelper.GetSqlServerTableInfo(tableName, ViewFlag);
                }
                if (dbType == DataBaseType.MySQL)
                {
                    var dbName = DbObjectHelper.GetDataBaseName(dbType);
                    var colList = DbObjectHelper.GetColumnsForMySQL(dbName, tableName);
                    txtInput3.Text = string.Join(",", colList.Select(i => i.FieldName));
                    txtKey3.Text = string.Join(",", DbObjectHelper.GetMySqlTablePrimaryKey(dbName, tableName));
                    tableInfoModel = DbObjectHelper.GetMySqlTableInfo(dbName, tableName);
                }

                lblTableInfo.Text = string.Format(tableInfoTmpl, tableInfoModel.TableName,
                    tableInfoModel.CreateDate?.ToString("yyyy-MM-dd HH:mm:ss"), tableInfoModel.LastUpdateDate?.ToString("yyyy-MM-dd HH:mm:ss"));
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
            tabControl1.SelectedIndex = 4;
        }

        private void btnCommaToBlank_Click(object sender, EventArgs e)
        {

        }

        private void btnNoNewLine3_Click(object sender, EventArgs e)
        {
            txtPerColNum.Text = "-1";
            txtPerColNum.Enabled = false;
        }

        private void btnDefaultNewLine3_Click(object sender, EventArgs e)
        {
            txtPerColNum.Text = "8";
            txtPerColNum.Enabled = true;
        }

        #endregion

        #region 快捷替换操作

        private void tsmAddDyh_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.ToList().Select(i => i.Trim()).ToArray();
            if (inputTexts.Length == 0)
            {
                return;
            }
            txtTempl.Text = "'" + string.Join("','", inputTexts) + "'";
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmDelDyh_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (inputTexts.Length == 0)
            {
                return;
            }
            inputTexts = inputTexts.ToList().Select(i => i.Trim('\'')).ToArray();
            txtTempl.Text = string.Join(",", inputTexts);
        }

        private void tsmAddSyh_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.ToList().Select(i => i.Trim()).ToArray();
            if (inputTexts.Length == 0)
            {
                return;
            }
            txtTempl.Text = "\"" + string.Join("\",\"", inputTexts) + "\"";
        }

        private void tsmDyhzy_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            txtTempl.Text = "\'" + ReplaceSpecialCharSQL(templ) + "\'";
        }
        private string ReplaceSpecialCharSQL(string srcString, char replaceChar = '\'')
        {
            if (string.IsNullOrWhiteSpace(srcString))
            {
                return srcString;
            }
            char[] specialChars = new char[] { '\'', '\"' };
            foreach (var item in specialChars)
            {
                srcString = srcString.Replace(item.ToString(), replaceChar.ToString() + item);
            }
            return srcString;
        }

        private void tsmSyhZy_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            txtTempl.Text = "\"" + ReplaceSpecialChar(templ) + "\"";
        }

        private string ReplaceSpecialChar(string srcString, char replaceChar = '\\')
        {
            if (string.IsNullOrWhiteSpace(srcString))
            {
                return srcString;
            }
            char[] specialChars = new char[] { '\'', '\"', '\\' };
            foreach (var item in specialChars)
            {
                srcString = srcString.Replace(item.ToString(), replaceChar.ToString() + item);
            }
            return srcString;
        }

        private void tsmDelSyh_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (inputTexts.Length == 0)
            {
                return;
            }
            inputTexts = inputTexts.ToList().Select(i => i.Trim('\"')).ToArray();
            txtTempl.Text = string.Join(",", inputTexts);
        }

        private void tsmKg2Dh_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, " ").Replace("\t", " ");
            var inputTexts = templ.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            txtTempl.Text = string.Join(",\t", inputTexts);
        }

        private void tsmDh2Hh_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.Select(i => i.Trim()).ToArray();
            txtTempl.Text = string.Join(System.Environment.NewLine, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmReplaceLine_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim());
            txtTempl.Text = string.Join(",", inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmNewLine2DyhIn_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, ",");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim());
            txtTempl.Text = "'" + string.Join("','", inputTexts) + "'";
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmAddComma_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim());
            txtTempl.Text = string.Join("," + System.Environment.NewLine, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmDelComma_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim().TrimEnd(','));
            txtTempl.Text = string.Join(System.Environment.NewLine, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmCustomLine_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            PerNewLineForm frm = new PerNewLineForm(txtTempl.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtTempl.Text = frm.InputText;
                //tabControl1.SelectedIndex = 1;
            }
        }

        private void tsmTrim_Click(object sender, EventArgs e)
        {
            lastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim().TrimEnd(','));
            txtTempl.Text = string.Join(System.Environment.NewLine, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmRedo_Click(object sender, EventArgs e)
        {
            //txtTempl.Text = lastText;
        }

        #endregion

        private void btnCreateModelByInput_Click(object sender, EventArgs e)
        {
            if (mgForm != null)
            {
                if (mgForm.IsDisposed)
                    mgForm = new ModelGeneratorForm(this);//如果已经销毁，则重新创建子窗口对象
                mgForm.Show();
                mgForm.Focus();
            }
            else
            {
                mgForm = new ModelGeneratorForm(this);
                mgForm.Show();
                mgForm.Focus();
            }
        }

        private void btnOracleQueryHelper_Click(object sender, EventArgs e)
        {
            OracleQueryHelperForm frm = new OracleQueryHelperForm();
            frm.Show();
        }

        #region 加密解密
        private void btnEncode_Click(object sender, EventArgs e)
        {
            txtOutput4.Text = cbEncodeType.SelectedIndex == 0
                ? DESUtil.DESEncrypt(txtInput4.Text.Trim(), txtKey4.Text.Trim())
                : Base64Util.EncodeBase64(txtInput4.Text.Trim());
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput4.Text = cbEncodeType.SelectedIndex == 0
               ? DESUtil.DESDecrypt(txtInput4.Text.Trim(), txtKey4.Text.Trim())
               : Base64Util.DecodeBase64(txtInput4.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        private void btnCopyToInput_Click(object sender, EventArgs e)
        {
            txtTempl.Text = txtOutput.Text;
            tabControl1.SelectedIndex = 0;
        }

        private void btnUpdateConfig_Click(object sender, EventArgs e)
        {
            UpdateAppkeyForm frm = new UpdateAppkeyForm();
            frm.Show();
        }

        private void btnImportFromInput_Click(object sender, EventArgs e)
        {
            try
            {
                var templ = txtTempl.Text.Trim();
                var inputTexts = templ.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim()).ToList();
                foreach (var textItem in inputTexts)
                {
                    var drNew = dt.NewRow();
                    var colItem = textItem.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim())?.ToList();
                    for (int i = 0; i < colItem.Count(); i++)
                    {
                        if (!dt.Columns.Contains($"col{i}"))
                        {
                            dt.Columns.Add($"col{i}");
                        }
                        drNew[$"col{i}"] = colItem[i];
                    }
                    dt.Rows.Add(drNew);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 导出数据到excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportData_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = txtInput3.Text.Trim();
                DataTable dtData = new DataTable();
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                if (dbType == DataBaseType.ORACLE)
                {
                    dtData = OracleHelper.ExecuteDataTable(sql);
                }
                else if (dbType == DataBaseType.SQLSERVER)
                {
                    dtData = DBHepler.SQLHelper.ExecuteDataTable(sql);
                }
                else if (dbType == DataBaseType.MySQL)
                {
                    dtData = DBHepler.MySQLHelper.ExecuteDataTable(sql);
                }
                #region ob...
                //var resultText = string.Empty;
                //StringBuilder sb = new StringBuilder();
                //if (dtData != null)
                //{
                //    foreach (DataRow dataRow in dtData.Rows)
                //    {
                //        var drData = dataRow[0];
                //        string drVal = string.Empty;
                //        if (drData is byte[])
                //        {
                //            drVal = System.Text.Encoding.Default.GetString(drData as byte[]);
                //        }
                //        else
                //        {
                //            drVal = drData?.ToString();
                //        }
                //        sb.AppendLine(drVal);
                //    }
                //}
                //txtOuput3.Text = sb.ToString();
                #endregion

                saveFileDialog1.Filter = "Excel(*.xlsx)|*.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    var wookbook = ExcelUtil.ToExcel(dtData, System.IO.Path.GetFileName(filePath));
                    using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.CreateNew))
                    {
                        wookbook.Write(fs);
                    }
                    MessageBox.Show("保存成功！");
                }
                saveFileDialog1.Filter = "All files(*.*)|*.*";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 生成数据字典（单表)
        /// </summary>
        private void btnCreateDicSingleTable_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableName3.Text))
            {
                MessageBox.Show("请指定表名！");
            }
            try
            {

                saveFileDialog1.Filter = "Word(*.docx)|*.docx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    List<TableColumnModel> data = null;

                    var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                    string dataBaseName = DbObjectHelper.GetDataBaseName(dbType);
                    if (dbType == DataBaseType.ORACLE)
                    {
                        data = DbObjectHelper.GetColumnsForOracle(txtTableName3.Text.Trim());
                    }
                    else if (dbType == DataBaseType.SQLSERVER)
                    {
                        data = DbObjectHelper.GetColumnsForSqlServer(txtTableName3.Text.Trim());
                    }
                    else if (dbType == DataBaseType.MySQL)
                    {
                        data = DbObjectHelper.GetColumnsForMySQL(dataBaseName, txtTableName3.Text.Trim());
                    }
                    dataBaseName = txtDocxTitle.Text.Trim().Length > 0 ? txtDocxTitle.Text.Trim() : dataBaseName;
                    DocxHelper.GenerateDocxByTable(filePath, dataBaseName, data);
                    MessageBox.Show("生成成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                saveFileDialog1.Filter = "All files(*.*)|*.*";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 生成数据字典（数据库)
        /// </summary>
        private void btnCreateDicAllDB_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Word(*.docx)|*.docx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    List<List<TableColumnModel>> lists = new List<List<TableColumnModel>>();

                    var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                    string dataBaseName = DbObjectHelper.GetDataBaseName(dbType);
                    var dtTables = DbObjectHelper.GetDataBaseTables(dbType);
                    if (dtTables == null || dtTables.Rows.Count == 0)
                    {
                        MessageBox.Show("未能查到相关数据表！");
                    }
                    foreach (DataRow drItem in dtTables.Rows)
                    {
                        string tableName = drItem.Field<string>("table_name");
                        if (dbType == DataBaseType.ORACLE)
                        {
                            var list = DbObjectHelper.GetColumnsForOracle(tableName);
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.SQLSERVER)
                        {
                            var list = DbObjectHelper.GetColumnsForSqlServer(tableName);
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.MySQL)
                        {
                            var list = DbObjectHelper.GetColumnsForMySQL(dataBaseName, tableName);
                            lists.Add(list);
                        }
                    }

                    DocxHelper.GenerateDocxByTables(filePath, txtDocxTitle.Text.Trim(), lists);
                    MessageBox.Show("生成成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                saveFileDialog1.Filter = "All files(*.*)|*.*";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExportDocxTables_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Word(*.docx)|*.docx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    List<List<TableColumnModel>> lists = new List<List<TableColumnModel>>();

                    var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                    string dataBaseName = DbObjectHelper.GetDataBaseName(dbType);

                    if (cblTableLists.CheckedItems == null || cblTableLists.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("未能查到相关数据表！");
                        return;
                    }

                    foreach (var item in cblTableLists.CheckedItems)
                    {
                        string tableName = item?.ToString();
                        if (dbType == DataBaseType.ORACLE)
                        {
                            var list = DbObjectHelper.GetColumnsForOracle(tableName);
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.SQLSERVER)
                        {
                            var list = DbObjectHelper.GetColumnsForSqlServer(tableName);
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.MySQL)
                        {
                            var list = DbObjectHelper.GetColumnsForMySQL(dataBaseName, tableName);
                            lists.Add(list);
                        }
                    }

                    DocxHelper.GenerateDocxByTables(filePath, txtDocxTitle.Text.Trim(), lists);
                    MessageBox.Show("生成成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                saveFileDialog1.Filter = "All files(*.*)|*.*";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (subForm != null)
            {
                subForm.Dispose();
            }
            if (mgForm != null)
            {
                mgForm.Dispose();
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblTableLists.Items.Count; i++)
            {
                cblTableLists.SetItemChecked(i, true);
            }
        }

        private void btnCancelSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblTableLists.Items.Count; i++)
            {
                cblTableLists.SetItemChecked(i, false);
            }
        }


    }
}