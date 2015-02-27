using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.Pub;
using System.Data;
using MCSFramework.BLL.SVM;

public partial class SubModule_CM_DI_SubClientList2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面

        if (!Page.IsPostBack)
        {
            #region 获取当前员工的关联经销商
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            int _relateclient = 0;
            if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
            {
                ViewState["ClientID"] = _relateclient;
                select_Client.Enabled = false;
            }
            else
            {
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                    Session["ClientID"] = ViewState["ClientID"];
                }
                else if (Session["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
                }
            }
            #endregion

            BindDropDown();
            //Session["MCSMenuControl_FirstSelectIndex"] = "12";
            MCSTabControl1.SelectedIndex = 1;
            if (ViewState["ClientID"] != null)
            {
                CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.FullName;
                if (client.ClientType != 2)
                {
                    MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                }
                BindGrid();
            }
            //else
            //{
            //    MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            //}

        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindGrid()
    {
        ViewState["ClientID"] = int.Parse(select_Client.SelectValue);
        //string ConditionStr = " ((CM_Client.Supplier = " + ViewState["ClientID"].ToString() + " OR mcs_sys.dbo.uf_spilt2('MCS_CM.dbo.CM_Client',CM_Client.ExtPropertys,'Supplier2')=" + ViewState["ClientID"].ToString() + ") AND ClientType=2 AND CM_Client.ActiveFlag=1 AND CM_Client.ApproveFlag=1)";
        string ConditionStr = " (CM_Client.Supplier = " + ViewState["ClientID"].ToString() + " AND ClientType=2 AND CM_Client.ActiveFlag=1 AND CM_Client.ApproveFlag=1)";

        ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CM.dbo.CM_Client',CM_Client.ExtPropertys,'DIClassify')='2' ";

        if (tbx_Condition.Text.Trim() != "")
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";


        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];

        CM_ClientBLL bll = new CM_ClientBLL(id);
        Response.Redirect("~/SubModule/SVM/SalesVolumeBatchInput.aspx?Type=1&ClientID=" + id.ToString());

    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region 显示销量库存录入情况
            int clientid = (int)gv_List.DataKeys[e.Row.RowIndex]["CM_Client_ID"];
            int month = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(0 - ConfigHelper.GetConfigInt("JXCDelayDays")));

            DataTable dt = SVM_SalesVolumeBLL.GetCountByClient(month, clientid);
            if (dt != null && dt.Rows.Count == 3)
            {
                foreach (DataRow row in dt.Rows)
                {
                    HyperLink link = null;
                    switch (row["Title"].ToString())
                    {
                        case "SaleIn":
                            link = (HyperLink)(e.Row.Cells[0].Controls[0]);
                            break;
                        case "SaleOut":
                            link = (HyperLink)(e.Row.Cells[1].Controls[0]);
                            break;
                        case "Inventory":
                            link = (HyperLink)(e.Row.Cells[2].Controls[0]);
                            break;
                    }
                    if (link != null)
                    {
                        link.Text += "<br/>";
                        if (row["ApprovedCount"] != DBNull.Value && (int)row["ApprovedCount"] > 0)
                        {
                            link.ToolTip += " 已审核:" + row["ApprovedCount"].ToString();
                            link.Text += "-<font color=red>" + row["ApprovedCount"].ToString() + "</font>";
                        }
                        if (row["SubmitedCount"] != DBNull.Value && (int)row["SubmitedCount"] > 0)
                        {
                            link.ToolTip += " 已提交:" + row["SubmitedCount"].ToString();
                            link.Text += "-<font color=blue>" + row["SubmitedCount"].ToString() + "</font>";
                        }
                        if (row["InputedCount"] != DBNull.Value && (int)row["InputedCount"] > 0)
                        {
                            link.ToolTip += " 未提交:" + row["InputedCount"].ToString();
                            link.Text += "-<font color=black>" + row["InputedCount"].ToString() + "</font>";
                        }
                    }
                }
            }
            #endregion
        }
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {


        if (MCSTabControl1.SelectedIndex == 0)
        {
            Response.Redirect("~/SubModule/CM/DI/SubClientList.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
        else
        {
            BindGrid();
        }
    }

}
