// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSourceDetail.aspx.cs 
// 生成日期:2010/9/30 10:53:18 
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
using MCSFramework.BLL.RPT;
using MCSFramework.Model.RPT;
public partial class SubModule_Reports_Rpt_DataSourceDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion

            BindDropDown();

            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                TextBox tbx_ProviderName = (TextBox)pl_detail.FindControl("Rpt_DataSource_ProviderName");
                if (tbx_ProviderName != null) tbx_ProviderName.Text = "System.Data.SqlClient";
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        Rpt_DataSource m = new Rpt_DataSourceBLL((Guid)ViewState["ID"]).Model;
        if (m != null) pl_detail.BindData(m);
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Rpt_DataSourceBLL _bll;
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll = new Rpt_DataSourceBLL((Guid)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Rpt_DataSourceBLL();
        }

        string pre_conn = _bll.Model.ConnectionString;
        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion

        if (_bll.Model.ConnectionString != pre_conn && _bll.Model.ConnectionString != "")
            _bll.Model.ConnectionString = DataEncrypter.EncrypteString(_bll.Model.ConnectionString);

        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "Rpt_DataSourceList.aspx");
            }
        }
        else
        {
            //新增
            if (_bll.Add() == 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "Rpt_DataSourceList.aspx");
            }
        }

    }

}