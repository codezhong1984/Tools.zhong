using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Common;
using System.Text;
using System.IO;

namespace DBHepler
{
    /// <summary>
    /// TPV数据库操作层DAL调用
    /// </summary>
    public abstract class MySQLHelper
    {
        /// <summary>
        /// 从连接字符串中获取数据库名
        /// </summary>
        /// <returns></returns>
        public static string GetDataBaseName()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            builder.ConnectionString = connectionString;
            string databaseName = builder["Initial Catalog"] as string;
            return databaseName;
        }

        //数据库连接字符串        

        public static MySqlConnection GetMySqlConnection()
        {
            string connectString = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
            return new MySqlConnection(connectString);
        }

      
        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception Ex)
                {
                    return 0;
                }

            }
        }

        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="conn">一个现有的数据库连接</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用现有的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据集</returns>
        public static DataSet ExecuteDataSet(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            MySqlDataAdapter adt = new MySqlDataAdapter();
            adt.SelectCommand = cmd;
            MySqlCommandBuilder cbd = new MySqlCommandBuilder(adt);
            DataSet ds = new DataSet();
            try
            {
                adt.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        /// <summary>
        /// 用现有的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据集</returns>
        public static DataSet ExecuteDataSet(string connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter adt = new MySqlDataAdapter();
                adt.SelectCommand = cmd;
                MySqlCommandBuilder cbd = new MySqlCommandBuilder(adt);
                DataSet ds = new DataSet();
                try
                {
                    adt.Fill(ds);
                    return ds;
                }
                catch(Exception ex)
                {
                    return null;
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// 用现有的数据库连接执行一个返回数据表的sql命令
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据表</returns>
        public static DataTable ExecuteDataTable(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            MySqlDataAdapter adt = new MySqlDataAdapter();
            adt.SelectCommand = cmd;
            MySqlCommandBuilder cbd = new MySqlCommandBuilder(adt);
            DataSet ds = new DataSet();
            try
            {
                adt.Fill(ds);
                return ds.Tables[0];
            }
            catch
            {
                return new DataTable();
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        /// <summary>
        /// 用现有的数据库连接执行一个返回数据表的sql命令
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据表</returns>
        public static DataTable ExecuteDataTable(string connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter adt = new MySqlDataAdapter();
                adt.SelectCommand = cmd;
                //adt.
                MySqlCommandBuilder cbd = new MySqlCommandBuilder(adt);
                DataSet ds = new DataSet();
                try
                {
                    adt.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception Ex)
                {
                    return new DataTable();
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
        }

        public static DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            using (MySqlConnection conn = GetMySqlConnection())
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter adt = new MySqlDataAdapter();
                adt.SelectCommand = cmd;
                MySqlCommandBuilder cbd = new MySqlCommandBuilder(adt);//???
                DataSet ds = new DataSet();
                try
                {
                    adt.Fill(ds);
                    return ds.Tables[0];
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
        }

        public static DataTable ExecuteDataTable(string cmdText)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            using (MySqlConnection conn = GetMySqlConnection())
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                MySqlDataAdapter adt = new MySqlDataAdapter();
                adt.SelectCommand = cmd;
                MySqlCommandBuilder cbd = new MySqlCommandBuilder(adt);//???
                DataSet ds = new DataSet();
                try
                {
                    adt.Fill(ds);
                    return ds.Tables[0];
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                try
                {
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception Ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <param name="conn">一个存在的数据库连接</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="cmdParms">一个将要添加到缓存的sql参数集合</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 找回缓存参数集合
        /// </summary>
        /// <param name="cacheKey">用于找回参数的关键字</param>
        /// <returns>缓存的参数集合</returns>
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }


        public static DataTable ExecuteDataTableDataBaseParam(string cmdText)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            
            using (MySqlConnection conn = GetMySqlConnection())
            {
                var dbParam = new MySqlParameter("@Database", conn.Database);
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, new MySqlParameter[] { dbParam} );
                MySqlDataAdapter adt = new MySqlDataAdapter();
                adt.SelectCommand = cmd;
                DataTable dt = new DataTable();
                try
                {
                    adt.Fill(dt);
                    return dt;
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
        }


        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">Sql连接</param>
        /// <param name="trans">Sql事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static object GetSingle(string connectionString, string cmdText, params MySqlParameter[] commandParameters)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.Clear();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataTable GetDataTable(string connectionString, string cmdText, params MySqlParameter[] commandParameters)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    DataTable table = null;
                    cmd.CommandText = cmdText;
                    var adapter = new MySqlDataAdapter();
                    adapter.TableMappings.Add("", "");
                    adapter.SelectCommand = cmd;
                    table = new DataTable();
                    adapter.Fill(table);
                    cmd.Parameters.Clear();
                    return table;
                }
            }
        }

        ///大批量数据插入,返回成功插入行数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据表</param>
        /// <returns>返回成功插入行数</returns>
        public static int BatchInsertDataTable(string connectionString, string targetTableName, DataTable dtData)
        {
            int insertCount = 0;
            string tmpPath = Path.GetTempFileName();
            string csv = DataTableToCsv(dtData);
            File.WriteAllText(tmpPath, csv);
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    //tran = conn.BeginTransaction();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        NumberOfLinesToSkip = 0,
                        TableName = targetTableName,                       
                    };
                    insertCount = bulk.Load();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            }
            File.Delete(tmpPath);
            return insertCount;
        }

        ///将DataTable转换为标准的CSV
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>返回标准的CSV</returns>
        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}