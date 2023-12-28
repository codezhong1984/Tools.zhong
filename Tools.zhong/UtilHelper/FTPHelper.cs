using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;
using Tools.zhong.Model;

namespace Tools.zhong.UtilHelper
{
    public class FtpHelper
    {
        #region 属性与构造函数

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddr { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelatePath { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public FtpHelper(string ipAddr, int port, string userName, string password, string relatePath = "/")
        {
            this.IpAddr = ipAddr;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
            this.RelatePath = relatePath;
        }

        #endregion

        #region 方法
        public bool Connect()
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    ftpClient.Connect();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public FtpListItem[] ListDir()
        {
            FtpListItem[] lists;
            using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
            {
                ftpClient.Connect();
                //ftpClient.SetWorkingDirectory(this.RelatePath);
                lists = ftpClient.GetListing();
            }
            return lists;
        }

        /// <summary>
        /// 上传单个文件
        /// </summary>
        public bool Put(string dir, string file, string fileExt = ".txt")
        {
            var isOk = false;
            try
            {
                FileInfo fi = new FileInfo(file);
                using (FileStream fs = fi.OpenRead())
                {
                    //long length = fs.Length;
                    using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                    {
                        ftpClient.Connect();
                        //ftpClient.SetWorkingDirectory(this.RelatePath);
                        string remotePath = dir + "/" + Path.GetFileName(file);
                        var ftpRemodeExistsMode = file.EndsWith(fileExt) ? FtpRemoteExists.Overwrite : FtpRemoteExists.Skip;
                        FtpStatus status = ftpClient.UploadStream(fs, remotePath, ftpRemodeExistsMode, true);
                        isOk = status == FtpStatus.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP上传多文件异常，原因：{0}", ex.Message));
            }
            return isOk;
        }

        /// <summary>
        /// 上传多个文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="isOk"></param>
        public bool Put(string dir, string[] files)
        {
            var isOk = true;
            try
            {
                if (DirExists(dir))
                {
                    foreach (var file in files)
                    {
                        isOk = isOk && Put(dir, file);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP上传多文件异常，原因：{0}", ex.Message));
            }
            return isOk;
        }

        private bool DirExists(string dir)
        {
            bool flag = false;
            using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
            {
                ftpClient.Connect();
                //ftpClient.SetWorkingDirectory(this.RelatePath);
                flag = ftpClient.DirectoryExists(dir);
                if (!flag)
                {
                    flag = ftpClient.CreateDirectory(dir);
                }
            }
            return flag;
        }

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="localAddress">远程文件地址，Example:/htdocs/1.txt</param>
        /// <param name="remoteAddress">本地文件地址</param>
        /// <returns></returns>
        public bool GetFile(string remoteAddress, string localAddress)
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    //ftpClient.SetWorkingDirectory("/");
                    ftpClient.Connect();
                    if (ftpClient.DownloadFile(localAddress, remoteAddress) == FtpStatus.Success)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP获取文件异常，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 下载目录中的所有文件
        /// </summary>
        /// <param name="localAddress">远程目录，Example:/htdocs</param>
        /// <param name="remoteAddress">本地目录</param>
        /// <returns></returns>
        public int GetFiles(string remoteFolder, string localFolder)
        {
            try
            {
                var result = 0;
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    //ftpClient.SetWorkingDirectory("/");
                    ftpClient.Connect();
                    var list = ftpClient.GetListing(remoteFolder, FtpListOption.NameList);
                    if (list != null && list.Length > 0)
                    {
                        foreach (var item in list)
                        {
                            if (item.Type != FtpObjectType.File)
                            {
                                continue;
                            }
                            var removeFilePath = item.FullName;
                            var localFilePath = localFolder + "/" + item.Name;
                            result += ftpClient.DownloadFile(localFolder, remoteFolder) == FtpStatus.Success ? 1 : 0;
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP获取多个文件异常，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        ///  删除ftp服务器上的文件
        /// </summary>
        /// <param name="remoteFile">服务器上的路径</param>
        public void Delete(string remoteFile)
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    ftpClient.Connect();
                    ftpClient.DeleteFile(remoteFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP文件删除异常，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        ///  移动ftp服务器上的文件
        /// </summary>
        public void MoveFile(string remoteSrcFilePath, string remoteDescFilePath)
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    ftpClient.Connect();
                    ftpClient.MoveFile(remoteSrcFilePath, remoteDescFilePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP文件删除异常，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        ///  判断ftp服务器上的文件是否存在
        /// </summary>
        public bool FileExists(string remoteFilePath)
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    ftpClient.Connect();
                    return ftpClient.FileExists(remoteFilePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("判断ftp服务器上的文件是否存在异常，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 判断ftp服务器上的目录是否存在
        /// </summary>
        public bool ExistDir(string remoteDirPath)
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    ftpClient.Connect();
                    return ftpClient.FileExists(remoteDirPath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("判断ftp服务器上的目录是否存在异常，原因：{0}", ex.Message));
            }
        }

        #endregion

        public List<FTPFileModel> ListFiles(string remotePath)
        {
            var resultList = new List<FTPFileModel>();
            using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
            {
                ftpClient.Connect();
                ftpClient.SetWorkingDirectory(remotePath);
                var list = ftpClient.GetListing();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.Type != FtpObjectType.File)
                        {
                            continue;
                        }
                        resultList.Add(new FTPFileModel
                        {
                            FileName = item.Name,
                            FileSize = item.Size,
                            LastWriteTime = item.Modified,
                            LastWriteTimeUtc = item.RawModified

                        });
                    }
                }
            }
            return resultList;
        }

        /// <summary>
        /// 获取ftp服务器上指定路径上的文件目录
        /// </summary>
        public TreeNode GetTreeViewNode(string remotePath)
        {
            try
            {
                using (var ftpClient = new FtpClient(this.IpAddr, this.Port, this.UserName, this.Password))
                {
                    ftpClient.Connect();
                    //ftpClient.SetWorkingDirectory(this.RelatePath);
                    var result = GetTreeViewNodeItem(ftpClient, remotePath);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FTP目录列表获取失败，原因：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 获取ftp服务器上指定路径上的文件目录
        /// </summary>
        private TreeNode GetTreeViewNodeItem(FtpClient ftpClient, string remotePath)
        {
            var treeViewNode = new TreeNode();
            remotePath = string.IsNullOrWhiteSpace(remotePath) || remotePath == "\\" ? "/" : remotePath;
            treeViewNode.Text = remotePath;
            treeViewNode.Name = treeViewNode.Text;
            treeViewNode.ToolTipText = remotePath;

            var list = ftpClient.GetListing(remotePath);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Type != FtpObjectType.Directory)
                    {
                        continue;
                    }
                    var childNode = GetTreeViewNodeItem(ftpClient, item.FullName);
                    treeViewNode.Nodes.Add(childNode);
                }
            }
            return treeViewNode;
        }
    }
}