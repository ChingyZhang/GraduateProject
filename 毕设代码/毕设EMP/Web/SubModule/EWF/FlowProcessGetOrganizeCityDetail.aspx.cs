using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;

public partial class SubModule_EWF_FlowProcessGetOrganizeCityDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["AppID"] = Request.QueryString["AppID"] != null ? new Guid(Request.QueryString["AppID"]) : Guid.Empty;
            ViewState["ProcessID"] = Request.QueryString["ProcessID"] != null ? new Guid(Request.QueryString["ProcessID"]) : Guid.Empty;
            #endregion


            if ((Guid)ViewState["AppID"] == Guid.Empty)
            {
                MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../Login/Index.aspx");
                return;
            }

            BindDropDown();
            if ((Guid)ViewState["ProcessID"] != Guid.Empty)
            {
                BindData();
                this.bt_Save.Text = "修改";
                this.bt_Save.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                bt_Del.Visible = false;
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        // 默认下一环节
        EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
        lb_AppName.Text = app.Model.Name;

        ddl_DefaultNextProcess.DataSource = app.GetProcessList();
        ddl_DefaultNextProcess.DataBind();
        ddl_DefaultNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_ProcessType");
        ddl_Type.DataBind();
        ddl_Type.SelectedValue = "8";

        ddl_OrganizeCityLevel.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");
        ddl_OrganizeCityLevel.DataBind();
        ddl_OrganizeCityLevel.Items.Insert(0, new ListItem("发起人所在级别", "0"));

        ddl_DataObject_OrganizeCityID.DataSource = app.GetDataObjectList();
        ddl_DataObject_OrganizeCityID.DataBind();
        ddl_DataObject_OrganizeCityID.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_DataObject_OrganizeCityName.DataSource = app.GetDataObjectList();
        ddl_DataObject_OrganizeCityName.DataBind();
        ddl_DataObject_OrganizeCityName.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));
    }
    #endregion

    private void BindData()
    {
        this.lb_ID.Text = ViewState["ProcessID"].ToString();
        EWF_Flow_ProcessGetOrganizeCityBLL bll = new EWF_Flow_ProcessGetOrganizeCityBLL((Guid)ViewState["ProcessID"]);

        #region 绑定基本信息
        tbx_Name.Text = bll.Model.Name;
        tbx_Description.Text = bll.Model.Description;
        ddl_DefaultNextProcess.SelectedValue = bll.Model.DefaultNextProcess.ToString();
        ddl_Type.SelectedValue = bll.Model.Type.ToString();
        tbx_Sort.Text = bll.Model.Sort.ToString();

        ddl_OrganizeCityLevel.SelectedValue = bll.Model.OrganizeCityLevel.ToString();
        ddl_DataObject_OrganizeCityID.SelectedValue = bll.Model.OrganizeCityID.ToString();
        ddl_DataObject_OrganizeCityName.SelectedValue = bll.Model.OrganizeCityName.ToString();
        #endregion
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] != Guid.Empty)//修改
        {
            EWF_Flow_ProcessGetOrganizeCityBLL bll = new EWF_Flow_ProcessGetOrganizeCityBLL((Guid)ViewState["ProcessID"]);

            bll.Model.Name = tbx_Name.Text;
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);

            bll.Model.OrganizeCityLevel = int.Parse(ddl_OrganizeCityLevel.SelectedValue);
            bll.Model.OrganizeCityID = new Guid(ddl_DataObject_OrganizeCityID.SelectedValue);
            bll.Model.OrganizeCityName = new Guid(ddl_DataObject_OrganizeCityName.SelectedValue);

            bll.Update();

        }
        else//新增
        {
            EWF_Flow_ProcessGetOrganizeCityBLL bll = new EWF_Flow_ProcessGetOrganizeCityBLL();
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.Name = tbx_Name.Text;
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            if (!string.IsNullOrEmpty(ddl_Type.SelectedValue)) bll.Model.Type = int.Parse(ddl_Type.SelectedValue);
            bll.Model.Sort = int.Parse(tbx_Sort.Text);
            bll.Model.Type = 8;

            bll.Model.OrganizeCityLevel = int.Parse(ddl_OrganizeCityLevel.SelectedValue);
            bll.Model.OrganizeCityID = new Guid(ddl_DataObject_OrganizeCityID.SelectedValue);
            bll.Model.OrganizeCityName = new Guid(ddl_DataObject_OrganizeCityName.SelectedValue);
            bll.Add();
        }

        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        new EWF_Flow_ProcessGetOrganizeCityBLL((Guid)ViewState["ProcessID"]).Delete();
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }
}
