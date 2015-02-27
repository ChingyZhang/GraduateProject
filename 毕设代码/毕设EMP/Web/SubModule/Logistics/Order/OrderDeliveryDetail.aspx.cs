using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;

public partial class SubModule_Logistics_Order_OrderDeliveryDetail : System.Web.UI.Page
{
    protected bool bNoDelivery = false;      //未发货，界面发货数量字段可编辑
    protected bool bNoSignIn = false;        //未签收，界面签收数量字段可编辑

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            BindDropDown();

            #region 创建空的列表
            ListTable<ORD_OrderDeliveryDetail> _details = new ListTable<ORD_OrderDeliveryDetail>
                (new ORD_OrderDeliveryBLL((int)ViewState["ID"]).Items, "ApplyDetailID");
            ViewState["Details"] = _details;
            #endregion

            if ((int)ViewState["ID"] == 0)
            {
                #region 新费用申请时，初始化申请信息
                pn_OrderDelivery.Visible = false;

                bt_Approve.Visible = false;
                bt_ConfirmDelivery.Visible = false;
                bt_ConfirmSignIn.Visible = false;
                bt_Delete.Visible = false;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 1].Visible = true;
                #endregion

                if (tr_OrganizeCity.SelectValue != "1")
                    BindOrderApplyCanDelivery();
            }
            else
            {
                tr_OrganizeCity.Enabled = false;
                BindData();
            }

        }
    }

    #region 绑定下拉列表框
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
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        Label lb_OrganizeCity = (Label)pn_OrderDelivery.FindControl("ORD_OrderDelivery_OrganizeCity");
        lb_OrganizeCity.Text = tr_OrganizeCity.SelectText;

        Label lb_SheetCode = (Label)pn_OrderDelivery.FindControl("ORD_OrderDelivery_SheetCode");
        lb_SheetCode.Text = ORD_OrderDeliveryBLL.GenerateSheetCode(int.Parse(tr_OrganizeCity.SelectValue), AC_AccountMonthBLL.GetCurrentMonth());

        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + e.CurSelectIndex.ToString();

        BindOrderApplyCanDelivery();
    }
    #endregion

    #region 绑定尚可以继续发放的通过审批的定单申请列表（含产品明细）
    private void BindOrderApplyCanDelivery()
    {
        string condition = "";

        #region 组织查询条件
        if (string.IsNullOrEmpty(tr_OrganizeCity.SelectValue) || tr_OrganizeCity.SelectValue == "0")
        {
            MessageBox.Show(this, "必须选择管理片区");
            return;
        }
        else
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition = " ORD_OrderApply.OrganizeCity IN (" + orgcitys + ")";
        }

        if (select_Client.SelectValue != "" && select_Client.SelectValue != "0")
            condition += " AND ORD_OrderApply.Client=" + select_Client.SelectValue;

        if (tbx_SheetCode.Text != "")
            condition += " AND ORD_OrderApply.SheetCode like '%" + tbx_SheetCode.Text + "%'";

        #region 排除已选中到发放列表中的产品
        string applydetailids = "";
        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
        foreach (ORD_OrderDeliveryDetail item in _details.GetListItem())
        {
            applydetailids += item.ApplyDetailID.ToString() + ",";
        }
        if (applydetailids != "")
        {
            applydetailids = applydetailids.Substring(0, applydetailids.Length - 1);
            condition += " AND ORD_OrderApplyDetail.ID not in (" + applydetailids + ")";
        }
        #endregion

        #endregion

        gv_OrderAplyList.ConditionString = condition;
        gv_OrderAplyList.BindGrid();

        bNoDelivery = true;
    }
    #endregion

    private void BindData()
    {
        int id = (int)ViewState["ID"];
        ORD_OrderDelivery m = new ORD_OrderDeliveryBLL(id).Model;

        if (m == null) Response.Redirect("OrderDeliveryList.aspx");

        pn_OrderDelivery.BindData(m);

        #region 绑定当前申请单的管理片区
        Label lb_OrganizeCity = (Label)pn_OrderDelivery.FindControl("ORD_OrderDelivery_OrganizeCity");
        lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_Sys.dbo.Addr_OrganizeCity", m.OrganizeCity);
        tr_OrganizeCity.SelectValue = m.OrganizeCity.ToString();
        tr_OrganizeCity.Enabled = false;
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        #endregion

        #region 根据状态控制页面显示
        switch (m.State)
        {
            case 1:     //未发货
                if (m.ApproveFlag == 2)
                {   //未审核
                    BindOrderApplyCanDelivery();
                    pn_OrderDelivery.SetPanelVisible("Panel_LGS_OrderDeliveryDetail_02", false);
                    bt_ConfirmDelivery.Visible = false;
                    gv_OrderList.Columns[gv_OrderList.Columns.Count - 1].Visible = true;
                }
                else
                {
                    bt_Approve.Visible = false;
                    tr_FeeApplyList.Visible = false;
                    bt_Save.Visible = false;
                    bt_Delete.Visible = false;
                }
                bt_ConfirmSignIn.Visible = false;
                bNoDelivery = true;
                pn_OrderDelivery.SetPanelVisible("Panel_LGS_OrderDeliveryDetail_03", false);
                break;
            case 2:     //已发货
                bt_Save.Visible = false;
                bt_Delete.Visible = false;
                bt_Approve.Visible = false;
                bt_ConfirmDelivery.Visible = false;
                tr_FeeApplyList.Visible = false;
                bNoSignIn = true;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 4].Visible = true;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 3].Visible = true;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 2].Visible = true;
                pn_OrderDelivery.SetPanelEnable("Panel_LGS_OrderDeliveryDetail_02", false);
                break;
            case 3:     //已签收
                bt_Save.Visible = false;
                bt_Delete.Visible = false;
                bt_Approve.Visible = false;
                bt_ConfirmDelivery.Visible = false;
                bt_ConfirmSignIn.Visible = false;
                tr_FeeApplyList.Visible = false;
                bNoSignIn = false;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 4].Visible = true;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 3].Visible = true;
                gv_OrderList.Columns[gv_OrderList.Columns.Count - 2].Visible = true;
                pn_OrderDelivery.SetPanelEnable("Panel_LGS_OrderDeliveryDetail_02", false);
                pn_OrderDelivery.SetPanelEnable("Panel_LGS_OrderDeliveryDetail_03", false);
                break;
            default:
                break;
        }
        #endregion

        BindGrid();


    }

    #region 绑定定单发放明细列表
    private void BindGrid()
    {
        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
        gv_OrderList.BindGrid<ORD_OrderDeliveryDetail>(_details.GetListItem());

        //求合计
        decimal sumPrice = 0;
        foreach (ORD_OrderDeliveryDetail _detail in _details.GetListItem())
        {
            sumPrice += _detail.Price * _detail.DeliveryQuantity;
        }
        lb_TotalCost.Text = sumPrice.ToString("0.##");

        //如果已有报销明细了，则不允许再修改管理片区
        if (_details.GetListItem().Count > 0)
        {
            tr_OrganizeCity.Enabled = false;
        }
    }
    #endregion

    #region 将申请单加入发放明细中
    protected void bt_AddToDeliveryList_Click(object sender, EventArgs e)
    {
        if (tr_OrganizeCity.SelectValue == "0")
        {
            MessageBox.Show(this, "必须选择管理片区才能点添加");
            return;
        }

        tr_OrganizeCity.Enabled = false;     //开始添加报销后,不允许选择其他的管理片区,保证所有的费用明细都来源于一个管理片区

        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;

        foreach (GridViewRow row in gv_OrderAplyList.Rows)
        {
            CheckBox cb_Selected = (CheckBox)row.FindControl("cb_Selected");
            if (cb_Selected.Checked)
            {
                int applyid = (int)gv_OrderAplyList.DataKeys[row.RowIndex][0];
                int applydetialid = (int)gv_OrderAplyList.DataKeys[row.RowIndex][1];


                ORD_OrderApplyBLL applyBLL = new ORD_OrderApplyBLL(applyid);
                ORD_OrderApplyDetail applydetail = applyBLL.GetDetailModel(applydetialid);

                ORD_OrderDeliveryDetail m = new ORD_OrderDeliveryDetail();

                m.ApplyDetailID = applydetialid;
                m.Client = applyBLL.Model.Client;
                m.Product = applydetail.Product;
                m.Price = applydetail.Price;
                m.FactoryPrice = m.FactoryPrice == 0 ? applydetail.Price : m.FactoryPrice;
                m.DeliveryQuantity = applydetail.BookQuantity + applydetail.AdjustQuantity - applydetail.DeliveryQuantity;
                m.SignInQuantity = m.DeliveryQuantity;

                _details.Add(m);
            }
        }
        bNoDelivery = true;
        BindGrid();
        gv_OrderAplyList.PageIndex = 0;
        BindOrderApplyCanDelivery();
    }
    #endregion

    #region 判断发货数量是否超过可发货数量，并保存发货数量
    protected void tbx_DeliveryQuantity_TextChanged(object sender, EventArgs e)
    {
        TextBox tbx_sender = (TextBox)sender;
        int Quantity = 0;
        if (int.TryParse(tbx_sender.Text, out Quantity))
        {
            int rowindex = ((GridViewRow)tbx_sender.Parent.Parent).RowIndex;
            if (!VerifyDeliveryQuantity(rowindex))
            {
                tbx_sender.Text = "0";
                return;
            }

            //求合计
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            decimal sumPrice = 0;
            foreach (ORD_OrderDeliveryDetail _detail in _details.GetListItem())
            {
                sumPrice += _detail.Price * _detail.DeliveryQuantity;
            }
            lb_TotalCost.Text = sumPrice.ToString("0.##");
        }
        else
        {
            tbx_sender.Text = "0";
        }
    }

    /// <summary>
    /// 验证指定发货明细里指定行号填写的发货数量是否超过可发货数量，并保存发货数量
    /// </summary>
    /// <param name="Rowindex">行号</param>
    /// <returns>true:可以发货 false:不可发货</returns>
    private bool VerifyDeliveryQuantity(int Rowindex)
    {
        int applydetailid = (int)gv_OrderList.DataKeys[Rowindex]["ApplyDetailID"];

        ORD_OrderApplyDetail m = new ORD_OrderApplyBLL().GetDetailModel(applydetailid);
        PDT_Product product = new PDT_ProductBLL(m.Product).Model;

        int ApplyQuantity = m.BookQuantity + m.AdjustQuantity - m.DeliveryQuantity;

        TextBox tbx_DeliveryQuantity_T = (TextBox)gv_OrderList.Rows[Rowindex].FindControl("tbx_DeliveryQuantity_T");
        TextBox tbx_DeliveryQuantity = (TextBox)gv_OrderList.Rows[Rowindex].FindControl("tbx_DeliveryQuantity");
        int DeliveryQuantity = (int.Parse(tbx_DeliveryQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_DeliveryQuantity.Text));

        if (DeliveryQuantity <= ApplyQuantity)
        {
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            ORD_OrderDeliveryDetail d = _details[applydetailid.ToString()];
            d.DeliveryQuantity = DeliveryQuantity;
            d.SignInQuantity = d.DeliveryQuantity;      //签收数量默认为实发数量
            _details.Update(d);
            return true;
        }
        else
        {
            MessageBox.Show(this, "对不起，产品：" + product.FullName + "最多只允许发放" + GetQuantityString(m.Product, ApplyQuantity));
            return false;
        }
    }
    #endregion

    #region 转换产品数量为界面需要的格式
    protected string GetQuantityString(int product, int quantity)
    {
        if (quantity == 0) return "0";

        PDT_Product p = new PDT_ProductBLL(product).Model;

        string packing1 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
        string packing2 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

        string ret = "";
        if (quantity / p.ConvertFactor != 0) ret += (quantity / p.ConvertFactor).ToString() + packing1 + " ";
        if (quantity % p.ConvertFactor != 0) ret += (quantity % p.ConvertFactor).ToString() + packing2 + " ";
        return ret;
    }

    protected int GetTrafficeQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product).Model;

        return quantity / p.ConvertFactor;
    }

    protected int GetPackagingQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product).Model;

        return quantity % p.ConvertFactor;
    }

    protected string GetTrafficeName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
    }

    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }
    #endregion

    protected void gv_OrderList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_OrderList.DataKeys[e.RowIndex]["ApplyDetailID"];

        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
        _details.Remove(id.ToString());

        BindOrderApplyCanDelivery();
        BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindOrderApplyCanDelivery();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        #region 有效性校验
        if (tr_OrganizeCity.SelectValue == "0")
        {
            MessageBox.Show(this, "必须选择管理片区才能保存!");
            return;
        }
        #endregion

        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;

        if (_details.GetListItem().Count == 0)
        {
            MessageBox.Show(this, "在保存之前，发货明细不能为空!");
            return;
        }
        ORD_OrderDeliveryBLL bll;

        if ((int)ViewState["ID"] == 0)
            bll = new ORD_OrderDeliveryBLL();
        else
            bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);

        if ((int)ViewState["ID"] == 0)
        {
            bll.Model.AccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            bll.Model.State = 1;
            bll.Model.ApproveFlag = 2;
            bll.Model.SheetCode = ORD_OrderDeliveryBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);
            bll.Items = _details.GetListItem();

            ViewState["ID"] = bll.Add();

        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];

            bll.Update();

            #region 修改明细

            #region 增加发放明细时，再次判断该项费用是否超过发放数量
            bll.Items = new List<ORD_OrderDeliveryDetail>();
            foreach (ORD_OrderDeliveryDetail item in _details.GetListItem(ItemState.Added))
            {
                ORD_OrderApplyDetail apply = new ORD_OrderApplyBLL().GetDetailModel(item.ApplyDetailID);
                if (apply.BookQuantity + apply.AdjustQuantity - apply.DeliveryQuantity >= item.DeliveryQuantity)
                {
                    bll.Items.Add(item);
                }
            }
            bll.AddDetail();
            #endregion

            foreach (ORD_OrderDeliveryDetail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                bll.DeleteDetail(_deleted.ID);
            }

            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();

            #endregion
        }
        if (sender != null)
            MessageBox.ShowAndRedirect(this, "保存成功", "OrderDeliveryDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            bll.Approve((int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "审核成功", "OrderDeliveryList.aspx");
        }
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_Save_Click(null, null);

            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            bll.Delete();

            MessageBox.ShowAndRedirect(this, "删除发货单成功", "OrderDeliveryList.aspx");
        }
    }

    protected void bt_ConfirmDelivery_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            #region 再次验证发货数量是否超允许发货数量,以防止用户在多个页面分别填写发货单，并保存后再分批发放
            for (int i = 0; i < gv_OrderList.Rows.Count; i++)
            {
                if (!VerifyDeliveryQuantity(i)) return;
            }
            #endregion

            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            pn_OrderDelivery.GetData(bll.Model);

            if (bll.Model.Store == 0)
            {
                MessageBox.Show(this, "对不起，必须选择发货仓库！");
                return;
            }

            bll.Model["DeliveryTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bll.Model["DeliveryStaff"] = Session["UserID"].ToString();
            
            bll.Update();

            #region 保存实发数量
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();
            #endregion
            

            bll.Delivery((int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "发货单发放成功，已为在途状态，待签收！", "OrderDeliveryList.aspx");
        }
    }

    protected void bt_ConfirmSignIn_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            pn_OrderDelivery.GetData(bll.Model);
            if (bll.Model["SignInTime"] == "1900-01-01")
            {
                MessageBox.Show(this, "请选择实际到货日期!");
                return;
            }
            if (DateTime.Parse(bll.Model["SignInTime"]) > DateTime.Now)
            {
                MessageBox.Show(this, "实际到货日期不能大于今天!");
                return;
            }

            if (new CM_ClientBLL(bll.Model.Client).Model.ClientType == 2 && SVM_InventoryBLL.GetModelList("Client=" + bll.Model.Client.ToString() + " AND AccountMonth=" + bll.Model.AccountMonth.ToString() + " AND ApproveFlag=2").Count > 0)
            {
                MessageBox.ShowAndRedirect(this, "该经销商本月还有未审核库存，请先审核库存再作此操作。", "InventoryList.aspx?ClientID=" + bll.Model.Client.ToString());
                return;
            }

            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;

            #region 判断签收数量是否大于发放数量
            for (int i = 0; i < gv_OrderList.Rows.Count; i++)
            {
                int id = (int)gv_OrderList.DataKeys[i]["ID"];

                ORD_OrderDeliveryDetail m = new ORD_OrderDeliveryBLL().GetDetailModel(id);
                PDT_Product product = new PDT_ProductBLL(m.Product).Model;


                TextBox tbx_SignInQuantity_T = (TextBox)gv_OrderList.Rows[i].FindControl("tbx_SignInQuantity_T");
                TextBox tbx_SignInQuantity = (TextBox)gv_OrderList.Rows[i].FindControl("tbx_SignInQuantity");

                TextBox tbx_BadQuantity_T = (TextBox)gv_OrderList.Rows[i].FindControl("tbx_BadQuantity_T");
                TextBox tbx_BadQuantity = (TextBox)gv_OrderList.Rows[i].FindControl("tbx_BadQuantity");

                TextBox tbx_LostQuantity_T = (TextBox)gv_OrderList.Rows[i].FindControl("tbx_LostQuantity_T");
                TextBox tbx_LostQuantity = (TextBox)gv_OrderList.Rows[i].FindControl("tbx_LostQuantity");

                int SignInQuantity = (int.Parse(tbx_SignInQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_SignInQuantity.Text));
                int BadQuantity = (int.Parse(tbx_BadQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_BadQuantity.Text));
                int LostQuantity = (int.Parse(tbx_LostQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_LostQuantity.Text));

                if (SignInQuantity < 0 || BadQuantity < 0 || LostQuantity < 0)
                {
                    MessageBox.Show(this, "对不起，产品：" + product.FullName + "签收、丢失、破损的数量不能小于0");
                    return;
                }
                if (SignInQuantity + BadQuantity + LostQuantity != m.DeliveryQuantity)
                {
                    MessageBox.Show(this, "对不起，产品：" + product.FullName + "签收、丢失、破损的数量必需等于实发数量" + GetQuantityString(m.Product, m.DeliveryQuantity));
                    return;
                }

                m.SignInQuantity = SignInQuantity;
                m.BadQuantity = BadQuantity;
                m.LostQuantity = LostQuantity;

                _details.Update(m);
            }
            #endregion


            bll.Model["SignInStaff"] = Session["UserID"].ToString();
            bll.Model.State = 3;        //已签收

            bll.Update();

            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();

            MessageBox.ShowAndRedirect(this, "发货单签收成功！", "OrderDeliveryList.aspx");
        }
    }
}
