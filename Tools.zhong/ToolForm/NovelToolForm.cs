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
using System.IO;

using System.Text;
using System.Text.RegularExpressions;

namespace Tools.zhong
{
    public partial class NovelToolForm : Form
    {
        public NovelToolForm()
        {
            InitializeComponent();
        }
        private void btnTempDir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTemplDir.Text = openFileDialog1.FileName;
            }
        }

        private void btnOutDir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputDir.Text = openFileDialog1.FileName;
            }
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTemplDir.Text) && string.IsNullOrWhiteSpace(txtOutputDir.Text))
            {
                MessageBox.Show("输入输出文件路径未指定！");
            }
            try
            {

                var text = File.ReadAllText(txtTemplDir.Text);
                //txtFileContent.MaxLength = text.Length;
                //txtFileContent.Text = text;
                var regex = new Regex(@"第\w{1,100}章\s\w+");
                var matches = regex.Matches(text);
                var dic = new Dictionary<string, int>();
                var dicRepeat = new Dictionary<string, int>();
                if (matches.Count > 0)
                {
                    foreach (Match matchItem in matches)
                    {
                        var val = matchItem.Value;
                        var index = matchItem.Index;
                        if (!dic.ContainsKey(val))
                        {
                            dic.Add(val, index);
                        }
                        else
                        {
                            dic[val] = index;
                            //添加重复值
                            if (!dicRepeat.ContainsKey(val))
                            {
                                dicRepeat.Add(val, index);
                            }
                            else
                            {
                                dicRepeat[val] = index;
                            }
                        }
                    }
                }

                //去重复内容
                //var sbText = new StringBuilder();
                var lastIndex = 0;
                //var textOut = "";
                foreach (var dicItem in dicRepeat)
                {
                    var textItem = text.Substring(lastIndex, dicItem.Value - 1);
                    //textOut += textItem;
                    File.AppendAllText(txtOutputDir.Text, textItem);
                    lastIndex = dicItem.Value;
                }
                //File.AppendAllText(txtOutputDir.Text, sbText.ToString());
                //File.WriteAllText(txtOutputDir.Text, textOut);
                MessageBox.Show("处理完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
