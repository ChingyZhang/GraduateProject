using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;

public partial class SubModule_FNA_StaffSalary_FNA_StaffBounsDetail : System.Web.UI.Page
{
    private int approveflag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Quarter"] = Request.QueryString["Quarter"] == null ? 0 : int.Parse(Request.QueryString["Quarter"]);
            BindDropDown();
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        ddl_Quarter.DataSource = AC_AccountQuarterBLL.GetModelList("");
        ddl_Quarter.DataBind();
        if ((int)ViewState["Quarter"] > 0)
            ddl_Quarter.SelectedValue = ViewState["Quarter"].ToString();
    }
    private void BindGrid()
    {
        gv_List.DataSource = FNA_StaffBounsLevelBLL.GetData(int.Parse(ddl_Quarter.SelectedValue));
        gv_List.BindGrid();
        approveflag = FNA_StaffBounsLevelBLL.GetApproveState(int.Parse(ddl_Quarter.SelectedValue));
        bt_Submit.Visible = !(approveflag == 1);
        lb_ApproveFlag.Text = approveflag == 1 ? "已审核" : (approveflag == 4 ? "已提交" : "未审核");
        div_header.InnerText = ddl_Quarter.SelectedItem.Text+"全国办事处绩效考核明细表";
        MatrixTable.GridViewMatric(gv_List);
        MatrixTable.GridViewMergSampeValueRow(gv_List, 0);
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            foreach (GridViewRow r in gv_List.Rows)
            {
                if (r.Cells.Count > 2)
                {
                    #region 金额数据格式化
                    for (int i = 2; i < r.Cells.Count; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            r.Cells[i].Text = d.ToString("0.##");
                        }
                    }
                    #endregion
                }
            }
        }
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        approveflag = FNA_StaffBounsLevelBLL.GetApproveState(int.Parse(ddl_Quarter.SelectedValue));
        if(approveflag==1 || approveflag==3)
        {
            MessageBox.Show(this, "该绩效已审核或已提交，无法再次发起申请");
            return;
        }
        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("Quarter", ViewState["Quarter"].ToString());
        
        int TaskID = EWF_TaskBLL.NewTask("Revocation_Staff",
                                        (int)Session["UserID"],new AC_AccountQuarterBLL((int)ViewState["Quarter"]).Model.Name +" 办事处主管绩效考核"
                                        , "~/SubModule/StaffManage/StaffDetail.aspx?ID="
                                        + ViewState["ID"].ToString(), dataobjects);
        if (TaskID > 0)
            FNA_StaffBounsLevelBLL.ChageApproveState((int)ViewState["Quarter"],4);
        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }
}
