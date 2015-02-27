// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model.EWF;
public partial class SubModule_EWF_RoleList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindGrid();
            
            #region 权限判断
            //Right right = new Right();
            //string strUserName = Session["UserName"].ToString();
            //if (!right.GetAccessPermission(strUserName, 110, 0)) Response.Redirect("../noaccessright.aspx");        //有无查看的权限
            //if (!right.GetAccessPermission(strUserName, 0, 1001)) bt_Add.Visible = false;                              //有无新增的权限
            #endregion
        }
    }

    private void BindGrid()
    {
        string condition = "";
        if (!string.IsNullOrEmpty(tbx_Condition.Text))
        {
            condition = " EWF_Role_Role.Name Like '%" + tbx_Condition.Text + "%'";
        }
        gv_List.BindGrid<EWF_Role>(EWF_RoleBLL.GetModelList(condition));
    }

    #region 分页、排序、选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = int.Parse(gv_List.DataKeys[e.NewSelectedIndex].Value.ToString());
        Response.Redirect("RoleBasicDetail.aspx?Role=" + this.gv_List.DataKeys[e.NewSelectedIndex]["ID"].ToString());
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("RoleBasicDetail.aspx");
    }


    protected void btn_SetEnableFlag_Click(object sender, EventArgs e)
    {
        if (this.gv_List.Rows.Count != 0)
        {
            int Num = 0;
            foreach (GridViewRow gr in this.gv_List.Rows)
            {
                if (((CheckBox)gr.FindControl("cbx_ID")).Checked)
                {
                    Guid ID = (Guid)gv_List.DataKeys[gr.RowIndex]["ID"];
                    new EWF_RoleBLL(ID).Delete();
                    Num++;
                }
            }
            if (Num == 0)
            {
                MessageBox.Show(this, "请选择待操作的流程！");
                return;
            }
            gv_List.PageIndex = 0;
            BindGrid();
        }
        else
        {
            MessageBox.Show(this, "无流程待操作！");
            return;
        }
    }


    
}