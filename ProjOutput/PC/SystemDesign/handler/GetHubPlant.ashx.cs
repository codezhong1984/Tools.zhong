using DBHepler;
using PanelTracking.Applications_Local;
using PanelTracking.Applications_Local.BLL;
using PanelTracking.Applications_Local.Models;
using PanelTracking.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace PanelTracking.PC.SystemDesign.handler
{
    /// <summary>
    /// GetHubPlant的摘要说明
    /// </summary>
    public class GetHubPlant : IHttpHandler
    {

        HubPlantBLL bll = new HubPlantBLL();
        public void ProcessRequest(HttpContext context)
        {
            Comm.CultureInfo(context);//初始化语言
            context.Response.Clear();
            context.Response.ContentType = "text/palin";
            string action = context.Request.Params["action"].ToString();
            switch (action)
            {
                case "GetList":
                    context.Response.Write(GetList(context));
                    break;
                case "Add":
                    context.Response.Write(Add(context));
                    break;
                case "GetUpdateData":
                    context.Response.Write(GetUpdateData(context));
                    break;
                case "Update":
                    context.Response.Write(Update(context));
                    break;
                case "Delete":
                    context.Response.Write(Delete(context));
                    break;
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetList(HttpContext context)
        {
            string language = context.Request.Cookies["Login"].Values["LoginLanguage"].ToString();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            int pageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            int pageSize = ComConstant.PageSize;
            int recCount = 0;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<div class=\"boxTable table-responsive\">");
            strBuilder.Append(" <table class=\"table  table-alert wp100\" border=\"1\" bordercolor=\"#848484\" cellspacing=\"0\" cellpadding=\"0\">");
            strBuilder.Append(" <thead>");
            strBuilder.Append(" <tr>");
            strBuilder.AppendFormat("<th class=\"w80 clearpad\" style=\"height:40px;\"><label class=\"label-check\"><input type=\"checkbox\" id=\"checkAll\" name=\"checkAll\"/><span class=\"table-checkbox\"></span></label></th>");
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_GID);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_COMPANY);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_PLANT);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_PLANT_NAME);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_SAP_CLIENT);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_NOTE);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_CREATE_DATE);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.HubPlant_UPDATE_DATE);
            strBuilder.AppendFormat("<th class=\"w80 clearpad\">{0}</th>", Resources.Client.Operate);
            strBuilder.AppendFormat("</tr></thead>");
            strBuilder.Append("<tbody>");

            //DB中获取列表数据
            DataTable dt = bll.GetDataTable("", pageIndex, pageSize, ref recCount);

            if ((dt == null) || (dt.Rows.Count == 0))
            {
                strBuilder.Append("<tr><td  colspan=\"4\" style=\"text-align:center;\">No Data.</td></tr></tbody></table></div>");
                return strBuilder.ToString();
            }
            int PageCount = recCount / pageSize + ((recCount % pageSize != 0) ? 1 : 0);
            if (pageIndex > PageCount)
            {
                pageIndex = PageCount;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strBuilder.Append("<tr>");
                strBuilder.AppendFormat("<td class=\"clearpad \"><label class=\"label-check\"><input type=\"checkbox\"  name=\"select_item\"  id=\"select_item\" value=\"{0}\"/><span class=\"table-checkbox\"></span></label></td>", dt.Rows[i]["GID"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["GID"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["COMPANY"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["PLANT"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["PLANT_NAME"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["SAP_CLIENT"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["NOTE"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["CREATE_DATE"]);
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["UPDATE_DATE"]);
                strBuilder.AppendFormat("<td class=\"clearpad\"><button type=\"button\" class=\"btn btnQuery fl ml10\" id=\"btnUpd\" onclick=\"javascript:window.location.href='UpdateHubPlant.aspx?id={0}'\">{1}</button ></td>", dt.Rows[i]["GID"], Resources.Client.Edit);
                strBuilder.Append("</tr>");
            }
            strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");
            strBuilder.Append("</div>");
            strBuilder.Append(PageClass.GetListPager(PageCount, pageSize, recCount, pageIndex, "page"));
            return strBuilder.ToString();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Delete(HttpContext context)
        {
            string CheId = context.Request.Params["CheID"].ToString();
            string id = CheId.Substring(0, CheId.Length - 1);
            string[] ids = Regex.Split(id, ",", RegexOptions.IgnoreCase);
            string msg = "";
            if (bll.DeleteBatch(ids))
            {
                msg = "Success";
            }
            else
            {
                return msg = "Fail";
            }

            return msg;
        }
        // <summary>
        // 新增
        // </summary>
        // <param name = "context" ></ param >
        // <returns></returns >
        public string Add(HttpContext context)
        {
            string msg = "";
            string GID = context.Request.Params["GID"].ToString();
            string COMPANY = context.Request.Params["COMPANY"].ToString();
            string PLANT = context.Request.Params["PLANT"].ToString();
            string PLANT_NAME = context.Request.Params["PLANT_NAME"].ToString();
            string SAP_CLIENT = context.Request.Params["SAP_CLIENT"].ToString();
            string NOTE = context.Request.Params["NOTE"].ToString();
            string CREATE_DATE = context.Request.Params["CREATE_DATE"].ToString();
            string UPDATE_DATE = context.Request.Params["UPDATE_DATE"].ToString();
            string Gid = Guid.NewGuid().ToString();
            string GidNew = Gid.Replace("-", "");
            string createEmp = HttpUtility.UrlDecode(context.Request.Cookies["Login"].Values["LoginName"].ToString(), Encoding.GetEncoding("UTF-8"));
            HubPlantClass HubPlant = new HubPlantClass
            {

                GID = GidNew,
                GID= GID,
                COMPANY= COMPANY,
                PLANT= PLANT,
                PLANT_NAME= PLANT_NAME,
                SAP_CLIENT= SAP_CLIENT,
                NOTE= NOTE,
                CREATE_DATE= CREATE_DATE,
                UPDATE_DATE= UPDATE_DATE,
                OWNER = owner,
                CREATE_DATE = DateTime.Now,
                CREATE_BY = createEmp,
            };
            if (bll.Add(HubPlant, null))
            {
                msg = "Success";
            }
            else
            {
                msg = "Fail";
            }
            return msg;
        }
        /// <summary>
        /// 修改时获取物流时间
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetUpdateData(HttpContext context)
        {
            string id = context.Request.Params["id"].ToString();
            string language = context.Request.Cookies["Login"].Values["LoginLanguage"].ToString();
            HubPlantClass HubPlant = bll.GetSingle(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(HubPlant);
        }
        /// <summary>
        /// 修改物流时间
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Update(HttpContext context)
        {
            string msg = "";
            string GID = context.Request.Params["GID"].ToString();
            string COMPANY = context.Request.Params["COMPANY"].ToString();
            string PLANT = context.Request.Params["PLANT"].ToString();
            string PLANT_NAME = context.Request.Params["PLANT_NAME"].ToString();
            string SAP_CLIENT = context.Request.Params["SAP_CLIENT"].ToString();
            string NOTE = context.Request.Params["NOTE"].ToString();
            string CREATE_DATE = context.Request.Params["CREATE_DATE"].ToString();
            string UPDATE_DATE = context.Request.Params["UPDATE_DATE"].ToString();
            string Gid = context.Request.Params["gid"].ToString();
            string updateEmp = HttpUtility.UrlDecode(context.Request.Cookies["Login"].Values["LoginName"].ToString(), Encoding.GetEncoding("UTF-8"));
            HubPlantClass HubPlant = new HubPlantClass
            {
                GID= GID,
                COMPANY= COMPANY,
                PLANT= PLANT,
                PLANT_NAME= PLANT_NAME,
                SAP_CLIENT= SAP_CLIENT,
                NOTE= NOTE,
                CREATE_DATE= CREATE_DATE,
                UPDATE_DATE= UPDATE_DATE,
            };
            if (bll.Update(HubPlant, null))
            {
                msg = "Success";
            }
            else
            {
                msg = "Fail";
            }
            return msg;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}