using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL;

public partial class SubModule_FNA_FeeApply_Pop_AdjustApplyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }
            BindDropDown();
            BindData();
        }
    }
    private void BindDropDown()
    {

    }

    private void BindData()
    {
        FNA_FeeApplyDetail m = new FNA_FeeApplyBLL().GetDetailModel((int)ViewState["ID"]);
        if (m.AccountTitle == 82)
        {
            tb_DIFL.Visible = true;
        }

        lb_AccountTitle.Text = TreeTableBLL.GetFullPathName("MCS_Pub.dbo.AC_AccountTitle", m.AccountTitle);
        lb_ApplyCost.Text = m.ApplyCost.ToString("0.###");

        tbx_ApproveCost.Text = (m.ApplyCost + m.AdjustCost).ToString("0.###");

        lbl_DICost.Text = m.DICost.ToString("0.###");
        tbx_ApproveDICost.Text = (m.DICost + m.DIAdjustCost).ToString("0.###");
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        FNA_FeeApplyDetail m = new FNA_FeeApplyBLL().GetDetailModel((int)ViewState["ID"]);

        if (m.AccountTitle!=82&&decimal.Parse(tbx_ApproveCost.Text) > m.ApplyCost)
        {
            MessageBox.Show(this, "我司批复金额不能超过原申请金额！");
            return;
        }

        if (decimal.Parse(tbx_ApproveCost.Text) < 0)
        {
            MessageBox.Show(this, "我司批复金额不能小于0！");
            return;
        }
        if (m.AccountTitle == 82 && decimal.Parse(tbx_ApproveDICost.Text) < 0)
        {
            MessageBox.Show(this, "经销商批复金额不能小于0！");
            return;
        }
        decimal OldAdjustCost = m.AdjustCost;

        m.AdjustCost = Math.Round(decimal.Parse(tbx_ApproveCost.Text) - m.ApplyCost, 1, MidpointRounding.AwayFromZero);
        m.AdjustReason = tbx_AdjustReason.Text;
        decimal OldDIAdjustCost = m.DIAdjustCost;
        if (m.AccountTitle == 82)
        {
         
            m.DIAdjustCost =Math.Round(decimal.Parse(tbx_ApproveDICost.Text) - m.DICost, 1, MidpointRounding.AwayFromZero);
            m["DIAdjustReason"] = tbx_DIAdjustReason.Text.Trim();
        }
        if (m.AdjustCost != 0 && tbx_AdjustReason.Text.Trim() == "")
        {
            MessageBox.Show(this, "请填写我司调整原因！");
            return;
        }
        if (m.DIAdjustCost != 0 && tbx_DIAdjustReason.Text.Trim() == "")
        {
            MessageBox.Show(this, "请填写经销商调整原因！");
            return;
        }
        FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL(m.ApplyID);
        bll.UpdateDetail(m);

        //保存调整记录
        FNA_FeeApplyBLL.UpdateAdjustRecord(m.ApplyID, (int)Session["UserID"], m.AccountTitle, OldAdjustCost.ToString("0.##"), m.AdjustCost.ToString("0.##"), m.AdjustReason);

        Session["SuccessFlag"] = true;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
    }
}
