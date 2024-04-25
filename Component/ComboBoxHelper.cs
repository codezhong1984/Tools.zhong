using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.zhong.Model;

namespace Tools.zhong.Component
{
    public class ComboBoxHelper
    {
        public static void BindSplitCharComboBox(ComboBox comboBox)
        {
            var splitList = new List<dynamic>();
            splitList.Add(new { Text = "回车换行", Value = System.Environment.NewLine });
            splitList.Add(new { Text = "逗号", Value = "," });
            splitList.Add(new { Text = "分号", Value = ";" });
            splitList.Add(new { Text = "空格", Value = " " });
            splitList.Add(new { Text = "Tab", Value = "\'" });
            splitList.Add(new { Text = "单引号", Value = "," });
            splitList.Add(new { Text = "双引号", Value = "\"" });
            splitList.Add(new { Text = "冒号", Value = ":" });
            comboBox.DataSource = splitList;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = 0;
        }

        public static void BindDBTypeComboBox(ComboBox cbDBType)
        {
            //加载数据库类型
            string[] dbTypes = Enum.GetNames(typeof(DataBaseType));
            cbDBType.Items.Clear();
            cbDBType.DataSource = dbTypes;
        }

        public static void BindLikeTypeComboBox(ComboBox cbLikeType)
        {
            var splitList = new List<dynamic>();
            splitList.Add(new { Text = "LIKE", Value = "LIKE" });
            splitList.Add(new { Text = "NOT LIKE", Value = "NOT LIKE" });
            splitList.Add(new { Text = "=", Value = "=" });
            cbLikeType.DataSource = splitList;
            cbLikeType.DisplayMember = "Text";
            cbLikeType.ValueMember = "Value";
            cbLikeType.SelectedIndex = 0;
        }
    }
}
