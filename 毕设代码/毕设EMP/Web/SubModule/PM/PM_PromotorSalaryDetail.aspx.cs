using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.Promotor;
using MCSFramework.BLL.Promotor;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;

public partial class SubModule_PM_PM_PromotorSalaryDetail : System.Web.UI.Page
{
    private DropDownList ddl_BasePayMode;               //底薪模式
    private DropDownList ddl_SeniorityPayMode;         //工龄工资模式
    private DropDownList ddl_BasePaySubsidyMode;   //底薪补贴类别
    private DropDownList ddl_MinimumWageMode;     //保底工资类型
    private DropDownList ddl_InsuranceMode;            //社保模式
    private DropDownList ddl_SalesType;//销售类别
    private CheckBox chk_arrivetarget = new CheckBox();
    private TextBox txt_AvgSales;//首两月均销量
    private TextBox txt_BaseFeeRate;//底薪费率 
    private TextBox txt_FloatingTarget;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 获取界面控件
        ddl_BasePayMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_BasePayMode");
        ddl_BasePaySubsidyMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_BasePaySubsidyMode");
        ddl_MinimumWageMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_MinimumWageMode");
        ddl_InsuranceMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_InsuranceMode");
        ddl_SeniorityPayMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_SeniorityPayMode");
        ddl_SalesType = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_SalesType");

        ddl_BasePayMode.AutoPostBack = true;
        ddl_BasePayMode.SelectedIndexChanged += new EventHandler(ddl_BasePayMode_SelectedIndexChanged);

        ddl_BasePaySubsidyMode.AutoPostBack = true;
        ddl_BasePaySubsidyMode.SelectedIndexChanged += new EventHandler(ddl_BasePaySubsidyMode_SelectedIndexChanged);

        ddl_MinimumWageMode.AutoPostBack = true;
        ddl_MinimumWageMode.SelectedIndexChanged += new EventHandler(ddl_MinimumWageMode_SelectedIndexChanged);

        ddl_InsuranceMode.AutoPostBack = true;
        ddl_InsuranceMode.SelectedIndexChanged += new EventHandler(ddl_InsuranceMode_SelectedIndexChanged);

        txt_AvgSales = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_AvgSales");
        txt_BaseFeeRate = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_BaseFeeRate");
        txt_FloatingTarget = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_FloatingTarget");

        chk_arrivetarget.Text = "是否达到过上限";
        chk_arrivetarget.Enabled = false;
        txt_FloatingTarget.Parent.Controls.Add(chk_arrivetarget);
        #endregion       
        if (!IsPostBack)
        {
            ViewState["PromotorSalaryID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["PromotorID"] = Request.QueryString["PromotorID"] == null ? 0 : int.Parse(Request.QueryString["PromotorID"]);
            ViewState["AvgSales"] = 0m; ViewState["BaseFeeRate"] = 0m;
            if ((int)ViewState["PromotorSalaryID"] > 0)
            {
                PM_PromotorSalary m = new PM_PromotorSalaryBLL((int)ViewState["PromotorSalaryID"]).Model;
                if (m != null)
                {
                    BindData(m);
                    bt_Add.Visible = false;
                }
                else
                    Response.Redirect("PM_PromotorDetail.aspx");
            }
            else if ((int)ViewState["PromotorID"] > 0)
            {
                IList<PM_PromotorSalary> lists = PM_PromotorSalaryBLL.GetModelList("Promotor=" + ViewState["PromotorID"].ToString() +
                    " AND State <= 3  ORDER BY State DESC,ID DESC");
                if (lists.Count == 0)
                {
                    //新增薪资标准
                    #region 判断当前导购是否有所在工作的门店
                    if (PM_PromotorInRetailerBLL.GetModelList("Promotor=" + ViewState["PromotorID"].ToString()).Count == 0)
                    {
                        MessageBox.ShowAndRedirect(this, "请设置该导购员所在的门店!", "PM_PromotorDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString());
                        return;
                    }
                    #endregion

                    BindData(null);
                    bt_Add.Visible = false;
                }
                else
                {
                    BindData(lists[0]);
                    if (lists.FirstOrDefault(p => p.State < 3) != null) bt_Add.Visible = false;
                }              
            }
            else
            {
                Response.Redirect("PM_PromotorDetail.aspx");
            }
        }
    }

    void ddl_BasePayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox tbx_BasePay = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_BasePay");
        if (tbx_BasePay == null) return;

        if (ddl_BasePayMode.SelectedValue == "1" || ddl_BasePayMode.SelectedValue == "4" || ddl_BasePayMode.SelectedValue == "5")
        {
            #region 抓取固定底薪标准
            decimal basepay = (decimal)ViewState["BasePay"];
            tbx_BasePay.Text = basepay.ToString("0.##");
            tbx_BasePay.Enabled = false;
            #endregion
            SetControlsEnable(true);
            //if (ddl_InsuranceMode != null)
            //{
            //    ddl_InsuranceMode.Items.FindByValue("1").Enabled = true;
            //    ddl_InsuranceMode_SelectedIndexChanged(null, null);
            //}
            switch (ddl_BasePayMode.SelectedValue)
            {
                case "1":
                    setdisabledfloatingcontorl(false);
                    break;
                case "4":

                    if ((int)ViewState["PromotorSalaryID"] == 0)
                    {
                        txt_AvgSales.Text = ((decimal)ViewState["AvgSales"]).ToString("0.##");
                        txt_BaseFeeRate.Text = ((decimal)ViewState["BaseFeeRate"]).ToString("0.##");
                    }                    
                    txt_FloatingTarget.Enabled = true;
                    ddl_SalesType.Enabled = false;
                    txt_AvgSales.Enabled = false;
                    txt_BaseFeeRate.Enabled = false;
                    //chk_arrivetarget.Enabled = false;//20130313

                    
                    chk_arrivetarget.Enabled = true;

                    //IList<PM_PromotorSalary> pmsalary = PM_PromotorSalaryBLL.GetModelList("State=3 AND Promotor=" + ViewState["PromotorID"].ToString());
                    //if (pmsalary.Count != 0)
                    //{
                    //    chk_arrivetarget.Checked = pmsalary[0]["ISArriveTarget"] == "1";
                    //}
                    break;
                case "5":
                    txt_FloatingTarget.Enabled = false;
                    txt_AvgSales.Enabled = false;
                    txt_BaseFeeRate.Enabled = true;
                    ddl_SalesType.Enabled = true;
                    break;
            }
        }
        else
        {
            setdisabledfloatingcontorl(false);
            tbx_BasePay.Text = "0";      //非固定底薪，底薪标准字段为0         
            tbx_BasePay.Enabled = false;
            if (ddl_BasePayMode.SelectedValue == "3")
            {
                SetControlsEnable(false);
            }
            //兼职底职无社保补贴项
            if (ddl_InsuranceMode != null)
            {
                if (ddl_InsuranceMode.SelectedValue == "1") ddl_InsuranceMode.SelectedValue = "0";
               // ddl_InsuranceMode.Items.FindByValue("1").Enabled = false;
                ddl_InsuranceMode_SelectedIndexChanged(null, null);
            }
        }
    }
    private void setdisabledfloatingcontorl(bool flag)
    {
        if (!flag)
        {
            txt_FloatingTarget.Text = "0";
            txt_AvgSales.Text = "0";
            txt_BaseFeeRate.Text = "0";
            ddl_SalesType.SelectedValue = "0";

            txt_AvgSales.Enabled = false;
            txt_FloatingTarget.Enabled = false;
            txt_BaseFeeRate.Enabled = false;
            ddl_SalesType.Enabled = false;
        }
    }
    void ddl_InsuranceMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_InsuranceSubsidy = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_InsuranceSubsidy");
        if (ddl_InsuranceSubsidy == null) return;

        if (ddl_InsuranceMode.SelectedValue == "1")
        {
            //自购社保，给予补贴
            ddl_InsuranceSubsidy.Enabled = true;
            ddl_InsuranceSubsidy.SelectedValue = "100";
        }
        else
        {
            ddl_InsuranceSubsidy.SelectedValue = "0";         
            ddl_InsuranceSubsidy.Enabled = false;
        }
    }

    void ddl_MinimumWageMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox tbx_MinimumWage = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_MinimumWage");
        TextBox tbx_MinimumWageBeginDate = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_MinimumWageBeginDate");
        TextBox tbx_MinimumWageEndDate = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_MinimumWageEndDate");

        if (tbx_MinimumWage == null || tbx_MinimumWageBeginDate == null || tbx_MinimumWageEndDate == null) return;

        switch (ddl_MinimumWageMode.SelectedValue)
        {
            case "0":      //无保底
                tbx_MinimumWage.Text = "0";
                tbx_MinimumWageBeginDate.Text = "";
                tbx_MinimumWageEndDate.Text = "";

                tbx_MinimumWage.Enabled = false;
                tbx_MinimumWageBeginDate.Enabled = false;
                tbx_MinimumWageEndDate.Enabled = false;
                break;
            case "1":  //标准保底
                if (tbx_MinimumWageBeginDate.Text == "")
                    tbx_MinimumWageBeginDate.Text = new AC_AccountMonthBLL
                        (AC_AccountMonthBLL.GetCurrentMonth()).Model.EndDate.AddDays(1).ToString("yyyy-MM-dd");

                #region 系统根据导购员所在区域属性自动设定标准保底额
                decimal MinumumWage = (decimal)ViewState["MinumumWage"];
                tbx_MinimumWage.Text = MinumumWage.ToString("0.##");
                #endregion

                tbx_MinimumWage.Enabled = false;
                tbx_MinimumWageBeginDate.Enabled = true;
                tbx_MinimumWageEndDate.Enabled = true;
                break;
            case "2":  //特殊保底
                tbx_MinimumWage.Enabled = true;
                tbx_MinimumWageBeginDate.Enabled = true;
                tbx_MinimumWageEndDate.Enabled = true;
                break;
        }
    }

    void ddl_BasePaySubsidyMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox tbx_BasePaySubsidy = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_BasePaySubsidy");
        TextBox tbx_BasePaySubsidyBeginDate = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_BasePaySubsidyBeginDate");
        TextBox tbx_BasePaySubsidyEndDate = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_BasePaySubsidyEndDate");

        if (tbx_BasePaySubsidy == null || tbx_BasePaySubsidyBeginDate == null || tbx_BasePaySubsidyEndDate == null) return;

        switch (ddl_BasePaySubsidyMode.SelectedValue)
        {
            case "0":      //无底薪补贴
                tbx_BasePaySubsidy.Text = "0";
                tbx_BasePaySubsidyBeginDate.Text = "";
                tbx_BasePaySubsidyEndDate.Text = "";

                tbx_BasePaySubsidy.Enabled = false;
                tbx_BasePaySubsidyBeginDate.Enabled = false;
                tbx_BasePaySubsidyEndDate.Enabled = false;
                break;
            case "1":      //临时补贴
                if (tbx_BasePaySubsidyBeginDate.Text == "")
                    tbx_BasePaySubsidyBeginDate.Text = new AC_AccountMonthBLL
                        (AC_AccountMonthBLL.GetCurrentMonth()).Model.EndDate.AddDays(1).ToString("yyyy-MM-dd");

                tbx_BasePaySubsidy.Enabled = true;
                tbx_BasePaySubsidyBeginDate.Enabled = true;
                tbx_BasePaySubsidyEndDate.Enabled = true;
                break;
            case "2":      //长期固定补贴
                if (tbx_BasePaySubsidyBeginDate.Text == "")
                    tbx_BasePaySubsidyBeginDate.Text = new AC_AccountMonthBLL
                        (AC_AccountMonthBLL.GetCurrentMonth()).Model.EndDate.AddDays(1).ToString("yyyy-MM-dd");
                tbx_BasePaySubsidyEndDate.Text = "";

                tbx_BasePaySubsidy.Enabled = true;
                tbx_BasePaySubsidyBeginDate.Enabled = true;
                tbx_BasePaySubsidyEndDate.Enabled = false;
                break;
        }
    }

    private void BindData(PM_PromotorSalary m)
    {
        if (m != null)
        {
            ViewState["PromotorID"] = m.Promotor;
            ViewState["PromotorSalaryID"] = m.ID;
        }

        #region 获取当前导购标准薪资、保底薪资、导购管理费
        if ((int)ViewState["PromotorID"] > 0)
        {
            decimal basepay = 0, minumumwage = 0, rtmanagecost = 0;
            new PM_PromotorBLL((int)ViewState["PromotorID"]).GetStdPay(out basepay, out minumumwage, out rtmanagecost);
            ViewState["BasePay"] = basepay;
            ViewState["MinumumWage"] = minumumwage;
            ViewState["RTManageCost"] = rtmanagecost;
        }
        #endregion
        PM_Promotor p = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
        if (p == null) Response.Redirect("PM_PromotorDetail.aspx");
        if (m == null)
        {
            m = new PM_PromotorSalary();
            m.Promotor = (int)ViewState["PromotorID"];
            m.State = 1;
            m.InsertStaff = (int)Session["UserID"];
            m.RTManageCost = (decimal)ViewState["RTManageCost"];           
            if (p["Classfiy"] == "1" || p["Classfiy"] == "2")
            {
                m.SeniorityPayMode = 1;
            }             
        }
        if (p["State"] == "2") bt_Add.Visible = false;

        UC_DetailView1.BindData(m);
        chk_arrivetarget.Checked = m["ISArriveTarget"] == "1";
        UC_DetailView1.SetControlsEnable(m.State == 1);
        bt_OK.Enabled = m.State == 1;

        if (m.ID != 0)
        {
            bt_Delete.Enabled = m.State == 1;
            bt_Submit.Enabled = m.State == 1;
            bt_Approve.Enabled = m.State == 1;
        }
        else
        {
            bt_Submit.Enabled = false;
            bt_Approve.Enabled = false;
            bt_Delete.Enabled = false;
        }
        if (m.BasePayMode != 4 && m.BasePayMode != 5)
        {
            setdisabledfloatingcontorl(false);
        }
      

        //导购为新入职、或离职状态
        if (p.ApproveFlag == 2) bt_Submit.Visible = false;
        //if (p.Dimission == 2) bt_Add.Visible = false;

        if (m.RTManageCost > 0 && (decimal)ViewState["RTManageCost"] == m.RTManageCost)
        {
            TextBox tbx = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_RTManageCost");
            if (tbx != null) tbx.Enabled = false;
        }
        if (m.State < 2)
        {
            #region 兼职导购的限定
            try
            {
                if (ddl_BasePayMode != null)
                {
                    if (p["Classfiy"] != "1" && p["Classfiy"] != "2")   //非专职、非流导，认为是兼职导购
                    {
                        //兼职
                        //ddl_BasePayMode.Items[0].Enabled = false;
                        //ddl_BasePayMode.Items[1].Enabled = false;
                        //ddl_BasePayMode.Items[2].Enabled = false;
                        SetControlsEnable(false);
                        ddl_BasePayMode.SelectedValue = "3";
                        ddl_BasePayMode.Enabled = false;
                    }
                    else
                    {                       
                        Addr_OrganizeCityBLL _bll = new Addr_OrganizeCityBLL(p.OrganizeCity);
                        string[] city3s = Addr_OrganizeCityParamBLL.GetValueByType(1, 19).Split(new char[] { ',', '，', ';', '；' });
                        int city3ID = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", p.OrganizeCity, ConfigHelper.GetConfigInt("OrganizePartCity-CityLevel"));

                        if (city3s.Contains(city3ID.ToString()))                       
                        {
                            ddl_BasePayMode.Items.Remove(new ListItem("浮动底薪(非华南)", "4"));
                        }
                        else
                        {
                            decimal AvgSales = 0, BaseFeeRate = 0;                           
                            int SalaryDelayDays = ConfigHelper.GetConfigInt("SalaryDelayDays");
                            ddl_SalesType.Enabled = false;
                            new PM_PromotorSalaryBLL().GetFloatingInfo(p.ID, AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-SalaryDelayDays)) - 1, out AvgSales, out BaseFeeRate);
                            
                            if (AvgSales == 0 && BaseFeeRate == 0)//首两月不是固定底薪，不能选择浮动底薪
                            {
                                ddl_BasePayMode.Items.Remove(new ListItem("浮动底薪(非华南)", "4"));
                            }
                            else
                            {
                                ViewState["AvgSales"] = AvgSales;
                                ViewState["BaseFeeRate"] = BaseFeeRate;                               
                            }

                            ddl_BasePayMode.Items.Remove(new ListItem("浮动底薪(华南)", "5"));
                        }
                        ddl_BasePayMode.Items.Remove(new ListItem("兼职底薪", "3"));
                        //50岁只能选择商保(5)或自购(8)
                        DateTime Birthday;
                        if (DateTime.TryParse(p["Birthday"], out Birthday) && Birthday.AddYears(50) < DateTime.Now)
                        {
                            foreach (ListItem item in ddl_InsuranceMode.Items)
                            {
                                if (item.Value != "5" && item.Value != "8" && item.Value != "0")
                                {
                                    item.Enabled = false;
                                }
                            }
                        }
                        //专职或流导
                        //ddl_BasePayMode.Items[3].Enabled = false;
                    }

                }
            }
            catch { }
            #endregion

            ddl_BasePayMode_SelectedIndexChanged(null, null);
            ddl_BasePaySubsidyMode_SelectedIndexChanged(null, null);
            ddl_InsuranceMode_SelectedIndexChanged(null, null);
            ddl_MinimumWageMode_SelectedIndexChanged(null, null);
        }
        BindGrid();
    }

    private void SetControlsEnable(bool EnableFlag)
    {
        if (ddl_BasePaySubsidyMode != null)
        {
            //兼职无法申请底薪补贴
            if (!EnableFlag) ddl_BasePaySubsidyMode.SelectedValue = "0";
            ddl_BasePaySubsidyMode.Enabled = EnableFlag;
        }

        if (ddl_MinimumWageMode != null)
        {
            //兼职无法申请最低保底
            if (!EnableFlag) ddl_MinimumWageMode.SelectedValue = "0";
            ddl_MinimumWageMode.Enabled = EnableFlag;
        }

        if (ddl_SeniorityPayMode != null)
        {
            //兼职不可以选则工龄模式
            if (!EnableFlag) ddl_SeniorityPayMode.SelectedValue = "0";
            ddl_SeniorityPayMode.Enabled = EnableFlag;
        }

        if (ddl_InsuranceMode != null)
        {
            //兼职无法申请社保
            if (!EnableFlag) ddl_InsuranceMode.SelectedValue = "0";
            ddl_InsuranceMode.Enabled = EnableFlag;
        }
    }

    private void BindGrid()
    {
        gv_list.ConditionString = "Promotor=" + ViewState["PromotorID"].ToString();
        gv_list.BindGrid();
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PM_PromotorSalaryBLL bll;
        if ((int)ViewState["PromotorSalaryID"] == 0)
            bll = new PM_PromotorSalaryBLL();
        else
            bll = new PM_PromotorSalaryBLL((int)ViewState["PromotorSalaryID"]);

        UC_DetailView1.GetData(bll.Model);

        #region 数据录入判断
        if (bll.Model.BasePayMode == 0)
        {
            MessageBox.Show(this, "请选择正确的底薪模式!");
            return;
        }
        if (bll.Model.BasePayMode == 1)
        {
            if (bll.Model.SeniorityPayMode == 0)
            {
                MessageBox.Show(this, "当底薪模式为固定底薪时，必须选择工龄工资模式!");
                return;
            }
            if (bll.Model.InsuranceMode == 0)
            {
                MessageBox.Show(this, "当底薪模式为固定底薪时，必须选择社保!");
                return;
            }
        }
        if (bll.Model.BasePaySubsidyMode != 0 && bll.Model.BasePaySubsidy == 0)
        {
            MessageBox.Show(this, "当选择了底薪补贴类型时，请设定补贴金额!");
            return;
        }

        if (bll.Model.BasePaySubsidyMode != 0 && bll.Model.BasePaySubsidyBeginDate.Year == 1900)
        {
            MessageBox.Show(this, "当选择了底薪补贴类型时，请设定底薪补贴起始日期!");
            return;
        }
        if (bll.Model.BasePaySubsidyMode == 1 && bll.Model.BasePaySubsidyEndDate.Year == 1900)
        {
            MessageBox.Show(this, "当选择了底薪补贴类型为临时补贴时，请设定底薪补贴截止日期!");
            return;
        }

        decimal BasePaySubsidylimt = 0;
        decimal.TryParse(Addr_OrganizeCityParamBLL.GetValueByType(1, 22), out BasePaySubsidylimt);
        if (BasePaySubsidylimt > 0 && BasePaySubsidylimt < bll.Model.BasePaySubsidy)
        {
            MessageBox.Show(this, "底薪补贴不得超过上限" + BasePaySubsidylimt.ToString() + "元");
            return;
        }
       
        if (bll.Model.MinimumWageMode == 2 && bll.Model.MinimumWage == 0)
        {
            MessageBox.Show(this, "当选择了特殊保底时，请设定保底金额!");
            return;
        }
        decimal MinimumWagelimit = 0;
        decimal.TryParse(Addr_OrganizeCityParamBLL.GetValueByType(1, 23), out MinimumWagelimit);
        if (bll.Model.MinimumWageMode == 2&&MinimumWagelimit > 0 && MinimumWagelimit < bll.Model.MinimumWage)
        {
            MessageBox.Show(this, "当选择了特殊保底时，保底工资不能超过上限" + MinimumWagelimit .ToString()+ "元!");
            return;
        }
        if (bll.Model.MinimumWageMode != 0 && bll.Model.MinimumWageBeginDate.Year == 1900)
        {
            MessageBox.Show(this, "当选择了薪资保底时，请设定保底的起始日期!");
            return;
        }
        if (bll.Model.MinimumWageMode != 0 && bll.Model.MinimumWageEndDate.Year == 1900)
        {
            MessageBox.Show(this, "当选择了薪资保底时，请设定保底的截止日期!");
            return;
        }
        if (bll.Model.InsuranceMode == 0 && bll.Model.BasePayMode != 3)
        {
            MessageBox.Show(this, "非兼职，请选择正确的社保模式!");
            return;
        }
        if (bll.Model.InsuranceMode == 1 && bll.Model.InsuranceSubsidy <= 0)
        {
            MessageBox.Show(this, "请正确输入保险补贴金额!");
            return;
        }
        if (bll.Model.BasePayMode == 4 && bll.Model["FloatingTarget"] == "0")
        {
            MessageBox.Show(this, "当底薪模式为浮动底薪(非华南)时，请设定浮动底薪上限任务量!");
            return;
        }
        if (bll.Model.BasePayMode == 4 && bll.Model["AvgSales"] == "0")
        {
            MessageBox.Show(this, "当底薪模式为浮动底薪(非华南)时，请设定前两月平均销量!");
            return;
        }
        //if (bll.Model.BasePayMode == 4 && bll.Model["BaseFeeRate"] == "0")
        //{
        //    MessageBox.Show(this, "当底薪模式为浮动底薪(非华南)时，请设定底薪费率!");
        //    return;
        //}
        if (bll.Model.BasePayMode == 5 && bll.Model["SalesType"] == "0")
        {
            MessageBox.Show(this, "当底薪模式为浮动底薪(华南)时，请设定实销类别!");
            return;
        }
        if (bll.Model.BasePayMode == 5 && bll.Model["BaseFeeRate"] == "0")
        {
            MessageBox.Show(this, "当底薪模式为浮动底薪(华南)时，请设定底薪费率!");
            return;
        }
        #endregion
        bll.Model["ISArriveTarget"] = chk_arrivetarget.Checked ? "1" : "2";
        if ((int)ViewState["PromotorSalaryID"] == 0)
        {
            bll.Model.Promotor = (int)ViewState["PromotorID"];
            bll.Model.State = 1;
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = (int)Session["UserID"];

            ViewState["PromotorSalaryID"] = bll.Add();
        }
        else
        {
            bll.Update();
        }

        BindGrid();
        MessageBox.ShowAndRedirect(this, "保存成功！", "PM_PromotorSalaryDetail.aspx?ID=" + ViewState["PromotorSalaryID"].ToString());
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        PM_PromotorSalaryBLL bll = new PM_PromotorSalaryBLL((int)ViewState["PromotorSalaryID"]);
        if (bll.Model.State < 3) bll.Approve(3);

        Response.Redirect("PM_PromotorSalaryDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString());
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PromotorSalaryID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }
        PM_PromotorSalaryBLL bll = new PM_PromotorSalaryBLL((int)ViewState["PromotorSalaryID"]);
        PM_Promotor p = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;

        #region 判断是否KA店导购
        bool IsKAChannel = false;  //是否KA店导购

        if (p["Classify"] != "2")       //非流导
        {
            IList<PM_PromotorInRetailer> retailers = PM_PromotorInRetailerBLL.GetModelList(" Promotor=" + ViewState["PromotorID"].ToString());
            //判断导购是否在KA店工作
            foreach (PM_PromotorInRetailer item in retailers)
            {

                CM_Client client = new CM_ClientBLL(item.Client).Model;
                if (client["RTChannel"] == "1" || client["RTChannel"] == "2") IsKAChannel = true;
            }
        }
        #endregion

        #region 判断是否超薪酬超准
        bool bSalaryFlag = false;
        PM_PromotorSalary salary = bll.Model;

        if (salary.BasePaySubsidy >= 1) bSalaryFlag = true;         //有底薪补贴
        if (salary.MinimumWageMode == 2) bSalaryFlag = true;   //特殊保底
        if (salary.InsuranceMode == 1 && salary.InsuranceSubsidy > 100) bSalaryFlag = true;     //社保补贴大于100元的
        #endregion

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", p.ID.ToString());
        dataobjects.Add("PromotorSalaryID", ViewState["PromotorSalaryID"].ToString());
        dataobjects.Add("OrganizeCity", p.OrganizeCity.ToString());
        dataobjects.Add("SalaryFlag", bSalaryFlag ? "2" : "1");      //薪酬标志 1：薪酬标准内 2：薪酬标准外
        dataobjects.Add("StaffName", p.Name.ToString());
        dataobjects.Add("IsKAChannel", IsKAChannel ? "1" : "2");                 //是否KA卖场的导购

        int TaskID = EWF_TaskBLL.NewTask("Apply_PromotorSalary", (int)Session["UserID"], "调整导购员薪酬福利申请,姓名:" + p.Name,
            "~/SubModule/PM/PM_PromotorSalaryDetail.aspx?ID=" + bll.Model.ID.ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model.ApproveTask = TaskID;
            bll.Model.State = 2;
            bll.Update();
            // new EWF_TaskBLL(TaskID).Start();        //直接启动流程
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        #region 判断当前导购是否有所在工作的门店
        if (PM_PromotorInRetailerBLL.GetModelList("Promotor=" + ViewState["PromotorID"].ToString()).Count == 0)
        {
            MessageBox.ShowAndRedirect(this, "请设置该导购员所在的门店!", "PM_PromotorDetail.aspx");
            return;
        }
        #endregion
        ViewState["PromotorSalaryID"] = 0;
        BindData(null);
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            Response.Redirect("PM_PromotorDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString() +
                (Request.QueryString["ViewFramework"] == null ? "" : "&ViewFramework=" + Request.QueryString["ViewFramework"]));
        }
    }
    protected void gv_list_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_list.DataKeys[e.NewSelectedIndex][0];
        PM_PromotorSalary m = new PM_PromotorSalaryBLL(id).Model;
        BindData(m);
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        PM_PromotorSalaryBLL bll = new PM_PromotorSalaryBLL((int)ViewState["PromotorSalaryID"]);
        if (bll.Model.State == 1) bll.Delete();

        Response.Redirect("PM_PromotorSalaryDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString());
    }
}
