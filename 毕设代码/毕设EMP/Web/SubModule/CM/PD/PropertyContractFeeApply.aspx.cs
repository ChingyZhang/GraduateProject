using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Model.Pub;

public partial class SubModule_CM_PD_PropertyContractFeeApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            #endregion

            BindDropDown();

            if (ViewState["ClientID"] != null && (int)ViewState["ClientID"] > 0)
            {
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                select_Client.SelectText = c.FullName;
                select_Client.SelectValue = c.ID.ToString();
                select_Client_SelectChange(null, null);
                tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>=GETDATE() AND BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();
    }
    #endregion

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=6&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        bt_Generate.Enabled = false;
        if (select_Client.SelectValue != "")
        {
            IList<CM_Contract> lists = CM_ContractBLL.GetModelList("Client=" + select_Client.SelectValue + " AND State=3");

            if (lists.Count == 0)
            {
                MessageBox.Show(this, "对不起，该客户没有可以申请费用的租赁协议!");
                return;
            }

            foreach (CM_Contract contract in lists)
            {
                CM_ContractBLL bll = new CM_ContractBLL(contract.ID);
                if (bll.Items.Count == 0) continue;

                if (bll.Items[0]["PayEndDate"] == "" || DateTime.Parse(bll.Items[0]["PayEndDate"]) < contract.EndDate)
                {
                    ddl_Contract.Items.Add(new ListItem("合同编号:" + contract["Code"] + " [" + contract.BeginDate.ToString("yyyy-MM-dd") + "至" +
                        contract.EndDate.ToString("yyyy-MM-dd") + "]", contract.ID.ToString()));
                }
            }

            if (ddl_Contract.Items.Count > 0)
            {
                ddl_Contract_SelectedIndexChanged(null, null);
                bt_Generate.Enabled = true;
            }
        }
    }

    protected void ddl_Contract_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Contract.SelectedValue != "" && ddl_Contract.SelectedValue != "0")
        {
            CM_ContractBLL bll = new CM_ContractBLL(int.Parse(ddl_Contract.SelectedValue));
            if (bll.Items.Count == 0) return;

            if (bll.Items[0]["PayEndDate"] == "")
                tbx_BeginDate.Text = bll.Model.BeginDate.ToString("yyyy-MM-dd");
            else
                tbx_BeginDate.Text = DateTime.Parse(bll.Items[0]["PayEndDate"]).AddDays(1).ToString("yyyy-MM-dd");

            tbx_BeginDate.Enabled = false;

            DateTime enddate = DateTime.Parse(tbx_BeginDate.Text).AddMonths(bll.Items[0].PayMode).AddDays(-1);
            if (enddate > bll.Model.EndDate) enddate = bll.Model.EndDate;
            tbx_EndDate.Text = enddate.ToString("yyyy-MM-dd");
        }
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        if (ddl_Contract.SelectedValue != "" && ddl_Contract.SelectedValue != "0")
        {
            CM_ContractBLL contractbll = new CM_ContractBLL(int.Parse(ddl_Contract.SelectedValue));

            DateTime begindate = DateTime.Parse(tbx_BeginDate.Text);
            DateTime enddate = DateTime.Parse(tbx_EndDate.Text);
            if (enddate <= begindate)
            {
                MessageBox.Show(this, "开始日期必需小于截止日期!");
                return;
            }

            if (enddate > contractbll.Model.EndDate)
            {
                MessageBox.Show(this, "截止日期不能大于租赁合同的截止日期" + contractbll.Model.EndDate.ToString("yyyy-MM-dd") + "!");
                return;
            }

            #region 判断该合同是否已生成过费用申请
            if (FNA_FeeApplyBLL.GetModelList(string.Format("State IN (1,2,3) AND FeeType=3 AND ID IN (SELECT ApplyID FROM FNA_FeeApplyDetail WHERE Client={0} AND RelateContractDetail={1} AND BeginDate='{2:yyyy-MM-dd}')",
        contractbll.Model.Client, contractbll.Items[0].ID, begindate)).Count > 0)
            {
                MessageBox.Show(this, "该租赁合同已申请过费用了，请勿重复申请！");
                return;
            }
            #endregion

            #region 计算开始日期及截止日期间月份及天数
            enddate = enddate.AddDays(1);
            DateTime tmpdate = begindate;
            int months = 0, days = 0;

            while (tmpdate.AddMonths(1) <= enddate)
            {
                tmpdate = tmpdate.AddMonths(1);
                months++;
            }
            if (tmpdate < enddate) days = (enddate - tmpdate).Days;
            enddate = enddate.AddDays(-1);
            #endregion

            #region 关联全品项
            string relatebrands = "";
            foreach (PDT_Brand brand in PDT_BrandBLL.GetModelList("IsOpponent=1"))
            {
                relatebrands += brand.ID + ",";
            }
            #endregion

            FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL();

            bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);

            bll.Model.FeeType = 3;      //管理费-租赁费
            bll.Model.State = 1;        //草稿
            bll.Model.ApproveFlag = 2;  //未审核
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model["Title"] = ddl_Month.SelectedItem.Text + " 租赁费申请单" + " " + select_Client.SelectText + " " + Session["UserRealName"].ToString();

            foreach (CM_ContractDetail item in contractbll.Items)
            {
                FNA_FeeApplyDetail detail = new FNA_FeeApplyDetail();
                detail.Client = int.Parse(select_Client.SelectValue);
                detail.AccountTitle = item.AccountTitle;
                detail.ApplyCost = Math.Round(item.ApplyLimit * months + item.ApplyLimit * days / DateTime.DaysInMonth(enddate.Year, enddate.Month), 1, MidpointRounding.AwayFromZero);
                detail.BeginDate = begindate;
                detail.EndDate = enddate;
                detail.BeginMonth = AC_AccountMonthBLL.GetMonthByDate(begindate);
                detail.EndMonth = AC_AccountMonthBLL.GetMonthByDate(enddate);
                detail.RelateContractDetail = item.ID;
                detail.Remark = "合同编号:" + contractbll.Model["Code"] + " 日期范围:" + begindate.ToString("yyyy-MM-dd") + "~" + enddate.ToString("yyyy-MM-dd");
                detail.RelateBrands = relatebrands;
                detail.Flag = 1;  //未报销
                detail["FeeApplyType"] = (begindate == contractbll.Model.BeginDate ? "1" : "2");
                bll.Items.Add(detail);
            }

            bll.Model.SheetCode = FNA_FeeApplyBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);
            int applyid = bll.Add();

            MessageBox.ShowAndRedirect(this, "费用申请成功！", ResolveUrl("~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + applyid.ToString()));
        }
    }



}
