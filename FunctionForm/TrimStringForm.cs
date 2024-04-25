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
using Tools.zhong.Model;

namespace Tools.zhong
{
    public partial class TrimStringForm : Form
    {
        public string SplitChar { get; set; }

        public string TrimString { get; set; }

        public bool TrimBlankFlag
        {
            get { return cbReserveSplitChar.Checked; }
        }
        public bool TrimEmptyLineFlag
        {
            get { return cbTrimEmptyLine.Checked; }
        }

        public OperatePosition Position { get; set; }

        public TrimStringForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SplitChar = cbSplitChar.SelectedValue.ToString();
            TrimString = txtInsertString.Text;
            Position = (OperatePosition)Enum.Parse(typeof(OperatePosition), cbBeforeOrAfter.SelectedValue.ToString());
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void InsertStringForm_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.BindSplitCharComboBox(cbSplitChar);
            //cbSplitChar.SelectedIndex = 1;

            var positionOptions = new List<dynamic>();
            positionOptions.Add(new { Text = "分隔项前", Value = OperatePosition.Before.ToString() });
            positionOptions.Add(new { Text = "分隔项后", Value = OperatePosition.After.ToString() });
            positionOptions.Add(new { Text = "分隔项前后", Value = OperatePosition.Include.ToString() });
            cbBeforeOrAfter.DataSource = positionOptions;
            cbBeforeOrAfter.DisplayMember = "Text";
            cbBeforeOrAfter.ValueMember = "Value";
            cbBeforeOrAfter.SelectedIndex = 0;
        }
    }
}
