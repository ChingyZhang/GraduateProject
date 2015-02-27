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
using MCSFramework.Model.Pub;
public partial class SubModule_AccountQuarter_AccountQuarter : System.Web.UI.Page
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
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year=" +  DateTime.Now.Year.ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year=" + DateTime.Now.Year.ToString());
        ddl_EndMonth.DataBind();
        ddl_EndMonth.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_year.DataSource = AC_AccountMonthBLL.GetAllYear();
        ddl_year.DataBind();
        ddl_year.SelectedValue = DateTime.Now.Year.ToString();

        ddl_Quarter.DataSource = DictionaryBLL.GetDicCollections("PUB_Quarter");
        ddl_Quarter.DataBind();
        ddl_Quarter.Items.Insert(0, new ListItem("请选择", "0"));

    }
    private void BindData()
    {
        if (ViewState["ID"] != null)
        {
            AC_AccountQuarter _m = new AC_AccountQuarterBLL(int.Parse(ViewState["ID"].ToString())).Model;
            this.tbx_Name.Text = _m.Name;
            ddl_Quarter.SelectedValue = _m.Quarter.ToString();
            ddl_year.SelectedValue = _m.Year.ToString();
            ddl_BeginMonth.SelectedValue = _m.BeginMonth.ToString();
            ddl_EndMonth.SelectedValue = _m.EndMonth.ToString();

            this.bt_Add.Text = "修改";
            this.bt_Add.ForeColor = System.Drawing.Color.Red;
            this.btn_Delete.Visible = true;
            MessageBox.ShowConfirm(this.btn_Delete, "确实要删除所选择的会计月吗？");
        }
        else
        {
 
        }
    }
    private void BindGrid()
    {
       
        gv_List.ConditionString = "";
        gv_List.BindGrid();
    }
    protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year="+ddl_year.SelectedValue);
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year=" + ddl_year.SelectedValue);
        ddl_EndMonth.DataBind();
        ddl_EndMonth.Items.Insert(0, new ListItem("请选择", "0"));
    }
    protected void ddl_BeginMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_EndMonth.Items.FindByValue((int.Parse(ddl_BeginMonth.SelectedValue) + 2).ToString()) != null)
        {
            ddl_EndMonth.SelectedValue = (int.Parse(ddl_BeginMonth.SelectedValue) + 2).ToString();
        }
        else
        {
            ddl_BeginMonth.SelectedValue = "0";
            ddl_EndMonth.SelectedValue = "0";
            MessageBox.Show(this, "请先维护后续会计月");
        }
    }

    protected void ddl_Quarter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Quarter.SelectedIndex != 0)
        {
            ddl_BeginMonth.SelectedValue = (AC_AccountMonthBLL.GetModelList("Month=1 AND Year=" + ddl_year.SelectedValue)[0].ID + (int.Parse(ddl_Quarter.SelectedValue) - 1) * 3).ToString();
            ddl_EndMonth.SelectedValue = (AC_AccountMonthBLL.GetModelList("Month=1 AND Year=" + ddl_year.SelectedValue)[0].ID + int.Parse(ddl_Quarter.SelectedValue) * 3 - 1).ToString();
        }
        else
        {
            ddl_BeginMonth.SelectedValue = "0";
            ddl_EndMonth.SelectedValue = "0";
        }
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        AC_AccountQuarterBLL bll = new AC_AccountQuarterBLL();
        if (ViewState["ID"] != null)
        {
            //修改
            bll = new AC_AccountQuarterBLL(int.Parse(ViewState["ID"].ToString()));
        }
        else
        {
            //新增
            bll = new AC_AccountQuarterBLL();
        }
        bll.Model.Name = tbx_Name.Text.Trim();
        int year, begainmonth, endmonth, Quarter;
        if (int.TryParse(ddl_year.SelectedValue, out year) && year ==0)
        {
            MessageBox.Show(this, "请选择会计年度");
            return;
        }
        if (int.TryParse(ddl_BeginMonth.SelectedValue, out begainmonth) && begainmonth == 0)
        {
            MessageBox.Show(this, "请选择开始月份");
            return;
        }
        if (int.TryParse(ddl_EndMonth.SelectedValue, out endmonth) && endmonth == 0)
        {
            MessageBox.Show(this, "请选择截止月份");
            return;
        }
        if (int.TryParse(ddl_Quarter.SelectedValue, out Quarter) && Quarter == 0)
        {
            MessageBox.Show(this, "请选择季度");
            return;
        }
        bll.Model.BeginDate = new AC_AccountMonthBLL(begainmonth).Model.BeginDate;
        bll.Model.EndDate = new AC_AccountMonthBLL(endmonth).Model.BeginDate;
        bll.Model.Year = year;
        bll.Model.Quarter = Quarter;
        bll.Model.BeginMonth = begainmonth;
        bll.Model.EndMonth = endmonth;

        if (ViewState["ID"] != null)
        {
            //修改
            if (bll.Update() < 0)
            {
                lbl_AlertInfo.Text = "更新记录失败！";
                return;
            }
        }
        else
        {
            //新增

            if (bll.Add() <= 0)
            {
                lbl_AlertInfo.Text = "新增记录失败！";
                return;
            }
        }
        BindGrid();
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if (ViewState["ID"] != null)
        {
            AC_AccountQuarterBLL bll = new AC_AccountQuarterBLL(int.Parse(ViewState["ID"].ToString()));
            bll.Delete();  
        }
       
        BindGrid();

    }

    #region 分页、排序、选中等事件
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["ID"] = gv_List.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindData();
    }
    
    
    #endregion
}
