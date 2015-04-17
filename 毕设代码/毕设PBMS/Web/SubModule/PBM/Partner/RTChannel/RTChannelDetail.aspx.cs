// ===================================================================
// 文件路径:SubModule/PBM/Partner/RTChannel/RTChannelDetail.aspx.cs 
// 生成日期:2015-02-04 16:14:30 
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
public partial class SubModule_PBM_Partner_RTChannel_RTChannelDetail : System.Web.UI.Page
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
		CM_RTChannel_TDP m = new CM_RTChannel_TDPBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);
            if (m.InsertStaff == 1)
            {
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
            }
        }
	}
	
	protected void bt_OK_Click(object sender, EventArgs e)
	{
		CM_RTChannel_TDPBLL _bll ;
       
		if ((int)ViewState["ID"]!=0)
		{
			//修改
			_bll = new CM_RTChannel_TDPBLL((int)ViewState["ID"]);
		}
		else
		{
			//新增
			_bll = new CM_RTChannel_TDPBLL();
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
				MessageBox.ShowAndRedirect(this,"修改成功!","RTChannelList.aspx");
			}
		}
		else
		{
			//新增
            TextBox ddl_Name = (TextBox)pl_detail.FindControl("CM_RTChannel_TDP_Name");
            IList<CM_RTChannel_TDP> ddl_Name_01 = CM_RTChannel_TDPBLL.GetModelList("Name='" + ddl_Name.Text.ToString() + "'" + "and OwnerClient=" + Session["OwnerClient"].ToString());
            if (ddl_Name_01.Count<=0)//判断渠道是否已经存在
            {
                _bll.Model.InsertStaff = (int)Session["UserID"];
                _bll.Model.OwnerClient = (int)Session["OwnerClient"];
                ViewState["ID"] = _bll.Add();
            }
            else
                MessageBox.Show(this,"渠道已存在！");
			if ((int)ViewState["ID"]>0)
			{
				MessageBox.ShowAndRedirect(this,"新增成功!","RTChannelList.aspx");
			}
		}
		
	}

}