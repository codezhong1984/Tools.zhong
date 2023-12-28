using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.zhong.Model;

namespace Tools.zhong.UtilHelper
{
    public class FTPHelper
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        FtpWebRequest reqFTP;

        public struct FileStruct
        {
            public string Flags;
            public string Owner;
            public string Group;
            public bool IsDirectory;
            public DateTime CreateTime;
            public string Name;
        }

        public enum FileListStyle
        {
            UnixStyle,
            WindowsStyle,
            Unknown
        }

        //連接FTP
        private void Connect(String path)
        {
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        }

        private string[] GetFileList(string path, string WRMethods)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            try
            {
                Connect(path);
                reqFTP.Method = WRMethods;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8); 
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append(" ");
                    line = reader.ReadLine();
                }
                // to remove the trailing ''
                result.Remove(result.ToString().LastIndexOf(' '),1);

                reader.Close();
                response.Close();

                return result.ToString().Split(' ');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                return downloadFiles;
            }
        }
    }
}
