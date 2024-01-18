using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tools.zhong.Component;

namespace Tools.zhong
{
    public partial class JsonToolForm : Form
    {
        public JsonToolForm()
        {
            InitializeComponent();
        }

        private void JsonToolForm_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.BindSplitCharComboBox(cbSplitChar);
            tabControl1.SelectedIndex = 1;
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            if (txtInput2.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "输入未完整！";
                return;
            }
            if (txtNode.Text.Trim().Length == 0)
            {
                txtOutput2.Text = "";
                return;
            }
            try
            {
                var jsonStr = txtInput2.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);

                var list = resultString.Where(i => Regex.IsMatch(i.Item1, txtNode.Text))?.Select(i => i.Item2)?.ToList();
                txtOutput2.Text = string.Join(cbSplitChar.SelectedValue?.ToString(), list);

            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }
        public void TraverseJObject(JToken token, ref List<Tuple<string, string>> result)
        {
            if (token is JProperty property && !property.HasValues)
            {
                //Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                result.Add(Tuple.Create(property.Name, property.Value?.ToString()));
            }
            else if (token is JValue jval)
            {
                result.Add(Tuple.Create(jval.Path, jval.Value?.ToString()));
            }

            foreach (var child in token.Children())
            {
                TraverseJObject(child, ref result);
            }
        }

        private void lblExample_Click(object sender, EventArgs e)
        {
            txtNode.Text = lblExample.Text.Trim();
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtInput2.Clear();
        }

        private void txtFormat_Click(object sender, EventArgs e)
        {
            if (txtInput2.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "输入未完整！";
                return;
            }
            try
            {
                var jsonStr = txtInput2.Text.Trim();
                JObject json = JObject.Parse(jsonStr);
                string formattedJson = json.ToString(Formatting.Indented);
                txtInput2.Text = formattedJson;
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }

        }

        private void btnShowNode_Click(object sender, EventArgs e)
        {
            if (txtInput2.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "输入未完整！";
                return;
            }
            try
            {
                var jsonStr = txtInput2.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);
                var regex = new Regex(@"\[\d+\]");
                var list = resultString?.OrderBy(i => regex.Replace(i.Item1, ""))?.Select(i => $"{i.Item1}\t ============ {i.Item2}")?.ToList();
                txtOutput2.Text = string.Join(cbSplitChar.SelectedValue?.ToString(), list);
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }
    }
}
