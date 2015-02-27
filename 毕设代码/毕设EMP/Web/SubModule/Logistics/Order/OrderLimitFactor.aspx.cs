using System;
using System.Data;
using System.Web.UI.WebControls;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Logistics;
using MCSFramework.Common;
using System.Collections.Generic;
public partial class SubModule_Logistics_Order_ORD_SalesFactor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropdown();
            BindGrid();
        }
    }

    private void BindDropdown()
    {
        #region 绑定用户可管辖的片区
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
     
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth()).ToString();

        IList<PDT_Brand> _brandList = PDT_BrandBLL.GetModelList("IsOpponent=1");
        ddl_Brand.DataTextField = "Name";
        ddl_Brand.DataValueField = "ID";
        ddl_Brand.DataSource = _brandList;
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.Items.Insert(0, new ListItem("所有", "0"));
    }

    private void BindGrid()
    {
        string ConditionString = " ORD_OrderLimitFactor.AccountMonth=" + ddl_AccountMonth.SelectedValue;
        if (select_client.SelectValue != "")
        {
            ConditionString += " AND ORD_OrderLimitFactor.Client=" + select_client.SelectValue;
        }
        if (ddl_Classify.SelectedValue != "0")
        {
            ConditionString += " AND MCS_Pub.dbo.PDT_Product.Classify="+ddl_Classify.SelectedValue;
        }
        if (ddl_Brand.SelectedValue != "0")
        {
            ConditionString += " AND MCS_Pub.dbo.PDT_Product.Brand=" + ddl_Brand.SelectedValue;
        }
        if (select_Product.SelectValue != "")
        {
            ConditionString += " AND ORD_OrderLimitFactor.Product=" + select_Product.SelectValue;
        }
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            string orgcitys = "";
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            if (orgcitys != "") ConditionString += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }
        gv_List.ConditionString = ConditionString;
        gv_List.BindGrid();

    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        #region 修改明细
        foreach (GridViewRow row in gv_List.Rows)
        {
            ORD_OrderLimitFactorBLL _bll = new ORD_OrderLimitFactorBLL(int.Parse(gv_List.DataKeys[row.RowIndex]["ORD_OrderLimitFactor_ID"].ToString()));
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            //_bll.Model.Factor = decimal.Parse(((TextBox)row.FindControl("tbx_Factor")).Text);
            _bll.Model.TheoryQuantity = int.Parse(((TextBox)row.FindControl("tbx_Quantity")).Text);
            _bll.Model.UpperLimit = int.Parse(((TextBox)row.FindControl("tbx_UpperLimit")).Text);
            _bll.Model.LowerLimit = int.Parse(((TextBox)row.FindControl("tbx_LowerLimit")).Text);
            _bll.Update();
        }

        #endregion
        BindGrid();
        MessageBox.Show(this, "保存成功!");
    }
    protected void ddl_AccountMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddl_AccountMonth.SelectedValue) < AC_AccountMonthBLL.GetCurrentMonth())
        {
            BtnAdd.Enabled = false;
            BtnDelete.Enabled = false;
            BtnSave.Enabled = false;
            btn_Approve.Enabled = false;
            btn_CancleApprove.Enabled = false;
        }
        else
        {
            BtnAdd.Enabled = true;
            BtnDelete.Enabled = true;
            BtnSave.Enabled = true;
            btn_Approve.Enabled = true;
            btn_CancleApprove.Enabled = true;
        }

    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (e.CurSelectIndex != 0)
        {
            select_client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"&OrganizeCity=" + tr_OrganizeCity.SelectValue;
            select_client.SelectText = "";
            select_client.SelectValue = "";
        }
    }


    protected void select_client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (select_client.SelectValue!="")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(e.SelectValue)).Model.OrganizeCity.ToString();
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            if (chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["ORD_OrderLimitFactor_ID"];
                new ORD_OrderLimitFactorBLL().Delete(id);
            }
        }
        BindGrid();
    }
    protected void CBAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cbx");
            cb_check.Checked = CBAll.Checked;
        }
        CBAll.Checked = false;
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (ORD_OrderLimitFactorBLL.GetModelList("AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND  Client=" + select_client.SelectValue + " AND Product=" + select_Product.SelectValue).Count > 0)
        {
            MessageBox.Show(this, "对不起,所选择的会计月,经销商限制已包含了相应产品");
        }
        ORD_OrderLimitFactorBLL _bll = new ORD_OrderLimitFactorBLL();
        _bll.Model.AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
        _bll.Model.Product = int.Parse(select_Product.SelectValue);
        _bll.Model.ApproveFlag=2;
        _bll.Model.Client=int.Parse(select_client.SelectValue);
        _bll.Model.Factor=1.1m;   
        _bll.Model.InsertStaff=(int)Session["UserID"];
        _bll.Model.InsertTime=DateTime.Now;
        _bll.Add();
        BindGrid();
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        Approve(1);
        BindGrid();
    }
    protected void btn_CancleApprove_Click(object sender, EventArgs e)
    {
        Approve(2);
        BindGrid();
    }

    private void Approve(int approveFlag)
    {   
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            if (chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["ORD_OrderLimitFactor_ID"];
                ORD_OrderLimitFactorBLL _bll = new ORD_OrderLimitFactorBLL(id);
                _bll.Model.ApproveFlag = approveFlag;
                _bll.Update();
            }
        }       
        BindGrid();
    }
    
    public void CheckChange(object sender, EventArgs e)
    {
        TextBox t = (TextBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        int preInve = Convert.ToInt32(((Label)drv.FindControl("lbl_PreInve")).Text);
        int quantity = Convert.ToInt32(((TextBox)drv.FindControl("tbx_Quantity")).Text);
        int upperLimit = Convert.ToInt32(((TextBox)drv.FindControl("tbx_UpperLimit")).Text);
        int lowerLimit = Convert.ToInt32(((TextBox)drv.FindControl("tbx_LowerLimit")).Text);
        if (preInve + quantity > upperLimit || preInve + quantity < lowerLimit)
        {
            ((TextBox)drv.FindControl("tbx_Quantity")).Text = (lowerLimit - preInve).ToString();
            MessageBox.Show(this, "理论订货量应该在理论存货量上下限之间！");
        }

    }
    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {   
        #region 绑定品系
        ddl_Classify.DataSource = PDT_ClassifyBLL.GetModelList("Brand="+ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("所有", "0"));
        #endregion
    }
}
