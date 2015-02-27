using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.SVM;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using System.Text;
using MCSFramework.Model.Promotor;
using System.Collections.Generic;

public partial class SubModule_SVM_SalesVolumeBatchInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["VolumeID"] = Request.QueryString["VolumeID"] == null ? 0 : int.Parse(Request.QueryString["VolumeID"]);
            if ((int)ViewState["VolumeID"] == 0)
            {
                ViewState["Type"] = Request.QueryString["Type"] == null ? 1 : int.Parse(Request.QueryString["Type"]);       //销量类型 1：经销商进货 2：经销商出货（即门店进货） 3：门店出货
                ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
                ViewState["IsCXP"] = Request.QueryString["IsCXP"] == null ? false : int.Parse(Request.QueryString["IsCXP"]) != 0;    //是否是赠品销量录入 0:成品 1:赠品
                if ((int)ViewState["ClientID"] == 0) Response.Redirect("../desktop.aspx");
            }
            #endregion

            BindDropDown();

            if ((int)ViewState["VolumeID"] == 0)
            {
                if ((int)ViewState["Type"] == 3)
                {
                    BindClientInfo(0, (int)ViewState["ClientID"]);
                    if (ddl_Promotor.Visible && ddl_Promotor.Items.Count > 1)
                    {
                        ddl_Promotor.Items[1].Selected = true;
                    }
                    tbx_VolumeDate.Enabled = false;
                    bt_Submit.Visible = false;          //门店销售单，不允许即时提交

                }
                else
                    BindClientInfo((int)ViewState["ClientID"], 0);

                int month = int.Parse(ddl_AccountMonth.SelectedValue);

                DataTable dt = SVM_SalesVolumeBLL.InitProductList((int)ViewState["VolumeID"], (int)ViewState["ClientID"], (int)ViewState["Type"], month, (bool)ViewState["IsCXP"]); //初始化产品列表
                DataColumn[] keys = { dt.Columns["Product"] };
                dt.PrimaryKey = keys;
                ViewState["DTDetail"] = dt;

                bt_Delete.Visible = false;
                bt_Approve.Visible = false;
                bt_ToForcast.Visible = false;

                if (!(bool)ViewState["IsCXP"]) tb_AddProduct.Visible = false;


            }
            else
            {
                cb_OnlyDisplayUnZero.Checked = true;
                BindData();
            }

            BindDropDown_ByIsCXP();
            if (Request.QueryString["SalesFlag"] != null && ddl_Flag.Items.FindByValue(Request.QueryString["SalesFlag"]) != null)
            {
                ViewState["SalesFlag"] = int.Parse(Request.QueryString["SalesFlag"]);
                ddl_Flag.SelectedValue = Request.QueryString["SalesFlag"];
                ddl_Flag.Enabled = false;
            }
            Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();
        }
        if ((int)ViewState["VolumeID"] == 0 && !Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeIN")
            && Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeBack"))
        {
            MessageBox.Show(this, "您只具有录入【退货】的权限,请确认是否要录入退货.");
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        tbx_VolumeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        int JXCDelayDays = 0;
        string monthconditon = "";
        if ((int)ViewState["VolumeID"] == 0)
        {
            if ((int)ViewState["Type"] == 3)
                JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");       //门店销量填报延迟天数             
            else
            {
                JXCDelayDays = ConfigHelper.GetConfigInt("DIJXCDelayDays");       //门店销量填报延迟天数  
                monthconditon = "'" + DateTime.Now.AddDays(-JXCDelayDays).ToString() + "'BETWEEN BeginDate AND EndDate OR  '" + DateTime.Now.ToString() + "' BETWEEN BeginDate AND EndDate";
                ddl_AccountMonth.Enabled = true;
            }
        }

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList(monthconditon);
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays)).ToString();
        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeIN") && AC_AccountMonthBLL.GetCurrentMonth() > int.Parse(ddl_AccountMonth.SelectedValue))
        {
            tbx_VolumeDate.Text = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue)).Model.EndDate.ToString("yyyy-MM-dd");
        }


    }

    private void BindDropDown_ByIsCXP()
    {
        if (ViewState["IsCXP"] == null) return;

        if ((bool)ViewState["IsCXP"])
        {
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('9')");

            //促销品单
            ddl_Flag.DataSource = DictionaryBLL.GetDicCollections("SVM_SalesFlag").Where(p => int.Parse(p.Key) > 10);
            ddl_Flag.DataBind();
            select_Product.PageUrl += "?IsOpponent=9";
            select_Product1.PageUrl += "?IsOpponent=9";
        }
        else
        {
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('1')");
            //成品单
            ddl_Flag.DataSource = DictionaryBLL.GetDicCollections("SVM_SalesFlag").Where(p => int.Parse(p.Key) <= 10);
            ddl_Flag.DataBind();
            select_Product.PageUrl += "?IsOpponent=1";
            select_Product1.PageUrl += "?IsOpponent=1";
        }
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand_SelectedIndexChanged(null, null);
        ddl_Flag_SelectedIndexChanged(null, null);
        //检查录入进货与退货的权限
        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeBack"))//退货
        {
            if ((int)ViewState["VolumeID"] == 0)
            {
                try
                {
                    ddl_Flag.Items.FindByValue("2").Enabled = false;
                }
                catch (Exception)
                {

                }

                try
                {
                    ddl_Flag.Items.FindByValue("12").Enabled = false;

                }
                catch (Exception)
                {


                }

            }
            else
            {
                SVM_SalesVolume sv = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]).Model;
                if (sv.Flag == 2 || sv.Flag == 12)
                {
                    bt_Delete.Visible = false;
                    bt_Save.Visible = false;
                    bt_Submit.Visible = false;
                }

            }
        }
        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeIN"))//进货（正数）
        {
            if ((int)ViewState["VolumeID"] == 0)
            {
                try
                {
                    ddl_Flag.Items.FindByValue("1").Enabled = false;
                }
                catch (Exception)
                {

                }
                try
                {
                    ddl_Flag.Items.FindByValue("11").Enabled = false;
                }
                catch (Exception)
                {

                }

            }
            else
            {
                SVM_SalesVolume sv = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]).Model;
                if (sv.Flag == 1 || sv.Flag == 11)
                {
                    bt_Delete.Visible = false;
                    bt_Save.Visible = false;
                    bt_Submit.Visible = false;
                }
            }
        }
        if ((int)ViewState["VolumeID"] > 0 && Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "UpdateFJFSalesVolume"))
        {
            SVM_SalesVolumeBLL _bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bt_Save.Visible = _bll.Model["DataSource"] == "2" && _bll.Model.ApproveFlag != 1;
        }


    }
    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = new PDT_ClassifyBLL()._GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
    }
    #endregion

    protected void tbx_VolumeDate_TextChanged(object sender, EventArgs e)
    {
        DateTime volumedate;
        if (DateTime.TryParse(tbx_VolumeDate.Text, out volumedate))
        {
            if (volumedate > DateTime.Now.Date)
            {
                MessageBox.Show(this, "发生日期不能超出当天日期！");
                tbx_VolumeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                return;
            }
            if (AC_AccountMonthBLL.GetMonthByDate(volumedate) != int.Parse(ddl_AccountMonth.SelectedValue))
            {
                AC_AccountMonthBLL _bll = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue));
                MessageBox.Show(this, "发生日期应在" + _bll.Model.BeginDate.ToShortDateString() + "~" + _bll.Model.EndDate.ToShortDateString() + "之间");
                tbx_VolumeDate.Text = _bll.Model.EndDate.ToShortDateString();
                return;
            }
        }
        else
        {
            MessageBox.Show(this, "日期格式不正确!");
            tbx_VolumeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 绑定客户信息
    /// <summary>
    /// 绑定客户信息
    /// </summary>
    /// <param name="type">销量类型 1：经销商进货 3：经销商出货（即门店进货） 3：门店出货</param>
    /// <param name="sellinclient">进货客户</param>
    /// <param name="supplier">出货客户</param>
    private void BindClientInfo(int sellinclient, int supplier)
    {
        if (sellinclient > 0)
        {
            CM_Client _r = new CM_ClientBLL(sellinclient).Model;
            switch (_r.ClientType)
            {
                case 1: //公司仓库
                    hy_SellInClient.NavigateUrl = "~/SubModule/CM/Store/StoreDetail.aspx?ClientID=" + sellinclient.ToString();
                    break;
                case 2: //经销商
                    hy_SellInClient.NavigateUrl = "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + sellinclient.ToString();
                    break;
                case 3: //终端门店
                    hy_SellInClient.NavigateUrl = "~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=" + sellinclient.ToString();
                    break;
            }
            hy_SellInClient.Text = "(" + _r.Code + ")" + _r.FullName;

            if (supplier == 0)
            {
                supplier = _r.Supplier;

                #region 根据门店或分销商取供货商
                //成品，只取第一供货商
                //赠品，先取第二供货商，如果取不到，则取第一供货商
                if ((bool)ViewState["IsCXP"])
                {
                    int supplier2 = 0;
                    int.TryParse(_r["Supplier2"], out supplier2);
                    if (supplier2 != 0) supplier = supplier2;
                }
                CM_Client _s = new CM_ClientBLL(supplier).Model;
                ddl_SellOutClient.Items.Add(new ListItem("(" + _s.Code + ")" + _s.FullName, _s.ID.ToString()));
                #endregion

                hy_SellOutClient.Visible = false;

                //经销商登录时，供货商直接选择该经销商
                Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
                if (!string.IsNullOrEmpty(staff.Model["RelateClient"]))
                {
                    if (ddl_SellOutClient.Items.FindByValue(staff.Model["RelateClient"]) == null)
                    {
                        MessageBox.ShowAndRedirect(this, "对不起，您无法向该客户配送" + ((bool)ViewState["IsCXP"] ? "赠品" : "成品") + "！",
                            "SalesVolumeList.aspx?Type=" + ViewState["Type"].ToString() + "&SellInClientID=" + _r.ID.ToString());
                    }

                    ddl_SellOutClient.SelectedValue = staff.Model["RelateClient"];
                    ddl_SellOutClient.Enabled = false;
                }

            }

            if ((int)ViewState["VolumeID"] == 0 && new CM_ClientBLL(int.Parse(ddl_SellOutClient.SelectedValue)).Model.ClientType == 2 && SVM_InventoryBLL.GetModelList("Client=" + ddl_SellOutClient.SelectedValue + " AND AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND ApproveFlag=2").Count > 0)
            {
                MessageBox.ShowAndRedirect(this, "该经销商本月还有未审核库存，请先审核库存再作此操作。", "InventoryList.aspx?ClientID=" + ddl_SellOutClient.SelectedValue);
                return;
            }
        }
        else
        {
            hy_SellInClient.Visible = false;
            lb_SellInTitle.Visible = false;
        }

        if (hy_SellOutClient.Visible)
        {
            if (supplier > 0)
            {
                CM_Client _s = new CM_ClientBLL(supplier).Model;
                switch (_s.ClientType)
                {
                    case 1: //公司仓库
                        hy_SellOutClient.NavigateUrl = "~/SubModule/CM/Store/StoreDetail.aspx?ClientID=" + supplier.ToString();
                        break;
                    case 2: //经销商
                        hy_SellOutClient.NavigateUrl = "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + supplier.ToString();
                        break;
                    case 3: //终端门店
                        hy_SellOutClient.NavigateUrl = "~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=" + supplier.ToString();

                        #region 绑定门店导购员
                        try
                        {
                            lbl_Promotor.Visible = true;
                            ddl_Promotor.Visible = true;

                            AC_AccountMonth month = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue)).Model;
                            StringBuilder condition = new StringBuilder(" PM_Promotor.BeginWorkDate<='" + month.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "' AND ISNULL(PM_Promotor.EndWorkDate,GETDATE())>='" + month.BeginDate.ToString("yyyy-MM-dd") + "' AND PM_Promotor.ApproveFlag=1 ");
                            condition.Append("AND ID in (SELECT Promotor FROM PM_PromotorInRetailer WHERE Client = " + supplier.ToString() + ")");
                            //if ((int)ViewState["VolumeID"] == 0)
                            //{
                            //    condition.Append(" AND ID NOT IN (SELECT Promotor  FROM [MCS_SVM].[dbo].[SVM_SalesVolume] WHERE Type=3 AND Supplier= " + supplier.ToString() + "  AND AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND Promotor IS NOT NULL)");
                            //}
                            ddl_Promotor.DataSource = PM_PromotorBLL.GetModelList(condition.ToString());
                            ddl_Promotor.DataBind();
                            ddl_Promotor.Items.Insert(0, new ListItem("请选择..", "0"));
                        }
                        catch { }
                        #endregion

                        break;
                }
                hy_SellOutClient.Text = "(" + _s.Code + ")" + _s.FullName;
                ddl_SellOutClient.Visible = false;
            }
            else
            {
                hy_SellOutClient.Visible = false;
                ddl_SellOutClient.Visible = false;
                lb_SellOutTitle.Visible = false;
            }
        }
    }
    #endregion

    private void BindData()
    {
        SVM_SalesVolume sv = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]).Model;
        ViewState["Type"] = sv.Type;

        ViewState["ClientID"] = sv.Type == 3 ? sv.Supplier : sv.Client;
        tbx_VolumeDate.Text = sv.SalesDate.ToString("yyyy-MM-dd");
        ddl_AccountMonth.SelectedValue = sv.AccountMonth.ToString();
        ddl_Flag.SelectedValue = sv.Flag.ToString();
        ddl_Flag.Enabled = false;
        tbx_sheetCode.Text = sv.SheetCode;
        tbx_Remark.Text = sv.Remark;
        ViewState["IsCXP"] = (sv.Flag > 10);
        if (!(bool)ViewState["IsCXP"]) tb_AddProduct.Visible = false;
        DataTable dt = SVM_SalesVolumeBLL.InitProductList((int)ViewState["VolumeID"], (int)ViewState["ClientID"],
            (int)ViewState["Type"], sv.AccountMonth, (bool)ViewState["IsCXP"]); //初始化产品列表
        DataColumn[] keys = { dt.Columns["Product"] };
        dt.PrimaryKey = keys;
        ViewState["DTDetail"] = dt;
        BindClientInfo(sv.Client, sv.Supplier);

        if (sv.Promotor != 0)
        {
            if (ddl_Promotor.Items.FindByValue(sv.Promotor.ToString()) == null)
            {
                PM_Promotor pm = new PM_PromotorBLL(sv.Promotor).Model;
                if (pm != null)
                    ddl_Promotor.Items.Add(new ListItem(pm.Name + "【已离开该门店】", sv.Promotor.ToString()));
            }
            ddl_Promotor.SelectedValue = sv.Promotor.ToString();
        }
        if (sv["DataSource"] == "2")
        {
            gv_List.Columns[7].Visible = true;

            gv_List.Columns[8].HeaderText = "数量确认";
        }
        else if (sv["DataSource"] == "1")
            gv_List.SetControlsEnable(false);
        BindGrid();
        if (sv.InsertStaff != (int)Session["UserID"])
        {
            bt_Delete.Visible = false;
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
        }
        if (sv.ApproveFlag == 1)
        {
            #region 已审核，不可再修改
            tbx_VolumeDate.Enabled = false;
            ddl_AccountMonth.Enabled = false;
            ddl_Flag.Enabled = false;
            ddl_Promotor.Enabled = false;
            tbx_Remark.Enabled = false;
            tbx_sheetCode.Enabled = false;
            gv_List.SetControlsEnable(false);

            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Delete.Visible = false;
            bt_Approve.Visible = false;
            bt_ToForcast.Visible = false;

            tb_AddProduct.Visible = false;
            bt_AddProduct.Visible = false;
            select_Product.Visible = false;
            #endregion
        }
        else
        {
            bt_Delete.Visible = (int)Session["UserID"] == sv.InsertStaff;
            if (sv["SubmitFlag"] == "1")
                bt_Submit.Visible = false;       //已提交
            else
            {
                bt_Approve.Visible = false;     //未提交                
            }
            //门店销售单，不允许即时提交及审核
            if ((int)ViewState["Type"] == 3)
            {
                bt_Submit.Visible = false;
                bt_Approve.Visible = false;
            }

            if ((int)ViewState["Type"] != 3 || (bool)ViewState["IsCXP"])
            {
                bt_ToForcast.Visible = false;
            }
        }
        if (sv["DataSource"] == "1")
            gv_List.SetControlsEnable(false);
        if (sv["DataSource"] == "2")
        {
            cb_OnlyDisplayUnZero.Enabled = false;
        }

    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];
        string condition = " 1=1 ";
        if (ddl_Classify.SelectedValue != "" && ddl_Classify.SelectedValue != "0")
        {
            condition += " AND Classify=" + ddl_Classify.SelectedValue;
        }
        else if (ddl_Brand.SelectedValue != "" && ddl_Brand.SelectedValue != "0")
        {
            condition += " AND Brand=" + ddl_Brand.SelectedValue;
        }
        if (select_Product1.SelectValue != "")
        {
            condition += " AND ID=" + select_Product1.SelectValue;
        }
        if (cb_OnlyDisplayUnZero.Checked)
        {
            condition += " AND Quantity <> 0";
        }

        switch ((int)ViewState["Type"])
        {
            case 1:     //办事处仓库出货（经销商进货）
            case 4:     //办事处仓库进货
                gv_List.Columns[6].Visible = false;                      //隐藏批发价
                break;
            case 2:     //经销商出货（终端门店进货）
            case 3:     //终端门店销售
                // gv_List.Columns[5].Visible = false;                      //隐藏出厂价      
                if (new CM_ClientBLL((int)ViewState["ClientID"]).Model.ClientType == 3) lbl_Notice.Text = "请以最小单位数量填报";
                break;
        }

        if (!bt_Save.Visible)
        {
            gv_List.SetControlsEnable(false);
        }

        dt.DefaultView.RowFilter = condition;
        gv_List.DataSource = dt.DefaultView;
        gv_List.DataBind();
    }
    #endregion

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["VolumeID"] == 0)
        {
            if (SVM_SalesVolumeBLL.CheckSalesVolume((int)ViewState["ClientID"], DateTime.Parse(tbx_VolumeDate.Text),
                (int)ViewState["Type"], int.Parse(ddl_Flag.SelectedValue)) > 0)
            {
                MessageBox.Show(this, "该客户在当前日期:" + tbx_VolumeDate.Text + " 已经填报过数据，请检查以免重复录入!");
            }
        }
        if (bt_Save.Visible) SaveMyViewState();
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void cb_OnlyDisplayUnZero_CheckedChanged(object sender, EventArgs e)
    {
        if (bt_Save.Visible) SaveMyViewState();
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void SaveMyViewState()
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];
        foreach (GridViewRow gr in gv_List.Rows)
        {
            DataRow[] rows = dt.Select("ID=" + gv_List.DataKeys[gr.RowIndex]["ID"].ToString());

            if (rows.Length > 0)
            {
                DataRow dr = rows[0];

                int convertfactor = string.IsNullOrEmpty(dr["ConvertFactor"].ToString()) ? 1 : (int)dr["ConvertFactor"];
                decimal price = 0;
                if (decimal.TryParse(((TextBox)gr.FindControl("tbx_Price")).Text, out price)) dr["Price"] = price;

                int quantity1 = int.Parse(((TextBox)gr.FindControl("tbx_Quantity1")).Text);
                int quantity2 = int.Parse(((TextBox)gr.FindControl("tbx_Quantity2")).Text);
                int quantity = quantity1 * convertfactor + quantity2;
                if (quantity < 0)
                {
                    MessageBox.Show(this, new PDT_ProductBLL((int)dr["ID"]).Model.FullName + "的数量不能为负数！");
                    return;
                }
                dr["Quantity"] = quantity1 * convertfactor + quantity2;
            }
        }
    }

    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        SaveMyViewState();
        CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;

        #region 更新销量内容

        #region 保存销量头信息
        SVM_SalesVolumeBLL bll;
        //成/赠品进货
        IList<SVM_SalesVolume> svmlists = new List<SVM_SalesVolume>();
        string conditon = "";
        string message = "";
        if (!(bool)ViewState["IsCXP"])
        {
            switch (ViewState["Type"].ToString())
            {
                case "1":
                case "2":
                    conditon = "Client=" + _r.ID.ToString()
                      + " AND Type IN(1,2) AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                      + " AND Flag=" + ddl_Flag.SelectedValue;
                    message = "该客户当月的进货已审核，不可再次录入";
                    break;
                case "3":
                    conditon = "Supplier=" + _r.ID.ToString()
                 + " AND Type=3 AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                 + " AND Flag=" + ddl_Flag.SelectedValue + " AND ISNULL(Promotor,0)=" + ddl_Promotor.SelectedValue;
                    message = "该客户当月的销量已审核，不可再次录入";
                    break;
            }

        }
        else //判断是否有过门店赠品进货
        {
            switch (ViewState["Type"].ToString())
            {
                case "1":
                case "2":
                    conditon = "Client=" + _r.ID.ToString()
                      + " AND Type IN(1,2) AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                      + " AND Flag=" + ddl_Flag.SelectedValue;
                    message = "该客户当月的赠品进货已审核，不可再次录入";
                    break;
                case "3":
                    conditon = "Client=" + _r.ID.ToString()
                      + " AND Type=3 AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                      + " AND Flag=" + ddl_Flag.SelectedValue;
                    message = "该客户当月的赠品销量已审核，不可再次录入";
                    break;
            }

        }
        svmlists = SVM_SalesVolumeBLL.GetModelList(conditon + " AND InsertStaff!=1");
        if (svmlists.Count > 0)
        {
            if (svmlists.FirstOrDefault(p => p.ApproveFlag == 1) != null)
            {
                MessageBox.Show(this, message);
                return;
            }
            if (svmlists.Count == 1)
            {
                ViewState["VolumeID"] = svmlists[0].ID;
            }
        }
        if ((int)ViewState["VolumeID"] == 0)
        {
            foreach (SVM_SalesVolume m in svmlists)
            {
                bll = new SVM_SalesVolumeBLL(m.ID);
                bll.DeleteDetail();
                bll.Delete();
            }

            bll = new SVM_SalesVolumeBLL();
            bll.Model.Type = (int)ViewState["Type"];
            bll.Model.OrganizeCity = _r.OrganizeCity;
            bll.Model["DataSource"] = "4";
            if (bll.Model.Type == 3)
            {
                //门店销售
                bll.Model.Supplier = _r.ID;
                bll.Model.Promotor = int.Parse(ddl_Promotor.SelectedValue);
            }
            else
            {
                bll.Model.Client = _r.ID;
                if (ddl_SellOutClient.Visible)
                    bll.Model.Supplier = int.Parse(ddl_SellOutClient.SelectedValue);
                else
                    bll.Model.Supplier = _r.Supplier;
            }
            bll.Model.InsertStaff = (int)Session["UserID"];
        }
        else
        {
            bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bll.Model.UpdateStaff = (int)Session["UserID"];
        }

        bll.Model.SalesDate = DateTime.Parse(this.tbx_VolumeDate.Text.Trim());
        bll.Model.AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
        bll.Model.Flag = int.Parse(ddl_Flag.SelectedValue);
        bll.Model.SheetCode = tbx_sheetCode.Text;
        bll.Model.Remark = tbx_Remark.Text;

        if (bll.Model.Type == 3)
        {
            //门店销售时记录导购员
            bll.Model.Promotor = int.Parse(ddl_Promotor.SelectedValue);
        }

        bll.Model["SubmitFlag"] = "2";

        if ((int)ViewState["VolumeID"] == 0)
            ViewState["VolumeID"] = bll.Add();
        else
            bll.Update();
        #endregion

        #region 更新产品明细数据
        DataTable dt = (DataTable)ViewState["DTDetail"];
        foreach (DataRow dr in dt.Rows)
        {
            int product = (int)dr["ID"];
            int quantity = (int)dr["Quantity"];
            if (quantity > 5000 && bll.Model.Type == 3)
            {
                MessageBox.Show(this, "超过系统设置的销量上限5000");
                return;
            }

            if ((bll.Model.Flag == 2 || bll.Model.Flag == 3 || bll.Model.Flag == 12) && quantity > 0)
                quantity = 0 - quantity;       //退货时，数量保存为负数 (2:原价退货 3:折价退货 12:赠品退货)

            SVM_SalesVolume_Detail _detail = bll.Items.FirstOrDefault(m => m.Product == product);
            if (_detail == null)
            {
                //销量明细里不存在该产品的记录
                if (quantity == 0) continue;     //新增销量时，数量为0的不保存到数据库中

                _detail = new SVM_SalesVolume_Detail();
                _detail.Product = product;
                _detail.SalesPrice = (decimal)dr["Price"];
                _detail.Quantity = quantity;
                _detail.FactoryPrice = (decimal)dr["FactoryPrice"];
                bll.AddDetail(_detail);
            }
            else
            {
                //销量明细里已存在该产品的记录
                if (quantity == 0)
                {
                    bll.DeleteDetail(_detail.ID);
                    continue;
                }
                else if (_detail.Quantity != quantity || _detail.SalesPrice != (decimal)dr["Price"])
                {
                    _detail.SalesPrice = (decimal)dr["Price"];
                    _detail.Quantity = quantity;
                    bll.UpdateDetail(_detail);
                }
            }
        }
        #endregion

        #endregion

        if (ddl_Promotor.Items.Count > 2)
        {
            MessageBox.Show(this, "本店有" + (ddl_Promotor.Items.Count - 1).ToString() + "个导购员，请确定每个导购员均已录入了销量");
        }
        if (sender != null)
        {
            MessageBox.ShowAndRedirect(this, "数据暂存成功，并在所有门店及经分销商的销量全部录入完成后，及时到汇总页面统一提交!",
                "SalesVolumeBatchInput.aspx?VolumeID=" + ViewState["VolumeID"].ToString());
        }
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        bt_Save_Click(null, null);
        if ((int)ViewState["VolumeID"] > 0)
        {
            SVM_SalesVolumeBLL bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bll.Model["SubmitFlag"] = "1";
            bll.Update();
            if (sender != null)
            {
                MessageBox.ShowAndRedirect(this, "数据提交成功!", "SalesVolumeList.aspx?SellOutClientID=" + bll.Model.Supplier.ToString() +
                     "&SellInClientID=" + bll.Model.Client.ToString() + "&Type=" + ViewState["Type"].ToString());
            }
        }
    }
    protected void bt_ToForcast_Click(object sender, EventArgs e)
    {
        bt_Save_Click(null, null);
        Response.Redirect("ClassifyForcastDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() +
            "&AccountMonth=" + (int.Parse(ddl_AccountMonth.SelectedValue) + 1).ToString());
    }
    #endregion

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["VolumeID"] != 0)
        {
            SVM_SalesVolumeBLL bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bll.Delete();

            MessageBox.ShowAndRedirect(this, "数据删除成功!", "SalesVolumeList.aspx?SellOutClientID=" + bll.Model.Supplier.ToString() +
                "&SellInClientID=" + bll.Model.Client.ToString() + "&Type=" + ViewState["Type"].ToString());
        }
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if (ViewState["VolumeID"] != null)
        {
            bt_Submit_Click(null, null);

            SVM_SalesVolumeBLL bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bll.Approve((int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "数据审核成功!", "SalesVolumeList.aspx?SellOutClientID=" + bll.Model.Supplier.ToString() +
                "&SellInClientID=" + bll.Model.Client.ToString() + "&Type=" + ViewState["Type"].ToString());
        }
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (bt_Save.Visible) SaveMyViewState();
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }


    protected void bt_AddProduct_Click(object sender, EventArgs e)
    {
        int product = 0;
        if (int.TryParse(select_Product.SelectValue, out product) && product > 0)
        {
            PDT_Product pdt = new PDT_ProductBLL(product).Model;
            if (pdt == null) return;

            int quantity = int.Parse(tbx_Q1.Text) * pdt.ConvertFactor + int.Parse(tbx_Q2.Text);

            DataTable dt = (DataTable)ViewState["DTDetail"];
            DataRow[] drs = dt.Select("ID=" + product.ToString());
            if (drs.Length > 0)
            {
                drs[0]["Quantity"] = quantity;
            }
            else
            {
                DataRow dr = dt.NewRow();

                decimal factoryprice = 0, price = 0;
                PDT_ProductPriceBLL.GetPriceByClientAndType((int)ViewState["ClientID"], product, (int)ViewState["Type"], out factoryprice, out price);

                dr["ID"] = product;
                dr["Code"] = pdt.Code;
                dr["FullName"] = pdt.FullName;
                dr["ShortName"] = pdt.ShortName;
                dr["Brand"] = pdt.Brand;
                dr["Spec"] = pdt["Spec"];
                dr["Classify"] = pdt.Classify;
                dr["ConvertFactor"] = pdt.ConvertFactor;
                dr["FactoryPrice"] = factoryprice;
                dr["Price"] = price;
                dr["Quantity"] = quantity;

                try
                {
                    dr["BrandName"] = new PDT_BrandBLL(pdt.Brand).Model.Name;
                    dr["ClassifyName"] = new PDT_ClassifyBLL(pdt.Classify).Model.Name;
                    dr["TrafficPackaging"] = DictionaryBLL.GetDicCollections("PDT_Packaging")[pdt.TrafficPackaging.ToString()].Name;
                    dr["Packaging"] = DictionaryBLL.GetDicCollections("PDT_Packaging")[pdt.Packaging.ToString()].Name;
                }
                catch { }

                dt.Rows.Add(dr);
            }
            BindGrid();
        }
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            TextBox tbx_Quantity1 = (TextBox)row.FindControl("tbx_Quantity1");
            if (tbx_Quantity1 != null) tbx_Quantity1.Attributes.Add("onkeydown", "javascript:keyDown(this)");

            TextBox tbx_Quantity2 = (TextBox)row.FindControl("tbx_Quantity2");
            if (tbx_Quantity2 != null) tbx_Quantity2.Attributes.Add("onkeydown", "javascript:keyDown(this)");

            if (row.RowIndex == 0)
            {
                row.BackColor = System.Drawing.Color.White;
                continue;
            }

            string p = (string)gv_List.DataKeys[row.RowIndex - 1][1];
            string c = (string)gv_List.DataKeys[row.RowIndex][1];

            if (p == c)
            {
                row.BackColor = gv_List.Rows[row.RowIndex - 1].BackColor;
            }
            else
            {
                if (gv_List.Rows[row.RowIndex - 1].BackColor == System.Drawing.Color.White)
                    row.BackColor = System.Drawing.Color.FromArgb(220, 220, 220);
                else
                    row.BackColor = System.Drawing.Color.White;
            }
        }
    }
    protected void ddl_Flag_SelectedIndexChanged(object sender, EventArgs e)
    {
        CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;

        IList<SVM_SalesVolume> svmlists = new List<SVM_SalesVolume>();
        string conditon = "";

        if (!(bool)ViewState["IsCXP"])
        {
            switch (ViewState["Type"].ToString())
            {
                case "1":
                case "2":
                    conditon = "Client=" + _r.ID.ToString()
                      + " AND Type IN(1,2) AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                      + " AND Flag=" + ddl_Flag.SelectedValue;

                    break;
                case "3":
                    conditon = "Supplier=" + _r.ID.ToString()
                 + " AND Type=3 AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                 + " AND Flag=" + ddl_Flag.SelectedValue + " AND ISNULL(Promotor,0)=" + ddl_Promotor.SelectedValue;

                    break;
            }

        }
        else //判断是否有过门店赠品进货
        {
            switch (ViewState["Type"].ToString())
            {
                case "1":
                case "2":
                    conditon = "Client=" + _r.ID.ToString()
                      + " AND Type IN(1,2) AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                      + " AND Flag=" + ddl_Flag.SelectedValue;

                    break;
                case "3":
                    conditon = "Client=" + _r.ID.ToString()
                      + " AND Type=3 AND AccountMonth=" + ddl_AccountMonth.SelectedValue
                      + " AND Flag=" + ddl_Flag.SelectedValue;

                    break;
            }

        }
        svmlists = SVM_SalesVolumeBLL.GetModelList(conditon);
        if (svmlists.Count > 0)
        {
            bt_Save.OnClientClick = "return confirm('系统中已存在其他对应条件的销量或进货，将会替换原数据明细！是否确定暂存？')";
            bt_Submit.OnClientClick = "return confirm('系统中已存在其他对应条件的销量或进货，将会替换原数据明细！是否确定暂存？')";
        }
    }
}

