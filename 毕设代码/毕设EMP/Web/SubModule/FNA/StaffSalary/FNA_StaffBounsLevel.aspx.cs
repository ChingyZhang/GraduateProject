using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;

public partial class SubModule_FNA_StaffSalary_FNA_StaffBounsLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }
    }

    private void BindDropDown()
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
        
        ddl_Quarter.DataSource = AC_AccountQuarterBLL.GetModelList("");
        ddl_Quarter.DataBind();
        ddl_level.DataSource = DictionaryBLL.GetDicCollections("FNA_BounsLevel");
        ddl_level.DataBind();
        ddl_level.Items.Insert(0, new ListItem("请选择", "0"));
        

    }
    private void BindGrid()
    {
        ddl_level.SelectedValue = "0";
        txt_sales1.Text = "0";
        txt_sales2.Text = "0";
        txt_BounsBase.Text = "0";

        string condition = " 1=1 ";

        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND FNA_StaffBounsLevel.OrganizeCity IN (" + orgcitys + ")";
        }
        if (ddl_Quarter.SelectedValue != "0")
        {
            condition += "AND FNA_StaffBounsLevel.Quarter=" + ddl_Quarter.SelectedValue.Trim();
        }
        #endregion
        
        gvList.ConditionString = condition;
        gvList.BindGrid();
    }

    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gvList.DataKeys[e.RowIndex][0].ToString());
        new FNA_StaffBounsLevelBLL(id).Delete();
        MessageBox.Show(this, "删除成功！");
        BindGrid();
    }
    protected void gvList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = int.Parse(gvList.DataKeys[e.NewSelectedIndex][0].ToString());
        FNA_StaffBounsLevelBLL bll = new FNA_StaffBounsLevelBLL(id);
        ddl_level.SelectedValue = bll.Model.Level.ToString();
        txt_sales1.Text =( bll.Model.SalesVolume1/10000).ToString();
        txt_sales2.Text =( bll.Model.SalesVolume2/10000).ToString();
        txt_BounsBase.Text = bll.Model.Bouns.ToString();
        tr_OrganizeCity.SelectValue = bll.Model.OrganizeCity.ToString();
        ViewState["ID"] = id;
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddl_Quarter.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择季度！");
            return;
        }

        FNA_StaffBounsLevelBLL bll = new FNA_StaffBounsLevelBLL();
        if (ViewState["ID"] != null)
        {
            bll = new FNA_StaffBounsLevelBLL((int)ViewState["ID"]);
        }
        bll.Model.Quarter = int.Parse(ddl_Quarter.SelectedValue);
        AC_AccountQuarterBLL quarter = new AC_AccountQuarterBLL(bll.Model.Quarter);
        bll.Model.BegainMonth = quarter.Model.BeginMonth;
        bll.Model.EndMonth = quarter.Model.EndMonth;
        bll.Model.Level = int.Parse(ddl_level.SelectedValue);
        bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
        bll.Model.SalesVolume1 = decimal.Parse(txt_sales1.Text.Trim())*10000;
        bll.Model.SalesVolume2 = decimal.Parse(txt_sales2.Text.Trim())*10000;
        bll.Model.Bouns = decimal.Parse(txt_BounsBase.Text.Trim());
        if (ViewState["ID"] != null)
        {
            bll.Update();
        }
        else
        {
            bll.Add(); 
        }
        ViewState["ID"] = null;
        BindGrid();
    }
    protected void ddl_Quarter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}
