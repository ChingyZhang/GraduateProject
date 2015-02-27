using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Common;

public partial class SubModule_OA_BBS_deleteitem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        int itemid = (Request.QueryString["ItemID"] == null) ? 0 : Int32.Parse(Request.QueryString["ItemID"].ToString());
        int boardid = (Request.QueryString["BoardID"] == null) ? 0 : Int32.Parse(Request.QueryString["BoardID"].ToString());
        try
        {
            BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(itemid);
            itembll.DeleteForumItem(itemid);
            MessageBox.ShowAndRedirect(this,"删除成功", "listview.aspx?Board=" + boardid.ToString());
        }
        catch (Exception)
        {
            Server.Transfer("../Mail/Error.aspx");
        }
    }
}
