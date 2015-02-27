// ===================================================================
// 文件路径:SubModule/RM/RetailerDetail.aspx.cs 
// 生成日期:2007-12-29 14:26:36 
// 作者:	  
// ===================================================================
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;

public partial class SubModule_EWF_Role_RoleBasicDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["Role"] = Request.QueryString["Role"] != null ? new Guid(Request.QueryString["Role"]) : Guid.Empty;
            #endregion

            BindDropDown();

            if ((Guid)ViewState["Role"] == Guid.Empty)
            {
                ddl_Type.Enabled = true;
            }
            else
            {
                ddl_Type.Enabled = false;
                this.bt_Save.Text = "修改";
                this.bt_Save.ForeColor = System.Drawing.Color.Red;
                BindData();
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        //所属流程
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

        switch (role.Model.Type)
        {
            case 1:
                Response.Redirect("RoleStaticList.aspx?Role=" + ViewState["Role"].ToString());
                break;
            case 2:
            case 8:
                Response.Redirect("RolePositionList.aspx?Role=" + ViewState["Role"].ToString());
                break;
            case 3:
                Response.Redirect("RoleDORelationalList.aspx?Role=" + ViewState["Role"].ToString());
                break;
            case 4:
                Response.Redirect("RoleDirector.aspx?Role=" + ViewState["Role"].ToString());
                break;
            case 6:
                Response.Redirect("RoleOrganizeCityManager.aspx?Role=" + ViewState["Role"].ToString());
                break;
        }

        #endregion
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (this.ddl_Type.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择角色类型！");
            return;
        }
        EWF_RoleBLL role;
        if ((Guid)ViewState["Role"] == Guid.Empty)
        {
            role = new EWF_RoleBLL();
            role.Model.Name = tbx_Name.Text;
            role.Model.Description = tbx_Description.Text;
            role.Model.Type = int.Parse(ddl_Type.SelectedValue);
            role.Add();
        }
        else
        {
            role = new EWF_RoleBLL((Guid)ViewState["Role"]);
            role.Model.Name = tbx_Name.Text;
            role.Model.Description = tbx_Description.Text;
            role.Update();
        }
        Response.Redirect("RoleList.aspx");
        
    }
}
