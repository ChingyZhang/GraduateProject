using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_FNA_FeeWriteoff_Pop_FeeWriteOffListByFeeApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["FeeApplyID"] = Request.QueryString["FeeApplyID"] == null ? 0 : int.Parse(Request.QueryString["FeeApplyID"]);
            ViewState["FeeApplyDetailID"] = Request.QueryString["FeeApplyDetailID"] == null ? 0 : int.Parse(Request.QueryString["FeeApplyDetailID"]);

            if ((int)ViewState["FeeApplyID"] > 0 || (int)ViewState["FeeApplyDetailID"] > 0)
            {
                BindGrid();
            }
        }
    }

    private void BindGrid()
    {
        string condition = "";

        if ((int)ViewState["FeeApplyID"] > 0)
            condition = "FNA_FeeWriteOffDetail.ApplyDetailID IN (SELECT ID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE ApplyID=" + ViewState["FeeApplyID"].ToString() + ")";
        else
            condition = "FNA_FeeWriteOffDetail.ApplyDetailID =" + ViewState["FeeApplyDetailID"].ToString();

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        decimal sum = 0;
        for (int i = 0; i < gv_List.Rows.Count; i++)
        {
            Label lb_ApproveCost = (Label)gv_List.Rows[i].FindControl("lb_ApproveCost");
            decimal c = 0;
            if (lb_ApproveCost != null && decimal.TryParse(lb_ApproveCost.Text, out c))
            {
                sum += c;
            }
        }
        lb_TotalCost.Text = sum.ToString("0.###");
    }
}
