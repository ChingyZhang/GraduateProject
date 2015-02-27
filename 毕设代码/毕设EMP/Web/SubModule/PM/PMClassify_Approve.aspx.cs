using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;
using MCSFramework.Common;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Pub;
using System.Collections.Generic;

public partial class SubModule_PM_PMClassify_Approve : System.Web.UI.Page
{
    private DropDownList ddl_BasePayMode;               //底薪模式
    private DropDownList ddl_SeniorityPayMode;         //工龄工资模式
    private DropDownList ddl_BasePaySubsidyMode;   //底薪补贴类别
    private DropDownList ddl_MinimumWageMode;     //保底工资类型
    private DropDownList ddl_InsuranceMode;            //社保模式
    private DropDownList ddl_SalesType;//销售类别
    private DropDownList ddl_BankType;//银行类别
    private CheckBox chk_arrivetarget = new CheckBox();
    private TextBox txt_AvgSales;//首两月均销量
    private TextBox txt_BaseFeeRate;//底薪费率 
    private TextBox txt_FloatingTarget;
    private TextBox txt_BankName;


    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_BasePayMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_BasePayMode");
        ddl_BasePaySubsidyMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_BasePaySubsidyMode");
        ddl_MinimumWageMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_MinimumWageMode");
        ddl_InsuranceMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_InsuranceMode");
        ddl_SeniorityPayMode = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_SeniorityPayMode");
        ddl_SalesType = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_SalesType");
    
        ddl_BankType = DV_pm.FindControl("PM_Promotor_BankType") == null ? null : (DropDownList)UC_DetailView1.FindControl("PM_Promotor_BankType");
        txt_BankName = DV_pm.FindControl("PM_Promotor_BankName") == null ? null : (TextBox)UC_DetailView1.FindControl("PM_Promotor_BankName");
       

        if (ddl_BankType != null)
        {
            ddl_BankType.AutoPostBack = true;
            ddl_BankType.SelectedIndexChanged += new EventHandler(ddl_BankType_SelectedIndexChanged);
        }

        ddl_BasePaySubsidyMode.AutoPostBack = true;
        ddl_BasePaySubsidyMode.SelectedIndexChanged += new EventHandler(ddl_BasePaySubsidyMode_SelectedIndexChanged);

        ddl_MinimumWageMode.AutoPostBack = true;
        ddl_MinimumWageMode.SelectedIndexChanged += new EventHandler(ddl_MinimumWageMode_SelectedIndexChanged);

        ddl_InsuranceMode.AutoPostBack = true;
        ddl_InsuranceMode.SelectedIndexChanged += new EventHandler(ddl_InsuranceMode_SelectedIndexChanged);
 

        txt_AvgSales = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_AvgSales");
        txt_BaseFeeRate = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_BaseFeeRate");
        txt_FloatingTarget = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_FloatingTarget");

      

        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));
        if (!IsPostBack)
        {
            txt_AvgSales.Enabled = false;
            txt_BaseFeeRate.Enabled = false;
            txt_FloatingTarget.Enabled = false;
            ddl_SalesType.Enabled = false;
            ddl_BasePaySubsidyMode.Enabled = false;
            ddl_MinimumWageMode.Enabled = false;

            txt_FloatingTarget.Text = "0";
            txt_AvgSales.Text = "0";
            txt_BaseFeeRate.Text = "0";

            ViewState["PromotorID"] = Request.QueryString["PromotorID"] == null ? 0 : int.Parse(Request.QueryString["PromotorID"]);

            //新增薪资标准
            #region 判断当前导购是否有所在工作的门店
            if (PM_PromotorInRetailerBLL.GetModelList("Promotor=" + ViewState["PromotorID"].ToString()).Count == 0)
            {
                MessageBox.ShowAndRedirect(this, "请设置该导购员所在的门店!", "PM_PromotorDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString());
                return;
            }
            #endregion

            PM_Promotor m = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
            if (m != null)
                DV_pm.BindData(m);
            else
            {
                MessageBox.ShowAndClose(this, "导购员信息读取失败！");
                return;
            }



            DropDownList ddl_Classfiy = DV_pm.FindControl("PM_Promotor_Classfiy") as DropDownList;
            TextBox tbx_BeginWorkDate = DV_pm.FindControl("PM_Promotor_BeginWorkDate") as TextBox;
            int classfiy = 0;
            int.TryParse(m["Classfiy"], out classfiy);
            if (classfiy == 0)
            {
                MessageBox.ShowAndClose(this, "导购员类别读取失败！");
                return;
            }
            if (m["State"] == "2")
            {
                MessageBox.ShowAndClose(this, "导购有流程正在审批，无法再发起流程！");
                return;
            }
            else if (classfiy == 1 || classfiy == 2)
            {
                ddl_Classfiy.Items.FindByValue("1").Enabled = false;
                ddl_Classfiy.Items.FindByValue("2").Enabled = false;
            }
            else
                ddl_Classfiy.Items.FindByValue("6").Enabled = false;
            tbx_BeginWorkDate.Text = "";
            BindData(classfiy);
        }
    }

    private void BindData(int classfiy)
    {

        PM_Promotor p = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
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

        PM_PromotorSalary m = new PM_PromotorSalary();
        m.Promotor = (int)ViewState["PromotorID"];
        m.State = 1;
        m.InsertStaff = (int)Session["UserID"];
        m.RTManageCost = (decimal)ViewState["RTManageCost"];
        ddl_BasePayMode.Enabled = false;
        if (classfiy == 1 || classfiy == 2)
        {
            m.BasePayMode = 3;          
        }
        else
        {          
           
            m.SeniorityPayMode = 1;
            ddl_BankType.Items[3].Enabled = false;

            #region 抓取固定底薪标准
            decimal basepay = (decimal)ViewState["BasePay"];
            m.BasePay = basepay;
            #endregion
            Addr_OrganizeCityBLL _bll = new Addr_OrganizeCityBLL(p.OrganizeCity);
            if (_bll.IsChildOrganizeCity(7))//判断是否为华南区
            {
                ddl_BasePayMode.Items.Remove(new ListItem("浮动底薪(非华南)", "4"));
                ddl_BasePayMode.Items.Remove(new ListItem("兼职底薪", "3"));
                ddl_BasePayMode.Enabled = true;
                ddl_SalesType.Enabled = true;
                txt_BaseFeeRate.Enabled = true;

            }
            else
            {
                m.BasePayMode = 1;
            }

        }        
        UC_DetailView1.BindData(m);

        if (m.RTManageCost > 0 && (decimal)ViewState["RTManageCost"] == m.RTManageCost)
        {
            TextBox tbx = (TextBox)UC_DetailView1.FindControl("PM_PromotorSalary_RTManageCost");
            if (tbx != null) tbx.Enabled = false;
        }
        if (m.State < 3)
        {
            #region 兼职导购的限定
            try
            {
                if (classfiy != 1 && classfiy != 2)   //非专职、非流导，认为是兼职导购
                {
                    DateTime Birthday;
                    if (DateTime.TryParse(p["Birthday"], out Birthday) && Birthday.AddYears(49) < DateTime.Now)
                    {
                        foreach (ListItem item in ddl_InsuranceMode.Items)
                        {
                            if (item.Value != "5" && item.Value != "8" && item.Value != "0")
                            {
                                item.Enabled = false;
                            }
                        }
                    }
                    SetControlsEnable(true);
                }
                else
                {
                    SetControlsEnable(false);

                }

            }
            catch { }
            #endregion


            ddl_BasePaySubsidyMode_SelectedIndexChanged(null, null);
            ddl_InsuranceMode_SelectedIndexChanged(null, null);
            ddl_MinimumWageMode_SelectedIndexChanged(null, null);
            ddl_BankType_SelectedIndexChanged(null, null);
        }
    }
    private void SetControlsEnable(bool EnableFlag)
    {
        if (ddl_BasePaySubsidyMode != null)
        {
            //兼职无法申请底薪补贴
            if (!EnableFlag) ddl_BasePaySubsidyMode.SelectedValue = "0";
           // ddl_BasePaySubsidyMode.Enabled = EnableFlag;
        }

        if (ddl_MinimumWageMode != null)
        {
            //兼职无法申请最低保底
            if (!EnableFlag) ddl_MinimumWageMode.SelectedValue = "0";
           // ddl_MinimumWageMode.Enabled = EnableFlag;
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        PM_PromotorBLL _promotor = new PM_PromotorBLL((int)ViewState["PromotorID"]);
        if (_promotor.Model["State"] == "2")
        {
            MessageBox.Show(this, "该导购有未完成的流程,请待流程完成后，再做调整！");
            return;
        }
        DropDownList ddl_Classfiy = DV_pm.FindControl("PM_Promotor_Classfiy") as DropDownList;
        TextBox tbx_BeginWorkDate = DV_pm.FindControl("PM_Promotor_BeginWorkDate") as TextBox;
        TextBox tbx_Remark = DV_pm.FindControl("PM_Promotor_Education") as TextBox;
        TextBox tbx_AccountNO = (TextBox)DV_pm.FindControl("PM_Promotor_AccountCode");
        if (ddl_BankType.SelectedValue == "0" || txt_BankName.Text.Trim() == "")
        {
           MessageBox.Show(this,"请选择开户行！");
           return;
        }

        if (ddl_Classfiy.SelectedValue != "0" && tbx_BeginWorkDate.Text.Trim() != "")
        {
            
            int budget = PM_PromotorNumberLimitBLL.CheckOverBudget(_promotor.Model.OrganizeCity, int.Parse(_promotor.Model["Classfiy"]));
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["PromotorID"].ToString());
            dataobjects.Add("OrganizeCity", _promotor.Model.OrganizeCity.ToString());
            dataobjects.Add("BeginWorkDate", tbx_BeginWorkDate.Text.ToString());
            dataobjects.Add("Classify", ddl_Classfiy.SelectedValue);
            dataobjects.Add("Remark", tbx_Remark.Text);
            dataobjects.Add("PM_Name", _promotor.Model.Name.ToString());
            dataobjects.Add("BankType", ddl_BankType.SelectedValue);
            dataobjects.Add("BankName", txt_BankName.Text.Trim());
            dataobjects.Add("AccountNO", tbx_AccountNO.Text.Trim());
            PM_PromotorSalaryBLL bll = new PM_PromotorSalaryBLL();
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

            if (bll.Model.MinimumWageMode == 2 && bll.Model.MinimumWage == 0)
            {
                MessageBox.Show(this, "当选择了特殊保底时，请设定保底金额!");
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
            int TaskID = EWF_TaskBLL.NewTask("PMClassify_Change", (int)Session["UserID"], "导购员类型变更流程,姓名:" + _promotor.Model.Name, "~/SubModule/PM/PM_PromotorDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString(), dataobjects);
            if (TaskID > 0)
            {               
                bll.Model.Promotor = (int)ViewState["PromotorID"];
                bll.Model.State = 2;
                bll.Model.ApproveTask = TaskID;
                bll.Model.ApproveFlag = 2;
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Add();
                _promotor.Submit(TaskID, (int)Session["UserID"]);
                new EWF_TaskBLL(TaskID).Start();
                MessageBox.ShowAndClose(this, "流程发起成功！");
            }
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

    void ddl_InsuranceMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_InsuranceSubsidy = (DropDownList)UC_DetailView1.FindControl("PM_PromotorSalary_InsuranceSubsidy");
        if (ddl_InsuranceSubsidy == null) return;

        if (ddl_InsuranceMode.SelectedValue == "1")
        {
            //自购社保，给予补贴
            ddl_InsuranceSubsidy.Enabled = true;
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

    protected void ddl_BankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_BankName != null)
        {
            txt_BankName.Enabled = int.Parse(ddl_BankType.SelectedValue) > 2;
            txt_BankName.Text = int.Parse(ddl_BankType.SelectedValue) > 2 ? "" : ddl_BankType.SelectedItem.Text;
        }
    }

}
