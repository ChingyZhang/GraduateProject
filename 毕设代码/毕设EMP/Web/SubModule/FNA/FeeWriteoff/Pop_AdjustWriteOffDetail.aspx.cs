using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.Pub;

public partial class SubModule_FNA_FeeWriteoff_Pop_AdjustWriteOffDetail : System.Web.UI.Page
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
        rbl_AdjustMode.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeWriteOffAdjustMode");
        rbl_AdjustMode.DataBind();
        ddl_DeductReason.DataSource = DictionaryBLL.GetDicCollections("FNA_DeductReason");
        ddl_DeductReason.DataBind();        
    }

    private void BindData()
    {
        FNA_FeeWriteOffDetail m = new FNA_FeeWriteOffBLL().GetDetailModel((int)ViewState["ID"]);

        lb_AccountTitle.Text = TreeTableBLL.GetFullPathName("MCS_Pub.dbo.AC_AccountTitle", m.AccountTitle);
        lb_ApplyCost.Text = m.ApplyCost.ToString("0.###");
        lb_WriteOffCost.Text = m.WriteOffCost.ToString("0.###");
        tbx_ApproveCost.Text = (m.WriteOffCost + m.AdjustCost).ToString("0.###");

        rbl_AdjustMode.SelectedValue = m.AdjustMode.ToString();

        try
        {
            if (new FNA_FeeWriteOffBLL(m.WriteOffID).Model["HasFeeApply"] == "N")
            {
                rbl_AdjustMode.Items.FindByValue("1").Enabled = false;        //无申请直接核销的单子，不可选择“退回重新再核销”
                rbl_AdjustMode.Items.FindByValue("2").Selected = true;
            }
        }
        catch { }
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        FNA_FeeWriteOffDetail m = new FNA_FeeWriteOffBLL().GetDetailModel((int)ViewState["ID"]);

        if (decimal.Parse(tbx_ApproveCost.Text) > m.WriteOffCost)
        {
            MessageBox.Show(this, "批复核销金额不能超过申请核销金额！");
            return;
        }

        decimal OldAdjustCost = m.AdjustCost;
        decimal AdjustCost = decimal.Parse(tbx_ApproveCost.Text) - m.WriteOffCost;

        m.AdjustReason = tbx_AdjustReason.Text;
        m["DeductReason"] = ddl_DeductReason.SelectedValue;
        if (AdjustCost != OldAdjustCost)
        {
            if (rbl_AdjustMode.SelectedValue == "")
            {
                MessageBox.Show(this, "请选择正确的调整方式！");
                return;
            }
            else
            {
                FNA_FeeWriteOffDetail_AdjustInfoBLL _Adjustbll = new FNA_FeeWriteOffDetail_AdjustInfoBLL();

                _Adjustbll.Model.WriteOffDetailID = (int)ViewState["ID"];
                _Adjustbll.Model.AdjustMode = int.Parse(rbl_AdjustMode.SelectedValue);
                _Adjustbll.Model.AdjustCost = decimal.Parse(tbx_ApproveCost.Text) - m.WriteOffCost;
                _Adjustbll.Model.AdjustReason = ddl_DeductReason.SelectedValue;
                _Adjustbll.Model.Remark = tbx_AdjustReason.Text;
                _Adjustbll.Model.InsertStaff = (int)Session["UserID"];
                _Adjustbll.Add();

                Session["SuccessFlag"] = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);

            }
        }
        else
        {
            MessageBox.Show(this, "批复金额尚没有修改！");
            return;
        }

    }
    protected void tbx_ApproveCost_TextChanged(object sender, EventArgs e)
    {
        decimal approvecost = 0;
        if (decimal.TryParse(tbx_ApproveCost.Text, out approvecost))
        {
            lbl_DeductCost.Text = (decimal.Parse(lb_WriteOffCost.Text) - approvecost).ToString();
        }
    }
}
