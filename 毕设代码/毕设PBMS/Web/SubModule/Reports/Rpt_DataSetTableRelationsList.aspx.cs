// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetTableRelationsList.aspx.cs 
// 生成日期:2010/9/30 13:22:29 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.RPT;
using MCSFramework.BLL.RPT;

public partial class SubModule_Reports_Rpt_DataSetTableRelationsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion
            BindDropDown();

            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                #region 绑定页面数据集信息
                Rpt_DataSet m = new Rpt_DataSetBLL((Guid)ViewState["ID"]).Model;
                if (m != null)
                {
                    lb_DataSetName.Text = m.Name;

                    #region 根据数据集类型控制Tab可见
                    switch (m.CommandType)
                    {
                        case 1:
                        case 2:
                            MCSTabControl1.Items[2].Visible = false;
                            MCSTabControl1.Items[3].Visible = false;
                            MCSTabControl1.Items[5].Visible = false;
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                #endregion

                BindGrid();
            }
            else
                Response.Redirect("Rpt_DataSetList.aspx");
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_ParentTable.DataSource = UD_TableListBLL.GetModelList
            ("ID IN(SELECT TableID FROM MCS_Reports.dbo.Rpt_DataSetTables WHERE DataSet='" + ViewState["ID"].ToString() + "')");
        ddl_ParentTable.DataBind();

        ddl_ChildTable.DataSource = UD_TableListBLL.GetModelList
            ("ID IN(SELECT TableID FROM MCS_Reports.dbo.Rpt_DataSetTables WHERE DataSet='" + ViewState["ID"].ToString() + "')");
        ddl_ChildTable.DataBind();

        ddl_ParentTable_SelectedIndexChanged(null, null);
        ddl_ChildTable_SelectedIndexChanged(null, null);
    }
    protected void ddl_ParentTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_ParentTable.SelectedValue != "")
        {
            ddl_ParentField.DataSource = UD_ModelFieldsBLL.GetModelList("TableID='" + ddl_ParentTable.SelectedValue + "'");
            ddl_ParentField.DataBind();
        }
    }

    protected void ddl_ChildTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_ChildTable.SelectedValue != "")
        {
            ddl_ChildField.DataSource = UD_ModelFieldsBLL.GetModelList("TableID='" + ddl_ChildTable.SelectedValue + "'");
            ddl_ChildField.DataBind();
        }
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " DataSet ='" + ViewState["ID"].ToString() + "' ORDER BY SortID";

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Rpt_DataSetTableRelationsBLL relation;
        if (ViewState["SelectedID"] == null)
            relation = new Rpt_DataSetTableRelationsBLL();
        else
            relation = new Rpt_DataSetTableRelationsBLL((Guid)ViewState["SelectedID"]);

        relation.Model.ParentTableID = new Guid(ddl_ParentTable.SelectedValue);
        relation.Model.ParentFieldID = new Guid(ddl_ParentField.SelectedValue);
        relation.Model.ChildTableID = new Guid(ddl_ChildTable.SelectedValue);
        relation.Model.ChildFieldID = new Guid(ddl_ChildField.SelectedValue);
        relation.Model.JoinMode = ddl_RelateionMode.SelectedValue;
        relation.Model.RelationCondition = tbx_RelationCondition.Text;
        relation.Model.SortID = int.Parse(tbx_SortID.Text);

        if (ViewState["SelectedID"] == null)
        {
            relation.Model.DataSet = (Guid)ViewState["ID"];
            relation.Add();
        }
        else
        {
            relation.Update();
            ViewState["SelectedID"] = null;
            gv_List.SelectedIndex = -1;
        }
        bt_Add.Text = "新 增";
        BindGrid();

        new Rpt_DataSetBLL((Guid)ViewState["ID"]).ClearCache();

        MessageBox.Show(this, "保存成功！");
    }

    private void BindData()
    {
        Rpt_DataSetTableRelationsBLL relation = new Rpt_DataSetTableRelationsBLL((Guid)ViewState["SelectedID"]);
        ddl_ParentTable.SelectedValue = relation.Model.ParentTableID.ToString();
        ddl_ParentTable_SelectedIndexChanged(null, null);
        ddl_ParentField.SelectedValue = relation.Model.ParentFieldID.ToString();
        ddl_ChildTable.SelectedValue = relation.Model.ChildTableID.ToString();
        ddl_ChildTable_SelectedIndexChanged(null, null);
        ddl_ChildField.SelectedValue = relation.Model.ChildFieldID.ToString();
        tbx_SortID.Text = relation.Model.SortID.ToString();

        ddl_RelateionMode.SelectedValue = relation.Model.JoinMode;
        tbx_RelationCondition.Text = relation.Model.RelationCondition;
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["SelectedID"] = new Guid(gv_List.DataKeys[e.NewSelectedIndex][0].ToString());
        BindData();
        bt_Add.Text = "修 改";
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = new Guid(gv_List.DataKeys[e.RowIndex][0].ToString());
        new Rpt_DataSetTableRelationsBLL(id).Delete();

        if (ViewState["SelectedID"] != null && id == (Guid)ViewState["SelectedID"])
        {
            bt_Add.Text = "新 增";
            ViewState["SelectedID"] = null;
        }

        BindGrid();
        new Rpt_DataSetBLL((Guid)ViewState["ID"]).ClearCache();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0":
                Response.Redirect("Rpt_DataSetDetail.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "1":
                Response.Redirect("Rpt_DataSetParamsList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "2":
                Response.Redirect("Rpt_DataSetTablesList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "3":
                Response.Redirect("Rpt_DataSetTableRelationsList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "4":
                Response.Redirect("Rpt_DataSetFieldsList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "5":
                Response.Redirect("Rpt_DataSetCondition.aspx?ID=" + ViewState["ID"].ToString());
                break;
            default:
                break;
        }
    }


}