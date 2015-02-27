using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.FNA;

public partial class SubModule_CM_RT_Pop_SetContractEndDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ContractID"] = Request.QueryString["ContractID"] == null ? 0 : int.Parse(Request.QueryString["ContractID"]);

            if ((int)ViewState["ContractID"] == 0 )
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }            
        }
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));
    }
    protected void bt_SetEndDate_Click(object sender, EventArgs e)
    {
        AC_AccountMonth maxendmonth = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetCurrentMonth() + 1).Model;
        CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
        if (_bll != null && _bll.Model.ApproveFlag == 1 && _bll.Model.State == 3)
        {
            if (Convert.ToDateTime(txt_EndDate.Text.ToString().Trim()) > _bll.Model.EndDate)
            {
                MessageBox.Show(this, "设定截止日期不能大于原有截止日期！");
                return;
            }
            if (_bll.Model.EndDate <= _bll.Model.BeginDate)
            {
                MessageBox.Show(this, "对不起，合同起始日期不能大于截止日期!");
                return;
            }
            _bll.Model.EndDate =Convert.ToDateTime(txt_EndDate.Text.ToString().Trim());

            if (_bll.Model.EndDate > maxendmonth.EndDate)
            {
                MessageBox.Show(this, "对不起，截止日期最大值为" + maxendmonth.EndDate.ToString("yyyy-MM-dd") + "。");
                return;
            }
            //导购工资
            if (_bll.Model.Classify == 3)
            {
                int lastmonth = _bll.CheckPMFeeApplyLastMonth();
                int conendmonth =AC_AccountMonthBLL.GetMonthByDate(_bll.Model.EndDate.AddDays(1));
                if (conendmonth <= lastmonth)
                {
                    MessageBox.Show(this, "对不起，该协议已生成预付管理费申请单，请重新填写终止日期(截止日期最小值为" + new AC_AccountMonthBLL(lastmonth).Model.EndDate.ToString("yyyy-MM-dd") + ")！");
                    return;
                }
            }

            _bll.Update();
            _bll.Disable((int)Session["UserID"]);
            MessageBox.ShowAndClose(this, "协议中止成功！");
        }
    }
}
