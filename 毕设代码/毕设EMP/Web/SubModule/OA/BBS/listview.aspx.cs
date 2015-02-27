using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.Common;
using System.Web.Security;

public partial class SubModule_OA_BBS_listview : System.Web.UI.Page
{
    protected int BoardID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["Board"] = Request.QueryString["Board"] == null ? 0 : int.Parse(Request.QueryString["Board"]);  //所属公告
            this.BoardID = Convert.ToInt32(ViewState["Board"]);
            ViewState["PageIndex"] = 0;
            BindGrid();
            BindTree(tr_Catalog.Nodes);
            ViewState["ConditionString"] = " BBS_ForumItem.ID in (select top 10 BBS_ForumItem.ID from MCS_OA.dbo.BBS_ForumItem where (datediff(d,BBS_ForumItem.SendTime,getdate())< 1) order by BBS_ForumItem.HitTimes desc)";
            BindHotForumItem();
            if (ConfigHelper.GetConfigString("BBSEditBoard").Contains(ViewState["Board"].ToString()) && new BBS_BoardUserMemberBLL().GetUserRoleByBoard((int)ViewState["Board"],Session["UserName"].ToString()) != 1)
            {
                bt_NewItem.Visible = false;
                bt_Delete.Visible = false;
            }
        }
    }

    private void BindTree(TreeNodeCollection TNC)
    {
        IList<BBS_Catalog> _list = BBS_CatalogBLL.GetModelList("");
        foreach (BBS_Catalog _m in _list)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _m.Name;
            tn.Value = "0";
            TNC.Add(tn);
            int isExpanded = BindChildTree(tn.ChildNodes, _m.ID);
            if (isExpanded != 0)
                tn.Expanded = true;
        }
    }

    private int BindChildTree(TreeNodeCollection TNC, int SuperID)
    {
        int isExpanded = 0;
        BBS_BoardBLL boardbll = new BBS_BoardBLL((int)ViewState["Board"]);
        string selectName = boardbll.Model.Name.ToString();
        IList<BBS_Board> _list = BBS_BoardBLL.GetModelList(" Catalog= " + SuperID.ToString());
        foreach (BBS_Board _m in _list)
        {
            bool flag = false;
            IList<BBS_BoardUserMember> userList = new BBS_BoardUserMemberBLL()._GetModelList(" Board=" + _m.ID);
            foreach (BBS_BoardUserMember user in userList)
            {
                if (user.UserName == (string)Session["UserName"])
                    flag = true;
            }
            if (_m.IsPublic == "1" || (_m.IsPublic == "2" && flag))
            {
                TreeNode tn = new TreeNode();
                tn.Text = _m.Name;
                if (selectName.Equals(_m.Name) == true)
                {
                    tn.Selected = true;
                    isExpanded = 1;
                }
                tn.Value = _m.ID.ToString();
                TNC.Add(tn);
            }
        }
        return isExpanded;
    }

    private void BindHotForumItem()
    {
        if (ViewState["ConditionString"] != null)
            gv_HotForumItem.ConditionString = ViewState["ConditionString"].ToString();
        gv_HotForumItem.Columns[1].Visible = false;
        gv_HotForumItem.Columns[2].Visible = false;
        gv_HotForumItem.BindGrid();

    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        ViewState["ConditionString"] = " BBS_ForumItem.ID in (select top 10 BBS_ForumItem.ID from MCS_OA.dbo.BBS_ForumItem";
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                ViewState["ConditionString"] = ViewState["ConditionString"] + " where (datediff(d,BBS_ForumItem.SendTime,getdate())< 1) order by BBS_ForumItem.HitTimes desc)";
                break;
            case "1":
                ViewState["ConditionString"] = ViewState["ConditionString"] + " where (datediff(d,BBS_ForumItem.SendTime,getdate())< 7) order by BBS_ForumItem.HitTimes desc)";
                break;
            case "2":
                ViewState["ConditionString"] = ViewState["ConditionString"] + " where (datediff(d,BBS_ForumItem.SendTime,getdate())< 30) order by BBS_ForumItem.HitTimes desc)";
                break;
            case "3":
                ViewState["ConditionString"] = ViewState["ConditionString"] + " order by BBS_ForumItem.HitTimes desc)  ";
                break;
        }
        BindHotForumItem();
    }

    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl2.SelectedTabItem.Value)
        {
            case "0":
                dgrd_Result.ConditionString = " BBS_ForumItem.Board =" + ViewState["Board"].ToString() + "  order by BBS_ForumItem.ExtPropertys desc,BBS_ForumItem.LastReplyTime desc";
                break;
            case "1":
                dgrd_Result.ConditionString = " BBS_ForumItem.Board =" + ViewState["Board"].ToString() + " and ExtPropertys like '%|Y|%' order by ExtPropertys desc,LastReplyTime desc";
                break;
        }
        dgrd_Result.PageIndex = 0;
        dgrd_Result.BindGrid();
    }

    protected void tr_Catalog_SelectedNodeChanged(object sender, EventArgs e)
    {
        tr_Catalog.HoverNodeStyle.Font.Underline = true;
        if (int.Parse(this.tr_Catalog.SelectedNode.Value) != 0)
        {
            ViewState["Board"] = int.Parse(this.tr_Catalog.SelectedNode.Value);
            BindGrid();
        }
        else
            Response.Redirect("index.aspx");
    }

    private void BindGrid()
    {
        #region 列隐藏
        dgrd_Result.Columns[2].Visible = false;
        dgrd_Result.Columns[4].Visible = false;
        dgrd_Result.Columns[7].Visible = false;
        dgrd_Result.Columns[9].Visible = false;
        dgrd_Result.Columns[10].Visible = false;
        dgrd_Result.Columns[11].Visible = false;
        dgrd_Result.Columns[13].Visible = false;
        #endregion
        #region 权限判断
        IList<BBS_BoardUserMember> boardUserList = BBS_BoardUserMemberBLL.GetModelList("");
        if (Roles.IsUserInRole("论坛管理员"))
        {
            bt_Delete.Visible = true;
            bt_NewItem.Visible = true;
            dgrd_Result.Columns[13].Visible = true;
        }
        else if (boardUserList != null)
        {
            foreach (BBS_BoardUserMember user in boardUserList)
            {
                bt_Delete.Visible = false;
                if (new BBS_BoardBLL((int)ViewState["Board"]).Model.IsPublic == "1" && user.Role == 1 && (string)Session["UserName"] == user.UserName)
                {
                    bt_Delete.Visible = true;
                    bt_NewItem.Visible = true;
                    dgrd_Result.Columns[13].Visible = true;
                    break;

                }
                else if ((string)Session["UserName"] == user.UserName && new BBS_BoardBLL((int)ViewState["Board"]).Model.IsPublic == "2")
                {
                    if (user.Role == 1)
                    {
                        bt_Delete.Visible = true;
                        bt_NewItem.Visible = true;
                        dgrd_Result.Columns[13].Visible = true;
                        break;
                    }
                    else if (user.Role == 2)
                    {
                        bt_NewItem.Visible = true;
                        bt_Delete.Visible = false;
                    }
                    else
                    {
                        bt_Delete.Visible = false;
                        bt_NewItem.Visible = false;
                    }
                }
            }
        }
        else
            bt_Delete.Visible = false;
        #endregion
        if (ViewState["PageIndex"] != null)
        {
            dgrd_Result.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }
        dgrd_Result.ConditionString = " BBS_ForumItem.Board =" + ViewState["Board"].ToString() + "  order by BBS_ForumItem.ExtPropertys desc,BBS_ForumItem.LastReplyTime desc";
        dgrd_Result.BindGrid();
        BBS_BoardBLL boardbll = new BBS_BoardBLL((int)ViewState["Board"]);
        lblCatalog.Text = new BBS_CatalogBLL(boardbll.Model.Catalog).Model.Name;
        lblBoardName.Text = boardbll.Model.Name;
        DataTable dataTable_boardmaster = new DataTable();
        DataSet ds = new DataSet();
        BBS_BoardUserMemberBLL boardUserMemberbll = new BBS_BoardUserMemberBLL();
        dataTable_boardmaster = boardUserMemberbll.GetAllBoardUserMember(" Role=1 and Board=" + (int)ViewState["Board"]);
        dataTable_boardmaster.TableName = "boardmasterTable";
        ds.Tables.Add(dataTable_boardmaster);
        rpt_boardmaster.DataSource = ds.Tables["boardmasterTable"].DefaultView;
        rpt_boardmaster.DataBind();

    }

    protected string GetType(int id)
    {
        BBS_ForumItemBLL forumItembll = new BBS_ForumItemBLL(id);
        //if (forumItembll.Model.IsBoardNotice == "Y" || forumItembll.Model.IsPublicNotice == "Y")
        //{
        //    if (forumItembll.Model.IsBoardNotice == "Y")
        //        return "<img src='../../../images/bulletin.gif'>";
        //    else
        //        return "<img src='../../../images/sysbulletin.gif'>";
        //}
      
            if (forumItembll.Model.HitTimes > 5)
                return "<img src='../../../images/hotfolder.gif'>";
            else
                return "<img src='../../../images/folder.gif'>";
        
    }

    protected string GetTypeIsTop(int id)
    {
        BBS_ForumItemBLL forumItembll = new BBS_ForumItemBLL(id);
        if (forumItembll.Model.ExtPropertys != null)
        {
            if (forumItembll.Model["IsTop"] == "Y")
                return "<img src='../../../images/istop.jpg'>";
            else
                return "";
        }
        else
        {
            return "";
        }
    }

    protected string GetTypeIsPith(int id)
    {
        BBS_ForumItemBLL forumItembll = new BBS_ForumItemBLL(id);
        if (forumItembll.Model.ExtPropertys != null)
        {
            if (forumItembll.Model["IsPith"] == "Y")
                return "<img src='../../../images/pith.jpg'>";
            else
                return "";
        }
        else
        {
            return "";
        }
    }

    protected void bt_NewItem_Click(object sender, EventArgs e)
    {
       // Response.Write("<script type='text/javascript'>window.open('NewItem.aspx?BoardID=" + ViewState["Board"].ToString() + "');</script>");
        Response.Redirect("NewItem.aspx?BoardID=" + ViewState["Board"].ToString());
    }

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/OA/BBS/Search/index.aspx");
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgrd_Result.Rows.Count; i++)
        {
            CheckBox chb = dgrd_Result.Rows[i].FindControl("chk_ID") as CheckBox;
            if (chb.Checked)
            {
                int id = int.Parse(dgrd_Result.DataKeys[i]["BBS_ForumItem_ID"].ToString());
                BBS_ForumItemBLL forumbll = new BBS_ForumItemBLL(id);
                forumbll.DeleteForumItem(id);
            }
        }
        BindGrid();
    }

    protected void dgrd_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
}
