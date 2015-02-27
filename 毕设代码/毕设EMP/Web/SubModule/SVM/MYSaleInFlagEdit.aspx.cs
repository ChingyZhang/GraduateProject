using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using System.Data;
using MCSFramework.Model.SVM;

public partial class SubModule_SVM_MYSaleInFlagEdit : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["UserID"] = 1282;
            BindDropDown();
        }


    }
    private void BindDropDown()
    {
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate <= GETDATE() AND YEAR >= 2013");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1).ToString();

        #region 绑定用户可管辖的管理片区
        if (Session["UserID"] != null)
        {
            int staffid = (int)Session["UserID"];
            Org_StaffBLL staff = new Org_StaffBLL(staffid);

            //select_Staff.SelectText = staff.Model.RealName;
            //select_Staff.SelectValue = staffid.ToString();

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
            
            
        }
        #endregion
       
    }

    private void BindGrid()
    {
        int _month = int.Parse(ddl_Month.SelectedValue);
        int _staff = 0;
        if(select_Staff.SelectValue!="")
            _staff = int.Parse(select_Staff.SelectValue);
        #region 编辑查询条件
        int _city = 1;
        if (tr_OrganizeCity.SelectValue != "")
        {
            _city = int.Parse(tr_OrganizeCity.SelectValue);
        }
        string RTCondition = "", DICondition = "";
        //if (ddl_DI.SelectedValue == "0")
        //{
            if (select_DI.SelectValue != "0" && select_DI.SelectValue != "")
            {
                DICondition += " AND Supplier.ID =" + select_DI.SelectValue;
            }
        //}
        //else
        //{
        //    if (tbx_DICode.Text != "")
        //    {
        //        DICondition += (ddl_DI.SelectedValue == "1" ? " AND Supplier.FullName" : " AND Supplier.Code") + " LIKE '%" + tbx_DICode.Text.Trim() + "%'";
        //    }
        //}
        //if (ddl_RT.SelectedValue == "0")
        //{
            if (select_RT.SelectValue != "0" && select_RT.SelectValue != "")
            {
                RTCondition += " AND CM_Client.ID =" + select_RT.SelectValue;
            }
        //}
        //else
        //{
        //    if (tbx_RTCode.Text != "")
        //    {
        //        RTCondition += (ddl_RT.SelectedValue == "1" ? " AND CM_Client.FullName" : " AND CM_Client.Code") + " LIKE '%" + tbx_RTCode.Text.Trim() + "%'";
        //    }
        //}
        #endregion
        

        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        if (staff.Model.Position == 211 ||
                staff.Model.Position == 212 ||
                staff.Model.Position == 209 ||
                staff.Model.Position == 210)
        {
            if (_month == AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1)
            {
                string[] allowdays = Addr_OrganizeCityParamBLL.GetValueByType(1, 25).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
                if (allowdays.Contains(DateTime.Now.Day.ToString()))
                {
                    bt_Submit.Visible = true;
                    cbx_Head.Visible = true;
                }
            }
        }
        DataTable dt = SVM_MYSaleInFlagBLL.GetList(_month, _city, _staff, DICondition, RTCondition);
        lt_RTCount.Text = dt.Rows.Count.ToString();
        gv_ListDetail.DataSource = dt;
        gv_ListDetail.BindGrid();
        

    }
    //protected void ddl_DI_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    select_DI.Visible = ddl_DI.SelectedIndex == 0;
    //    tbx_DICode.Visible = ddl_DI.SelectedIndex != 0;
    //}
    //protected void ddl_RT_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    select_RT.Visible = ddl_RT.SelectedIndex == 0;
    //    tbx_RTCode.Visible = ddl_RT.SelectedIndex != 0;
    //}
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        int _month = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1;
        foreach (GridViewRow row in gv_ListDetail.Rows)
        {
            int hasid = int.Parse(gv_ListDetail.DataKeys[row.RowIndex][0].ToString());
            
            bool usefullnow = (row.FindControl("cbx_id") as CheckBox).Checked;
            if (usefullnow)
            {
                if (hasid == 0)
                {
                    SVM_MYSaleInFlagBLL _item = new SVM_MYSaleInFlagBLL();
                    _item.Model.ClientID = int.Parse(gv_ListDetail.DataKeys[row.RowIndex][1].ToString());
                    _item.Model.AccountMonth = _month;
                    _item.Model.ApproveFlag = 2;
                    _item.Add();
                }

            }
            else
            {
                if (hasid != 0)
                {
                    SVM_MYSaleInFlagBLL _item = new SVM_MYSaleInFlagBLL(hasid);
                    _item.Delete(hasid);
                }
            
            }
        }
        MessageBox.ShowAndRedirect(this, "提交成功！", "MYSaleInFlagEdit.aspx");
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void gv_ListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
