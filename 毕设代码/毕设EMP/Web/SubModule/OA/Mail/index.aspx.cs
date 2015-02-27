using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;

public partial class SubModule_OA_Mail_index : System.Web.UI.Page
{
    protected static string contionString = "1=1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["FolderType"] != null)
                ViewState["FolderType"] = int.Parse(Request.QueryString["FolderType"]);
            else
                ViewState["FolderType"] = 1;

            if ((int)ViewState["FolderType"] == 2) this.btnClear.Visible = true;
            if ((int)ViewState["FolderType"] == 4)
            {
                tr_ExtMailReceive.Visible = true;
            }
            if (Session["UserID"] != null && Session["UserID"].ToString().Length != 0)
            {
                BindMail();
            }
        }
    }


    #region 将数据绑定控件
    private void BindMail()
    {
        if (ViewState["PageIndex"] != null)
        {
            this.ud_Mail.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }
        BindFolderType();

        string userName = (string)Session["UserName"];
        
        this.ud_Mail.ConditionString = contionString;
        this.ud_Mail.BindGrid();

        int count = 0;
        for (int i = 0; i < this.ud_Mail.Rows.Count; i++)
        {
            ML_MailBLL bll = new ML_MailBLL(Convert.ToInt32(this.ud_Mail.DataKeys[i]["ML_Mail_ID"].ToString()));
            if (bll.Model.IsRead == "N")
                count++;
        }


        #region 显示未读邮件数，删除按钮
        if (ViewState["FolderType"].ToString() != "4")
        {
            this.lblMsg.Text = this.ud_Mail.Rows.Count.ToString() + "/<font color=red>" + count.ToString() + "</font>未读";
        }
        if (this.ud_Mail.Rows.Count != 0)
        {
            this.btnDelete.Visible = true;
            this.btnDelete.Attributes["onclick"] = "javascript:return confirm('您确认要删除吗?');";
        }
        #endregion
    }
    #endregion

    #region 根据不同邮箱显示不同列
    private void BindFolderType()
    {
        this.ud_Mail.Columns[2].Visible = false;//　隐藏序号
        this.ud_Mail.Columns[6].Visible = false;//　隐藏主题
        this.ud_Mail.Columns[8].Visible = false;//　隐藏IsRead
        this.ud_Mail.Columns[9].Visible = false;//　隐藏number
        this.btnClear.Visible = false;

        if (ViewState["FolderType"].ToString() == "1")              //收件箱
        {
            this.ud_Mail.Columns[4].Visible = false;//　隐藏收件人
            this.ud_Mail.Columns[3].Visible = true; //　显示发件人

            if (tbx_SelectContent.Text != "")
            {
                contionString = "(ML_Mail.Receiver = '" + Session["UserName"].ToString() + "' and ML_Mail.Folder = 1  and IsDelete = 'N'  and  ML_Mail."+ddl_sampleSelect.SelectedValue+"like '%"+tbx_SelectContent.Text+"%') Order by ML_Mail.SendTime desc ";
            }
            else
            {
                contionString = "(ML_Mail.Receiver = '" + Session["UserName"].ToString() + "' and ML_Mail.Folder = 1  and IsDelete = 'N' ) Order by ML_Mail.SendTime desc ";
            }
            
           
            
        }
        else if (ViewState["FolderType"].ToString() == "2")         //发件箱
        {
            this.ud_Mail.Columns[4].Visible = true; //　显示收件人
            this.ud_Mail.Columns[3].Visible = false;//　隐藏发件人
            if (tbx_SelectContent.Text != "")
            {
                contionString = "(ML_Mail.Sender = '" + Session["UserName"].ToString() + "' and ML_Mail.Folder = 2 and IsDelete = 'N'  and  ML_Mail." + ddl_sampleSelect.SelectedValue + " like '%" +tbx_SelectContent.Text+ "%' ) order by ML_Mail.SendTime desc ";
            }
            else
            {
                contionString = "(ML_Mail.Sender = '" + Session["UserName"].ToString() + "' and ML_Mail.Folder = 2 and IsDelete = 'N' ) order by ML_Mail.SendTime desc ";
            }
          
        }
        else if (ViewState["FolderType"].ToString() == "3")         //垃圾箱
        {
            this.ud_Mail.Columns[4].Visible = true;//　隐藏收件人
            this.ud_Mail.Columns[3].Visible = true;//　显示发件人

            if (tbx_SelectContent.Text != "")
            {
                contionString += "((ML_Mail.Receiver = '" + Session["UserName"].ToString() + "' OR ML_Mail.Sender = '" + Session["UserName"].ToString() +
                "') and ML_Mail.Folder = 3 and IsDelete = 'Y'   and  ML_Mail." + ddl_sampleSelect.SelectedValue + " like  '%" + tbx_SelectContent.Text + "%') order by ML_Mail.SendTime desc ";
            }
            else
            {
                contionString += "((ML_Mail.Receiver = '" + Session["UserName"].ToString() + "' OR ML_Mail.Sender = '" + Session["UserName"].ToString() +
                "') and ML_Mail.Folder = 3 and IsDelete = 'Y' ) order by ML_Mail.SendTime desc ";
            }
            

            this.btnClear.Attributes["onclick"] = "javascript:return confirm('您确认要清空吗?');";
            this.btnClear.Visible = true;
        }
        Session["FolderType"] = ViewState["FolderType"];
    }
    #endregion

    #region 写邮件
    protected void btnwritenewmail_Click(object sender, EventArgs e)
    {
        //写新邮件
        Response.Redirect("compose.aspx");
    }
    #endregion

    #region 清空垃圾邮件
    protected void btnClear_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int i = 0; i < this.ud_Mail.Rows.Count; i++)
        {
            new ML_MailBLL(Convert.ToInt32(this.ud_Mail.DataKeys[i]["ID"].ToString())).UpdateIsdelete();
            count++;
        }
        if (count == this.ud_Mail.Rows.Count)
            Page.ClientScript.RegisterStartupScript(this.GetType(), "success", "<script language='javascript'>alert('邮箱清空成功!');</script>");
        BindMail();
    }
    #endregion

    #region 删除选中邮件
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.ud_Mail.Rows.Count; i++)
        {
            CheckBox chk = this.ud_Mail.Rows[i].FindControl("chk") as CheckBox;
            int id = Convert.ToInt32(this.ud_Mail.DataKeys[i]["ML_Mail_ID"].ToString());

            if (chk.Checked == true)
            {
                new ML_MailBLL(id).UpdateIsdelete();
            }
        }
        BindMail();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "success", "<script language='javascript'>alert('邮件删除成功!');</script>");
    }
    #endregion

    #region 进入不同类型邮箱
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        ViewState["PageIndex"] = 0;
        switch (e.item.Value)
        {
            case "1":
                ViewState["FolderType"] = 1;
                this.btnClear.Visible = false;
              
                break;
            case "2":
                ViewState["FolderType"] = 2;
                this.btnClear.Visible = true;
                break;
            case "3":
                ViewState["FolderType"] = 3;
                this.btnClear.Visible = false;
                break;
            default:
                break;
        }
        BindMail();

    }
    #endregion

    #region 将邮件转移至指定信箱
    protected void FolderListChange(object sender, EventArgs e)
    {
        string sql = "";
        int FolderType = Int32.Parse(this.listFolderType.SelectedItem.Value); //设置转移目标
        
        for (int i = 0; i < this.ud_Mail.Rows.Count; i++)
        {
            CheckBox chk = this.ud_Mail.Rows[i].FindControl("chk") as CheckBox;
            int id = Convert.ToInt32(this.ud_Mail.DataKeys[i]["ML_Mail_ID"].ToString());
            if (chk.Checked == true)
            {
                new ML_MailBLL(id).UpdateMailFolder(FolderType);
                sql += id.ToString();
            }
        }
        //选择为空
        if (sql == String.Empty)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script language='javascript'>alert('请选择邮件!');</script>");

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "success", "<script language='javascript'>alert('邮件移动成功!');</script>");

        }
        BindMail();
    }
    #endregion

    #region 外部邮件操作
    protected void btnBeginReceive_Click(object sender, EventArgs e)
    {
        //开始接受
        //string Username = Session["UserName"].ToString();
        //int OrderID = Int32.Parse(this.listExtMail.SelectedItem.Value.ToString());
        //try
        //{
        //    mail.ReceiveMails(Username, OrderID);
        //    Response.Write("<script language=javascript>alert('接收完成!');</script>");
        //}
        //catch (Exception ex)
        //{
        //    Meichis.Common.Error.Log(ex.ToString());
        //    //Server.Transfer("../../Error.aspx");
        //    Response.Write("<script language=javascript>alert('服务器正在忙碌中,请稍候再试');</script>");

        //}
        ViewState["FolderType"] = 4;

    }
    protected void btnExtPopSetup_Click(object sender, EventArgs e)
    {
        //外部邮箱设置
        Response.Redirect("External/SetupNavi.aspx");
    }
    #endregion

    protected void ud_Mail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindMail();
    }

    protected string DisplayHasAttachFile(int id)
    {
        if (new ML_MailBLL(id).GetAttachFiles().Count > 0)
            return "<img src='../../../DataImages/attach.gif' border='0'>";
        else
            return "";
    }

    protected void bt_sampleSelect_Click(object sender, EventArgs e)
    {
        BindMail();
    }
}
