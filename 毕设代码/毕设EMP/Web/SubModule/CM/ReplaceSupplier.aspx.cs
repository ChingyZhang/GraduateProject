using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Model.CM;

public partial class SubModule_CM_ReplaceSupplier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Supplier"] = Request.QueryString["Supplier"] == null ? 0 : int.Parse(Request.QueryString["Supplier"]);
            ViewState["Supplier2"] = Request.QueryString["Supplier2"] == null ? 0 : int.Parse(Request.QueryString["Supplier2"]);

            if ((int)ViewState["Supplier"] == 0 && (int)ViewState["Supplier2"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }
            if ((int)ViewState["Supplier"] != 0)
                BindData();
            if ((int)ViewState["Supplier2"] != 0)
                BindData2();
        }
    }

    private void BindData()
    {
        CM_Client client = new CM_ClientBLL((int)ViewState["Supplier"]).Model;
        if (client != null)
        {
            select_OrgSupplier.SelectText = client.FullName;
            select_OrgSupplier.SelectValue = client.ID.ToString();

            lb_Prompt.Text = "注意：该操作将会替换原供货商为【" + client.FullName + "】的所有客户(含分销商及零售商)，请小心操作!";
        }

        select_OrgSupplier.PageUrl += "&OrganizeCity=" + client.OrganizeCity;
        select_NewSupplier.PageUrl += "&OrganizeCity=" + client.OrganizeCity;
    }

    private void BindData2()
    {
        CM_Client client = new CM_ClientBLL((int)ViewState["Supplier2"]).Model;
        if (client != null)
        {
            select_OrgSupplier.SelectText = client.FullName;
            select_OrgSupplier.SelectValue = client.ID.ToString();

            lb_Prompt.Text = "注意：该操作将会替换原赠品供货商为【" + client.FullName + "】的所有客户(含分销商及零售商)，请小心操作!";
        }

        select_OrgSupplier.PageUrl += "&OrganizeCity=" + client.OrganizeCity;
        select_NewSupplier.PageUrl += "&OrganizeCity=" + client.OrganizeCity;
    }


    protected void bt_Replace_Click(object sender, EventArgs e)
    {
        if (select_OrgSupplier.SelectValue != "" && select_NewSupplier.SelectValue != "")
        {
            int newsupplier=int.Parse(select_NewSupplier.SelectValue);
            int count = CM_ClientBLL.ReplaceSupplier(int.Parse(select_OrgSupplier.SelectValue),(int)ViewState["Supplier"] != 0?newsupplier:0,(int)ViewState["Supplier2"] != 0?newsupplier:0 );
            MessageBox.ShowAndClose(this, "成功批量替换" + count.ToString() + "个客户的"+ ((int)ViewState["Supplier"] != 0?"成品":"赠品")+"供货商!");
        }
    }
    protected void select_OrgSupplier_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (select_OrgSupplier.SelectValue != "")
        {
            CM_Client client = new CM_ClientBLL(int.Parse(select_OrgSupplier.SelectValue)).Model;
            if (client != null)
            {
                lb_Prompt.Text = "注意：该操作将会替换原" + ((int)ViewState["Supplier"] != 0?"成品":"赠品")+ "供货商为【" + client.FullName + "】的所有客户(含分销商及零售商)，请小心操作!";
            }
        }
    }
}
