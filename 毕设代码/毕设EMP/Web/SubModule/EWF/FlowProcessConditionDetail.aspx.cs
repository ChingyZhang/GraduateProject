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

public partial class SubModule_EWF_FlowProcessConditionDetail : System.Web.UI.Page
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
        ddl_Type.SelectedValue = "4";

        ddl_OperatorType.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_OperatorType");
        ddl_OperatorType.DataBind();

        ddl_DataObject_Condition.DataSource = app.GetDataObjectList();
        ddl_DataObject_Condition.DataBind();
        ddl_DataObject_Condition.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));


        ddl_TrueNextProcess.DataSource = app.GetProcessList();
        ddl_TrueNextProcess.DataBind();
        ddl_TrueNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_FalseNextProcess.DataSource = app.GetProcessList();
        ddl_FalseNextProcess.DataBind();
        ddl_FalseNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

    }
    #endregion

    private void BindData()
    {
        this.lb_ID.Text = ViewState["ProcessID"].ToString();
        EWF_Flow_ProcessConditionBLL bll = new EWF_Flow_ProcessConditionBLL((Guid)ViewState["ProcessID"]);

        #region 绑定基本信息
        tbx_Name.Text = bll.Model.Name;
        tbx_Description.Text = bll.Model.Description;
        ddl_DefaultNextProcess.SelectedValue = bll.Model.DefaultNextProcess.ToString();
        ddl_Type.SelectedValue = bll.Model.Type.ToString();
        tbx_Sort.Text = bll.Model.Sort.ToString();

        ddl_OperatorType.SelectedValue = bll.Model.OperatorType.ToString();
        ddl_DataObject_Condition.SelectedValue = bll.Model.DataObject.ToString();
        tbx_Value1.Text = bll.Model.Value1;
        tbx_Value2.Text = bll.Model.Value2;
        ddl_TrueNextProcess.SelectedValue = bll.Model.TrueNextProcess.ToString();
        ddl_FalseNextProcess.SelectedValue = bll.Model.FalseNextProcess.ToString();
        #endregion
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] != Guid.Empty)//修改
        {
            EWF_Flow_ProcessConditionBLL bll = new EWF_Flow_ProcessConditionBLL((Guid)ViewState["ProcessID"]);

            bll.Model.DataObject = new Guid(ddl_DataObject_Condition.SelectedValue);
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);

            bll.Model.TrueNextProcess = new Guid(ddl_TrueNextProcess.SelectedValue);
            bll.Model.FalseNextProcess = new Guid(ddl_FalseNextProcess.SelectedValue);
            bll.Model.Name = tbx_Name.Text;
            if (!string.IsNullOrEmpty(ddl_OperatorType.SelectedValue)) bll.Model.OperatorType = int.Parse(ddl_OperatorType.SelectedValue);

            bll.Model.Value1 = tbx_Value1.Text;
            bll.Model.Value2 = tbx_Value2.Text;
            bll.Update();

        }
        else//新增
        {
            EWF_Flow_ProcessConditionBLL bll = new EWF_Flow_ProcessConditionBLL();
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.DataObject = new Guid(ddl_DataObject_Condition.SelectedValue);
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            if (!string.IsNullOrEmpty(ddl_Type.SelectedValue)) bll.Model.Type = int.Parse(ddl_Type.SelectedValue);
            bll.Model.Sort = int.Parse(tbx_Sort.Text);
            bll.Model.Type = 4;

            bll.Model.TrueNextProcess = new Guid(ddl_TrueNextProcess.SelectedValue);
            bll.Model.FalseNextProcess = new Guid(ddl_FalseNextProcess.SelectedValue);
            bll.Model.Name = tbx_Name.Text;
            if (!string.IsNullOrEmpty(ddl_OperatorType.SelectedValue)) bll.Model.OperatorType = int.Parse(ddl_OperatorType.SelectedValue);
            bll.Model.Value1 = tbx_Value1.Text;
            bll.Model.Value2 = tbx_Value2.Text;
            bll.Add();
        }

        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        new EWF_Flow_ProcessConditionBLL((Guid)ViewState["ProcessID"]).Delete();
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }
}
