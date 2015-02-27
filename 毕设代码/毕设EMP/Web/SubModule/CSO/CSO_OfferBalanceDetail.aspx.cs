using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CSO;
using MCSFramework.Model.CSO;
using MCSFramework.Common;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class SubModule_CSO_CSO_OfferBalanceDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["BalanceID"] = Request.QueryString["OfferBalanceID"] == null ? 0 : int.Parse(Request.QueryString["OfferBalanceID"]);
            if ((int)ViewState["BalanceID"] != 0)
                BindData();
            else
                Response.Redirect("CSO_OfferBalanceList.aspx");
        }
        #region 注册弹出窗口脚本
        string script = "function PopAdjust(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AdjustOfferBalanceFee.aspx") +
            "?ID=' + id + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=300px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAdjust", script, true);
        #endregion
    }
    private void BindData()
    {
        CSO_OfferBalance m = new CSO_OfferBalanceBLL(int.Parse(ViewState["BalanceID"].ToString())).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

            if (m.State != 1)
            {
                bt_Submit.Visible = false;
                bt_Delete.Visible = false;
                gv_List.Columns[1].Visible = false;
                bt_BatAdjust.Visible = false;
            }

            if (m.ApproveFlag != 1) bt_CreateFeeApply.Visible = false;

            BindGrid();
        }
    }

    private void BindGrid()
    {
        string condition = " Balance=" + ViewState["BalanceID"].ToString();
        if (select_Client.SelectValue != "")
            condition += " AND CSO_OfferBalance_Detail.Distributor = " + select_Client.SelectValue;

        if (tbx_StaffName.Text != "")
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CSO.dbo.CSO_OfferBalance_Detail',CSO_OfferBalance_Detail.ExtPropertys,'TrackStaffName')='" + tbx_StaffName.Text + "'";
        if (tbx_OfferManName.Text != "")
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CSO.dbo.CSO_OfferBalance_Detail',CSO_OfferBalance_Detail.ExtPropertys,'OfferManName')='" + tbx_OfferManName.Text + "'";
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        int id = (int)ViewState["BalanceID"];
        CSO_OfferBalanceBLL bll = new CSO_OfferBalanceBLL(id);
        if (bll.Model != null && bll.Model.State == 1)
        {
            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["BalanceID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("TotalFee", bll.Items.Sum(p => p.PayFee).ToString("0.##"));

            #region 组合审批任务主题
            string title = "";
            AC_AccountMonth month = new AC_AccountMonthBLL(bll.Model.AccountMonth).Model;
            if (month != null) title += month.Name;
            title += " " + TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", bll.Model.OrganizeCity);
            title += "营养教育新客费用申请";

            #endregion

            int TaskID = EWF_TaskBLL.NewTask("CSO_OfferBalanceApply", (int)Session["UserID"], title,
                "~/SubModule/CSO/CSO_OfferBalanceDetail.aspx?OfferBalanceID=" + ViewState["BalanceID"].ToString(), dataobjects);
            if (TaskID > 0)
            {
                bll.Submit(TaskID, (int)Session["UserID"]);

                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }
            #endregion

            MessageBox.ShowAndRedirect(this, "结算单成功提交申请!", "CSO_OfferBalanceList.aspx");
        }
    }
    protected void bt_CreateFeeApply_Click(object sender, EventArgs e)
    {
        int id = (int)ViewState["BalanceID"];
        CSO_OfferBalanceBLL bll = new CSO_OfferBalanceBLL(id);
        if (bll.Model != null && bll.Model.ApproveFlag != 1)
        {
            int feeapplyid = bll.CreateFeeApply((int)Session["UserID"]);
            MessageBox.ShowAndRedirect(this, "费用申请单申请成功!",
                Page.ResolveUrl("~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + feeapplyid.ToString()));
        }
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        int id = (int)ViewState["BalanceID"];
        CSO_OfferBalanceBLL bll = new CSO_OfferBalanceBLL(id);
        if (bll.Model != null && bll.Model.ApproveFlag != 1)
        {
            bll.Delete();
            MessageBox.ShowAndRedirect(this, "结算单删除成功!", "CSO_OfferBalanceList.aspx");
        }
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        gv_List.Columns[0].Visible = false;
        gv_List.Columns[1].Visible = false;

        BindGrid();

        ToExcel(gv_List, "ExtportFile.xls");

        gv_List.AllowPaging = true;
        gv_List.Columns[0].Visible = true;
        gv_List.Columns[1].Visible = true;
        BindGrid();
    }

    private void ToExcel(Control ctl, string FileName)
    {
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int balanceid = (int)gv_List.DataKeys[e.Row.RowIndex]["CSO_OfferBalance_Detail_Balance"];
            int offerman = (int)gv_List.DataKeys[e.Row.RowIndex]["CSO_OfferBalance_Detail_OfferMan"];
            string offermode = (string)gv_List.DataKeys[e.Row.RowIndex]["CSO_OfferBalance_Detail_OfferMode"];
            int trackstaff = (int)gv_List.DataKeys[e.Row.RowIndex]["CSO_OfferBalance_Detail_TrackStaff"];
            string doctorstandard = (string)gv_List.DataKeys[e.Row.RowIndex]["CSO_OfferBalance_Detail_DoctorStandard"];
            int product = (int)gv_List.DataKeys[e.Row.RowIndex]["CSO_OfferBalance_Detail_Product"];

            HyperLink HyperLink1 = (HyperLink)e.Row.FindControl("HyperLink1");
            if (HyperLink1 != null)
            {
                HyperLink1.NavigateUrl = string.Format("CSO_SampleOfferDetail_GetByCondtion.aspx?OfferBalanceID={0}&OfferMan={1}&OfferModeName={2}&DoctorStandard={3}&Staff={4}&Product={5}&Source=20",
                    balanceid, offerman, Server.UrlEncode(offermode), Server.UrlEncode(doctorstandard), trackstaff, product);
            }
        }
    }
    protected void bt_Adjust_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void bt_BatAdjust_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chk");
            if (chk != null && chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["CSO_OfferBalance_Detail_ID"];
                CSO_OfferBalance_Detail detail = new CSO_OfferBalanceBLL().GetDetailModel(id);
                if (detail == null) continue;


                detail.AwardFee = 0 - detail.ActualFee;
                detail.PayFee = 0;
                detail["AdjustReason"] = "批量扣除(" + (string)Session["UserName"] + ")";
                new CSO_OfferBalanceBLL(detail.Balance).UpdateDetail(detail);
            }
        }

        BindGrid();
    }

}
