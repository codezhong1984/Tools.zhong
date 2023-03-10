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

        public ModelGeneratorForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var dataList = _ListColumns.ToList<TableColumnModel>();
            this.CodeText = DbObjectHelper.GenerateCode(dataList, cbLineDeal.Checked, cbDisplayName.Checked,
              tbNameSpace.Text.Trim(), null, false, cbFullProp.Checked);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            cbSplitChar.SelectedIndex = 0;
        }

        private void btnPreCreate_Click(object sender, EventArgs e)
        {
            string inputVal = txtCode.Text.Trim();
            string[] inputVals = inputVal.Split(new string[] { SplitChar }, StringSplitOptions.RemoveEmptyEntries);
            _ListColumns.Clear();
            //dataGridView1.DataSource = null;
            if (dataGridView1.DataSource != null && dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows.Clear();
            }

            foreach (var item in inputVals)
            {
                var colItem = new TableColumnModel();
                var propName = item.Replace(System.Environment.NewLine, "").Trim();

                //针对SQLServer去除首尾的综括号
                propName = propName.TrimStart('[').TrimEnd(']');
                //针对MySql、Oracel等去除首尾双引号
                propName = propName.TrimStart('"').TrimEnd('"');

                colItem.FieldName = DbObjectHelper.ToUperFirstChar(propName);
                colItem.DataType = "string";
                colItem.TableName = txtClassName.Text.Trim();
                colItem.TableComment = txtTableDescription.Text.Trim();
                colItem.FieldRemarks = "";
                colItem.IsNullable = true;
                _ListColumns.Add(colItem);
            }

            dataGridView1.DataSource = _ListColumns;
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
            cbFullProp.Checked = !cbIfTrim.Checked;
        }

        private void cbFullProp_CheckedChanged(object sender, EventArgs e)
        {
            cbIfTrim.Checked = !cbFullProp.Checked;
        }
    }
}
