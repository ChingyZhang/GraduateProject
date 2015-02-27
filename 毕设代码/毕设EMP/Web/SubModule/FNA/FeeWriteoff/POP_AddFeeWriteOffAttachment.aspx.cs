using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.CM;
using MCSFramework.Model.FNA;
using MCSFramework.Common;

public partial class SubModule_FNA_FeeWriteoff_POP_AddFeeWriteOffAttachment : System.Web.UI.Page
{
    protected TextBox tbx_DiscountRate, tbx_DiscountCost, tbx_InvoiceCost, tbx_RebateRate, tbx_WriteOffCost, tbx_WriteOffCost2;
    protected Button bt_ComputeMixRate;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));

        #region 页面加载初始化
        tbx_DiscountRate = (TextBox)pn_Detail.FindControl("FNA_FeeWriteOffDetail_DiscountRate");
        tbx_DiscountCost = (TextBox)pn_Detail.FindControl("FNA_FeeWriteOffDetail_DiscountCost");
        tbx_InvoiceCost = (TextBox)pn_Detail.FindControl("FNA_FeeWriteOffDetail_InvoiceCost");
        tbx_RebateRate = (TextBox)pn_Detail.FindControl("FNA_FeeWriteOffDetail_RebateRate");
        tbx_WriteOffCost = (TextBox)pn_Detail.FindControl("FNA_FeeWriteOffDetail_WriteOffCost");
        tbx_WriteOffCost2 = (TextBox)pn_Detail.FindControl("FNA_FeeWriteOffDetail_WriteOffCost2");

        if (tbx_InvoiceCost != null)
        {
            tbx_InvoiceCost.AutoPostBack = true;
            tbx_InvoiceCost.TextChanged += new EventHandler(tbx_InvoiceCost_TextChanged);
        }
        if (tbx_DiscountRate != null)
        {
            tbx_DiscountRate.AutoPostBack = true;
            tbx_DiscountRate.TextChanged += new EventHandler(tbx_InvoiceCost_TextChanged);
        }
        if (tbx_RebateRate != null)
        {
            tbx_RebateRate.AutoPostBack = true;
            tbx_RebateRate.TextChanged += new EventHandler(tbx_InvoiceCost_TextChanged);
        }
        if (tbx_WriteOffCost != null)
        {
            tbx_WriteOffCost.AutoPostBack = true;
            tbx_WriteOffCost.TextChanged += new EventHandler(tbx_InvoiceCost_TextChanged);
        }
        if (tbx_WriteOffCost2 != null)
        {
            tbx_WriteOffCost2.AutoPostBack = true;
            tbx_WriteOffCost2.TextChanged += new EventHandler(tbx_InvoiceCost_TextChanged);
            bt_ComputeMixRate = new Button();
            bt_ComputeMixRate.Text = "计算冲调占比";
            bt_ComputeMixRate.CssClass = "button";
            bt_ComputeMixRate.Width = new Unit(80);
            bt_ComputeMixRate.Click += new EventHandler(bt_ComputeMixRate_Click);
            tbx_WriteOffCost2.Parent.Controls.Add(bt_ComputeMixRate);
        }
        #endregion


        if (!Page.IsPostBack)
        {
            ViewState["DetailID"] = Request.QueryString["DetailID"] == null ? 0 : int.Parse(Request.QueryString["DetailID"]);
            if (Request.QueryString["DetailID"] != null)
            {
                BindData();
            }
        }
    }

    private void BindData()
    {
        if ((int)ViewState["DetailID"] != 0)
        {
            FNA_FeeWriteOffDetail _model = new FNA_FeeWriteOffBLL().GetDetailModel((int)ViewState["DetailID"]);
            if (_model == null)
            {
                MessageBox.ShowAndClose(this, "请先保存核销单后，再完善发票信息!");
                return;
            }
            FNA_FeeWriteOff writeoff = new FNA_FeeWriteOffBLL(_model.WriteOffID).Model;
            FNA_FeeApplyDetail _applydetail;

            if (_model.ApplyDetailID > 0)
            {
                _applydetail = new FNA_FeeApplyBLL().GetDetailModel(_model.ApplyDetailID);
                lbl_applyCode.Text = new FNA_FeeApplyBLL(_applydetail.ApplyID).Model.SheetCode;

                lb_AvailCost.Text = (_applydetail.AvailCost).ToString("0.00");
                ViewState["AvailCost"] = _applydetail.AvailCost;

            }

            lbl_Client.Text = _model.Client > 0 ? new CM_ClientBLL(_model.Client).Model.FullName : "";
            ViewState["ClientID"] = _model.Client;
            ViewState["Month"] = _model.BeginMonth;

            pn_Detail.BindData(_model);

            if (writeoff.State == 1)
            {
                if (tbx_RebateRate.Text == "" || tbx_RebateRate.Text == "0") tbx_RebateRate.Text = "100";
                if (tbx_DiscountRate.Text == "") tbx_DiscountRate.Text = "0";
                if (tbx_InvoiceCost.Text == "") tbx_InvoiceCost.Text = _model.WriteOffCost.ToString("0.00");
                if (tbx_WriteOffCost2.Text == "") tbx_WriteOffCost2.Text = "0.00";
            }
            ComputInvoiceCost();

            UploadFile1.RelateID = (int)ViewState["DetailID"];

            if (writeoff.State >= 2)
            {
                if (bt_ComputeMixRate != null) bt_ComputeMixRate.Visible = false;

                UploadFile1.CanDelete = false;
                pn_Detail.SetControlsEnable(false);
                bt_Save.Visible = false;
            }
            if (writeoff.State >= 3)
            {
                UploadFile1.CanUpload = false;
            }
        }
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["DetailID"] != 0)
        {
            FNA_FeeWriteOffDetail _modeldetail = new FNA_FeeWriteOffBLL().GetDetailModel((int)ViewState["DetailID"]);
            FNA_FeeWriteOffBLL _bll = new FNA_FeeWriteOffBLL(_modeldetail.WriteOffID);
            pn_Detail.GetData(_modeldetail);

            if (_modeldetail["InvoiceClassify"] == "1" && _modeldetail["VATInvoiceNO"] == "")
            {
                MessageBox.Show(this, "请录入增值税发票号码!");
                return;
            }
            if (_modeldetail["InvoiceClassify"] == "1" && _modeldetail["VATInvoiceNO"] != "" &&
                FNA_FeeWriteOffBLL.VerifyNO(_modeldetail.ID, 1, _modeldetail["VATInvoiceNO"].Trim()) > 0)
            {
                MessageBox.Show(this, "增值税发票号码重复!");
                return;
            }

            if (_modeldetail["AcceptanceNO"] != "" &&
                FNA_FeeWriteOffBLL.VerifyNO(_modeldetail.ID, 2, _modeldetail["VATInvoiceNO"].Trim()) > 0)
            {
                MessageBox.Show(this, "验收凭证号码重复!");
                return;
            }


            _bll.UpdateDetail(_modeldetail);

            Session["POP_AddFeeWriteOffAttachment"] = true;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
        }
    }
    void tbx_InvoiceCost_TextChanged(object sender, EventArgs e)
    {
        ComputInvoiceCost();
    }
    protected void ComputInvoiceCost()
    {
        if ((int)ViewState["DetailID"] != 0)
        {
            decimal InvoiceCost, DiscountRate, DiscountCost, RebateRate, WriteOffCost, WriteOffCost2;
            if (decimal.TryParse(tbx_InvoiceCost.Text, out InvoiceCost) &&
                decimal.TryParse(tbx_DiscountRate.Text, out DiscountRate) &&
                decimal.TryParse(tbx_RebateRate.Text, out RebateRate) &&
                decimal.TryParse(tbx_WriteOffCost.Text, out WriteOffCost) &&
                decimal.TryParse(tbx_WriteOffCost2.Text, out WriteOffCost2))
            {
                if (DiscountRate > 100)
                {
                    DiscountRate = 100;
                    tbx_DiscountRate.Text = "100";
                }
                if (RebateRate > 100)
                {
                    RebateRate = 100;
                    tbx_RebateRate.Text = "100";
                }

                DiscountCost = InvoiceCost * DiscountRate / 100;
                tbx_DiscountCost.Text = DiscountCost.ToString("0.00");

                if ((WriteOffCost + WriteOffCost2) > (InvoiceCost - DiscountCost) * RebateRate / 100)
                {
                    WriteOffCost = (InvoiceCost - DiscountCost) * RebateRate / 100 - WriteOffCost2;
                    if (WriteOffCost < 0)
                    {
                        WriteOffCost = (InvoiceCost - DiscountCost) * RebateRate / 100;
                        WriteOffCost2 = 0;
                    }
                }

                if (ViewState["AvailCost"] != null && WriteOffCost > (decimal)ViewState["AvailCost"])
                {
                    WriteOffCost = (decimal)ViewState["AvailCost"];
                }

                tbx_WriteOffCost.Text = WriteOffCost.ToString("0.00");
                tbx_WriteOffCost2.Text = WriteOffCost2.ToString("0.00");
            }
        }


    }
    protected void bt_ComputeMixRate_Click(object sender, EventArgs e)
    {
        if (ViewState["ClientID"] != null && ViewState["Month"] != null)
        {
            int client = (int)ViewState["ClientID"];
            int month = (int)ViewState["Month"];
            int brand = ConfigHelper.GetConfigInt("MixesBrandID");

            decimal mixesrate = new CM_ClientBLL(client).GetBrandSalesVolumeRate(month, brand);

            decimal InvoiceCost, DiscountRate, RebateRate, WriteOffCost, WriteOffCost2;
            if (decimal.TryParse(tbx_InvoiceCost.Text, out InvoiceCost) &&
                decimal.TryParse(tbx_DiscountRate.Text, out DiscountRate) &&
                decimal.TryParse(tbx_RebateRate.Text, out RebateRate))
            {
                decimal summary = InvoiceCost * (1 - DiscountRate / 100) * RebateRate / 100;

                WriteOffCost = summary * (1 - mixesrate);
                WriteOffCost2 = summary * mixesrate;

                if (ViewState["AvailCost"] != null && WriteOffCost > (decimal)ViewState["AvailCost"])
                {
                    WriteOffCost = (decimal)ViewState["AvailCost"];
                }

                tbx_WriteOffCost.Text = WriteOffCost.ToString("0.00");
                tbx_WriteOffCost2.Text = WriteOffCost2.ToString("0.00");

                MessageBox.Show(this, string.Format("合计总报销金额:{0:0.##元},冲调占当月该客户销量的{1:0.0%},冲调需承担{2:0.00元}费用!", summary, mixesrate, WriteOffCost2));
            }

        }
    }
}
