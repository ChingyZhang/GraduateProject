// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Model;
public partial class SubModule_EWF_RoleDORelationalList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["Role"] = Request.QueryString["Role"] != null ? new Guid(Request.QueryString["Role"]) : Guid.Empty;
            ViewState["PageIndex"] = 0;

            BindDropDown();

            if ((Guid)ViewState["Role"] != Guid.Empty)
            {
                BindData();
            }

            #region 权限判断
            //Right right = new Right();
            //string strUserName = Session["UserName"].ToString();
            //if (!right.GetAccessPermission(strUserName, 110, 0)) Response.Redirect("../noaccessright.aspx");        //有无查看的权限
            //if (!right.GetAccessPermission(strUserName, 0, 1001)) bt_Add.Visible = false;                              //有无新增的权限
            #endregion
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Role_Type");
        ddl_Type.DataBind();
        ddl_Type.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    #endregion

    private void BindData()
    {
        EWF_RoleBLL role = new EWF_RoleBLL((Guid)ViewState["Role"]);
        #region 绑定基本信息
        lb_ID.Text = ViewState["Role"].ToString();
        tbx_Name.Text = role.Model.Name;
        tbx_Description.Text = role.Model.Description;
        ddl_Type.SelectedValue = role.Model.Type.ToString();
        #endregion

        #region 绑定角色关联明细信息
        DataTable dt = role.GetDetailList();
        DataColumn[] pk = { dt.Columns["ScrValue"], dt.Columns["RecipientStaff"] };
        dt.PrimaryKey = pk;
        dt.AcceptChanges();
        ViewState["DTDetail"] = dt;
        BindGrid();
        #endregion
    }
    private void BindGrid()
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];

        if (!string.IsNullOrEmpty(tbx_Condition.Text))
        {
            dt.DefaultView.RowFilter = " ScrValue Like '%" + tbx_Condition.Text + "%'";
        }
        if (ViewState["Sort"] != null)
        {
            dt.DefaultView.Sort = ViewState["Sort"].ToString() + " " + ViewState["SortDirect"];
        }
        gv_List.DataSource = dt.DefaultView;
        gv_List.DataBind();

    }

    #region 分页、排序、选中等事件
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = int.Parse(gv_List.DataKeys[e.NewSelectedIndex].Value.ToString());

    }
    protected void gv_List_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortDirect"] != null)
        {
            if (ViewState["SortDirect"].ToString() == "DESC")
                ViewState["SortDirect"] = "ASC";
            else
                ViewState["SortDirect"] = "DESC";
        }
        else
            ViewState["SortDirect"] = "ASC";

        ViewState["SortField"] = (string)e.SortExpression;

        ViewState["Sort"] = e.SortExpression;
        BindGrid();
    }

    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];
        Org_Staff staff = new Org_StaffBLL(int.Parse(select_Recipient.SelectValue)).Model;
        string scrvalue = tbx_ScrValue.Text;

        if (staff != null)
        {
            object[] pks = { scrvalue, staff.ID };
            if (!dt.Rows.Contains(pks))
            {
                DataRow dr = dt.NewRow();
                dr["Role"] = (Guid)ViewState["Role"];
                dr["ScrValue"] = scrvalue;
                dr["RecipientStaff"] = staff.ID;
                dr["RecipientStaffName"] = staff.RealName;
                dr["RecipientStaffCode"] = staff.StaffCode;
                dt.Rows.Add(dr);
            }
            BindGrid();
        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string SrcValue = gv_List.DataKeys[e.RowIndex]["ScrValue"].ToString();
        int RecipientStaff = (int)gv_List.DataKeys[e.RowIndex]["RecipientStaff"];

        DataTable dt = (DataTable)ViewState["DTDetail"];

        object[] pks = { SrcValue, RecipientStaff };
        if (dt.Rows.Contains(pks))
        {
            dt.Rows.Find(pks).Delete();
        }
        BindGrid();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["Role"] != Guid.Empty)
        {
            EWF_RoleBLL role = new EWF_RoleBLL((Guid)ViewState["Role"]);
            role.Model.Name = tbx_Name.Text;
            role.Model.Description = tbx_Description.Text;
            role.Update();

            DataTable dt = (DataTable)ViewState["DTDetail"];
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr.RowState)
                {
                    case DataRowState.Added:
                        role.Add_DORelational((string)dr["ScrValue"], (int)dr["RecipientStaff"]);
                        break;
                    case DataRowState.Deleted:
                        role.Delete_DORelational((string)dr["ScrValue", DataRowVersion.Original], (int)dr["RecipientStaff", DataRowVersion.Original]);
                        break;
                }
            }
        }
        Response.Redirect("RoleList.aspx");
    }
}