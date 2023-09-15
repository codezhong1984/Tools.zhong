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
    /// Get%C%的摘要说明
    /// </summary>
    public class Get%C% : IHttpHandler
    {

        %C%BLL bll = new %C%BLL();
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
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.Stage);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.%C%);
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
%LP%
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["$1"]);
%ELP%
                strBuilder.AppendFormat("<td class=\"clearpad\"><button type=\"button\" class=\"btn btnQuery fl ml10\" id=\"btnUpd\" onclick=\"javascript:window.location.href='Update%C%.aspx?id={0}'\">{1}</button ></td>", dt.Rows[i]["GID"], Resources.Client.Edit);
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
%LP%
            string $1 = context.Request.Params["$1"].ToString();
%ELP%
            string Gid = Guid.NewGuid().ToString();
            string GidNew = Gid.Replace("-", "");
            string createEmp = HttpUtility.UrlDecode(context.Request.Cookies["Login"].Values["LoginName"].ToString(), Encoding.GetEncoding("UTF-8"));
            %C%Class %C% = new %C%Class
            {

                GID = GidNew,
%LP%
                $1= $1,
%ELP%
                OWNER = owner,
                CREATE_DATE = DateTime.Now,
                CREATE_BY = createEmp,
            };
            if (bll.Add(%C%, null))
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
            %C%Class %C% = bll.GetSingle(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(%C%);
        }
        /// <summary>
        /// 修改物流时间
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Update(HttpContext context)
        {
            string msg = "";
%LP%
            string $1 = context.Request.Params["$1"].ToString();
%ELP%
            string Gid = context.Request.Params["gid"].ToString();
            string updateEmp = HttpUtility.UrlDecode(context.Request.Cookies["Login"].Values["LoginName"].ToString(), Encoding.GetEncoding("UTF-8"));
            %C%Class %C% = new %C%Class
            {
%LP%
                $1= $1,
%ELP%
            };
            if (bll.Update(%C%, null))
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
}﻿using DBHepler;
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
    /// GetTnOwner的摘要说明
    /// </summary>
    public class GetTnOwner : IHttpHandler
    {

        TnOwnerBLL bll = new TnOwnerBLL();
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
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.Stage);
            strBuilder.AppendFormat("<th>{0}</th>", Resources.Client.TnOwner);
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
%LP%
                strBuilder.AppendFormat("<td>{0}</td>", dt.Rows[i]["$1"]);
%ELP%
                strBuilder.AppendFormat("<td class=\"clearpad\"><button type=\"button\" class=\"btn btnQuery fl ml10\" id=\"btnUpd\" onclick=\"javascript:window.location.href='UpdateTnOwner.aspx?id={0}'\">{1}</button ></td>", dt.Rows[i]["GID"], Resources.Client.Edit);
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
%LP%
            string $1 = context.Request.Params["$1"].ToString();
%ELP%
            string Gid = Guid.NewGuid().ToString();
            string GidNew = Gid.Replace("-", "");
            string createEmp = HttpUtility.UrlDecode(context.Request.Cookies["Login"].Values["LoginName"].ToString(), Encoding.GetEncoding("UTF-8"));
            TnOwnerClass TnOwner = new TnOwnerClass
            {

                GID = GidNew,
%LP%
                $1= $1,
%ELP%
                OWNER = owner,
                CREATE_DATE = DateTime.Now,
                CREATE_BY = createEmp,
            };
            if (bll.Add(TnOwner, null))
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
            TnOwnerClass TnOwner = bll.GetSingle(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(TnOwner);
        }
        /// <summary>
        /// 修改物流时间
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Update(HttpContext context)
        {
            string msg = "";
%LP%
            string $1 = context.Request.Params["$1"].ToString();
%ELP%
            string Gid = context.Request.Params["gid"].ToString();
            string updateEmp = HttpUtility.UrlDecode(context.Request.Cookies["Login"].Values["LoginName"].ToString(), Encoding.GetEncoding("UTF-8"));
            TnOwnerClass TnOwner = new TnOwnerClass
            {
%LP%
                $1= $1,
%ELP%
            };
            if (bll.Update(TnOwner, null))
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