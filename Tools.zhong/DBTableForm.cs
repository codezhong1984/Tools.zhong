using DBHepler;
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
using Tools.zhong.Model;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public partial class DbTableForm : Form
    {
        #region 公共属性

        private bool EnableMapperTableName = true;
        private bool DisplayView = false;
        public string CodeText { get; set; }
        public string LikeType { get; set; }
        public string FilterText { get; set; }
        private MainForm mainFrm;
        #endregion

        public DbTableForm(MainForm mainFrm,string likeType,string filterText)
        {
            this.mainFrm = mainFrm;
            LikeType = likeType;
            FilterText = filterText;
            InitializeComponent();
        }

        #region 获取数据表结构

        private string GetCodeForOracle(string tableName)
        {
            var list = DbObjectHelper.GetColumnsForOracle(tableName);
            var option = new CodeGenerateOption()
            {
                 AddDisplayName = cbDisplayName.Checked,
                 EnumCode = null,
                 FullPropFlag = cbFullProp.Checked,
                 MapperTableName = cbCreateTbName.Checked,
                 NameSpace = tbNameSpace.Text.Trim(),
                 TrimProp= cbIfTrim.Checked,
                 Underline = cbLineDeal.Checked
            };
            var code = UtilHelper.DbObjectHelper.GenerateCode(list, option);
            return code;
        }

        private string GetCodeForSqlServer(string tableName, bool isView)
        {
            var list = DbObjectHelper.GetColumnsForSqlServer(tableName, isView);
            var option = new CodeGenerateOption()
            {
                AddDisplayName = cbDisplayName.Checked,
                EnumCode = null,
                FullPropFlag = cbFullProp.Checked,
                MapperTableName = cbCreateTbName.Checked,
                NameSpace = tbNameSpace.Text.Trim(),
                TrimProp = cbIfTrim.Checked,
                Underline = cbLineDeal.Checked
            };
            var code = UtilHelper.DbObjectHelper.GenerateCode(list, option);
            return code;
        }

        private string GetCodeForMySQL(string tableName)
        {
            var dataBaseName = DbObjectHelper.GetDataBaseName(DataBaseType.MySQL);
            var list = DbObjectHelper.GetColumnsForMySQL(dataBaseName, tableName);
            var enumCode = GetEnumCodeForMySQL(tableName);
            var option = new CodeGenerateOption()
            {
                AddDisplayName = cbDisplayName.Checked,
                EnumCode = enumCode,
                FullPropFlag = cbFullProp.Checked,
                MapperTableName = cbCreateTbName.Checked,
                NameSpace = tbNameSpace.Text.Trim(),
                TrimProp = cbIfTrim.Checked,
                Underline = cbLineDeal.Checked
            };
            var code = UtilHelper.DbObjectHelper.GenerateCode(list, option);
            return code;
        }

        private string GetEnumCodeForMySQL(string tableName)
        {
            var dtData = DbObjectHelper.GetEnumCodeForMySQL(tableName);
            var list = UtilHelper.DbObjectHelper.GetEnumFieldsFormDB(dtData);
            var code = UtilHelper.DbObjectHelper.GenerateEnumCode(list);
            return code;
        }

        #endregion

        #region 控件事件定义

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbNameSpace.Text))
                {
                    ConfigHelper.SetValue("NameSpace", tbNameSpace.Text.Trim());
                }

                //if (cbDBType.SelectedIndex <= 0)
                //{
                //    cbDBType.Focus();
                //    MessageBox.Show("数据库类型未选择！");
                //    return;
                //}
                if (cbTableName.SelectedIndex < 0)
                {
                    cbTableName.Focus();
                    MessageBox.Show("数据库表未选择！");
                    return;
                }

                List<string> listTables = new List<string>();
                foreach (var item in cbTableName.CheckedItems)
                {
                    listTables.Add(((DataRowView)item)["table_name"]?.ToString());
                }

                string tableName = string.Empty;
                StringBuilder sbCodes = new StringBuilder();
                #region CreateCodeText

                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                if (dbType == DataBaseType.ORACLE)
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
                else if (dbType == DataBaseType.SQLSERVER)
                {
                    foreach (var item in listTables)
                    {
                        if (listTables.Count > 1)
                        {
                            sbCodes.AppendLine();
                            sbCodes.AppendLine($"//**************************** {item}  *******************************");
                            sbCodes.AppendLine();
                        }
                        var codeSqlServer = GetCodeForSqlServer(item, DisplayView);
                        sbCodes.AppendLine(codeSqlServer);
                    }

                    this.CodeText = sbCodes.ToString();
                }
                else if (dbType == DataBaseType.MySQL)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this.DialogResult = DialogResult.OK;
            this.mainFrm.TextOutPut.Text = this.CodeText;
            this.mainFrm.TabControl.SelectedIndex = 4;
            this.mainFrm.BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DBTaleForm_Load(object sender, EventArgs e)
        {
            tbNameSpace.Text = ConfigHelper.GetValue("NameSpace");
            folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            //加载数据库类型
            var dbTypes = Enum.GetNames(typeof(DataBaseType)).ToList<string>();
            //dbTypes.Insert(0, "请选择");
            cbDBType.DataSource = dbTypes;
            cbDBType.SelectedIndex = 0;

            cbLikeType.Items.Add(new ListItem("LIKE", "LIKE"));
            cbLikeType.Items.Add(new ListItem("NOT LIKE", "NOT LIKE"));
            cbLikeType.Items.Add(new ListItem("=", "="));
            cbLikeType.DisplayMember = "Text";
            cbLikeType.ValueMember = "Value";
            cbLikeType.SelectedIndex = 0;

            tbFilter.Text = FilterText;
            cbLikeType.Text = LikeType;
        }

        private void cbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
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
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                if (dbType == DataBaseType.ORACLE)
                {
                    foreach (var item in listTables)
                    {
                        var codeOracle = GetCodeForOracle(item);
                        using (StreamWriter sw = new StreamWriter($"{dirPath}\\{DbObjectHelper.ToUperFirstChar(item)}.cs", false, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine(codeOracle);
                        }
                    }
                }
                else if (dbType == DataBaseType.SQLSERVER)
                {
                    foreach (var item in listTables)
                    {
                        var codeSqlServer = GetCodeForSqlServer(item, DisplayView);
                        using (StreamWriter sw = new StreamWriter($"{dirPath}\\{DbObjectHelper.ToUperFirstChar(item)}.cs", false, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine(codeSqlServer);
                        }
                    }
                }
                else if (dbType == DataBaseType.MySQL)
                {
                    foreach (var item in listTables)
                    {
                        var codeMySQL = GetCodeForMySQL(item);
                        using (StreamWriter sw = new StreamWriter($"{dirPath}\\{DbObjectHelper.ToUperFirstChar(item)}.cs", false, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine(codeMySQL);
                        }
                    }
                }
                #endregion

                MessageBox.Show("文件保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void cbCreateTbName_CheckedChanged(object sender, EventArgs e)
        {
            EnableMapperTableName = cbCreateTbName.Checked;
        }

        #endregion

        private void cbView_CheckedChanged(object sender, EventArgs e)
        {
            DisplayView = cbView.Checked;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cbDBType.Text == "请选择")
            {
                return;
            }
            try
            {
                var dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cbDBType.Text, true);
                DataTable dtData = DisplayView ? DbObjectHelper.GetDataBaseViews(dbType, tbFilter.Text, cbLikeType.Text) : DbObjectHelper.GetDataBaseTables(dbType, tbFilter.Text, cbLikeType.Text);
                cbTableName.DataSource = dtData;
                cbTableName.DisplayMember = "table_name";
                cbTableName.ValueMember = "table_name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
