using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;
using MCSFramework.Model;

namespace MCSCCS.SubModule.UnitiveDocument
{
    public partial class Switch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                JumpPage();
            }
        }

        public void JumpPage()
        {
            int moduleid = 0;
            int.TryParse(Request.QueryString["Module"], out moduleid);
            if (moduleid == 0) Response.Redirect("desktop.aspx");

            Right_Module Module = new Right_Module_BLL(moduleid, true).Model;

            Session["ActiveModule"] = Module.ID;

            switch (Module.ID)
            {
                default:
                    if (!string.IsNullOrEmpty(Module.Remark))
                    {
                        if (Module.Remark.ToLower().StartsWith("http:"))
                            Response.Write("<script laguage='javascript'>window.open('" + Module.Remark + "');history.back(-1);</script>");
                        else if (Module.Remark.StartsWith("~"))
                        {
                            Response.Redirect(Module.Remark);
                        }
                        else
                        {
                            Session["ReportPath"] = Module.Remark;
                            Response.Redirect("../ReportViewer/PubReportViewer.aspx");
                        }
                    }
                    break;
            }
            Response.Redirect("desktop.aspx");

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN：该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
