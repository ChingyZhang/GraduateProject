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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;

public partial class Web_Panel_TableRelation : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PanelID"] != null)
                ViewState["PanelID"] = new Guid(Request.QueryString["PanelID"]);
            else
                Response.Redirect("PanelList.aspx");

            BindDropDown();
            lbl_PanelName.Text = new UD_PanelBLL((Guid)ViewState["PanelID"]).Model.Name;
            ViewState["PageIndex"] = 0;
            ;
            BindGrid();

        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        //ddl_ParentTable.DataSource = new UD_TableListBLL()._GetModelList("ID in (select TableID from UD_Panel_Table where PanelID=" + ViewState["PanelID"].ToString() + ")");
        ddl_ParentTable.DataSource = UD_Panel_TableBLL.GetTableListByPanelID(new Guid(ViewState["PanelID"].ToString()));
        //UD_StaticDAL.GetTableListByPanelID(int.Parse(.ToString()));
        ddl_ParentTable.DataBind();

        ddl_ChildTable.DataSource = UD_Panel_TableBLL.GetTableListByPanelID(new Guid(ViewState["PanelID"].ToString()));
        ddl_ChildTable.DataBind();

        ddl_ParentTable_SelectedIndexChanged(null, null);
        ddl_ChildTable_SelectedIndexChanged(null, null);

    }
    #endregion

    private void BindGrid()
    {
        gv_Relation.DataBind();

        gv_Relation.BindGrid<UD_Panel_TableRelation>(UD_Panel_TableRelationBLL.GetModelList("PanelID='" + ViewState["PanelID"].ToString() + "'"));
    }

    private void BindData()
    {
        UD_Panel_TableRelationBLL relation = new UD_Panel_TableRelationBLL((Guid)ViewState["SelectedID"]);
        lbl_ID.Text = relation.Model.ID.ToString();
        ddl_ParentTable.SelectedValue = relation.Model.ParentTableID.ToString();
        ddl_ParentTable_SelectedIndexChanged(null, null);
        ddl_ParentField.SelectedValue = relation.Model.ParentFieldID.ToString();
        ddl_ChildTable.SelectedValue = relation.Model.ChildTableID.ToString();
        ddl_ChildTable_SelectedIndexChanged(null, null);
        ddl_ChildField.SelectedValue = relation.Model.ChildFieldID.ToString();
        tbx_SortID.Text = relation.Model.SortID.ToString();

        ddl_RelateionMode.SelectedValue = relation.Model.JoinMode;
        tbx_RelationCondition.Text = relation.Model.RelationCondition;


        bt_OK.Text = "修 改";
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        UD_Panel_TableRelationBLL relation;
        if (ViewState["SelectedID"] == null)
            relation = new UD_Panel_TableRelationBLL();
        else
            relation = new UD_Panel_TableRelationBLL((Guid)ViewState["SelectedID"]);

        relation.Model.ParentTableID = new Guid(ddl_ParentTable.SelectedValue);
        relation.Model.ParentFieldID = new Guid(ddl_ParentField.SelectedValue);
        relation.Model.ChildTableID = new Guid(ddl_ChildTable.SelectedValue);
        relation.Model.ChildFieldID = new Guid(ddl_ChildField.SelectedValue);
        relation.Model.JoinMode = ddl_RelateionMode.SelectedValue;
        relation.Model.RelationCondition = tbx_RelationCondition.Text;
        relation.Model.SortID = int.Parse(tbx_SortID.Text);

        if (ViewState["SelectedID"] == null)
        {
            relation.Model.PanelID = (Guid)ViewState["PanelID"];
            relation.Add();
        }
        else
        {
            relation.Update();
            ViewState["SelectedID"] = null;
        }
        bt_OK.Text = "新 增";
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                Response.Redirect("PanelDetail.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "1":
                Response.Redirect("Panel_Table.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "2":
                //Response.Redirect("Panel_TableRelation.aspx");
                break;
            case "3":
                Response.Redirect("FieldsInPanel.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
        }
    }

    protected void ddl_ParentTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_ParentField.DataSource = new UD_ModelFieldsBLL()._GetModelList("TableID='" + ddl_ParentTable.SelectedValue + "'");
        ddl_ParentField.DataBind();
    }

    protected void ddl_ChildTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_ChildField.DataSource = new UD_ModelFieldsBLL()._GetModelList("TableID='" + ddl_ChildTable.SelectedValue + "'");
        ddl_ChildField.DataBind();
    }

    //protected void gv_Relation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    ViewState["ID"] = gv_Relation.DataKeys[e.NewSelectedIndex]["ID"].ToString();
    //    BindData();
    //}

    protected void gv_Relation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)gv_Relation.DataKeys[e.RowIndex]["ID"];
        new UD_Panel_TableRelationBLL(id).Delete();
        BindGrid();
    }
    protected void gv_Relation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid id = (Guid)gv_Relation.DataKeys[e.NewSelectedIndex]["ID"];

        ViewState["SelectedID"] = id;
        BindData();
    }

    protected void bt_Increase_Click(object sender, EventArgs e)
    {
        Guid id = (Guid)gv_Relation.DataKeys[((GridViewRow)((Button)sender).Parent.Parent).RowIndex][0];
        UD_Panel_TableRelationBLL bll = new UD_Panel_TableRelationBLL(id);
        bll.Model.SortID++;
        bll.Update();

        BindGrid();
    }
    protected void bt_Decrease_Click(object sender, EventArgs e)
    {
        Guid id = (Guid)gv_Relation.DataKeys[((GridViewRow)((Button)sender).Parent.Parent).RowIndex][0];
        UD_Panel_TableRelationBLL bll = new UD_Panel_TableRelationBLL(id);
        if (bll.Model.SortID > 0) bll.Model.SortID--;
        bll.Update();

        BindGrid();
    }
}