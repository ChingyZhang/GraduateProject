// ===================================================================
// 文件路径:SubModule/RM/OfficeDetail.aspx.cs 
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
using System.IO;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;

public partial class SubModule_OC_OfficeDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            #endregion

            BindDropDown();
            //Session["MCSMenuControl_FirstSelectIndex"] = "17";

            if (ViewState["ClientID"] != null)
                BindData();
            else if (Request.QueryString["Mode"] == "New")
            {
                //新增门店时的初始值
                DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
                ddl_ActiveFlag.SelectedValue = "1";

                TextBox tbx_OpenTime = (TextBox)pl_detail.FindControl("CM_Client_OpenTime");
                tbx_OpenTime.Text = DateTime.Today.ToString("yyyy-MM-dd");

                bt_Approve.Visible = false;
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在‘仓库列表’中选择要查看的仓库！", "StoreList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }


        #region 给活跃标志加事件
        DropDownList ddl_ActiveFlag_1 = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        ddl_ActiveFlag_1.AutoPostBack = true;
        ddl_ActiveFlag_1.SelectedIndexChanged += new EventHandler(ddl_ActiveFlag_SelectedIndexChanged);
        #endregion

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }

    void ddl_ActiveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        if (ddl_ActiveFlag.SelectedValue == "2")
        {
            ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = "";
        }
    }
    #endregion

    private void BindData()
    {
        lb_ID.Text = ViewState["ClientID"].ToString();

        CM_Client m = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        pl_detail.BindData(m);

        if (m.ApproveFlag == 1)
        {
            bt_Approve.Visible = false;
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        if (ViewState["ClientID"] == null)
        {
            _bll = new CM_ClientBLL();
        }
        else
        {
            _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "所属的管理片区必填!");
            return;
        }
        if (_bll.Model.OfficalCity == 0)
        {
            MessageBox.Show(this, "所属的行政城市必填!");
            return;
        }
        if (_bll.Model.OrganizeCity > 1 && _bll.Model.Supplier == 0)
        {
            MessageBox.Show(this, "供货商信息必填!");
            return;
        }
        #endregion

        #region 判断活跃标志
        if (_bll.Model.ActiveFlag == 1 && _bll.Model.CloseTime != new DateTime(1900, 1, 1))
            _bll.Model.CloseTime = new DateTime(1900, 1, 1);

        if (_bll.Model.ActiveFlag == 2 && _bll.Model.CloseTime == new DateTime(1900, 1, 1))
            _bll.Model.CloseTime = DateTime.Now;
        #endregion

        if (ViewState["ClientID"] == null)
        {
            _bll.Model.ClientType = 1;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ClientID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }

        MessageBox.ShowAndRedirect(this, "保存仓库资料成功！", "StoreDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());

    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            _bll.Model.ApproveFlag = 1;
            _bll.Update();
            MessageBox.ShowAndRedirect(this, "审核仓库资料成功！", "StoreDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }

}
