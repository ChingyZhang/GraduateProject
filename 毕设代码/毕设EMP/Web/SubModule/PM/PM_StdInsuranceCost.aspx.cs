using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Promotor;
using System.Text;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.Model.Promotor;

public partial class SubModule_PM_PM_StdInsuranceCost : System.Web.UI.Page
{
    int pid = 0;
    PM_StdInsuranceCostBLL insuranceCost;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindGrid();
            dvBasePay.Visible = false;
            btnSave.Visible = false;
        }
        string script = "function PopInCity(insure){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("PM_StdInsuranceCostInCity.aspx") +
            "?Insurance='+ insure + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopInCity", script, true);
    }
    private void BindDropDown()
    {

        #region 绑定保险模式
        ddl_InsuranceMode.DataSource = DictionaryBLL.Dictionary_Data_GetAlllList(" TableName = 'PM_InsuranceClassify'");
        ddl_InsuranceMode.DataTextField = "Name";
        ddl_InsuranceMode.DataValueField = "Code";
        ddl_InsuranceMode.DataBind();
        #endregion
    }
    private void BindGrid()
    {
        string condition = String.Format(
            ddl_InsuranceMode.SelectedValue + " = case  "
             + ddl_InsuranceMode.SelectedValue
             + " when 0 then 0 else InsuranceMode end");
        gvList.ConditionString = condition;
        gvList.BindGrid();
        btnSave.Visible = false;
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        gvList.SelectedIndex = -1;
        dvBasePay.Visible = false;
        btn_Add.Visible = true;
        BindGrid();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        dvBasePay.BindData(new PM_StdInsuranceCost());
        btn_Add.Visible = false;
        dvBasePay.Visible = true;
        btnSave.Visible = true;
        gvList.SelectedIndex = -1;
        btnDelete.Text = "取 消";
    }
    private void BindData(int pid)
    {
        insuranceCost = new PM_StdInsuranceCostBLL(pid);
        dvBasePay.BindData(insuranceCost.Model);
    }
    protected void gvList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        pid = (int)gvList.DataKeys[e.NewSelectedIndex][0];
        BindData(pid);
        dvBasePay.Visible = true;
        btnSave.Visible = true;
        btn_Add.Visible = false;
        btnDelete.Text = "删 除";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        insuranceCost = new PM_StdInsuranceCostBLL();
        dvBasePay.GetData(insuranceCost.Model);
        if (gvList.SelectedIndex != -1)
        {
            insuranceCost.Model.ID = (int)gvList.SelectedDataKey[0];
            insuranceCost.Update();
        }
        else
            BindData(insuranceCost.Add());
        dvBasePay.Visible = false;
        btn_Add.Visible = true;
        gvList.SelectedIndex = -1;
        btnDelete.Text = "删 除";
        BindGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        insuranceCost = new PM_StdInsuranceCostBLL();
        if (gvList.SelectedIndex != -1)
        {
            insuranceCost.Delete((int)gvList.SelectedDataKey[0]);
        }
        btnDelete.Text = "删 除";
        BindGrid();
        gvList.SelectedIndex = -1;
        dvBasePay.Visible = false;
        btn_Add.Visible = true;
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //PM_StdInsuranceCost_ID
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb_InCity = e.Row.FindControl("lb_InCity") as LinkButton;
            int insurance = (int)gvList.DataKeys[e.Row.RowIndex]["PM_StdInsuranceCost_ID"];
            IList<PM_StdInsuranceCostInCity> costs = PM_StdInsuranceCostInCityBLL.GetModelList("Insurance=" + insurance.ToString());
            if (costs.Count > 0)
            {
                string citys = "";
                foreach (PM_StdInsuranceCostInCity c in costs)
                {
                    if (citys != "")
                        citys = ",";
                    citys += new Addr_OrganizeCityBLL(c.City).Model.Name;
                }
                lb_InCity.Text = citys;
            }
            else
                lb_InCity.Text = "(默认)";
            lb_InCity.OnClientClick = "PopInCity(" + insurance + ")";
        }
    }
}
