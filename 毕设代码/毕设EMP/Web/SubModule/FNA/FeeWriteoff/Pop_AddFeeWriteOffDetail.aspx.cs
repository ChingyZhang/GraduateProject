using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.CM;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Promotor;
using System.Collections;
public partial class SubModule_FNA_FeeWriteoff_Pop_AddFeeWriteOffDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["Client"] = Request.QueryString["Client"] == null ? 0 : int.Parse(Request.QueryString["Client"]);
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]);

            if ((int)ViewState["OrganizeCity"] == 0 || Session["FeeWriteOffDetails"] == null)
            {
                Session["SuccessFlag"] = false;
                MessageBox.ShowAndClose(this, "参数错误！" + ViewState["OrganizeCity"].ToString());
            }

            select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?&OrganizeCity=" + ViewState["OrganizeCity"].ToString();

            BindDropDown();
            if ((int)ViewState["FeeType"] != 0) { ddl_FeeType.SelectedValue = ViewState["FeeType"].ToString(); ddl_FeeType.Enabled = false; }
            if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
            {
                //无查看营养教育费用权限
                ListItem item = ddl_FeeType.Items.FindByValue(ConfigHelper.GetConfigInt("CSOCostType").ToString());
                if (item != null) item.Enabled = false;
            }
            BindFeeApplyNoWriteOff();
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year>="+(DateTime.Now.Year-1).ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 3).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate>DateAdd(year,-1,GETDATE())");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType");
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_FeeType_SelectedIndexChanged(null, null);
    }

    protected Dictionary<string, Dictionary_Data> GetBalanceMode()
    {
        Dictionary<string, Dictionary_Data> dicts = MCSFramework.BLL.DictionaryBLL.GetDicCollections("FNA_FeeWriteOffBalanceMode");
        return dicts;
    }

    #region 绑定尚未报销的通过审批的费用申请列表（含科目）
    private void BindFeeApplyNoWriteOff()
    {
        #region 组织查询条件
        string condition = " FNA_FeeApplyDetail.AvailCost > 0 ";
        condition += " AND FNA_FeeApply.OrganizeCity =" + ViewState["OrganizeCity"].ToString();

        condition += " AND FNA_FeeApplyDetail.BeginMonth >= " + ddl_BeginMonth.SelectedValue;
        condition += " AND FNA_FeeApplyDetail.EndMonth <= " + ddl_EndMonth.SelectedValue;

        if (ddl_FeeType.SelectedValue != "0")
            condition += "AND FNA_FeeApply.FeeType=" + ddl_FeeType.SelectedValue;
        if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
        {
            //无查看营养教育费用权限
            condition += " AND FNA_FeeApply.FeeType <> " + ConfigHelper.GetConfigInt("CSOCostType").ToString();
        }

        if (select_Client.SelectValue != "" && select_Client.SelectValue != "0")
            condition += " AND FNA_FeeApplyDetail.Client=" + select_Client.SelectValue;

        if (tbx_SheetCode.Text != "")
            condition += " AND FNA_FeeApply.SheetCode like '%" + tbx_SheetCode.Text + "%'";

        if (ddl_AccountTitle.SelectedValue != "0")
        {
            condition += " AND FNA_FeeApplyDetail.AccountTitle=" + ddl_AccountTitle.SelectedValue;
        }
        if ((int)ViewState["Client"] == 0)
            condition += " AND FNA_FeeApply.Client IS NULL";
        else
        {
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["Client"]);
            string clientconditon = "";
            if (_bll.Model["DIClassify"] == "1")
            {
                clientconditon = "SELECT ID FROM MCS_CM.dbo.CM_Client WHERE ID=" + ViewState["Client"].ToString() + "OR Supplier=" + ViewState["Client"].ToString() + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)='3'";
            }
            else
            {
                clientconditon = ViewState["Client"].ToString() + "," + _bll.Model.Supplier.ToString();
            }
            condition += " AND FNA_FeeApply.Client IN (" + clientconditon + ")";

        }


        #region 排除已选中到报销列表中的申请单
        string applydetailids = "";
        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;
        foreach (FNA_FeeWriteOffDetail item in _details.GetListItem())
        {
            applydetailids += item.ApplyDetailID.ToString() + ",";
        }
        if (applydetailids != "")
        {
            applydetailids = applydetailids.Substring(0, applydetailids.Length - 1);
            condition += " AND FNA_FeeApplyDetail.ID not in (" + applydetailids + ")";
        }
        #endregion

        #endregion

        gv_FeeAplyList.ConditionString = condition;
        gv_FeeAplyList.BindGrid();
    }
    #endregion

    #region 绑定费用核消明细列表
    private void BindGrid()
    {
        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;
        gv_List.BindGrid<FNA_FeeWriteOffDetail>(_details.GetListItem());

        //求费用核消金额合计
        decimal _totalapplycost = 0, _totalcost = 0;
        foreach (FNA_FeeWriteOffDetail _detail in _details.GetListItem())
        {
            _totalapplycost += _detail.ApplyCost;
            _totalcost += _detail.WriteOffCost + _detail.AdjustCost;
        }
        lb_TotalCost.Text = _totalcost.ToString("0.###");
    }
    #endregion

    #region 将列表中已填写的信息保存到ListTable中
    protected void tbx_WriteOffCost_TextChanged(object sender, EventArgs e)
    {
        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;

        TextBox tb_WriteOffCost = (TextBox)sender;
        int id = (int)gv_List.DataKeys[((GridViewRow)tb_WriteOffCost.Parent.Parent).RowIndex]["ID"];

        FNA_FeeWriteOffDetail m = _details[id.ToString()];

        decimal writeoffcost = 0;
        if (decimal.TryParse(tb_WriteOffCost.Text, out writeoffcost))
        {
            int overpercent = 0;
            int.TryParse(new AC_AccountTitleBLL(m.AccountTitle).Model["OverPercent"], out overpercent);

            if (writeoffcost > (m.ApplyCost * (100 + overpercent) / 100))
            {
                tb_WriteOffCost.Text = m.ApplyCost.ToString("0.###");
                MessageBox.Show(this, "超可报销金额，本科目最多可超申请金额的 " + overpercent + "% 报销，最大报销金额为 " + (m.ApplyCost * (100 + overpercent) / 100).ToString() + "元");
                return;
            }

            lb_TotalCost.Text = (decimal.Parse(lb_TotalCost.Text) - m.WriteOffCost).ToString("0.###");

            m.WriteOffCost = writeoffcost;
            _details.Update(m);

            lb_TotalCost.Text = (decimal.Parse(lb_TotalCost.Text) + m.WriteOffCost).ToString("0.###");

            //DropDownList ddl_BalanceMode = (DropDownList)(((GridViewRow)tb_WriteOffCost.Parent.Parent).FindControl("ddl_BalanceMode"));
            //ddl_BalanceMode.Enabled = writeoffcost < m.ApplyCost;
        }
        else
        {
            tb_WriteOffCost.Text = m.ApplyCost.ToString("0.###");
            MessageBox.Show(this, "填写格式必需为数值型！");
        }
    }
    #endregion

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            int applydetailid = (int)gv_List.DataKeys[e.Row.RowIndex]["ApplyDetailID"];

            ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;
            FNA_FeeWriteOffDetail m = _details[id.ToString()];

            string sheetcode = "";

            FNA_FeeApplyDetail applydetail = new FNA_FeeApplyDetail();
            if (applydetailid > 0)
            {
                applydetail = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                sheetcode = FNA_FeeApplyBLL.GetSheetCodeByDetailID(applydetailid);

            }

            HyperLink hy_ApplySheetCode = (HyperLink)e.Row.FindControl("hy_ApplySheetCode");
            hy_ApplySheetCode.Text = sheetcode;
            hy_ApplySheetCode.NavigateUrl = "~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + applydetail.ApplyID;

            if (applydetail.Client > 0)
            {
                HyperLink hy_Client = (HyperLink)e.Row.FindControl("hy_Client");
                hy_Client.Text = new CM_ClientBLL(applydetail.Client).Model.FullName;
                hy_Client.NavigateUrl = "~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=" + m.Client;
            }

            DropDownList ddl_BalanceMode = (DropDownList)e.Row.FindControl("ddl_BalanceMode");
            if (ddl_BalanceMode != null)
            {
                ddl_BalanceMode.Enabled = m.WriteOffCost < m.ApplyCost;
                if (m.BalanceMode > 0) ddl_BalanceMode.SelectedValue = m.BalanceMode.ToString();
            }
        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.RowIndex][0];

        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;
        _details.Remove(id.ToString());

        BindGrid();
        BindFeeApplyNoWriteOff();
    }

    #region 将申请单加入报销明细中
    protected void bt_AddToWriteOffList_Click(object sender, EventArgs e)
    {
        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;
        int maxid = 0;
        if (_details != null)
            if (_details.GetListItem().Count > 0) maxid = _details.GetListItem().Max(p => p.ID);
        maxid++;

        foreach (GridViewRow row in gv_FeeAplyList.Rows)
        {
            CheckBox cb_Selected = (CheckBox)row.FindControl("cb_Selected");
            if (cb_Selected.Checked)
            {
                int applyid = (int)gv_FeeAplyList.DataKeys[row.RowIndex][0];
                int applydetialid = (int)gv_FeeAplyList.DataKeys[row.RowIndex][1];

                FNA_FeeApplyBLL applyBLL = new FNA_FeeApplyBLL(applyid);
                FNA_FeeApply apply = applyBLL.Model;
                FNA_FeeApplyDetail applydetail = applyBLL.GetDetailModel(applydetialid);

                #region 陈列、返利费用判断协议是否关联合同
                IList<CM_Contract> contractList;
                contractList = CM_ContractBLL.GetModelList(@"ContractCode!='' AND ContractCode=MCS_SYS.dbo.UF_Spilt('" + applydetail.Remark + "',':',2)");
                if (applydetail.RelateContractDetail != 0)
                {
                    int ID = 0;
                    CM_ContractDetail detail = new CM_ContractBLL().GetDetailModel(applydetail.RelateContractDetail);
                    if (detail != null)
                        ID = detail.ContractID;
                    contractList = CM_ContractBLL.GetModelList("ID=" + ID.ToString());
                }

                if (contractList.Count > 0 && contractList[0].Classify < 3 && ATMT_AttachmentBLL.GetModelList("RelateType=35 AND RelateID=" + contractList[0].ID.ToString()).Count == 0)
                {
                    MessageBox.Show(this, "陈列、返利费用操作费用核销申请时，门店协议必须上传附件，请上传后再核销！");
                    return;
                }
                #endregion

                FNA_FeeWriteOffDetail m = new FNA_FeeWriteOffDetail();
                m.ID = maxid++;
                m.ApplyDetailID = applydetialid;
                m.Client = applydetail.Client;
                m.AccountTitle = applydetail.AccountTitle;
                m.ProductBrand = apply.ProductBrand;
                m.ApplyCost = applydetail.AvailCost;
                m.BeginMonth = applydetail.BeginMonth;
                m.EndMonth = applydetail.EndMonth;
                m.BeginDate = applydetail.BeginDate;
                m.EndDate = applydetail.EndDate;
                m.WriteOffCost = applydetail.AvailCost;
                m.Remark = applydetail.Remark;
                if (applydetail["BankVoucherNo"] != "") m.Remark += ",凭证:" + applydetail["BankVoucherNo"];

                if (applydetail.Remark.IndexOf("是否CA") > 0)
                    m.Remark = applydetail.Remark.Substring(applydetail.Remark.IndexOf("是否CA") - 1);
                if (_details == null)
                    _details = new ListTable<FNA_FeeWriteOffDetail>(new List<FNA_FeeWriteOffDetail>(), "ID");
                _details.Add(m);
            }
        }

        BindGrid();
        gv_FeeAplyList.PageIndex = 0;
        BindFeeApplyNoWriteOff();
    }
    #endregion


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_FeeAplyList.PageIndex = 0;
        BindFeeApplyNoWriteOff();
    }

    #region 保存GridView内里数据至Session
    private bool SaveGrid()
    {
        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;

        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            int applydetailid = (int)gv_List.DataKeys[row.RowIndex]["ApplyDetailID"];
            FNA_FeeWriteOffDetail m = _details[id.ToString()];

            #region 保存核销开始日期
            TextBox tbx_BeginDate = (TextBox)row.FindControl("tbx_BeginDate");
            if (tbx_BeginDate != null && tbx_BeginDate.Text != "")
            {
                DateTime begin = new DateTime(1900, 1, 1);

                if (DateTime.TryParse(tbx_BeginDate.Text, out begin))
                {
                    FNA_FeeApplyDetail apply = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                    if (begin >= apply.BeginDate && begin <= apply.EndDate && begin <= m.EndDate)
                    {
                        m.BeginDate = begin;
                        m.BeginMonth = AC_AccountMonthBLL.GetMonthByDate(begin);
                        _details.Update(m);
                    }
                    else
                    {
                        tbx_BeginDate.Text = m.BeginDate.ToString("yyyy-MM-dd");
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，报销开始日期必须在该费用申请时指定的日期范围之内！" +
                            apply.BeginDate.ToString("yyyy-MM-dd") + " — " + apply.EndDate.ToString("yyyy-MM-dd"));
                        return false;
                    }
                }
                else
                {
                    tbx_BeginDate.Text = m.BeginDate.ToString("yyyy-MM-dd");
                    MessageBox.Show(this, "开始日期填写格式必需为日期型！");
                    return false;
                }
            }
            #endregion

            #region 保存核销截止日期
            TextBox tbx_EndDate = (TextBox)row.FindControl("tbx_EndDate");
            if (tbx_EndDate != null && tbx_EndDate.Text != "")
            {
                DateTime end = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(tbx_EndDate.Text, out end))
                {
                    FNA_FeeApplyDetail apply = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                    if (end >= apply.BeginDate && end <= apply.EndDate && end >= m.BeginDate)
                    {
                        m.EndDate = end;
                        m.EndMonth = AC_AccountMonthBLL.GetMonthByDate(end);
                        _details.Update(m);
                    }
                    else
                    {
                        tbx_EndDate.Text = m.EndDate.ToString("yyyy-MM-dd");
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，报销截止日期必须在该费用申请时指定的日期范围之内！" +
                            apply.BeginDate.ToString("yyyy-MM-dd") + " — " + apply.EndDate.ToString("yyyy-MM-dd"));
                        return false;
                    }
                }
                else
                {
                    tbx_EndDate.Text = m.BeginDate.ToString("yyyy-MM-dd");
                    MessageBox.Show(this, "截止日期填写格式必需为日期型！");
                    return false;
                }
            }
            #endregion
            m["DiscountRate"] = "0";
            m["RebateRate"] = "0";
            //m["VATInvoiceNO"] = "";
            m["AcceptanceNO"] = "";
            m["InvoiceDate"] = "";
            m["DeductReason"] = "";
            #region 结余方式
            //if (m.WriteOffCost < m.ApplyCost)
            //{
            //    DropDownList ddl_BalanceMode = (DropDownList)row.FindControl("ddl_BalanceMode");
            //    m.BalanceMode = int.Parse(ddl_BalanceMode.SelectedValue);
            //}
            //else
            m.BalanceMode = 2;//不允许多次核销
            #endregion

            #region  保存备注
            TextBox tbx_Remark = (TextBox)row.FindControl("tbx_Remark");
            m.Remark = tbx_Remark.Text;
            _details.Update(m);
            #endregion
        }

        return true;
    }
    #endregion

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (!SaveGrid()) return;

        ListTable<FNA_FeeWriteOffDetail> _details = Session["FeeWriteOffDetails"] as ListTable<FNA_FeeWriteOffDetail>;

        if (_details.GetListItem().Count == 0)
        {
            MessageBox.Show(this, "对不起，报销科目明细中没有记录!");
            return;
        }

        Session["SuccessFlag"] = true;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
    }
    protected void ddl_FeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_FeeType.SelectedValue != "0")
        {
            ddl_AccountTitle.DataSource = AC_AccountTitleBLL.GetListByFeeType(int.Parse(ddl_FeeType.SelectedValue));
            ddl_AccountTitle.DataBind();
        }

        ddl_AccountTitle.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_AccountTitle.SelectedValue = "0";

    }
    protected void cb_Selected_CheckedChanged(object sender, EventArgs e)
    {
        //81,80,82 常规导购管理费（月付类）临时导购管理费（工服工卡等费用项）兼职导购工资次次月核销10日核销
        //SuperID IN (176,73)陈列费 月付，季度付 次月10号才能核销

        CheckBox t = (CheckBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        int applydetailid = (int)gv_FeeAplyList.DataKeys[rowIndex]["FNA_FeeApplyDetail_ID"];
        FNA_FeeApplyDetail applydetail = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
        CM_ContractDetail contractdetail= new CM_ContractBLL().GetDetailModel(applydetail.RelateContractDetail);
        try
        {
            if (applydetail.AccountTitle == 81 && applydetail.Remark.IndexOf("合同编号")>=0)
            {
                return;
            }
            if (applydetail.AccountTitle >= 80 && applydetail.AccountTitle <= 82)
            {
                t.Checked = CheckLimitWriteOffDate(applydetail.BeginMonth + 2);
            }
            else if (AC_AccountTitleBLL.GetModelList("SuperID IN (176,73)").Where(p => p.ID == applydetail.AccountTitle).ToList().Count > 0
                && contractdetail != null && contractdetail.PayMode != 12 && contractdetail.PayMode != 6 && contractdetail.BearMode==1)
            {
                t.Checked = CheckLimitWriteOffDate(applydetail.BeginMonth + 1);
            }
        }
        catch (Exception)
        {
            
            throw;
        }

    }

    private bool CheckLimitWriteOffDate(int month)
    {
        bool flag = true;
        AC_AccountMonthBLL _bll = new AC_AccountMonthBLL(month);
        if (_bll.Model.EndDate.AddDays(-11) > DateTime.Now)
        {
            flag = false;
            MessageBox.Show(this, "对不起，" + _bll.Model.EndDate.AddDays(-11).ToString() + "后才可操作该费用的核销!");
        }
        return flag;
    }
}
