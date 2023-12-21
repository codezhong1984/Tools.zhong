using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class AppConfigModel
    {
        public List<ParamItem> AppParamConfig { get; set; }
    }

    public class ParamItem
    {
        public string ParamType { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
    }
}
