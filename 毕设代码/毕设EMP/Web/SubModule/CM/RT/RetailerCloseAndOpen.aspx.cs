using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using System.Text;
using System.Text.RegularExpressions;
using MCSFramework.Common;

public partial class SubModule_CM_RT_RetailerCloseAndOpen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private bool CheckID(string[] stringA, string[] stringB, out string[] mixed)
    {
        mixed = stringA.Intersect(stringB).ToArray();
        if (mixed.Length > 0)
        {
            return false;
        }
        return true;
    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int ApproveCount = 0, UnApproveCount = 0, errcount = 0, LoseApproveCount = 0;
        string[] mixed;
        if (txt_OpenID.Text.Trim() != "")
        {
            string[] OpenID = Regex.Split(txt_OpenID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);

            if (txt_CloseID.Text.Trim() != "")
            {
                string[] CloseID = Regex.Split(txt_CloseID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
                if (!CheckID(OpenID, CloseID, out mixed))
                {
                    MessageBox.Show(this, "ID为【" + mixed[0] + "】的门店ID同时出现在了【开通】与【关闭】编辑框中！");
                    return;
                }
            }
            if (txt_LoseID.Text.Trim() != "")
            {
                string[] LoseID = Regex.Split(txt_LoseID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
                if (!CheckID(OpenID, LoseID, out mixed))
                {
                    MessageBox.Show(this, "ID为【" + mixed[0] + "】的门店ID同时出现在了【开通】与【失效】编辑框中！");
                    return;
                }
            }
        }
        if (txt_CloseID.Text.Trim() != "")
        {
            string[] CloseID = Regex.Split(txt_CloseID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
            if (txt_LoseID.Text.Trim() != "")
            {
                string[] LoseID = Regex.Split(txt_LoseID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
                if (!CheckID(CloseID, LoseID, out mixed))
                {
                    MessageBox.Show(this, "ID为【" + mixed[0] + "】的门店ID同时出现在了【关闭】与【失效】编辑框中！");
                    return;
                }
            }
        }
        if (txt_LoseID.Text.Trim() != "")
        {
            string[] LoseID = Regex.Split(txt_LoseID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
            CM_ClientBLL _bll;
            StringBuilder approvebuild = new StringBuilder("");

            for (int i = 0; i < LoseID.Length; i++)
            {
                int Client;
                if (!LoseID[i].Trim().Equals("") && int.TryParse(LoseID[i].Trim(), out Client))
                {
                    _bll = new CM_ClientBLL(Client);
                    if (_bll.Model != null)
                    {
                        _bll.Model["ISFL"] = "3";
                        _bll.Update();
                        approvebuild.Append("ID为【" + Client.ToString() + "】的门店已失效返利  ");
                        ApproveCount++;
                    }
                    else
                    {
                        approvebuild.Append("<span style='color: Red'>ID为【" + Client.ToString() + "】的门店未能在系统中找到  </span> ");
                        errcount++;
                    }
                }
                else if (!LoseID[i].Trim().Equals("") && !int.TryParse(LoseID[i].Trim(), out Client))
                {
                    approvebuild.Append("<span style='color: Red'>【" + Client.ToString() + "】不是一个有效的门店ID  </span> ");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) approvebuild.Append("<br/>");
            }
            lb_LoseErrorInfo.Text = approvebuild.ToString();
        }
        if (txt_OpenID.Text.Trim() != "")
        {
            string[] OpenID = Regex.Split(txt_OpenID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
            CM_ClientBLL _bll;
            StringBuilder approvebuild = new StringBuilder("");

            for (int i = 0; i < OpenID.Length; i++)
            {
                int Client;
                if (!OpenID[i].Trim().Equals("") && int.TryParse(OpenID[i].Trim(), out Client))
                {
                    _bll = new CM_ClientBLL(Client);
                    if (_bll.Model != null)
                    {
                        _bll.Model["ISFL"] = "1";
                        _bll.Update();
                        approvebuild.Append("ID为【" + Client.ToString() + "】的门店已开通返利  ");
                        ApproveCount++;
                    }
                    else
                    {
                        approvebuild.Append("<span style='color: Red'>ID为【" + Client.ToString() + "】的门店未能在系统中找到  </span> ");
                        errcount++;
                    }

                }
                else if (!OpenID[i].Trim().Equals("") && !int.TryParse(OpenID[i].Trim(), out Client))
                {
                    approvebuild.Append("<span style='color: Red'>【" + Client.ToString() + "】不是一个有效的门店ID  </span> ");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) approvebuild.Append("<br/>");
            }
            lb_OpenErrorInfo.Text = approvebuild.ToString();
        }
        if (txt_CloseID.Text.Trim() != "")
        {
            string[] CloseID = Regex.Split(txt_CloseID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
            CM_ClientBLL _bll;
            StringBuilder unapprovebuild = new StringBuilder("");
            for (int i = 0; i < CloseID.Length; i++)
            {
                int Client;
                if (!CloseID[i].Trim().Equals("") && int.TryParse(CloseID[i].Trim(), out Client))
                {
                    _bll = new CM_ClientBLL(Client);
                    if (_bll.Model != null)
                    {
                        UnApproveCount++;
                        _bll.Model["ISFL"] = "2";
                        _bll.Update();
                        unapprovebuild.Append("ID为【" + Client.ToString() + "】的门店已关闭返利  ");

                    }
                    else
                    {
                        errcount++;
                        unapprovebuild.Append("<span style='color: Red'>ID为【" + Client.ToString() + "】的返利协议未能在系统中找到  </span>");
                    }

                }
                else if (!CloseID[i].Trim().Equals("") && !int.TryParse(CloseID[i].Trim(), out Client))
                {
                    unapprovebuild.Append("<span style='color: Red'>【" + Client.ToString() + "】不是一个有效的门店ID  </span>");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) unapprovebuild.Append("<br/>");

            }
            lb_CloseErrorInfo.Text = unapprovebuild.ToString();
        }

        MessageBox.Show(this, "开通门店个数：" + ApproveCount.ToString() + @"\n关闭门店个数：" + UnApproveCount.ToString() +
                      @"\n失效门店个数：" + LoseApproveCount.ToString() + @"\n未能导入门店个数：" + errcount.ToString());
        return;
    }
}
