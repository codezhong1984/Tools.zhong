using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.zhong.Model;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public partial class ModelGeneratorForm : Form
    {
        private BindingList<TableColumnModel> _ListColumns = new BindingList<TableColumnModel>();
        public string CodeText { get; set; }

        private string SplitChar = "\t";

        private MainForm mainFrm;
        public ModelGeneratorForm(MainForm mainFrm)
        {
            this.mainFrm = mainFrm;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var dataList = _ListColumns.ToList<TableColumnModel>();
            var option = new CodeGenerateOption()
            {
                AddDisplayName = cbDisplayName.Checked,
                EnumCode = null,
                FullPropFlag = cbFullProp.Checked,
                MapperTableName = cbCreateTbName.Checked,
                NameSpace = tbNameSpace.Text.Trim(),
                TrimProp = cbIfTrim.Checked,
                Underline = cbLineDeal.Checked,
                Required = cbRequired.Checked,
            };
            this.CodeText = DbObjectHelper.GenerateCode(dataList, option);
            //this.DialogResult = DialogResult.OK;
            this.mainFrm.TextOutPut.Text = this.CodeText;
            this.mainFrm.TabControl.SelectedIndex = 4;
            this.mainFrm.BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            cbSplitChar.SelectedIndex = 3;
            var list = new List<ListItem>();
            list.Add(new ListItem("字符串", "string"));
            list.Add(new ListItem("整型", "int"));
            list.Add(new ListItem("浮点型", "decimal"));
            list.Add(new ListItem("日期", "DateTime"));
            list.Add(new ListItem("是/否", "bool"));
            list.Add(new ListItem("字符", "char"));
            cbFieldType.DataSource = list;
            cbFieldType.ValueMember = "Value";
            cbFieldType.DisplayMember = "Text";
            cbFieldType.SelectedIndex = 0;
            cbCol.SelectedIndex = 0;
        }

        private void btnPreCreate_Click(object sender, EventArgs e)
        {
            string inputVal = txtCode.Text.Trim();
            string[] inputVals = inputVal.Split(new string[] { SplitChar }, StringSplitOptions.RemoveEmptyEntries);
            if (cbCol.Text == "FieldName")
            {
                foreach (var item in inputVals)
                {
                    var colItem = new TableColumnModel();
                    var propName = item.Replace(System.Environment.NewLine, "").Trim();

                    //针对SQLServer去除首尾的综括号
                    propName = propName.TrimStart('[').TrimEnd(']');
                    //针对MySql、Oracel等去除首尾双引号
                    propName = propName.TrimStart('"').TrimEnd('"');

                    colItem.FieldName = DbObjectHelper.ToUperFirstChar(propName);
                    colItem.DataType = cbFieldType.SelectedValue.ToString();
                    colItem.TableName = txtClassName.Text.Trim();
                    colItem.TableComment = "";
                    colItem.FieldRemarks = "";
                    colItem.IsNullable = false;
                    _ListColumns.Add(colItem);
                }
            }
            else if (_ListColumns != null && _ListColumns.Count > 0)
            {
                for (int i = 0; i < _ListColumns.Count; i++)
                {
                    if (i > inputVals.Length - 1)
                    {
                        break;
                    }
                    var propName = inputVals[i].Replace(System.Environment.NewLine, "").Trim();
                    propName = propName.TrimStart('[').TrimEnd(']');
                    propName = propName.TrimStart('"').TrimEnd('"');
                    if (cbCol.Text == "DataType")
                    {
                        _ListColumns[i].DataType = propName;
                    }
                    else if (cbCol.Text == "FieldRemarks")
                    {
                        _ListColumns[i].FieldRemarks = propName;
                    }
                    else if (cbCol.Text == "IsNullable")
                    {
                        _ListColumns[i].IsNullable = propName == "是";
                    }
                }
            }
            dataGridView1.DataSource = _ListColumns;
            dataGridView1.Refresh();
        }

        private void cbSplitChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSplitChar.Text)
            {
                case "Tab": SplitChar = "\t"; break;
                case "空格": SplitChar = " "; break;
                case "逗号": SplitChar = ","; break;
                case "回车换行": SplitChar = Environment.NewLine; break;
                default:
                    SplitChar = "\t";
                    break;
            }
        }

        private void cbIfTrim_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbFullProp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDelColumn_Click(object sender, EventArgs e)
        {
            _ListColumns.Clear();
            dataGridView1.DataSource = _ListColumns;
        }

        private void btnRequired_Click(object sender, EventArgs e)
        {
            if (_ListColumns != null && _ListColumns.Count > 0)
            {
                for (int i = 0; i < _ListColumns.Count; i++)
                {
                    _ListColumns[i].IsNullable = btnRequired.Text == "全部非必填";
                }
            }
            dataGridView1.DataSource = _ListColumns;
            dataGridView1.Refresh();

            btnRequired.Text = btnRequired.Text == "全部非必填" ? "全部必填" : "全部非必填";
        }
    }
}
