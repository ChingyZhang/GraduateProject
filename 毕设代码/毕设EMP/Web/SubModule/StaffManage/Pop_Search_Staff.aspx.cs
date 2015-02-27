using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model;

public partial class SubModule_StaffManage_Pop_Search_Staff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //是否可以多选
            ViewState["MultiSelected"] = Request.QueryString["MultiSelected"] == null ? false :
                (Request.QueryString["MultiSelected"].ToUpper() == "Y");

            #region 获取由Select控件传送过来的值

            if ((bool)ViewState["MultiSelected"] && Request.QueryString["SelectValue"] != null &&
                Request.QueryString["SelectValue"].ToString() != "")
            {
                string selectvalue = Request.QueryString["SelectValue"].ToString();
                string[] list_value = selectvalue.Split(new char[] { ',' });
                foreach (string item in list_value)
                {
                    tbx_value.Text += item + ",";
                    tbx_text.Text += new Org_StaffBLL(int.Parse(item)).Model.RealName + ",";
                }
                if (tbx_value.Text.Length > 0 && tbx_value.Text.Substring(tbx_value.Text.Length - 1, 1) == ",")
                {
                    tbx_value.Text = tbx_value.Text.Substring(0, tbx_value.Text.Length - 1);
                    tbx_text.Text = tbx_text.Text.Substring(0, tbx_text.Text.Length - 1);
                }
            }

            ViewState["MultiSelected"] = Request.QueryString["MultiSelected"] == null ? false :
                (Request.QueryString["MultiSelected"].ToUpper() == "Y");
            #endregion

            BindDropDown();

            if (Request.QueryString["OrganizeCity"] != null)
            {
                tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"];
                tr_OrganizeCity.Enabled = false;
            }
            if (Request.QueryString["Position"] != null)
            {
                tr_Position.RootValue = Request.QueryString["Position"];
                tr_Position.SelectValue = Request.QueryString["Position"];
            }

            BindGrid();
        }
    }

    public void gv_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox checkbox = (CheckBox)e.Row.FindControl("cb_select");
            string select_value = gv_List.DataKeys[e.Row.RowIndex][0].ToString();
            string[] list_value = tbx_value.Text.Split(new char[] { ',' });
            foreach (string item in list_value)
            {
                if (select_value == item)
                {
                    checkbox.Checked = true;
                }
            }
        }
    }

    public void cb_select_CheckedChanged(object sender, EventArgs e)
    {
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;//获取复选框当前所在的行
        CheckBox checkbox = (CheckBox)this.gv_List.Rows[index].Cells[0].FindControl("cb_select");
        //获取已经选择的值列表
        string[] list_value = tbx_value.Text.Split(new char[] { ',' });
        string[] list_text = tbx_text.Text.Split(new char[] { ',' });

        //获取当前选择行的值
        string select_value = gv_List.DataKeys[index][0].ToString();
        string select_text = (string)gv_List.DataKeys[index][1];

        if (checkbox.Checked)
        {
            if (!(bool)ViewState["MultiSelected"] && tbx_value.Text != "")
            {
                MessageBox.Show(this, "对不起，不可多选!当前已选择项:" + tbx_text.Text);
                checkbox.Checked = false;
                return;
            }
            if (tbx_value.Text != "")
            {
                tbx_value.Text += "," + select_value;
                tbx_text.Text += "," + select_text;
            }
            else
            {
                tbx_value.Text += select_value;
                tbx_text.Text += select_text;
            }

        }
        else
        {
            tbx_value.Text = "";
            tbx_text.Text = "";
            for (int i = 0; i < list_value.Length; i++)
            {
                if (list_value[i] != select_value)
                {
                    if (i == list_value.Length - 1)
                    {
                        tbx_value.Text += list_value[i];
                        tbx_text.Text += list_text[i];
                    }
                    else
                    {
                        tbx_value.Text += list_value[i] + ",";
                        tbx_text.Text += list_text[i] + ",";
                    }
                }
            }
        }

        if (tbx_value.Text.Length > 0 && tbx_value.Text.Substring(tbx_value.Text.Length - 1, 1) == ",")
        {
            tbx_value.Text = tbx_value.Text.Substring(0, tbx_value.Text.Length - 1);
            tbx_text.Text = tbx_text.Text.Substring(0, tbx_text.Text.Length - 1);
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

        #region 绑定职位
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();

        #region 如果非总部职位，其只能选择自己职位及以下职位
        Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
        if (p != null && p.IsHeadOffice != "Y" && p.Remark != "OfficeHR")
        {
            tr_Position.RootValue = p.SuperID.ToString();
            tr_Position.SelectValue = staff.Model.Position.ToString();
        }
        else
        {
            tr_Position.RootValue = "0";
            tr_Position.SelectValue = staff.Model.Position.ToString();
        }
        #endregion
        #endregion
    }
    #endregion

    private void BindGrid()
    {
        string condition = " Dimission=1 AND ApproveFlag=1 ";     //仅仅查询在职的
        if (tbx_Condition.Text != string.Empty)
        {
            condition += " AND " + ddl_SearchType.SelectedValue + " Like '%" + this.tbx_Condition.Text.Trim() + "%'";
        }

        if (tr_Position.SelectValue != "0")
        {
            if (cb_IncludeChild.Checked || tr_Position.SelectValue == tr_Position.RootValue)
            {
                string positions = "";
                Org_PositionBLL pos = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
                positions = pos.GetAllChildPosition();
                if (tr_Position.SelectValue != tr_Position.RootValue)
                {
                    if (positions != "") positions += ",";
                    positions += tr_Position.SelectValue;
                }

                condition += " AND Org_Staff.Position IN (" + positions + ")";
            }
            else
                condition += " AND Org_Staff.Position = " + tr_Position.SelectValue;
        }

        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            string orgcitys = "";
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND (Org_Staff.OrganizeCity IN (" + orgcitys + 
                ") OR Org_Staff.ID IN (SELECT Staff FROM MCS_SYS.dbo.Org_Staff_StaffInOrganizeCity WHERE OrganizeCity IN (" + orgcitys + ")) )";            
        }
        #endregion

        if (Request.QueryString["ExtCondition"] != null)
        {
            condition += " AND (" + Request.QueryString["ExtCondition"] + ")";
        }

        #region 将指定区域的上级经理加入人员选择窗口中
        if (Request.QueryString["IncludeSuperManager"] != null && Request.QueryString["OrganizeCity"] != null)
        {
            int supercitymanager = 0;
            Addr_OrganizeCity city1 = new Addr_OrganizeCityBLL(int.Parse(Request.QueryString["OrganizeCity"])).Model;
            if (city1 != null)
            {
                Addr_OrganizeCity city2 = new Addr_OrganizeCityBLL(city1.SuperID).Model;
                if (city2 != null) supercitymanager = city2.Manager;
            }

            if (supercitymanager != 0)
            {
                condition += " OR Org_Staff.ID=" + supercitymanager.ToString();
            }
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        tbx_value.Text = gv_List.DataKeys[e.NewSelectedIndex][0].ToString();
        tbx_text.Text = (string)gv_List.DataKeys[e.NewSelectedIndex][1];
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }
}
