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
    public partial class FilesByTemplateForm : Form
    {
        public FilesByTemplateForm(string tableName, string className, string functionName)
        {
            InitializeComponent();
            txtClassName.Text = className;
            txtFuncName.Text = functionName;
            txtTableName.Text = tableName;
        }

        public FileTemplateModel FileTemplateModel { get; set; }

        private void btnTempDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTemplDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOutDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                txtOutputDir.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.FileTemplateModel = new FileTemplateModel
            {
                TableName = txtTableName.Text.Trim(),
                ClassName = txtClassName.Text.Trim(),
                FuncName = txtFuncName.Text.Trim(),
                TemplDir = txtTemplDir.Text.Trim(),
                OutputDir = txtOutputDir.Text.Trim()
            };
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FilesByTemplateForm_Load(object sender, EventArgs e)
        {
            txtTemplDir.Text = folderBrowserDialog1.SelectedPath;
            txtOutputDir.Text = folderBrowserDialog2.SelectedPath;
        }
    }
}
