using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.Model;
using MCSFramework.BLL.Pub;

public partial class SubModule_OA_Journal_JournalDetail_FDDJ : System.Web.UI.Page
{
    private MCSSelectControl select_RelateClient;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        select_RelateClient = (MCSSelectControl)pl_detail.FindControl("JN_Journal_RelateClient");
        select_RelateClient.SelectChange += new SelectChangeEventHandler(select_RelateClient_SelectChange);

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ID"] != null) ViewState["ID"] = Int32.Parse(Request.QueryString["ID"]);
            #endregion

            BindDropDown();

            if (ViewState["ID"] != null)
            {
                BindData();
            }
            else
            {
                ((TextBox)pl_detail.FindControl("JN_Journal_BeginTime")).Text = DateTime.Today.ToString("yyyy-MM-dd");

                //Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
                //tr_OfficialCity.SelectValue = staff.Model.OfficialCity.ToString();
                //tbx_IPAddress.Text = Request.UserHostAddress;


                bt_Delete.Visible = false;
                tr_comment.Visible = false;
                tr_uploadfile.Visible = false;
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }


    private void select_RelateClient_SelectChange(object sender, SelectChangeEventArgs e)
    {
        int _clientid = 0;
        if (!string.IsNullOrEmpty(select_RelateClient.SelectValue) && int.TryParse(select_RelateClient.SelectValue, out _clientid))
        {
            TextBox tbx_Address = (TextBox)pl_detail.FindControl("JN_Journal_Address");
            CM_ClientBLL _c = new CM_ClientBLL(_clientid);
            if (tbx_Address != null && _c.Model != null)
            {
                tbx_Address.Text = _c.Model.Address;
            }
        }
    }
    #endregion

    private void BindData()
    {
        JN_Journal m = new JN_JournalBLL((int)ViewState["ID"]).Model;
        pl_detail.BindData(m);

        if (m.InsertStaff != (int)Session["UserID"] || m.ApproveFlag == 1 || (DateTime.Today - m.InsertTime.Date).Days > 7)
        {
            pl_detail.SetControlsEnable(false);
            UploadFile1.CanDelete = false;
            UploadFile1.CanUpload = false;
            bt_OK.Visible = false;
            bt_Delete.Visible = false;
        }

        if ((DateTime.Today - m.InsertTime.Date).Days > 0) bt_Delete.Visible = false;       //只能删除当日的日志

        #region 展示附件
        UploadFile1.RelateID = (int)ViewState["ID"];
        UploadFile1.RelateType = 90;
        UploadFile1.BindGrid();
        #endregion

        lb_CommentCounts.Text = JN_JournalCommentBLL.GetModelList("JournalID=" + ViewState["ID"].ToString()).Count.ToString();
    }

    #region
    protected string FormatTxt(string content)
    {
        return (content.Replace(((char)13).ToString(), "<br>"));
    }
    #endregion

    #region 评论操作
    #region 显示评论内容
    protected void btn_LookComment_Click(object sender, EventArgs e)
    {
        table_comment.Visible = true;
        BindGridList();
    }
    #endregion

    #region 到数据库中查出评论内容
    private void BindGridList()
    {
        dgshow.DataSource = JN_JournalCommentBLL.GetModelList("JournalID=" + ViewState["ID"].ToString());
        dgshow.DataBind();
    }
    #endregion

    #region 取消发表评论的内容
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        tbx_content.Text = "";
    }
    #endregion

    #region 向数据库中添加一条新的评论内容
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        JN_JournalCommentBLL commentbll = new JN_JournalCommentBLL();
        commentbll.Model.JournalID = Convert.ToInt32(ViewState["ID"]);
        commentbll.Model.Staff = int.Parse(Session["UserID"].ToString());
        commentbll.Model.Content = tbx_content.Text;
        commentbll.Model.CommentTime = DateTime.Now;
        commentbll.Add();

        BindGridList();

        //清空评论板内的内容
        tbx_content.Text = "";
    }
    #endregion

    protected void dgshow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgshow.PageIndex = e.NewPageIndex;
        BindGridList();
    }

    #endregion

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        JN_JournalBLL _bll = null;
        if (ViewState["ID"] == null)
        {
            _bll = new JN_JournalBLL();
        }
        else
        {
            _bll = new JN_JournalBLL((int)ViewState["ID"]);
        }

        pl_detail.GetData(_bll.Model);

        _bll.Model.Title = "拜访医院:"+select_RelateClient.SelectText;
        _bll.Model.EndTime = _bll.Model.BeginTime;
        
        #region 判断必填项
        if (_bll.Model["SynergeticStaff"] == "")
        {
            MessageBox.Show(this, "辅导与代教员工必填!");
            return;
        }
        #endregion


        if (ViewState["ID"] == null)
        {
            _bll.Model.Staff = (int)Session["UserID"];
            _bll.Model.OrganizeCity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
            _bll.Model.JournalType = 4;
            _bll.Model.WorkingClassify = 1;
            _bll.Model.ApproveFlag = 2;
            _bll.Model["HasSynergeticStaff"] = "1";
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model["IPAddress"] = Request.UserHostAddress;
            ViewState["ID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }

        if (sender != null)
            MessageBox.ShowAndRedirect(this, "日志保存成功！", "JournalList_FDDJ.aspx");//.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_ListView_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalList_FDDJ.aspx");
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if (ViewState["ID"] != null)
        {
            JN_JournalBLL bll = new JN_JournalBLL((int)ViewState["ID"]);
            bll.Delete();
            MessageBox.ShowAndRedirect(this, "日志删除成功!", "JournalCalendar.aspx");
        }
    }
}
