using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;

public partial class SubModule_SVM_JXCSummary_Edit : System.Web.UI.Page
{
    protected bool bEditPurchaseVolume = false;     //是否可填报 本期进货
    protected bool bEditSalesVolume = false;          //是否可填报 本期销售
    protected bool bEditRecallVolume = false;          //是否可填报 下游退货
    protected bool bEditReturnedVolume = false;     //是否可填报 退货
    protected bool bEditGiftVolume = false;             //是否可填报 发出买赠
    protected bool bEditEndingInventory = false;     //是否可填报 期末实际盘存
    protected bool bEditStaleInventory = false;        //是否可填报 界期品库存

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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

            ViewState["IsOpponent"] = Request.QueryString["IsOpponent"] == null ? 1 : int.Parse(Request.QueryString["IsOpponent"]);
            #endregion

            BindDropDown();

            if (Request.QueryString["AccountMonth"] != null) ddl_Month.SelectedValue = Request.QueryString["AccountMonth"];

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                #region 载入客户信息
                ViewState["ClientType"] = client.Model.ClientType;
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" +
                    client.Model.ClientType.ToString() + "&OrganizeCity=" + client.Model.OrganizeCity.ToString();
                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();

                select_Client.Enabled = false;
                tr_OrganizeCity.Enabled = false;

                #region 判断传入客户参数的客户类别
                if (client.Model.ClientType == 2 && client.Model["DIClassify"] == "1")
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=1";    //1:经销商
                else if (client.Model.ClientType == 2 && client.Model["DIClassify"] != "1")
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=2";    //2:分销商
                else if (client.Model.ClientType == 3 && client.Model["IsPromote"] == "1")
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=3";    //3:促销门店
                else
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=4";    //4:非促销门店
                #endregion

                #region 获取填报权限
                int module = 0;
                string extCode = "";      //分销商、返利店的进销存填报动作权限的后缀为2
                switch (Header.Attributes["WebPageSubCode"])
                {
                    case "ClientClassify=1":        //1:经销商
                        module = 1220;
                        break;
                    case "ClientClassify=2":        //2:分销商
                        module = 1220;
                        extCode = "2";
                        break;
                    case "ClientClassify=3":        //3:促销店
                        module = 1120;
                        break;
                    case "ClientClassify=4":        //4:返利店
                        module = 1120;
                        extCode = "2";
                        break;
                }

                ViewState["bEditPurchaseVolume"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditPurchaseVolume" + extCode);
                ViewState["bEditSalesVolume"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditSalesVolume" + extCode);
                ViewState["bEditRecallVolume"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditRecallVolume" + extCode);
                ViewState["bEditReturnedVolume"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditReturnedVolume" + extCode);
                ViewState["bEditGiftVolume"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditGiftVolume" + extCode);
                ViewState["bEditEndingInventory"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditEndingInventory" + extCode);
                ViewState["bEditStaleInventory"] = Right_Assign_BLL.GetAccessRight((string)Session["UserName"], module, "EditStaleInventory" + extCode);

                bEditPurchaseVolume = (bool)ViewState["bEditPurchaseVolume"];
                bEditSalesVolume = (bool)ViewState["bEditSalesVolume"];
                bEditRecallVolume = (bool)ViewState["bEditRecallVolume"];
                bEditReturnedVolume = (bool)ViewState["bEditReturnedVolume"];
                bEditGiftVolume = (bool)ViewState["bEditGiftVolume"];
                bEditEndingInventory = (bool)ViewState["bEditEndingInventory"];
                bEditStaleInventory = (bool)ViewState["bEditStaleInventory"];
                #endregion

                if (Request.QueryString["AccountMonth"] != null) //||
                //AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(-7)) < AC_AccountMonthBLL.GetMonthByDate(DateTime.Today) ||
                //AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(+7)) > AC_AccountMonthBLL.GetMonthByDate(DateTime.Today))
                {
                    //有指定会计月，或当前为月初前7天或是月末前7天，默认开始填报
                    int month = int.Parse(ddl_Month.SelectedValue);
                    SVM_JXCSummaryBLL.Init(int.Parse(select_Client.SelectValue), month, (int)ViewState["IsOpponent"]);
                    BindGrid();
                }
                #endregion
            }
            else
            {
                Response.Redirect("~/SubModule/desktop.aspx");
            }
        }

        #region 获取权限
        bEditPurchaseVolume = (bool)ViewState["bEditPurchaseVolume"];
        bEditSalesVolume = (bool)ViewState["bEditSalesVolume"];
        bEditRecallVolume = (bool)ViewState["bEditRecallVolume"];
        bEditReturnedVolume = (bool)ViewState["bEditReturnedVolume"];
        bEditGiftVolume = (bool)ViewState["bEditGiftVolume"];
        bEditEndingInventory = (bool)ViewState["bEditEndingInventory"];
        bEditStaleInventory = (bool)ViewState["bEditStaleInventory"];
        #endregion

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND EndDate>DateAdd(month,-3,GETDATE())");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(-7)).ToString();
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        switch ((int)ViewState["ClientType"])
        {
            case 2:
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                break;
            case 3:
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=3&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                break;
        }
        select_Client.SelectText = "";
        select_Client.SelectValue = "";
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (e.SelectValue != "")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(e.SelectValue)).Model.OrganizeCity.ToString();
        }
    }
    #endregion

    protected void bt_Load_Click(object sender, EventArgs e)
    {
        int client = int.Parse(select_Client.SelectValue);
        int month = int.Parse(ddl_Month.SelectedValue);

        IList<SVM_JXCSummary> lists = SVM_JXCSummaryBLL.GetModelList("AccountMonth=" + month.ToString() +
            " AND Client=" + client.ToString()+@" AND Product IN (SELECT PDT_Product.ID FROM MCS_PUB.dbo.PDT_Product 
			            INNER JOIN MCS_Pub.dbo.PDT_Brand ON PDT_Product.Brand = PDT_Brand.ID AND PDT_Brand.IsOpponent=" + ViewState["IsOpponent"].ToString() + ")");

        if (lists.Count > 0 && lists.Where(p => p.ApproveFlag == 2).Count() == 0)
        {
            MessageBox.ShowAndRedirect(this, "对不起，该月份进销存数据已被审核通过，不可再填报!",
                "JXCSummary_Detail.aspx?ClientID=" + client.ToString() + "&AccountMonth=" + month.ToString());
            return;
        }
        else
        {
            ddl_Month.Enabled = false;
            SVM_JXCSummaryBLL.Init(client, month, (int)ViewState["IsOpponent"]);

            gv_List.PageIndex = 0;
            BindGrid();
        }
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue == "")
        {
            MessageBox.Show(this, "对不起，您必须选择一个客户！");
            return;
        }

        SVM_JXCSummaryBLL.DeleteJXC(int.Parse(select_Client.SelectValue), int.Parse(ddl_Month.SelectedValue));
        MessageBox.ShowAndRedirect(this, "删除成功！", "JXCSummary_List.aspx?ClientID=" + select_Client.SelectValue);
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);

        if (select_Client.SelectValue == "")
        {
            MessageBox.Show(this, "对不起，您必须选择一个客户进行填报！");
            return;
        }

        IList<SVM_JXCSummary> lists = SVM_JXCSummaryBLL.GetModelList("AccountMonth=" + month.ToString() + " AND Client=" + select_Client.SelectValue + @" AND Product IN (SELECT PDT_Product.ID FROM MCS_PUB.dbo.PDT_Product 
			            INNER JOIN MCS_Pub.dbo.PDT_Brand ON PDT_Product.Brand = PDT_Brand.ID AND PDT_Brand.IsOpponent=" + ViewState["IsOpponent"].ToString() + ")");

        if (lists.Count != 0)
        {
            gv_List.BindGrid(lists.OrderBy(p => p.ProductCode).ToList());
            ddl_Month.Enabled = false;
            bt_Load.Enabled = false;
            if (lists.Where(p => p.ApproveFlag == 2).Count() > 0)
            {
                bt_Save.Enabled = true;
                bt_Delete.Enabled = true;
            }
            bt_Return.Enabled = true;
        }
        else
        {
            MessageBox.Show(this, "对不起，当前客户初始化进销存数据失败,请检查是否正确设定了该客户的价表!");
            return;
        }

    }
    protected string GetPDTBrandName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;
        PDT_Brand b = new PDT_BrandBLL(p.Brand).Model;

        if (b != null)
            return b.Name;
        else
            return "";
    }
    protected string GetPDTClassifyName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;
        PDT_Classify c = new PDT_ClassifyBLL(p.Classify).Model;

        if (c != null)
            return c.Name;
        else
            return "";
    }
    #endregion

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            SVM_JXCSummaryBLL jxc = new SVM_JXCSummaryBLL(id);
            if (jxc.Model == null) continue;

            string productname = row.Cells[2].Text; //new PDT_ProductBLL(jxc.Model.Product, true).Model.ShortName;

            TextBox tbx = null;
            int quantity = 0;

            tbx = (TextBox)row.FindControl("tbx_PurchaseVolume");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.PurchaseVolume = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“本期进货数量”格式填写错误，必须为整型！");
                return;
            }

            tbx = (TextBox)row.FindControl("tbx_RecallVolume");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.RecallVolume = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“下游退货”数量格式填写错误，必须为整型！");
                return;
            }

            tbx = (TextBox)row.FindControl("tbx_SalesVolume");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.SalesVolume = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“本期销售”数量格式填写错误，必须为整型！");
                return;
            }

            tbx = (TextBox)row.FindControl("tbx_GiftVolume");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.GiftVolume = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“发出买赠”格式填写错误，必须为整型！");
                return;
            }

            tbx = (TextBox)row.FindControl("tbx_ReturnedVolume");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.ReturnedVolume = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“退货数量”数量格式填写错误，必须为整型！");
                return;
            }

            tbx = (TextBox)row.FindControl("tbx_EndingInventory");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.EndingInventory = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“期末实际盘存”数量格式填写错误，必须为整型！");
                return;
            }

            tbx = (TextBox)row.FindControl("tbx_StaleInventory");
            if (tbx != null && int.TryParse(tbx.Text, out quantity))
            {
                jxc.Model.StaleInventory = quantity;
            }
            else if (tbx != null && tbx.Text != "")
            {
                tbx.Focus();
                MessageBox.Show(this, productname + "的“界期品库存”数量格式填写错误，必须为整型！");
                return;
            }

            //非促销店本期销售默认为本期进货
            if (Header.Attributes["WebPageSubCode"] == "ClientClassify=4" && !bEditSalesVolume)
                jxc.Model.SalesVolume = jxc.Model.PurchaseVolume - jxc.Model.ReturnedVolume;

            jxc.Update();
        }

        SVM_JXCSummaryBLL.ComputInventory(int.Parse(select_Client.SelectValue), int.Parse(ddl_Month.SelectedValue));
        BindGrid();

        MessageBox.Show(this, "保存成功！");
    }

    protected void bt_Return_Click(object sender, EventArgs e)
    {
        Response.Redirect("JXCSummary_Detail.aspx?ClientID=" + select_Client.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue);
    }


}
