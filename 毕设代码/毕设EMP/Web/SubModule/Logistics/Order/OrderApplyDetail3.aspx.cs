using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using System.Collections.Generic;

public partial class SubModule_Logistics_Order_OrderApplyDetail3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] > 0)
            {
                Session["LogisticsOrderApplyID"] = null;            //申请单ID
                Session["LogisticsOrderApplyDetail"] = null;        //购物车明细                
            }
            else if (Session["LogisticsOrderApplyID"] != null)
            {
                //Session中存有申请单ID，说明是对现有申请单进行编辑
                ViewState["ID"] = (int)Session["LogisticsOrderApplyID"];
            }

            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
            else if (Session["LogisticsOrderApplyDetail"] != null)
            {
                #region 新增申请单
                //当无申请单ID，但有购物车中有明细时，则说明是新增申请单
                ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];

                if (Session["LogisticsOrderApplyID"] == null)
                {
                    ViewState["Publish"] = cart.Publish;
                    ORD_ApplyPublish publish = new ORD_ApplyPublishBLL(cart.Publish).Model;
                    if (publish == null) Response.Redirect("OrderApplyDetail0.aspx");

                    int month = publish.AccountMonth;

                    #region 初始化申请单详细信息控件
                    ORD_OrderApply apply = new ORD_OrderApply();
                    apply.OrganizeCity = cart.OrganizeCity;
                    apply.AccountMonth = cart.AccountMonth;
                    apply.Type = cart.Type;
                    apply.Client = cart.Client;
                    apply.PreArrivalDate = DateTime.Today.AddMonths(1);
                    apply["ProductBrand"] = cart.Brand.ToString();
                    apply["GiftClassify"] = cart.GiftClassify.ToString();
                    apply.State = 1;
                    apply.InsertStaff = (int)Session["UserID"];
                    apply.InsertTime = DateTime.Now;
                    apply.PublishID = cart.Publish;
                    apply["AddressID"] = cart.AddressID.ToString();
                    apply["Receiver"] = cart.Receiver.ToString();
                    pn_OrderApply.BindData(apply);
                    #endregion

                    if (cart.Type == 2)
                    {
                        //促销品申请
                        #region 绑定赠品申请预算信息
                        BindBudgetInfo(cart.OrganizeCity, cart.AccountMonth, cart.Client, cart.GiftFeeType, cart.Brand, cart.GiftClassify);
                        tb_GiftBudgetInfo.Visible = true;
                        #endregion
                    }

                    BindGrid();

                    bt_Finish.Visible = false;
                    bt_Delete.Visible = false;
                    //bt_Submit.Visible = false;
                }
                #endregion
            }
            else
            {
                if (Session["SubmitOrderApplyID"] == null)
                {
                    MessageBox.ShowAndRedirect(this, "加载购物车失败，请重新选购!", "OrderApplyDetail0.aspx");
                    return;
                }
                else
                {
                    ViewState["ID"] = (int)Session["SubmitOrderApplyID"];
                    BindData();
                }
            }
        }

        #region 注册弹出窗口脚本
        string script = "function PopAdjust(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_OrderApplyDetailAdjust.aspx") +
            "?ID=' + id + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAdjust", script, true);

        #endregion
    }

    #region 获取当前管理片区的预算信息
    private void BindBudgetInfo(int city, int month, int client, int feetype, int productbrand, int giftclassify)
    {
        decimal totalbudget = FNA_BudgetBLL.GetUsableAmount(month, city, feetype, false);
        lb_TotalBudget.Text = totalbudget.ToString("0.##");

        IList<ORD_GiftApplyAmount> giftamounts = ORD_GiftApplyAmountBLL.GetModelList(
            string.Format("AccountMonth={0} AND Client={1} AND Brand={2} AND Classify={3}",
            month, client, productbrand, giftclassify));
        ViewState["SalesVolume"] = 0m;
        if (giftamounts.Count > 0)
        {
            decimal available = giftamounts[0].AvailableAmount + giftamounts[0].PreBalance - giftamounts[0].DeductibleAmount; ;
            decimal balance = giftamounts[0].BalanceAmount;
            ViewState["SalesVolume"] = giftamounts[0].SalesVolume;
            lb_AvailableAmount.Text = available.ToString("0.##");
            lb_BalanceAmount.Text = balance.ToString("0.##");
            //2012-3-27 暂时只取赠品额度，不取预算
            //lb_BalanceAmount.Text = (totalbudget > balance ? balance : totalbudget).ToString("0.##");
        }
        else
        {
            lb_AvailableAmount.Text = "0";
            lb_BalanceAmount.Text = "0";
        }

        hl_ViewBudget.NavigateUrl = "~/SubModule/FNA/Budget/BudgetBalance.aspx?OrganizeCity=" + city.ToString();
    }
    #endregion

    #region 界面绑定
    private void BindData()
    {
        ORD_OrderApplyBLL apply = new ORD_OrderApplyBLL((int)ViewState["ID"]);
        if (apply.Model == null) Response.Redirect("FeeApplyList.aspx");

        pn_OrderApply.BindData(apply.Model);

        ViewState["Publish"] = apply.Model.PublishID;
        ORD_ApplyPublish publish = new ORD_ApplyPublishBLL(apply.Model.PublishID).Model;
        #region 根据审批状态控制页面
        if (apply.Model.Type == 2 && apply.Model.State == 1)
        {
            #region 绑定赠品申请预算信息


            if (publish == null) Response.Redirect("OrderApplyDetail0.aspx");
            int productbrand = 0, giftclassify = 0;
            int.TryParse(publish["ProductBrand"], out productbrand);
            int.TryParse(publish["GiftClassify"], out giftclassify);
            int giftfeetype = publish.FeeType;
            int month = apply.Model.AccountMonth;
            int city = apply.Model.OrganizeCity;
            int client = apply.Model.Client;

            BindBudgetInfo(city, month, client, giftfeetype, productbrand, giftclassify);

            tb_GiftBudgetInfo.Visible = true;
            #endregion
        }

        if (apply.Model.State != 1)
        {
            //非 未提交 状态
            pn_OrderApply.SetControlsEnable(false);

            bt_EditCart.Visible = false;
            bt_Delete.Visible = false;
            bt_Save.Visible = false;
            bt_Submit.Visible = false;

            //可见调整数量及原因
            gv_List.Columns[gv_List.Columns.Count - 4].Visible = true;      //调整原因
            gv_List.Columns[gv_List.Columns.Count - 5].Visible = true;      //批复数量         
        }

        AC_AccountMonth endmonht = new AC_AccountMonthBLL(apply.Model.AccountMonth).Model;
        DateTime reapplyend;
        reapplyend = DateTime.Parse(endmonht.Name + ConfigHelper.GetConfigString("GiftReApplyDate"));
        bt_ReApply.Visible = !(DateTime.Now > reapplyend);
        switch (apply.Model.State)
        {
            case 1:     //编辑状态
                bt_Finish.Visible = false;
                bt_ReApply.Visible = false;
                break;
            case 2:     //已提交状态，审批过程中，可以作申请数量调整
                if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
                    gv_List.Columns[gv_List.Columns.Count - 2].Visible = true;      //允许调整

                bt_Finish.Visible = false;
                bt_ReApply.Visible = false;
                break;
            case 3:     //审核已通过
            case 4:     //审核已通过后，终止发放(该功能暂未启用)
                bt_Finish.Visible = false;
                bt_ReApply.Visible = false;
                //gv_List.Columns[gv_List.Columns.Count - 1].Visible = true;      //已发放数量 
                break;
            case 8:     //审核不通过
                bt_Finish.Visible = false;
                int forwarddays = ConfigHelper.GetConfigInt("GiftApplyForwardDays");
                if (apply.Model.AccountMonth < AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)))
                    bt_ReApply.Visible = false;
                break;
            default:
                break;
        }
        #endregion

        BindGrid();
    }

    private void BindGrid()
    {
        if (Session["LogisticsOrderApplyDetail"] != null)
        {
            //有购物车内容，从购物车绑定
            ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
            gv_List.DataSource = cart.Items.Where(p => p.BookQuantity > 0).OrderByDescending(p => p.Price).ToList();
            gv_List.DataBind();
            lb_TotalCost.Text = cart.Items.Sum(m => (m.BookQuantity + m.AdjustQuantity) * m.Price).ToString("0.00");
        }
        else
        {
            //从申请单明细绑定
            ORD_OrderApplyBLL apply = new ORD_OrderApplyBLL((int)ViewState["ID"]);
            gv_List.DataSource = apply.Items.Where(p => p.BookQuantity > 0).OrderByDescending(p => p.Price).ToList();
            gv_List.DataBind();
            lb_TotalCost.Text = apply.Items.Sum(m => (m.BookQuantity + m.AdjustQuantity) * m.Price).ToString("0.00");
        }
        if (lb_BalanceAmount.Text != "" && decimal.Parse(lb_BalanceAmount.Text) != 0)
        {
            lb_AfterSubmitBalance.Text = (decimal.Parse(lb_BalanceAmount.Text) - decimal.Parse(lb_TotalCost.Text)).ToString("0.00");
            lb_Percent.Text = (decimal.Parse(lb_TotalCost.Text) / decimal.Parse(lb_BalanceAmount.Text)).ToString("0.##%");
            td_Percent.Visible = true;
            td_AfterSubmitBalance.Visible = true;
        }
        if (lb_TotalCost.Text != "" && decimal.Parse(lb_TotalCost.Text) != 0 && ViewState["SalesVolume"] != null && (decimal)ViewState["SalesVolume"] > 0)
        {
            lb_ActFeeRate.Text = Math.Round((decimal.Parse(lb_TotalCost.Text) / (decimal)ViewState["SalesVolume"] * 100), 2).ToString();
            td_actfeerate.Visible = true;
        }
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            if (id > 0)
            {
                Button bt_OpenAdjust = (Button)row.FindControl("bt_OpenAdjust");
                bt_OpenAdjust.OnClientClick = "PopAdjust(" + id.ToString() + ")";
            }
        }
    }
    #endregion

    #region 提供界面需要的信息
    protected string GetProductInfo(int ProductID, string FieldName)
    {
        return new PDT_ProductBLL(ProductID, true).Model[FieldName];
    }

    protected string GetQuantityString(int product, int quantity)
    {
        if (quantity == 0) return "0";

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

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

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return quantity / p.ConvertFactor;
    }

    protected int GetPackagingQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return quantity % p.ConvertFactor;
    }

    protected string GetTrafficeName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
    }

    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }
    protected int GetSubmitQuantity(int product)
    {
        if ((int)ViewState["Publish"] > 0)
            return ORD_OrderApplyBLL.GetSubmitQuantity((int)ViewState["Publish"], product);
        else
            return 0;
    }
    protected string GetPublishDetailGiveLevel(int product)
    {
        if ((int)ViewState["Publish"] > 0)
        {
            ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL((int)ViewState["Publish"]);

            ORD_ApplyPublishDetail detail = publish.Items.FirstOrDefault(p => p.Product == product);
            if (detail != null)
                return detail["GiveLevel"];
            else
                return "";
        }
        else
            return "";
    }
    protected string GetPublishDetailRemark(int product)
    {
        if ((int)ViewState["Publish"] > 0)
        {
            ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL((int)ViewState["Publish"]);

            ORD_ApplyPublishDetail detail = publish.Items.FirstOrDefault(p => p.Product == product);
            if (detail != null)
                return detail["Remark"];
            else
                return "";
        }
        else
            return "";
    }
    #endregion

    protected void bt_OpenAdjust_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && (bool)Session["SuccessFlag"])
        {
            BindGrid();
        }
    }

    protected void bt_EditCart_Click(object sender, EventArgs e)
    {
        if (Session["LogisticsOrderApplyDetail"] != null)
            Response.Redirect("OrderApplyDetail2.aspx");
        else
        {
            ORD_OrderCartBLL cart = ORD_OrderCartBLL.InitByOrderApply((int)ViewState["ID"]);
            if (cart == null)
            {
                MessageBox.Show(this, "创建购物车对象失败!" + ViewState["ID"].ToString());
                return;
            }
            else
            {
                if ((int)ViewState["ID"] > 0) Session["LogisticsOrderApplyID"] = (int)ViewState["ID"];
                Session["LogisticsOrderApplyDetail"] = cart;

                Response.Redirect("OrderApplyDetail2.aspx");
            }
        }
    }

    private bool Save()
    {
        ORD_OrderCartBLL cart = null;
        if (Session["LogisticsOrderApplyDetail"] != null) cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];

        if ((int)ViewState["ID"] == 0)
        {
            if (cart == null || cart.Items.Count == 0)
            {
                MessageBox.Show(this, "对不起，定单申请明细不能为空!");
                return false;
            }

            ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL();
            pn_OrderApply.GetData(bll.Model);

            #region 判断有没有填写经销商
            if (bll.Model.Client == 0)
            {
                MessageBox.Show(this, "对不起，请填写申请定购的经销商!");
                return false;
            }
            #endregion

            #region 初始化定单字段
            bll.Model.OrganizeCity = cart.OrganizeCity;
            bll.Model.PublishID = cart.Publish;
            bll.Model.AccountMonth = cart.AccountMonth;
            bll.Model.SheetCode = ORD_OrderApplyBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);   //自动产生备案号
            bll.Model.ApproveFlag = 2;
            bll.Model.State = 1;
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model["AddressID"] = cart.AddressID.ToString();
            bll.Model["Receiver"] = cart.Receiver.ToString();
            if (cart.Publish > 0)
            {
                ORD_ApplyPublish publish = new ORD_ApplyPublishBLL(cart.Publish).Model;
                if (publish != null)
                {
                    bll.Model["ProductBrand"] = publish["ProductBrand"];
                    bll.Model["GiftClassify"] = publish["GiftClassify"];
                }
            }
            #endregion

            ViewState["ID"] = bll.Add();

            #region 新增定单明细明细
            foreach (ORD_OrderCart cartitem in cart.Items)
            {
                if (cartitem.BookQuantity == 0) continue;
                ORD_OrderApplyDetail m = new ORD_OrderApplyDetail();
                m.ApplyID = (int)ViewState["ID"];
                m.Product = cartitem.Product;
                m.Price = cartitem.Price;
                m.BookQuantity = cartitem.BookQuantity;
                m.AdjustQuantity = 0;
                m.DeliveryQuantity = 0;

                bll.AddDetail(m);
            }
            #endregion
        }
        else
        {
            ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL((int)ViewState["ID"]);
            pn_OrderApply.GetData(bll.Model);
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();

            #region 修改明细
            if (cart != null)
            {
                //先将现有定单中每个品项与购物中的比较
                //如果购物车中没有该产品，则删除，如有且数量不同，则更新，并从购物车中移除该品项
                foreach (ORD_OrderApplyDetail m in bll.Items)
                {
                    ORD_OrderCart cartitem = cart.Items.FirstOrDefault(p => p.Product == m.Product);
                    if (cartitem == null)
                        bll.DeleteDetail(m.ID);
                    else
                    {
                        if (cartitem.BookQuantity != m.BookQuantity)
                        {
                            m.BookQuantity = cartitem.BookQuantity;
                            bll.UpdateDetail(m);
                        }
                        cart.RemoveProduct(m.Product);
                    }
                }

                //新购物车中新增的品项加入定单明细中
                foreach (ORD_OrderCart cartitem in cart.Items)
                {
                    ORD_OrderApplyDetail m = new ORD_OrderApplyDetail();
                    m.ApplyID = (int)ViewState["ID"];
                    m.Product = cartitem.Product;
                    m.Price = cartitem.Price;
                    m.BookQuantity = cartitem.BookQuantity;
                    m.AdjustQuantity = 0;
                    m.DeliveryQuantity = 0;

                    bll.AddDetail(m);
                }
            }
            #endregion
        }

        return true;
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (!Save()) return;

        if (sender != null)
            Response.Redirect("OrderApplyDetail3.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if (!Save()) return;

        if ((int)ViewState["ID"] != 0)
        {
            ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL((int)ViewState["ID"]);
            Session["SubmitOrderApplyID"] = (int)ViewState["ID"];
            if (bll.Model.State > 1) Response.Redirect("OrderApplyList.aspx");

            if (bll.Items.Count == 0)
            {
                MessageBox.ShowAndRedirect(this, "对不起，定单申请明细不能为空!", "OrderApplyDetail3.aspx?ID=" + ViewState["ID"].ToString());
                return;
            }

            if (bll.Model.AccountMonth < AC_AccountMonthBLL.GetCurrentMonth())
            {
                MessageBox.Show(this, "对不起，该订单申请月份已不是当前月份，已不可提交，请删除该单后重新申请赠品!");
                return;
            }

            decimal totalcost = bll.Items.Sum(m => m.BookQuantity * m.Price);

            if (bll.Model.Type == 2)
            {
                //促销品申请
                #region 判断预算额度余额是否够申请
                ORD_ApplyPublish publish = new ORD_ApplyPublishBLL(bll.Model.PublishID).Model;
                int productbrand = 0, giftclassify = 0;
                int.TryParse(publish["ProductBrand"], out productbrand);
                int.TryParse(publish["GiftClassify"], out giftclassify);

                decimal _budgetbalance = FNA_BudgetBLL.GetUsableAmount(bll.Model.AccountMonth, bll.Model.OrganizeCity, publish.FeeType);
                decimal _productbalance = ORD_GiftApplyAmountBLL.GetBalanceAmount(bll.Model.AccountMonth, bll.Model.Client, productbrand, giftclassify);

                decimal _balance = _productbalance;
                //2012-3-27 暂时限定赠品额度，不限定预算
                //decimal _balance = (_budgetbalance > _productbalance ? _productbalance : _budgetbalance);
                string[] nolimitbrand = Addr_OrganizeCityParamBLL.GetValueByType(1, 24).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
                if (_balance < totalcost && !nolimitbrand.Contains(publish["ProductBrand"]))
                {
                    if (bll.Model["GiftClassify"] == "2" && _balance > 0 && ORD_OrderApplyBLL.GetModelList("Client=" + bll.Model.Client.ToString() + " AND AccountMonth=" + bll.Model.AccountMonth.ToString() + " AND State IN (2,3) AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',5)=" + bll.Model["ProductBrand"] + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)=" + bll.Model["GiftClassify"]).Count == 0)
                    {
                        //判断是否可以申请最低金额赠品1件
                        decimal MinApplyAmount = 0, MaxApplyAmount = 0;

                        ORD_ApplyPublishBLL _publishbll = new ORD_ApplyPublishBLL(bll.Model.PublishID);
                        _publishbll.GetMinApplyAmount(out MinApplyAmount, out MaxApplyAmount);
                        if (_balance > 0 && _balance < MinApplyAmount)
                        {

                            if (bll.Items.Count > 1)
                            {
                                MessageBox.Show(this, "对不起，您当前的可申请余额不足，仅能申请最多1件赠品");
                                return;
                            }
                            else
                            {
                                int applyproduct = bll.Items[0].Product;
                                ORD_ApplyPublishDetail applydetail = _publishbll.Items.FirstOrDefault(p => p.Product == applyproduct);
                                if (applydetail != null && applydetail.MinQuantity < bll.Items[0].BookQuantity)
                                {
                                    MessageBox.Show(this, "对不起，您当前的可申请余额不足，仅能申请最多1件赠品");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this, "对不起，您当前的可申请余额[" + _balance.ToString("0.##") + "]不够申请这些品项！", "OrderApplyDetail3.aspx?ID=" + ViewState["ID"].ToString());
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this, "对不起，您当前的可申请余额[" + _balance.ToString("0.##") + "]不够申请这些品项！", "OrderApplyDetail3.aspx?ID=" + ViewState["ID"].ToString());
                        return;
                    }
                }
                #endregion

                #region 发起工作流
                NameValueCollection dataobjects = new NameValueCollection();

                dataobjects.Add("ID", ViewState["ID"].ToString());
                dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
                dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
                dataobjects.Add("FeeType", publish.FeeType.ToString());
                dataobjects.Add("ProductBrand", publish["ProductBrand"]);
                dataobjects.Add("GiftClassify", publish["GiftClassify"]);
                dataobjects.Add("TotalFee", totalcost.ToString());

                int TaskID = EWF_TaskBLL.NewTask("CuXiaoPin_Apply", (int)Session["UserID"], "促销品申请申请流程", "~/SubModule/Logistics/Order/OrderApplyDetail3.aspx?ID=" + ViewState["ID"].ToString() + "&Type=2", dataobjects);

                new EWF_TaskBLL(TaskID).Start();
                #endregion

                bll.Submit((int)Session["UserID"], TaskID);

                Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
            }
            else
            {
                //产品申请申请
                bll.Submit((int)Session["UserID"], 0);
                Response.Redirect("OrderApplyList.aspx");
            }
        }
    }

    protected void bt_Finish_Click(object sender, EventArgs e)
    {
        ORD_OrderApplyBLL apply = new ORD_OrderApplyBLL((int)ViewState["ID"]);

        if (apply.Model.State == 3) apply.Finish((int)Session["UserID"]);

        Response.Redirect("OrderApplyList.aspx");
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        ORD_OrderApplyBLL apply = new ORD_OrderApplyBLL((int)ViewState["ID"]);

        if (apply.Model.State == 1)
        {
            apply.DeleteDetail();
            apply.Delete();
        }
        Response.Redirect("OrderApplyList.aspx");
    }
    protected void bt_ReApply_Click(object sender, EventArgs e)
    {
        ORD_OrderApplyBLL apply = new ORD_OrderApplyBLL((int)ViewState["ID"]);
        if (apply.Model == null) Response.Redirect("OrderApplyList.aspx");

        apply.Model.SheetCode = ORD_OrderApplyBLL.GenerateSheetCode(apply.Model.OrganizeCity, apply.Model.AccountMonth);
        apply.Model.State = 1;
        apply.Model["TaskID"] = "";
        int c = apply.Items.Count;      //要激活一下Items对象才能在Add方法中被新增到数据库中

        int newid = apply.Add();
        Response.Redirect("OrderApplyDetail3.aspx?ID=" + newid.ToString());
    }
}
