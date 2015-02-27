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

public partial class SubModule_OA_SM_MsgSender : System.Web.UI.Page
{
    public string SendTo = "", SendToRealName = "";

    /// <summary>
    /// 信息发送页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SendTo = (Request.QueryString["SendTo"] != null) ? Request.QueryString["SendTo"].ToString() + "," : "";
            if (SendTo != "")
            {
                SendTo = Server.UrlDecode(Request.QueryString["SendTo"].ToString().Replace("*", "%"));
                if (UserBLL.GetStaffByUsername(SendTo) != null)
                    SendToRealName = UserBLL.GetStaffByUsername(SendTo).RealName;
            }
            //为btnSend添加JavaScript脚本onclick事件
            //this.btnSend.Attributes["onclick"] = "javascript: window.close();";
            //为btnReturn添加JavaScript脚本onclick事件
            this.btnReturn.Attributes["onclick"] = "javascript: window.close();";
        }
    }

    /// <summary>
    /// 发送短讯
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        SendTo = Request.Form["hdnTxtSendTo"].ToString();
        string recipients = this.SendTo;
        if (recipients.EndsWith(",")) recipients = recipients.Substring(0, recipients.Length - 1);
        recipients = recipients.Replace("，", ",");
        string[] separators = new string[] { "," };
        string[] senders;

        senders = recipients.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        SM_MsgBLL bll = new SM_MsgBLL();
        bll.Model.Sender = (string)Session["UserName"];
        bll.Model.Content = this.ckedit_content.Text;
        bll.Model.SendTime = DateTime.Now;
        bll.Model.Type = 1;
        bll.Model.IsDelete = "N";

        int id = bll.Add();

        SM_ReceiverBLL _receiverbll = new SM_ReceiverBLL();
        foreach (string s in senders)
        {
            _receiverbll.Model.MsgID = id;
            _receiverbll.Model.IsRead = "N";
            _receiverbll.Model.IsDelete = "N";
            _receiverbll.Model.Receiver = s;
            int i = _receiverbll.Add();
        }

        MessageBox.ShowAndClose(this, "站内短讯发送成功!");
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {

    }


}
