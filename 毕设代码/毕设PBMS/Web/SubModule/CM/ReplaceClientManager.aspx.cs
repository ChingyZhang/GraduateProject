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

public partial class SubModule_CM_ReplaceClientManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ClientManager"] = Request.QueryString["ClientManager"] == null ? 0 : int.Parse(Request.QueryString["ClientManager"]);
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 0 : int.Parse(Request.QueryString["ClientType"]);

            if ((int)ViewState["ClientManager"] == 0 || (int)ViewState["ClientType"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }

            BindData();
        }
    }

    private void BindData()
    {
        Org_Staff staff = new Org_StaffBLL((int)ViewState["ClientManager"]).Model;
        if (staff != null)
        {
            select_OrgClientManager.SelectText = staff.RealName;
            select_OrgClientManager.SelectValue = staff.ID.ToString();

            lb_Prompt.Text = "注意：该操作将会替换所有原客户经理为【" + staff.RealName + "】的" + ((int)ViewState["ClientType"] == 2 ? "经销商" : "零售商") + "，请小心操作!";
        }
    }

    protected void bt_Replace_Click(object sender, EventArgs e)
    {
        if (select_OrgClientManager.SelectValue != "" && select_NewClientManager.SelectValue != "")
        {
            int count = CM_ClientBLL.ReplaceClientManager(int.Parse(select_OrgClientManager.SelectValue),
                int.Parse(select_NewClientManager.SelectValue), (int)ViewState["ClientType"]);
            MessageBox.ShowAndClose(this, "成功批量替换" + count.ToString() + "个客户的客户经理!");
        }
    }
    protected void select_OrgClientManager_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (select_OrgClientManager.SelectValue != "")
        {
            Org_Staff staff = new Org_StaffBLL(int.Parse(select_OrgClientManager.SelectValue)).Model;
            if (staff != null)
            {
                lb_Prompt.Text = "注意：该操作将会替换所有原客户经理为【" + staff.RealName + "】的" + ((int)ViewState["ClientType"] == 2 ? "经销商" : "零售商") + "，请小心操作!";
            }
        }
    }
}
