using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
//using MCSFramework.BLL.CAT;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.FNA;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;

public partial class SubModule_FNA_FeeApply_FeeApplyDetail3 : System.Web.UI.Page
{
    /// <summary>
    /// 设置页面gv_list控件中，调整金额是否只读
    /// </summary>
    protected bool AdjustReadOnly = true;
    protected bool IsRed = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 判断传入参数是否为SheetCode
            if (Request.QueryString["SheetCode"] != null)
            {
                string code = Request.QueryString["SheetCode"];
                IList<FNA_FeeApply> list = FNA_FeeApplyBLL.GetModelList("SheetCode='" + code + "'");
                if (list.Count > 0)
                    Response.Redirect("FeeApplyDetail3.aspx?ID=" + list[0].ID.ToString());
                else
                    Response.Redirect("FeeApplyList.aspx");
            }
            #endregion

            #region 获取参数
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]);
            ViewState["AccountTitle2"] = Request.QueryString["AccountTitle2"] == null ? 0 : int.Parse(Request.QueryString["AccountTitle2"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            ViewState["ActivityID"] = Request.QueryString["ActivityID"] == null ? 0 : int.Parse(Request.QueryString["ActivityID"]);
            ViewState["Client"] = Request.QueryString["Client"] == null ? 0 : int.Parse(Request.QueryString["Client"]);
            ViewState["Brand"] = Request.QueryString["Brand"] == null ? 0 : int.Parse(Request.QueryString["Brand"]);
            ViewState["RelateCar"] = Request.QueryString["RelateCar"] == null ? 0 : int.Parse(Request.QueryString["RelateCar"]);
            ViewState["GiftFeeClassify"] = Request.QueryString["GiftFeeClassify"] == null ? 0 : int.Parse(Request.QueryString["GiftFeeClassify"]);
            ViewState["FromGeneralFlow"] = Request.QueryString["FromGeneralFlow"] == null ? "N" : Request.QueryString["FromGeneralFlow"];

            Session["FeeApplyDetail"] = null;
            Session["SuccessFlag"] = null;
            #endregion

            BindDropDown();

            #region 创建费用明细的列表
            ListTable<FNA_FeeApplyDetail> _details = new ListTable<FNA_FeeApplyDetail>(new FNA_FeeApplyBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;

            int max = 0;
            if (_details.GetListItem().Count > 0) _details.GetListItem().Max(p => p.ID);
            ViewState["MaxID"] = max;
            #endregion

            if ((int)ViewState["ID"] == 0)
            {
                if ((int)ViewState["FeeType"] == 0 || (int)ViewState["OrganizeCity"] == 0)
                {
                    Response.Redirect("FeeApplyDetail0.aspx");
                    return;
                }
                ViewState["DicFeeType"] = DictionaryBLL.GetDicCollections("FNA_FeeType")[ViewState["FeeType"].ToString()];

                if ((int)ViewState["AccountMonth"] == 0)
                    ViewState["AccountMonth"] = AC_AccountMonthBLL.GetCurrentMonth();

                #region 新费用申请时，初始化申请信息
                Label lb_OrganizeCity = (Label)pn_FeeApply.FindControl("FNA_FeeApply_OrganizeCity");
                if (lb_OrganizeCity != null) lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", (int)ViewState["OrganizeCity"]);

                Label lb_month = (Label)pn_FeeApply.FindControl("FNA_FeeApply_AccountMonth");
                if (lb_month != null) lb_month.Text = new AC_AccountMonthBLL((int)ViewState["AccountMonth"]).Model.Name;

                Label lb_FeeType = (Label)pn_FeeApply.FindControl("FNA_FeeApply_FeeType");
                if (lb_FeeType != null) lb_FeeType.Text = ((Dictionary_Data)ViewState["DicFeeType"]).Name;

                Label lb_staff = (Label)pn_FeeApply.FindControl("FNA_FeeApply_InsertStaff");
                if (lb_staff != null) lb_staff.Text = new Org_StaffBLL((int)Session["UserID"]).Model.RealName;

                Label lb_InsertTime = (Label)pn_FeeApply.FindControl("FNA_FeeApply_InsertTime");
                if (lb_InsertTime != null) lb_InsertTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                TextBox tbx_Title = (TextBox)pn_FeeApply.FindControl("FNA_FeeApply_Title");
                if (tbx_Title != null && lb_FeeType != null)
                    tbx_Title.Text = lb_month.Text + " " + lb_FeeType.Text + "申请单" + Session["UserRealName"].ToString();

                if ((int)ViewState["AccountTitle2"] != 0)
                {
                    Label lb_AccountTitle2 = (Label)pn_FeeApply.FindControl("FNA_FeeApply_AccountTitle2");
                    if (lb_AccountTitle2 != null) lb_AccountTitle2.Text = new AC_AccountTitleBLL((int)ViewState["AccountTitle2"]).Model.Name;
                }

                DropDownList ddl_Brand = (DropDownList)pn_FeeApply.FindControl("FNA_FeeApply_ProductBrand");
                if (ddl_Brand != null) ddl_Brand.SelectedValue = ViewState["Brand"].ToString();

                if ((int)ViewState["RelateCar"] != 0)
                {
                    Label lb_RelateCar = (Label)pn_FeeApply.FindControl("FNA_FeeApply_RelateCar");
                    if (lb_RelateCar != null)
                    {
                        Car_CarList car = new Car_CarListBLL((int)ViewState["RelateCar"]).Model;
                        if (car != null)
                            lb_RelateCar.Text = car.CarNo;
                        else
                            ViewState["RelateCar"] = 0;
                    }
                }
                #endregion

                if (((Dictionary_Data)ViewState["DicFeeType"]).Description == "BudgetControl")
                    BindBudgetInfo();
                else if (((Dictionary_Data)ViewState["DicFeeType"]).Description == "FeeRateControl")
                    BindFeeRateInfo();
                UploadFile1.Visible = false;
                bt_Submit.Visible = false;
                bt_ViewReport.Visible = false;
                bt_Print.Visible = false;
                bt_Copy.Visible = false;
                bt_ViewWriteOff.Visible = false;
                bt_Cancel.Visible = false;
                tbl_Remark.Visible = true;
            }
            else
            {
                BindData();
            }
            bt_AddDetail.OnClientClick =
                string.Format("PopAddFeeDetail({0},{1},{2},{3},{4},{5});",
                    ViewState["FeeType"].ToString(),
                    ViewState["OrganizeCity"].ToString(),
                    ViewState["AccountMonth"].ToString(),
                    ViewState["AccountTitle2"].ToString(),
                    ViewState["Brand"].ToString(),
                    ViewState["RelateCar"].ToString()
                    );
        }

        #region 注册弹出窗口脚本
        string script = "function PopAddFeeDetail(feetype,organizecity,month,accounttitle2,brand,car){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AddFeeApplyDetailItem.aspx") +
            "?FeeType=' + feetype + '&OrganizeCity=' + organizecity + '&AccountMonth=' + month + '&AccountTitle2=' + accounttitle2 + '&Client=" +
            ViewState["Client"].ToString() + "&Brand=' + brand + '&RelateCar=' + car + '&FromGeneralFlow=" + ViewState["FromGeneralFlow"].ToString()
            + "&tempid='+tempid, window, 'dialogWidth:800px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAddFeeDetail", script, true);

        script = "function PopReport(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/ReportViewer/PubReportViewerFeeApp.aspx?ViewFramework=false&ReportPath=/MCS_FNA_Report/Report_FNA_ClientInfoByAppID_001&FeeAppID=' + id ") +
            ", window, 'dialogWidth:800px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReport", script, true);

        script = "function PopWriteOffListByDetailID(detailid){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../FeeWriteoff/Pop_FeeWriteOffListByFeeApply.aspx?tempid='+tempid+'&FeeApplyDetailID=' + detailid ") +
            ", window, 'dialogWidth:800px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopWriteOffListByDetailID", script, true);

        script = "function PopWriteOffListByApplyID(applyid){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../FeeWriteoff/Pop_FeeWriteOffListByFeeApply.aspx?tempid='+tempid+'&FeeApplyID=' + applyid ") +
            ", window, 'dialogWidth:800px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopWriteOffListByApplyID", script, true);

        script = "function PopAdjust(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AdjustApplyDetail.aspx") +
            "?ID=' + id + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAdjust", script, true);

        script = "function PopClientFNAInfo(clientid){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../FeeApplyOrWriteoffByClientList.aspx") +
            "?ClientID=' + clientid + '&tempid='+tempid, window, 'dialogWidth:900px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopClientFNAInfo", script, true);
        #endregion
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        DropDownList ddl_Brand = (DropDownList)pn_FeeApply.FindControl("FNA_FeeApply_ProductBrand");
        if (ddl_Brand != null)
        {
            ddl_Brand.DataTextField = "Name";
            ddl_Brand.DataValueField = "ID";
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent='1'");
            ddl_Brand.DataBind();
            ddl_Brand.Items.Add(new ListItem("多品牌", "0"));
        }
    }
    #endregion

    #region 获取指定管理片区的预算信息
    private void BindBudgetInfo()
    {
        tr_BudgetInfo.Visible = true;
        int month = (int)ViewState["AccountMonth"];
        int organizecity = (int)ViewState["OrganizeCity"];
        int feetype = (int)ViewState["FeeType"];

        lb_BudgetAmount.Text = FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, feetype).ToString("0.###");
        lb_BudgetBalance.Text = FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype).ToString("0.###");
        lb_SubmitTotalCost.Text = FNA_FeeApplyBLL.GetApplyTotalCost(month, organizecity, feetype).ToString("0.###");

        if (decimal.Parse(lb_BudgetAmount.Text) != 0)
            lb_TotalPercent.Text = (decimal.Parse(lb_SubmitTotalCost.Text) / decimal.Parse(lb_BudgetAmount.Text)).ToString("0.##%");
        hl_ViewBudget.NavigateUrl = "~/SubModule/FNA/Budget/BudgetBalance.aspx?OrganizeCity=" + organizecity.ToString() + "&AccountMonth=" + month.ToString();

        if (feetype == ConfigHelper.GetConfigInt("GiftFeeType"))
        {
            if ((int)ViewState["GiftFeeClassify"] == 0)
            {
                MessageBox.ShowAndRedirect(this, "赠品费用请至赠品专项费用申请页面申请!", "FeeApply_GiftFeeApply.aspx");
                return;
            }

            GetGiftAmountBalance();
        }
    }

    private decimal GetGiftAmountBalance()
    {
        int id = (int)ViewState["ID"];
        FNA_FeeApply apply = new FNA_FeeApplyBLL(id).Model;
        if (apply == null) return 0;

        decimal giftamountbalance = 0;
        IList<ORD_GiftApplyAmount> giftamounts = ORD_GiftApplyAmountBLL.GetModelList(
            string.Format("AccountMonth={0} AND Client={1} AND Brand={2} AND Classify={3}",
            apply.AccountMonth, apply.Client, apply.ProductBrand, (int)ViewState["GiftFeeClassify"]));
        if (giftamounts.Count > 0)
        {
            decimal budget = decimal.Parse(lb_BudgetBalance.Text);
            decimal balance = giftamounts[0].BalanceAmount;

            giftamountbalance = budget > balance ? balance : budget;
        }

        lb_GiftAmountBalance.Visible = true;
        lb_GiftAmountBalance.Text = string.Format("赠品费用可申请额度：{0:0.##}元", giftamountbalance);

        if (pn_FeeApply.FindControl("FNA_FeeApply_Client") != null)
            ((MCSSelectControl)pn_FeeApply.FindControl("FNA_FeeApply_Client")).Enabled = false;
        return giftamountbalance;
    }
    #endregion

    #region 获取指定管理片区的费点信息
    private void BindFeeRateInfo()
    {
        tr_FeeRateInfo.Visible = true;
        int month = (int)ViewState["AccountMonth"];
        int organizecity = (int)ViewState["OrganizeCity"];
        int feetype = (int)ViewState["FeeType"];

        decimal forcastvolume = SVM_SalesForcastBLL.GetTotalVolume(month, organizecity, true);
        decimal balance = FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype);
        decimal totalcost = FNA_FeeApplyBLL.GetApplyTotalCost(month, organizecity, feetype);

        lb_ForcastVolume.Text = forcastvolume.ToString("0.###");
        lb_BudgetBalance1.Text = balance.ToString("0.###");
        lb_SubmitTotalCost1.Text = totalcost.ToString("0.###");

        if (forcastvolume != 0) lb_FeeRate.Text = ((totalcost - balance) / forcastvolume).ToString("0.##%");

    }
    #endregion

    #region 绑定显示
    private void BindData()
    {
        int id = (int)ViewState["ID"];

        FNA_FeeApply apply = new FNA_FeeApplyBLL(id).Model;
        if (apply == null) Response.Redirect("FeeApplyList.aspx");

        ViewState["AccountMonth"] = apply.AccountMonth;
        ViewState["OrganizeCity"] = apply.OrganizeCity;
        ViewState["FeeType"] = apply.FeeType;
        ViewState["AccountTitle2"] = string.IsNullOrEmpty(apply["AccountTitle2"]) ? 0 : int.Parse(apply["AccountTitle2"]);
        ViewState["DicFeeType"] = DictionaryBLL.GetDicCollections("FNA_FeeType")[ViewState["FeeType"].ToString()];
        ViewState["RelateCar"] = string.IsNullOrEmpty(apply["RelateCar"]) ? 0 : int.Parse(apply["RelateCar"]);
        ViewState["GiftFeeClassify"] = string.IsNullOrEmpty(apply["GiftFeeClassify"]) ? 0 : int.Parse(apply["GiftFeeClassify"]);
        ViewState["FromGeneralFlow"] = string.IsNullOrEmpty(apply["FromGeneralFlow"]) ? "Y" : "N";

        pn_FeeApply.BindData(apply);

        if (apply.Client != 0) cbx_NoInsteadPayClient.Visible = false;

        UploadFile1.RelateID = (int)ViewState["ID"];

        if (((Dictionary_Data)ViewState["DicFeeType"]).Description == "BudgetControl")
            BindBudgetInfo();
        else if (((Dictionary_Data)ViewState["DicFeeType"]).Description == "FeeRateControl")
            BindFeeRateInfo();

        #region 根据审批状态控制页面
        bt_ViewWriteOff.Visible = false;
        if (apply.State == 1)
        {
            //未提交状态
            bt_ViewReport.Visible = false;

            tbx_Remark.Text = apply["Remark"];
            tbl_Remark.Visible = true;
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = true;  //删除按钮
        }

        if (apply.State != 1)
        {
            //非 未提交 状态
            bt_Delete.Visible = false;
            bt_AddDetail.Visible = false;
            pn_FeeApply.SetPanelEnable("Panel_FNA_FeeApplyDetail_1", false);
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
            bt_Save.Visible = false;
            bt_Submit.Visible = false;

            apply["Remark"] = apply["Remark"].Replace("\r", "<br/>");
            pn_Remark.BindData(apply);
            pn_Remark.Visible = true;

            cbx_NoInsteadPayClient.Visible = false;

            UploadFile1.CanDelete = false;

            if (apply.FeeType == 1)
                bt_ViewReport.OnClientClick = "PopReport(" + apply.ID.ToString() + ")";
            else
                bt_ViewReport.Visible = false;


            //可见调整金额及原因
            gv_List.Columns[gv_List.Columns.Count - 7].Visible = true;  //批复额
            gv_List.Columns[gv_List.Columns.Count - 10].Visible = true;  //调整原因
            gv_List.Columns[gv_List.Columns.Count - 11].Visible = true;  //调整金额
            if((ViewState["Details"] as ListTable<FNA_FeeApplyDetail>).GetListItem().FirstOrDefault(p=>p.AccountTitle==82)!=null)
            {
                gv_List.Columns[gv_List.Columns.Count - 6].Visible = true;  //批复额
                gv_List.Columns[gv_List.Columns.Count - 8].Visible = true;  //调整原因
                gv_List.Columns[gv_List.Columns.Count - 9].Visible = true;  //调整金额
            }
        }
        if ((ViewState["Details"] as ListTable<FNA_FeeApplyDetail>).GetListItem()[0].RelateContractDetail > 0)
        {
            gv_List.Columns[4].Visible = true;
        }

        if (apply.State == 2)
        {
            ///已提交状态，审批过程中，可以作金额调整
            ///审批过程中，可以作金额调整 Decision参数为在审批过程中传进来的参数
            if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
            {
                gv_List.Columns[gv_List.Columns.Count - 2].Visible = true; //允许调整申请金额
                int fltype = ConfigHelper.GetConfigInt("ContractFeeType-FL");
                if (apply.FeeType == fltype)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "返利不能单独审批", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btn_Pass').disabled='disabled'; </script>");
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "返利不能单条审批", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_bt_SaveDecisionComment').disabled='disabled'; </script>");
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "返利不能单条处理", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btn_WaitProcess').disabled='disabled'; </script>");
                    string[] allowdays = Addr_OrganizeCityParamBLL.GetValueByType(1, 9).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
                    if (!allowdays.Contains(DateTime.Now.Day.ToString()))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "超时不能单独审批", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btn_NotPass').disabled='disabled'; </script>");
                    }
                }
            }
            bt_ViewWriteOff.Visible = false;
        }

        if (apply.State == 3)
        {
            //已审批
            bt_ViewWriteOff.Visible = true;
            gv_List.Columns[gv_List.Columns.Count - 3].Visible = true; //查看核销 链接
            gv_List.Columns[gv_List.Columns.Count - 4].Visible = true; //可报销额
            gv_List.Columns[gv_List.Columns.Count - 5].Visible = true; //报销标志  

            bt_ViewWriteOff.OnClientClick = "PopWriteOffListByApplyID(" + id.ToString() + ")";
            if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1503, "CancelWirteOffDetail"))
            {
                bt_CancelWriteOff.Visible = true;
                bt_CancelWriteOff.OnClientClick = "return confirm('是否确定取消选中的费用，取消后，该费用不可再次核销！\n【若需要再次核销则需要重新发起申请！】')";
                gv_List.Columns[0].Visible = true;
            }
        }

        if (apply.InsertStaff != (int)Session["UserID"] || apply.State != 3)
        {
            bt_Cancel.Visible = false;
        }

        if (apply.InsertStaff != (int)Session["UserID"] || apply.State >= 3)
        {
            UploadFile1.CanUpload = false;
            UploadFile1.CanDelete = false;
        }

        if (apply.State != 8)
        {
            bt_Copy.Visible = false;    //只有审批未通过才可重新激活
        }

        #endregion

        if (apply["FromGeneralFlow"] == "N")
        {
            if (apply.FeeType == ConfigHelper.GetConfigInt("ContractFeeType") ||
                apply.FeeType == ConfigHelper.GetConfigInt("ContractFeeType-KA") ||
                apply.FeeType == ConfigHelper.GetConfigInt("ContractFeeType-FL")
                )
                bt_AddDetail.Visible = false;
        }
        #region 增加客户后的详细内容按钮
        if (apply.Client != 0)
        {
            //MCSSelectControl sc_Client = (MCSSelectControl)pn_FeeApply.FindControl("FNA_FeeApply_Client");
            //if (sc_Client != null)
            //{
            //    HyperLink hy_clientdetail = new HyperLink();
            //    hy_clientdetail.ImageUrl = "~/Images/Gif/gif-0818.gif";
            //    hy_clientdetail.NavigateUrl = "~/SubModule/CM/RT/RetailerAnalysis.aspx?ClientID=" + apply.Client.ToString();
            //    hy_clientdetail.Target = "_blank";
            //    sc_Client.Parent.Controls.Add(hy_clientdetail);
            //}
        }
        #endregion

        #region 增加关联活动链接
        int activityid = 0;

        if (int.TryParse(apply["ActivityID"], out activityid) && activityid > 0)
        {
            Label label = (Label)pn_FeeApply.FindControl("FNA_FeeApply_ActivityID");
            if (label != null)
            {
                HyperLink hy_activitydetail = new HyperLink();
                hy_activitydetail.Text = " 查看活动详情";
                hy_activitydetail.ForeColor = System.Drawing.Color.Blue;
                hy_activitydetail.NavigateUrl = "~/SubModule/CAT/CAT_ActivityDetail.aspx?ID=" + activityid.ToString();
                hy_activitydetail.Target = "_blank";
                label.Parent.Controls.Add(hy_activitydetail);
            }
        }
        #endregion

        #region 增加关联HDM结算单链接
        int hdmbalance = 0;

        if (int.TryParse(apply["HDMBalance"], out hdmbalance) && hdmbalance > 0)
        {
            Label label = (Label)pn_FeeApply.FindControl("FNA_FeeApply_HDMBalance");
            if (label != null)
            {
                HyperLink hy_hdmbalance = new HyperLink();
                hy_hdmbalance.Text = " 查看结算单详情";
                hy_hdmbalance.ForeColor = System.Drawing.Color.Blue;
                hy_hdmbalance.NavigateUrl = "~/SubModule/CSO/CSO_OfferBalanceDetail.aspx?OfferBalanceID=" + hdmbalance.ToString();
                hy_hdmbalance.Target = "_blank";
                label.Parent.Controls.Add(hy_hdmbalance);
            }
        }
        #endregion
        BindGrid();
    }
    #endregion

    #region 绑定费用申请明细列表
    private void BindGrid()
    {
        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        gv_List.BindGrid<FNA_FeeApplyDetail>(_details.GetListItem());
        
        //求金额合计
        decimal _totalcost = _details.GetListItem().Sum(p => p.ApplyCost + p.AdjustCost);
        lb_TotalCost.Text = _totalcost.ToString("0.###");

        decimal _totalcost_thismonth = _details.GetListItem().Sum(p => p.BeginMonth <= (int)ViewState["AccountMonth"] ? (p.ApplyCost + p.AdjustCost) : 0);
        lb_TotalCost_ThisMonth.Text = _totalcost_thismonth.ToString("0.###");

        //if (lb_BudgetAmount.Text != string.Empty && decimal.Parse(lb_BudgetAmount.Text) != 0)
        //    lb_Percent.Text = (_totalcost / decimal.Parse(lb_BudgetAmount.Text)).ToString("0.##%");
        //else
        //    lb_Percent.Text = "N%";

        lb_BorrowTotal.Text = _details.GetListItem().Sum(p => p.BeginMonth > (int)ViewState["AccountMonth"] ? (p.ApplyCost + p.AdjustCost) : 0).ToString("0.###");
        if (_details.GetListItem().Count(p => p.Flag == 1) == 0) bt_Cancel.Visible = false;
    }
    protected string GetDisplayCount(int RelateContractDetail)
    {
        CM_ContractDetail _m = new CM_ContractBLL().GetDetailModel(RelateContractDetail);      
        return _m != null ? _m["DiaplayCount"] : "0";
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            if (id > 0)
            {
                Button bt_OpenAdjust = (Button)e.Row.FindControl("bt_OpenAdjust");
                bt_OpenAdjust.OnClientClick = "PopAdjust(" + id.ToString() + ")";
            }

            ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
            FNA_FeeApplyDetail item = _details[id.ToString()];

            if (item.Client > 0)
            {
                HyperLink hy_Client = (HyperLink)e.Row.FindControl("hy_Client");
                if (hy_Client != null)
                {
                    CM_Client c = new CM_ClientBLL(item.Client).Model;
                    if (c != null)
                    {
                        hy_Client.Text = c.FullName;
                        hy_Client.NavigateUrl = "../FeeApplyOrWriteoffByClientList.aspx?ClientID=" + item.Client.ToString();

                        int linkman = 0;
                        if (int.TryParse(item["RelateLinkMan"], out linkman) && linkman > 0)
                        {
                            HyperLink hy_RelateLinkMan = (HyperLink)e.Row.FindControl("hy_RelateLinkMan");
                            if (hy_RelateLinkMan != null)
                            {
                                CM_LinkMan m = new CM_LinkManBLL(linkman).Model;
                                if (m != null)
                                {
                                    hy_RelateLinkMan.Text = m.Name;
                                    if (c.ClientType == 5)
                                        hy_RelateLinkMan.NavigateUrl = "~/SubModule/CM/Hospital/DoctorDetail.aspx?ID=" + m.ID.ToString();
                                    else
                                        hy_RelateLinkMan.NavigateUrl = "~/SubModule/CM/LM/LinkManDetail.aspx.aspx?ID=" + m.ID.ToString();
                                }
                            }
                        }
                    }
                }
            }

            Label lb_RelateBrand = (Label)e.Row.FindControl("lb_RelateBrand");
            if (lb_RelateBrand != null)
            {
                foreach (string brand in item.RelateBrands.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    PDT_Brand b = new PDT_BrandBLL(int.Parse(brand)).Model;
                    if (b != null) lb_RelateBrand.Text += b.Name + ",";
                }
            }

        }
    }
    #endregion

    #region 界面绑定所需的方法
    protected string GetContractPageURL(int contractdetailid)
    {
        if (contractdetailid > 0)
        {
            CM_ContractDetail d = new CM_ContractBLL().GetDetailModel(contractdetailid);
            if (d == null) return "";
            CM_Contract c = new CM_ContractBLL(d.ContractID).Model;
            if (c != null)
            {
                switch (c.Classify)
                {
                    case 1:
                    case 2:
                    case 3:
                        return "~/SubModule/CM/RT/RetailerContractDetail.aspx?ContractID=" + c.ID.ToString();
                    case 21:
                        return "~/SubModule/CM/PD/PropertyContractDetail.aspx?ContractID=" + c.ID.ToString();
                }
            }
        }
        return "";
    }
    protected string GetPreApplyInfo(int detailid, decimal applycost)
    {
        string str = "";
        return "";
        FNA_FeeApplyDetail predetail = FNA_FeeApplyBLL.GetPreApplyInfoByClient(detailid);
        IsRed = false;
        if (predetail != null)
        {
            decimal preCost = predetail.ApplyCost + predetail.AdjustCost;
            if (preCost > 0)
            {
                FNA_FeeApply preApply = new FNA_FeeApplyBLL(predetail.ApplyID).Model;
                int month = new FNA_FeeApplyBLL().GetDetailModel(detailid).BeginMonth;
                if (month == predetail.BeginMonth)
                {
                    str = "※当月重复※<br/>单号:" + preApply.SheetCode + "<br/>";
                    IsRed = true;
                }
                str += "金额:" + preCost.ToString("0.##元") + "   " + "与前次比例:" + (applycost / preCost).ToString("0%") + " <br/>" +
                    "月份:" + new AC_AccountMonthBLL(predetail.BeginMonth).Model.Name + " <br/>" +
                    "说明:" + predetail.Remark;
                if ((applycost / preCost) > 1.5m)
                {
                    IsRed = true;
                }
            }
        }

        return str;
    }

    protected decimal GetPreSalesVolume(int clientid)
    {
        if (clientid == 0) return 0;
        return new CM_ClientBLL(clientid).GetSalesVolume(AC_AccountMonthBLL.GetCurrentMonth() - 1);
    }

    protected decimal GetAvgSalesVolume(int clientid)
    {
        if (clientid == 0) return 0;
        return new CM_ClientBLL(clientid).GetSalesVolumeAvg();
    }
    #endregion

    #region 新增费用明细
    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && Session["FeeApplyDetail"] != null && (bool)Session["SuccessFlag"])
        {
            ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
            ListTable<FNA_FeeApplyDetail> _newdetails = (ListTable<FNA_FeeApplyDetail>)Session["FeeApplyDetail"];

            #region 费用申请时，是否可以在一个申请单中申请多家店面费用
            try
            {
                if (!ConfigHelper.GetConfigBool("FeeApplyMutiClientsFee"))
                {
                    if (_details.GetListItem().Count > 0 && _newdetails.GetListItem().Count > 0)
                    {
                        if (_details.GetListItem()[0].Client != _newdetails.GetListItem()[0].Client)
                        {
                            Session["FeeApplyDetail"] = null;
                            Session["SuccessFlag"] = null;

                            MessageBox.Show(this, "对不起，一份申请单只能申请同一个客户的费用科目申请!");
                            return;
                        }
                    }
                }
            }
            catch { }
            #endregion

            foreach (FNA_FeeApplyDetail item in _newdetails.GetListItem())
            {
                ViewState["MaxID"] = (int)ViewState["MaxID"] + 1;
                item.ID = (int)ViewState["MaxID"];
                _details.Add(item);
            }

            Session["FeeApplyDetail"] = null;
            Session["SuccessFlag"] = null;

            BindGrid();
        }
    }
    #endregion

    #region 科目明细的编辑及删除
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.RowIndex]["ID"];

        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        _details.Remove(id.ToString());

        BindGrid();
    }
    #endregion

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        #region 判断预算余额是否够申请该项费用
        //if (((Dictionary_Data)ViewState["DicFeeType"]).Description == "BudgetControl" &&
        //    decimal.Parse(lb_BudgetBalance.Text) < decimal.Parse(lb_TotalCost.Text))
        //{
        //    MessageBox.Show(this, "对不起，您当前的预算余额不够申请该项费用!");
        //    return;
        //}
        #endregion

        if (gv_List.Rows.Count == 0)
        {
            MessageBox.Show(this, "对不起， 必须添加相应的费用明细!");
            return;
        }
        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        FNA_FeeApplyBLL bll;
        if ((int)ViewState["ID"] == 0)
            bll = new FNA_FeeApplyBLL();
        else
        {
            bll = new FNA_FeeApplyBLL((int)ViewState["ID"]);
            if (bll.Model.State != 1)
            {
                MessageBox.Show(this, "对不起，该申请单不为草稿状态，不能修改!");
                return;
            }
        }
        pn_FeeApply.GetData(bll.Model);

        bll.Model["Remark"] = tbx_Remark.Text.Replace("|", "");

        if ((int)ViewState["ID"] == 0)
        {
            bll.Model.SheetCode = FNA_FeeApplyBLL.GenerateSheetCode((int)ViewState["OrganizeCity"], (int)ViewState["AccountMonth"]);   //自动产生备案号
            bll.Model.AccountMonth = (int)ViewState["AccountMonth"];
            bll.Model.FeeType = (int)ViewState["FeeType"];
            bll.Model.OrganizeCity = (int)ViewState["OrganizeCity"];
            bll.Model["AccountTitle2"] = ViewState["AccountTitle2"].ToString() == "0" ? "" : ViewState["AccountTitle2"].ToString();
            bll.Model["ActivityID"] = ViewState["ActivityID"].ToString() == "0" ? "" : ViewState["ActivityID"].ToString();
            bll.Model["RelateCar"] = ViewState["RelateCar"].ToString() == "0" ? "" : ViewState["RelateCar"].ToString();
            bll.Model["FromGeneralFlow"] = ViewState["FromGeneralFlow"].ToString();

            bll.Model.ApproveFlag = 1;
            bll.Model.State = 1;
            bll.Model.InsertStaff = (int)Session["UserID"];

            bll.Items = _details.GetListItem();

            #region 费用申请时，一个申请单中只能申请一家店面费用时，将门店设置到Head表中
            try
            {
                if (!ConfigHelper.GetConfigBool("FeeApplyMutiClientsFee") && bll.Items.Count > 0)
                {
                    bll.Model.Client = bll.Items[0].Client;
                }
            }
            catch { }
            #endregion

            ViewState["ID"] = bll.Add();
        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();

            #region 修改明细
            bll.Items = _details.GetListItem(ItemState.Added);
            bll.AddDetail();

            foreach (FNA_FeeApplyDetail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                bll.DeleteDetail(_deleted.ID);
            }

            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();

            #endregion
        }

        if (sender != null)
        {
            MessageBox.ShowAndRedirect(this, "申请保存成功，请尽快点击“提交申请”按钮，否则本次申请未生效！", "FeeApplyDetail3.aspx?ID=" + ViewState["ID"].ToString());
        }
        else
        {
            //明细保存成功后，重置明细里各记录的新增、修改状态,以免重复新增
            ViewState["Details"] = new ListTable<FNA_FeeApplyDetail>(new FNA_FeeApplyBLL((int)ViewState["ID"]).Items, "ID");
        }
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            if (gv_List.Rows.Count == 0)
            {
                MessageBox.Show(this, "对不起， 必须添加相应的费用明细!");
                return;
            }

            bt_Save_Click(null, null);

            FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL((int)ViewState["ID"]);
            if (bll.Model.State != 1)
            {
                MessageBox.Show(this, "对不起，该申请单不为草稿状态，不能提交!");
                return;
            }

            if (bll.Model.Client == 0 && !cbx_NoInsteadPayClient.Checked)
            {
                MessageBox.Show(this, "请正确选择费用代垫客户，如果确认无代垫客户，请勾选【确认无代垫客户】复选框!");
                return;
            }

            if (cbx_NoInsteadPayClient.Checked && bll.Model.Client != 0)
            {
                MessageBox.Show(this, "您已选择了费用代垫客户，又请勾选【确认无代垫客户】复选框，两者只能选择其一，请确认!");
                return;
            }

            #region 判断预算额度余额是否够申请
            if (((Dictionary_Data)ViewState["DicFeeType"]).Description == "BudgetControl")
            {
                decimal _balance = FNA_BudgetBLL.GetUsableAmount(bll.Model.AccountMonth, bll.Model.OrganizeCity, bll.Model.FeeType);
                decimal _applycost = decimal.Parse(lb_TotalCost.Text);
                lb_BudgetAmount.Text = _balance.ToString("0.###");

                if (bll.Model.FeeType == ConfigHelper.GetConfigInt("GiftFeeType"))
                {
                    _balance = GetGiftAmountBalance();
                }

                if (_balance < _applycost)
                {
                    MessageBox.Show(this, "对不起，您当前的预算余额不够申请该项费用！");
                    return;
                }
            }
            #endregion

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("TotalFee", lb_TotalCost.Text);
            dataobjects.Add("BorrowFee", lb_BorrowTotal.Text);
            dataobjects.Add("FeeType", bll.Model.FeeType.ToString());
            dataobjects.Add("AccountTitleType", bll.Model["AccountTitle2"]);

            #region 组合审批任务主题
            string title = bll.Model["Title"] + ",申请备案号:" + bll.Model.SheetCode + ",总费用:" + lb_TotalCost.Text;
            #endregion

            string AppCode = "FNA_FeeApplyFlow";
            if (EWF_Flow_AppBLL.GetModelList("Code='" + AppCode + "_" + bll.Model.FeeType.ToString() +
                "' AND EnableFlag='Y'").Count > 0)
                AppCode = AppCode + "_" + bll.Model.FeeType.ToString();

            if (bll.Model["AccountTitle2"] =="81")//导购管理费
            {
                AppCode ="FNA_FeeApplyFlow_8";
            }
 
            int TaskID = EWF_TaskBLL.NewTask(AppCode, (int)Session["UserID"], title, "~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID <= 0)
            {
                MessageBox.Show(this, "对不起，工作流发起失败，请与管理员联系！");
                return;
            }
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            #endregion

            bll.Submit((int)Session["UserID"], TaskID);

            MessageBox.ShowAndRedirect(this, "费用申请提交成功！", "FeeApplyList.aspx?FeeType=" + ViewState["FeeType"].ToString() + "&AccountMonth=" + ViewState["AccountMonth"].ToString());
        }
    }

    /// <summary>
    /// 删除已保存但未提交的申请单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
            FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL((int)ViewState["ID"]);
            if (bll.Model.State != 1)
            {
                MessageBox.Show(this, "对不起，该申请单不为草稿状态，不能删除!");
                return;
            }
            int activityid = 0;
            if (int.TryParse(bll.Model["ActivityID"], out activityid) && activityid > 0)
            {
                //CAT_ActivityBLL cat = new CAT_ActivityBLL(activityid);
                //cat.Model.FeeApply = 0;
            }
            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.DeleteDetail();
            bll.Delete();

            Response.Redirect("~/SubModule/FNA/FeeApply/FeeApplyList.aspx");
        }
    }

    protected void bt_OpenAdjust_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && (bool)Session["SuccessFlag"])
        {
            ListTable<FNA_FeeApplyDetail> _details = new ListTable<FNA_FeeApplyDetail>(new FNA_FeeApplyBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;
            BindData();
        }
    }

    protected void bt_Copy_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
            int month = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays));

            int id = FNA_FeeApplyBLL.Copy((int)ViewState["ID"], (int)Session["UserID"], month);

            MessageBox.ShowAndRedirect(this, "申请单重新激活成功!", "FeeApplyDetail3.aspx?ID=" + id.ToString());
        }
    }

    protected void bt_Print_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Response.Redirect("FeeApplyDetail_Print.aspx?ID=" + ViewState["ID"].ToString());
        }
    }

    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL((int)ViewState["ID"]);

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("FeeType", bll.Model.FeeType.ToString());

            #region 组合审批任务主题
            string title = "费用取消" + ",申请备案号:" + bll.Model.SheetCode;
            #endregion

            int TaskID = EWF_TaskBLL.NewTask("FNA_FeeApplyFlow_Cancel", (int)Session["UserID"], title, "~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID <= 0)
            {
                MessageBox.Show(this, "对不起，工作流发起失败，请与管理员联系！");
                return;
            }
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            #endregion

            MessageBox.ShowAndRedirect(this, "取消费用申请单成功!", "FeeApplyList.aspx");
        }
    }
    protected void bt_CancelWriteOff_Click(object sender, EventArgs e)
    {
        FNA_FeeApplyBLL apply = new FNA_FeeApplyBLL((int)ViewState["ID"]);
        if (apply.Model.State == 3)
        {
            foreach (GridViewRow row in gv_List.Rows)
            {
                CheckBox cbx = row.FindControl("cb_Selected") == null ? null : row.FindControl("cb_Selected") as CheckBox;
                if (cbx.Visible && cbx.Checked)
                {
                    int detailid = (int)gv_List.DataKeys[row.RowIndex]["ID"];
                    FNA_FeeApplyDetail detail = apply.GetDetailModel(detailid);
                    detail.Flag = 3;
                    detail.Remark += "::取消时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 取消人:" + new Org_StaffBLL((int)Session["UserID"]).Model.RealName;
                    apply.UpdateDetail(detail);
                }
            }
            ListTable<FNA_FeeApplyDetail> _details = new ListTable<FNA_FeeApplyDetail>(new FNA_FeeApplyBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;
            BindGrid();
        }
    }
}
