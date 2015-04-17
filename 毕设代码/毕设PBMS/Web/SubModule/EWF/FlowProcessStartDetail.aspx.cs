// ===================================================================
// 文件路径:SubModule/RM/RetailerDetail.aspx.cs 
// 生成日期:2007-12-29 14:26:36 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.BLL;

public partial class SubModule_EWF_FlowProcessStartDetail : System.Web.UI.Page
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
                this.ddl_Type.SelectedValue = Request.QueryString["Type"];
            }
            if (ddl_Type.SelectedValue == "2")
            {
                ddl_DefaultNextProcess.Enabled = false;
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        //所属流程
        EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
        lb_AppName.Text = app.Model.Name;

        // 默认下一环节
        ddl_DefaultNextProcess.DataSource = app.GetProcessList();
        ddl_DefaultNextProcess.DataBind();
        ddl_DefaultNextProcess.Items.Insert(0,new ListItem("请选择...",Guid.Empty.ToString()));

        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_ProcessType");
        ddl_Type.DataBind(); 
    }
    #endregion

    private void BindData()
    {
        this.lb_ID.Text = ViewState["ProcessID"].ToString();
        EWF_Flow_ProcessBLL bll = new EWF_Flow_ProcessBLL((Guid)ViewState["ProcessID"]);
        
        #region 绑定基本信息
        tbx_Name.Text = bll.Model.Name;
        tbx_Description.Text = bll.Model.Description;
        ddl_DefaultNextProcess.SelectedValue = bll.Model.DefaultNextProcess.ToString();
        ddl_Type.SelectedValue = bll.Model.Type.ToString();
        tbx_Sort.Text = bll.Model.Sort.ToString();
        #endregion
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] != Guid.Empty)//修改
        {
            EWF_Flow_ProcessBLL bll = new EWF_Flow_ProcessBLL((Guid)ViewState["ProcessID"]);
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Name = tbx_Name.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);
            bll.Update();
        }
        else//新增
        {
            EWF_Flow_ProcessBLL bll = new EWF_Flow_ProcessBLL();
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue.Trim());
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Name = tbx_Name.Text;
            bll.Model.Type = int.Parse(ddl_Type.SelectedValue);
            bll.Model.Sort = int.Parse(tbx_Sort.Text);
            bll.Add();
        }
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());

    }
}
