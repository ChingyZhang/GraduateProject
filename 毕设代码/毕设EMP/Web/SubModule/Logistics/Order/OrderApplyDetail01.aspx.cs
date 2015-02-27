using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Logistics;
using MCSFramework.Model.Logistics;
using System.Data;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class SubModule_Logistics_Order_OrderApplyDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Type"] = Request.QueryString["Type"] == null ? 0 : int.Parse(Request.QueryString["Type"]); //类型，1.发布成品目录 2：发布促销品目录

            Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();
            BindDropDown();

            Session["LogisticsOrderApplyID"] = null;
            Session["LogisticsOrderApplyDetail"] = null;
            if ((int)ViewState["Type"] != 0)
            {
                rbl_Type.SelectedValue = ViewState["Type"].ToString();
                rbl_Type.Enabled = false;
            }

            if ((int)ViewState["Type"] == 2) tr_selectClient.Visible = false;

            BindPublish();
        }
    } 

    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion

        rbl_Type.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderType");
        rbl_Type.DataBind();
        rbl_Type.SelectedValue = "2";

        double DelayDays = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["OrderDelayDays"]);
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-DelayDays)).ToString();

        ddl_Publish.Items.Insert(0, new ListItem("请选择...", "0"));
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        BindPublish();
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }
    protected void rbl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPublish();
    }
    private void BindPublish()
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            DataTable dt = TreeTableBLL.GetFullPath("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", int.Parse(tr_OrganizeCity.SelectValue));

            string citys = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                citys += dt.Rows[i]["ID"].ToString();

                if (i < dt.Rows.Count - 1) citys += ",";
            }

            if (rbl_Type.SelectedValue == "1")
            {             
                ddl_OrderType.DataSource = DictionaryBLL.GetDicCollections("ORD_ProductOrderType");
                ddl_OrderType.DataTextField = "Value";
                ddl_OrderType.DataValueField = "Key";
                ddl_OrderType.DataBind();
                rbl_IsSpecial.DataSource = DictionaryBLL.GetDicCollections("PUB_YesOrNo");
                rbl_IsSpecial.DataBind();
                rbl_IsSpecial.SelectedValue = "2";           
           
                IList<PDT_Brand> _brandList = PDT_BrandBLL.GetModelList("IsOpponent=1");
                ddl_Brand.DataTextField = "Name";
                ddl_Brand.DataValueField = "ID";
                ddl_Brand.DataSource = _brandList;
                ddl_Brand.DataBind();
                ddl_Brand_SelectedIndexChanged(null, null);
              
            }
        }
    }
    protected void bt_Confirm_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_Publish.SelectedValue != "0")
        {
            int OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            int Publish = int.Parse(ddl_Publish.SelectedValue);
            int Client = 0;
            int.TryParse(select_Client.SelectValue, out Client);

            if (rbl_Type.SelectedValue == "1")
            {
                if (Client == 0)
                {
                    MessageBox.Show(this, "请选择申请的客户!");
                    return;
                }
                else
                {
                    OrganizeCity = new CM_ClientBLL(Client).Model.OrganizeCity;
                }
                if (rbl_IsSpecial.SelectedValue == "2" && ddl_OrderType.SelectedValue=="1")
                {
                    ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL();
                    double DelayDays = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["OrderDelayDays"]);
                    IList<ORD_OrderApply> orderlist = bll._GetModelList(" Client=" + Client.ToString() + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',4)='1'  AND AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',5)=" + ddl_Brand.SelectedValue);
                    if (orderlist.Count >= 2)
                    {
                        MessageBox.Show(this, "对不起，非特殊订单只能申请两次。");
                        return;
                    }
                }               
            }
            Session["LogisticsOrderApplyDetail"] = new ORD_OrderCartBLL(Publish, OrganizeCity,int.Parse(ddl_OrderType.SelectedValue),int.Parse(ddl_Brand.SelectedValue),int.Parse(rbl_IsSpecial.SelectedValue),Client);
            Response.Redirect("OrderApplyDetail1.aspx");
          

        }
        else
        {
            ddl_Publish.Focus();
            MessageBox.Show(this, "请正确选择要申请的产品目录!");
        }
    }


    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            DataTable dt = TreeTableBLL.GetFullPath("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", int.Parse(tr_OrganizeCity.SelectValue));

            string citys = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                citys += dt.Rows[i]["ID"].ToString();

                if (i < dt.Rows.Count - 1) citys += ",";
            }
            string condition = "ToOrganizeCity IN (" + citys + ") AND GETDATE() BETWEEN BeginTime AND DateAdd(day,1,EndTime) AND State=2 AND Type=" + rbl_Type.SelectedValue + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)="+ddl_Brand.SelectedValue;
            IList<ORD_ApplyPublish> list = ORD_ApplyPublishBLL.GetModelList(condition);
            ddl_Publish.DataTextField = "Topic";
            ddl_Publish.DataValueField = "ID";
            ddl_Publish.DataSource = list;
            ddl_Publish.DataBind();
            if (list.Count == 0)
            {
                ddl_Publish.Items.Insert(0, new ListItem("当前无可以申请的产品目录", "0"));
                bt_Confirm.Enabled = false;
            }
            else
            {
                bt_Confirm.Enabled = true;
            }
        }
    }
}
