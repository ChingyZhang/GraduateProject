using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using System.Text;
using MCSFramework.Model.CM;
public partial class SubModule_CM_RT_RetailerIntegralCloseAndOpen : System.Web.UI.Page
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
            string[] OpenID = Regex.Split(txt_OpenID.Text.Trim().Replace("\r\n", "\n").Replace(" ", "\n"), "\n", RegexOptions.IgnoreCase);

            if (txt_CloseID.Text.Trim() != "")
            {
                string[] CloseID = Regex.Split(txt_CloseID.Text.Trim().Replace("\r\n", "\n").Replace(" ", "\n"), "\n", RegexOptions.IgnoreCase);
                if (!CheckID(OpenID, CloseID, out mixed))
                {
                    MessageBox.Show(this, "客户代码为【" + mixed[0] + "】的客户代码同时出现在了【开通】与【关闭】编辑框中！");
                    return;
                }
            }

        }

        if (txt_OpenID.Text.Trim() != "")
        {
            string[] OpenID = Regex.Split(txt_OpenID.Text.Trim().Replace("\r\n", "\n").Replace(" ", "\n"), "\n", RegexOptions.IgnoreCase);
            CM_ClientBLL _bll;
            StringBuilder approvebuild = new StringBuilder("");

            for (int i = 0; i < OpenID.Length; i++)
            {

                if (!OpenID[i].Trim().Equals(""))
                {
                    IList<CM_Client> cmlist = CM_ClientBLL.GetModelList("Code='" + OpenID[i].Trim()+"'");

                    if (cmlist.Count > 0)
                    {
                        _bll = new CM_ClientBLL(cmlist[0].ID);
                        _bll.Model["IsRMSClient"] = "3";
                        _bll.Model["RMSAccountEnabled"] = "1";
                        _bll.Update();
                        approvebuild.Append("客户代码为【" + OpenID[i].Trim() + "】的门店已开通积分");
                        ApproveCount++;
                    }
                    else
                    {
                        approvebuild.Append("<span style='color: Red'>客户代码为【" + OpenID[i].Trim() + "】的门店未能在系统中找到  </span> ");
                        errcount++;
                    }

                }
                else if (!OpenID[i].Trim().Equals(""))
                {
                    approvebuild.Append("<span style='color: Red'>【" + OpenID[i].Trim() + "】不是一个有效的客户代码</span> ");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) approvebuild.Append("<br/>");
            }
            lb_OpenErrorInfo.Text = approvebuild.ToString();
        }
        if (txt_CloseID.Text.Trim() != "")
        {
            string[] CloseID = Regex.Split(txt_CloseID.Text.Trim().Replace("\r\n", "\n").Replace(" ", "\n"), "\n", RegexOptions.IgnoreCase);
            CM_ClientBLL _bll;
            StringBuilder unapprovebuild = new StringBuilder("");
            for (int i = 0; i < CloseID.Length; i++)
            {

                if (!CloseID[i].Trim().Equals(""))
                {
                    IList<CM_Client> cmlist = CM_ClientBLL.GetModelList("Code='" + CloseID[i].Trim() + "'");

                    if (cmlist.Count > 0)
                    {
                        _bll = new CM_ClientBLL(cmlist[0].ID);
                        _bll.Model["IsRMSClient"] = "2";
                        _bll.Model["RMSAccountEnabled"] = "2";
                        _bll.Update();
                        unapprovebuild.Append("客户代码为【" + CloseID[i].Trim() + "】的门店已关闭积分");
                        UnApproveCount++;
                    }
                    else
                    {
                        errcount++;
                        unapprovebuild.Append("<span style='color: Red'>ID为【" + CloseID[i].Trim() + "】的返利协议未能在系统中找到  </span>");
                    }

                }
                else if (!CloseID[i].Trim().Equals(""))
                {
                    unapprovebuild.Append("<span style='color: Red'>【" + CloseID[i].Trim() + "】不是一个有效的客户代码  </span>");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) unapprovebuild.Append("<br/>");

            }
            lb_CloseErrorInfo.Text = unapprovebuild.ToString();
        }

        MessageBox.Show(this, "开通积分门店个数：" + ApproveCount.ToString() + @"\n关闭积分门店个数：" + UnApproveCount.ToString() +
                       @"\n未能导入门店个数：" + errcount.ToString());
        return;
    }
}
