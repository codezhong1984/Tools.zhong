﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PanelTracking.PC.SystemDesign
{
    public partial class Add%C% : System.Web.UI.Page
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
                h3ViewHistory.InnerHtml = Resources.Client.%C%Configure;
                requestinfo.InnerHtml = " <i class=\"myicon-apply1\"></i>Add";
%LP%				
	span$1.InnerHtml = Resources.Client.%C%_$1;
%ELP%				
                btnAdd.InnerHtml = "<i class=\"myicon-login1\"></i>" + Resources.Client.Save;
            }
        }
    }
}