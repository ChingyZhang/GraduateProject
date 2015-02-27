using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_FNA_FeeWriteoff_FeeWriteoffDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]); //费用类型

            BindDropDown();

            if ((int)ViewState["FeeType"] != 0)
            {
                rbl_FeeType.SelectedValue = ViewState["FeeType"].ToString();
                rbl_FeeType.Enabled = false;
            }

        }
    }

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

        rbl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name).ToList();
        rbl_FeeType.DataBind();

        foreach (ListItem item in rbl_FeeType.Items)
        {
            if (AC_AccountTitleBLL.GetListByFeeType(int.Parse(item.Value)).Where(p => p.ID > 1).ToList().Count == 0)
            {
                item.Enabled = false;
            }
        }
        rbl_FeeType.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_InvoiceClassAB.DataSource = DictionaryBLL.GetDicCollections("FNA_InvoiceClassAB").OrderBy(p => p.Value.Name).ToList();
        ddl_InvoiceClassAB.DataBind();
        ddl_InvoiceClassAB.Items.Insert(0, new ListItem("请选择", "0"));

        //select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,3)\"";
        //select_Staff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }

    protected void rbl_HasFeeApply_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_FeeType.Visible = rbl_HasFeeApply.SelectedValue == "N";
    }

    protected void bt_Confirm_Click(object sender, ImageClickEventArgs e)
    {
        if (select_Staff.SelectValue == "" && ddl_InvoiceClassAB.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择要报销的抵货款类型!");
            return;
        }
        if (rbl_HasFeeApply.SelectedValue == "N")
        {
            if (rbl_FeeType.SelectedValue == "0")
            {
                MessageBox.Show(this, "请选择要报销的费用类型!");
                return;
            }
            Response.Redirect("FeeWriteoffDetail.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue +
                       "&InsteadPayClient=" + (select_Client.SelectValue == "" ? "0" : select_Client.SelectValue) +
                       "&InsteadPayStaff=" + (select_Staff.SelectValue == "" ? "0" : select_Staff.SelectValue) +
                       "&FeeType=" + rbl_FeeType.SelectedValue + "&InvoiceClassAB=" + ddl_InvoiceClassAB.SelectedValue +
                       "&FeeApplyClient=" + (select_ApplyClient.SelectValue == "" ? "0" : select_ApplyClient.SelectValue) +
                       "&FeeApplyStaff=" + (select_applyStaff.SelectValue == "" ? "0" : select_applyStaff.SelectValue) +
                       "&HasFeeApply=N");
        }
        else
        {
            Response.Redirect("FeeWriteoffDetail.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&InvoiceClassAB=" + ddl_InvoiceClassAB.SelectedValue +
                       "&InsteadPayClient=" + (select_Client.SelectValue == "" ? "0" : select_Client.SelectValue) +
                       "&InsteadPayStaff=" + (select_Staff.SelectValue == "" ? "0" : select_Staff.SelectValue) +
                       "&FeeApplyClient=" + (select_ApplyClient.SelectValue == "" ? "0" : select_ApplyClient.SelectValue) +
                       "&FeeApplyStaff=" + (select_applyStaff.SelectValue == "" ? "0" : select_applyStaff.SelectValue) +
                       "&HasFeeApply=Y");
        }
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        lb_MasterAccountName.Text = "";

        int id = 0;
        if (int.TryParse(select_Client.SelectValue, out id) && id > 0)
        {
            lb_MasterAccountName.Text = GetRleatClient(id,true);
            select_Staff.Enabled = false;
            select_Staff.ToolTip = "费用代垫对像不能同时为经销商与员工";
        }
    }
    protected void select_ApplyClient_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        lbl_MasterApplyClient.Text = "";

        int id = 0;
        if (int.TryParse(select_ApplyClient.SelectValue, out id) && id > 0)
        {
            lbl_MasterApplyClient.Text = GetRleatClient(id,false);
            select_applyStaff.Enabled = false;
            select_applyStaff.ToolTip = "费用申请对像不能同时为经销商与员工";
        }

    }

    private string GetRleatClient(int id,bool ischangecity)
    {

        string result = "";


        CM_Client client = new CM_ClientBLL(id).Model;
        if (ischangecity)
        {
            tr_OrganizeCity.SelectValue = client.OrganizeCity.ToString();
        }
        if (client != null && client.ClientType == 2 && client["DIClassify"] == "3")
        {
            CM_Client supplier = new CM_ClientBLL(client.Supplier).Model;
            if (supplier != null && supplier.ClientType == 2 && supplier["DIClassify"] == "1")
                result = "关联主户头：" + supplier.FullName;
        }
        if (client != null && client.ClientType == 2 && client["DIClassify"] == "1")
        {
            IList<CM_Client> _listsubclient = CM_ClientBLL.GetModelList(@"Supplier=" + client.ID.ToString() + " AND ClientType=2 AND ActiveFlag=1 AND ApproveFlag=1 AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)='3'");
            result = _listsubclient.Count > 0 ? "关联子户头：" : "";

            foreach (CM_Client m in _listsubclient)
            {
                result += m.FullName + "<br/> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp";

            }
        }

        return result;

    }
    protected void select_applyStaff_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (select_applyStaff.SelectValue != "")
        {
            select_Client.Enabled = false;
            select_Client.ToolTip = "费用申请为员工时费用不能由经销商代垫";
            select_ApplyClient.Enabled = false;
            select_ApplyClient.ToolTip = "费用申请对像不能同时为经销商与员工";
        }
    }
    protected void select_Staff_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {

        if (select_Staff.SelectValue != "")
        {
            ddl_InvoiceClassAB.Enabled = false;
            select_Client.Enabled = false;
            select_Client.ToolTip = "费用代垫对像不能同时为经销商与员工";
        }

    }
}
