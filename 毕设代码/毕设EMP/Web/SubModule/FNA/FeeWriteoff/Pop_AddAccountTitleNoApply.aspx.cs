using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.Model.Pub;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Model;
using MCSFramework.BLL;

public partial class SubModule_FNA_FeeWriteoff_Pop_AddAccountTitleNoApply : System.Web.UI.Page
{
    protected DateTime BeginDate, EndDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));
        if (!IsPostBack)
        {
            #region 获取参数
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 1 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 1 : int.Parse(Request.QueryString["FeeType"]);
            #endregion

            BindDropDown();

            #region 获取当前会计月的开始及截止日期
            int month = AC_AccountMonthBLL.GetCurrentMonth();
            AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
            BeginDate = m.BeginDate;
            EndDate = m.EndDate;
            #endregion

            #region 传递或创建空的费用明细列表
            ListTable<FNA_FeeWriteOffDetail> _details;

            if (Session["FeeWriteOffDetails"] != null)
            {
                _details = (ListTable<FNA_FeeWriteOffDetail>)Session["FeeWriteOffDetails"];
                int max = 0;
                foreach (FNA_FeeWriteOffDetail item in _details.GetListItem())
                {
                    if (item.ID > max) max = item.ID;
                }
                ViewState["MaxID"] = max;
                ViewState["Details"] = _details;
                BindGrid();
                AddEmptyDetail();
            }
            else
            {
                _details = new ListTable<FNA_FeeWriteOffDetail>(new List<FNA_FeeWriteOffDetail>(), "ID");
                ViewState["MaxID"] = 0;
                ViewState["Details"] = _details;
                AddEmptyDetail();
            }
            #endregion

            if ((int)ViewState["FeeType"] == ConfigHelper.GetConfigInt("ManagementCostType"))
            {
                tb_TeleFee.Visible = true;
                tb_MobileFee.Visible = true;

                //绑定显示报销电话费与手机费
                BindTeleList();
            }
        }
    }

    private void BindDropDown()
    {
    }

    #region 返回指定类型的会计科目
    protected IList<AC_AccountTitle> GetAccountTitleList()
    {
        return AC_AccountTitleBLL.GetListByFeeType((int)ViewState["FeeType"]).
            Where(p => (p["MustApplyFirst"] == "N" || p.ID == 1)).ToList();
    }

    protected IList<AC_AccountMonth> GetAccountMonth()
    {
        return AC_AccountMonthBLL.GetModelList("BeginDate BETWEEN DateAdd(month,-3,DATEADD(day,-7,GETDATE())) AND GETDATE()");
    }
    #endregion

    protected void AddEmptyDetail()
    {
        SaveGrid();

        ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        for (int i = 0; i < 5; i++)
        {
            ViewState["MaxID"] = (int)ViewState["MaxID"] + 1;

            FNA_FeeWriteOffDetail item;

            item = new FNA_FeeWriteOffDetail();
            item.AccountTitle = 1;
            item.ApplyCost = 0;
            item.WriteOffCost = 0;
            item.ID = (int)ViewState["MaxID"];

            #region 获取当前会计月的开始及截止日期
            int month = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(-7));
            AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
            item.BeginMonth = m.ID;
            item.EndMonth = m.ID;
            item.BeginDate = m.BeginDate;
            item.EndDate = m.EndDate;
            #endregion

            item.Remark = "";
            _details.Add(item);             //新增科目 
        }
        BindGrid();
    }

    #region 绑定费用申请明细列表
    private void BindGrid()
    {
        ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        gv_List.BindGrid<FNA_FeeWriteOffDetail>(_details.GetListItem().OrderBy(p => p.ID).ToList());

        //求合计
        lb_TotalCost.Text = _details.GetListItem().Sum(p => p.WriteOffCost + p.AdjustCost).ToString("0.###");
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
            FNA_FeeWriteOffDetail item = _details.GetListItem().FirstOrDefault(p => p.ID == id);

            if (item != null && item.AccountTitle > 1)
            {
                if (!string.IsNullOrEmpty(item["TeleFeeRelateTelephone"]) ||
                    !string.IsNullOrEmpty(item["MobileFeeRelateStaff"]))
                {
                    if (e.Row.FindControl("ddl_AccountTitle") != null)
                        ((DropDownList)e.Row.FindControl("ddl_AccountTitle")).Enabled = false;

                    if (e.Row.FindControl("ddl_BeginMonth") != null)
                        ((DropDownList)e.Row.FindControl("ddl_BeginMonth")).Enabled = false;

                    if (e.Row.FindControl("tbx_WriteOffCost") != null)
                        ((TextBox)e.Row.FindControl("tbx_WriteOffCost")).Enabled = false;

                    if (e.Row.FindControl("tbx_Remark") != null)
                        ((TextBox)e.Row.FindControl("tbx_Remark")).Enabled = false;
                }
            }
        }
    }
    #endregion

    #region 保存GridView内里数据至内存
    private bool SaveGrid()
    {
        ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        foreach (GridViewRow gr in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[gr.RowIndex]["ID"];
            FNA_FeeWriteOffDetail item = _details.GetListItem().FirstOrDefault(p => p.ID == id);
            if (item == null)
            {
                item = new FNA_FeeWriteOffDetail();
                item.ID = id;
            }

            item.AccountTitle = int.Parse(((DropDownList)gr.FindControl("ddl_AccountTitle")).SelectedValue);

            //忽略手工直填的电话费及手机费,必须通过专用面板加入报销信息
            if (item.AccountTitle == ConfigHelper.GetConfigInt("TeleCostACTitle") ||
                item.AccountTitle == ConfigHelper.GetConfigInt("MobileCostACTitle"))
            {
                if (((TextBox)gr.FindControl("tbx_WriteOffCost")).Enabled) _details.Remove(item);
                continue;
            }

            if (((TextBox)gr.FindControl("tbx_WriteOffCost")).Text.Trim() != "")
                item.WriteOffCost = decimal.Parse(((TextBox)gr.FindControl("tbx_WriteOffCost")).Text.Trim());

            if (item.AccountTitle > 1 && item.WriteOffCost != 0)
            {
                #region 获取选择的会计月
                if (gr.FindControl("ddl_BeginMonth") != null &&
                    item.BeginMonth.ToString() != ((DropDownList)gr.FindControl("ddl_BeginMonth")).SelectedValue)
                {
                    item.BeginMonth = int.Parse(((DropDownList)gr.FindControl("ddl_BeginMonth")).SelectedValue);
                    item.EndMonth = item.BeginMonth;

                    AC_AccountMonth month = new AC_AccountMonthBLL(item.BeginMonth).Model;
                    item.BeginDate = month.BeginDate;
                    item.EndDate = month.EndDate;
                }
                #endregion

                item.Remark = ((TextBox)gr.FindControl("tbx_Remark")).Text;

                if (item.Remark == "")
                {
                    MessageBox.Show(this, "备注必填");
                    return false;
                }
                _details.Update(item);
            }
            else
            {
                _details.Remove(item);
            }
        }

        return true;
    }
    #endregion

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaveGrid();
        int id = (int)gv_List.DataKeys[e.RowIndex]["ID"];

        ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        _details.Remove(id.ToString());

        AddEmptyDetail();

        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        AddEmptyDetail();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (SaveGrid())
        {
            Session["FeeWriteOffDetails"] = ViewState["Details"];
            Session["SuccessFlag"] = true;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
        }
    }

    #region 报销电话费
    private void BindTeleList()
    {
        IList<CM_PropertyInTelephone> teles = CM_PropertyInTelephoneBLL.GetListByOrganizeCity((int)ViewState["OrganizeCity"]);
        ddl_Tele.DataSource = teles;
        ddl_Tele.DataBind();
        ddl_Tele.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_TeleCostMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate BETWEEN DateAdd(month,-3,DATEADD(day,-7,GETDATE())) AND GETDATE()");
        ddl_TeleCostMonth.DataBind();
        ddl_TeleCostMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(-7)).ToString();

        ddl_MobileCostMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate BETWEEN DateAdd(month,-3,DATEADD(day,-7,GETDATE())) AND GETDATE()");
        ddl_MobileCostMonth.DataBind();
        ddl_MobileCostMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(-7)).ToString();

        select_MobileStaff.SelectValue = Session["UserID"].ToString();
        select_MobileStaff.SelectText = Session["UserRealName"].ToString();
        select_MobileStaff_SelectChange(null, null);
    }

    protected void ddl_Tele_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Tele.SelectedValue != "0")
        {
            int teleid = int.Parse(ddl_Tele.SelectedValue);
            CM_PropertyInTelephone tele = new CM_PropertyInTelephoneBLL(teleid).Model;

            lb_TeleApplyInfo.Text = string.Format("电话费:{0:0.##元},宽带费:{1:0.##元}", tele.TeleCost, tele.NetCost);
            lb_TeleApplyCost.Text = (tele.TeleCost + tele.NetCost).ToString("0.##");
            tbx_TeleCost.Text = lb_TeleApplyCost.Text;

            bt_AddTeleFee.Enabled = true;
        }
        else
        {
            bt_AddTeleFee.Enabled = false;
        }
    }

    protected void bt_AddTeleFee_Click(object sender, EventArgs e)
    {
        if (ddl_Tele.SelectedValue == "0") return;

        int teleid = int.Parse(ddl_Tele.SelectedValue);
        CM_PropertyInTelephone tele = new CM_PropertyInTelephoneBLL(teleid).Model;

        decimal writeoffcost = decimal.Parse(tbx_TeleCost.Text);
        decimal applycost = tele.TeleCost + tele.NetCost;

        if (writeoffcost > applycost)
        {
            MessageBox.Show(this, "对不起，实际报销金额不能超过申请限额" + lb_TeleApplyCost.Text + "!");
            tbx_TeleCost.Text = lb_TeleApplyCost.Text;
            return;
        }

        SaveGrid();
        ViewState["MaxID"] = (int)ViewState["MaxID"] + 1;

        #region 组织电话费核销明细
        FNA_FeeWriteOffDetail item = new FNA_FeeWriteOffDetail();
        item.ID = (int)ViewState["MaxID"];
        item.AccountTitle = ConfigHelper.GetConfigInt("TeleCostACTitle");
        item.ApplyCost = applycost;
        item.WriteOffCost = writeoffcost;

        item["TeleFeeRelateTelephone"] = tele.ID.ToString();
        item.Client = tele.Client;

        #region 获取当前会计月的开始及截止日期
        int month = int.Parse(ddl_TeleCostMonth.SelectedValue);
        AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
        item.BeginMonth = m.ID;
        item.EndMonth = m.ID;
        item.BeginDate = m.BeginDate;
        item.EndDate = m.EndDate;
        #endregion

        if (tele.Client > 0)
        {
            CM_Client client = new CM_ClientBLL(tele.Client).Model;
            if (client != null) item.Remark = "物业:" + client.FullName + ";";
        }
        item.Remark += "电话号码:" + ddl_Tele.SelectedItem.Text + ";月份:" + ddl_TeleCostMonth.SelectedItem.Text +
            ";限额:" + lb_TeleApplyInfo.Text + ";";

        if (tbx_TeleRemark.Text != "")
            item.Remark += "说明:" + tbx_TeleRemark.Text;
        #endregion

        if (FNA_FeeWriteOffBLL.CheckTeleFeeHasWriteOff(item.AccountTitle, item.BeginMonth, tele.ID))
        {
            MessageBox.Show(this, "该电话指定月的费用，已在其他报销单中报销，请勿重复报销!");
        }
        else
        {
            ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
            if (_details.GetListItem().FirstOrDefault(p =>
                p.AccountTitle == item.AccountTitle &&
                p.BeginMonth == item.BeginMonth &&
                p["TeleFeeRelateTelephone"] == item["TeleFeeRelateTelephone"]) == null)
            {
                _details.Add(item);             //新增科目 
            }
            else
            {
                MessageBox.Show(this, "该电话指定月的费用，已在本报销单中，请勿重复报销!");
            }
        }
        BindGrid();
        AddEmptyDetail();
    }

    protected void select_MobileStaff_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        int staffid = 0;
        if (int.TryParse(select_MobileStaff.SelectValue, out staffid) && staffid > 0)
        {
            Org_Staff staff = new Org_StaffBLL(staffid).Model;
            if (staff != null)
            {
                if (string.IsNullOrEmpty(staff["ManageInfo11"])) staff["ManageInfo11"] = "0";
                decimal applycost = 0;
                decimal.TryParse(staff["ManageInfo11"], out applycost);

                lb_MobileNumber.Text = staff["Mobile"];
                lb_MobileApplyCost.Text = applycost.ToString("0.##");
                tbx_MobileCost.Text = lb_MobileApplyCost.Text;

                if (applycost > 0) bt_AddMobileFee.Enabled = true;
            }
        }
        else
        {
            bt_AddMobileFee.Enabled = false;
        }
    }

    protected void bt_AddMobileFee_Click(object sender, EventArgs e)
    {
        int staffid = 0;
        int.TryParse(select_MobileStaff.SelectValue, out staffid);
        if (staffid == 0) return;

        Org_Staff staff = new Org_StaffBLL(staffid).Model;
        if (staff == null) return;

        if (string.IsNullOrEmpty(staff["ManageInfo11"])) staff["ManageInfo11"] = "0";
        decimal applycost = 0;
        decimal.TryParse(staff["ManageInfo11"], out applycost);
        if (applycost <= 0) return;


        decimal writeoffcost = decimal.Parse(tbx_MobileCost.Text);

        if (writeoffcost > applycost)
        {
            MessageBox.Show(this, "对不起，实际报销金额不能超过申请限额" + lb_MobileApplyCost.Text + "!");
            tbx_MobileCost.Text = lb_MobileApplyCost.Text;
            return;
        }

        SaveGrid();
        ViewState["MaxID"] = (int)ViewState["MaxID"] + 1;

        #region 组织手机费核销明细
        FNA_FeeWriteOffDetail item = new FNA_FeeWriteOffDetail();
        item.ID = (int)ViewState["MaxID"];
        item.AccountTitle = ConfigHelper.GetConfigInt("MobileCostACTitle");
        item.ApplyCost = applycost;
        item.WriteOffCost = writeoffcost;

        item["MobileFeeRelateStaff"] = staff.ID.ToString();

        #region 获取当前会计月的开始及截止日期
        int month = int.Parse(ddl_MobileCostMonth.SelectedValue);
        AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
        item.BeginMonth = m.ID;
        item.EndMonth = m.ID;
        item.BeginDate = m.BeginDate;
        item.EndDate = m.EndDate;
        #endregion

        item.Remark = "员工:" + staff.RealName + ";";
        item.Remark += "手机号码:" + staff["Mobile"] + ";月份:" + ddl_MobileCostMonth.SelectedItem.Text +
            ";限额:" + lb_MobileApplyCost.Text + ";";

        if (tbx_MobileRemark.Text != "")
            item.Remark += "说明:" + tbx_MobileRemark.Text;
        #endregion
        if (FNA_FeeWriteOffBLL.CheckMobileFeeHasWriteOff(item.AccountTitle, item.BeginMonth, staffid))
        {
            MessageBox.Show(this, "该员工指定月的手机费，已在其他报销单中报销，请勿重复报销!");
        }
        else
        {
            ListTable<FNA_FeeWriteOffDetail> _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
            if (_details.GetListItem().FirstOrDefault(p =>
                p.AccountTitle == item.AccountTitle &&
                p.BeginMonth == item.BeginMonth &&
                p["MobileFeeRelateStaff"] == item["MobileFeeRelateStaff"]) == null)
            {
                _details.Add(item);             //新增科目 
            }
            else
            {
                MessageBox.Show(this, "该员工指定月的手机费，已在本报销单中，请勿重复报销!");
            }
        }
        BindGrid();
        AddEmptyDetail();
    }
    #endregion




}
