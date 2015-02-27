using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;

public partial class SubModule_CM_RT_RetailerFLContract_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["ContractID"] = Request.QueryString["ContractID"] == null ? 0 : int.Parse(Request.QueryString["ContractID"]);
        if ((int)ViewState["ContractID"] > 0)
        {
            Bind();
        }        
    }
    private void Bind()
    {
        CM_ContractBLL _cmbll = new CM_ContractBLL((int)ViewState["ContractID"]);
        lb_Code.Text = _cmbll.Model.ContractCode;
        lb_Supplier.InnerText = lb_Supplier2.InnerText = getSupplier(_cmbll.Model.Client);

        CM_ClientBLL _client = new CM_ClientBLL(_cmbll.Model.Client);
        lb_Client.InnerText = lb_Client3.InnerText = _client.Model.FullName;
        if (_client.Model["IsRMSClient"] != "2" && _client.Model["IsRMSClient"] != "0")
        {
            lbl_fjfjp.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;α金装袋装400G 、能慧全品项、新配方全品项、米粉全品项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_fjffl.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;17&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_fjfxdcp.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;α金装三联包&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_fjfxdcpfl.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_jfcp.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_jffl.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_wfljf.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;雅士利金装、超级金装、普装安贝慧全品项、α金装罐装900G及盒装400G&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        else
        {
            lbl_fjfjp.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;α金装袋装400G 、能慧全品项、新配方全品项、米粉全品项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_fjffl.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;17&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_fjfxdcp.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;α金装三联包&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_fjfxdcpfl.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_jfcp.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;普装安贝慧全品项、α金装罐装900G及盒装400G &nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_jffl.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lbl_wfljf.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;雅士利金装及超级金装 &nbsp;&nbsp;&nbsp;&nbsp;";
        }
        lb_StartTime.InnerText = _cmbll.Model.BeginDate.ToString("yyyy年MM月dd日");
        lb_EndTime.InnerText = _cmbll.Model.EndDate.ToString("yyyy年MM月dd日");
        lb_ClientAccount.InnerText = _cmbll.Model["BankAccountNo"].ToString();
        lb_AccountName.InnerText = _cmbll.Model["BankName"].ToString();
        lb_Client2.InnerText = _cmbll.Model["AccountName"];
        //lb_Percent.InnerText = "甲方:" + _cmbll.Model["RebateRate"] + "+" + "乙方:" + _cmbll.Model["DIRebateRate"] + "=" + ((decimal.Parse(_cmbll.Model["RebateRate"]) + decimal.Parse(_cmbll.Model["DIRebateRate"])).ToString("0.##"));
    }
    private string getSupplier(int ID)
    {
        CM_ClientBLL c = new CM_ClientBLL(ID);
        while (c.Model.ClientType >= 2 && c.Model["DIClassify"] != "1")
        {
            c = new CM_ClientBLL(c.Model.Supplier);
        }
        return c.Model.FullName;
    }
}
