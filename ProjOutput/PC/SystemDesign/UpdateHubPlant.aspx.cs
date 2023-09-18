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
    public partial class UpdateHubPlant : System.Web.UI.Page
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
                string id = Request.QueryString["id"].ToString();
                this.hidId.Value = id;
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
                h3ViewHistory.InnerHtml = Resources.Client.HubPlantConfigure;
                requestinfo.InnerHtml = " <i class=\"myicon-apply1\"></i>Modify";
				                spanGID.InnerHtml = Resources.Client.HubPlant_GID;
				                spanCOMPANY.InnerHtml = Resources.Client.HubPlant_COMPANY;
				                spanPLANT.InnerHtml = Resources.Client.HubPlant_PLANT;
				                spanPLANT_NAME.InnerHtml = Resources.Client.HubPlant_PLANT_NAME;
				                spanSAP_CLIENT.InnerHtml = Resources.Client.HubPlant_SAP_CLIENT;
				                spanNOTE.InnerHtml = Resources.Client.HubPlant_NOTE;
				                spanCREATE_DATE.InnerHtml = Resources.Client.HubPlant_CREATE_DATE;
				                spanUPDATE_DATE.InnerHtml = Resources.Client.HubPlant_UPDATE_DATE;
                btnUpdate.InnerHtml = "<i class=\"myicon-login1\"></i>" + Resources.Client.Save;
            }
        }
    }
}