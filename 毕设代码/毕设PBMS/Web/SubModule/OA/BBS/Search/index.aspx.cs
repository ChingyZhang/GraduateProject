using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.Common;

public partial class SubModule_OA_BBS_Search_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dgrd_Result.Visible = false;
            td_tl.Visible = false;
            BindGrid();
            BindTree(tr_Catalog.Nodes);
            ViewState["ConditionString"] = " BBS_ForumItem.ID in (select top 10 BBS_ForumItem.ID from MCS_OA.dbo.BBS_ForumItem where (datediff(d,BBS_ForumItem.SendTime,getdate())< 1) order by BBS_ForumItem.HitTimes desc)";
            BindHotForumItem();
        }
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
                ViewState["ConditionString"] = ViewState["ConditionString"] +  " order by BBS_ForumItem.HitTimes desc)  ";
                break;
        }
        BindHotForumItem();
    }
    private void BindGrid()
    {
        dll_Board.DataSource = BBS_BoardBLL.GetModelList("");
        dll_Board.DataBind();
        dll_Board.Items.Insert(0,new ListItem("全部", "0"));
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
            BindChildTree(tn.ChildNodes, _m.ID);
 
        }
    }
    private void BindChildTree(TreeNodeCollection TNC, int SuperID)
    {
        IList<BBS_Board> _list = BBS_BoardBLL.GetModelList(" Catalog= " + SuperID.ToString());
        foreach (BBS_Board _m in _list)
        {
            bool flag = false;
            IList<BBS_BoardUserMember> userList = new BBS_BoardUserMemberBLL()._GetModelList(" Board=" + _m.ID);
            if (userList != null)
            {
                foreach (BBS_BoardUserMember user in userList)
                {
                    if (user.UserName==(string)Session["UserName"])
                        flag = true;
                }
            }
            if (_m.IsPublic == "1" || (_m.IsPublic == "2" && flag))
            {
                TreeNode tn = new TreeNode();
                tn.Text = _m.Name;
                tn.Value = _m.ID.ToString();
                TNC.Add(tn);
            }
        }
    }
    protected void tr_Catalog_SelectedNodeChanged(object sender, EventArgs e)
    {//MessageBox.Show(this,this.tr_Catalog.SelectedNode.Value);
    if (int.Parse(this.tr_Catalog.SelectedNode.Value) != 0)
    {
        ViewState["Board"] = int.Parse(this.tr_Catalog.SelectedNode.Value);
        Response.Redirect("~/SubModule/OA/BBS/listview.aspx?Board=" + ViewState["Board"].ToString());
    }
    else
    {

        Response.Redirect("~/SubModule/OA/BBS/index.aspx");
    }
    }
    protected string GetType(int id)
    {
        BBS_ForumItemBLL forumItembll = new BBS_ForumItemBLL(id);
        //if (forumItembll.Model.IsBoardNotice == "Y" || forumItembll.Model.IsPublicNotice == "Y")
        //{
        //    if (forumItembll.Model.IsBoardNotice == "Y")
        //        return "<img src='../../../../images/bulletin.gif'>";
        //    else
        //        return "<img src='../../../../images/sysbulletin.gif'>";
        //}

            if(forumItembll.Model.HitTimes > 5)
                return "<img src='../../../../images/hotfolder.gif'>";
            else
                return "<img src='../../../../images/folder.gif'>";
    }
    protected string GetTypeIsTop(int id)
    {
        BBS_ForumItemBLL forumItembll = new BBS_ForumItemBLL(id);
        if (forumItembll.Model.ExtPropertys != null)
        {
            if (forumItembll.Model["IsTop"] == "Y")
                return "<img src='../../../../images/istop.jpg'>";
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
                return "<img src='../../../../images/pith.jpg'>";
            else
                return "";
        }
        else
        {
            return "";
        }
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        dgrd_Result.Visible = true;
        td_tl.Visible = true;
        #region 列隐藏
        dgrd_Result.Columns[2].Visible = false;
        dgrd_Result.Columns[4].Visible = false;
        dgrd_Result.Columns[7].Visible = false;
        dgrd_Result.Columns[9].Visible = false;
        dgrd_Result.Columns[10].Visible = false;
        dgrd_Result.Columns[11].Visible = false;
        #endregion
        string sql = "";
        string searchtype = (rbtn_author.Checked) ? "Sender" : "Title";
        if (tbx_Key.Text != null && tbx_Key.Text != "")
        {
            if (searchtype.Equals("Title"))
                sql = " (BBS_ForumItem.Title like '%" + tbx_Key.Text + "%') ";
            else
                sql = " (BBS_ForumItem.Sender like '%" + tbx_Key.Text + "%') ";
        }

        string data = "";
        switch (ddl_Time.SelectedValue)
        {
            case "0":
                data = "";
                break;
            case "d":
                data = (tbx_Time.Text.Trim() == "") ? "1" : tbx_Time.Text.Trim();
                break;
            case "w":
                data = (tbx_Time.Text.Trim() == "") ? "7" : (int.Parse(tbx_Time.Text.Trim()) * 7).ToString();
                break;
            case "m":
                data = (tbx_Time.Text.Trim() == "") ? "30" : (int.Parse(tbx_Time.Text.Trim()) * 30).ToString();
                break;
            case "y":
                data = (tbx_Time.Text.Trim() == "") ? "365" : (int.Parse(tbx_Time.Text.Trim()) * 365).ToString();
                break;
        }
        if (data != "")
        {
            if(sql!="")
               sql += " and(datediff(d,BBS_ForumItem.SendTime,getdate())< " + data + ")";
            else
                sql = " (datediff(d,BBS_ForumItem.SendTime,getdate())< " + data + ")";
        }

        int boardID = Int32.Parse(dll_Board.SelectedValue);
        if (boardID > 0)
        {
            if(sql!="")
               sql += " and BBS_ForumItem.Board = " + boardID;
            else
                sql = " BBS_ForumItem.Board = " + boardID;
        }

        dgrd_Result.ConditionString = sql + " order by BBS_ForumItem.ExtPropertys desc,BBS_ForumItem.LastReplyTime desc";
        dgrd_Result.BindGrid();
    }
}
