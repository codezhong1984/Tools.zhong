using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.zhong.Component;

namespace Tools.zhong
{
    public partial class InsertStringForm : Form
    {
        public string SplitChar { get; set; }

        public string PrefixString { get; set; }      
        
        public bool TrimBlankFlag 
        {
            get { return cbReserveSplitChar.Checked; } 
        }
        public bool TrimEmptyLineFlag
        {
            get { return cbTrimEmptyLine.Checked; }
        }

        public string Position { get; set; }

        public InsertStringForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SplitChar = cbSplitChar.SelectedValue.ToString();
            PrefixString = txtInsertString.Text;
            Position = cbBeforeOrAfter.SelectedValue.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void InsertStringForm_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.BindSplitCharComboBox(cbSplitChar);
            cbSplitChar.SelectedIndex = 1;

            var positionOptions = new List<dynamic>();
            positionOptions.Add(new { Text = "分隔符前", Value = "B" });
            positionOptions.Add(new { Text = "分隔符后", Value = "A" });
            cbBeforeOrAfter.DataSource = positionOptions;
            cbBeforeOrAfter.DisplayMember = "Text";
            cbBeforeOrAfter.ValueMember = "Value";
            cbBeforeOrAfter.SelectedIndex = 0;
        }
    }
}
