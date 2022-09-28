using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;

namespace Tools.zhong
{
    public partial class UpdateAppkeyForm : Form
    {
        public UpdateAppkeyForm()
        {
            InitializeComponent();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateAppkeyForm_Load(object sender, EventArgs e)
        {
            var type = "ParamType";
            var name = "ParamName";
            var value = "ParamValue";
            DataTable dt = new DataTable();
            dt.Columns.Add(type);
            dt.Columns.Add(name);
            dt.Columns.Add(value);

            var connectionStrings = ConfigurationManager.ConnectionStrings;
            var appSettings = ConfigurationManager.AppSettings;
            if (connectionStrings != null)
            {
                foreach (ConnectionStringSettings item in connectionStrings)
                {
                    var drRow = dt.NewRow();
                    drRow[type] = "ConnectionString";
                    drRow[name] = item.Name;
                    drRow[value] = item.ConnectionString;
                    dt.Rows.Add(drRow);
                }
            }
            if (appSettings != null)
            {
                foreach (var item in appSettings.AllKeys)
                {
                    var drRow = dt.NewRow();
                    drRow[type] = "AppSetting";
                    drRow[name] = item;
                    drRow[value] = appSettings.Get(item);
                    dt.Rows.Add(drRow);
                }
            }

            dataGridView1.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.ExecutablePath + ".config");//获取当前配置文件
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var ParamType = item.Cells[0].Value?.ToString();
                    var ParamName = item.Cells[1].Value?.ToString();
                    var ParamValue = item.Cells[2].Value?.ToString();

                    XmlNode node = null;
                    if (ParamType == "AppSetting")
                    {
                        node = doc.SelectSingleNode(@"//add[@key='" + ParamName + "']");
                        XmlElement ele = (XmlElement)node;
                        if (ele == null)
                        {
                            continue;
                        }
                        ele.SetAttribute("value", ParamValue);
                    }
                    else
                    {
                        node = doc.SelectSingleNode(@"//add[@name='" + ParamName + "']");
                        XmlElement ele = (XmlElement)node;
                        if (ele == null)
                        {
                            continue;
                        }
                        ele.SetAttribute("connectionString", ParamValue);
                    }
                }
                doc.Save(Application.ExecutablePath + ".config");
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常，" + ex.Message);
                return;
            }
            MessageBox.Show("修改成功");
            this.Close();
        }
    }
}
