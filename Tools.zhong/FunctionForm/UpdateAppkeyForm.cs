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
using System.IO;
using Tools.zhong.Model;

namespace Tools.zhong
{
    public partial class UpdateAppkeyForm : Form
    {
        public UpdateAppkeyForm()
        {
            InitializeComponent();
        }

        private AppConfigModel _AppConfigModel;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateAppkeyForm_Load(object sender, EventArgs e)
        {
            try
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
                        if (item.Name?.StartsWith("Local") ?? false)
                        {
                            continue;
                        }
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

                #region 加载Josn参数配置

                var jsFilePath = System.Environment.CurrentDirectory + "/ParamConfig.json";
                var jsonData = File.ReadAllText(jsFilePath);

                if (jsonData != null)
                {
                    _AppConfigModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AppConfigModel>(jsonData);
                    cbParamType.DataSource = _AppConfigModel.AppParamConfig.GroupBy(i => i.ParamType).Select(i => i.Key).ToList();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //var dataRow = dataGridView1.Rows[e.RowIndex];
            //if (dataRow.Cells["ParamName"]?.Value?.ToString() == "OracleDB")
            //{
            //    var columnn = (DataGridViewComboBoxColumn)dataGridView1.Columns[dataRow.Cells["ParamValue"].ColumnIndex];
            //    columnn.Items.Add(dataRow.Cells["ParamValue"]?.Value?.ToString());
            //}
            //else
            //{
            //    var columnn = (DataGridViewComboBoxColumn)dataGridView1.Columns[dataRow.Cells["ParamValue"].ColumnIndex];
            //    columnn.Items.Add(dataRow.Cells["ParamValue"]?.Value?.ToString());
            //}           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbParamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbParamType.Text))
            {
                return;
            }
            cbParamName.DataSource = _AppConfigModel.AppParamConfig
                .Where(i => i.ParamType == cbParamType.Text)
                .GroupBy(i => i.ParamName).Select(i => i.Key)
                .ToList();
        }

        private void cbParamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cbParamName.Text))
                {
                    return;
                }
                txtParamValue.Text = _AppConfigModel.AppParamConfig
                    .FirstOrDefault(i => i.ParamType == cbParamType.Text && i.ParamName == cbParamName.Text)
                    ?.ParamValue;

                if (dataGridView1.Rows != null)
                {
                    var dt = (DataTable)dataGridView1.DataSource;
                    foreach (DataRow item in dt.Rows)
                    {
                        var paramType = item["ParamName"]?.ToString();
                        if (paramType == cbParamType.Text)
                        {
                            item["ParamValue"] = txtParamValue.Text;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常，" + ex.Message);
                return;
            }
        }
    }
}
