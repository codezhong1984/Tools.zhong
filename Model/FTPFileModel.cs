using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class FTPFileModel
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
    }
}
