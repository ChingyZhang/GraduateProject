using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL;
using MCSFramework.UD_Control;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_FNA_FeeWriteoff_FeeWriteOffDetail_Print1 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["InsteadPayClient"] = Request.QueryString["InsteadPayClient"] == null ? 0 : int.Parse(Request.QueryString["InsteadPayClient"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            BindGrid();
        }
    }

    #region 绑定费用核消明细列表
    private void BindGrid()
    {
        decimal subtotal = 0;
        if ((int)ViewState["InsteadPayClient"] > 0)
        {
            lbl_message.Text = new CM_ClientBLL((int)ViewState["InsteadPayClient"]).Model.FullName+"经销商代垫费用抵货款明细表";
        }
        string condition = "FNA_FeeWriteOff.AccountMonth=" + ViewState["AccountMonth"].ToString() + " AND FNA_FeeWriteOff.InsteadPayClient=" + ViewState["InsteadPayClient"].ToString();
        gv_ListDetail.ConditionString = condition;
        gv_ListDetail.BindGrid();
        foreach (GridViewRow row in gv_ListDetail.Rows)
        {
            subtotal += (decimal)gv_ListDetail.DataKeys[row.RowIndex]["FNA_FeeWriteOffDetail_WriteOffCost"] + (decimal)gv_ListDetail.DataKeys[row.RowIndex]["FNA_FeeWriteOffDetail_AdjustCost"];
        }
        lab_SubTotalCostCN.Text = MCSFramework.Common.Rmb.CmycurD(subtotal);
        lab_SubTotalCost.Text = subtotal.ToString("0.##元");
    }
    #endregion

    protected void gv_ListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int applydetailid = (int)gv_ListDetail.DataKeys[e.Row.RowIndex]["FNA_FeeWriteOffDetail_ApplyDetailID"];
            if (applydetailid > 0)
            {
                Label lb_ApplySheetCode = (Label)e.Row.FindControl("lb_ApplySheetCode");
                if (lb_ApplySheetCode != null)
                    lb_ApplySheetCode.Text = FNA_FeeApplyBLL.GetSheetCodeByDetailID(applydetailid);

                Label lb_RelateBrand = (Label)e.Row.FindControl("lb_RelateBrand");
                if (lb_RelateBrand != null)
                {
                    string[] brands = new FNA_FeeApplyBLL().GetDetailModel(applydetailid).RelateBrands.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lb_RelateBrand.Text = "";

                    foreach (string b in brands)
                    {
                        lb_RelateBrand.Text += new PDT_BrandBLL(int.Parse(b)).Model.Name + ",";
                    }
                }
            }
        }
    }

   
}
