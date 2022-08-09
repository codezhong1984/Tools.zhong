using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Text;

namespace DBHepler
{
    /// <summary>
    ///abstract 抽象类 修饰符指示所修饰的内容缺少实现或未完全实现。 
    ///abstract 修饰符可用于类、方法、属性、索引器和事件。 
    ///在类声明中使用 abstract 修饰符以指示某个类只能是其他类的基类。
    ///标记为抽象或包含在抽象类中的成员必须通过从抽象类派生的类来实现。
    /// A helper class used to execute queries against an Oracle database
    /// </summary>
    public abstract class OracleHelper
    {
        //Create a hashtable for the parameter cached
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 创建连接对象（默认连上PTS数据库）
        /// </summary>
        /// <returns></returns>
        public static OracleConnection GetOracleConnection()
        {
            string connectString = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;
            return new OracleConnection(connectString);
        }

        public static OracleTransaction BeginTransaction()  //todo tobe test
        {
            OracleConnection conn = GetOracleConnection();
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            return trans;
        }

        /// <summary>  
        /// 执行数据库查询操作,返回DataTable类型的结果集  
        /// </summary>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param>  
        /// <returns>当前查询操作返回的DataTable类型的结果集</returns>  
        public static DataTable ExecuteDataTable(string cmdText, params OracleParameter[] commandParameters)
        {
            return ExecuteDataTable(null, cmdText, commandParameters);
        }

        /// <summary>  
        /// 执行数据库查询操作,返回DataTable类型的结果集  
        /// </summary>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param>  
        /// <returns>当前查询操作返回的DataTable类型的结果集</returns>  
        public static DataTable ExecuteDataTable(OracleTransaction trans, string cmdText, params OracleParameter[] commandParameters)
        {
            bool bNewConnection = trans == null;
            OracleConnection conn = bNewConnection ? GetOracleConnection() : trans.Connection;

            OracleCommand cmd = new OracleCommand();
            DataTable table = null;
            try
            {
                PrepareCommand(cmd, conn, trans, CommandType.Text, cmdText, commandParameters);
                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.TableMappings.Add("", "");
                adapter.SelectCommand = cmd;
                table = new DataTable();
                adapter.Fill(table);
                cmd.Parameters.Clear();
            }
            finally
            {
                cmd.Dispose();
                if (bNewConnection)
                {
                    conn.Dispose();
                }
            }
            return table;
        }

        public static DataTable ExecuteDataTablePaging(int pageIndex, int pageSize, bool retTotalCount, ref int totalCount, OracleTransaction trans, string cmdText, params OracleParameter[] commandParameters)
        {
            if (retTotalCount)
            {
                totalCount = int.Parse(ExecuteScalar(trans, CommandType.Text, string.Format("select count(0) cnt from ({0}) ", cmdText), commandParameters).ToString());
            }

            return ExecuteDataTable(trans
                , string.Format("select * from ( select rownum \"#n\",t.* from ( {0} ) t) t where t.\"#n\"<= {1} and t.\"#n\" > {2}", cmdText, pageSize * pageIndex, (pageIndex - 1) * pageSize)
                , commandParameters);
        }

        /// <summary>  
        /// 执行数据库查询操作,返回结果集中位于第一行第一列的Object类型的值  
        /// </summary>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param>  
        /// <returns>当前查询操作返回的结果集中位于第一行第一列的Object类型的值</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            return ExecuteScalar(null, cmdType, cmdText, commandParameters);
        }
               
        ///    <summary>  
        ///    执行数据库事务查询操作,返回结果集中位于第一行第一列的Object类型的值  
        ///    </summary>  
        ///    <param name="transaction">一个已存在的数据库事务对象</param>  
        ///    <param name="commandType">命令类型</param>  
        ///    <param name="commandText">Oracle存储过程名称或PL/SQL命令</param>  
        ///    <param name="commandParameters">命令参数集合</param>  
        ///    <returns>当前事务查询操作返回的结果集中位于第一行第一列的Object类型的值</returns> 
        public static object ExecuteScalar(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            bool bNewConnection = trans == null;
            OracleConnection conn = bNewConnection ? GetOracleConnection() : trans.Connection;

            OracleCommand cmd = new OracleCommand();
            object result = null;
            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, commandParameters);
                result = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            finally
            {
                cmd.Dispose();
                if (bNewConnection)
                {
                    conn.Dispose();
                }
            }
            return result;
        }
        

        /// <summary>  
        /// 执行数据库查询操作,返回受影响的行数  
        /// </summary>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param>  
        /// <returns>当前查询操作影响的数据行数</returns> 
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            return ExecuteNonQuery(null, cmdType, cmdText, commandParameters);
        }

        /// <summary>  
        /// 执行数据库事务查询操作,返回受影响的行数  
        /// </summary>  
        /// <param name="trans">数据库事务对象</param>  
        /// <param name="cmdType">Command类型</param>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param>  
        /// <returns>当前事务查询操作影响的数据行数</returns> 
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            bool bNewConnection = trans == null;
            OracleConnection conn = bNewConnection ? GetOracleConnection() : trans.Connection;

            OracleCommand cmd = new OracleCommand();
            int result = 0;
            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, commandParameters);
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            finally
            {
                cmd.Dispose();
                if (bNewConnection)
                {
                    conn.Dispose();
                }
            }
            return result;
        }

        /// <summary>  
        /// 执行数据库查询操作,返回OracleDataReader类型的内存结果集  
        /// </summary>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param>  
        /// <returns>当前查询操作返回的OracleDataReader类型的内存结果集</returns>
        public static OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            return ExecuteReader(null, cmdType, cmdText, commandParameters);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            bool bNewConnection = trans == null;
            OracleConnection conn = bNewConnection ? GetOracleConnection() : trans.Connection;

            OracleCommand cmd = new OracleCommand();
            OracleDataReader reader = null;
            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, commandParameters);
                reader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
            }
            finally
            {
                cmd.Dispose();
                if (bNewConnection)
                {
                    conn.Dispose();
                }
            }
            return reader;
        }

        /// <summary>  
        /// 执行数据库命令前的准备工作  
        /// </summary>  
        /// <param name="command">Command对象</param>  
        /// <param name="connection">数据库连接对象</param>  
        /// <param name="trans">事务对象</param>  
        /// <param name="cmdType">Command类型</param>  
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>  
        /// <param name="commandParameters">命令参数集合</param> 
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        #region parameter relative
        /// <summary>
        /// Add a set of parameters to the cached
        /// </summary>
        /// <param name="cacheKey">Key value to look up the parameters</param>
        /// <param name="commandParameters">Actual parameters to cached</param>
        public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Fetch parameters from the cache
        /// </summary>
        /// <param name="cacheKey">Key to look up the parameters</param>
        /// <returns></returns>
        public static OracleParameter[] GetCachedParameters(string cacheKey)
        {
            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            // If the parameters are in the cache
            OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

            // return a copy of the parameters
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static string OraBit(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static bool OraBool(string value)
        {
            if (value.Equals("Y"))
                return true;
            else
                return false;
        }

        /// <summary>  
        /// 将.NET日期时间类型转化为Oracle兼容的日期时间格式字符串  
        /// </summary>  
        /// <param name="date">.NET日期时间类型对象</param>  
        /// <returns>Oracle兼容的日期时间格式字符串（如该字符串：TO_DATE('2014-04-10','YYYY-MM-DD')）</returns>  
        internal static string GetOracleDateFormat(DateTime date)
        {
            return "TO_DATE('" + date.ToString("yyyy-M-dd") + "','YYYY-MM-DD')";
        }

        /// <summary>  
        /// 将.NET日期时间类型转化为Oracle兼容的日期格式字符串  
        /// </summary>  
        /// <param name="date">.NET日期时间类型对象</param>  
        /// <param name="format">Oracle日期时间类型格式化限定符</param>  
        /// <returns>Oracle兼容的日期时间格式字符串（如该字符串：TO_DATE('2014-04-10','YYYY-MM-DD')）</returns>  
        internal static string GetOracleDateFormat(DateTime date, string format)
        {
            if (format == null || format.Trim() == "") format = "YYYY-MM-DD";
            return "TO_DATE('" + date.ToString("yyyy-M-dd") + "','" + format + "')";
        }

        /// <summary>  
        /// 将指定的关键字处理为模糊查询时的合法参数值  
        /// </summary>  
        /// <param name="source">待处理的查询关键字</param>  
        /// <returns>过滤后的查询关键字</returns>  
        internal static string HandleLikeKey(string source)
        {
            if (source == null || source.Trim() == "") return null;

            source = source.Replace("[", "[]]");
            source = source.Replace("_", "[_]");
            source = source.Replace("%", "[%]");

            return ("%" + source + "%");
        }
        #endregion

        #region CLOB relative
        /// <summary>  
        /// 将文本内容写入到数据库的CLOB字段中（不可用：报连接被关闭的异常）  
        /// </summary>  
        /// <param name="connectionString">数据库连接字符串</param>  
        /// <param name="table">数据库表名称</param>  
        /// <param name="where">指定的WHERE条件语句</param>  
        /// <param name="clobField">CLOB字段的名称</param>  
        /// <param name="content">要写入的文本内容</param>  
        internal static void WriteCLOB(string table, string where, string clobField, string content)
        {
            if (String.IsNullOrEmpty(table) || String.IsNullOrEmpty(clobField)) return;

            using (OracleConnection connection = GetOracleConnection())
            {
                OracleCommand command = null;

                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT " + clobField + " FROM " + table + " WHERE " + where + " FOR UPDATE";
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        command.Transaction = command.Connection.BeginTransaction();

                        OracleLob lob = reader.GetOracleLob(0);
                        byte[] buffer = Encoding.Unicode.GetBytes(content);
                        if (lob != OracleLob.Null) lob.Erase();
                        lob.Write(buffer, 0, ((buffer.Length % 2 == 0) ? buffer.Length : (buffer.Length - 1)));

                        command.Transaction.Commit();
                        reader.Close();
                    }
                }
                catch
                {
                    command.Transaction.Rollback();
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>  
        /// 从数据库中读取CLOB字段的内容并进行输出  
        /// </summary>  
        /// <param name="connectionString">数据库连接字符串</param>  
        /// <param name="table">数据库表名称</param>  
        /// <param name="where">指定的WHERE条件语句</param>  
        /// <param name="clobField">CLOB字段的名称</param>  
        /// <param name="output">保存内容输出的字符串变量</param>  
        internal static void ReadCLOB(string table, string where, string clobField, ref string output)
        {
            if (String.IsNullOrEmpty(table) || String.IsNullOrEmpty(clobField)) return;

            using (OracleConnection connection = GetOracleConnection())
            {
                OracleCommand command = null;
                StreamReader stream = null;

                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT " + clobField + " FROM " + table + " WHERE " + where;
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        command.Transaction = command.Connection.BeginTransaction();

                        OracleLob lob = reader.GetOracleLob(0);
                        if (lob != OracleLob.Null)
                        {
                            stream = new StreamReader(lob, Encoding.Unicode);
                            output = stream.ReadToEnd().Trim();
                            command.Transaction.Commit();
                            reader.Close();
                        }
                    }
                }
                catch
                {
                    command.Transaction.Rollback();
                    throw;
                }
                finally
                {
                    stream.Close();
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        #endregion
    }
}