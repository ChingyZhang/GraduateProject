// ===================================================================
// 文件路径:SubModule/PBM/Partner/RTSalesArea/RTSalesAreaDetail.aspx.cs 
// 生成日期:2015-02-04 16:15:35 
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
using MCSFramework.BLL.CM;using MCSFramework.Model.CM;
public partial class SubModule_PBM_Partner_RTSalesArea_RTSalesAreaDetail : System.Web.UI.Page
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
		CM_RTSalesArea_TDP m = new CM_RTSalesArea_TDPBLL((int)ViewState["ID"]).Model;
		if (m!=null) pl_detail.BindData(m);
	}
	
	protected void bt_OK_Click(object sender, EventArgs e)
	{
		CM_RTSalesArea_TDPBLL _bll ;
		if ((int)ViewState["ID"]!=0)
		{
			//修改
			_bll = new CM_RTSalesArea_TDPBLL((int)ViewState["ID"]);
		}
		else
		{
			//新增
			_bll = new CM_RTSalesArea_TDPBLL();
		}
		
		pl_detail.GetData(_bll.Model);
		
		#region 判断必填项
		
		#endregion
		if ((int)ViewState["ID"]!=0)
		{
			//修改
			_bll.Model.InsertStaff = (int)Session["UserID"];
			if (_bll.Update()==0)
			{
				MessageBox.ShowAndRedirect(this,"修改成功!","RTSalesAreaList.aspx");
			}
		}
		else
		{
			//新增
			_bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.OwnerClient =(int) Session["OwnerClient"];
           
			ViewState["ID"] = _bll.Add();
			if ((int)ViewState["ID"]>0)
			{
				MessageBox.ShowAndRedirect(this,"新增成功!","RTSalesAreaList.aspx");
			}
		}
		
	}

}