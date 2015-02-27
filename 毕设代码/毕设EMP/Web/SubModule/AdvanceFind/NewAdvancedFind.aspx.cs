using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;


public partial class Controls_NewAdvancedFind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["PanelID"] = Request.QueryString["PanelID"] != null ? new Guid(Request.QueryString["PanelID"]) : Guid.Empty;
            #endregion

            if ((Guid)ViewState["PanelID"] != Guid.Empty)
            {
                UD_PanelBLL _p = new UD_PanelBLL((Guid)ViewState["PanelID"]);
                lb_PanelName.Text = _p.Model.Name;
            }
            else
            {
                MessageBox.ShowAndClose(this, "对不起，PanelID不能为空!");
                return;
            }

        }
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ADFind_FindConditionBLL _f = new ADFind_FindConditionBLL();
        _f.Model.CreateDate = DateTime.Now;
        _f.Model.OpStaff = int.Parse(Session["UserID"].ToString());

        _f.Model.Name = tbx_Name.Text;
        _f.Model.Panel = (Guid)ViewState["PanelID"];
        _f.Model.IsPublic = ddl_IsPublic.SelectedValue;

        Session["AdvancedFindNewID"] = _f.Add();

        MessageBox.ResponseScript(this, "window.close();");
    }
}
