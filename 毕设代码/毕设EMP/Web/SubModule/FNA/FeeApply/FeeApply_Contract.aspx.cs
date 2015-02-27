using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.FNA;
using MCSFramework.Model.Pub;

public partial class SubModule_FNA_FeeApply_FeeApply_Contract : System.Web.UI.Page
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

            ViewState["ContractID"] = Request.QueryString["ContractID"] == null ? 0 : int.Parse(Request.QueryString["ContractID"]);
            #endregion

            BindDropDown();

            if ((int)ViewState["ContractID"] != 0)
            {
                CM_Contract _contract = new CM_ContractBLL((int)ViewState["ContractID"]).Model;
                if (_contract != null)
                {
                    ViewState["ClientID"] = _contract.Client;
                }
            }

            if (ViewState["ClientID"] != null && (int)ViewState["ClientID"] > 0)
            {
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                select_Client.SelectText = c.FullName;
                select_Client.SelectValue = c.ID.ToString();
                select_Client_SelectChange(null, null);
                //tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
            }

            if ((int)ViewState["ContractID"] != 0)
            {
                ListItem item = ddl_Contract.Items.FindByValue(ViewState["ContractID"].ToString());
                if (item != null)
                {
                    ddl_Contract.ClearSelection();
                    item.Selected = true;
                    ddl_Contract_SelectedIndexChanged(null, null);
                }
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
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        bt_Generate.Enabled = false;
        if (select_Client.SelectValue != "")
        {
            ViewState["ClientID"] = int.Parse(select_Client.SelectValue);
            ddl_Contract.Items.Clear();
            IList<CM_Contract> lists = CM_ContractBLL.GetModelList("Client=" + select_Client.SelectValue + " AND State=3");

            if (lists.Count == 0)
            {
                MessageBox.Show(this, "对不起，该客户没有可以申请费用的协议!");
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
                if ((int)ViewState["ContractID"] == 0) ddl_Contract_SelectedIndexChanged(null, null);
                bt_Generate.Enabled = true;
            }
        }
    }

    /// <summary>
    /// 获取最早的付款截止日期
    /// </summary>
    /// <param name="bll"></param>
    /// <returns></returns>
    private DateTime GetMinPayDate(CM_ContractBLL bll)
    {
        DateTime minpaydate = bll.Model.EndDate;
        foreach (CM_ContractDetail item in bll.Items)
        {
            IList<FNA_FeeApplyDetail> feeapplydetails = new FNA_FeeApplyBLL().GetDetail
                ("Client = " + select_Client.SelectValue + " AND RelateContractDetail = " + item.ID.ToString() +
                " AND Flag<>3");
            if (feeapplydetails.Count == 0)
            {
                minpaydate = bll.Model.BeginDate;
                break;
            }
            else
            {
                DateTime _date = feeapplydetails.Max(p => p.EndDate).AddDays(1);
                if (_date < minpaydate) minpaydate = _date;
            }
        }

        return minpaydate;
    }

    protected void ddl_Contract_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Contract.SelectedValue != "" && ddl_Contract.SelectedValue != "0")
        {
            CM_ContractBLL bll = new CM_ContractBLL(int.Parse(ddl_Contract.SelectedValue));
            if (bll.Items.Count == 0)
            {
                MessageBox.Show(this, "对不起，该合同没有需要付款的明细科目!");
                bt_Generate.Enabled = false;
                return;
            }

            #region 获取最早的付款截止日期
            DateTime minpaydate = GetMinPayDate(bll);

            if (minpaydate >= bll.Model.EndDate)
            {
                MessageBox.Show(this, "对不起，该合同没有需要付款的明细科目!");
                bt_Generate.Enabled = false;
                return;
            }
            lb_PayDateRegion.Text = minpaydate.ToString("yyyy年MM月dd日");

            DateTime monthenddate = new AC_AccountMonthBLL(int.Parse(ddl_Month.SelectedValue)).Model.EndDate;
            if (minpaydate > monthenddate)
            {
                MessageBox.Show(this, "对不起，该合同的付款明细科目尚未到请款时间，可以请款时间:" + lb_PayDateRegion.Text);
                bt_Generate.Enabled = false;
                return;
            }
            #endregion

            #region 获取允许预付的截止日期
            DateTime maxpaydate;
            int paymode = bll.Items.Where(p => p.PayMode <= 20).Max(p => p.PayMode);
            if (paymode == 20)
                maxpaydate = bll.Model.EndDate;
            else
            {
                maxpaydate = minpaydate.AddMonths(paymode).AddDays(-1);
                if (maxpaydate > bll.Model.EndDate) maxpaydate = bll.Model.EndDate;
            }
            lb_PayDateRegion.Text += "至" + maxpaydate.ToString("yyyy年MM月dd日");
            #endregion

            ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList
                ("EndDate>='" + minpaydate.ToString("yyyy-MM-dd") +
                "' AND BeginDate<='" + maxpaydate.ToString("yyyy-MM-dd") + "'");
            ddl_BeginMonth.DataBind();
            ddl_BeginMonth.Enabled = false;

            ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList
                ("EndDate>='" + minpaydate.ToString("yyyy-MM-dd") +
                "' AND BeginDate<='" + maxpaydate.ToString("yyyy-MM-dd") +
                "' AND ID<=" + (int.Parse(ddl_Month.SelectedValue) + paymode - 1).ToString());
            ddl_EndMonth.DataBind();
            if (ddl_EndMonth.Items.Count > 0)
                ddl_EndMonth.Items[ddl_EndMonth.Items.Count - 1].Selected = true;

            bt_Generate.Enabled = true;
        }
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        if (ddl_Contract.SelectedValue != "" && ddl_Contract.SelectedValue != "0")
        {
            #region 本次合同费用支付的开始及截止日期
            CM_ContractBLL contractbll = new CM_ContractBLL(int.Parse(ddl_Contract.SelectedValue));

            #region 获取最早的付款截止日期,再次获取，是防止同时打开多个该页面后，分别点申请费用
            DateTime begindate = GetMinPayDate(contractbll);

            if (begindate >= contractbll.Model.EndDate)
            {
                MessageBox.Show(this, "对不起，该合同没有需要付款的明细科目!");
                bt_Generate.Enabled = false;
                return;
            }

            lb_PayDateRegion.Text = begindate.ToString("yyyy年MM月dd日");
            #endregion

            DateTime enddate = contractbll.Model.EndDate;

            DateTime monthenddate = new AC_AccountMonthBLL(int.Parse(ddl_EndMonth.SelectedValue) + 1).Model.EndDate;
            while (enddate > monthenddate)
            {
                enddate = enddate.AddMonths(-1);
            }
            if (enddate <= begindate)
            {
                MessageBox.Show(this, "该合同所有范围的款项均已支付完成!" + enddate.ToString());
                return;
            }
            #endregion

            #region 求出当前客户的直销总经销商
            CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
            if (client == null) return;
            int supplier = 0;

            while (client != null)
            {
                client = new CM_ClientBLL(client.Supplier).Model;
                if (client == null) break;
                if (client.ClientType == 2 && client["DIClassify"] == "1")
                {
                    supplier = client.ID;
                    break;
                }
            }
            #endregion

            FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL();

            bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);
            bll.Model.Client = supplier;       //费用代垫客户
            bll.Model.State = 1;               //草稿
            bll.Model.ApproveFlag = 2;         //未审核
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model["Title"] = ddl_Month.SelectedItem.Text + " 合同费用提前预支付申请单" + " " + select_Client.SelectText + " " + Session["UserRealName"].ToString();

            #region 判断合同类型
            switch (contractbll.Model.Classify)
            {
                case 1:         //陈列合同
                    if (new CM_ClientBLL((int)ViewState["ClientID"]).Model["RTChannel"] == "1")
                        //NKA合同费用
                        bll.Model.FeeType = ConfigHelper.GetConfigInt("ContractFeeType-KA");
                    else
                        //非NKA合同费用
                        bll.Model.FeeType = ConfigHelper.GetConfigInt("ContractFeeType");
                    break;
                case 3:         //导购管理费
                    bll.Model.FeeType = ConfigHelper.GetConfigInt("ContractFeeType-PromotorCost");
                    break;
                case 21:        //租赁合同
                    bll.Model.FeeType = ConfigHelper.GetConfigInt("ContractFeeType-PD");
                    break;
                default:
                    MessageBox.Show(this, "对不起，该合同类型暂不支持费用申请！合同类别:" + contractbll.Model.Classify.ToString());
                    return;
            }
            if (contractbll.Model.Classify != 21)
            {
                if (contractbll.Items.Where(p => p.PayMode > 1).ToList().Count == 0)
                {
                    //非租赁费的合同，不可申请逐月支付的科目费用
                    MessageBox.Show(this, "对不起,付款方式为每月付的科目不可提前申请费用！");
                    return;
                }
            }
            #endregion

            foreach (CM_ContractDetail item in contractbll.Items)
            {
                if (contractbll.Model.Classify != 21)
                {
                    //非租赁费的合同，不可申请逐月支付的科目费用
                    if (item.PayMode == 1) continue;
                }

                DateTime d = begindate;
                while (d < enddate)
                {
                    FNA_FeeApplyDetail detail = new FNA_FeeApplyDetail();
                    detail.Client = (int)ViewState["ClientID"];
                    detail.AccountTitle = item.AccountTitle;
                    detail.BeginDate = d;

                    if (d.AddMonths(1).AddDays(-1) <= contractbll.Model.EndDate)
                    {
                        detail.EndDate = d.AddMonths(1).AddDays(-1);
                        detail.ApplyCost = item.ApplyLimit * item.BearPercent / 100;
                        detail.DICost = item.ApplyLimit * (100 - item.BearPercent) / 100;
                    }
                    else
                    {
                        #region 最后一个月不足一个月时的处理
                        detail.EndDate = contractbll.Model.EndDate;
                        int days = (detail.EndDate - detail.BeginDate).Days + 1;
                        detail.ApplyCost = Math.Round(item.ApplyLimit * days / 30, 1, MidpointRounding.AwayFromZero) * item.BearPercent / 100;
                        detail.DICost = Math.Round(item.ApplyLimit * days / 30, 1, MidpointRounding.AwayFromZero) * (100 - item.BearPercent) / 100;
                        #endregion
                    }
                    detail.BeginMonth = AC_AccountMonthBLL.GetMonthByDate(detail.BeginDate);
                    detail.EndMonth = detail.BeginMonth;
                    detail.LastWriteOffMonth = bll.Model.AccountMonth + 1;

                    #region 不可申请之前月的费用
                    if (detail.BeginMonth < bll.Model.AccountMonth)
                    {
                        d = d.AddMonths(1);
                        continue;
                    }
                    #endregion

                    #region 判断当月费用是否已申请过
                    string condition = "Client = " + detail.Client.ToString() + " AND RelateContractDetail = " + item.ID.ToString() + " AND Flag<>3";
                    if (item.PayMode != 20) condition += "AND BeginMonth=" + detail.BeginMonth.ToString();

                    if (new FNA_FeeApplyBLL().GetDetail(condition).Count > 0)
                    {
                        d = d.AddMonths(1);
                        continue;
                    }
                    #endregion

                    detail.RelateContractDetail = item.ID;
                    detail.Remark = "合同编号:" + contractbll.Model["Code"] + " 日期范围:" + detail.BeginDate.ToString("yyyy-MM-dd") + "~" + detail.EndDate.ToString("yyyy-MM-dd");
                    detail.RelateBrands = item["RelateBrand"];
                    if (string.IsNullOrEmpty(detail.RelateBrands))
                    {
                        foreach (PDT_Brand brand in PDT_BrandBLL.GetModelList("IsOpponent=1"))
                        {
                            detail.RelateBrands += brand.ID.ToString() + ",";
                        }
                    }
                    detail.Flag = 1;  //未报销
                    detail["FeeApplyType"] = (d == contractbll.Model.BeginDate ? "1" : "2");
                    bll.Items.Add(detail);

                    d = d.AddMonths(1);

                    #region 以协议里该科目付款周期为准
                    if (item.PayMode != 20)
                    {
                        DateTime maxpaydate = begindate.AddMonths(item.PayMode).AddDays(-1);
                        if (d > maxpaydate) break;
                    }
                    else
                    {
                        break;
                    }
                    #endregion
                }
            }

            bll.Model.SheetCode = FNA_FeeApplyBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);
            int applyid = bll.Add();

            MessageBox.ShowAndRedirect(this, "费用申请成功！", ResolveUrl("~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + applyid.ToString()));
        }
    }

}
