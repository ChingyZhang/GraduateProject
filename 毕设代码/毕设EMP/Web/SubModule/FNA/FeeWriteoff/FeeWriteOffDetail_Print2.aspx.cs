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
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;
using System.Data;

public partial class SubModule_FNA_FeeWriteoff_FeeWriteOffDetail_Print2 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            BindGrid();
        }
    }

    #region 绑定费用核消明细列表
    private void BindGrid()
    {
        lbl_message.Text = "费用报销审批汇总表";
        int id = (int)ViewState["ID"];

        FNA_FeeWriteOffBLL bll = new FNA_FeeWriteOffBLL(id);
        ViewState["Details"] = bll.Items.OrderBy(p => p.Client).ThenBy(p => p.AccountTitle).ThenBy(p => p.BeginMonth).ToList();

        FNA_FeeWriteOff writeoff = bll.Model;
        if (writeoff == null) Response.Redirect("FeeWriteOffList.aspx");

        BindGridPrint();
        string type = "";
        if (writeoff["InvoiceClassAB"] != "")
            type = DictionaryBLL.GetDicCollections("FNA_InvoiceClassAB")[writeoff["InvoiceClassAB"]].Name;
        // type = DictionaryBLL.Dictionary_Data_GetAlllList("TableName='FNA_InvoiceClassAB' AND Code='" + writeoff["InvoiceClassAB"] + "'")[0].Name;
        if (type == "")
            type = "抵款";
        span_sheetcode.InnerText = writeoff.SheetCode;
        span_accountmonth.InnerText = new AC_AccountMonthBLL(writeoff.AccountMonth).Model.Name;
        if (bll.Model["InsteadPayStaff"] != "" && bll.Model["InsteadPayStaff"] != "0")
        {
            p_ddtype.InnerText = "员工";
            span_client.InnerText = new Org_StaffBLL(int.Parse(bll.Model["InsteadPayStaff"])).Model.RealName;
        }
        else
            span_client.InnerText = new CM_ClientBLL(writeoff.InsteadPayClient).Model.FullName;
        int city5 = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", writeoff.OrganizeCity, 5);
        span_orgnizecity.InnerText = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name", "SuperID", 1, city5 > 0 ? city5 : writeoff.OrganizeCity);
        span_type.InnerText = type;
        span1.InnerText = span2.InnerText = "1";

        //求费用核消金额合计
        decimal _totalcost = 0;
        foreach (FNA_FeeWriteOffDetail _detail in bll.Items)
        {
            _totalcost += _detail.WriteOffCost + _detail.AdjustCost;
        }

        lab_SubTotalCostCN.Text = MCSFramework.Common.Rmb.CmycurD(_totalcost.ToString());
        lab_SubTotalCost.Text = _totalcost.ToString("#,##0.00");
        lab_SubTotalCost.Text = lab_SubTotalCost.Text;
        lab_SubTotalCost.Text = lab_SubTotalCost.Text;
    }
    #endregion
    #region 绑定费用核消明细打印列表
    private void BindGridPrint()
    {

        IList<FNA_FeeWriteOffDetail> list = (IList<FNA_FeeWriteOffDetail>)ViewState["Details"];
        if (list.Count(p => p.AccountTitle == 82) > 0)
        {
            ViewState["FLPurchase"] = FNA_FeeWriteOffBLL.GetPurchaseVolume((int)ViewState["ID"]);
            gv_ListDetail.Columns[9].Visible = true;
        }
        gv_ListDetail.BindGrid<FNA_FeeWriteOffDetail>(list.OrderBy(p => p.Client).ThenBy(p => p.AccountTitle).ThenBy(p => p.BeginMonth).ToList());

    }
    #endregion
    protected void gv_ListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int applydetailid = (int)gv_ListDetail.DataKeys[e.Row.RowIndex]["ApplyDetailID"];
            if (applydetailid > 0)
            {
                Label lb_ApplySheetCode = (Label)e.Row.FindControl("lb_ApplySheetCode");
                if (lb_ApplySheetCode != null)
                    lb_ApplySheetCode.Text = FNA_FeeApplyBLL.GetSheetCodeByDetailID(applydetailid);
                FNA_FeeApplyDetail _detail = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                Label lb_AllCost = (Label)e.Row.FindControl("lb_AllCost");
                if (lb_AllCost != null)
                    lb_AllCost.Text = Math.Round(decimal.Parse(_detail["DICost"] == "" ? "0" : _detail["DICost"]) + _detail.ApplyCost, 1).ToString("0.##");

                Label lb_Remark = (Label)e.Row.FindControl("lb_Remark");
                if (lb_Remark != null)
                {
                    if (ViewState["FLPurchase"] != null)
                        lb_Remark.Text = "进货额:" + ((DataTable)ViewState["FLPurchase"]).Compute("Sum(PurchaseVolume)", "ID=" + gv_ListDetail.DataKeys[e.Row.RowIndex]["ID"]).ToString();
                }
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
