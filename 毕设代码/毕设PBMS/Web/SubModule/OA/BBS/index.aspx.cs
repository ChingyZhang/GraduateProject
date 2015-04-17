using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.BLL;
using MCSFramework.Model;
using System.Web.Security;

public partial class SubModule_OA_BBS_index : System.Web.UI.Page
{
    protected static bool Admin = false;
    protected int count = 0;
    public static string username = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            username = (string)Session["UserName"];

            #region 页面设置
            labOnLine.Text = Membership.GetNumberOfUsersOnline().ToString();
            lbForumItemCount.Text = BBS_ForumItemBLL.GetModelList("").Count.ToString();
            lbReplyCount.Text = BBS_ForumReplyBLL.GetModelList("").Count.ToString();
            lbNowForumItemCount.Text = BBS_ForumItemBLL.GetModelList(" (datediff(d,BBS_ForumItem.SendTime,getdate())< 1)").Count.ToString();
            lbUserCount.Text = Membership.GetAllUsers().Count.ToString();
            lbLastTime.Text = Membership.GetUser().LastLoginDate.ToString();
            lbYesterdayForumItemCount.Text = (BBS_ForumItemBLL.GetModelList(" (datediff(d,BBS_ForumItem.SendTime,getdate())< 2)").Count - BBS_ForumItemBLL.GetModelList(" (datediff(d,BBS_ForumItem.SendTime,getdate())< 1)").Count).ToString();
            lbTopDayForumItemCount.Text = new BBS_ForumItemBLL().GetTopDayForumItemCount().ToString();
            if (username != "")
            {
                lbUser.Text = username;
                IList<BBS_ForumItem> forumItemList = BBS_ForumItemBLL.GetModelList("");
                int sum = 0;
                foreach (BBS_ForumItem forumItem in forumItemList)
                {
                    if (forumItem.Sender == username)
                        sum++;
                }
                lbForumItem.Text = sum.ToString();

                if (username == "admin")
                    lbRole.Text = "管理员";
                else
                {
                    lbRole.Text = "会员";
                    IList<BBS_BoardUserMember> userList = BBS_BoardUserMemberBLL.GetModelList("");
                    foreach (BBS_BoardUserMember user in userList)
                    {
                        if (user.Role == 1 && user.UserName == username)
                            lbRole.Text = "板块版主";
                    }
                }
            }
            else
            {
                lbUser.Text = "公司论坛";
                lbForumItem.Text = "0";
                lbRole.Text = "游客";
            }
            #endregion

            PopulateData();
        }

    }

    #region 显示数据
    /// <summary>
    /// 显示数据
    /// </summary>
    private void PopulateData()
    {
        if (Roles.IsUserInRole("论坛管理员"))
            Admin = true;
        else
            Admin = false;

        bt_Insert.Visible = Admin;

        #region 初始化数据
        DataTable dataTable_catalog = new DataTable();
        DataTable dataTable_board = new DataTable();
        DataTable dataTable_boardmaster = new DataTable();
        DataTable dataTable_boardmember = new DataTable();
        DataSet ds = new DataSet();
        BBS_CatalogBLL catalogbll = new BBS_CatalogBLL();//分类
        BBS_BoardBLL boardbll = new BBS_BoardBLL();//板块
        BBS_BoardUserMemberBLL boardUserMemberbll = new BBS_BoardUserMemberBLL();//斑竹信息
        #endregion

        //得到分类信息 
        dataTable_catalog = catalogbll.GetAllCatalog("");
        dataTable_catalog.TableName = "catalogTable";
        ds.Tables.Add(dataTable_catalog);

        if (dataTable_catalog.Rows.Count > 0)
        {
            //得到板块信息 
            dataTable_board = boardbll.GetIndexInfo();
            dataTable_board.TableName = "boardTable";
            ds.Tables.Add(dataTable_board);

            //得到斑竹信息
            dataTable_boardmaster = boardUserMemberbll.GetAllBoardUserMember(" Role=1");
            dataTable_boardmaster.TableName = "boardmasterTable";
            ds.Tables.Add(dataTable_boardmaster);

            //得到成员信息
            dataTable_boardmember = UserBLL.GetOnlineUserList();
            dataTable_boardmember.TableName = "boardmemberTable";
            ds.Tables.Add(dataTable_boardmember);

            //对子表进行数据绑定
            ds.Relations.Add("catalog_board", ds.Tables["catalogTable"].Columns["ID"], ds.Tables["boardTable"].Columns["Catalog"], false);
            ds.Relations.Add("board_boardmaster", ds.Tables["boardTable"].Columns["ID"], ds.Tables["boardmasterTable"].Columns["Board"], false);


            if (!string.IsNullOrEmpty(Request.QueryString["Catalog"]))
                ds.Tables["catalogTable"].DefaultView.RowFilter = "ID=" + Request.QueryString["Catalog"];

            rpt_catalog.DataSource = ds.Tables["catalogTable"].DefaultView;
            rpt_catalog.DataBind();
            rpt_boardmember.DataSource = ds.Tables["boardmemberTable"].DefaultView;
            rpt_boardmember.DataBind();
            Page.DataBind();
        }
    }

    #endregion


    #region 获取在线会员的论坛权限
    protected string GetOnLineRight(string name)
    {
        string url = "";
        IList<BBS_BoardUserMember> boardUserMemberList = BBS_BoardUserMemberBLL.GetModelList("");
        if (Roles.IsUserInRole("论坛管理员"))
        {
            url = "<img src='../../../Images/online_admin.gif'>";
        }
        else
        {
            url = "<img src='../../../Images/online_member.gif'>";
            foreach (BBS_BoardUserMember boardUserMember in boardUserMemberList)
            {

                if (boardUserMember.Role == 1 && boardUserMember.UserName == name)
                    url = "<img src='../../../Images/online_moderator.gif'>";
            }
        }
        return url;
    }
    #endregion


    #region 删除板块

    public void DeleteBoard(object sender, System.EventArgs e)
    {
        BBS_BoardBLL boardbll = new BBS_BoardBLL();
        try
        {
            boardbll.DeleteBoard(Int32.Parse(((LinkButton)sender).CommandArgument));
            boardbll = null;
            MessageBox.ShowAndRedirect(this, "删除成功！", "index.aspx");
        }
        catch (Exception ex)
        {
            MessageBox.ShowAndRedirect(this, "删除出错！", "index.aspx");
        }
    }
    #endregion

    #region 删除分类
    protected void DeleteCatalog(object sender, System.EventArgs e)
    {
        BBS_CatalogBLL catalogbll = new BBS_CatalogBLL();
        try
        {
            catalogbll.DeleteCatalog(Int32.Parse(((LinkButton)sender).CommandArgument));
            catalogbll = null;
            MessageBox.ShowAndRedirect(this, "删除成功！", "index.aspx");
        }
        catch (Exception ex)
        {
            MessageBox.ShowAndRedirect(this, "删除出错！", "index.aspx");
        }
    }
    #endregion

    protected void rpt_board_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        WebControl ctl = (WebControl)e.Item.FindControl("lbtnDelBoard");
        if (ctl != null)
        {

            ctl.Attributes["onclick"] = "return confirm('确定删除吗?')";
        }
    }

    protected void bt_Insert_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatalogManage.aspx");
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search/index.aspx");
    }
}
