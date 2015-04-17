using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using System.Data;
using MCSFramework.BLL;

public partial class SubModule_EWF_RoleOrganizeCityManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["Role"] = Request.QueryString["Role"] != null ? new Guid(Request.QueryString["Role"]) : Guid.Empty;

            BindDropDown();

            if ((Guid)ViewState["Role"] != Guid.Empty)
            {
                BindData();
            }

        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Role_Type");
        ddl_Type.DataBind();
        ddl_Type.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");
        ddl_Level.DataBind();
        ddl_Level.Items.Insert(0, new ListItem("请选择...", "0"));
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
        if (dt.Rows.Count > 0)
        {
            ddl_Level.SelectedValue = dt.Rows[0]["OrganizeCityLevel"].ToString();
        }
        #endregion
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["Role"] != Guid.Empty && ddl_Level.SelectedValue != "0")
        {
            EWF_RoleBLL role = new EWF_RoleBLL((Guid)ViewState["Role"]);
            role.Model.Name = tbx_Name.Text;
            role.Model.Description = tbx_Description.Text;
            role.Update();

            DataTable dt = role.GetDetailList();

            if (dt.Rows.Count > 0 && ddl_Level.SelectedValue != dt.Rows[0]["OrganizeCityLevel"].ToString())
            {
                role.Delete_OrganizeCityManager((int)dt.Rows[0]["OrganizeCityLevel"]);
            }

            role.Add_OrganizeCityManager(int.Parse(ddl_Level.SelectedValue));

            Response.Redirect("RoleList.aspx");
        }

    }
}
