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

public partial class SubModule_FNA_FeeWriteoff_Pop_FeeWriteoffDetail_Taxes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["DetailID"] = Request.QueryString["DetailID"] == null ? 0 : int.Parse(Request.QueryString["DetailID"]);
            if ((int)ViewState["ID"] == 0)
            {

                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }
            if ((int)ViewState["DetailID"] > 0)
            {
                FNA_FeeWriteOffDetail m = new FNA_FeeWriteOffBLL().GetDetailModel((int)ViewState["DetailID"]);
                tbx_Taxes.Text = m.ApplyCost.ToString("#.##");
                tbx_Remark.Text = m.Remark;
 
            }
        }
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {

        FNA_FeeWriteOffBLL _bll = new FNA_FeeWriteOffBLL((int)ViewState["ID"]);
        FNA_FeeWriteOffDetail m = new FNA_FeeWriteOffDetail();

        if ((int)ViewState["DetailID"] > 0)
        {
            m = _bll.GetDetailModel((int)ViewState["DetailID"]);
            m.ApplyCost = decimal.Parse(tbx_Taxes.Text.Trim());
            m.WriteOffCost = m.ApplyCost;
            m.Remark = tbx_Remark.Text.Trim();
            _bll.UpdateDetail(m);

        }
        else
        {
            m.ID = 0;
            m.ApplyDetailID = 0;
            m.Client = _bll.Model.InsteadPayClient;
            m.BeginMonth = _bll.Model.AccountMonth;
            m.EndMonth = _bll.Model.AccountMonth;
            m.AccountTitle = 129;
            m.ApplyCost = decimal.Parse(tbx_Taxes.Text.Trim());
            m.WriteOffCost = m.ApplyCost;
            m.Remark = tbx_Remark.Text.Trim();      
            _bll.AddDetail(m);
        }
        Session["SuccessFlag"] = true;
        MessageBox.ShowAndClose(this, "税金调整成功。");
    }
}
