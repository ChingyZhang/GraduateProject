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
using MCSFramework.Model.CM;

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

        int forwarddays = ConfigHelper.GetConfigInt("GiftApplyForwardDays");
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>=GETDATE() AND BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();


        rbl_Type.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderType");
        rbl_Type.DataBind();
        rbl_Type.SelectedValue = "2";

        ddl_Publish.Items.Insert(0, new ListItem("请选择...", "0"));
        //ddl_Address.Items.Insert(0, new ListItem("请选择...", "0"));
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"";
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        BindPublish();
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }
    protected void rbl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPublish();
    }
    private void BindPublish()
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
             
            DataTable dt_publish = ORD_ApplyPublishBLL.GetbyOrganizeCity(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(rbl_Type.SelectedValue));
            ddl_Publish.DataSource = dt_publish;
            ddl_Publish.DataBind();


            if (dt_publish.Rows.Count == 0)
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
    protected void bt_Confirm_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_Publish.SelectedValue != "0")
        {
            int Client = 0;
            int.TryParse(select_Client.SelectValue, out Client);
            if (Client == 0)
            {
                MessageBox.Show(this, "请选择申请的客户!");
                return;
            }
            int Receiver = 0;
            int.TryParse(select_Receiver.SelectValue, out Receiver);
            if (Receiver == 0)
            {
                MessageBox.Show(this, "请选择收货客户！");
                return;
            }
            //if (ddl_Address.SelectedValue == "0")
            //{
            //    MessageBox.Show(this, "该客户没有对应ERP地址ID!");
            //    return;
            //}
            if (ORD_OrderApplyBLL.CheckClientCanApply(int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(select_Client.SelectValue), int.Parse(ddl_Publish.SelectedValue)) <= 0)
            {
                MessageBox.Show(this, "该申请品项目录当月申请已达到可申请上限!");
                ddl_Publish.Focus();
                return;
            }
            int Publish = int.Parse(ddl_Publish.SelectedValue);
            int OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            int AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);

            Session["LogisticsOrderApplyDetail"] = new ORD_OrderCartBLL(Publish, OrganizeCity, Client, AccountMonth,0, Receiver, "");
            Response.Redirect("OrderApplyDetail1.aspx");

        }
        else
        {
            ddl_Publish.Focus();
            MessageBox.Show(this, "请正确选择要申请的发布目录!");
        }
    }


    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        LoadGiftApplyAmount();
        select_Receiver.SelectText = "";
        select_Receiver.SelectValue = "0";
        int client = 0;
        int.TryParse(select_Client.SelectValue, out client);

        int OrganizePartCity_Level = ConfigHelper.GetConfigInt("OrganizePartCity-CityLevel");
     
        if (client > 0)
        {
            CM_ClientBLL _cmbll = new CM_ClientBLL(client);


            if (_cmbll != null && TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", _cmbll.Model.OrganizeCity, OrganizePartCity_Level) !=
                                                    TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", int.Parse(tr_OrganizeCity.SelectValue), OrganizePartCity_Level))
            {
                MessageBox.Show(this, "该经销商不在本营业部内，不能为其申请赠品订单，请重新选择经销商！");
                select_Client.SelectText = "";
                select_Client.SelectValue = "";
                return;
            }
            if (_cmbll != null && _cmbll.Model.FullName.Contains("[ERP已撤消]"))
            {
                MessageBox.Show(this, "该经销商在ERP中已撤销，请重新选择经销商！");
                select_Client.SelectText = "";
                select_Client.SelectValue = "";
                return;
            }
            select_Receiver.Enabled = true;
            select_Receiver.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&ExtCondition=\"(CM_Client.ID=" + select_Client.SelectValue + "$OR$Supplier=" + select_Client.SelectValue + ")$AND$MCS_SYS.dbo.UF_Spilt2(~MCS_CM.dbo.CM_Client~,CM_Client.ExtPropertys,~DIClassify~)<>~2~\"";
        }
        else
            select_Receiver.Enabled = false;


    }
    protected void ddl_Publish_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGiftApplyAmount();
    }

    private void LoadGiftApplyAmount()
    {
        int month = 0;
        int client = 0;

        int.TryParse(ddl_AccountMonth.SelectedValue, out month);
        int.TryParse(select_Client.SelectValue, out client);

        if (month > 0 && client > 0)
        {

            IList<ORD_GiftApplyAmount> lists = ORD_GiftApplyAmountBLL.GetModelList(string.Format("AccountMonth={0} AND Client={1}", month, client));
            if (lists.Count == 0)
            {
                ORD_GiftApplyAmountBLL.ComputAvailableAmount(month, client);
            }

            int pulishid = 0;
            int brand = 0;
            int giftclassify = 0;

            int.TryParse(ddl_Publish.SelectedValue, out pulishid);          

            if (pulishid > 0)
            {
                ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL(pulishid);
                int.TryParse(publish.Model["ProductBrand"], out brand);
                int.TryParse(publish.Model["GiftClassify"], out giftclassify);

                lists = ORD_GiftApplyAmountBLL.GetModelList(string.Format("AccountMonth={0} AND Client={1} AND Brand={2} AND Classify={3}", month, client, brand, giftclassify));
                if (lists.Count > 0)
                {
                    lb_GiftApplyAmount.Text = string.Format("销量:{0:0.#元},赠品费率:{1:0.###％},赠品总额度:{2:0.#元}, 赠品额度余额:{3:0.#元}", lists[0].SalesVolume, lists[0].FeeRate, lists[0].AvailableAmount + lists[0].PreBalance - lists[0].DeductibleAmount, lists[0].BalanceAmount);
                }
                else
                {
                    string[] nolimitbrand = Addr_OrganizeCityParamBLL.GetValueByType(1, 24).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
                    
                    lb_GiftApplyAmount.Text =nolimitbrand.Contains(publish.Model["ProductBrand"])?"本品类不限制申请额度":"上月无销量!";
                }
            }
        }
    }

    protected void select_Receiver_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        LoadGiftApplyAmount();
        //int receiver = 0;
        //int.TryParse(select_Receiver.SelectValue, out receiver);

        //if (receiver > 0)
        //{
        //    IList<CM_DIAddressID> _addressList = CM_ClientBLL.GetAddressByClient(receiver);
        //    ddl_Address.DataSource = _addressList;
        //    ddl_Address.DataBind();
        //    if (_addressList.Count == 0)
        //    {
        //        ddl_Address.Items.Insert(0, new ListItem("当前收货经销商无对应收货地址", "0"));
        //    }
        //}
    }
}
