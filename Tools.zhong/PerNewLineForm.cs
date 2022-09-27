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
    public partial class PerNewLineForm : Form
    {
        private string inputText;

        public string InputText { get => inputText; set => inputText = value; }

        public PerNewLineForm(string inputText)
        {
            InitializeComponent();
            this.inputText = inputText;
            cbChar.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputText))
            {
                var inputVals = inputText.Split(new string[] { cbChar.Text }, StringSplitOptions.RemoveEmptyEntries);
                int perCol = -1;
                int.TryParse(txtCols.Text.Trim(), out perCol);
                if (perCol == -1)
                {
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                StringBuilder sbResult = new StringBuilder();
                for (int i = 0; i < inputVals.Length; i++)
                {
                    sbResult.Append(inputVals[i]);
                    sbResult.Append((i + 1) % perCol == 0 && perCol != inputVals.Length - 1 ? Environment.NewLine : "");
                }
                this.inputText = sbResult.ToString();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
