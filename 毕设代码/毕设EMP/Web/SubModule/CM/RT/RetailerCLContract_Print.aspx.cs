using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
public partial class SubModule_CM_RT_RetailerCLContract_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["ContractID"] = Request.QueryString["ContractID"] == null ? 0 : int.Parse(Request.QueryString["ContractID"]);
        if ((int)ViewState["ContractID"] > 0)
        {
            BindData();
        }        
    }
    private void BindData()
    {
        CM_ContractBLL _cmbll = new CM_ContractBLL((int)ViewState["ContractID"]);
        lb_client.Text = new CM_ClientBLL(_cmbll.Model.Client).Model.FullName;
        lbl_partyB.Text = new CM_ClientBLL(_cmbll.Model.Client).Model.FullName;
        IList<CM_ContractDetail> _listDetail = _cmbll.Items;
        foreach (CM_ContractDetail detail in _listDetail)
        {
            chk_BZ.Checked = (detail.AccountTitle == 142 || detail.AccountTitle == 186);//包柱
            chk_ZG.Checked = (detail.AccountTitle == 143 || detail.AccountTitle == 187);//专柜
            chk_DJ.Checked = (detail.AccountTitle == 144 || detail.AccountTitle == 188);//端架
            chk_DT.Checked = (detail.AccountTitle == 145 || detail.AccountTitle == 189);//端架
        }
        lbl_time.Text = _cmbll.Model.BeginDate.ToString("yyyy年MM月dd日") + "-----" + _cmbll.Model.EndDate.ToString("yyyy年MM月dd日");
        lb_TotalCost.Text = _listDetail.Sum(p => p.ApplyLimit).ToString("0.##");
        lb_TotalCostCN.Text = MCSFramework.Common.Rmb.CmycurD(lb_TotalCost.Text);
        lbl_SignMan.Text = _cmbll.Model.SignMan != "" ? _cmbll.Model.SignMan : "_________________";
    }
}
