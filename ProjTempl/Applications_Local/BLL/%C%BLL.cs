using PanelTracking.Applications_Local.DAL;
using PanelTracking.Applications_Local.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace PanelTracking.Applications_Local.BLL
{
    public class %C%BLL
    {
        %C%DAL dal = new %C%DAL();
        /// <summary>
        /// 添加
        /// </summary>
        public bool Add(%C%Class model, OracleTransaction tran)
        {
            return dal.Add(model, null);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(%C%Class model, OracleTransaction tran)
        {
            return dal.Update(model, null);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public DataTable GetDataTable(string sqlWhere, int PageIndex, int PageSize, ref int totalCount)
        {
            return dal.GetDataTable(sqlWhere, PageIndex, PageSize, ref totalCount);
        }

        /// <summary>
        /// 获取单条站点映射
        /// </summary>
        public %C%Class GetSingle(string gid)
        {
            return dal.GetSingle(gid);
        }

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public bool DeleteBatch(string[] gids)
        {
            return dal.DeleteBatch(gids);
        }
    }
}