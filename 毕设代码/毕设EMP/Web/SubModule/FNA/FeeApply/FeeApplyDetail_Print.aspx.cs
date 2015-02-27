using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.EWF;

public partial class SubModule_FNA_FeeApply_FeeApplyDetail_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string title = ConfigHelper.GetConfigString("PageTitle");
            if (!String.IsNullOrEmpty(title))
            {
                lb_Header.Text = title;
            }

            #region 获取参数
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 1 : int.Parse(Request.QueryString["FeeType"]);
            Session["FeeApplyDetail"] = null;
            Session["SuccessFlag"] = null;
            #endregion

            BindDropDown();

            #region 创建明细的列表
            ListTable<FNA_FeeApplyDetail> _details = new ListTable<FNA_FeeApplyDetail>(new FNA_FeeApplyBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;
            #endregion


            BindData();

        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindData()
    {
        int id = (int)ViewState["ID"];

        FNA_FeeApply apply = new FNA_FeeApplyBLL(id).Model;
        ViewState["AccountMonth"] = apply.AccountMonth;

        if (apply == null) Response.Redirect("FeeApplyList.aspx");

        pn_FeeApply.BindData(apply);

        Label lb_OrganizeCity = (Label)pn_FeeApply.FindControl("FNA_FeeApply_OrganizeCity");
        lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_Sys.dbo.Addr_OrganizeCity", apply.OrganizeCity);

        #region 将备注信息单独显示出来
        Label lb_remark = (Label)pn_FeeApply.FindControl("FNA_FeeApply_Remark");
        if (lb_remark != null)
        {
            lb_remark.Text = apply["Remark"].Replace("\r\n", "<br/>");
        }
        #endregion

        BindGrid();

        #region 获取流程审批意见
        int TaskID = 0;

        if (int.TryParse(apply["TaskID"], out TaskID) && TaskID > 0)
        {
            EWF_TaskBLL task = new EWF_TaskBLL(TaskID);
            if (task.Model != null)
            {
                lb_TaskApproveInfo.Text = task.Model.Remark;
            }
        }
        #endregion
    }

    #region 绑定费用申请明细列表
    private void BindGrid()
    {
        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        gv_List.BindGrid<FNA_FeeApplyDetail>(_details.GetListItem());

        //求销售额合计
        decimal _totalcost = 0;
        foreach (FNA_FeeApplyDetail _detail in _details.GetListItem())
        {
            _totalcost += _detail.ApplyCost + _detail.AdjustCost;
        }
        lb_TotalCost.Text = _totalcost.ToString("0.###");
    }
    #endregion
}
