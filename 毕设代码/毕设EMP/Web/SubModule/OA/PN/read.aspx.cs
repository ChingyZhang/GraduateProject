using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Web.Profile;
using System.IO;
using MCSFramework.Model;

public partial class SubModule_OA_PN_readter : System.Web.UI.Page
{
    protected int insertstaff;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //公告编号
            ViewState["ID"] = Request.QueryString["ID"] != null ? Request.QueryString["ID"].ToString() : "";
            //登陆的员工
            //staff = int.Parse(Session["UserID"].ToString());
            //记录下阅读过此公告的员工
            AddPN_HasReadStaffInfo();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        int approveflag;
        string CanComment;

        #region 显示公告主题内容
        PN_NoticeBLL noticebll = new PN_NoticeBLL(Convert.ToInt32(ViewState["ID"]));
        lbl_Topic.Text = noticebll.Model.Topic.ToString();
        lbl_content.Text = noticebll.Model.Content.ToString();

        PN_HasReadStaffBLL hasReadStaffbll = new PN_HasReadStaffBLL();
        PN_CommentBLL commentbll = new PN_CommentBLL();
        lab_hasRead.Text = hasReadStaffbll.GetReadCountByNotice(Convert.ToInt32(ViewState["ID"])).ToString();
        lab_comment.Text = commentbll.GetCommentCountByNotice(Convert.ToInt32(ViewState["ID"])).ToString();

        insertstaff = int.Parse(noticebll.Model.InsertStaff.ToString());
        lbl_InsertStaffName.Text = new Org_StaffBLL(insertstaff).Model.RealName;
        lbl_InsertTime.Text = noticebll.Model.InsertTime.ToString();
        approveflag = noticebll.Model.ApproveFlag;            //是否可以添加评论
        CanComment = noticebll.Model.CanComment;              //审核标志
        ViewState["Catalog"] = noticebll.Model["Catalog"];
        #endregion

        #region 展示附件
        UploadFile1.RelateID = Convert.ToInt32(ViewState["ID"]);
        UploadFile1.BindGrid();
        #endregion

        #region 初始化时折叠评论内容和评论板
        //UpdatePanel1.Visible = false;
        #endregion

        #region 判断此公告是否可以评论
        if (CanComment == "N")
        {
            btn_LookComment.Visible = false;
        }
        #endregion
    }

    #region 向数据库中添加一条新的评论内容
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        PN_CommentBLL commentbll = new PN_CommentBLL();
        commentbll.Model.Notice = Convert.ToInt32(ViewState["ID"]);
        commentbll.Model.Staff = int.Parse(Session["UserID"].ToString());
        commentbll.Model.Content = content.Text;
        commentbll.Model.CommentTime = DateTime.Now;
        commentbll.Add();

        BindGridList();

        //清空评论板内的内容
        content.Text = "";
    }
    #endregion

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
        PN_CommentBLL commentbll = new PN_CommentBLL();
        dgshow.DataSource = commentbll.GetUserList(Convert.ToInt32(ViewState["ID"]));
        dgshow.DataBind();
    }
    #endregion

    #region 向数据库中添加查看公告的人，记录下阅读公告的人及具体时间
    public void AddPN_HasReadStaffInfo()
    {
        PN_HasReadStaffBLL readbll = new PN_HasReadStaffBLL();
        readbll.Model.Notice = Convert.ToInt32(ViewState["ID"]);
        readbll.Model.Staff = int.Parse(Session["UserID"].ToString());
        readbll.Model.ReadTime = DateTime.Now;
        readbll.Add();
    }
    #endregion

    #region 返回公告主页面
    protected void btn_return_Click(object sender, System.EventArgs e)
    {
        if ((string)ViewState["Catalog"] != "")
            Response.Redirect("index.aspx?Catalog=" + (string)ViewState["Catalog"]);
        else
            Response.Redirect("index.aspx");
    }
    #endregion

    #region 取消发表评论的内容
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        content.Text = "";
    }
    #endregion

    #region 转换发布者或回复者的全称信息
    protected string DisplayFullInfo(string loginname)
    {
        string ret = "";
        Org_Staff staff = UserBLL.GetStaffByUsername(loginname);

        ret = "登录名称：<a href=\"javascript:SendMsg('" + loginname + "','" + Server.UrlPathEncode(staff.RealName) + "');\" title='发送短内短信' style='color:red' target='_self'>" + loginname + "</a>";
        ret += "<br/>真实姓名：" + staff.RealName + "<br/>所属区域：" + TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name", "SuperID", 0, staff.OrganizeCity);

        Org_Position p = new Org_PositionBLL(staff.Position).Model;
        if (p != null)
            ret += "<br/>职位：" + p.Name;
        return ret;
    }
    #endregion

    protected void PageCut_click(object sender, EventArgs e)
    {
        string commandarg = ((LinkButton)sender).CommandArgument;
        switch (commandarg)
        {
            case "First":
                dgshow.PageIndex = 0;
                break;
            case "Next":
                dgshow.PageIndex = (int)Math.Max(0, dgshow.PageIndex + 1);
                break;
            case "Piv":
                dgshow.PageIndex = (int)Math.Max(0, dgshow.PageIndex - 1);
                break;
            case "Last":
                dgshow.PageIndex = (int)Math.Max(0, dgshow.PageCount);
                break;
        }
        BindGridList();
    }

    protected void dgshow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgshow.PageIndex = e.NewPageIndex;
        BindGridList();
    }

    #region
    protected string FormatTxt(string content)
    {
        return (content.Replace(((char)13).ToString(), "<br>"));
    }
    #endregion
}
