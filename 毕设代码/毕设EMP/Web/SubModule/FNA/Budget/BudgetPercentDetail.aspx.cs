using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Model;
using MCSFramework.Common;

public partial class SubModule_FNA_Budget_BudgetPercentDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            BindDropDown();

            if ((int)ViewState["OrganizeCity"] > 0)
            {
                tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
                tr_OrganizeCity.Enabled = false;
                BindGrid();
            }
            else
            {

            }
        }
    }

    #region 绑定下拉列表框
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
    }
    #endregion

    private void BindGrid()
    {
        IList<FNA_BudgetPercentFeeType> list = FNA_BudgetPercentFeeTypeBLL.GetModelList("OrganizeCity=" + ViewState["OrganizeCity"].ToString());

        Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("FNA_FeeType");
        foreach (Dictionary_Data item in dic.Values)
        {
            if (list.FirstOrDefault(p => p.FeeType.ToString() == item.Code) == null)
            {
                FNA_BudgetPercentFeeType m = new FNA_BudgetPercentFeeType();
                m.FeeType = int.Parse(item.Code);
                m.BudgetPercent = 0;
                list.Add(m);
            }
        }
        gv_List.BindGrid<FNA_BudgetPercentFeeType>(list);

    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        #region 验证所有百分比选项是否小于或等于100，小于100时，其余部分用作机动费用
        decimal percent = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            percent += decimal.Parse(((TextBox)row.FindControl("tbx_BudgetPercent")).Text);
        }
        if (percent > 100)
        {
            MessageBox.Show(this, "对不起，各费用类型设定的百分比总和不能大于100！");
            return;
        }
        #endregion

        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            if (id == 0)
            {
                FNA_BudgetPercentFeeTypeBLL bll = new FNA_BudgetPercentFeeTypeBLL();
                bll.Model.OrganizeCity = (int)ViewState["OrganizeCity"];
                bll.Model.FeeType = (int)gv_List.DataKeys[row.RowIndex]["FeeType"];
                bll.Model.BudgetPercent = decimal.Parse(((TextBox)row.FindControl("tbx_BudgetPercent")).Text);
                bll.Model.ApproveFlag = 1;
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Add();
            }
            else
            {
                FNA_BudgetPercentFeeTypeBLL bll = new FNA_BudgetPercentFeeTypeBLL(id);
                bll.Model.BudgetPercent = decimal.Parse(((TextBox)row.FindControl("tbx_BudgetPercent")).Text);
                bll.Model.UpdateStaff = (int)Session["UserID"];
                bll.Update();
            }
        }

        MessageBox.ShowAndRedirect(this, "保存成功!", "BudgetPercentList.aspx");
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "" && tr_OrganizeCity.SelectValue != "0")
        {
            ViewState["OrganizeCity"] = int.Parse(tr_OrganizeCity.SelectValue);
            gv_List.PageIndex = 0;
            BindGrid();
        }
        else
        {
            MessageBox.Show(this, "对不起，请选择正确的管理片区单元!");
            return;
        }
    }
    
}
