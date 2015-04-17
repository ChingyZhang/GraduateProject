using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model.EWF;
using MCSFramework.UD_Control;
using System.Collections.Specialized;

public partial class SubModule_EWF_Apply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ViewState["TaskID"] = Request.QueryString["TaskID"] == null ? 0 : int.Parse(Request.QueryString["TaskID"]);

            if ((int)ViewState["TaskID"] != 0)
            {
                BindData();
            }
            else
            {
                if (Request.QueryString["AppID"] == null)
                {
                    MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../Login/index.aspx");
                    return;
                }
                ViewState["AppID"] = new Guid(Request.QueryString["AppID"]);

                EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"], false);
                if (app.Model.RelateBusiness == "Y")
                {
                    MessageBox.ShowAndRedirect(this, "对不起，该流程不能直接发起，必须在业务系统中发起！", "FlowAppList.aspx");
                    return;
                }

                lbl_Applyer.Text = Session["UserRealName"].ToString();
                lbl_AppName.Text = app.Model.Name;

                lb_StartTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                lb_EndTime.Text = "未结束";

                hyl_RelateURL.Visible = false;
            }
        }
    }



    //Submit the Apply
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        EWF_TaskBLL task;
        if ((int)ViewState["TaskID"] == 0)
        {
            task = new EWF_TaskBLL();
            task.Model.App = (Guid)ViewState["AppID"];
            task.Model.Initiator = (int)Session["UserID"];
            task.Model.Title = tbx_Topic.Text;
            task.Model.DataObjectValues = pl_dataobjectinfo.GetData();
            task.Add();
        }
        else
        {
            task = new EWF_TaskBLL((int)ViewState["TaskID"]);
            task.SetDataObjectValue(pl_dataobjectinfo.GetData());
        }
        task.Start();

        MessageBox.ShowAndRedirect(this, "流程已成功发起！！", "TaskDetail.aspx?TaskID=" + task.Model.ID.ToString());
    }

    //Bind Basic Info
    private void BindData()
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);

        //绑定流程信息
        lbl_Applyer.Text = new Org_StaffBLL(task.Model.Initiator).Model.RealName;
        lbl_AppName.Text = new EWF_Flow_AppBLL(task.Model.App).Model.Name;
        tbx_Topic.Text = task.Model.Title;

        lb_StartTime.Text = task.Model.StartTime.ToString();
        if (task.Model.EndTime != new DateTime(1900, 1, 1))
            lb_EndTime.Text = task.Model.EndTime.ToString();
        else
            lb_EndTime.Text = "未结束";

        if (task.Model.RelateURL != "")
        {
            hyl_RelateURL.NavigateUrl = task.Model.RelateURL;
            hyl_RelateURL.Visible = true;
        }
        else
            hyl_RelateURL.Visible = false;

        //Bind the dataobject info
        NameValueCollection dataobjects = task.GetDataObjectValue();
        pl_dataobjectinfo.BindData(dataobjects);

    }

}
