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

using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.zhong.Component;

namespace Tools.zhong
{
    public partial class MainForm : Form
    {
        #region 属性变量

        private const string SELECT_TEMPLATE = @"SELECT {#COLUMNS} {#LINE_SPLIT}FROM {#TABLE_NAME} {#LINE_SPLIT}WHERE 1=1 ";

        private const string INSERT_TEMPLATE = @"INSERT INTO {#TABLE_NAME} {#LINE_SPLIT}({#COLUMNS}){#LINE_SPLIT}VALUES{#LINE_SPLIT}({#IPARAMS})";

        private const string UPDATE_TEMPLATE = @"UPDATE {#TABLE_NAME} {#LINE_SPLIT}SET {#UPARAMS} {#LINE_SPLIT}WHERE {#KEYPARAMS}";

        private const string DELETE_TEMPLATE = @"DELETE FROM {#TABLE_NAME} WHERE {#KEYPARAMS}";

        private const string RECREATE_TEMPLATE = @"CREATE TABLE {#TABLE_NAME}{#DATE} AS SELECT * FROM {#TABLE_NAME};{#LINE_SPLIT}{#LINE_SPLIT}--NOTE:RECREATE TABLE SCRIPT{#LINE_SPLIT}{#LINE_SPLIT}INSERT INTO {#TABLE_NAME}{#LINE_SPLIT}({#COLUMNS}){#LINE_SPLIT}SELECT {#COLUMNS} {#LINE_SPLIT}FROM {#TABLE_NAME}{#DATE};{#LINE_SPLIT}{#LINE_SPLIT}---DROP TABLE {#TABLE_NAME}{#DATE};{#LINE_SPLIT}";

        private const string ORACLE11_PAGE_TEMPLATE = "SELECT * {#LINE_SPLIT}FROM (\tSELECT rnt.*,ROWNUM RN{#LINE_SPLIT}\t\tFROM (\tSELECT {#COLUMNS} {#LINE_SPLIT}\t\t\t\tFROM {#TABLE_NAME}{#LINE_SPLIT}\t\t\t\tORDER BY {#KEYPARAMS}) rnt{#LINE_SPLIT}\t\tWHERE ROWNUM <= 10){#LINE_SPLIT}WHERE RN >= 0;";

        private const string ORACLE12_PAGE_TEMPLATE = "SELECT {#COLUMNS}{#LINE_SPLIT}FROM {#TABLE_NAME}{#LINE_SPLIT}ORDER BY {#KEYPARAMS}{#LINE_SPLIT}OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY";

        private const string SQLSERVER_PAGE_TEMPLATE = "SELECT {#COLUMNS}{#LINE_SPLIT}FROM {#TABLE_NAME}{#LINE_SPLIT}ORDER BY {#KEYPARAMS}{#LINE_SPLIT}OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY";

        private const string MYSQL_PAGE_TEMPLATE = "SELECT {#COLUMNS}{#LINE_SPLIT}FROM {#TABLE_NAME}{#LINE_SPLIT}ORDER BY {#KEYPARAMS}{#LINE_SPLIT}LIMIT 0,10";

        //SQL脚本参数前辍
        private string SQL_PARAM_PREFIX = "@";

        private DataTable dt;

        private int _HistoryIndex = -1;
        private List<string> _ListHistoryList = new List<string>();
        public string _LastText
        {
            set
            {
                if (_ListHistoryList != null && _ListHistoryList.Count > 0
                    && _ListHistoryList.Contains(value))
                {
                    return;
                }
                _HistoryIndex++;
                _ListHistoryList.Add(value);
            }
            get
            {
                if (_HistoryIndex < 0)
                {
                    return "";
                }
                return _ListHistoryList[_HistoryIndex];
            }
        }
        private DbTableForm subForm;

        private ModelGeneratorForm mgForm;

        //    txtOutput.Text = subForm.CodeText;
        //    tabControl1.SelectedIndex = 1;

        public TextBox TextOutPut => txtOutput;
        public TabControl TabControl => tabControl1;

        /// <summary>
        /// 标记是否加载的视图
        /// </summary>
        public bool ViewFlag { get; set; }

        private string _DefaultSplitChar;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        #region 代码转换工具

        private void btnOutput_Click(object sender, EventArgs e)
        {
            var templ = txtTempl.Text;
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

            var saveDefaultPath = ConfigHelper.GetConfigValue("SaveDefaultPath");
            saveFileDialog1.InitialDirectory = string.IsNullOrWhiteSpace(saveDefaultPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : saveDefaultPath;
            cbEncodeType.SelectedIndex = 0;

            //加载匹配符
            ComboBoxHelper.BindLikeTypeComboBox(cbLikeType);
            //加载数据库类型
            ComboBoxHelper.BindDBTypeComboBox(cbDBType);
            ComboBoxHelper.BindSplitCharComboBox(cbSplitChar);

            dtPicker.Value = DateTime.Now.Date;

            cbSplitChar.SelectedIndex = 0;
            cbSplitChar_SelectedIndexChanged(null, null);

            cbToDateFormat.SelectedIndex = 0;
            cbToolFormat.SelectedIndex = 0;
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
            lblTotalRows.Text = $"共 {(dt != null ? dt.Rows.Count : 0)} 行 |";
        }

        private void btnRemoveCol_Click(object sender, EventArgs e)
        {
            if (dt.Columns.Count > 0)
            {
                dt.Columns.RemoveAt(dt.Columns.Count - 1);
            }
            lblTotalRows.Text = $"共 {(dt != null ? dt.Rows.Count : 0)} 行 |";
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

        #region 生成脚本

        private void btnCreateSelect_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;
            if (string.IsNullOrWhiteSpace(txtTableName3.Text.Trim()))
            {
                MessageBox.Show("表名未填写！");
                txtTableName3.Focus();
                return;
            }

            txtOuput3.Text = CreateSelectSql();
        }

        private string CreateSelectSql()
        {
            string inputText = txtInput3.Text.Trim();
            if (inputText.IndexOf(",") == -1 && inputText.IndexOf(System.Environment.NewLine) > 0)
            {
                inputText = inputText.Replace(System.Environment.NewLine, ",");
            }
            string tableName = txtTableName3.Text.Trim();
            string key = txtKey3.Text.Trim();
            int rowsPerCount = int.Parse(txtPerColNum.Text.Trim());

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

            return outText;
        }

        private void btnCreateInsert_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;

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
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;

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
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;

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

        private void btnMerge_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;

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

            var _MergeSQLTEMPL = "MERGE INTO {#TABLE_NAME} TDESC {#LINE_SPLIT}USING ({#LINE_SPLIT}SELECT {#TSRC_FIELDS}{#LINE_SPLIT}FROM DUAL{#LINE_SPLIT}) TSRC {#LINE_SPLIT}" +
                                 "ON ({#ON_FIELDS}){#LINE_SPLIT}WHEN NOT MATCHED  THEN {#LINE_SPLIT}INSERT({#INSERT_FIELDS}) {#LINE_SPLIT}VALUES ({#INSERT_VALUES_FIELDS}){#LINE_SPLIT}WHEN MATCHED  THEN {#LINE_SPLIT}UPDATE SET {#UPDATE_FIELDS}";


            string[] inputVals = inputText.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            inputVals = inputVals.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();

            StringBuilder TSRC_FIELDS = new StringBuilder();
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
                TSRC_FIELDS.Append(string.Concat(i == 0 ? "" : " ,", $":{inputItem} {inputItem}",
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine + "" : ""));
            }

            StringBuilder ON_FIELDS = new StringBuilder();
            string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            keys = keys.AsEnumerable().Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i.Trim()).ToArray();
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
                ON_FIELDS.Append(string.Concat(i == 0 ? "" : " AND ", $"TDESC.{inputItem}=TSRC.{inputItem}"));
            }


            StringBuilder INSERT_FIELDS = new StringBuilder();
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
                INSERT_FIELDS.Append(string.Concat(i == 0 ? "" : " ,", $"{inputItem}",
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine + "" : ""));
            }

            StringBuilder INSERT_VALUES_FIELDS = new StringBuilder();
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
                INSERT_VALUES_FIELDS.Append(string.Concat(i == 0 ? "" : " ,", $"TSRC.{inputItem}",
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine + "" : ""));
            }

            StringBuilder UPDATE_FIELDS = new StringBuilder();
            for (int i = 0; i < inputVals.Length; i++)
            {
                var inputItem = inputVals[i].Replace(System.Environment.NewLine, "")
                    .Replace(",", "")
                    .Replace("，", "")
                    .Replace(";", "")
                    .Trim();
                if (string.IsNullOrWhiteSpace(inputItem) || keys.Contains(inputItem))
                {
                    continue;
                }
                UPDATE_FIELDS.Append(string.Concat(i == 0 ? "" : " ,", $"TDESC.{inputItem} = TSRC .{inputItem}",
                    (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 ? System.Environment.NewLine + "" : ""));
            }

            var outText = _MergeSQLTEMPL.Replace("{#TABLE_NAME}", tableName)
                .Replace("{#TSRC_FIELDS}", TSRC_FIELDS.ToString())
                .Replace("{#ON_FIELDS}", ON_FIELDS.ToString())
                .Replace("{#INSERT_FIELDS}", INSERT_FIELDS.ToString())
                .Replace("{#INSERT_VALUES_FIELDS}", INSERT_VALUES_FIELDS.ToString())
                .Replace("{#UPDATE_FIELDS}", UPDATE_FIELDS.ToString().Trim().Trim(','))
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void btnReCreate_Click(object sender, EventArgs e)
        {
            txtOuput3.Text = "";
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;

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

            var outText = RECREATE_TEMPLATE
                .Replace("{#DATE}", DateTime.Now.ToString("MMdd"))
                .Replace("{#TABLE_NAME}", tableName)
                .Replace("{#COLUMNS}", sbColumns.ToString())
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void btnPage_Click(object sender, EventArgs e)
        {
            string sqlTemplate = string.Empty;
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;

            var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
            if (dbType == DataBaseType.MySQL)
            {
                sqlTemplate = MYSQL_PAGE_TEMPLATE;
            }
            else if (dbType == DataBaseType.SQLSERVER)
            {
                sqlTemplate = SQLSERVER_PAGE_TEMPLATE;
            }
            else if (dbType == DataBaseType.ORACLE)
            {
                string oraVer = ConfigHelper.GetConfigValue("ORACLE_VERSION");
                if (!string.IsNullOrWhiteSpace(oraVer) && oraVer.Length > 2 && Convert.ToInt32(oraVer.Substring(0, 2)) > 11)
                {
                    sqlTemplate = ORACLE12_PAGE_TEMPLATE;
                }
                else
                {
                    sqlTemplate = ORACLE11_PAGE_TEMPLATE;
                }
            }

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
                var newLineFlag = (i + 1) % rowsPerCount == 0 && rowsPerCount != -1 && rowsPerCount != inputVals.Length;
                sbColumns.Append(string.Concat(i == 0 ? " " : " ,",
                    inputItem, newLineFlag ? System.Environment.NewLine : ""));
            }

            var outText = sqlTemplate
                .Replace("{#TABLE_NAME}", tableName)
                .Replace("{#COLUMNS}", sbColumns.ToString())
                .Replace("{#KEYPARAMS}", txtKey3.Text.Trim())
                .Replace("{#LINE_SPLIT}", System.Environment.NewLine);

            txtOuput3.Text = outText;
        }

        private void btnSqlFields_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            string tableName = txtTableName3.Text.Trim();
            var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
            string dataBaseName = DbObjectHelper.GetDataBaseName(dbType);
            List<TableColumnModel> list = null;
            if (dbType == DataBaseType.ORACLE)
            {
                list = DbObjectHelper.GetColumnsForOracle(tableName);
            }
            else if (dbType == DataBaseType.SQLSERVER)
            {
                list = DbObjectHelper.GetColumnsForSqlServer(tableName);
            }
            else if (dbType == DataBaseType.MySQL)
            {
                list = DbObjectHelper.GetColumnsForMySQL(dataBaseName, tableName);
            }

            if (list != null)
            {
                dt = list.ToDataTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Remove("TableName");
                    dt.Columns.Remove("TableComment");
                }

                dt.Columns.Add("CodeType");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["CodeType"] = DbObjectHelper.ChangeToCsharpType(dr.Field<string>("DataType"));
                }
                dt.Columns["CodeType"].SetOrdinal(5);
            }

            dataGridView1.DataSource = dt;
            tabControl1.SelectedIndex = 0;
        }
        #endregion

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

                txtOuput3.Visible = true;
                dataGridViewQuery.Visible = false;
                if (dtData == null || dtData.Rows.Count == 0)
                {
                    lblTableInfo.Text = $"不存在匹配表或视图";
                }
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

                txtOuput3.Visible = true;
                dataGridViewQuery.Visible = false;
                if (dtData == null || dtData.Rows.Count == 0)
                {
                    lblTableInfo.Text = $"不存在匹配表或视图";
                }
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
                //MessageBox.Show("复制成功");
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
            txtOuput3.Visible = true;
            dataGridViewQuery.Visible = false;
            if (txtTableName3.Items.Count == 0)
            {
                lblTableInfo.Text = $"不存在匹配表或视图";
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
                    if (cbOrderByFieldName.Checked)
                    {
                        colList = colList?.OrderBy(i => i.FieldName)?.ToList();
                    }
                    txtInput3.Text = string.Join(",", colList.Select(i => i.FieldName));
                    txtKey3.Text = string.Join(",", DbObjectHelper.GetOracleTablePrimaryKey(tableName));
                    tableInfoModel = DbObjectHelper.GetOracleTableInfo(tableName);
                }
                if (dbType == DataBaseType.SQLSERVER)
                {
                    var colList = DbObjectHelper.GetColumnsForSqlServer(tableName, ViewFlag);
                    if (cbOrderByFieldName.Checked)
                    {
                        colList = colList?.OrderBy(i => i.FieldName)?.ToList();
                    }
                    txtInput3.Text = string.Join(",", colList.Select(i => i.FieldName));
                    txtKey3.Text = string.Join(",", DbObjectHelper.GetSqlServerTablePrimaryKey(tableName));
                    tableInfoModel = DbObjectHelper.GetSqlServerTableInfo(tableName, ViewFlag);
                }
                if (dbType == DataBaseType.MySQL)
                {
                    var dbName = DbObjectHelper.GetDataBaseName(dbType);
                    var colList = DbObjectHelper.GetColumnsForMySQL(dbName, tableName);
                    if (cbOrderByFieldName.Checked)
                    {
                        colList = colList?.OrderBy(i => i.FieldName)?.ToList();
                    }
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
            tabControl1.SelectedIndex = 1;
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

        #region 下拉菜单操作事件

        private void tsmAddDyh_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.ToList().Select(i => i.Trim()).ToArray();
            if (inputTexts.Length == 0)
            {
                return;
            }
            txtTempl.Text = "'" + string.Join("'" + _DefaultSplitChar + "'", inputTexts) + "'";
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmDelDyh_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            if (inputTexts.Length == 0)
            {
                return;
            }
            inputTexts = inputTexts.ToList().Select(i => i.Trim('\'')).ToArray();
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }

        private void tsmAddSyh_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.ToList().Select(i => i.Trim()).ToArray();
            if (inputTexts.Length == 0)
            {
                return;
            }
            txtTempl.Text = "\"" + string.Join("\"" + _DefaultSplitChar + "\"", inputTexts) + "\"";
        }

        private void tsmDyhzy_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.ToList().Select(i => ReplaceSpecialCharSQL(i.Trim())).ToArray();
            if (inputTexts.Length == 0)
            {
                return;
            }
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }
        private string ReplaceSpecialCharSQL(string srcString, char replaceChar = '\'')
        {
            if (string.IsNullOrWhiteSpace(srcString))
            {
                return srcString;
            }
            srcString = srcString.Replace("\'", "\'\'");
            srcString = srcString.Replace("\"", "\'\'\'\'");
            return srcString;
        }

        private void tsmSyhZy_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.ToList().Select(i => ReplaceSpecialChar(i.Trim())).ToArray();
            if (inputTexts.Length == 0)
            {
                return;
            }
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }

        private string ReplaceSpecialChar(string srcString, char replaceChar = '\\')
        {
            if (string.IsNullOrWhiteSpace(srcString))
            {
                return srcString;
            }
            char[] specialChars = new char[] { '\\', '\'', '\"' };
            foreach (var item in specialChars)
            {
                srcString = srcString.Replace(item.ToString(), replaceChar.ToString() + item);
            }
            return srcString;
        }

        private void tsmDelSyh_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(_DefaultSplitChar, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (inputTexts.Length == 0)
            {
                return;
            }
            inputTexts = inputTexts.ToList().Select(i => i.Trim('\"')).ToArray();
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }

        private void tsmKg2Dh_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, " ").Replace("\t", " ");
            var inputTexts = templ.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            txtTempl.Text = string.Join(",\t", inputTexts);
        }

        private void tsmDh2Hh_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            inputTexts = inputTexts.Select(i => i.Trim()).ToArray();
            txtTempl.Text = string.Join(System.Environment.NewLine, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmReplaceLine_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, "");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i);
            txtTempl.Text = string.Join(",", inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// SQLIN
        /// </summary>
        private void tsmNewLine2DyhIn_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, ",");
            var inputTexts = templ.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim());
            txtTempl.Text = "'" + string.Join("','", inputTexts) + "'";
            //tabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// SQLIN ROLLBACK
        /// </summary>
        private void tsmSqlinRollback_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace(System.Environment.NewLine, ",");
            var inputTexts = templ.Split(new string[] { "','" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim('\''));
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmAddComma_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim());
            txtTempl.Text = string.Join("," + _DefaultSplitChar, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmDelComma_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim().TrimEnd(','));
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmCustomLine_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            PerNewLineForm frm = new PerNewLineForm(txtTempl.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtTempl.Text = frm.InputText;
                //tabControl1.SelectedIndex = 1;
            }
        }

        private void tsmTrim_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim().TrimEnd(','));
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
            //tabControl1.SelectedIndex = 1;
        }

        private void tsmUndo_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            if (_HistoryIndex < 0)
            {
                return;
            }
            _HistoryIndex--;
            txtTempl.Text = _HistoryIndex >= 0 ? _ListHistoryList[_HistoryIndex] : "";
        }

        private void tsmRedo_Click(object sender, EventArgs e)
        {
            _HistoryIndex++;
            txtTempl.Text = _HistoryIndex >= 0 ? _ListHistoryList[_HistoryIndex] : "";
        }

        private void tsmClearHis_Click(object sender, EventArgs e)
        {
            _HistoryIndex = -1;
            _ListHistoryList.Clear();
        }

        /// <summary>
        /// 以逗号分隔，并合并为一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmToOneDHLine_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim(',').Trim() + ",");
            txtTempl.Text = string.Join("", inputTexts);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        private void tsmFirstUpper_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                ?.Select(i => i.Trim())
                ?.Select(i => string.Concat(i.Substring(0, 1).ToUpper(), i.Substring(1)));
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }

        /// <summary>
        /// 全部大写
        /// </summary>
        private void tsmToUpper_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim()?.ToUpper());
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        private void tsmFirstLower_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                ?.Select(i => i.Trim())
                ?.Select(i => string.Concat(i.Substring(0, 1).ToLower(), i.Substring(1)));
            txtTempl.Text = string.Join(_DefaultSplitChar, inputTexts);
        }

        /// <summary>
        /// 全部小写
        /// </summary>
        private void tsmToLower_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim()?.ToLower());
            txtTempl.Text = string.Join(",", inputTexts);
        }

        /// <summary>
        /// 横线换大写字母
        /// </summary>
        private void tsmLineToUpper_Click(object sender, EventArgs e)
        {
            try
            {
                _LastText = txtTempl.Text;
                var templ = txtTempl.Text.Trim();
                var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
                var resultList = new List<string>();
                foreach (var item in inputTexts)
                {
                    var subItems = item.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(k => k.Substring(0, 1).ToUpper() + k.Substring(1));
                    resultList.Add(string.Join("", subItems));
                }
                txtTempl.Text = string.Join(_DefaultSplitChar, resultList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 横线换大写字母
        /// </summary>
        private void tsmToCamel_Click(object sender, EventArgs e)
        {
            try
            {
                _LastText = txtTempl.Text;
                var templ = txtTempl.Text.Trim();
                var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
                var resultList = new List<string>();
                foreach (var item in inputTexts)
                {
                    var subItems = item.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(k => k.Substring(0, 1).ToUpper() + k.Substring(1).ToLower());
                    resultList.Add(string.Join("", subItems));
                }
                txtTempl.Text = string.Join(_DefaultSplitChar, resultList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 大写字母换横线
        /// </summary>
        private void tsmUpperToLine_Click(object sender, EventArgs e)
        {
            try
            {
                _LastText = txtTempl.Text;
                var templ = txtTempl.Text.Trim();
                var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
                var resultList = new List<string>();
                foreach (var item in inputTexts)
                {
                    //typeIDS -> type_ID  PIDGid-->PID_Gid
                    var charArr = item.ToCharArray();
                    var lastUpIndex = 0;
                    var itemList = new List<string>();
                    for (int i = 1; i < charArr.Length; i++)
                    {
                        if (charArr[i - 1].IsBetween('a', 'z') && charArr[i].IsBetween('A', 'Z'))
                        {
                            itemList.Add(item.Substring(lastUpIndex, i - lastUpIndex));
                            lastUpIndex = i;
                        }
                    }
                    if (lastUpIndex >= 0)
                    {
                        itemList.Add(item.Substring(lastUpIndex));
                    }
                    resultList.Add(string.Join("_", itemList));
                }
                txtTempl.Text = string.Join(_DefaultSplitChar, resultList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///单词中间空格换大写字母
        /// </summary>
        private void tsmBlankToUpper_Click(object sender, EventArgs e)
        {
            try
            {
                _LastText = txtTempl.Text;
                var templ = txtTempl.Text.Trim();
                var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries);
                var resultList = new List<string>();
                foreach (var item in inputTexts)
                {
                    var subItems = item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(k => k.Substring(0, 1).ToUpper() + k.Substring(1));
                    resultList.Add(string.Join("", subItems));
                }
                txtTempl.Text = string.Join(_DefaultSplitChar, resultList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiDtS_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace("'", "\"");
            txtTempl.Text = templ;
        }

        private void tsmiStD_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            templ = templ.Replace("\"", "'");
            txtTempl.Text = templ;
        }

        private void tsmReplace_Click(object sender, EventArgs e)
        {
            try
            {
                var rpForm = new ReplaceForm();
                if (rpForm.ShowDialog() == DialogResult.OK)
                {
                    var newText = rpForm.NewText;
                    var oldText = rpForm.OldText;
                    if (string.IsNullOrWhiteSpace(oldText))
                    {
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow drItem in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                var nVal = drItem[i]?.ToString().Replace(oldText, newText);
                                drItem[i] = nVal;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void tsmSplitTrimString_Click(object sender, EventArgs e)
        {
            try
            {
                var dialogForm = new TrimStringForm();
                if (dialogForm.ShowDialog() == DialogResult.OK)
                {
                    _LastText = txtTempl.Text;
                    var splitChar = dialogForm.SplitChar;
                    var trimString = dialogForm.TrimString;
                    var position = dialogForm.Position;
                    var trimBlankFlag = dialogForm.TrimBlankFlag;
                    var trimEmptyLineFlag = dialogForm.TrimEmptyLineFlag;

                    _LastText = txtTempl.Text;
                    var templ = trimBlankFlag ? txtTempl.Text.Trim() : txtTempl.Text;
                    string[] inputTexts = null;
                    inputTexts = templ.Split(new string[] { splitChar }, trimEmptyLineFlag ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

                    var resultList = new List<string>();
                    foreach (var item in inputTexts)
                    {
                        var result = item;
                        if (trimBlankFlag)
                        {
                            result = result.Trim();
                        }
                        switch (position)
                        {
                            case OperatePosition.Before:
                                result = result.TrimStartString(trimString);
                                break;
                            case OperatePosition.After:
                                result = result.TrimEndString(trimString);
                                break;
                            case OperatePosition.Include:
                                result = result.TrimString(trimString);
                                break;
                            default:
                                break;
                        }
                        resultList.Add(result);
                    }
                    txtTempl.Text = string.Join(splitChar, resultList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmSplitInsertString_Click(object sender, EventArgs e)
        {
            try
            {
                var dialogForm = new InsertStringForm();
                if (dialogForm.ShowDialog() == DialogResult.OK)
                {
                    _LastText = txtTempl.Text;
                    var splitChar = dialogForm.SplitChar;
                    var insertString = dialogForm.PrefixString;
                    var position = dialogForm.Position;
                    var trimBlankFlag = dialogForm.TrimBlankFlag;
                    var trimEmptyLineFlag = dialogForm.TrimEmptyLineFlag;

                    _LastText = txtTempl.Text;
                    var templ = trimBlankFlag ? txtTempl.Text.Trim() : txtTempl.Text;
                    string[] inputTexts = null;
                    inputTexts = templ.Split(new string[] { splitChar }, trimEmptyLineFlag ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

                    var resultList = new List<string>();
                    for (int i = 0; i < inputTexts.Length; i++)
                    {
                        var item = inputTexts[i];
                        var prefixBlank = "";
                        var suffixBlank = "";
                        var result = string.Empty;
                        if (!trimBlankFlag)
                        {
                            Match match = Regex.Match(item, @"^\s*");
                            prefixBlank = match.Value;
                            match = Regex.Match(item, @"\s*$");
                            suffixBlank = match.Value;
                        }
                        item = item.Trim();
                        switch (position)
                        {
                            case OperatePosition.Before:
                                result = string.Concat(prefixBlank, insertString, item, suffixBlank);
                                break;
                            case OperatePosition.After:
                                result = string.Concat(prefixBlank, item, insertString, suffixBlank);
                                break;
                            case OperatePosition.Include:
                                result = string.Concat(prefixBlank, insertString, item, insertString, suffixBlank);
                                break;
                            default:
                                break;
                        }
                        resultList.Add(result);
                    }
                    var resultText = string.Join(splitChar, resultList);
                    txtTempl.Text = resultText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmTrimRepeat_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var values = templ.SplitIncludeEmptry(_DefaultSplitChar);
            if (values == null)
            {
                return;
            }
            var groupValues = values.GroupBy(i => i).Select(g => g.Key);
            txtTempl.Text = string.Join(_DefaultSplitChar, groupValues);
        }

        private void tsmStringBuilder_Click(object sender, EventArgs e)
        {
            _LastText = txtTempl.Text;
            var templ = txtTempl.Text.Trim();
            var values = templ.SplitIncludeEmptry(_DefaultSplitChar);
            if (values == null)
            {
                return;
            }
            var listResult = new List<string>();
            listResult.Add("var sbText = new StringBuilder();");
            foreach (var item in values)
            {
                var itemText = item;
                itemText = itemText.Replace("\"", "\\\"");
                listResult.Add($"sbText.AppendLine(\"{itemText}\");");
            }
            listResult.Add("var sbResult = sbText.ToString();");
            txtTempl.Text = string.Join(System.Environment.NewLine, listResult);
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
            try
            {
                //DES  BASE64     MD5
                if (cbEncodeType.Text == "DES")
                {
                    txtOutput4.Text = DESUtil.DESEncrypt(txtInput4.Text.Trim(), txtKey4.Text.Trim());
                }
                else if (cbEncodeType.Text == "BASE64")
                {
                    txtOutput4.Text = Base64Util.EncodeBase64(txtInput4.Text.Trim());
                }
                else if (cbEncodeType.Text == "MD5")
                {
                    txtOutput4.Text = MD5Util.Encrypt(txtInput4.Text.Trim());
                }
                else if (cbEncodeType.Text == "URL")
                {
                    string input = txtInput4.Text.Trim();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        txtOutput4.Text = "";
                        return;
                    }
                    string[] inputItems = input.SplitNoEmpty("/");
                    inputItems = inputItems.Select(i => URLUtil.Encrypt(i)).ToArray();
                    txtOutput4.Text = string.Join("/", inputItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                //DES  BASE64     MD5
                if (cbEncodeType.Text == "DES")
                {
                    txtOutput4.Text = DESUtil.DESDecrypt(txtInput4.Text.Trim(), txtKey4.Text.Trim());
                }
                else if (cbEncodeType.Text == "BASE64")
                {
                    txtOutput4.Text = Base64Util.DecodeBase64(txtInput4.Text.Trim());
                }
                else if (cbEncodeType.Text == "MD5")
                {
                    MessageBox.Show("MD5 does not support decode");
                }
                else if (cbEncodeType.Text == "URL")
                {
                    txtOutput4.Text = URLUtil.Decrypt(txtInput4.Text.Trim());
                }
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
                var inputTexts = templ.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim()).ToList();
                foreach (var textItem in inputTexts)
                {
                    var drNew = dt.NewRow();
                    var colItem = textItem.Split(new string[] { _DefaultSplitChar }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim())?.ToList();
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
            lblTotalRows.Text = $"共 {(dt != null ? dt.Rows.Count : 0)} 行 |";
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
                string sql = CreateSelectSql();
                saveFileDialog1.Filter = "Excel(*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = txtTableName3.Text;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    var dtData = new DataTable();
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
                    var wookbook = ExcelUtil.ToExcel(dtData, System.IO.Path.GetFileName(filePath));
                    using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.CreateNew))
                    {
                        wookbook.Write(fs);
                    }
                    lblTableInfo.Text = $"导出成功,共{dtData.Rows.Count}条数据！";
                }
                saveFileDialog1.Filter = "All files(*.*)|*.*";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 导入表数据
        /// </summary>
        private void btnImportData_Click(object sender, EventArgs e)
        {
            int affectRows = 0;
            try
            {
                string selectCmdText = CreateSelectSql();
                DataTable dtData = new DataTable();
                if (openImportExcelFile.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openImportExcelFile.FileName;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        dtData = ExcelUtil.ToDataTable(fs);
                    }
                    dtData.TableName = txtTableName3.Text;
                    var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                    if (dbType == DataBaseType.ORACLE)
                    {
                        affectRows = OracleHelper.InsertDataTable(dtData, selectCmdText);
                    }
                    else if (dbType == DataBaseType.SQLSERVER)
                    {
                        affectRows = DBHepler.SQLHelper.InsertDataTable(dtData, selectCmdText);
                    }
                    else if (dbType == DataBaseType.MySQL)
                    {
                        affectRows = DBHepler.MySQLHelper.InsertDataTable(dtData, selectCmdText);
                    }

                    lblTableInfo.Text = $"导入成功，共{affectRows}条！";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        private void btnSqlQuery_Click(object sender, EventArgs e)
        {
            txtOuput3.Visible = false;
            dataGridViewQuery.Visible = true;
            try
            {
                string selectCmdText = txtInput3.Text.Trim();
                DataTable dtData = new DataTable();
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                if (dbType == DataBaseType.ORACLE)
                {
                    dtData = OracleHelper.ExecuteDataTable(selectCmdText);
                }
                else if (dbType == DataBaseType.SQLSERVER)
                {
                    dtData = SQLHelper.ExecuteDataTable(selectCmdText);
                }
                else if (dbType == DataBaseType.MySQL)
                {
                    dtData = MySQLHelper.ExecuteDataTable(selectCmdText);
                }

                dataGridViewQuery.DataSource = dtData;
                lblTableInfo.Text = $"查询执行成功，共{dtData.Rows.Count}条！";
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
                    DocxHelper.GenerateDocxByTable(filePath, dataBaseName, data, cbHideNumberCol.Checked);
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
            if (MessageBox.Show("确认要导出所有表到Word文档吗？", "系统提醒", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
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
                            if (cbOrderByFieldName.Checked)
                            {
                                list = list?.OrderBy(i => i.FieldName)?.ToList();
                            }
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.SQLSERVER)
                        {
                            var list = DbObjectHelper.GetColumnsForSqlServer(tableName);
                            if (cbOrderByFieldName.Checked)
                            {
                                list = list?.OrderBy(i => i.FieldName)?.ToList();
                            }
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.MySQL)
                        {
                            var list = DbObjectHelper.GetColumnsForMySQL(dataBaseName, tableName);
                            if (cbOrderByFieldName.Checked)
                            {
                                list = list?.OrderBy(i => i.FieldName)?.ToList();
                            }
                            lists.Add(list);
                        }
                    }

                    DocxHelper.GenerateDocxByTables(filePath, txtDocxTitle.Text.Trim(), lists, cbHideNumberCol.Checked);
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
                            if (cbOrderByFieldName.Checked)
                            {
                                list = list?.OrderBy(i => i.FieldName)?.ToList();
                            }
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.SQLSERVER)
                        {
                            var list = DbObjectHelper.GetColumnsForSqlServer(tableName);
                            if (cbOrderByFieldName.Checked)
                            {
                                list = list?.OrderBy(i => i.FieldName)?.ToList();
                            }
                            lists.Add(list);
                        }
                        else if (dbType == DataBaseType.MySQL)
                        {
                            var list = DbObjectHelper.GetColumnsForMySQL(dataBaseName, tableName);
                            if (cbOrderByFieldName.Checked)
                            {
                                list = list?.OrderBy(i => i.FieldName)?.ToList();
                            }
                            lists.Add(list);
                        }
                    }

                    DocxHelper.GenerateDocxByTables(filePath, txtDocxTitle.Text.Trim(), lists, cbHideNumberCol.Checked);
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

        private void btnRegexMatch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbRegex.Text))
            {
                MessageBox.Show("正则表达式不能为空");
                cbRegex.Focus();
                return;
            }
            var regex = new Regex(cbRegex.Text);
            var matches = regex.Matches(txtTempl.Text);
            var sbResult = new StringBuilder();
            foreach (Match item in matches)
            {
                if (item.Groups.Count == 0)
                {
                    continue;
                }
                var i = 0;
                foreach (Group groupItem in item.Groups)
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    sbResult.AppendLine(groupItem.Value);
                }
            }
            txtOutput.Text = sbResult.ToString();
            tabControl1.SelectedIndex = 1;
        }

        //private void tbDateFormat_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(tbDateFormat.Text))
        //    {
        //        return;
        //    }
        //    dtPicker.CustomFormat = tbDateFormat.Text.Trim();
        //}

        private void btnOrlToDate_Click(object sender, EventArgs e)
        {
            if (txtInputDateText.Text == "")
            {
                return;
            }
            string templ = "to_date('{0}','{1}')";
            string dval = dtPicker.Value.ToString(cbToDateFormat.Text.Replace("hh24", "HH").Replace("mi", "mm"));
            tbToDateOutput.Text = string.Format(templ, dval, cbToDateFormat.Text);
        }

        private void btnMinHour_Click(object sender, EventArgs e)
        {
            var curDate = dtPicker.Value;
            dtPicker.Value = new DateTime(curDate.Year, curDate.Month, curDate.Day, 0, 0, 0);
            txtInputDateText.Text = dtPicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnMaxHour_Click(object sender, EventArgs e)
        {
            var curDate = dtPicker.Value;
            dtPicker.Value = new DateTime(curDate.Year, curDate.Month, curDate.Day, 23, 59, 59);
            txtInputDateText.Text = dtPicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void cbSplitChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DefaultSplitChar = cbSplitChar.SelectedValue.ToString();
        }

        /// <summary>
        /// 提取JSON字段
        /// </summary>
        private void btnJsonField_Click(object sender, EventArgs e)
        {
            try
            {
                var resultList = new List<string>();
                var json = txtTempl.Text.Trim();
                var list = JsonConvert.DeserializeObject(json) as JToken;
                var listdata = new List<JToken>();
                var listResult = new List<string>();
                foreach (var item in list.Children())
                {
                    switch (item.Type)
                    {
                        case JTokenType.Object:
                            ChildrenTokens(item, listdata);
                            break;
                        case JTokenType.Property:
                            listdata.Add(item);
                            break;
                        default:
                            throw new Exception($"暂未适配该类型");
                    }
                }
                //遍历JProperty
                foreach (var item in listdata)
                {
                    var jProperty = item.ToObject<JProperty>();
                    string propName = jProperty.Name;
                    //JToken value = jProperty.Value;
                    listResult.Add(propName);
                }

                txtOutput.Text = string.Join(System.Environment.NewLine, listResult);
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChildrenTokens(JToken jObject, List<JToken> jTokens)
        {
            if (jTokens == null) throw new Exception("接收对象不能为空");

            var childrens = jObject.Children();
            foreach (var item in childrens)
            {
                switch (item.Type)
                {
                    case JTokenType.Object:
                        ChildrenTokens(item, jTokens);
                        break;
                    case JTokenType.Property:
                        jTokens.Add(item);
                        break;
                    default:
                        throw new Exception($"暂未适配该类型");
                }
            }
        }

        private void btnImportSingleCol_Click(object sender, EventArgs e)
        {
            try
            {
                var templ = txtTempl.Text.Trim();
                var inputTexts = templ.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim()).ToList();
                if (dt.Rows.Count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("col0");
                    }
                    foreach (var textItem in inputTexts)
                    {
                        var drNew = dt.NewRow();
                        drNew[$"col0"] = textItem.Trim();
                        dt.Rows.Add(drNew);
                    }
                }
                else
                {
                    int index = 0;
                    var colCount = dt.Columns.Count;
                    dt.Columns.Add($"col{colCount}");
                    foreach (var textItem in inputTexts)
                    {
                        DataRow drRow;
                        if (index >= dt.Rows.Count)
                        {
                            drRow = dt.NewRow();
                            drRow[$"col{colCount}"] = textItem.Trim();
                            dt.Rows.Add(drRow);
                        }
                        else
                        {
                            drRow = dt.Rows[index];
                            drRow[$"col{colCount}"] = textItem.Trim();
                        }
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            lblTotalRows.Text = $"共 {(dt != null ? dt.Rows.Count : 0)} 行 |";
        }

        private void btnRegexImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbRegex.Text))
            {
                MessageBox.Show("正则表达式不能为空");
                cbRegex.Focus();
                return;
            }
            var regex = new Regex(cbRegex.Text);
            var matches = regex.Matches(txtTempl.Text);
            var sbResult = new StringBuilder();
            dt = new DataTable();
            foreach (Match item in matches)
            {
                int gTotal = 0;
                DataRow drNew = dt.NewRow();
                foreach (Group groupItem in item.Groups)
                {
                    gTotal++;
                    if (dt.Columns.Count < gTotal)
                    {
                        dt.Columns.Add($"col{gTotal - 1}");
                    }
                    drNew[gTotal - 1] = groupItem.Value;
                }
                dt.Rows.Add(drNew);
            }
            dataGridView1.DataSource = dt;
            lblTotalRows.Text = $"共 {(dt != null ? dt.Rows.Count : 0)} 行 |";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows == null)
            {
                return;
            }
            lblCurRow.Text = $"当前第 {e.RowIndex + 1} 行";
        }

        private void btnDBTransferData_Click(object sender, EventArgs e)
        {
            DBTransferDataForm frm = new DBTransferDataForm();
            frm.Show();
        }

        private void btnToolConvertTo_Click(object sender, EventArgs e)
        {
            if (cbToolFormat.Text == "NumberFormat")
            {
                double d = 0.0;
                if (double.TryParse(txtToolFormatInput.Text, out d))
                {
                    txtToolFormatOutput.Text = d.ToString(txtToolFormat.Text.Trim());
                }
                else
                {
                    txtToolFormatOutput.Text = "Format Faild.";
                }
            }
            else if (cbToolFormat.Text == "DateFormat")
            {
                DateTime d = DateTime.Now;
                if (DateTime.TryParse(txtToolFormatInput.Text, out d))
                {
                    txtToolFormatOutput.Text = d.ToString(txtToolFormat.Text.Trim());
                }
                else
                {
                    txtToolFormatOutput.Text = "Format Faild.";
                }
            }
            else
            {
                txtToolFormatOutput.Text = string.Format(txtToolFormat.Text.Trim(), txtToolFormatInput.Text.Trim());
            }
        }

        private void btnClearInput_Click(object sender, EventArgs e)
        {
            txtInput4.Clear();
            txtOutput4.Clear();
        }

        /// <summary>
        /// 根据模板生成文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputFiles_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = string.Empty;
                string className = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    tableName = dt.Rows[0][0]?.ToString();
                    var classNameSplit = tableName.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(k => k.Substring(0, 1).ToUpper() + k.Substring(1).ToLower());
                    className = string.Join("", classNameSplit);
                }
                var diaFile = new FilesByTemplateForm(tableName, className, "");
                if (diaFile.ShowDialog() == DialogResult.OK)
                {
                    var templModel = diaFile.FileTemplateModel;
                    var generator = new FileTemplateGenerator(dt);
                    generator.GenerFiles(templModel);
                    MessageBox.Show("Successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtPicker_ValueChanged(object sender, EventArgs e)
        {
            txtInputDateText.Text = dtPicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void txtInputDateText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt;
                if (!DateTime.TryParse(txtInputDateText.Text, out dt))
                {
                    lblOtherMsg.Text = "日期格式不正确！";
                    return;
                }
                lblOtherMsg.Text = "";
                dtPicker.Value = dt;
            }
            catch (Exception ex)
            {
                lblOtherMsg.Text = ex.Message;
            }

        }

        private void btnNovelTool_Click(object sender, EventArgs e)
        {
            var frm = new NovelToolForm();
            frm.Show();
        }

        private void btnFtpTool_Click(object sender, EventArgs e)
        {
            var frm = new SFTPToolForm();
            frm.Show();
        }

        private void btnJsonTool_Click(object sender, EventArgs e)
        {
            var frm = new JsonToolForm();
            frm.Show();
        }

        private void txtTempl_TextChanged(object sender, EventArgs e)
        {
            var values = txtTempl.Text.SplitIncludeEmptry(_DefaultSplitChar);
            if (values == null)
            {
                lblSummary.Text = $"记录：0 | 重复项：0";
                return;
            }
            //var rptCount = values.GroupBy(i => i).Where(g => g.Count() > 1).Count();
            var rptCount = values.GroupBy(i => i).Count();
            lblSummary.Text = $"记录：{values.Length} | 重复项：{values.Length - rptCount}";
        }

    }
}