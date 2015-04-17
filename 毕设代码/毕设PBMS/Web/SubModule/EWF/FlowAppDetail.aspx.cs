using System;
using System.Web.UI;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using System.Web.UI.WebControls;

public partial class SubModule_EWF_FlowAppDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["AppID"] = Request.QueryString["AppID"] != null ? new Guid(Request.QueryString["AppID"]) : Guid.Empty;
            #endregion

            BindDropDown();

            if ((Guid)ViewState["AppID"] != Guid.Empty)
            {
                BindData();
            }
            else
            {
                //新增
                MCSTabControl1.Visible = false;
                bt_Copy.Visible = false;//若没有appID 则有些影藏
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((Guid)ViewState["AppID"] != Guid.Empty)
        {
            EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
            ddl_RelateDataObject.DataSource = app.GetDataObjectList();
            ddl_RelateDataObject.DataBind();
        }
        ddl_RelateDataObject.Items.Insert(0, new ListItem("请选择", Guid.Empty.ToString()));
    }
    #endregion

    private void BindData()
    {
        EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);

        if (app.Model != null)
        {
            #region 绑定基本信息
            lb_ID.Text = app.Model.ID.ToString();
            tbx_Name.Text = app.Model.Name;
            tbx_Code.Text = app.Model.Code;
            tbx_Description.Text = app.Model.Description;
            lbl_InsertSatff.Text = app.Model.InsertStaff == 0 ? "" : new Org_StaffBLL(app.Model.InsertStaff).Model.RealName;
            if (app.Model.InsertTime != new DateTime(1900, 1, 1)) lbl_InsertTime.Text = app.Model.InsertTime.ToShortDateString();
            tbx_Remark.Text = app.Model.Remark;
            lbl_UpdateStaff.Text = app.Model.UpdateStaff == 0 ? "" : new Org_StaffBLL(app.Model.UpdateStaff).Model.RealName;
            if (app.Model.UpdateTime != new DateTime(1900, 1, 1)) lbl_UpdateTime.Text = app.Model.UpdateTime.ToShortDateString();
            ddl_EnableFlag.SelectedValue = app.Model.EnableFlag;

            rbl_CanBatchApprove.SelectedValue = app.Model["CanBatchApprove"] == "Y" ? "Y" : "N";

            ddl_RelateDataObject.SelectedValue = app.Model["RelateDataObject"];
            tbx_RelateURL.Text = app.Model["RelateUrl"];

            if (tbx_RelateURL.Text == "")
                ddl_RelateObject.SelectedValue = "NULL";
            else if (ddl_RelateObject.Items.FindByValue(tbx_RelateURL.Text) != null)
            {
                ddl_RelateObject.SelectedItem.Selected = false;
                ddl_RelateObject.Items.FindByValue(tbx_RelateURL.Text).Selected = true;
            }
            else
                ddl_RelateObject.SelectedValue = "UserDefined";
            ddl_RelateObject_SelectedIndexChanged(null, null);
            #endregion
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        EWF_Flow_AppBLL app;
        if ((Guid)ViewState["AppID"] != Guid.Empty)
            app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]); //修改
        else
            //新增
            app = new EWF_Flow_AppBLL();

        #region 获取界面信息
        app.Model.Description = tbx_Description.Text;
        app.Model.Name = tbx_Name.Text;
        app.Model.Code = tbx_Code.Text;
        app.Model.Remark = tbx_Remark.Text;
        app.Model.EnableFlag = ddl_EnableFlag.SelectedValue;
        app.Model["RelateDataObject"] = ddl_RelateDataObject.SelectedValue;
        app.Model["RelateUrl"] = tbx_RelateURL.Text;
        app.Model["CanBatchApprove"] = rbl_CanBatchApprove.SelectedValue;
        #endregion

        if ((Guid)ViewState["AppID"] != Guid.Empty)
        {
            //修改
            app.Model.UpdateTime = DateTime.Now;
            app.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            
            if (app.Update() > 0)
            {
                MessageBox.Show(this, "更新记录保存成功!");
            }
        }
        else
        {
            //新增
            app.Model.InsertTime = DateTime.Now;
            app.Model.InsertStaff = int.Parse(Session["UserID"].ToString());
            if (app.Add() > 0)
            {
                MessageBox.Show(this, "新增记录保存成功!");
            }
        }
        Response.Redirect("FlowAppList.aspx");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
            Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 2)
            Response.Redirect("FlowDataObjectList.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 3)
            Response.Redirect("FlowInitPosition.aspx?AppID=" + ViewState["AppID"].ToString());
    }

    protected void ddl_RelateObject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_RelateObject.SelectedValue == "NULL")
        {
            ddl_RelateDataObject.SelectedValue = Guid.Empty.ToString();
            tbx_RelateURL.Text = "";//对文本框赋值
            ddl_RelateDataObject.Enabled = false;
            tbx_RelateURL.Enabled = false;
        }
        else if (ddl_RelateObject.SelectedValue == "UserDefined")//用户自定义
        {
            tbx_RelateURL.Text = "";
            ddl_RelateDataObject.Enabled = true;
            tbx_RelateURL.Enabled = true;
        }
        else
        {
            tbx_RelateURL.Text = ddl_RelateObject.SelectedValue;
            ddl_RelateDataObject.Enabled = true;
            tbx_RelateURL.Enabled = true;
        }
    }
    protected void bt_Copy_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["AppID"] != Guid.Empty)
        {
            EWF_Flow_AppBLL.Copy((Guid)ViewState["AppID"]);
            Response.Redirect("FlowAppList.aspx");
        }
    }
}
