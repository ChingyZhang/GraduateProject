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
            // CODEGEN���õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
