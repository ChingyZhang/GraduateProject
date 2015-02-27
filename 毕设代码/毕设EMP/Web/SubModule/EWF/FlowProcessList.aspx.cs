// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.Common;
using MCSFramework.BLL;

public partial class SubModule_EWF_FlowProcessList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["AppID"] = Request.QueryString["AppID"] != null ? new Guid(Request.QueryString["AppID"]) : Guid.Empty;
            #endregion
            BindDropDown();
            if ((Guid)ViewState["AppID"] != Guid.Empty)
            {
                ViewState["PageIndex"] = 0;
                BindGrid();
            }
            #region 权限判断
            //Right right = new Right();
            //string strUserName = Session["UserName"].ToString();
            //if (!right.GetAccessPermission(strUserName, 110, 0)) Response.Redirect("../noaccessright.aspx");        //有无查看的权限
            //if (!right.GetAccessPermission(strUserName, 0, 1001)) bt_Add.Visible = false;                              //有无新增的权限
            #endregion
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_ProcessType");
        ddl_Type.DataBind();
        ddl_Type.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    #endregion

    private void BindGrid()
    {
        EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
        lb_AppName.Text = app.Model.Name;

        lb_AppName.NavigateUrl = "FlowAppDetail.aspx?AppID='" + app.Model.ID.ToString() + "'";

        gv_List.BindGrid<EWF_Flow_Process>(app.GetProcessList());

    }

    #region 分页、排序、选中等事件

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid ID = (Guid)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        EWF_Flow_ProcessBLL bll = new EWF_Flow_ProcessBLL(ID);

        switch (bll.Model.Type)
        {
            case 1:
                Response.Redirect("FlowProcessStartDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 2:
                Response.Redirect("FlowProcessFinishDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 3:
                Response.Redirect("FlowProcessDecisionDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 4:
                Response.Redirect("FlowProcessConditionDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 5:
                Response.Redirect("FlowProcessDataBaseDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 6:
                Response.Redirect("FlowProcessEmailDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 7:
                Response.Redirect("FlowProcessGetPositionDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 8:
                Response.Redirect("FlowProcessGetOrganizeCityDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 9:
                Response.Redirect("FlowProcessCCDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
            case 10:
                Response.Redirect("FlowProcessJointDecisionDetail.aspx?ProcessID=" + bll.Model.ID.ToString() + "&AppID=" + ViewState["AppID"].ToString());
                break;
        }

    }

    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if (ddl_Type.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择需要添加环节的类型！");
            return;
        }
        switch (ddl_Type.SelectedValue)
        {
            case "1":
                Response.Redirect("FlowProcessStartDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "2":
                Response.Redirect("FlowProcessFinishDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "3":
                Response.Redirect("FlowProcessDecisionDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "4":
                Response.Redirect("FlowProcessConditionDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "5":
                Response.Redirect("FlowProcessDataBaseDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "6":
                Response.Redirect("FlowProcessEmailDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "7":
                Response.Redirect("FlowProcessGetPositionDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "8":
                Response.Redirect("FlowProcessGetOrganizeCityDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "9":
                Response.Redirect("FlowProcessCCDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
            case "10":
                Response.Redirect("FlowProcessJointDecisionDetail.aspx?AppID=" + ViewState["AppID"].ToString() + "&Type=" + ddl_Type.SelectedValue);
                break;
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
            Response.Redirect("FlowAppDetail.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 2)
            Response.Redirect("FlowDataObjectList.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 3)
            Response.Redirect("FlowInitPosition.aspx?AppID=" + ViewState["AppID"].ToString());
    }
}