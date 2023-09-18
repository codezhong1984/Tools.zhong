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
    public class HubPlantDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        public bool Add(HubPlantClass model, OracleTransaction tran)
        {
            bool Add = false;
            string sql = @"INSERT INTO C_HUB_PLANT_T
                        ( GID,COMPANY,PLANT,PLANT_NAME,SAP_CLIENT,NOTE,CREATE_DATE,UPDATE_DATE)
                        VALUES
                        (:GID,:COMPANY,:PLANT,:PLANT_NAME,:SAP_CLIENT,:NOTE,:CREATE_DATE,:UPDATE_DATE)";
            OracleParameter[] par = new OracleParameter[] {
                				new OracleParameter(":GID",OracleType.VARCHAR2,50) { Value= Comm.ToDBValue(model.GID) },
                				new OracleParameter(":COMPANY",OracleType.VARCHAR2,25) { Value= Comm.ToDBValue(model.COMPANY) },
                				new OracleParameter(":PLANT",OracleType.VARCHAR2,4) { Value= Comm.ToDBValue(model.PLANT) },
                				new OracleParameter(":PLANT_NAME",OracleType.VARCHAR2,50) { Value= Comm.ToDBValue(model.PLANT_NAME) },
                				new OracleParameter(":SAP_CLIENT",OracleType.VARCHAR2,20) { Value= Comm.ToDBValue(model.SAP_CLIENT) },
                				new OracleParameter(":NOTE",OracleType.VARCHAR2,200) { Value= Comm.ToDBValue(model.NOTE) },
                				new OracleParameter(":CREATE_DATE",OracleType.DATE,7) { Value= Comm.ToDBValue(model.CREATE_DATE) },
                				new OracleParameter(":UPDATE_DATE",OracleType.DATE,7) { Value= Comm.ToDBValue(model.UPDATE_DATE) },
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
            string sql = @"DELETE FROM C_HUB_PLANT_T WHERE GID = :GID";
            OracleParameter[] par = new OracleParameter[] {
				new OracleParameter(":GID",OracleType.VarChar,50)
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
        public bool Update(HubPlantClass model, OracleTransaction tran)
        {
            bool Upd = false;
            string sql = @"UPDATE C_HUB_PLANT_T
                        SET GID = :GID,COMPANY = :COMPANY,PLANT = :PLANT,PLANT_NAME = :PLANT_NAME,SAP_CLIENT = :SAP_CLIENT,NOTE = :NOTE,CREATE_DATE = :CREATE_DATE,UPDATE_DATE = :UPDATE_DATE
                        WHERE GID = :GID";
            OracleParameter[] par = new OracleParameter[] {
                			new OracleParameter(":GID",OracleType.VARCHAR2,50) { Value= Comm.ToDBValue(model.GID) },
                			new OracleParameter(":COMPANY",OracleType.VARCHAR2,25) { Value= Comm.ToDBValue(model.COMPANY) },
                			new OracleParameter(":PLANT",OracleType.VARCHAR2,4) { Value= Comm.ToDBValue(model.PLANT) },
                			new OracleParameter(":PLANT_NAME",OracleType.VARCHAR2,50) { Value= Comm.ToDBValue(model.PLANT_NAME) },
                			new OracleParameter(":SAP_CLIENT",OracleType.VARCHAR2,20) { Value= Comm.ToDBValue(model.SAP_CLIENT) },
                			new OracleParameter(":NOTE",OracleType.VARCHAR2,200) { Value= Comm.ToDBValue(model.NOTE) },
                			new OracleParameter(":CREATE_DATE",OracleType.DATE,7) { Value= Comm.ToDBValue(model.CREATE_DATE) },
                			new OracleParameter(":UPDATE_DATE",OracleType.DATE,7) { Value= Comm.ToDBValue(model.UPDATE_DATE) },
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
            string sql = @"SELECT GID,COMPANY,PLANT,PLANT_NAME,SAP_CLIENT,NOTE,CREATE_DATE,UPDATE_DATE
                          FROM C_HUB_PLANT_T";
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
        public HubPlantClass GetSingle(string gid)
        {
            string sql = @"select GID,COMPANY,PLANT,PLANT_NAME,SAP_CLIENT,NOTE,CREATE_DATE,UPDATE_DATE 
                           from C_HUB_PLANT_T a 
                           where a.gid=:GID";

            OracleParameter[] par = new OracleParameter[]{
                new OracleParameter(":GID",OracleType.VarChar,50)
            };
            par[0].Value = gid;
            HubPlantClass model = new HubPlantClass();
            try
            {
                var dt = OracleHelper.ExecuteDataTable(sql, par);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dataRow = dt.Rows[0];
                    model.GID = dataRow.Field<VARCHAR2>("GID");
					model.COMPANY = dataRow.Field<VARCHAR2>("COMPANY");
					model.PLANT = dataRow.Field<VARCHAR2>("PLANT");
					model.PLANT_NAME = dataRow.Field<VARCHAR2>("PLANT_NAME");
					model.SAP_CLIENT = dataRow.Field<VARCHAR2>("SAP_CLIENT");
					model.NOTE = dataRow.Field<VARCHAR2>("NOTE");
					model.CREATE_DATE = dataRow.Field<DATE>("CREATE_DATE");
					model.UPDATE_DATE = dataRow.Field<DATE>("UPDATE_DATE"); 
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