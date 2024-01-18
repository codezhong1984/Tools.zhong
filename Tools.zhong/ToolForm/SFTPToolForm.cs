using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public enum FTP_TYPE
    {
        FTP,
        SFTP
    }

    public partial class SFTPToolForm : Form
    {
        private FTP_TYPE ftpType = FTP_TYPE.SFTP;

        public SFTPToolForm()
        {
            InitializeComponent();
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (ftpType == FTP_TYPE.SFTP)
                {
                    var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    if (helper.Connect())
                    {
                        lblResult.Text = "连接成功";
                    }
                }
                else if (ftpType == FTP_TYPE.FTP)
                {
                    var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    if (helper.Connect())
                    {
                        lblResult.Text = "连接成功";
                    }
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = $"连接失败，异常信息：{ex.Message}";
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRemoteFolder.Text.Trim().Length == 0)
                {
                    lblResult.Text = "远程目录未指定！";
                    return;
                }
                if (txtLoacalFolder.Text.Trim().Length == 0)
                {
                    lblResult.Text = "本地目录未指定！";
                    return;
                }
                if (ftpType == FTP_TYPE.SFTP)
                {
                    var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());

                    var txtFileName = System.IO.Path.GetFileName(txtLoacalFolder.Text);

                    helper.Put(txtLoacalFolder.Text, txtRemoteFolder.Text + "\\" + txtFileName);
                    lblResult.Text = "上传成功！";
                }
                else if (ftpType == FTP_TYPE.FTP)
                {
                    var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    helper.Put(txtRemoteFolder.Text.Trim(), new string[] { txtLoacalFolder.Text.Trim() });
                    lblResult.Text = "上传成功！";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRemoteFolder.Text.Trim().Length == 0)
                {
                    lblResult.Text = "远程目录未指定！";
                    return;
                }
                if (txtLoacalFolder.Text.Trim().Length == 0)
                {
                    lblResult.Text = "本地目录未指定！";
                    return;
                }
                var txtLocal = txtLoacalFolder.Text.Trim();
                var localFilePath = System.IO.Path.GetFileName(txtLocal);
                var lastIndex = txtLocal.LastIndexOf(localFilePath);
                var localPath = lastIndex != -1 ? txtLocal.Substring(0, lastIndex) : txtLocal;

                var remotePath = txtRemoteFolder.Text;
                remotePath = remotePath.StartsWith("\\") || remotePath.StartsWith("/") ? remotePath : "/" + remotePath;
                remotePath = remotePath.EndsWith("\\") || remotePath.EndsWith("/") ? remotePath : remotePath + "/";

                if (ftpType == FTP_TYPE.SFTP)
                {
                    var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var list = helper.GetFileList(remotePath) ?? new List<Model.FTPFileModel>();

                    foreach (var item in list)
                    {
                        //排除文件夹
                        helper.Get(remotePath + item.FileName, localPath + item.FileName);
                    }

                    lblResult.Text = $"共下载{list.Count}个文件";
                }
                else if (ftpType == FTP_TYPE.FTP)
                {
                    var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var fileCount = helper.GetFiles(remotePath, txtLocal);
                    lblResult.Text = $"共下载{fileCount}个文件";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRemoteFolder.Text.Trim().Length == 0)
                {
                    lblResult.Text = "远程目录未指定！";
                    return;
                }
                if (ftpType == FTP_TYPE.SFTP)
                {
                    var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var list = helper.GetFileList(txtRemoteFolder.Text);
                    dataGridView1.DataSource = list;
                    lblResult.Text = $"共获取{list.Count}个文件";
                }
                else if (ftpType == FTP_TYPE.FTP)
                {
                    var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                         , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var list = helper.ListFiles(txtRemoteFolder.Text);
                    dataGridView1.DataSource = list;
                    lblResult.Text = $"共获取{list.Count}个文件";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLoacalFolder.Text = openFileDialog1.FileName;
            }
        }

        private void cbFTPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ftpType = (FTP_TYPE)Enum.Parse(typeof(FTP_TYPE), cbFTPType.Text);
            if (ftpType == FTP_TYPE.FTP)
            {
                InitFTPParamValue();
            }
            else
            {
                InitSFTPParamValue();
            }
            txtPassword.Text = "";
            dataGridView1.DataSource = null;
            treeViewFTPFolder.Nodes.Clear();
        }

        private void SFTPToolForm_Load(object sender, EventArgs e)
        {
            cbFTPType.SelectedIndex = 0;
            lblResult.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtRemoteFolder.Text.Trim().Length == 0)
            {
                lblResult.Text = "远程目录未指定！";
                return;
            }
            if (MessageBox.Show($"确认要删除远程文件{txtRemoteFolder.Text.Trim()}吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    if (ftpType == FTP_TYPE.SFTP)
                    {
                        var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                            , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        helper.Delete(txtRemoteFolder.Text.Trim());
                        lblResult.Text = $"删除成功";
                    }
                    else if (ftpType == FTP_TYPE.FTP)
                    {
                        var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                             , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        helper.Delete(txtRemoteFolder.Text);
                        lblResult.Text = $"删除成功";
                    }
                }
                catch (Exception ex)
                {
                    lblResult.Text = ex.Message;
                }
            }
        }

        private void btnLoadFTPFolder_Click(object sender, EventArgs e)
        {
            if (txtRemoteFolder.Text.Trim().Length == 0)
            {
                lblResult.Text = "远程目录未指定！";
                txtRemoteFolder.Focus();
                return;
            }
            if (txtUserName.Text.Trim().Length == 0)
            {
                lblResult.Text = "用户名未填写！";
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                lblResult.Text = "密码未填写！";
                txtPassword.Focus();
                return;
            }
            try
            {
                if (ftpType == FTP_TYPE.SFTP)
                {
                    var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());

                    var treeNode = helper.GetTreeViewNode(txtRemoteFolder.Text.Trim());
                    treeViewFTPFolder.Nodes.Clear();
                    treeNode.Expand();
                    treeViewFTPFolder.Nodes.Add(treeNode);
                }
                else if (ftpType == FTP_TYPE.FTP)
                {
                    var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                         , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var treeNode = helper.GetTreeViewNode(txtRemoteFolder.Text.Trim());
                    treeViewFTPFolder.Nodes.Clear();
                    treeNode.Expand();
                    treeViewFTPFolder.Nodes.Add(treeNode);
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        private void treeViewFTPFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeViewFTPFolder.Nodes.Count == 0 || treeViewFTPFolder.SelectedNode == null)
                {
                    return;
                }
                if (ftpType == FTP_TYPE.SFTP)
                {
                    var helper = new SFTPHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                        , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var list = helper.GetFileList(treeViewFTPFolder.SelectedNode.ToolTipText);
                    dataGridView1.DataSource = list;
                    lblResult.Text = $"共获取{list.Count}个文件";
                }
                else if (ftpType == FTP_TYPE.FTP)
                {
                    var helper = new FtpHelper(txtHost.Text.Trim(), int.Parse(txtPort.Text.Trim())
                         , txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    var list = helper.ListFiles(treeViewFTPFolder.SelectedNode.ToolTipText);
                    dataGridView1.DataSource = list;
                    lblResult.Text = $"共获取{list.Count}个文件";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        private void InitSFTPParamValue()
        {
            txtHost.Text = "HQASFTP.TPV-TECH.COM";
            txtPort.Text = "22";
            txtUserName.Text = "Hub_Manage";
            txtRemoteFolder.Text = "\\PTSOUTS";
            txtLoacalFolder.Text = "D:\\Hub\\SFTP(CSDB)\\";
        }

        private void InitFTPParamValue()
        {
            txtHost.Text = "172.20.32.49";
            txtPort.Text = "21";
            txtUserName.Text = "SAP_PTS";
            txtRemoteFolder.Text = "\\";
            txtLoacalFolder.Text = "D:\\Hub\\SFTP(CSDB)\\";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (treeViewFTPFolder.Nodes.Count > 0 && treeViewFTPFolder.SelectedNode == null)
            {
                treeViewFTPFolder.SelectedNode = treeViewFTPFolder.Nodes[0];
            }
            treeViewFTPFolder_AfterSelect(null, null);
            lblResult.Text = "数据已刷新！";
        }
    }
}
