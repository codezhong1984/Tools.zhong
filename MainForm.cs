using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tools.zhong.Component;
using Tools.zhong.Model;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public partial class MainForm : Form
    {
        #region 属性变量

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

            var saveDefaultPath = ConfigHelper.GetConfigValue("SaveDefaultPath");
            saveFileDialog1.InitialDirectory = string.IsNullOrWhiteSpace(saveDefaultPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : saveDefaultPath;
            cbEncodeType.SelectedIndex = 0;

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
           
        }

        private void btnOracleQueryHelper_Click(object sender, EventArgs e)
        {
           
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
                    //string input = txtInput4.Text.Trim();
                    //if (string.IsNullOrWhiteSpace(input))
                    //{
                    //    txtOutput4.Text = "";
                    //    return;
                    //}
                    //string[] inputItems = input.SplitNoEmpty("/");
                    //inputItems = inputItems.Select(i => URLUtil.Encrypt(i)).ToArray();
                    //txtOutput4.Text = string.Join("/", inputItems);
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
                    //txtOutput4.Text = URLUtil.Decrypt(txtInput4.Text.Trim());
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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