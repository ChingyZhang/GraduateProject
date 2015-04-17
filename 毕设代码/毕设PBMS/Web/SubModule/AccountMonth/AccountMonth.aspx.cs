// ===================================================================
// 文件路径:SubModule/RM/AccountMonthe_list.aspx.cs 
// 生成日期:2008-1-29 16:20:24 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using System.Collections.Generic;

public partial class SubModule_AccountMonth_AccountMonth : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindGrid();
        }
    }

    private void BindGrid()
    {
        IList<AC_AccountMonth> _accountmonthlist = new AC_AccountMonthBLL()._GetModelList("");

        if (ViewState["PageIndex"] != null)
        {
            gv_List.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }

        gv_List.DataSource = _accountmonthlist;
        gv_List.DataBind();

        lb_rowcount.Text =_accountmonthlist.Count.ToString();
        tbx_PageGo.Text = (gv_List.PageIndex + 1).ToString();
    }

    private void BindData()
    {
        if (ViewState["ID"] != null)
        {
            AC_AccountMonth _month =new AC_AccountMonthBLL(int.Parse(ViewState["ID"].ToString())).Model;
            this.tbx_Name.Text = _month.Name;
            this.tbx_AccountMonth.Text = _month.Month.ToString();
            this.tbx_AccountYear.Text = _month.Year.ToString();
            this.tbx_BeginDate.Text = _month.BeginDate.ToShortDateString();
            this.tbx_EndDate.Text = _month.EndDate.ToShortDateString();
            this.bt_Add.Text = "修改";
            this.bt_Add.ForeColor = System.Drawing.Color.Red;
            this.btn_Delete.Visible = true;
            MessageBox.ShowConfirm(this.btn_Delete, "确实要删除所选择的会计月吗？");
        }
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
    protected void gv_List_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if (ViewState["SortDirect"] != null)
        //{
        //    if (ViewState["SortDirect"].ToString() == "DESC")
        //        ViewState["SortDirect"] = "ASC";
        //    else
        //        ViewState["SortDirect"] = "DESC";
        //}
        //else
        //    ViewState["SortDirect"] = "ASC";

        //ViewState["SortField"] = (string)e.SortExpression;

        //ViewState["Sort"] = e.SortExpression;
        //BindGrid();
    }
    protected void bt_PageOk_Click(object sender, EventArgs e)
    {
        int _page = Int32.Parse(tbx_PageGo.Text) - 1;
        if (_page >= 0 && _page <= gv_List.PageCount - 1)
        {
            ViewState["PageIndex"] = _page;
            BindGrid();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PageError", "<script language='javascript'>alert('页码范围无效!');</script>");
            tbx_PageGo.Text = ((int)ViewState["PageIndex"] + 1).ToString();
        }
    }
    #endregion


    protected void bt_Add_Click(object sender, EventArgs e)
    {
        AC_AccountMonthBLL _accountmonthbll;
        if (ViewState["ID"] != null)
        {
            //修改
            _accountmonthbll = new AC_AccountMonthBLL(int.Parse(ViewState["ID"].ToString()));
        }
        else
        {
            //新增
            _accountmonthbll = new AC_AccountMonthBLL();
        }

        #region 获取界面信息
        _accountmonthbll.Model.Name = this.tbx_Name.Text;
        _accountmonthbll.Model.Year = int.Parse(this.tbx_AccountYear.Text.Trim());
        _accountmonthbll.Model.BeginDate = DateTime.Parse(this.tbx_BeginDate.Text.Trim());
        _accountmonthbll.Model.EndDate = DateTime.Parse(this.tbx_EndDate.Text.Trim() + " 23:59:59");
        _accountmonthbll.Model.Month = int.Parse(this.tbx_AccountMonth.Text.Trim());
        #endregion

        if (ViewState["ID"] != null)
        {
            //修改
            if (_accountmonthbll.Update()<0)
            {
                lbl_AlertInfo.Text = "更新记录失败！";
                return;
            }
        }
        else
        {
            //新增

            if (_accountmonthbll.Add()<=0)
            {
                lbl_AlertInfo.Text = "新增记录失败！";
                return;
            }
        }

        Response.Redirect("AccountMonth.aspx");
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if (ViewState["ID"] != null)
        {
            AC_AccountMonthBLL _accountmonthbll= new AC_AccountMonthBLL(int.Parse(ViewState["ID"].ToString()));
            if (_accountmonthbll.Delete() < 0)
            {
                lbl_AlertInfo.Text = "删除会计月失败";
                return;
            }
        }
        else
        {
           lbl_AlertInfo.Text ="请选择要删除的会计月";
            return;
        }

        Response.Redirect("AccountMonth.aspx");
    }
}