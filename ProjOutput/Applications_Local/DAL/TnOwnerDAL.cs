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
}﻿using DBHepler;
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
    public class TnOwnerDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        public bool Add(TnOwnerClass model, OracleTransaction tran)
        {
            bool Add = false;
            string sql = @"INSERT INTO C_TnOwner_T
                        ( GID,SUPPLIER_CODE,SITE_CODE,KPSN_GID,FG_SN,FG_PN,BU_PLANT,T5_STARTDATE_1,T5_STARTDATE_2,T5_ENDDATE,IS_END,MATERIAL_NUMBER,MATERIAL_DESCRIPTION,PN_SIZE,INBOUND_TYPE,SAP_CLIENT_IN,INBOUND_DN,INBOUND_DN_ITEM,INBOUND_DATE,MOVEIN_TYPE,MOVEIN_TYPE_DESC,PLANT_CODE,STORE_LOC_IN,SAP_CLIENT_OUT,OUTBOUND_DN,OUTBOUND_DN_ITEM,OUTBOUND_DATE,MOVEOUT_TYPE,MOVEOUT_TYPE_DESC,PLANT_CODE_OUT,STORE_LOC_OUT,DC_SHIP_TO,TRANSFER_FLAG,TRANSFER_SITE,TRANSFER_DATE,SAP_CLIENT_BACK,BACK_DN,BACK_DN_ITEM,BACK_DATE,DEDUCT_DAYS,KPJOB_STATE,LAST_STORE_LOC,LAST_OP_TYPE,LAST_SESSION_GID,LAST_LOG_GID,LAST_OP_TIME,DEL_FLAG,DEL_REASON,CREATE_DATE,CREATE_BY,UPDATE_DATE,UPDATE_BY,IS_SYNC_OUT,SYNC_OUT_DATE,SYNC_OUT_MESSAGE,)
                        VALUES
                        (:GID:SUPPLIER_CODE:SITE_CODE:KPSN_GID:FG_SN:FG_PN:BU_PLANT:T5_STARTDATE_1:T5_STARTDATE_2:T5_ENDDATE:IS_END:MATERIAL_NUMBER:MATERIAL_DESCRIPTION:PN_SIZE:INBOUND_TYPE:SAP_CLIENT_IN:INBOUND_DN:INBOUND_DN_ITEM:INBOUND_DATE:MOVEIN_TYPE:MOVEIN_TYPE_DESC:PLANT_CODE:STORE_LOC_IN:SAP_CLIENT_OUT:OUTBOUND_DN:OUTBOUND_DN_ITEM:OUTBOUND_DATE:MOVEOUT_TYPE:MOVEOUT_TYPE_DESC:PLANT_CODE_OUT:STORE_LOC_OUT:DC_SHIP_TO:TRANSFER_FLAG:TRANSFER_SITE:TRANSFER_DATE:SAP_CLIENT_BACK:BACK_DN:BACK_DN_ITEM:BACK_DATE:DEDUCT_DAYS:KPJOB_STATE:LAST_STORE_LOC:LAST_OP_TYPE:LAST_SESSION_GID:LAST_LOG_GID:LAST_OP_TIME:DEL_FLAG:DEL_REASON:CREATE_DATE:CREATE_BY:UPDATE_DATE:UPDATE_BY:IS_SYNC_OUT:SYNC_OUT_DATE:SYNC_OUT_MESSAGE)";
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
            string sql = @"DELETE FROM C_TnOwner_T WHERE GID = :GID";
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
        public bool Update(TnOwnerClass model, OracleTransaction tran)
        {
            bool Upd = false;
            string sql = @"UPDATE C_TnOwner_T
                        SET GID = :GID, SUPPLIER_CODE = :SUPPLIER_CODE, SITE_CODE = :SITE_CODE, KPSN_GID = :KPSN_GID, FG_SN = :FG_SN, FG_PN = :FG_PN, BU_PLANT = :BU_PLANT, T5_STARTDATE_1 = :T5_STARTDATE_1, T5_STARTDATE_2 = :T5_STARTDATE_2, T5_ENDDATE = :T5_ENDDATE, IS_END = :IS_END, MATERIAL_NUMBER = :MATERIAL_NUMBER, MATERIAL_DESCRIPTION = :MATERIAL_DESCRIPTION, PN_SIZE = :PN_SIZE, INBOUND_TYPE = :INBOUND_TYPE, SAP_CLIENT_IN = :SAP_CLIENT_IN, INBOUND_DN = :INBOUND_DN, INBOUND_DN_ITEM = :INBOUND_DN_ITEM, INBOUND_DATE = :INBOUND_DATE, MOVEIN_TYPE = :MOVEIN_TYPE, MOVEIN_TYPE_DESC = :MOVEIN_TYPE_DESC, PLANT_CODE = :PLANT_CODE, STORE_LOC_IN = :STORE_LOC_IN, SAP_CLIENT_OUT = :SAP_CLIENT_OUT, OUTBOUND_DN = :OUTBOUND_DN, OUTBOUND_DN_ITEM = :OUTBOUND_DN_ITEM, OUTBOUND_DATE = :OUTBOUND_DATE, MOVEOUT_TYPE = :MOVEOUT_TYPE, MOVEOUT_TYPE_DESC = :MOVEOUT_TYPE_DESC, PLANT_CODE_OUT = :PLANT_CODE_OUT, STORE_LOC_OUT = :STORE_LOC_OUT, DC_SHIP_TO = :DC_SHIP_TO, TRANSFER_FLAG = :TRANSFER_FLAG, TRANSFER_SITE = :TRANSFER_SITE, TRANSFER_DATE = :TRANSFER_DATE, SAP_CLIENT_BACK = :SAP_CLIENT_BACK, BACK_DN = :BACK_DN, BACK_DN_ITEM = :BACK_DN_ITEM, BACK_DATE = :BACK_DATE, DEDUCT_DAYS = :DEDUCT_DAYS, KPJOB_STATE = :KPJOB_STATE, LAST_STORE_LOC = :LAST_STORE_LOC, LAST_OP_TYPE = :LAST_OP_TYPE, LAST_SESSION_GID = :LAST_SESSION_GID, LAST_LOG_GID = :LAST_LOG_GID, LAST_OP_TIME = :LAST_OP_TIME, DEL_FLAG = :DEL_FLAG, DEL_REASON = :DEL_REASON, CREATE_DATE = :CREATE_DATE, CREATE_BY = :CREATE_BY, UPDATE_DATE = :UPDATE_DATE, UPDATE_BY = :UPDATE_BY, IS_SYNC_OUT = :IS_SYNC_OUT, SYNC_OUT_DATE = :SYNC_OUT_DATE, SYNC_OUT_MESSAGE = :SYNC_OUT_MESSAGE, 
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
            string sql = @"SELECT GID,SUPPLIER_CODE,SITE_CODE,KPSN_GID,FG_SN,FG_PN,BU_PLANT,T5_STARTDATE_1,T5_STARTDATE_2,T5_ENDDATE,IS_END,MATERIAL_NUMBER,MATERIAL_DESCRIPTION,PN_SIZE,INBOUND_TYPE,SAP_CLIENT_IN,INBOUND_DN,INBOUND_DN_ITEM,INBOUND_DATE,MOVEIN_TYPE,MOVEIN_TYPE_DESC,PLANT_CODE,STORE_LOC_IN,SAP_CLIENT_OUT,OUTBOUND_DN,OUTBOUND_DN_ITEM,OUTBOUND_DATE,MOVEOUT_TYPE,MOVEOUT_TYPE_DESC,PLANT_CODE_OUT,STORE_LOC_OUT,DC_SHIP_TO,TRANSFER_FLAG,TRANSFER_SITE,TRANSFER_DATE,SAP_CLIENT_BACK,BACK_DN,BACK_DN_ITEM,BACK_DATE,DEDUCT_DAYS,KPJOB_STATE,LAST_STORE_LOC,LAST_OP_TYPE,LAST_SESSION_GID,LAST_LOG_GID,LAST_OP_TIME,DEL_FLAG,DEL_REASON,CREATE_DATE,CREATE_BY,UPDATE_DATE,UPDATE_BY,IS_SYNC_OUT,SYNC_OUT_DATE,SYNC_OUT_MESSAGE,
                          FROM C_TnOwner_T";
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
        public TnOwnerClass GetSingle(string gid)
        {
            string sql = @"select  GID,SUPPLIER_CODE,SITE_CODE,KPSN_GID,FG_SN,FG_PN,BU_PLANT,T5_STARTDATE_1,T5_STARTDATE_2,T5_ENDDATE,IS_END,MATERIAL_NUMBER,MATERIAL_DESCRIPTION,PN_SIZE,INBOUND_TYPE,SAP_CLIENT_IN,INBOUND_DN,INBOUND_DN_ITEM,INBOUND_DATE,MOVEIN_TYPE,MOVEIN_TYPE_DESC,PLANT_CODE,STORE_LOC_IN,SAP_CLIENT_OUT,OUTBOUND_DN,OUTBOUND_DN_ITEM,OUTBOUND_DATE,MOVEOUT_TYPE,MOVEOUT_TYPE_DESC,PLANT_CODE_OUT,STORE_LOC_OUT,DC_SHIP_TO,TRANSFER_FLAG,TRANSFER_SITE,TRANSFER_DATE,SAP_CLIENT_BACK,BACK_DN,BACK_DN_ITEM,BACK_DATE,DEDUCT_DAYS,KPJOB_STATE,LAST_STORE_LOC,LAST_OP_TYPE,LAST_SESSION_GID,LAST_LOG_GID,LAST_OP_TIME,DEL_FLAG,DEL_REASON,CREATE_DATE,CREATE_BY,UPDATE_DATE,UPDATE_BY,IS_SYNC_OUT,SYNC_OUT_DATE,SYNC_OUT_MESSAGE, 
                           from C_TnOwner_T a 
                           where a.gid=:GID";

            OracleParameter[] par = new OracleParameter[]{
                new OracleParameter(":GID",OracleType.VarChar,50)
            };
            par[0].Value = gid;
            TnOwnerClass model = new TnOwnerClass();
            try
            {
                var dt = OracleHelper.ExecuteDataTable(sql, par);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dataRow = dt.Rows[0];
                    model.GID = dataRow.Field<VARCHAR2>("GID");model.SUPPLIER_CODE = dataRow.Field<VARCHAR2>("SUPPLIER_CODE");model.SITE_CODE = dataRow.Field<VARCHAR2>("SITE_CODE");model.KPSN_GID = dataRow.Field<VARCHAR2>("KPSN_GID");model.FG_SN = dataRow.Field<VARCHAR2>("FG_SN");model.FG_PN = dataRow.Field<VARCHAR2>("FG_PN");model.BU_PLANT = dataRow.Field<VARCHAR2>("BU_PLANT");model.T5_STARTDATE_1 = dataRow.Field<DATE>("T5_STARTDATE_1");model.T5_STARTDATE_2 = dataRow.Field<DATE>("T5_STARTDATE_2");model.T5_ENDDATE = dataRow.Field<DATE>("T5_ENDDATE");model.IS_END = dataRow.Field<NUMBER>("IS_END");model.MATERIAL_NUMBER = dataRow.Field<VARCHAR2>("MATERIAL_NUMBER");model.MATERIAL_DESCRIPTION = dataRow.Field<VARCHAR2>("MATERIAL_DESCRIPTION");model.PN_SIZE = dataRow.Field<VARCHAR2>("PN_SIZE");model.INBOUND_TYPE = dataRow.Field<VARCHAR2>("INBOUND_TYPE");model.SAP_CLIENT_IN = dataRow.Field<VARCHAR2>("SAP_CLIENT_IN");model.INBOUND_DN = dataRow.Field<VARCHAR2>("INBOUND_DN");model.INBOUND_DN_ITEM = dataRow.Field<NUMBER>("INBOUND_DN_ITEM");model.INBOUND_DATE = dataRow.Field<DATE>("INBOUND_DATE");model.MOVEIN_TYPE = dataRow.Field<VARCHAR2>("MOVEIN_TYPE");model.MOVEIN_TYPE_DESC = dataRow.Field<VARCHAR2>("MOVEIN_TYPE_DESC");model.PLANT_CODE = dataRow.Field<VARCHAR2>("PLANT_CODE");model.STORE_LOC_IN = dataRow.Field<VARCHAR2>("STORE_LOC_IN");model.SAP_CLIENT_OUT = dataRow.Field<VARCHAR2>("SAP_CLIENT_OUT");model.OUTBOUND_DN = dataRow.Field<VARCHAR2>("OUTBOUND_DN");model.OUTBOUND_DN_ITEM = dataRow.Field<NUMBER>("OUTBOUND_DN_ITEM");model.OUTBOUND_DATE = dataRow.Field<DATE>("OUTBOUND_DATE");model.MOVEOUT_TYPE = dataRow.Field<VARCHAR2>("MOVEOUT_TYPE");model.MOVEOUT_TYPE_DESC = dataRow.Field<VARCHAR2>("MOVEOUT_TYPE_DESC");model.PLANT_CODE_OUT = dataRow.Field<VARCHAR2>("PLANT_CODE_OUT");model.STORE_LOC_OUT = dataRow.Field<VARCHAR2>("STORE_LOC_OUT");model.DC_SHIP_TO = dataRow.Field<VARCHAR2>("DC_SHIP_TO");model.TRANSFER_FLAG = dataRow.Field<NUMBER>("TRANSFER_FLAG");model.TRANSFER_SITE = dataRow.Field<VARCHAR2>("TRANSFER_SITE");model.TRANSFER_DATE = dataRow.Field<DATE>("TRANSFER_DATE");model.SAP_CLIENT_BACK = dataRow.Field<VARCHAR2>("SAP_CLIENT_BACK");model.BACK_DN = dataRow.Field<VARCHAR2>("BACK_DN");model.BACK_DN_ITEM = dataRow.Field<NUMBER>("BACK_DN_ITEM");model.BACK_DATE = dataRow.Field<DATE>("BACK_DATE");model.DEDUCT_DAYS = dataRow.Field<NUMBER>("DEDUCT_DAYS");model.KPJOB_STATE = dataRow.Field<VARCHAR2>("KPJOB_STATE");model.LAST_STORE_LOC = dataRow.Field<VARCHAR2>("LAST_STORE_LOC");model.LAST_OP_TYPE = dataRow.Field<VARCHAR2>("LAST_OP_TYPE");model.LAST_SESSION_GID = dataRow.Field<VARCHAR2>("LAST_SESSION_GID");model.LAST_LOG_GID = dataRow.Field<VARCHAR2>("LAST_LOG_GID");model.LAST_OP_TIME = dataRow.Field<DATE>("LAST_OP_TIME");model.DEL_FLAG = dataRow.Field<NUMBER>("DEL_FLAG");model.DEL_REASON = dataRow.Field<VARCHAR2>("DEL_REASON");model.CREATE_DATE = dataRow.Field<DATE>("CREATE_DATE");model.CREATE_BY = dataRow.Field<VARCHAR2>("CREATE_BY");model.UPDATE_DATE = dataRow.Field<DATE>("UPDATE_DATE");model.UPDATE_BY = dataRow.Field<VARCHAR2>("UPDATE_BY");model.IS_SYNC_OUT = dataRow.Field<NUMBER>("IS_SYNC_OUT");model.SYNC_OUT_DATE = dataRow.Field<DATE>("SYNC_OUT_DATE");model.SYNC_OUT_MESSAGE = dataRow.Field<VARCHAR2>("SYNC_OUT_MESSAGE"); 
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