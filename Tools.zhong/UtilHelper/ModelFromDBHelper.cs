using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.UtilHelper
{
    public class ModelFromDBHelper
    {
        public static string GenerateCode(List<TableColumnModel> listColumns, bool underline = false, bool addDisplayName = false,
            string ns = "DBModel")
        {
            if (listColumns == null || listColumns.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sbResult = new StringBuilder();
            sbResult.AppendLine("using System;");
            if (addDisplayName)
            {
                sbResult.AppendLine("using System.ComponentModel;");
            }
            sbResult.AppendLine();
            sbResult.AppendLine($"namespace {(string.IsNullOrWhiteSpace(ns) ? "DBModel" : ns)}");
            sbResult.AppendLine("{");

            sbResult.AppendLine("    /// <summary>");
            sbResult.AppendLine("    /// " + listColumns[0].TableComment);
            sbResult.AppendLine("    /// 创建于 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbResult.AppendLine("    /// </summary>");

            sbResult.AppendLine("    public class " + listColumns[0].TableName);
            sbResult.AppendLine("    {");


            #region 生成私有变量

            //foreach (var _item in listColumns)
            //{
            //    if (_item.FieldName.ToLower() != "id")
            //    {
            //        string oneValue = _item.FieldName.Substring(0, 1).ToLower();
            //        string fullValue = oneValue + _item.FieldName.Substring(1);
            //        string dataType = ChangeToCsharpType(_item.DataType);//根据数据库数据类型转换成c#对应数据类型
            //        string dataTypeValue = DefaultForType(dataType);//根据c#数据类型获取初始值
            //        text += tabBlank + tabBlank + "private " + dataType + " _" + fullValue + " = " + dataTypeValue + ";\r\n";
            //    }
            //}
            //foreach (var item in listColumns)
            //{
            //    if (item.FieldName.ToLower() != "id")
            //    {
            //        string dataType = string.Empty;
            //        dataType = ChangeToCsharpType(item.DataType);
            //        text += tabBlank + tabBlank + "/// <summary>\r\n" + tabBlank + tabBlank + "/// " + item.FieldRemarks + "\r\n" + tabBlank + tabBlank + "/// </summary>\r\n";
            //        text += tabBlank + tabBlank + "\r\npublic " + dataType + " " + item.FieldName + "\r\n";
            //        string oneValue = item.FieldName.Substring(0, 1).ToLower();
            //        string fullValue = oneValue + item.FieldName.Substring(1);
            //        text += tabBlank + tabBlank + "{\r\n" + tabBlank + tabBlank + tabBlank + "get { return " + " _" + fullValue + "; }\r\n";
            //        text += tabBlank + tabBlank + tabBlank + "set { " + " _" + fullValue + " = value; }\r\n" + tabBlank + tabBlank + "}\r\n";
            //    }
            //}

            #endregion

            int i = 0;
            foreach (var item in listColumns)
            {
                string dataType = string.Empty;
                dataType = ChangeToCsharpType(item.DataType);
                if (i++>0)
                {
                    sbResult.AppendLine();
                }
                sbResult.AppendLine("        /// <summary>");
                sbResult.AppendLine("        /// " + item.FieldRemarks.Replace("\\n",""));
                sbResult.AppendLine("        /// </summary>");

                if (addDisplayName)
                {
                    sbResult.AppendLine("        [DisplayName(\"" + item.FieldRemarks + "\")]");
                }
                var fieldCode = $"        public {dataType}{(item.IsNullable && !IsNullableType(dataType) ? "?" : "")}"
                              + $" {(underline ? ReplaceUnderline(item.FieldName) : ToUperFirstChar(item.FieldName))} "
                              + "{ get; set; }";

                sbResult.AppendLine(fieldCode);
            }
            sbResult.AppendLine("    }");
            sbResult.AppendLine("}");

            return sbResult.ToString();
        }

        public static string ToUperFirstChar(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return fieldName;
            }
            return fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
        }

        public static string ReplaceUnderline(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return fieldName;
            }

            string[] fields = fieldName.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length == 1)
            {
                return ToUperFirstChar(fieldName);
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in fields)
            {
                sb.Append(item.Substring(0, 1).ToUpper());
                sb.Append(item.Substring(1).ToLower());
            }

            return sb.ToString();
        }


        //数据库对应类型
        public static string ChangeToCsharpType(string type)
        {
            string reval = string.Empty;
            switch (type.ToLower())
            {
                case "int":
                    reval = "Int32";
                    break;
                case "integer":
                    reval = "Int32";
                    break;
                case "text":
                    reval = "string";
                    break;
                case "bigint":
                    reval = "Int64";
                    break;
                case "binary":
                    reval = "byte[]";
                    break;
                case "bit":
                    reval = "bool";
                    break;
                case "char":
                    reval = "string";
                    break;
                case "datetime":
                    reval = "DateTime";
                    break;
                case "date":
                    reval = "DateTime";
                    break;
                case "timestamp":
                    reval = "DateTime";
                    break;
                case "time":
                    reval = "DateTime";
                    break;
                case "decimal":
                    reval = "decimal";
                    break;
                case "number":
                    reval = "decimal";
                    break;
                case "float":
                    reval = "double";
                    break;
                case "image":
                    reval = "byte[]";
                    break;
                case "money":
                    reval = "decimal";
                    break;
                case "nchar":
                    reval = "string";
                    break;
                case "nchar2":
                    reval = "string";
                    break;
                case "ntext":
                    reval = "string";
                    break;
                case "numeric":
                    reval = "decimal";
                    break;
                case "nvarchar2":
                    reval = "string";
                    break;
                case "nvarchar":
                    reval = "string";
                    break;
                case "real":
                    reval = "single";
                    break;
                case "smalldatetime":
                    reval = "DateTime";
                    break;
                case "smallint":
                    reval = "Int16";
                    break;
                case "smallmoney":
                    reval = "decimal";
                    break;
                case "tinyint":
                    reval = "byte";
                    break;
                case "uniqueidentifier":
                    reval = "guid";
                    break;
                case "varbinary":
                    reval = "byte[]";
                    break;
                case "varchar":
                    reval = "string";
                    break;
                case "varchar2":
                    reval = "string";
                    break;
                case "variant":
                    reval = "object";
                    break;
                default:
                    reval = "string";
                    break;
            }
            return reval;
        }

        public static bool IsNullableType(string type)
        {
            return type == "string" || type == "object" || type == "byte[]";
        }

        /// <summary>
        /// 各数据类型初始化
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string DefaultForType(string type)
        {
            string value = string.Empty;
            type = type.ToLower();
            switch (type)
            {
                case "string":
                    value = "string.Empty";
                    break;
                case "int32":
                    value = "-1";
                    break;
                case "int16":
                    value = "-1";
                    break;
                case "int64":
                    value = "-1";
                    break;
                case "object":
                    value = "null";
                    break;
                case "datetime":
                    value = "DateTime.Now";
                    break;
                case "decimal":
                    value = "0.0m";
                    break;
                case "single":
                    value = "-1";
                    break;
                case "byte[]":
                    value = "null";
                    break;
                case "double":
                    value = "-1";
                    break;
                case "boolean":
                    value = "false";
                    break;
                case "byte":
                    value = "0";
                    break;
            }
            return value;
        }

        /// <summary>
        /// Oracle 取表属性
        /// </summary>
        public static List<TableColumnModel> GetFieldsFormDB(DataTable dataTable)
        {
            var list = new List<TableColumnModel>();
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 1)
                {
                    foreach (DataRow drItem in dataTable.Rows)
                    {
                        var modelItem = new TableColumnModel();
                        modelItem.TableName = drItem["table_name"]?.ToString();
                        modelItem.TableComment = drItem["table_comments"]?.ToString();
                        modelItem.FieldName = drItem["column_name"]?.ToString();
                        modelItem.DataType = drItem["data_type"]?.ToString();
                        modelItem.FieldRemarks = drItem["column_comments"]?.ToString();
                        modelItem.IsNullable = drItem["NULLABLE"] == null || drItem["NULLABLE"]?.ToString() == "Y";
                        list.Add(modelItem);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class TableColumnModel
    {
        public string TableName { get; set; }
        public string TableComment { get; set; }
        public string FieldName { get; set; }
        public int DataLength { get; set; }
        public string FieldRemarks { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
    }
}
