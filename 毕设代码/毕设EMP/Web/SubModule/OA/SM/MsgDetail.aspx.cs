using System;
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

public partial class SubModule_OA_SM_SMDetail : System.Web.UI.Page
{
    /// <summary>
    /// 新信息页面
    /// </summary>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == null)
        {
            int _id = 0;
            ViewState["ID"] = SM_ReceiverBLL.GetNextID(_id, (string)Session["UserName"]);
        }
        else
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
        }

        if ((int)ViewState["ID"] > 0)
        {
            hasNext();
            SM_ReceiverBLL r = new SM_ReceiverBLL((int)ViewState["ID"]);
            SM_MsgBLL _m = new SM_MsgBLL(r.Model.MsgID);

            ViewState["MsgID"] = r.Model.MsgID;
            this.txtRealName.Text = _m.Model.Sender.ToString();
            this.ltlContent.Text = _m.Model.Content.ToString();
            this.txt_msgtime.Text = _m.Model.SendTime.ToString();
            this.btnReply.Enabled = true;
        }
        else
        {
            MessageBox.Show(this, "没有新短讯需要读取！");
            RegisterClientScriptBlock("Close", "<script language='JavaScript'>window.close();</script>");
        }
    }

    /// <summary>
    /// 回复按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReply_Click(object sender, EventArgs e)
    {
        string receiver = (string)Session["UserName"];
        SM_ReceiverBLL.IsRead((int)ViewState["MsgID"], receiver);
        Response.Redirect("MsgSender.aspx?SendTo=" + txtRealName.Text);
    }

    /// <summary>
    /// 通过当前的ID获取Msg信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        int id = SM_ReceiverBLL.GetNextID((int)ViewState["ID"], (string)Session["UserName"]);
        if (id > 0)
            Response.Redirect("MsgDetail.aspx?ID=" + id);
        else
        {
            MessageBox.Show(this, "没有下一条新短讯需要读取!");
            btnNext.Visible = false;
        }
    }

    /// <summary>
    /// 设定是否已读
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRead_Click(object sender, EventArgs e)
    {
        string receiver = (string)Session["UserName"];
        SM_ReceiverBLL.IsRead((int)ViewState["MsgID"], receiver);

        int id = SM_ReceiverBLL.GetNextID((int)ViewState["ID"], (string)Session["UserName"]);
        if (id > 0)
            Response.Redirect("MsgDetail.aspx?ID=" + id);
        else
        {
            
            Response.Write("<script language=javascript>window.close();</script>");
        }
    }

    /// <summary>
    /// 是否还有新信息
    /// </summary>
    public void hasNext()
    {
        string receiver = (string)Session["UserName"];
        int count = SM_ReceiverBLL.HasNewMsg(receiver);
        if (count >= 2)
        {
            this.btnNext.Enabled = true;
        }
        else
        {
            this.btnNext.Enabled = false;
        }
    }
}
