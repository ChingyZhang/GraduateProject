using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.FNA;
using MCSControls.MCSWebControls;
using MCSFramework.Model.CM;
using System.Data;

public partial class SubModule_FNA_FeeWriteoff_FeeWriteoffDetail : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Button btn_save = new Button();
        btn_save.OnClientClick = "return confirm('是否确定更改抵货款类型？')";
        btn_save.Text = "调整";
        btn_save.ID = "btn_saveAB";
        btn_save.Click += new EventHandler(btn_save_Click);
        btn_save.Visible = false;
        
        if (pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB") != null)
        {
            pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB").Parent.Controls.Add(btn_save);
        }
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["InsteadPayClient"] = Request.QueryString["InsteadPayClient"] == null ? 0 : int.Parse(Request.QueryString["InsteadPayClient"]);
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]);
            ViewState["HasFeeApply"] = Request.QueryString["HasFeeApply"] == null ? "Y" : Request.QueryString["HasFeeApply"];
            ViewState["InvoiceClassAB"] = Request.QueryString["InvoiceClassAB"] == null ? 0 : int.Parse(Request.QueryString["InvoiceClassAB"]);
            ViewState["InsteadPayStaff"] = Request.QueryString["InsteadPayStaff"] == null ? 0 : int.Parse(Request.QueryString["InsteadPayStaff"]);
            ViewState["FeeApplyClient"] = Request.QueryString["FeeApplyClient"] == null ? 0 : int.Parse(Request.QueryString["FeeApplyClient"]);
            ViewState["FeeApplyStaff"] = Request.QueryString["FeeApplyStaff"] == null ? 0 : int.Parse(Request.QueryString["FeeApplyStaff"]);
            BindDropDown();

            int month = AC_AccountMonthBLL.GetCurrentMonth();

            #region 创建空的列表
            ListTable<FNA_FeeWriteOffDetail> _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;
            #endregion

            if ((int)ViewState["ID"] == 0)
            {
                if ((int)ViewState["OrganizeCity"] == 0)
                {
                    Response.Redirect("FeeWriteoffDetail0.aspx");
                    return;
                }

                ViewState["AccountMonth"] = AC_AccountMonthBLL.GetCurrentMonth();

                #region 新费用申请时，初始化申请信息
                Label lb_OrganizeCity = (Label)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_OrganizeCity");
                if (lb_OrganizeCity != null) lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", (int)ViewState["OrganizeCity"]);

                Label lb_month = (Label)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_AccountMonth");
                if (lb_month != null) lb_month.Text = new AC_AccountMonthBLL((int)ViewState["AccountMonth"]).Model.Name;

                if ((int)ViewState["FeeType"] > 1)
                {
                    Label lb_FeeType = (Label)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_FeeType");
                    if (lb_FeeType != null) lb_FeeType.Text =
                        DictionaryBLL.GetDicCollections("FNA_FeeType")[ViewState["FeeType"].ToString()].Name;
                }
                Label lb_staff = (Label)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InsertStaff");
                if (lb_staff != null) lb_staff.Text = new Org_StaffBLL((int)Session["UserID"]).Model.RealName;

                Label lb_InsertTime = (Label)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InsertTime");
                if (lb_InsertTime != null) lb_InsertTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                MCSSelectControl select_InsteadPayClient = (MCSSelectControl)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InsteadPayClient");
                MCSSelectControl select_InsteadPayStaff = (MCSSelectControl)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InsteadPayStaff");
                DropDownList ddl_InvoiceClassAB = pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB") == null ? null : (DropDownList)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB");
                if (ddl_InvoiceClassAB != null) ddl_InvoiceClassAB.SelectedValue = ViewState["InvoiceClassAB"].ToString();
                if (select_InsteadPayClient != null)
                {
                    if ((int)ViewState["InsteadPayClient"] != 0)
                    {
                        select_InsteadPayClient.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,3)\"";
                        select_InsteadPayClient.SelectValue = ViewState["InsteadPayClient"].ToString();
                        select_InsteadPayClient.SelectText = new CM_ClientBLL((int)ViewState["InsteadPayClient"]).Model.FullName;
                        select_InsteadPayClient.Enabled = false;

                        if (select_InsteadPayStaff != null) select_InsteadPayStaff.Enabled = false;
                    }
                    else if (select_InsteadPayStaff != null && (int)ViewState["InsteadPayStaff"] == 0)
                    {
                        select_InsteadPayStaff.SelectValue = Session["UserID"].ToString();
                        select_InsteadPayStaff.SelectText = Session["UserRealName"].ToString();
                        select_InsteadPayStaff.Enabled = false;
                    }

                }
                if (select_InsteadPayStaff != null)
                {
                    if ((int)ViewState["InsteadPayStaff"] != 0)
                    {
                        select_InsteadPayStaff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + ViewState["OrganizeCity"];
                        select_InsteadPayStaff.SelectValue = ViewState["InsteadPayStaff"].ToString();
                        select_InsteadPayStaff.SelectText = new Org_StaffBLL((int)ViewState["InsteadPayStaff"]).Model.RealName;
                        select_InsteadPayStaff.Enabled = false;
                    }
                }
                bt_Submit.Visible = false;
                bt_Delete.Visible = false;
                bt_Print.Visible = false;
                bt_Print2.Visible = false;
                UploadFile1.Visible = false;

                tbl_Remark.Visible = true;
                #endregion

                SetGridViewColumnAndButton(ViewState["HasFeeApply"].ToString() != "N", false, 1);

                Session["FeeWriteOffDetails"] = ViewState["Details"];
            }
            else
            {
                BindData();
            }
        }

        #region 注册弹出窗口脚本
        string script = "function PopEditFeeDetail(feetype,organizecity,client){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AddFeeWriteOffDetail.aspx") +
            "?FeeType=' + feetype + '&OrganizeCity=' + organizecity + '&Client=' + client + '&tempid='+tempid, window, 'dialogWidth:960px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopEditFeeDetail", script, true);

        script = "function PopTitleNoApply(feetype,organizecity){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AddAccountTitleNoApply.aspx") +
            "?FeeType=' + feetype + '&OrganizeCity=' + organizecity + '&tempid='+tempid, window, 'dialogWidth:960px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopTitleNoApply", script, true);

        script = "function PopReport(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/ReportViewer/PubReportViewerFeeApp.aspx?ViewFramework=false&ReportPath=/MCS_FNA_Report/Report_FNA_ClientInfoByWriteOffID_001&FeeAppID=' + id ") +
            ", window, 'dialogWidth:800px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReport", script, true);

        script = "function PopAdjust(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AdjustWriteOffDetail.aspx") +
            "?ID=' + id + '&tempid='+tempid, window, 'dialogWidth:960px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAdjust", script, true);

        script = "function PopEditEvectionRoute(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_EvectionRoteList.aspx") +
            "?WriteOffID=' + id + '&tempid='+tempid, window, 'dialogWidth:1000px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopEditEvectionRoute", script, true);

        script = "function PopEditAttachment(DetailID){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("POP_AddFeeWriteOffAttachment.aspx") +
            "?DetailID=' + DetailID + '&tempid='+tempid, window, 'dialogWidth:900px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopEditAttachment", script, true);

        script = "function PopEditTaxes(id,detailid){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_FeeWriteoffDetail_Taxes.aspx") +
            "?ID=' + id + '&DetailID='+ detailid +'&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=400px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopEditTaxes", script, true);
        
        
        #endregion
    }

    private void BindDropDown()
    {
    }

    #region 获取指定管理片区的预算信息
    private void BindBudgetInfo()
    {
        tbl_BudgetInfo.Visible = true;
        int month = (int)ViewState["AccountMonth"];
        int organizecity = (int)ViewState["OrganizeCity"];
        int feetype = (int)ViewState["FeeType"];

        lb_BudgetAmount.Text = (FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, feetype) +
            FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, 0)).ToString("0.###");

        lb_BudgetBalance.Text = (FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype) +
            FNA_BudgetBLL.GetUsableAmount(month, organizecity, 0)).ToString("0.###");
    }
    #endregion

    #region 绑定报销单详细信息
    private void BindData()
    {
        int id = (int)ViewState["ID"];

        FNA_FeeWriteOff writeoff = new FNA_FeeWriteOffBLL(id).Model;
        if (writeoff == null) Response.Redirect("FeeWriteOffList.aspx");
        if (writeoff["InvoiceClassAB"] == "1")
        {
            string Detailid = "0";
            FNA_FeeWriteOffDetail _Taxes = new FNA_FeeWriteOffBLL(id).Items.FirstOrDefault(p => p.AccountTitle == 129);
            if (_Taxes != null) Detailid = _Taxes.ID.ToString();
            bt_Taxes.OnClientClick = "PopEditTaxes(" + ViewState["ID"].ToString() + "," + Detailid + ")";
        }
        else
        {
            bt_Taxes.Visible = false;
        }
        ViewState["AccountMonth"] = writeoff.AccountMonth;
        ViewState["OrganizeCity"] = writeoff.OrganizeCity;
        ViewState["InsteadPayClient"] = writeoff.InsteadPayClient;
        ViewState["FeeType"] = writeoff.FeeType;
        ViewState["HasFeeApply"] = writeoff["HasFeeApply"];

        pn_FeeWriteOff.BindData(writeoff);

        #region 绑定代垫客户的主户头
        ViewState["MasterInsteadPayClient"] = 0;

        if (ViewState["InsteadPayClient"] != null && (int)ViewState["InsteadPayClient"] > 0)
        {
            CM_Client client = new CM_ClientBLL((int)ViewState["InsteadPayClient"]).Model;
            if (client != null && client.ClientType == 2 && client["DIClassify"] == "3")
            {
                CM_Client supplier = new CM_ClientBLL(client.Supplier).Model;
                if (supplier != null && supplier.ClientType == 2 && supplier["DIClassify"] == "1")
                {
                    HyperLink hy_MasterAccountName = new HyperLink();
                    hy_MasterAccountName.NavigateUrl = "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + supplier.ID.ToString();
                    hy_MasterAccountName.Target = "_blank";
                    hy_MasterAccountName.Text = "关联主户头：" + supplier.FullName;
                    hy_MasterAccountName.ForeColor = System.Drawing.Color.Blue;
                    pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InsteadPayClient").Parent.Controls.Add(hy_MasterAccountName);
                    ViewState["MasterInsteadPayClient"] = supplier.ID;
                }
            }
        }
        #endregion

        UploadFile1.RelateID = (int)ViewState["ID"];

        #region 根据审批状态控制页面
        if (writeoff.State == 1)
        {
            tbx_Remark.Text = writeoff["Remark"];
            tbl_Remark.Visible = true;
            bt_Print.Visible = false;
        }

        if (writeoff.State != 1)
        {
            //非 未提交 状态
            bt_EditWriteOffDetail.Visible = false;
            bt_AddTitleNoApply.Visible = false;
            pn_FeeWriteOff.SetPanelEnable("Panel_FNA_FeeWriteOffDetail_1", false);
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false; //不可删除

            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Delete.Visible = false;

            writeoff["Remark"] = writeoff["Remark"].Replace("\r", "<br/>");
            pn_Remark.BindData(writeoff);
            pn_Remark.Visible = true;
            bt_Print.Visible = true;
            UploadFile1.CanDelete = false;
            bt_Taxes.Visible = false;

            //可见调整金额及原因
            gv_List.Columns[gv_List.Columns.Count - 6].Visible = true; //扣减备注
            gv_List.Columns[gv_List.Columns.Count - 7].Visible = true; //扣减原因
            gv_List.Columns[gv_List.Columns.Count - 8].Visible = true; //扣减方式
            gv_List.Columns[gv_List.Columns.Count - 9].Visible = true; //扣减金额
            gv_List.Columns[gv_List.Columns.Count - 10].Visible = true; //是否逾期
        }

        if (writeoff.State == 2)
        {
            //已提交状态
            //审批过程中，可以作金额调整 Decision参数为在审批过程中传进来的参数
            if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
            {
                gv_List.Columns[gv_List.Columns.Count - 5].Visible = true; //调整按钮
                bt_Taxes.Visible = true;
            }
        }
        if (writeoff.State != 3 && Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1503, "ModifyInfoAB"))
        {
            //非已审核状态可以调整
            if (pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB") != null)
            {
                DropDownList invoice = pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB") as DropDownList;
                invoice.Enabled = true;
                Button btn_save = pn_FeeWriteOff.FindControl("btn_saveAB") as Button;
                btn_save.Visible = true;
                btn_save.Enabled = true;

            }
        }
        //上传附件
        gv_List.Columns[gv_List.Columns.Count - 3].Visible =
            (writeoff.InsertStaff == (int)Session["UserID"] && writeoff.State == 1) ||
            (writeoff.State < 3 && Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1503, "AddAttachment"));

        if (writeoff.InsertStaff != (int)Session["UserID"] || writeoff.State >= 3)
        {
            UploadFile1.CanUpload = false;
            UploadFile1.CanDelete = false;
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = true;//附件
        }

        #endregion

        SetGridViewColumnAndButton(writeoff["HasFeeApply"] != "N", writeoff["IsEvectionWriteOff"] == "Y", writeoff.State);

        BindGrid();
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
        DropDownList invoice = pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_InvoiceClassAB") as DropDownList;
        if (invoice.SelectedValue == "0" || invoice.SelectedValue == "")
        {
            MessageBox.Show(this, "请选择抵货款类型！");
            return;
        }
        FNA_FeeWriteOffBLL writeoff = new FNA_FeeWriteOffBLL((int)ViewState["ID"]);
        writeoff.Model["InvoiceClassAB"] = invoice.SelectedValue;
        if (writeoff.Update() >= 0)
            MessageBox.Show(this, "调整成功！");
        else
            MessageBox.Show(this, "调整失败！");

    }

    #endregion



    #region 绑定费用核消明细列表
    private void BindGrid()
    {
        ListTable<FNA_FeeWriteOffDetail> _details;
        if (ViewState["Details"] != null)
            _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        else _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
        if (_details.GetListItem().Count(p => p.AccountTitle == 82) > 0)
        {
            ViewState["FLPurchase"] = FNA_FeeWriteOffBLL.GetPurchaseVolume((int)ViewState["ID"]);
        }
        gv_List.BindGrid<FNA_FeeWriteOffDetail>(_details.GetListItem().OrderBy(p => p.Client).ThenBy(p => p.AccountTitle).ThenBy(p => p.BeginMonth).ToList());

        //求费用核消金额合计
        decimal _totalcost = 0;
        foreach (FNA_FeeWriteOffDetail _detail in _details.GetListItem())
        {
            _totalcost += _detail.WriteOffCost + _detail.AdjustCost;
        }
        lb_TotalCost.Text = _totalcost.ToString("0.###");

        if (bt_EditWriteOffDetail.Visible || bt_AddTitleNoApply.Visible)
        {
            Session["FeeWriteOffDetails"] = ViewState["Details"];       //放入Session中，允许编辑明细
        }
    }

    /// <summary>
    /// 根据核销单是否有申请单、是否差旅行程报销单、状态，设置GridView列的显示式及三个编辑按钮的显示
    /// </summary>
    /// <param name="HasFeeApply">是否有申请单</param>
    /// <param name="IsEvectionWriteOff">是否差旅行程报销单</param>
    /// <param name="State">状态</param>
    private void SetGridViewColumnAndButton(bool HasFeeApply, bool IsEvectionWriteOff, int State)
    {
        if (HasFeeApply)
        {
            //有费用申请
            bt_EditWriteOffDetail.OnClientClick = "PopEditFeeDetail(" + ViewState["FeeType"].ToString() + "," + ViewState["OrganizeCity"].ToString() + "," + ViewState["FeeApplyClient"].ToString() + ")";
            bt_AddTitleNoApply.Visible = false;
            bt_EditEvectionRoute.Visible = false;
        }
        else
        {
            if (State == 1 && DictionaryBLL.GetDicCollections("FNA_FeeType")[ViewState["FeeType"].ToString()].Description == "BudgetControl")
                BindBudgetInfo();

            //当费用为无申请费用时，下列字段隐藏
            gv_List.Columns[0].Visible = false;        //申请单备案号
            gv_List.Columns[1].Visible = false;        //发生客户
            gv_List.Columns[3].Visible = false;        //可核销额
            gv_List.Columns[6].Visible = false;        //结余方式

            bt_AddTitleNoApply.OnClientClick = "PopTitleNoApply(" + ViewState["FeeType"].ToString() + "," + ViewState["OrganizeCity"].ToString() + ")";
            bt_EditWriteOffDetail.Visible = false;
        }

        if (IsEvectionWriteOff)
        {
            //当核销单关联至差旅行程报销时，下列字段隐藏
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;  //不可删除单项科目
            gv_List.Columns[7].Visible = false;        //备注

            bt_EditEvectionRoute.OnClientClick = "PopEditEvectionRoute(" + ViewState["ID"].ToString() + ")";
            bt_EditWriteOffDetail.Visible = false;
            bt_AddTitleNoApply.Visible = false;
        }
        else
        {
            bt_EditEvectionRoute.Visible = false;
        }
    }
    #endregion

    protected void bt_EditWriteOffDetail_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && Session["FeeWriteOffDetails"] != null && (bool)Session["SuccessFlag"])
        {
            ViewState["Details"] = Session["FeeWriteOffDetails"];
            Session["SuccessFlag"] = null;

            BindGrid();
        }
    }

    protected void bt_AddTitleNoApply_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && Session["FeeWriteOffDetails"] != null && (bool)Session["SuccessFlag"])
        {
            ViewState["Details"] = Session["FeeWriteOffDetails"];
            Session["SuccessFlag"] = null;

            BindGrid();
        }
    }

    protected void bt_EditEvectionRoute_Click(object sender, EventArgs e)
    {
        ListTable<FNA_FeeWriteOffDetail> _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
        ViewState["Details"] = _details;

        BindGrid();
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int applydetailid = (int)gv_List.DataKeys[e.Row.RowIndex]["ApplyDetailID"];
            if (applydetailid > 0)
            {
                string sheetcode = FNA_FeeApplyBLL.GetSheetCodeByDetailID(applydetailid);
                FNA_FeeApplyDetail applydetail = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                if (applydetail != null)
                {
                    HyperLink hy_ApplySheetCode = (HyperLink)e.Row.FindControl("hy_ApplySheetCode");
                    hy_ApplySheetCode.Text = sheetcode;
                    hy_ApplySheetCode.NavigateUrl = "~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + applydetail.ApplyID;
                    HyperLink hy_TaskApprove = (HyperLink)e.Row.FindControl("hy_TaskApprove");
                    FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL(applydetail.ApplyID);
                    if (hy_TaskApprove != null && bll.Model.ApproveTask > 0)
                    {
                        hy_TaskApprove.NavigateUrl = "~/SubModule/EWF/TaskDetail.aspx?TaskID=" + bll.Model.ApproveTask;
                        hy_TaskApprove.Visible = true;
                    }
                    if (applydetail.Client > 0)
                    {
                        HyperLink hy_Client = (HyperLink)e.Row.FindControl("hy_Client");
                        hy_Client.Text = new CM_ClientBLL(applydetail.Client).Model.FullName;
                        hy_Client.NavigateUrl = "~/SubModule/FNA/FeeApplyOrWriteoffByClientList.aspx?ClientID=" + applydetail.Client;
                    }
                    if (applydetail.AccountTitle == 82 && ViewState["ID"] != null && (int)ViewState["ID"] > 0)
                    {
                        Object obj_lab = e.Row.FindControl("lb_DeductReason");
                        if (obj_lab != null && ViewState["FLPurchase"] != null)
                            e.Row.Cells[7].Text = "总进货额:" + ((DataTable)ViewState["FLPurchase"]).Compute("Sum(PurchaseVolume)", "ID=" + gv_List.DataKeys[e.Row.RowIndex]["ID"]).ToString() + ";\n"
                                + e.Row.Cells[7].Text;
                    }
                }
            }
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            if (id > 0)
            {
                Button bt_OpenAdjust = e.Row.FindControl("bt_OpenAdjust") != null ? (Button)e.Row.FindControl("bt_OpenAdjust") : null;
                if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
                {
                    bt_OpenAdjust.OnClientClick = "PopAdjust(" + id.ToString() + ")";
                    bt_OpenAdjust.Visible = true;
                }
                FNA_FeeWriteOffDetail writedetail = new FNA_FeeWriteOffBLL().GetDetailModel(id);
                if (applydetailid == 0)
                {
                    Button btn_Attachment = e.Row.FindControl("btn_Attachment") != null ? (Button)e.Row.FindControl("btn_Attachment") : null;
                    if (btn_Attachment != null) btn_Attachment.Visible = false;
                    HyperLink hy_Client = (HyperLink)e.Row.FindControl("hy_Client");
                    CM_Client client = new CM_ClientBLL(writedetail.Client).Model;
                    hy_Client.Text = client == null || string.IsNullOrEmpty(client.FullName) ? string.Empty : client.FullName;
                    hy_Client.NavigateUrl = "~/SubModule/FNA/FeeApplyOrWriteoffByClientList.aspx?ClientID=" + writedetail.Client;
                    bt_OpenAdjust.Visible = false;
                }
            }

        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.RowIndex]["ID"];

        ListTable<FNA_FeeWriteOffDetail> _details;
        if (ViewState["Details"] != null)
            _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        else _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
        _details.Remove(id.ToString());

        BindGrid();
    }

    protected void bt_OpenAdjust_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && (bool)Session["SuccessFlag"])
        {
            ListTable<FNA_FeeWriteOffDetail> _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;
            BindData();
        }
    }

    #region 保存费用报销单（只有未提交或新生产的核单但可以保存）
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ListTable<FNA_FeeWriteOffDetail> _details;
        if (ViewState["Details"] != null)
            _details = ViewState["Details"] as ListTable<FNA_FeeWriteOffDetail>;
        else _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");

        FNA_FeeWriteOffBLL bll;

        if ((int)ViewState["ID"] == 0)
            bll = new FNA_FeeWriteOffBLL();
        else
        {
            bll = new FNA_FeeWriteOffBLL((int)ViewState["ID"]);
            if (bll.Model.State != 1)
            {
                MessageBox.ShowAndRedirect(this, "对不起，当前核销单的状态不是“未提交”状态，不可保存！", "FeeWriteOffDetail.aspx?ID=" + ViewState["ID"].ToString());
                return;
            }
        }
        pn_FeeWriteOff.GetData(bll.Model);
        bll.Model["Remark"] = tbx_Remark.Text;

        #region 有效性校验
        // 判断是否选择了费用代垫客户或代垫员工
        {
            if (bll.Model["InsteadPaySystem"] == "0") bll.Model["InsteadPaySystem"] = "";
            if (bll.Model["InsteadPayStaff"] == "0") bll.Model["InsteadPayStaff"] = "";

            int insteadcount = 0;
            if (bll.Model.InsteadPayClient != 0) insteadcount++;
            if (!string.IsNullOrEmpty(bll.Model["InsteadPayStaff"])) insteadcount++;
            if (!string.IsNullOrEmpty(bll.Model["InsteadPaySystem"])) insteadcount++;

            if (insteadcount > 1)
            {
                MessageBox.Show(this, "代垫信息中，只能填写其中一个代垫信息!");
                return;
            }
            else if (insteadcount == 0)
            {
                MessageBox.Show(this, "代垫信息中，必须填写其中一个代垫信息!");
                return;
            }
        }
        #endregion

        if ((int)ViewState["ID"] == 0)
        {
            bll.Model.SheetCode = FNA_FeeWriteOffBLL.GenerateSheetCode((int)ViewState["OrganizeCity"]);   //自动产生报销单号
            bll.Model.AccountMonth = (int)ViewState["AccountMonth"];
            bll.Model.FeeType = (int)ViewState["FeeType"];
            bll.Model.OrganizeCity = (int)ViewState["OrganizeCity"];
            bll.Model.ApproveFlag = 1;
            bll.Model.State = 1;
            bll.Model["HasFeeApply"] = ViewState["HasFeeApply"].ToString();     //是否有申请单
            bll.Model["IsEvectionWriteOff"] = "N";                                               //非关联于差旅行程的报销
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model["InvoiceClassAB"] = ViewState["InvoiceClassAB"].ToString();
            bll.Model["FeeApplyClient"] = ViewState["FeeApplyClient"].ToString();
            bll.Model["FeeApplyStaff"] = ViewState["FeeApplyStaff"].ToString();
            bll.Items = _details.GetListItem();

            ViewState["ID"] = bll.Add();
        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();

            #region 修改明细

            #region 增加报销明细时，再次判断该项费用是否已报销
            bll.Items = new List<FNA_FeeWriteOffDetail>();
            foreach (FNA_FeeWriteOffDetail item in _details.GetListItem(ItemState.Added))
            {
                FNA_FeeApplyBLL apply = new FNA_FeeApplyBLL();
                if (apply.GetDetailModel(item.ApplyDetailID).Flag == 1)
                {
                    bll.Items.Add(item);
                }
            }
            bll.AddDetail();
            #endregion

            foreach (FNA_FeeWriteOffDetail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                bll.DeleteDetail(_deleted.ID);
            }

            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();

            #endregion
        }

        if (sender != null)
            Response.Redirect("FeeWriteOffDetail.aspx?ID=" + ViewState["ID"].ToString());
    }
    #endregion

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_Save_Click(null, null);

            FNA_FeeWriteOffBLL bll = new FNA_FeeWriteOffBLL((int)ViewState["ID"]);
            int KAtitle = ConfigHelper.GetConfigInt("AccountTitle-KA");
            foreach (FNA_FeeWriteOffDetail item in bll.Items)
            {
                //如果是KA合同则必填票号信息
                if (item.AccountTitle == KAtitle && (item["VATInvoiceNO"] == "" ||
                                               item["InvoiceDate"] == "" ||
                                               item["AcceptanceNO"] == "" ||
                                               item["DiscountRate"] == "" ||
                                               item["DiscountCost"] == "" ||
                                               item["RebateRate"] == ""))
                {
                    MessageBox.Show(this, "KA合同核销费用时，必须填写完整票号信息！");
                    return;
                }
            }
            if (bll.Model.InsteadPayClient == 0 && bll.Model["InsteadPayStaff"] == "" && bll.Model["InsteadPaySystem"] == "")
            {
                MessageBox.Show(this, "代垫信息中，必须填写其中一个代垫信息!");
                return;
            }

            if (bll.Items.Count == 0)
            {
                MessageBox.Show(this, "要报销的费用科目明细不能为空，请先添加要报销的费用科目!");
                return;
            }

            if (bll.Model.State != 1)
            {
                MessageBox.ShowAndRedirect(this, "对不起，当前流程的状态不是“未提交”状态，不可再次提交！", "FeeWriteOffDetail.aspx?ID=" + ViewState["ID"].ToString());
                return;
            }

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("FeeType", bll.Model.FeeType.ToString());
            dataobjects.Add("WriteoffCost", lb_TotalCost.Text);
            dataobjects.Add("InsteadPayClient", bll.Model.InsteadPayClient != 0 ? "1" : "2");
            dataobjects.Add("MasterInsteadPayClient", ViewState["MasterInsteadPayClient"] == null ? "0" :
                ViewState["MasterInsteadPayClient"].ToString());

            //组合审批任务主题
            Label lb_OrganizeCity = (Label)pn_FeeWriteOff.FindControl("FNA_FeeWriteOff_OrganizeCity");
            string title = lb_OrganizeCity.Text + ",核销单号:" + bll.Model.SheetCode + ",申请总额:" + lb_TotalCost.Text;

            int TaskID = EWF_TaskBLL.NewTask("FNA_FeeWriteoffFlow", (int)Session["UserID"], title, "~/SubModule/FNA/FeeWriteoff/FeeWriteoffDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);

            if (TaskID > 0)
            {
                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }
            #endregion

            bll.Submit((int)Session["UserID"], TaskID);

            MessageBox.ShowAndRedirect(this, "费用报销单提交成功，请打印报销单，并附贴相关票据!",
                "FeeWriteOffDetail.aspx?ID=" + ViewState["ID"].ToString());

        }
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            FNA_FeeWriteOffBLL bll = new FNA_FeeWriteOffBLL((int)ViewState["ID"]);
            if (bll.Model.State == 1)
            {
                bll.Delete();
                Response.Redirect("FeeWriteOffList.aspx");
            }
        }
    }
    protected void bt_Print_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Response.Redirect("FeeWriteOffDetail_Print.aspx?ID=" + ViewState["ID"].ToString());
        }
    }

    protected string GetDeductReason(string DeductReason)
    {
        try
        {
            int DeductReasonCode;
            return int.TryParse(DeductReason, out DeductReasonCode) ? DictionaryBLL.GetDicCollections("FNA_DeductReason")[DeductReason].Name : "";
        }
        catch { return ""; }

    }
    protected string GetISDelay(int FeeApplyDetailID)
    {
        if (FeeApplyDetailID == 0) return "";
        FNA_FeeApplyDetail detail = new FNA_FeeApplyBLL().GetDetailModel(FeeApplyDetailID);
        if (detail == null) return "";
        FNA_FeeApply feeapply = new FNA_FeeApplyBLL(detail.ApplyID).Model;
        int month = AC_AccountMonthBLL.GetMonthByDate(feeapply.InsertTime.AddDays(-ConfigHelper.GetConfigInt("FeeWriteDelayDays")));
        return month > detail.LastWriteOffMonth ? "<font color=red>逾期" + (month - detail.LastWriteOffMonth).ToString() + "个月</font>" : "<font color=blue></font>";
    }
    protected void gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RefreshDetail" && Session["POP_AddFeeWriteOffAttachment"] != null &&
            (bool)Session["POP_AddFeeWriteOffAttachment"])
        {
            ViewState["Details"] = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
            BindGrid();
        }
    }
    protected void bt_Print2_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Response.Redirect("FeeWriteOffDetail_Print2.aspx?ID=" + ViewState["ID"].ToString());
        }
    }
    protected void bt_Taxes_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && (bool)Session["SuccessFlag"])
        {
            ListTable<FNA_FeeWriteOffDetail> _details = new ListTable<FNA_FeeWriteOffDetail>(new FNA_FeeWriteOffBLL((int)ViewState["ID"]).Items, "ID");
            ViewState["Details"] = _details;
            BindData();
        }
    }
}
