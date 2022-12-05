using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class TableColumnModel
    {
        public string TableName { get; set; }
        public string TableComment { get; set; }
        public string FieldName { get; set; }
        public int? DataLength { get; set; }
        public string FieldRemarks { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }

        /// <summary>
        /// 数据精度
        /// </summary>
        public int? DataPrecision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int? DataScale { get; set; }
    }
}
