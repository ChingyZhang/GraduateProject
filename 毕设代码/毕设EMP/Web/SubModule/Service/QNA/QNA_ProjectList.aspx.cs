// ===================================================================
// 文件路径:SubModule/Service/QNA/QNA_ProjectList.aspx.cs 
// 生成日期:2009/12/13 17:56:40 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.QNA;

public partial class SubModule_Service_QNA_QNA_ProjectList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
		// 在此处放置用户代码以初始化页面
		if (!Page.IsPostBack)
		{
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BindDropDown();
			
			BindGrid();			
		}
	}
	#region 绑定下拉列表框
    private void BindDropDown()
    {	
	}
	#endregion
	
	private void BindGrid()
    {
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1);
        string ConditionStr = " 1 = 1 ";

        ConditionStr += " AND MCS_QNA.dbo.QNA_Project.InsertTime between ' " + dtBegin.ToShortDateString() + " 'and '" + dtEnd.ToShortDateString() + "' ";

        if (tbx_Search.Text.Trim() != "")
        {
            ConditionStr += " AND  MCS_QNA.dbo.QNA_Project.Name  like '%" + tbx_Search.Text.Trim() + "%'";
        }

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

	}
	
	protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
	
	protected void bt_Add_Click(object sender, EventArgs e)
    {
		Response.Redirect("QNA_ProjectDetail.aspx");
    }

    protected string GetResultCount(int project)
    {
        return new QNA_ProjectBLL(project).GetResultCount().ToString();
    }

    protected void gv_List_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}