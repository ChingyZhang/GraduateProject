using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;

public partial class SubModule_EWF_FlowProcessCCDetail : System.Web.UI.Page
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
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
        lb_AppName.Text = app.Model.Name;

        // 默认下一环节
        ddl_DefaultNextProcess.DataSource = app.GetProcessList();
        ddl_DefaultNextProcess.DataBind();
        ddl_DefaultNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        //环节类型
        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_ProcessType");
        ddl_Type.DataBind();
        this.ddl_Type.SelectedValue = "9";

        ddl_RecipientRole_Decision.DataSource = EWF_RoleBLL.GetModelList("");
        ddl_RecipientRole_Decision.DataBind();
        ddl_RecipientRole_Decision.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_DataObject_Decision.DataSource = app.GetDataObjectList();
        ddl_DataObject_Decision.DataBind();
        ddl_DataObject_Decision.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));
    }
    #endregion

    private void BindData()
    {
        EWF_Flow_ProcessCCBLL bll = new EWF_Flow_ProcessCCBLL((Guid)ViewState["ProcessID"]);

        #region 绑定基本信息
        this.lb_ID.Text = ViewState["ProcessID"].ToString();
        tbx_Name.Text = bll.Model.Name;
        tbx_Description.Text = bll.Model.Description;
        ddl_DefaultNextProcess.SelectedValue = bll.Model.DefaultNextProcess.ToString();
        tbx_Sort.Text = bll.Model.Sort.ToString();
        #endregion

        ddl_RecipientRole_Decision.SelectedValue = bll.Model.RecipientRole.ToString();
        ddl_RecipientRole_Decision_SelectedIndexChanged(null, null);
        ddl_DataObject_Decision.SelectedValue = bll.Model.DataObject.ToString();
        tbx_MessageSubject.Text = bll.Model.MessageSubject;
       
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] != Guid.Empty)//修改
        {
            EWF_Flow_ProcessCCBLL bll = new EWF_Flow_ProcessCCBLL((Guid)ViewState["ProcessID"]);

            bll.Model.DataObject = new Guid(ddl_DataObject_Decision.SelectedValue);
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);

            bll.Model.MessageSubject = tbx_MessageSubject.Text;
            bll.Model.Name = tbx_Name.Text;
            bll.Model.RecipientRole = new Guid(ddl_RecipientRole_Decision.SelectedValue);
            bll.Update();
        }
        else//新增
        {
            EWF_Flow_ProcessCCBLL bll = new EWF_Flow_ProcessCCBLL();
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.DataObject = new Guid(ddl_DataObject_Decision.SelectedValue);
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);
            bll.Model.Type = 9;

            bll.Model.MessageSubject = tbx_MessageSubject.Text;
            bll.Model.Name = tbx_Name.Text;
            bll.Model.RecipientRole = new Guid(ddl_RecipientRole_Decision.SelectedValue);
            bll.Add();

        }
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());

    }

    protected void ddl_RecipientRole_Decision_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_DataObject_Decision.SelectedValue = Guid.Empty.ToString();
        if (ddl_RecipientRole_Decision.SelectedValue != Guid.Empty.ToString())
        {
            int roletype = new EWF_RoleBLL(new Guid(ddl_RecipientRole_Decision.SelectedValue)).Model.Type;
            if (roletype == 3 || roletype == 6)
            {
                this.ddl_DataObject_Decision.Enabled = true;
            }
            else
            {
                this.ddl_DataObject_Decision.Enabled = false;
            }
        }
        else
            this.ddl_DataObject_Decision.Enabled = false;
    }
    protected void bt_Del_Click(object sender, EventArgs e)
    {
        new EWF_Flow_ProcessCCBLL((Guid)ViewState["ProcessID"]).Delete();
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }
}
