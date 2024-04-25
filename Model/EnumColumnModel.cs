using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class EnumColumnModel
    {
        public string TableName { get; set; }
        public string TableComment { get; set; }
        public string FieldName { get; set; }
        public string FieldRemarks { get; set; }
        public string[] EnumValues { get; set; }
    }
}
