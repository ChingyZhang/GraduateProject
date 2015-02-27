// ===================================================================
// 文件路径:Web/Panel_Table.aspx.cs 
// 生成日期:2008-12-9 19:07:17 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;

public partial class Web_Panel_Table : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PanelID"] != null)
                ViewState["PanelID"] = new Guid(Request.QueryString["PanelID"]);
            else
                return;

            ViewState["PageIndex"] = 0;

            BindDropDown();

            BindGrid();
        }
        #region Detail详细信息，不需要定义表关系
        UD_PanelBLL bll = new UD_PanelBLL((Guid)ViewState["PanelID"]);
        lbl_PanelName.Text = bll.Model.Name;
        if (bll.Model.DisplayType == 1)
        {
            //Detail详细信息，不需要定义表关系
            MCSTabControl1.Items[2].Visible = false;
        }
        #endregion
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_TableName.DataSource = new UD_TableListBLL()._GetModelList("");
        ddl_TableName.DataBind();
    }
    #endregion

    private void BindGrid()
    {
        //UD_Panel_TableBLL _tablebll = new UD_Panel_TableBLL();
        //gv_List.DataSource = _tablebll._GetModelList("PanelID=" + ViewState["PanelID"].ToString());
        gv_List.DataSource = UD_Panel_TableBLL.GetTableListByPanelID(new Guid(ViewState["PanelID"].ToString()));
        //UD_StaticDAL.GetTableListByPanelID(int.Parse(.ToString()));
        gv_List.DataBind();

    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                Response.Redirect("PanelDetail.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "1":
                //Response.Redirect("Panel_Table.aspx");
                break;
            case "2":
                Response.Redirect("Panel_TableRelation.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "3":
                Response.Redirect("FieldsInPanel.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        UD_Panel_TableBLL _tb = new UD_Panel_TableBLL();
        _tb.Model.PanelID = (Guid)ViewState["PanelID"];
        _tb.Model.TableID = new Guid(ddl_TableName.SelectedValue);
        _tb.Add();
        BindGrid();
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        new UD_Panel_TableBLL((Guid)gv_List.DataKeys[e.RowIndex]["ID"]).Delete();
        BindGrid();
    }
}