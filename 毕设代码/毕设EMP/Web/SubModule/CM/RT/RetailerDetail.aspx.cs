// ===================================================================
// 文件路径:SubModule/RM/RetailerDetail.aspx.cs 
// 生成日期:2007-12-29 14:26:36 
// 作者:	  
// ===================================================================
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Promotor;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model;
using System.Collections.Generic;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Promotor;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;

public partial class SubModule_RM_RetailerDetail : System.Web.UI.Page
{
    MCSTreeControl CM_Client_OfficalCity;
    DropDownList CM_Client_MarketType, CM_Client_Classification, ddl_RTChannel;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        CM_Client_Classification = pl_detail.FindControl("CM_Client_Classification") != null ? (DropDownList)pl_detail.FindControl("CM_Client_Classification") : null;
        // 在此处放置用户代码以初始化页面
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
            #endregion

            //Session["MCSMenuControl_FirstSelectIndex"] = "11";


            if (ViewState["ClientID"] != null)
            {
                BindData();
                BindDropDown();
            }
            else if (Request.QueryString["Mode"] == "New")
            {
                #region 新增门店时的初始值
                Org_Staff staff = new Org_StaffBLL((int)Session["UserID"]).Model;
                if (staff == null) Response.Redirect("~/SubModule/Desktop.aspx");

                #region 新增客户时，详细资料界面控件初始化
                DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
                if (ddl_ActiveFlag != null) ddl_ActiveFlag.SelectedValue = "4";

                TextBox tbx_OpenTime = (TextBox)pl_detail.FindControl("CM_Client_OpenTime");
                if (tbx_OpenTime != null) tbx_OpenTime.Text = DateTime.Today.ToString("yyyy-MM-dd");

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OrganizeCity");
                if (tr_OrganizeCity != null) tr_OrganizeCity.SelectValue = staff.OrganizeCity.ToString();

                MCSTreeControl tr_OfficalCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OfficalCity");
                if (tr_OfficalCity != null) tr_OfficalCity.SelectValue = staff.OfficialCity.ToString();

                MCSSelectControl select_ClientManager = (MCSSelectControl)pl_detail.FindControl("CM_Client_ClientManager");
                if (select_ClientManager != null)
                {
                    select_ClientManager.SelectText = staff.RealName;
                    select_ClientManager.SelectValue = staff.ID.ToString();
                }

                DropDownList ddl_ChiefLinkMan = pl_detail.FindControl("CM_Client_ChiefLinkMan") != null ? (DropDownList)pl_detail.FindControl("CM_Client_ChiefLinkMan") : null;
                if (ddl_ChiefLinkMan != null)
                {
                    ddl_ChiefLinkMan.Items.Clear();
                    ddl_ChiefLinkMan.Enabled = false;
                }

                DropDownList ddl_RTClassify = (DropDownList)pl_detail.FindControl("CM_Client_RTClassify");
                if (ddl_RTClassify != null)
                {
                    if (ddl_RTClassify.Items.FindByValue("1") != null) ddl_RTClassify.SelectedValue = "1";
                }
                #endregion

                bt_AddLinkMan.Visible = false;
                bt_Approve.Visible = false;

                tr_Contract.Visible = false;
                tr_LinkMan.Visible = false;
                tr_Promotor.Visible = false;
                bt_Analysis.Visible = false;

                bt_AddApply.Visible = false;
                bt_RevocationApply.Visible = false;
                bt_Record.Visible = false;
                bt_ReplaceClientManager.Visible = false;
                #endregion
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }

        #region 给活跃标志加事件
        DropDownList ddl_ActiveFlag_1 = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        ddl_ActiveFlag_1.AutoPostBack = true;
        ddl_ActiveFlag_1.SelectedIndexChanged += new EventHandler(ddl_ActiveFlag_SelectedIndexChanged);
        #endregion
        TextBox tbx_Code = (TextBox)pl_detail.FindControl("CM_Client_Code");
        tbx_Code.AutoPostBack = true;
        tbx_Code.TextChanged += new EventHandler(tbx_Code_TextChanged);



        CM_Client_MarketType = pl_detail.FindControl("CM_Client_MarketType") != null ? (DropDownList)pl_detail.FindControl("CM_Client_MarketType") as DropDownList : null;

        CM_Client_OfficalCity = pl_detail.FindControl("CM_Client_OfficalCity") != null ? (MCSTreeControl)pl_detail.FindControl("CM_Client_OfficalCity") : null;
        if (CM_Client_OfficalCity != null)
        {
            CM_Client_OfficalCity.AutoPostBack = true;
            CM_Client_OfficalCity.Selected += new SelectedEventHandler(CM_Client_OfficalCity_Selected);
        }
        TextBox tbx_FullName = pl_detail.FindControl("CM_Client_FullName") == null ? null : (TextBox)pl_detail.FindControl("CM_Client_FullName");
        if (tbx_FullName != null)
        {
            tbx_FullName.AutoPostBack = true;
            tbx_FullName.TextChanged += new EventHandler(tbx_FullName_TextChanged);
        }
        #region 注册弹出窗口脚本
        string script = "function PopReplaceClientManager(id,clienttype){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../ReplaceClientManager.aspx") +
            "?ClientManager=' + id + '&ClientType='+clienttype+'&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReplaceClientManager", script, true);
        #endregion

        //ddl_RTChannel = pl_detail.FindControl("CM_Client_RTChannel") as DropDownList;
        //ddl_RTChannel.AutoPostBack = true;
        //ddl_RTChannel.SelectedIndexChanged += new EventHandler(ddl_RTChannel_SelectedIndexChanged);

        DropDownList ddl_IsRMSClient = pl_detail.FindControl("CM_Client_IsRMSClient") as DropDownList;
        //ddl_IsRMSClient.AutoPostBack = true;
        //ddl_IsRMSClient.SelectedIndexChanged += new EventHandler(ddl_IsRMSClient_SelectedIndexChanged);
        //foreach (ListItem item in ddl_IsRMSClient.Items)
        //{
        //    if (item.Value == "1" && item.Text == "已启动")
        //    {
        //        item.Attributes.Add("disabled", "true");
        //    }
        //}


        //非"雅慧电商"的流通店 不允许选择积分店状态
        //DropDownList ddl_RTClassifyTemp = pl_detail.FindControl("CM_Client_RTClassify") as DropDownList;
        //if (ddl_RTClassifyTemp != null && ddl_RTClassifyTemp.SelectedValue == "1" && ddl_RTChannel.SelectedValue != "20")
        //{
        //    ddl_IsRMSClient.SelectedValue = "2";
        //    ddl_IsRMSClient.Enabled = false;
        //    DropDownList ddl_RMSAccountEnabled = pl_detail.FindControl("CM_Client_RMSAccountEnabled") as DropDownList;
        //    ddl_RMSAccountEnabled.SelectedValue = "2";
        //}
        //this.ddl_RMSAccountEnabled_Select();

        //this.ShowRMSCloseDate();
    }

    //protected void ddl_RTChannel_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddl_VestKA = pl_detail.FindControl("CM_Client_VestKA") as DropDownList;
    //    string value = ddl_RTChannel.SelectedValue;
    //    if (DictionaryBLL.GetDicCollections("CM_RT_Channel").Count(p => (p.Value.Code == value && p.Value.Description == "KA")) > 0)
    //        ddl_VestKA.Enabled = true;
    //    else
    //    {
    //        ddl_VestKA.SelectedIndex = 0;
    //        ddl_VestKA.Enabled = false;
    //    }
    //}

    protected void ShowRMSCloseDate()
    {
        DropDownList ddl_IsRMSClient = pl_detail.FindControl("CM_Client_IsRMSClient") as DropDownList;
        Label lb_RMSBeginDate = pl_detail.FindControl("CM_Client_RMSBeginDate") as Label;
        TextBox txt_RMSCloseDate = pl_detail.FindControl("CM_Client_RMSCloseDate") as TextBox;
        lb_RMSBeginDate.Visible = true;
        txt_RMSCloseDate.Visible = true;
        //已启动状态下积分店关闭日期不可见
        if (ddl_IsRMSClient.SelectedValue == "1")
        {
            txt_RMSCloseDate.Visible = false;
        }
        else
        {
            txt_RMSCloseDate.Visible = true;
        }
    }

    //为控件ddl_RMSAccountEnabled赋值
    //protected void ddl_RMSAccountEnabled_Select()
    //{
    //    DropDownList ddl_IsRMSClient = pl_detail.FindControl("CM_Client_IsRMSClient") as DropDownList;
    //    DropDownList ddl_RMSAccountEnabled = pl_detail.FindControl("CM_Client_RMSAccountEnabled") as DropDownList;
    //    TextBox tb_RMSCloseDate = pl_detail.FindControl("CM_Client_RMSCloseDate") as TextBox;
    //    //请选择和已关闭状态下，积分状态启用否选项都为“否”
    //    if (ddl_IsRMSClient.SelectedValue == "3")//准备启动
    //    {
    //        ddl_RMSAccountEnabled.SelectedValue = "1";
    //    }
    //    else if (ddl_IsRMSClient.SelectedValue == "2")
    //    {
    //        ddl_RMSAccountEnabled.SelectedValue = "2";
    //    }
    //}


    //protected void ddl_IsRMSClient_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.ddl_RMSAccountEnabled_Select();
    //}


    private void tbx_FullName_TextChanged(object sender, EventArgs e)
    {
        Org_Staff staff = new Org_StaffBLL((int)Session["UserID"]).Model;
        string orgcitys = "";
        string ConditionStr = "FullName like '%" + ((TextBox)sender).Text.Trim() + "%'";
        string clients = "";
        #region 判断当前可查询的范围
        if (staff.OrganizeCity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(staff.OrganizeCity, true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += staff.OrganizeCity;
            if (orgcitys != "") ConditionStr += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion
        if (ViewState["ClientID"] != null)
        {
            ConditionStr = ConditionStr + " AND ID!=" + ViewState["ClientID"].ToString();
        }
        IList<CM_Client> clientList = CM_ClientBLL.GetModelList(ConditionStr);
        foreach (CM_Client client in clientList)
        {
            clients += client.FullName + "、";
        }
        if (!string.IsNullOrEmpty(clients))
        {
            MessageBox.Show(this, "该门店名称与已有【" + clients.Substring(0, clients.Length - 1) + "】出现相同或相似，请确认是有重复！");
        }
    }

    #region 绑定下拉列表框
    public void BindDropDown()
    {
        CM_Client m = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        ddl_Promotor.DataTextField = "Name";
        ddl_Promotor.DataValueField = "ID";
        ddl_Promotor.DataSource = PM_PromotorBLL.GetModelList(" OrganizeCity in (" + m.OrganizeCity + @") AND Dimission=1 AND ApproveFlag=1 AND 
            (MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Promotor',ExtPropertys,'Classfiy')='2' OR ID NOT IN (SELECT Promotor FROM MCS_Promotor.dbo.PM_PromotorInRetailer))");
        ddl_Promotor.DataBind();
        ddl_Promotor.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_state.DataSource = DictionaryBLL.GetDicCollections("CM_ContractState");
        ddl_state.DataBind();
        ddl_state.Items.Insert(0, new ListItem("所有", "0"));
        ddl_state.SelectedValue = "3";
    }

    void ddl_ActiveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        if (ddl_ActiveFlag.SelectedValue == "2")
        {
            ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = "";
        }
    }
    #endregion

    private void BindData()
    {
        CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        CM_Client m = _bll.Model;
        switch (m.ClientType)
        {
            case 1:
                Response.Redirect("../Store/StoreDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            case 2:
                Response.Redirect("../DI/DistributorDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            case 3:
                break;
            default:
                MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
                break;
        }

        pl_detail.BindData(m);


        //lbl_preSales.Text = _bll.GetSalesVolume(AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();
        //lbl_AvageSales.Text = _bll.GetSalesVolumeAvg().ToString();

        MCSSelectControl select_ClientManager = (MCSSelectControl)pl_detail.FindControl("CM_Client_ClientManager");
        if (select_ClientManager != null) select_ClientManager.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + m.OrganizeCity + "&IncludeSuperManager=Y";

        MCSSelectControl select_Supplier = (MCSSelectControl)pl_detail.FindControl("CM_Client_Supplier");
        if (select_Supplier != null) select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCityEnabled=Y&OrganizeCity=" + m.OrganizeCity + "&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,2)\"";

        MCSSelectControl select_Supplier2 = (MCSSelectControl)pl_detail.FindControl("CM_Client_Supplier2");
        if (select_Supplier2 != null) select_Supplier2.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCityEnabled=Y&OrganizeCity=" + m.OrganizeCity + "&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,2)\"";

        if (m.ApproveFlag == 1)
        {
            //已审核
            TextBox tbx_OpenTime = (TextBox)pl_detail.FindControl("CM_Client_OpenTime");

            if (m.ActiveFlag == 1)
            {
                if (tbx_OpenTime != null && tbx_OpenTime.Text != "") tbx_OpenTime.Enabled = false;
                bt_AddApply.Visible = false;
            }
            else
            {
                TextBox tbx_CloseTime = (TextBox)pl_detail.FindControl("CM_Client_CloseTime");
                if (tbx_CloseTime != null && tbx_CloseTime.Text != "") tbx_CloseTime.Enabled = false;
                bt_RevocationApply.Visible = false;
            }

            Header.Attributes["WebPageSubCode"] = "Modify";
            bt_Approve.Visible = false;
        }
        else
        {
            bt_RevocationApply.Visible = false;

        }

        if (string.IsNullOrEmpty(m["TaskID"]))
        {
            bt_Record.Visible = false;
        }
        else
        {
            if (m["State"] == "2")
            {
                //审批中，不可修改数据
                bt_AddApply.Visible = false;
                bt_RevocationApply.Visible = false;
                bt_OK.Visible = false;
                bt_RevocationApply.Visible = false;
            }
        }


        #region 绑定该客户的首要联系人
        DropDownList ddl_ChiefLinkMan = pl_detail.FindControl("CM_Client_ChiefLinkMan") != null ? (DropDownList)pl_detail.FindControl("CM_Client_ChiefLinkMan") : null;
        try
        {
            ddl_ChiefLinkMan.DataTextField = "Name";
            ddl_ChiefLinkMan.DataValueField = "ID";
            ddl_ChiefLinkMan.DataSource = CM_LinkManBLL.GetModelList("ClientID=" + ViewState["ClientID"].ToString());
            ddl_ChiefLinkMan.DataBind();

            ddl_ChiefLinkMan.Items.Insert(0, new ListItem("请选择", "0"));
            ddl_ChiefLinkMan.SelectedValue = m.ChiefLinkMan.ToString();
        }
        catch { }

        #endregion

        BindGrid();
        //MessageBox.Show(this, Session["UserID"].ToString());
        bt_ReplaceClientManager.OnClientClick = "javascript:PopReplaceClientManager(" + m.ClientManager.ToString() + "," + m.ClientType.ToString() + ")";

        //管理员登录状况下  雅慧电商的流通店  数据有误发出警报
        if (Session["UserID"].ToString() == "1" && m["RTChannel"] != "20" && m["RTClassify"] == "1"
            && (m["IsRMSClient"] != "2" || m["RMSAccountEnabled"] != "2"))
        {
            MessageBox.Show(this, "非雅慧电商的流通店积分店状态(" + m["IsRMSClient"] + ")或积分账户启用否(" + m["RMSAccountEnabled"] + ")数据有误");
        }

    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        if (ViewState["ClientID"] == null)
        {
            _bll = new CM_ClientBLL();

        }
        else
        {
            _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            if (CM_LinkManBLL.GetModelList("ClientID=" + ViewState["ClientID"].ToString()).Count == 0)
            {
                MessageBox.Show(this, "对不起，请至少提供一名客户联系人！");
                return;
            }
        }

        string IsRMSClient = _bll.Model["IsRMSClient"];
        pl_detail.GetData(_bll.Model);


        #region 判断必填项
        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "所属的管理片区必填!");
            return;
        }
        if (_bll.Model.OfficalCity == 0)
        {
            MessageBox.Show(this, "所属的行政城市必填!");
            return;
        }
        if (_bll.Model.Supplier == 0)
        {
            MessageBox.Show(this, "供货商信息必填!");
            return;
        }
        if (_bll.Model["Supplier2"] == "")
        {
            MessageBox.Show(this, "赠品供货商信息必填!");
            return;
        }
        if (_bll.Model["OperateProperty"] == "0")
        {
            MessageBox.Show(this, "归属办事处类型必填!");
            return;
        }
        if (new Addr_OfficialCityBLL(_bll.Model.OfficalCity).Model.Level < 3)
        {
            MessageBox.Show(this, "门店所在行政城市必须到区或县城!");
            return;
        }
        if (_bll.Model["RTChannel"] == "0")
        {
            MessageBox.Show(this, "门店渠道必填!");
            return;
        }
        if ((_bll.Model["RTChannel"] == "1" || _bll.Model["RTChannel"] == "2") && _bll.Model["VestKA"] == "0")
        {
            MessageBox.Show(this, "当门店渠道为LKA,或NKA时，KA系统必填!");
            return;
        }
        if (_bll.Model["Classification"] == "" || _bll.Model["Classification"] == "0")
        {
            MessageBox.Show(this, "“门店归类”必填!");
            return;
        }
        if ((_bll.Model["MarketType"] == "" || _bll.Model["MarketType"] == "0"))
        {
            MessageBox.Show(this, "“市场类型”必填!");
            return;
        }
        #endregion

        if (_bll.Model["IsRMSClient"] == "2" && IsRMSClient != "2")
        {
            _bll.Model["RMSCloseDate"] = DateTime.Now.ToString("yyyy-MM-dd");
        }

        string[] RTChannelLimit1 = ConfigHelper.GetConfigString("RTChannelLimit1").Split(',');
        string[] RTChannelLimit2 = ConfigHelper.GetConfigString("RTChannelLimit2").Split(',');
        string[] RT_Classification1 = ConfigHelper.GetConfigString("Classification1").Split(',');
        string[] RT_Classification2 = ConfigHelper.GetConfigString("Classification2").Split(',');
        if (RTChannelLimit1.Contains(_bll.Model["RTChannel"]) && !RT_Classification1.Contains(_bll.Model["Classification"])
            || RTChannelLimit2.Contains(_bll.Model["RTChannel"]) && !RT_Classification2.Contains(_bll.Model["Classification"]))
        {
            MessageBox.Show(this, "“门店渠道”与“门店归类”匹配不规范，请重新选填!");
            return;
        }


        #region 判断活跃标志
        if (_bll.Model.ActiveFlag == 1 && _bll.Model.CloseTime != new DateTime(1900, 1, 1))
            _bll.Model.CloseTime = new DateTime(1900, 1, 1);

        if (_bll.Model.ActiveFlag == 2 && _bll.Model.CloseTime == new DateTime(1900, 1, 1))
            _bll.Model.CloseTime = DateTime.Now;
        #endregion

        #region 自动设定“是否促销店”字段
        switch (_bll.Model["RTClassify"])
        {
            case "2":      //返利店
                _bll.Model["IsPromote"] = "2";
                _bll.Model["IsRebate"] = "1";
                break;
            case "3":      //导购店
                _bll.Model["IsPromote"] = "1";
                _bll.Model["IsRebate"] = "2";
                break;
            case "4":      //导购返利店
                _bll.Model["IsPromote"] = "1";
                _bll.Model["IsRebate"] = "1";
                break;
            case "1":      //流通店
            default:
                _bll.Model["IsPromote"] = "2";
                _bll.Model["IsRebate"] = "2";
                break;
        }
        #endregion

        Addr_OfficialCity city = new Addr_OfficialCityBLL(_bll.Model.OfficalCity).Model;
        if (city != null)
        {
            if (_bll.Model.PostCode == "") _bll.Model.PostCode = city.PostCode;
            if (_bll.Model["Township"] == "" && city.Level == 4) _bll.Model["Township"] = city.Name;
        }

        if (ViewState["ClientID"] == null)
        {
            _bll.Model.ClientType = 3;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ClientID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }
        if (sender != null)
        {
            MessageBox.ShowAndRedirect(this, "保存终端门店资料成功！", "RetailerDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }

    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            _bll.Model.ApproveFlag = 1;
            _bll.Model.ActiveFlag = 1;
            if (_bll.Model.OpenTime.Year == 1900) _bll.Model.OpenTime = DateTime.Today;
            _bll.Model.CloseTime = new DateTime(1900, 1, 1);
            _bll.Update();
            MessageBox.ShowAndRedirect(this, "审核终端门店资料成功！", "RetailerDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }

    private void BindGrid()
    {
        if (ViewState["ClientID"] != null)
        {
            string ConditionStr = "CM_LinkMan.ClientID = " + ViewState["ClientID"].ToString();
            gv_List.ConditionString = ConditionStr;
            gv_List.BindGrid();

            gv_Promotor.ConditionString = "PM_PromotorInRetailer.Client = " + ViewState["ClientID"].ToString();
            gv_Promotor.BindGrid();

            #region 判断是否允许新增导购名单至该店
            //IList<CM_Contract> contracts = CM_ContractBLL.GetModelList("Client= " + ViewState["ClientID"].ToString() +
            //    " AND GETDATE() BETWEEN BeginDate AND DATEADD(day,1,ISNULL(EndDate,GETDATE())) AND State=3");

            //CM_Contract _c = contracts.FirstOrDefault(p => p.Classify == 3);
            //if (_c == null || _c["PromotorCount"] == "" || gv_Promotor.Rows.Count >= int.Parse(_c["PromotorCount"]))
            //{
            //    ddl_Promotor.Enabled = false;
            //    bt_AddPromotor.Enabled = false;
            //}
            //else
            //{
            //    ddl_Promotor.Enabled = true;
            //    bt_AddPromotor.Enabled = true;
            //}
            #endregion

            gv_list01.ConditionString = "CM_Contract.State<8 AND CM_Contract.Client = " + ViewState["ClientID"].ToString();
            gv_list01.BindGrid();

            IList<CM_Contract> _listcontract = CM_ContractBLL.GetModelList("State IN(1,2) AND Classify=2 AND Client=" + ViewState["ClientID"].ToString() + " Order By EndDate DESC");
            if (_listcontract.Count > 0)
            {
                bt_AddContract2.Enabled = false;
                bt_AddContract2.ToolTip = "该门店有未提交或审批中返利协议，请先处理完再新增";
            }
            _listcontract = CM_ContractBLL.GetModelList("State IN(1,2) AND Classify=3 AND Client=" + ViewState["ClientID"].ToString() + " Order By EndDate DESC");
            if (_listcontract.Count > 0)
            {
                bt_AddContract3.Enabled = false;
                bt_AddContract3.ToolTip = "该门店有未提交或审批中导购协议，请先处理完再新增";
            }
        }
    }

    protected void bt_AddLinkMan_Click(object sender, EventArgs e)
    {
        Response.Redirect("../LM/LinkManDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&URL=~/SubModule/CM/RT/RetailerDetail.aspx");
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void tbx_Code_TextChanged(object sender, EventArgs e)
    {
        if (!Check_CMCode())
        {
            MessageBox.Show(this, "该门店的编号已经存在,请重新输入!");
            ((TextBox)pl_detail.FindControl("CM_Client_Code")).Text = "";
        }
    }

    private bool Check_CMCode()
    {
        string code = ((TextBox)pl_detail.FindControl("CM_Client_Code")).Text;
        if (ViewState["ClientID"] != null && code != "")
        {
            if (CM_ClientBLL.GetModelList(" Code = '" + code + "' and ID<>" + ViewState["ClientID"].ToString()).Count > 0)
                return false;
        }
        else
        {
            if (CM_ClientBLL.GetModelList(" Code = '" + code + "'").Count > 0)
                return false;
        }
        return true;
    }

    protected void bt_AddPromotor_Click(object sender, EventArgs e)
    {
        //判断门店返利协议是否已过期
        bool flag = true;
        string condition = " Client=" + ViewState["ClientID"].ToString() + "  AND Classify=2 ";
        IList<CM_Contract> list = CM_ContractBLL.GetModelList(condition);
        foreach (CM_Contract contract in list)
        {
            int currentMonth = AC_AccountMonthBLL.GetCurrentMonth();
            int endMonth = AC_AccountMonthBLL.GetMonthByDate(contract.EndDate);

            if (contract.State == 3 || contract.State == 9 && endMonth >= currentMonth)
            {
                flag = false;
                break;
            }
        }
        if (!flag)
        {
            MessageBox.Show(this, "当前门店存在返利协议费用");
            return;
        }

        PM_PromotorInRetailerBLL bll = new PM_PromotorInRetailerBLL();
        bll.Model.Client = int.Parse(ViewState["ClientID"].ToString());
        bll.Model.Promotor = int.Parse(ddl_Promotor.SelectedValue.ToString());
        if (bll.Model.Promotor <= 0)
        {
            MessageBox.Show(this, "导购员未选择，请选择需要添加的导购员！");
            return;
        }
        if (bll._GetModelList(" Promotor=" + bll.Model.Promotor + " and  Client=" + bll.Model.Client).Count > 0)
        {
            MessageBox.Show(this, "该导购员那已经存在！");
            return;
        }
        else
        {
            CM_ClientBLL _cm = new CM_ClientBLL(bll.Model.Client);
            //导购店添加返利协议
            if (_cm.Model["RTClassify"] == "2")
            {
                MessageBox.Show(this, _cm.CheckRealClassifyShowMessage(2));
                return;
            }
            bll.Add();
        }
        ddl_Promotor.SelectedValue = "0";
        BindData();

    }

    protected void gv_Promotor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string promotor = gv_Promotor.DataKeys[e.RowIndex]["PM_Promotor_ID"].ToString();
        IList<PM_PromotorInRetailer> _list = PM_PromotorInRetailerBLL.GetModelList("Promotor=" + promotor);
        if (_list.Count > 1)
        {
            int id = int.Parse(gv_Promotor.DataKeys[e.RowIndex]["PM_PromotorInRetailer_ID"].ToString());
            PM_PromotorInRetailerBLL bll = new PM_PromotorInRetailerBLL(id);
            bll.Delete();
            BindData();
        }
        else
        {
            MessageBox.Show(this, "请确保将导购关联新的门店再删除！");
            return;
        }
    }

    protected void bt_AddContract_Click(object sender, EventArgs e)
    {
        Response.Redirect("RetailerContractDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&Classify=1");
    }

    /// <summary>
    /// 新增返利协议
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_AddContract2_Click(object sender, EventArgs e)
    {
        bool flag = true;//允许生生成陈列协议

        string condition = " Client=" + ViewState["ClientID"].ToString();
        IList<PM_PromotorInRetailer> list = PM_PromotorInRetailerBLL.GetModelList(condition);
        for (int i = 0; i < list.Count; i++)
        {
            PM_PromotorBLL PM_PromotorBLL = new PM_PromotorBLL(list[i].Promotor);
            PM_Promotor promoter = PM_PromotorBLL.Model;
            int endMonth = AC_AccountMonthBLL.GetMonthByDate(promoter.EndWorkDate);
            int currentMonth = AC_AccountMonthBLL.GetCurrentMonth();
            //导购在职或导购离职判断离职日期，即使无销量也需生成基本工资
            if (promoter.Dimission == 1 || promoter.Dimission == 2 && endMonth >= currentMonth)//
            {
                flag = false;
                break;
            }
        }
        if (!flag)
        {
            MessageBox.Show(this, "当前门店存在导购或本月需生成导购工资");
            return;
        }

        bt_OK_Click(null, null);
        CM_ClientBLL _cm = new CM_ClientBLL((int)ViewState["ClientID"]);
        if (_cm.Model["Classification"] == "" || _cm.Model["Classification"] == "0")
        {
            MessageBox.Show(this, "请先选择门店归类再新增返利协议！");
            return;
        }
        //导购店添加返利协议
        if (_cm.Model["RTClassify"] == "3")
        {
            MessageBox.Show(this, _cm.CheckRealClassifyShowMessage(1));
            return;
        }
        Response.Redirect("RetailerContractDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&Classify=2");
    }
    protected void bt_AddContract3_Click(object sender, EventArgs e)
    {
        bt_OK_Click(null, null);
        CM_ClientBLL _cm = new CM_ClientBLL((int)ViewState["ClientID"]);
        IList<PM_PromotorInRetailer> pms = PM_PromotorInRetailerBLL.GetModelList("Client=" + ViewState["ClientID"] + "AND EXISTS (SELECT 1 FROM MCS_Promotor.dbo.PM_Promotor " +
                                                                                " WHERE Dimission=1 AND ApproveFlag=1 AND PM_Promotor.ID=PM_PromotorInRetailer.Promotor)");
        if (_cm.Model.ClientManager == 0 || pms.Count == 0)
        {
            MessageBox.Show(this, "门店无负责人或者无导购不能新增导购协议！");
            return;
        }
        Response.Redirect("RetailerContractDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&Classify=3");
    }
    protected void gv_list01_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_list01.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("RetailerContractDetail.aspx?ContractID=" + id);
    }

    protected void bt_Analysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("RetailerAnalysis.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    protected void bt_AddApply_Click(object sender, EventArgs e)
    {
        bt_OK_Click(null, null);
        if (CM_LinkManBLL.GetModelList("ClientID=" + ViewState["ClientID"].ToString()).Count == 0)
        {
            MessageBox.Show(this, "对不起，请至少提供一名客户联系人！");
            return;
        }
        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", bll.Model.ID.ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("ClientName", bll.Model.FullName);
        dataobjects.Add("Channel", bll.Model["RTChannel"]);
        dataobjects.Add("StoreAnalysis", bll.Model["Store_Analysis"]);
        dataobjects.Add("IsACClient", bll.Model["IsACClient"]);

        int TaskID = EWF_TaskBLL.NewTask("Add_Retailer", (int)Session["UserID"], "终端门店名称：" + bll.Model.FullName, "~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=" + ViewState["ClientID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
            //new EWF_TaskBLL(TaskID).Start();        //直接启动流程
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }

    protected void bt_Record_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] == 0)
        {
            MessageBox.Show(this, "对不起，当前还没有审批记录");
            return;
        }

        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);

        Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + bll.Model["TaskID"].ToString());
    }

    protected void bt_RevocationApply_Click(object sender, EventArgs e)
    {
        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);

        if (PM_PromotorInRetailerBLL.GetModelList("Client=" + bll.Model.ID.ToString() + " AND EXISTS(SELECT 1 FROM MCS_Promotor.dbo.PM_Promotor WHERE PM_Promotor.ID=PM_PromotorInRetailer.Promotor AND Dimission=1 AND ApproveFlag=1)").Count > 0)
        {
            MessageBox.Show(this, "该门店还有关联的导购，请转移或解除与该门店的关系！");
            return;
        }

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ClientID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("ClientName", bll.Model.FullName.ToString());
        dataobjects.Add("Channel", bll.Model["RTChannel"]);
        dataobjects.Add("CloseTime", DateTime.Now.ToShortDateString());
        dataobjects.Add("Remark", "门店终止合作");
        dataobjects.Add("IsKeyClient", bll.Model["IsKeyClient"].ToString());


        int TaskID = EWF_TaskBLL.NewTask("Revocation_Retailer", (int)Session["UserID"], "撤销门店,名称：" + bll.Model.FullName, "~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=" + ViewState["ClientID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }

    protected void bt_ReplaceClientManager_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["ClientID"] != null)
        {
            gv_list01.ConditionString = "CM_Contract.Client = " + ViewState["ClientID"].ToString() + (ddl_state.SelectedValue == "0" ? "" : " AND CM_Contract.State=" + ddl_state.SelectedValue);
            gv_list01.BindGrid();
        }
    }

    protected void CM_Client_OfficalCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        //int officalcity=0;
        //int.TryParse(CM_Client_OfficalCity.SelectValue, out officalcity);
        //Addr_OfficialCityBLL _bll=new Addr_OfficialCityBLL(officalcity);
        //if (_bll.Model != null && _bll.Model.Level > 2)
        //{
        //    CM_Client_MarketType.SelectedValue = (_bll.Model.Level - 1).ToString();
        //}
        //else
        //{
        //    CM_Client_MarketType.SelectedValue = "1";
        //}
    }
}
