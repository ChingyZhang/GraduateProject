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
public partial class SubModule_CM_RT_RetailerContractFirstFLApprove : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int ApproveCount=0, UnApproveCount=0,errcount=0;
        if (txt_UnApproveContractID.Text.Trim() != "")
        {
            string[] UnApproveID = Regex.Split(txt_UnApproveContractID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);

            if (txt_ApproveContractID.Text.Trim() != "")
            {
                string[] ApproveID = Regex.Split(txt_ApproveContractID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
                foreach (string contractid in UnApproveID)
                {
                    if (ApproveID.Contains(contractid))
                    {
                        MessageBox.Show(this, "ID为【" + contractid + "】的返利协议同时出现在了【审批通过】与【审批不通过】编辑框中！");
                        return;
                    }
                }
            }
        }
        if (txt_ApproveContractID.Text.Trim() != "")
        {
            string[] ApproveID = Regex.Split(txt_ApproveContractID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
            CM_ContractBLL _bll;
            StringBuilder approvebuild = new StringBuilder("");

            for (int i = 0; i < ApproveID.Length; i++)
            {
                int ContractID;
                if (!ApproveID[i].Trim().Equals("") && int.TryParse(ApproveID[i].Trim(), out ContractID))
                {
                    _bll = new CM_ContractBLL(ContractID);
                    if (_bll.Model != null)
                    {
                        int jobid = EWF_TaskBLL.StaffCanApproveTask(_bll.Model.ApproveTask, 7394);
                        if (jobid > 0)
                        {
                            EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                            if (job != null)
                            {
                                int decisionid = job.StaffCanDecide(7394);
                                if (decisionid > 0)
                                {
                                    ApproveCount++;
                                    job.Decision(decisionid, (int)Session["UserID"], 2, "批量审批通过!");       //2:审批已通过
                                    approvebuild.Append("ID为【" + ContractID.ToString() + "】的返利协议审批通过  ");
                                   
                                }
                            }
                        }
                        else
                        {
                            approvebuild.Append("<span style='color: Red'>ID为【" + ContractID.ToString() + "】的返利协议未到客服确认环节   </span>");
                            errcount++;
                        }

                    }
                    else
                    {
                        approvebuild.Append("<span style='color: Red'>ID为【" + ContractID.ToString() + "】的返利协议未能在系统中找到  </span> ");
                        errcount++;
                    }

                }
                else if (!ApproveID[i].Trim().Equals("") && !int.TryParse(ApproveID[i].Trim(), out ContractID))
                {
                    approvebuild.Append("<span style='color: Red'>【" + ContractID.ToString() + "】不是一个有效的返利协议ID  </span> ");
                    errcount++;
                }
                if (i % 5 == 0&&i!=0) approvebuild.Append("<br/>");
            }
            lb_ApproveErrorInfo.Text = approvebuild.ToString();
        }
        if (txt_UnApproveContractID.Text.Trim() != "")
        {
            string[] UnApproveID = Regex.Split(txt_UnApproveContractID.Text.Trim(), "\r\n", RegexOptions.IgnoreCase);
            CM_ContractBLL _bll;
            StringBuilder unapprovebuild = new StringBuilder("");
            for (int i = 0; i < UnApproveID.Length; i++)
            {
                int ContractID;
                if (!UnApproveID[i].Trim().Equals("") && int.TryParse(UnApproveID[i].Trim(), out ContractID))
                {
                    _bll = new CM_ContractBLL(ContractID);
                    if (_bll.Model != null)
                    {
                        int jobid = EWF_TaskBLL.StaffCanApproveTask(_bll.Model.ApproveTask, 7394);
                        if (jobid > 0)
                        {
                            EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                            if (job != null)
                            {
                                int decisionid = job.StaffCanDecide(7394);
                                if (decisionid > 0)
                                {
                                    UnApproveCount++;
                                    job.Decision(decisionid, (int)Session["UserID"], 3, "批量审批不通过!");       //3:审批不通过
                                    unapprovebuild.Append("ID为【" + ContractID.ToString() + "】的返利协议审批未通过  ");
                                }
                            }
                        }
                        else
                        {
                            unapprovebuild.Append("<span style='color: Red'>ID为【" + ContractID.ToString() + "】的返利协议未到客服确认环节  </span>");
                            errcount++;
                        }
                    }
                    else
                    {
                        errcount++;
                        unapprovebuild.Append("<span style='color: Red'>ID为【" + ContractID.ToString() + "】的返利协议未能在系统中找到  </span>");
                    }

                }
                else if (!UnApproveID[i].Trim().Equals("") && !int.TryParse(UnApproveID[i].Trim(), out ContractID))
                {
                    unapprovebuild.Append("<span style='color: Red'>【" + ContractID.ToString() + "】不是一个有效的返利协议ID  </span>");
                    errcount++;
                }
                if (i % 5 == 0 && i != 0) unapprovebuild.Append("<br/>");
              
            }
            lb_UnApproveErrorInfo.Text = unapprovebuild.ToString();
        }
       
        MessageBox.Show(this, "审批通过协议个数：" + ApproveCount.ToString() +   @"\n审批未通过协议个数：" + UnApproveCount.ToString() +  
            @"\n未能导入协议个数：" + errcount.ToString()  );
        return;
    }
}
