using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model;
using System.Data;
using MCSFramework.Model.Promotor;
using MCSFramework.Common;
using MCSControls.MCSWebControls;

public partial class SubModule_PM_PM_PromotorSalaryBasePay : System.Web.UI.Page
{
    int pid = 0;
    PM_StdBasePayBLL basepay;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindGrid();
            dvBasePay.Visible = false;
        }
    }
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        
        #region 绑定行政城市
        //tr_OfficialCity.DataSource = Addr_OfficialCityBLL.GetAllOfficialCity();

         int city = staff.Model.OfficialCity == 0 ? 1 : staff.Model.OfficialCity;
         tr_OfficialCity.RootValue = "0";
         tr_OfficialCity.SelectValue = city.ToString();

        #endregion

        #region 绑定市场类别 
         ddlMarketType.DataSource = DictionaryBLL.Dictionary_Data_GetAlllList(" TableName = 'CM_MarketType'");
        ddlMarketType.DataTextField = "Name";
        ddlMarketType.DataValueField = "Code";
        ddlMarketType.DataBind();
        #endregion
    }
    private void BindGrid()
    {
        string condition = String.Format("(MCS_SYS.dbo.UF_IsChildOfficialCity("
             +tr_OfficialCity.SelectValue
             +",City)=0 OR City="
             +tr_OfficialCity.SelectValue
             + ") and "
             + ddlMarketType.SelectedValue + " = case  "
             + ddlMarketType.SelectedValue
             + " when 0 then 0 else Classify end");
        gvList.ConditionString = condition;
        gvList.BindGrid();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        gvList.SelectedIndex = -1;
        dvBasePay.Visible = false;
        btn_Add.Enabled = true;
        BindGrid();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        dvBasePay.BindData(new PM_StdBasePay());
        MCSTreeControl officialCity = (MCSTreeControl)dvBasePay.FindControl("PM_StdBasePay_City");
        officialCity.RootValue = "0";
        officialCity.SelectValue = tr_OfficialCity.SelectValue;
        btn_Add.Enabled = false;
        dvBasePay.Visible = true;
        gvList.SelectedIndex = -1;
        btnDelete.Text = "取 消";
    }
    protected void tr_OfficialCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
    }
    private void BindData(int pid)
    {
        basepay = new PM_StdBasePayBLL(pid);
        dvBasePay.BindData(basepay.Model);
    }
    protected void gvList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        pid = (int)gvList.DataKeys[e.NewSelectedIndex][0];
        BindData(pid);
        dvBasePay.Visible = true;
        btn_Add.Enabled = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        basepay = new PM_StdBasePayBLL();
        dvBasePay.GetData(basepay.Model);
        if (gvList.SelectedIndex!=-1)
        {
            basepay.Model.ID = (int)gvList.SelectedDataKey[0];
            basepay.Update();
        }
        else
            BindData(basepay.Add());
        dvBasePay.Visible = false;
        btn_Add.Enabled = true;
        gvList.SelectedIndex = -1;
        btnDelete.Text = "删 除";
        BindGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        basepay = new PM_StdBasePayBLL();
        if (gvList.SelectedIndex != -1)
        {
            basepay.Delete((int)gvList.SelectedDataKey[0]);
        }
        btnDelete.Text = "删 除";
        BindGrid();
        gvList.SelectedIndex = -1;
        dvBasePay.Visible = false;
        btn_Add.Enabled = true;
    }
}
