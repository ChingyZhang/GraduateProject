// ===================================================================
// 文件路径:SubMoudle/CM/FindCodition/FindCondition_Detail.aspx.cs 
// 生成日期:2008/3/1 21:45:07 
// 作者:	  Shen Gang
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
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
public partial class SubMoudle_FindCondition_FindCondition_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                if (Request.QueryString["Panel"] != null && ddl_PanelList.Items.FindByValue(Request.QueryString["Panel"]) != null)
                    ddl_PanelList.SelectedValue = Request.QueryString["Panel"];
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_PanelList.DataSource = UD_PanelBLL.GetModelList("AdvanceFind='Y'");
        ddl_PanelList.DataBind();
        ddl_PanelList.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    #endregion

    private void BindData()
    {
        ADFind_FindCondition _findcondition = new ADFind_FindConditionBLL((int)ViewState["ID"]).Model;
        #region 绑定基本信息
        lb_ID.Text = _findcondition.ID.ToString();
        tbx_Name.Text = _findcondition.Name;
        tbx_ConditionText.Text = _findcondition.ConditionText;
        tbx_ConditionValue.Text = _findcondition.ConditionValue;
        tbx_ConditionSQL.Text = _findcondition.ConditionSQL;
        if (_findcondition.CreateDate != new DateTime(1900, 1, 1)) tbx_CreateDate.Text = _findcondition.CreateDate.ToString();
        ddl_IsPublic.SelectedValue = _findcondition.IsPublic;

        ddl_PanelList.SelectedValue = _findcondition.Panel.ToString();
        ddl_PanelList.Enabled = false;
        #endregion
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if (ddl_PanelList.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择所属Panel！");
            return;
        }
        ADFind_FindConditionBLL _findconditionbll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _findconditionbll = new ADFind_FindConditionBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _findconditionbll = new ADFind_FindConditionBLL();
            _findconditionbll.Model.CreateDate = DateTime.Now;
            _findconditionbll.Model.OpStaff = int.Parse(Session["UserID"].ToString());
        }

        #region 获取界面信息
        _findconditionbll.Model.Name = tbx_Name.Text;
        _findconditionbll.Model.ConditionText = tbx_ConditionText.Text;
        _findconditionbll.Model.ConditionValue = tbx_ConditionValue.Text;
        _findconditionbll.Model.ConditionSQL = tbx_ConditionSQL.Text;
        _findconditionbll.Model.Panel = new Guid(ddl_PanelList.SelectedValue);
        _findconditionbll.Model.IsPublic = ddl_IsPublic.SelectedValue;

        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            if (_findconditionbll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "更新记录保存成功!", "FindCondition_List.aspx");
            }
        }
        else
        {
            //新增
            ViewState["ID"] = _findconditionbll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增记录保存成功!", "FindCondition_List.aspx");
            }
        }
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        ADFind_FindConditionBLL _findconditionbll;
        if ((int)ViewState["ID"] != 0)
        {
            _findconditionbll = new ADFind_FindConditionBLL((int)ViewState["ID"]);
            try
            {
                _findconditionbll.Delete();
                MessageBox.ShowAndRedirect(this, "删除条件记录成功!", "FindCondition_List.aspx");
            }
            catch
            {
                MessageBox.Show(this, "由于该查询条件正在系统中使用,暂时无法删除!");
            }
        }
    }

}