// ===================================================================
// 文件路径:SubModule/PBM/Account/BalanceUsageDetail.aspx.cs 
// 生成日期:2015/1/27 14:31:53 
// 作者:	  Jace
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.PBM;using MCSFramework.Model.PBM;
public partial class SubModule_PBM_Account_BalanceUsageDetail : System.Web.UI.Page
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
			
			if ((int)ViewState["ID"]!=0)
			{
				//修改
				BindData();
			}
			else
			{
				//新增
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
		AC_BalanceUsageList m = new AC_BalanceUsageListBLL((int)ViewState["ID"]).Model;
		if (m!=null) pl_detail.BindData(m);
	}
	
	protected void bt_OK_Click(object sender, EventArgs e)
	{
		AC_BalanceUsageListBLL _bll ;
		if ((int)ViewState["ID"]!=0)
		{
			//修改
			_bll = new AC_BalanceUsageListBLL((int)ViewState["ID"]);
		}
		else
		{
			//新增
			_bll = new AC_BalanceUsageListBLL();
		}
		
		pl_detail.GetData(_bll.Model);
		
		#region 判断必填项
		
		#endregion
		if ((int)ViewState["ID"]!=0)
		{
			//修改
			if (_bll.Update()==0)
			{
				MessageBox.ShowAndRedirect(this,"修改成功!","BalanceUsageList.aspx");
			}
		}
		else
		{
			//新增
			_bll.Model.InsertStaff = (int)Session["UserID"];
			ViewState["ID"] = _bll.Add();
			if ((int)ViewState["ID"]>0)
			{
				MessageBox.ShowAndRedirect(this,"新增成功!","BalanceUsageList.aspx");
			}
		}
		
	}

}