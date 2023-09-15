using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PanelTracking.PC.SystemDesign
{
    public partial class %C% : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Language = Request.Cookies["Login"].Values["LoginLanguage"].ToString();
            if (!IsPostBack)
            {
                if (Request.Cookies["Login"] == null)
                {
                    Response.Write("<script>window.parent.location.href=\"../Login.aspx\";</script>");
                }
                string rolenum = "8006";
                if (Session["pageRole"] != null)
                {
                    string role = System.Web.HttpContext.Current.Session["pageRole"].ToString();
                    if (!role.Contains(rolenum))
                    {
                        Response.Write("<script>window.parent.location.href=\"../Login.aspx\";</script>");
                    }
                }
                else
                {
                    Response.Write("<script>window.parent.location.href=\"../Login.aspx\";</script>");
                }
                InitPageData(Language);
            }
        }

        /// <summary>
        /// 初始化页面语言
        /// </summary>
        public void InitPageData(string Language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
            if (Language != "ZH-CN")
            {
                h3ViewHistory.InnerHtml = Resources.Client.%C%_Title;
                //btnQuery.Value = Resources.Client.Query;
                btnAdd.Value = Resources.Client.Create;
                btnDel.Value = Resources.Client.Delete;
            }
        }
    }
}﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PanelTracking.PC.SystemDesign
{
    public partial class TnOwner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Language = Request.Cookies["Login"].Values["LoginLanguage"].ToString();
            if (!IsPostBack)
            {
                if (Request.Cookies["Login"] == null)
                {
                    Response.Write("<script>window.parent.location.href=\"../Login.aspx\";</script>");
                }
                string rolenum = "8006";
                if (Session["pageRole"] != null)
                {
                    string role = System.Web.HttpContext.Current.Session["pageRole"].ToString();
                    if (!role.Contains(rolenum))
                    {
                        Response.Write("<script>window.parent.location.href=\"../Login.aspx\";</script>");
                    }
                }
                else
                {
                    Response.Write("<script>window.parent.location.href=\"../Login.aspx\";</script>");
                }
                InitPageData(Language);
            }
        }

        /// <summary>
        /// 初始化页面语言
        /// </summary>
        public void InitPageData(string Language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
            if (Language != "ZH-CN")
            {
                h3ViewHistory.InnerHtml = Resources.Client.TnOwner_Title;
                //btnQuery.Value = Resources.Client.Query;
                btnAdd.Value = Resources.Client.Create;
                btnDel.Value = Resources.Client.Delete;
            }
        }
    }
}