using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_OA_KB_CatalogManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                KB_ArticleBLL _bll = new KB_ArticleBLL();
                _bll.UpdateReadCount((int)ViewState["ID"]);
                UploadFile1.RelateID = (int)ViewState["ID"];

                BindData();
            }

            ViewState["Catalog"] = 0;
        }
    }

    public void BindData()
    {
        KB_ArticleBLL bll = new KB_ArticleBLL((int)ViewState["ID"]);

        UploadFile1.BindGrid();

        lt_article_comment.Text = bll.Model.Content.Replace("\r", "<br/>");


        lb_Title.Text = bll.Model.Title;
        lb_Author.Text = bll.Model.Author;
        lb_Keyword.Text = bll.Model.KeyWord;
        lb_ReadCounts.Text = bll.Model.ReadCount.ToString();

        panel1.BindData(bll.Model);

        bt_Edit.Visible = (bll.Model.UploadStaff == (int)Session["UserID"]);

        if (new KB_CatalogBLL(bll.Model.Catalog).Model.CommentFlag == "N")
        {
            tb_Comment.Visible = false;
        }
        else
        {
            ud_grid_comment.ConditionString = " KB_Comment.IsDelete = 'N' and KB_Comment.Article=" + ViewState["ID"].ToString();
            ud_grid_comment.BindGrid();
        }
    }

    protected void btn_add_comment_Click(object sender, EventArgs e)
    {
        KB_CommentBLL bll = new KB_CommentBLL();
        bll.Model.Article = (int)ViewState["ID"];
        bll.Model.CommentStaff = (int)Session["UserID"];
        bll.Model.IsDelete = "N";
        bll.Model.Content = tbx_CommentContent.Text;

        int i = bll.Add();
        tbx_CommentContent.Text = "";
        BindData();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        KB_ArticleBLL bll = new KB_ArticleBLL((int)ViewState["ID"]);
        if (this.rbl_VisitFlag.Text == "Y")
        {
            bll.Model.UsefullCount += 1;
            bll.Update();
        }
        BindData();
    }
    protected void btn_del_comment_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid_comment.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                new KB_CommentBLL().DeleteByID(int.Parse(ud_grid_comment.DataKeys[gr.RowIndex]["KB_Comment_ID"].ToString()));
                //new KB_ArticleBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex]["ID"].ToString())).DeleteByID();
            }
        }
        BindData();
    }
    protected void bt_Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewArticle.aspx?KB_Article_ID=" + ViewState["ID"].ToString());
    }
}
