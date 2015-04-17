// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetTablesList.aspx.cs 
// 生成日期:2010/9/30 13:23:05 
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

public partial class SubModule_Reports_Rpt_DataSetTablesList : System.Web.UI.Page
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
        ddl_TableName.DataSource = UD_TableListBLL.GetModelList("");
        ddl_TableName.DataBind();
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " DataSet ='" + ViewState["ID"].ToString() + "' ";

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
        Rpt_DataSetTablesBLL bll = new Rpt_DataSetTablesBLL();
        bll.Model.DataSet = (Guid)ViewState["ID"];
        bll.Model.TableID = new Guid(ddl_TableName.SelectedValue);
        bll.Add();
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
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = new Guid(gv_List.DataKeys[e.RowIndex][0].ToString());
        new Rpt_DataSetTablesBLL(id).Delete();

        BindGrid();
        new Rpt_DataSetBLL((Guid)ViewState["ID"]).ClearCache();
    }
}