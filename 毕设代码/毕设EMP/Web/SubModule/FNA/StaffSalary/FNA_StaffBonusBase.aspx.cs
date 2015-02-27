using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;

public partial class SubModule_FNA_StaffSalary_FNA_StaffBonusBase : System.Web.UI.Page
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
        ddl_PositionType.DataSource = DictionaryBLL.GetDicCollections("PUB_PositionType");
        ddl_PositionType.DataBind();
        ddl_PositionType.Items.Insert(0, new ListItem("请选择", "0"));
    }

    private void BindGrid()
    {
        ddl_PositionType.SelectedValue = "0";
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

            condition += " AND FNA_StaffBonusBase.OrganizeCity IN (" + orgcitys + ")";
        }
        if (ddl_PositionType.SelectedIndex != 0)
        {
            condition += "AND FNA_StaffBonusBase.PositionType=" + ddl_PositionType.Text.Trim();
        }
        #endregion

        gvList.ConditionString = condition;
        gvList.BindGrid();
    }


    protected void gvList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = int.Parse(gvList.DataKeys[e.NewSelectedIndex]["FNA_StaffBonusBase_ID"].ToString());
        FNA_StaffBonusBaseBLL bll = new FNA_StaffBonusBaseBLL(id);
        ddl_PositionType.SelectedItem.Text = bll.Model.PositionType;
        txt_BounsBase.Text = bll.Model.BounsBase.ToString();
        tr_OrganizeCity.SelectValue = bll.Model.OrganizeCity.ToString();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        FNA_StaffBonusBaseBLL bll = new FNA_StaffBonusBaseBLL();
        if (ViewState["ID"] != null)
        {
            bll = new FNA_StaffBonusBaseBLL(int.Parse(ViewState["ID"].ToString()));           
        }
      
        bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
        bll.Model.PositionType = ddl_PositionType.SelectedItem.Text;
        bll.Model.BounsBase = decimal.Parse(txt_BounsBase.Text.Trim());
        if (ViewState["ID"] != null)
        {
            bll.Update();         
        }
        else 
        {

            if (FNA_StaffBonusBaseBLL.GetModelList("OrganizeCity=" + tr_OrganizeCity.SelectValue + " AND PositionType='" + ddl_PositionType.SelectedItem.Text+"'").Count > 0)
            {
                MessageBox.Show(this,"该片区内已有相关信息");
                return;
            }
            bll.Add(); 
        }    
        BindGrid();
    }

    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gvList.DataKeys[e.RowIndex]["FNA_StaffBonusBase_ID"].ToString());
        FNA_StaffBonusBaseBLL bll = new FNA_StaffBonusBaseBLL(id);
        bll.Delete();
        BindGrid();
    }
}
