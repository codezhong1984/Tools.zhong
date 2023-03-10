using DBHepler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools.zhong.Model;

namespace Tools.zhong.UtilHelper
{
    public class DbObjectHelper
    {
        public static string GenerateCode(List<TableColumnModel> listColumns, bool underline = false, bool addDisplayName = false,
            string ns = "DBModel", string enumCode = null, bool EnableMapperTableName = false, bool fullPropFlag = false)
        {
            if (listColumns == null || listColumns.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sbResult = new StringBuilder();
            sbResult.AppendLine("using System;");
            if (addDisplayName || EnableMapperTableName)
            {
                sbResult.AppendLine("using System.ComponentModel;");
            }
            if (EnableMapperTableName)
            {
                sbResult.AppendLine("using System.ComponentModel.DataAnnotations;");
                sbResult.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            }
            sbResult.AppendLine();
            sbResult.AppendLine($"namespace {(string.IsNullOrWhiteSpace(ns) ? "DBModel" : ns)}");
            sbResult.AppendLine("{");

            if (!string.IsNullOrWhiteSpace(enumCode))
            {
                sbResult.AppendLine(enumCode);
            }

            sbResult.AppendLine("    /// <summary>");
            sbResult.AppendLine("    /// " + listColumns[0].TableComment);
            sbResult.AppendLine("    /// 创建于 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbResult.AppendLine("    /// </summary>");
            if (EnableMapperTableName)
            {
                sbResult.AppendLine("    [Table(\"" + ToUperFirstChar(listColumns[0].TableName) + "\")]");
            }
            sbResult.AppendLine("    public class " + ToUperFirstChar(listColumns[0].TableName));
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
                if (i++ > 0)
                {
                    sbResult.AppendLine();
                }
                sbResult.AppendLine("        /// <summary>");
                sbResult.AppendLine("        /// " + item.FieldRemarks?.Replace("\\n", ""));
                sbResult.AppendLine("        /// </summary>");

                if (addDisplayName)
                {
                    sbResult.AppendLine("        [DisplayName(\"" + item.FieldRemarks + "\")]");
                }
                if (EnableMapperTableName && i == 1)
                {
                    sbResult.AppendLine("        [Key]");
                }

                var fieldCode = new StringBuilder();
                if (fullPropFlag)
                {
                    fieldCode.AppendLine($"        public {dataType}{(item.IsNullable && !IsNullableType(dataType) ? "?" : "")}"
                           + $"{(item.DataType == "enum" ? ToUperFirstChar(listColumns[0].TableName) + "_" + ToUperFirstChar(item.FieldName) : "")}"
                           + $" {(underline ? ReplaceUnderline(item.FieldName) : ToUperFirstChar(item.FieldName))} ");
                    fieldCode.AppendLine("        {");
                    fieldCode.AppendLine("            get { return _" + (underline ? ReplaceUnderline(item.FieldName) : ToUperFirstChar(item.FieldName)) + "; }");
                    fieldCode.AppendLine("            set { _" + (underline ? ReplaceUnderline(item.FieldName) : ToUperFirstChar(item.FieldName)) + " = value; }");
                    fieldCode.AppendLine("        }");
                    fieldCode.AppendLine($"        private {dataType}{(item.IsNullable && !IsNullableType(dataType) ? "?" : "")}"
                           + $"{(item.DataType == "enum" ? ToUperFirstChar(listColumns[0].TableName) + "_" + ToUperFirstChar(item.FieldName) : "")}"
                           + $" _{(underline ? ReplaceUnderline(item.FieldName) : ToUperFirstChar(item.FieldName))}; ");
                }
                else
                {
                    fieldCode.AppendLine($"        public {dataType}{(item.IsNullable && !IsNullableType(dataType) ? "?" : "")}"
                             + $"{(item.DataType == "enum" ? ToUperFirstChar(listColumns[0].TableName) + "_" + ToUperFirstChar(item.FieldName) : "")}"
                             + $" {(underline ? ReplaceUnderline(item.FieldName) : ToUperFirstChar(item.FieldName))} "
                             + "{ get; set; }");
                }

                sbResult.AppendLine(fieldCode.ToString());
            }
            sbResult.AppendLine("    }");
            sbResult.AppendLine("}");

            return sbResult.ToString();
        }

        public static string GenerateEnumCode(List<EnumColumnModel> list)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sbResult = new StringBuilder();

            sbResult.AppendLine("    #Region Enum " + ToUperFirstChar(list[0].TableName));

            foreach (var colItem in list)
            {
                sbResult.AppendLine("    public enum " + ToUperFirstChar(list[0].TableName) + "_" + ToUperFirstChar(colItem.FieldName));
                sbResult.AppendLine("    {");

                int i = 0;
                foreach (var item in colItem.EnumValues)
                {
                    var enumValItem = item.Replace("'", "").Replace(" ", "");
                    sbResult.AppendLine($"        {enumValItem},");
                }
                sbResult.AppendLine("    }");
            }

            sbResult.AppendLine("    #End Region Enum " + list[0].TableName);
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
            if (type == null)
            {
                return "string";
            }
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
                case "enum"://mysql 单独定义枚举类型对象
                    reval = "";
                    break;
                default:
                    reval = "string";
                    break;
            }
            return reval;
        }

        public static bool IsNullableType(string type)
        {
            return type == "string" || type == "object" || type == "byte[]" || type == "enum";
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
        public static List<EnumColumnModel> GetEnumFieldsFormDB(DataTable dataTable)
        {
            var list = new List<EnumColumnModel>();
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drItem in dataTable.Rows)
                    {
                        var modelItem = new EnumColumnModel();
                        modelItem.TableName = drItem["table_name"]?.ToString();
                        modelItem.FieldName = drItem["column_name"]?.ToString();

                        //enum('Asia','Europe','North America','Africa','Oceania','Antarctica','South America')
                        string enumVals = drItem["column_type"]?.ToString();
                        Regex regex = new Regex(@"enum\((.+)\)$");
                        //'Asia','Europe','North America','Africa','Oceania','Antarctica','South America'
                        var match = regex.Match(enumVals);
                        enumVals = match.Groups.Count <= 1 ? "" : match.Groups[1].Value;
                        modelItem.EnumValues = enumVals.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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

        #region 获取数据表列表

        public static DataTable GetDataBaseTables(DataBaseType dbType, string tableFilter = null, string filterType = "LIKE")
        {
            var filterChar = filterType == "=" ? string.Empty : "%";
            if (dbType == DataBaseType.ORACLE)
            {
                string sql = "select table_name from user_tables {0} order by table_name";
                sql = string.Format(sql, !string.IsNullOrWhiteSpace(tableFilter) ? $"where table_name {filterType} '{filterChar}{tableFilter.ToUpper()}{filterChar}'" : "");
                var dtData = OracleHelper.ExecuteDataTable(sql);
                return dtData;
            }
            if (dbType == DataBaseType.SQLSERVER)
            {
                string sql = "select name table_name from sys.tables {0} order by name";
                sql = string.Format(sql, !string.IsNullOrWhiteSpace(tableFilter) ? $"where name {filterType} '{filterChar}{tableFilter}{filterChar}'" : "");
                var dtData = DBHepler.SQLHelper.ExecuteDataTable(sql);
                return dtData;
            }
            if (dbType == DataBaseType.MySQL)
            {
                string sql = "select table_name from information_schema.tables where table_schema=@DataBase {0} order by table_name";
                sql = string.Format(sql, !string.IsNullOrWhiteSpace(tableFilter) ? $"and table_name {filterType} '{filterChar}{tableFilter}{filterChar}'" : "");
                var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(sql);
                return dtData;
            }
            return null;
        }

        public static DataTable GetDataBaseViews(DataBaseType dbType, string tableFilter = null, string filterType = "LIKE")
        {
            var filterChar = filterType == "=" ? string.Empty : "%";
            if (dbType == DataBaseType.ORACLE)
            {
                string sql = "select view_name table_name from user_views {0} order by view_name";
                sql = string.Format(sql, !string.IsNullOrWhiteSpace(tableFilter) ? $"where view_name like {filterType} '{filterChar}{tableFilter}{filterChar}'" : "");
                var dtData = OracleHelper.ExecuteDataTable(sql);
                return dtData;
            }
            if (dbType == DataBaseType.SQLSERVER)
            {
                string sql = "select name table_name from sys.views {0} order by name";
                sql = string.Format(sql, !string.IsNullOrWhiteSpace(tableFilter) ? $"where name {filterType} '{filterChar}{tableFilter}{filterChar}'" : "");
                var dtData = DBHepler.SQLHelper.ExecuteDataTable(sql);
                return dtData;
            }
            if (dbType == DataBaseType.MySQL)
            {
                string sql = "select table_name from information_schema.views where table_schema=@DataBase {0} order by table_name";
                sql = string.Format(sql, !string.IsNullOrWhiteSpace(tableFilter) ? $"and table_name {filterType} '{filterChar}{tableFilter}{filterChar}'" : "");
                var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(sql);
                return dtData;
            }
            return null;
        }

        #endregion

        #region 获取数据库表字段

        /// <summary>
        /// 获取Orcle 数据表字段
        /// </summary>
        public static List<TableColumnModel> GetColumnsForOracle(string tableName)
        {
            //--添加表注释 COMMENT ON TABLE STUDENT_INFO IS '学生信息表'
            //添加字段注释 comment on column R_StockAnalysis_T.WORKNO is '工单号';
            string sql = @"select a.TABLE_NAME,c.COMMENTS as table_comments, a.column_name,a.DATA_TYPE,b.Comments as column_comments,a.NULLABLE
                                 ,a.DATA_LENGTH as DataLength,a.DATA_PRECISION as DataPrecision,a.DATA_SCALE as DataScale
                            from user_tab_columns a 
                            left join user_col_comments b on a.TABLE_NAME=b.TABLE_NAME and a.COLUMN_NAME = b.column_name
                            left join user_tab_comments c on a.TABLE_NAME=c.TABLE_NAME
                            where a.table_name ='{0}'
                            order by a.COLUMN_ID ";

            var dtData = OracleHelper.ExecuteDataTable(string.Format(sql, tableName.Trim()));
            var list = GetFieldsFormDB(dtData);
            return list;
        }

        /// <summary>
        /// 获取SqlServer 数据表字段
        /// </summary>
        public static List<TableColumnModel> GetColumnsForSqlServer(string tableName, bool isView = false)
        {
            string sql = @" select a.name table_name,b.value table_comments,c.name column_name,e.name data_type,d.value column_comments
                                    ,IIF(c.is_nullable=1,'Y','N') nullable,c.max_length as DataLength,iif(c.precision='0',null,c.precision) DataPrecision
                                    ,c.scale as DataScale
                                from sys.{0} a 
                                left join sys.extended_properties b on a.object_id=b.major_id and b.minor_id=0
                                left join sys.columns c on a.object_id=c.object_id
                                left join sys.extended_properties d on d.major_id=c.object_id and d.minor_id=c.column_id
                                left join sys.systypes e on c.system_type_id=e.xtype and e.xtype=e.xusertype
                                where a.name='{1}'
                                order by c.column_id ";

            var dtData = DBHepler.SQLHelper.ExecuteDataTable(string.Format(sql, isView ? "views" : "tables", tableName));
            var list = GetFieldsFormDB(dtData);
            return list;
        }

        /// <summary>
        /// 获取MySql 数据表字段
        /// </summary>
        public static List<TableColumnModel> GetColumnsForMySQL(string dataBaseName, string tableName)
        {
            //添加表注释 alter table test1 comment '修改后的表的注释';
            //添加列注释 alter table test modify column id int not null default 0 comment '测试表id'

            string sql = @" select b.table_name,b.table_comment table_comments,a.column_name,a.data_type,a.column_comment column_comments,if(a.is_Nullable='YES','Y','N') nullable
                               ,a.CHARACTER_MAXIMUM_LENGTH as DataLength,a.NUMERIC_PRECISION as DataPrecision,a.NUMERIC_SCALE as DataScale
                            from information_schema.columns a inner join information_schema.tables b on a.table_name=b.table_name and a.table_schema=b.table_schema
                            where b.table_schema='{0}' and b.table_name='{1}'
                            order by a.ORDINAL_POSITION ";

            var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(string.Format(sql, dataBaseName, tableName));
            var list = GetFieldsFormDB(dtData);
            return list;
        }

        #endregion

        #region 获取数据表的创建时间、修改时间
        /// <summary>
        /// 获取Orcle 数据表信息
        /// </summary>
        public static TableModel GetOracleTableInfo(string tableName)
        {
            string sql = @"SELECT object_name TableName,CREATED CreateDate,LAST_DDL_TIME LastUpdateDate 
                           from user_objects 
                           where object_name=upper('{0}') ";

            var dtData = OracleHelper.ExecuteDataTable(string.Format(sql, tableName.Trim()));
            var model = GetTabelInfoFormDB(dtData);
            return model;
        }

        /// <summary>
        /// 获取SqlServer 数据表信息
        /// </summary>
        public static TableModel GetSqlServerTableInfo(string tableName, bool viewFlag = false)
        {
            string sql = @"select name TableName,create_date CreateDate,modify_date LastUpdateDate
                           from sys.{0}
                           where name='{1}' ";

            var dtData = SQLHelper.ExecuteDataTable(string.Format(sql, viewFlag ? "views" : "tables", tableName.Trim()));
            var model = GetTabelInfoFormDB(dtData);
            return model;
        }

        /// <summary>
        /// 获取MySql 数据表信息
        /// </summary>
        public static TableModel GetMySqlTableInfo(string dataBaseName, string tableName)
        {
            string sql = @"select table_name TableName,Create_Time CreateDate,Update_Time LastUpdateDate from information_schema.TABLES 
                           where table_schema='{0}' and table_name='{1}'; ";

            var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(string.Format(sql, dataBaseName, tableName));
            var model = GetTabelInfoFormDB(dtData);
            return model;
        }
        #endregion

        #region 获取数据表的主键

        /// <summary>
        /// 获取指定数据表的主键信息
        /// </summary>
        public static List<string> GetOracleTablePrimaryKey(string tableName)
        {
            string sql = @"select column_name ColumnName 
                           from user_cons_columns cu, user_constraints au 
                           where cu.constraint_name = au.constraint_name and au.constraint_type = 'P' and au.table_name = '{0}'";
            var dtData = OracleHelper.ExecuteDataTable(string.Format(sql, tableName.Trim()));
            return GetTablePrimaryKey(dtData);
        }

        /// <summary>
        /// 获取指定数据表的主键信息
        /// </summary>
        public static List<string> GetSqlServerTablePrimaryKey(string tableName)
        {
            string sql = @"SELECT cols.name ColumnName
                        FROM sys.index_columns indexCols
                        INNER JOIN sys.columns cols ON indexCols.object_id = cols.object_id AND indexCols.column_id = cols.column_id
                        INNER JOIN sys.indexes inds ON indexCols.object_id = inds.object_id AND indexCols.index_id = inds.index_id
                        WHERE indexCols.object_id = OBJECT_ID('{0}', 'u') AND inds.is_primary_key = 1";
            var dtData = SQLHelper.ExecuteDataTable(string.Format(sql, tableName.Trim()));
            return GetTablePrimaryKey(dtData);
        }

        /// <summary>
        /// 获取指定数据表的主键信息
        /// </summary>
        public static List<string> GetMySqlTablePrimaryKey(string dataBaseName, string tableName)
        {
            string sql = @"SELECT Column_Name ColumnName
                           FROM  INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                           WHERE CONSTRAINT_NAME = 'PRIMARY' AND Table_Name='{0}' and table_schema='{1}'";
            sql = string.Format(sql, tableName.Trim(), dataBaseName);
            var dtData = MySQLHelper.ExecuteDataTable(sql);
            return GetTablePrimaryKey(dtData);
        }

        #endregion

        public static DataTable GetEnumCodeForMySQL(string tableName)
        {
            string sql = @" select table_name,column_name,column_type 
                            from information_schema.columns 
                            where table_name='{0}' and data_type='enum' and table_schema=@DataBase
                            order by ORDINAL_POSITION ";

            var dtData = DBHepler.MySQLHelper.ExecuteDataTableDataBaseParam(string.Format(sql, tableName));
            return dtData;
        }

        public static string GetDataBaseName(DataBaseType dataBaseType)
        {
            switch (dataBaseType)
            {
                case DataBaseType.SQLSERVER:
                    return SQLHelper.GetDataBaseName();
                case DataBaseType.ORACLE:
                    return OracleHelper.GetDataBaseName();
                case DataBaseType.MySQL:
                    return MySQLHelper.GetDataBaseName();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 取表字段属性 DataTable -> List
        /// </summary>
        private static List<TableColumnModel> GetFieldsFormDB(DataTable dataTable)
        {
            var list = new List<TableColumnModel>();
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drItem in dataTable.Rows)
                    {
                        var modelItem = new TableColumnModel();
                        modelItem.TableName = drItem["table_name"]?.ToString();
                        modelItem.TableComment = drItem["table_comments"]?.ToString();
                        modelItem.FieldName = drItem["column_name"]?.ToString();
                        modelItem.DataType = drItem["data_type"]?.ToString();
                        modelItem.FieldRemarks = drItem["column_comments"]?.ToString();
                        if (drItem["DataLength"] != null && drItem["DataLength"] != DBNull.Value
                            && int.Parse(drItem["DataLength"].ToString()) != 0)
                        {
                            modelItem.DataLength = int.Parse(drItem["DataLength"].ToString());
                        }
                        if (drItem["DataPrecision"] != null && drItem["DataPrecision"] != DBNull.Value
                            && int.Parse(drItem["DataPrecision"].ToString()) != 0)
                        {
                            modelItem.DataPrecision = int.Parse(drItem["DataPrecision"].ToString());
                        }
                        if (drItem["DataScale"] != null && drItem["DataScale"] != DBNull.Value
                            && int.Parse(drItem["DataScale"].ToString()) != 0)
                        {
                            modelItem.DataScale = int.Parse(drItem["DataScale"].ToString());
                        }
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

        /// <summary>
        /// 取表属性 DataTable -> List
        /// </summary>
        private static TableModel GetTabelInfoFormDB(DataTable dataTable)
        {
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drItem in dataTable.Rows)
                    {
                        var modelItem = new TableModel();
                        if (drItem["TableName"] != null && drItem["TableName"] != DBNull.Value)
                        {
                            modelItem.TableName = drItem["TableName"]?.ToString();
                        }
                        if (drItem["CreateDate"] != null && drItem["CreateDate"] != DBNull.Value)
                        {
                            modelItem.CreateDate = DateTime.Parse(drItem["CreateDate"].ToString());
                        }
                        if (drItem["LastUpdateDate"] != null && drItem["LastUpdateDate"] != DBNull.Value)
                        {
                            modelItem.LastUpdateDate = DateTime.Parse(drItem["LastUpdateDate"].ToString());
                        }
                        return modelItem;
                    }
                }
                return new TableModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static List<string> GetTablePrimaryKey(DataTable dtData)
        {
            var list = new List<string>();
            if (dtData != null && dtData.Rows.Count > 0)
            {
                foreach (DataRow item in dtData.Rows)
                {
                    list.Add(item.Field<string>("ColumnName"));
                }
            }
            return list;
        }
    }
}
