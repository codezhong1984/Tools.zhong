using DBHepler;
using PanelTracking.Applications_Local.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using PanelTracking.Common;

namespace PanelTracking.Applications_Local.DAL
{
    public class %C%DAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        public bool Add(%C%Class model, OracleTransaction tran)
        {
            bool Add = false;
            string sql = @"INSERT INTO %T%
                        ( %LP%$1,%ELP%)
                        VALUES
                        (%LP%:$1%ELP%)";
            OracleParameter[] par = new OracleParameter[] {
%LP%
                new OracleParameter(":$1",OracleType.$4,$2) { Value= Comm.ToDBValue(model.$1) },
%ELP%
            };
            try
            {
                Add = 0 < OracleHelper.ExecuteNonQuery(tran, CommandType.Text, sql, par);
            }
            catch (Exception ex)
            {

                Error.Log(ex.ToString());
                throw;
            }
            return Add;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string gid, OracleTransaction tran)
        {
            bool resultFlag = false;
            string sql = @"DELETE FROM %T% WHERE GID = :GID";
            OracleParameter[] par = new OracleParameter[] {
	 new OracleParameter(":GID",OracleType.VarChar,50)new OracleParameter(":GID",OracleType.VarChar,50)
            };
            par[0].Value = gid;
            try
            {
                resultFlag = 0 < OracleHelper.ExecuteNonQuery(tran, CommandType.Text, sql, par);
            }
            catch (Exception ex)
            {
                Error.Log(ex.ToString());
                throw;
            }
            return resultFlag;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(%C%Class model, OracleTransaction tran)
        {
            bool Upd = false;
            string sql = @"UPDATE %T%
                        SET %LP%$1 = :$1, %ELP%
                        WHERE GID = :GID";
            OracleParameter[] par = new OracleParameter[] {
%LP%
                new OracleParameter(":$1",OracleType.$4,$2) { Value= Comm.ToDBValue(model.$1) },
%ELP%
            };
            try
            {
                Upd = 0 < OracleHelper.ExecuteNonQuery(tran, CommandType.Text, sql, par);
            }
            catch (Exception ex)
            {
                Error.Log(ex.ToString());
                throw;
            }
            return Upd;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public DataTable GetDataTable(string sqlWhere, int PageIndex, int PageSize, ref int totalCount)
        {
            DataTable dt = null;
            string sql = @"SELECT %LP%$1,%ELP%
                          FROM %T%";
            try
            {
                dt = OracleHelper.ExecuteDataTablePaging(PageIndex, PageSize, true, ref totalCount, null, sql, null);
            }
            catch (Exception ex)
            {
                Error.Log(ex.ToString());
                throw;
            }
            return dt;
        }

        /// <summary>
        /// 获取单条站点映射
        /// </summary>
        public %C%Class GetSingle(string gid)
        {
            string sql = @"select  %LP%$1,%ELP% 
                           from %T% a 
                           where a.gid=:GID";

            OracleParameter[] par = new OracleParameter[]{
                new OracleParameter(":GID",OracleType.VarChar,50)
            };
            par[0].Value = gid;
            %C%Class model = new %C%Class();
            try
            {
                var dt = OracleHelper.ExecuteDataTable(sql, par);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dataRow = dt.Rows[0];
                    %LP%model.$1 = dataRow.Field<$4>("$1");%ELP% 
                }
            }
            catch (Exception ex)
            {
                Error.Log(ex.ToString());
                throw;
            }
            return model;
        }

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public bool DeleteBatch(string[] gids)
        {
            if (gids == null || gids.Length == 0)
            {
                return true;
            }
            bool result = true;
            using (OracleConnection connection = OracleHelper.GetOracleConnection())
            {
                connection.Open();
                using (OracleTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var gid in gids)
                        {
                            result = Delete(gid, tran) && result;
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Error.Log(ex.ToString());
                        throw;
                    }
                }
            }
            return result;
        }
    }
}