using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;

public partial class SubModule_OA_PN_select : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
         
            int id = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);  
            PN_HasReadStaffBLL hasReadStaffbll = new PN_HasReadStaffBLL();
            PN_CommentBLL commentbll = new PN_CommentBLL();
            lab_hasRead.Text = hasReadStaffbll.GetReadCountByNotice(id).ToString();
            lab_comment.Text = commentbll.GetCommentCountByNotice(id).ToString();
            
        }
    }
}
