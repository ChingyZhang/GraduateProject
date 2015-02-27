using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;

public partial class SubModule_UDM_WebPageControlDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
                ViewState["ID"] = new Guid(Request.QueryString["ID"]);

            if (Request.QueryString["WebPageID"] != null)
                ViewState["WebPageID"] = new Guid(Request.QueryString["WebPageID"]);

            if (ViewState["ID"] != null)
            {
                BindData();
            }
            else
            {
                if (ViewState["WebPageID"] == null)
                    Response.Redirect("WebPageList.aspx");
                else
                    BindDropDown();
            }
        }
    }

    private void BindDropDown()
    {
        IList<Right_Action> actions = Right_ActionBLL.GetModelList("Module=" + new UD_WebPageBLL((Guid)ViewState["WebPageID"]).Model.Module.ToString());
        DropDownList ddl_VisibleActionCode = (DropDownList)UC_DetailView1.FindControl("UD_WebPageControl_VisibleActionCode");
        DropDownList ddl_EnableActionCode = (DropDownList)UC_DetailView1.FindControl("UD_WebPageControl_EnableActionCode");


        ddl_VisibleActionCode.DataTextField = "Name";
        ddl_VisibleActionCode.DataValueField = "Code";
        ddl_VisibleActionCode.DataSource = actions;
        ddl_VisibleActionCode.DataBind();
        ddl_VisibleActionCode.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_EnableActionCode.DataTextField = "Name";
        ddl_EnableActionCode.DataValueField = "Code";
        ddl_EnableActionCode.DataSource = actions;
        ddl_EnableActionCode.DataBind();
        ddl_EnableActionCode.Items.Insert(0, new ListItem("请选择", "0"));

    }
    private void BindData()
    {
        UD_WebPageControl m = new UD_WebPageControlBLL((Guid)ViewState["ID"]).Model;

        ViewState["WebPageID"] = m.WebPageID;

        BindDropDown();

        UC_DetailView1.BindData(m);

    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        UD_WebPageControlBLL bll;
        if (ViewState["ID"] == null)
            bll = new UD_WebPageControlBLL();
        else
            bll = new UD_WebPageControlBLL((Guid)ViewState["ID"]);

        UC_DetailView1.GetData(bll.Model);

        if (ViewState["ID"] == null)
        {
            bll.Model.WebPageID = (Guid)ViewState["WebPageID"];
            bll.Add();
        }
        else
        {
            bll.Update();
        }
        DataCache.RemoveCache("UD_WebPage-WebControls-" + bll.Model.WebPageID.ToString());
        Response.Redirect("WebPageControlList.aspx?WebPageID=" + ViewState["WebPageID"].ToString());
    }
}
