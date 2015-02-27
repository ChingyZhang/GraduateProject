using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.CSO;
using MCSFramework.Model.CSO;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

public partial class SubModule_CSO_Pop_AdjustOfferBalanceFee : System.Web.UI.Page
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
            BindData();
        }
    }
    private void BindData()
    {
        CSO_OfferBalance_Detail detail = new CSO_OfferBalanceBLL().GetDetailModel((int)ViewState["ID"]);
        if (detail == null)
        {
            MessageBox.ShowAndClose(this, "参数错误!");
            return;
        }

        if (detail.OfferMan > 0)
        {
            CM_LinkMan doctor = new CM_LinkManBLL(detail.OfferMan).Model;
            if (doctor != null) lb_DoctorName.Text = doctor.Name;
        }

        lb_ActualFee.Text = detail.ActualFee.ToString("0.##");
        tbx_AdjustFee.Text = (0 - detail.AwardFee).ToString("0.##");
        tbx_AdjustReason.Text = detail["AdjustReason"];
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        CSO_OfferBalance_Detail detail = new CSO_OfferBalanceBLL().GetDetailModel((int)ViewState["ID"]);
        if (detail == null)
        {
            MessageBox.ShowAndClose(this, "参数错误!");
            return;
        }

        detail.AwardFee = 0 - decimal.Parse(tbx_AdjustFee.Text);
        detail.PayFee = detail.ActualFee + detail.AwardFee;
        detail["AdjustReason"] = tbx_AdjustReason.Text;

        if (!detail["AdjustReason"].EndsWith("(" + (string)Session["UserName"] + ")"))
            detail["AdjustReason"] += "(" + (string)Session["UserName"] + ")";

        if (detail.AwardFee > 0)
        {
            MessageBox.Show(this, "对不起，扣减金额不可为负！");
            return;
        }
        new CSO_OfferBalanceBLL(detail.Balance).UpdateDetail(detail);
        MessageBox.ShowAndClose(this, "扣减成功!");
    }
}
