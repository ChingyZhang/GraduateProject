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
public partial class SubModule_EWF_RolePositionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["Role"] = Request.QueryString["Role"] != null ? new Guid(Request.QueryString["Role"]) : Guid.Empty;
            ViewState["State"] = 0;
            ViewState["PageIndex"] = 0;


            BindDropDown();

            if ((Guid)ViewState["Role"] !=Guid.Empty)
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

        tr_SuperPosition.DataSource = Org_PositionBLL.GetAllPostion();
        tr_SuperPosition.DataBind();
        tr_SuperPosition.SelectValue = "0";
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
        DataColumn[] pk = { dt.Columns["Position"] };
        dt.PrimaryKey = pk;
        dt.AcceptChanges();
        ViewState["DTDetail"] = dt;
        BindGrid();
        #endregion
    }

    private void BindGrid()
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];

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


    protected void bt_Add_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];

        int position = int.Parse(tr_SuperPosition.SelectValue);
        if (position == 0) return;
        if (!dt.Rows.Contains(position))
        {
            DataRow dr = dt.NewRow();
            dr["Role"] = (Guid)ViewState["Role"];
            dr["Position"] = position;
            dr["PositionName"] = tr_SuperPosition.SelectText;
            dr["RoleName"] = tbx_Name.Text;
            dt.Rows.Add(dr);

            BindGrid();
        }
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Position = (int)gv_List.DataKeys[e.RowIndex]["Position"];
        DataTable dt = (DataTable)ViewState["DTDetail"];

        if (dt.Rows.Contains(Position))
        {
            dt.Rows.Find(Position).Delete();
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
                        role.Add_Position((int)dr["Position"]);
                        break;
                    case DataRowState.Deleted:
                        role.Delete_Position((int)dr["Position", DataRowVersion.Original]);
                        break;
                }
            }
        }
        Response.Redirect("RoleList.aspx");
    }
}