using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL;
using MCSFramework.Model.Promotor;
using System.Text.RegularExpressions;
using MCSFramework.Common;
using System.Text;
public partial class SubModule_PM_PM_PromotorRefreshCallFee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            TextBox1.Text = Addr_OrganizeCityParamBLL.GetValueByType(1, 21);
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
        decimal subsidy = 0;
        decimal.TryParse(TextBox1.Text, out subsidy);
        int subsidyCount = 0, nosubsidyCount = 0, errcount = 0;
        string[] SubsidyID = Regex.Split(txt_SubsidyID.Text.Trim().Replace("\r\n","\n").Replace(" ","\n"), "\n", RegexOptions.IgnoreCase);
        string[] NoSubsidyID = Regex.Split(txt_NOsubsidyID.Text.Trim().Replace("\r\n", "\n").Replace(" ", "\n"), "\n", RegexOptions.IgnoreCase);
        string[] mixed;


        if (!CheckID(SubsidyID, NoSubsidyID, out mixed))
        {
            MessageBox.Show(this, "ID为【" + mixed[0] + "】的导购ID同时出现在了【有通讯补贴】与【无通讯补贴】编辑框中！");
            return;
        }

        if (SubsidyID.Length > 0)
        {
            PM_PromotorSalaryBLL _bll;
            StringBuilder approvebuild = new StringBuilder("");

            for (int i = 0; i < SubsidyID.Length; i++)
            {
                int PromotorID;
                if (!SubsidyID[i].Trim().Equals("") && int.TryParse(SubsidyID[i].Trim(), out PromotorID))
                {
                    IList<PM_PromotorSalary> _list = PM_PromotorSalaryBLL.GetModelList("Promotor=" + PromotorID.ToString() + " AND State IN (1,2,3)");
                    if (_list.Count == 0)
                    {
                        approvebuild.Append("<span style='color: Red'>ID为【" + PromotorID.ToString() + "】的导购未能在系统中找到有效的薪酬定义  </span> ");
                        if (i % 5 == 0 && i != 0) approvebuild.Append("<br/>");
                        errcount++;
                        continue;
                    }
                    foreach (PM_PromotorSalary m in _list)
                    {
                        _bll = new PM_PromotorSalaryBLL(m.ID);
                        _bll.Model.DIFeeSubsidy = subsidy;
                        _bll.Update();
                        approvebuild.Append("   ID为【" + PromotorID.ToString() + "】的导购我司通讯补贴已设置为" + subsidy.ToString());
                    }
                    subsidyCount++;

                }
                else if (!SubsidyID[i].Trim().Equals("") && !int.TryParse(SubsidyID[i].Trim(), out PromotorID))
                {
                    approvebuild.Append("<span style='color: Red'>【" + SubsidyID[i].Trim() + "】不是一个有效的导购ID  </span> ");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) approvebuild.Append("<br/>");
            }
            lb_SubsidyErrorInfo.Text = approvebuild.ToString();
        }
        if (NoSubsidyID.Length > 0)
        {

            PM_PromotorSalaryBLL _bll;
            StringBuilder unapprovebuild = new StringBuilder("");
            for (int i = 0; i < NoSubsidyID.Length; i++)
            {
                int PromotorID;

                if (!NoSubsidyID[i].Trim().Equals("") && int.TryParse(NoSubsidyID[i].Trim(), out PromotorID))
                {
                    IList<PM_PromotorSalary> _list = PM_PromotorSalaryBLL.GetModelList("Promotor=" + PromotorID.ToString() + " AND State IN (1,2,3)");
                    if (_list.Count == 0)
                    {
                        unapprovebuild.Append("<span style='color: Red'>ID为【" + PromotorID.ToString() + "】的导购未能在系统中找到有效的薪酬定义</span> ");
                        errcount++;
                        if (i % 5 == 0 && i != 0) unapprovebuild.Append("<br/>");
                        continue;
                    }
                    foreach (PM_PromotorSalary m in _list)
                    {
                        _bll = new PM_PromotorSalaryBLL(m.ID);

                        _bll.Model.DIFeeSubsidy = 0;
                        _bll.Update();
                        unapprovebuild.Append("   ID为【" + PromotorID.ToString() + "】的导购我司通讯补贴已设置为0");
                    }
                    nosubsidyCount++;
                }
                else if (!NoSubsidyID[i].Trim().Equals("") && !int.TryParse(NoSubsidyID[i].Trim(), out PromotorID))
                {
                    unapprovebuild.Append("<span style='color: Red'>【" + NoSubsidyID[i].Trim() + "】不是一个有效的导购ID  </span> ");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) unapprovebuild.Append("<br/>");

            }
            lb_NOsubsidyErrorInfo.Text = unapprovebuild.ToString();
        }

        MessageBox.Show(this, "有通讯补贴导购数：" + subsidyCount.ToString() + @"\n无通讯补贴导购数：" + nosubsidyCount.ToString() +
           @"\n未能设置导购个数：" + errcount.ToString());
    
    }

}
