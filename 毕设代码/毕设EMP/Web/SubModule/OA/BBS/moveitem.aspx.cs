using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

public partial class SubModule_OA_BBS_moveitem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            ViewState["itemid"] = (Request.QueryString["ItemID"] == null) ? 0 : Int32.Parse(Request.QueryString["ItemID"].ToString()); //帖子的编号
            BindData();
        }
    }

    #region 获取论坛文章的板块
    protected void BindData() 
    {
        BBS_BoardBLL boardbll = new BBS_BoardBLL(Convert.ToInt32(ViewState["itemid"]));
        ddlBoardList.DataSource = boardbll._GetModelList("");

        ddlBoardList.DataTextField = "Name";
        ddlBoardList.DataValueField = "ID";
        ddlBoardList.DataBind();
    }
    #endregion

    #region 提交
    protected void cmdOK_ServerClick(object sender, System.EventArgs e)
    {
        int BoardID = Int32.Parse(ddlBoardList.SelectedItem.Value);
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["itemid"]));
        itembll.Model.Board = BoardID;
        try
        {
            itembll.Update();
            ddlBoardList.Visible = false;
            cmdOK.Visible = false;
            ltMessage.Visible = true;
            ltMessage.Text = "移动成功！";

        }
        catch (Exception)
        {
            Server.Transfer("../Mail/Error.aspx");
        }
    }
    #endregion
}
