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
using MCSControls.MCSWebControls;

public partial class SubModule_OA_KB_NewArticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["KB_Article_ID"] == null ? 0 : int.Parse(Request.QueryString["KB_Article_ID"]);
            ViewState["Catalog"] = Request.QueryString["Catalog"] == null ? 0 : int.Parse(Request.QueryString["Catalog"]);

            ckedit_content.Text = "";
            if ((int)ViewState["ID"] > 0)
            {
                this.UploadFile1.Visible = true;
                this.UploadFile1.RelateID = (int)ViewState["ID"];

                BindData();
            }
            else
            {
                if ((int)ViewState["Catalog"] > 0)
                {
                    MCSTreeControl select_Catalog = (MCSTreeControl)panel1.FindControl("KB_Article_Catalog");
                    if (select_Catalog != null) select_Catalog.SelectValue = ViewState["Catalog"].ToString();

                    TextBox tbx_Author = (TextBox)panel1.FindControl("KB_Article_Author");
                    if (tbx_Author != null) tbx_Author.Text = (string)Session["UserRealName"];

                }
                tr_Approve.Visible = false;
                UploadFile1.Visible = false;
            }
            ViewState["Catalog"] = 0;
        }
    }

    public void BindData()
    {
        KB_ArticleBLL bll = new KB_ArticleBLL((int)ViewState["ID"]);
        panel1.BindData(bll.Model);
        this.txt_approve_idea.Text = bll.Model.ApproveStaffIdeas;
        btn_ok.Text = "修改";
        ckedit_content.Text = bll.Model.Content.Replace("\r", "<br/>");

        if (bll.Model.HasApproved == "Y")
        {
            //btn_ok.Visible = false;
            tr_Approve.Visible = false;
        }

        UploadFile1.BindGrid();
    }


    protected void btn_ok_Click(object sender, EventArgs e)
    {
        KB_ArticleBLL bll = null;
        if ((int)ViewState["ID"] == 0)
        {
            bll = new KB_ArticleBLL();
            bll.Model.UploadStaff = (int)Session["UserID"];
            bll.Model.HasApproved = "N";
            bll.Model.IsDelete = "N";
            bll.Model.ReadCount = 1;
            bll.Model.UsefullCount = 0;
        }
        else
        {
            bll = new KB_ArticleBLL((int)ViewState["ID"]);
            bll.Model.IsDelete = "N";
        }

        panel1.GetData(bll.Model);
        bll.Model.Content = ckedit_content.Text.ToString();

        if ((int)ViewState["ID"] == 0)
        {
            ViewState["ID"] = bll.Add();
        }
        else
        {
            if (bll.Model.HasApproved == "Y")
            {
                bll.Model.HasApproved = "N";        //文章经过修改后，再次回来未审核状态
                bll.Model.ApproveStaff = 0;
                bll.Model.ApproveTime = new DateTime(1900, 1, 1);
            }
            bll.Update();
        }
        BindData();
        MessageBox.ShowAndRedirect(this, "该文章当前为未审核状态，请尽快审核发布！", "NewArticle.aspx?KB_Article_ID=" + ViewState["ID"].ToString());
    }

    protected void btn_check_Click(object sender, EventArgs e)
    {
        int id = (int)ViewState["ID"];
        int userid = (int)Session["UserID"];
        string ideas = this.txt_approve_idea.Text;
        KB_ArticleBLL bll = new KB_ArticleBLL();
        bll.UpdateApprov(id, userid, ideas);
        BindData();
        Response.Redirect("ArticleManager.aspx");
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        KB_ArticleBLL bll = new KB_ArticleBLL((int)ViewState["ID"]);
        bll.DeleteByID((int)ViewState["ID"]);

        Response.Redirect("index.aspx");
    }
}
