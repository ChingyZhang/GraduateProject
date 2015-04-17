using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.Common;

public partial class SubModule_CM_RT_RetailerPictureList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 0 : int.Parse(Request.QueryString["ClientType"]); //客户类型，２：经销商，３：终端门店


            #endregion

            tbx_begin.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");


            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                if (Request.QueryString["ClientType"] != null && client.Model.ClientType != (int)ViewState["ClientType"])
                {
                    Session["ClientID"] = null;
                    Response.Redirect(Request.Url.PathAndQuery);
                }
                ViewState["ClientType"] = client.Model.ClientType;

                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" +
                    client.Model.ClientType.ToString();// +"&OrganizeCity=" + client.Model.OrganizeCity.ToString();

                UploadFile1.RelateID = (int)ViewState["ClientID"];
                BindGrid();

                Header.Attributes["WebPageSubCode"] = "ClientType=" + ViewState["ClientType"].ToString();
            }
            else
            {
                if ((int)Session["AccountType"] == 2)
                {
                    Response.Redirect("ClientPictureList.aspx?ClientID=" + Session["UserID"].ToString());
                }
                else
                {
                    if ((int)ViewState["ClientType"] == 2)
                        MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                    else
                        MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
                }
            }
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
        {
            UploadFile1.RelateID = int.Parse(select_Client.SelectValue);
            BindGrid();
        }
        else
            MCSFramework.Common.MessageBox.Show(this, "请先选择要查询的客户！");
    }

    protected void select_Retailer_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (select_Client.SelectValue != "")
        {
            bt_Find_Click(null, null);
        }
    }

    private void BindGrid()
    {
        UploadFile1.RelateID = int.Parse(select_Client.SelectValue);
        UploadFile1.BeginTime = DateTime.Parse(this.tbx_begin.Text);
        UploadFile1.EndTime = DateTime.Parse(this.tbx_end.Text).AddDays(1);

        string extcondition = "";
        if (tbx_FindName.Text != "")
            extcondition = " Name like '%" + tbx_FindName.Text + "%' ";

        if (cb_OnlyPic.Checked)
        {
            if (extcondition != "") extcondition += " AND ";
            extcondition += "lower(ExtName) in ('bmp','jpg','gif','png')";
        }
        UploadFile1.ExtCondition = extcondition;

        UploadFile1.BindGrid();
    }

}
