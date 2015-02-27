// ===================================================================
// 文件路径:SubModule/CM/RebateRule/RebateRuleDetail.aspx.cs 
// 生成日期:2012/1/18 12:38:24 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL;
public partial class SubModule_CM_RebateRule_RebateRuleDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                tb_ApplyCity.Visible = false;
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
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

    private void BindData()
    {
        CM_RebateRule m = new CM_RebateRuleBLL((int)ViewState["ID"]).Model;
        if (m != null) pl_detail.BindData(m);

        BindGrid();
    }

    private void BindGrid()
    {
        gv_List.ConditionString = "RebateRule=" + ViewState["ID"].ToString();
        gv_List.BindGrid();
    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_RebateRuleBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new CM_RebateRuleBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new CM_RebateRuleBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "RebateRuleList.aspx");
            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "RebateRuleDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }

    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        IList<CM_RebateRule_ApplyCity> ApplyCitys = CM_RebateRule_ApplyCityBLL.GetModelList("RebateRule=" + ViewState["ID"].ToString());

        int city = 0;
        if (int.TryParse(tr_OrganizeCity.SelectValue, out city) && city > 0)
        {
            if (ApplyCitys.FirstOrDefault(p => p.OrganizeCity == city) != null)
            {
                MessageBox.Show(this, "对不起，该区域已属于该返利方案，请勿重复添加!");
                return;
            }

            Addr_OrganizeCityBLL c = new Addr_OrganizeCityBLL(city);
            foreach (DataRow row in c.GetFullPath().Rows)
            {
                if (ApplyCitys.FirstOrDefault(p => p.OrganizeCity == (int)row["ID"]) != null)
                {
                    MessageBox.Show(this, "对不起，该区域的上级区域" + new Addr_OrganizeCityBLL((int)row["ID"]).Model.Name + "已属于该返利方案，请勿重复添加!");
                    return;
                }
            }

            foreach (DataRow row in c.GetAllChildNode().Rows)
            {
                if (ApplyCitys.FirstOrDefault(p => p.OrganizeCity == (int)row["ID"]) != null)
                {
                    MessageBox.Show(this, "对不起，该区域的下级区域" + new Addr_OrganizeCityBLL((int)row["ID"]).Model.Name + "已属于该返利方案，请勿重复添加!");
                    return;
                }
            }

            CM_RebateRule_ApplyCityBLL bll = new CM_RebateRule_ApplyCityBLL();
            bll.Model.RebateRule = (int)ViewState["ID"];
            bll.Model.OrganizeCity = city;
            bll.Add();
        }

        BindGrid();
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex][0];
                CM_RebateRule_ApplyCityBLL bll = new CM_RebateRule_ApplyCityBLL(id);
                bll.Delete();
            }
        }

        BindGrid();
    }
}