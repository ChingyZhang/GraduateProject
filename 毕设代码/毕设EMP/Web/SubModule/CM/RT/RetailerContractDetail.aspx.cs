using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.Promotor;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.FNA;
using MCSFramework.Model;

public partial class SubModule_CM_RT_RetailerContractDetail : System.Web.UI.Page
{
    protected decimal SalesTarget = 1M;
    protected DropDownList ddl_PartyC, ddl_PartyCSignMan, ddl_RebateLevel;
    protected TextBox tbx_EndDate, tbx_BeginDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 动态加入事件
        ddl_PartyCSignMan = (DropDownList)pl_detail.FindControl("CM_Contract_PartyCSignMan");
        ddl_PartyC = (DropDownList)pl_detail.FindControl("CM_Contract_PartyC");
        if (ddl_PartyC != null)
        {
            ddl_PartyC.AutoPostBack = true;
            ddl_PartyC.SelectedIndexChanged += new EventHandler(ddl_PartyC_SelectedIndexChanged);
        }

        ddl_RebateLevel = (DropDownList)pl_detail.FindControl("CM_Contract_RebateLevel");
        if (ddl_RebateLevel != null)
        {
            ddl_RebateLevel.AutoPostBack = true;
            ddl_RebateLevel.SelectedIndexChanged += new EventHandler(ddl_RebateLevel_SelectedIndexChanged);
        }
        tbx_EndDate = pl_detail.FindControl("CM_Contract_EndDate") != null ? (TextBox)pl_detail.FindControl("CM_Contract_EndDate") : null;
        tbx_BeginDate = pl_detail.FindControl("CM_Contract_BeginDate") != null ? (TextBox)pl_detail.FindControl("CM_Contract_BeginDate") : null;
        
        #endregion

        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ContractID"] = Request.QueryString["ContractID"] == null ? 0 : int.Parse(Request.QueryString["ContractID"]);
            ViewState["Classify"] = Request.QueryString["Classify"] == null ? 0 : int.Parse(Request.QueryString["Classify"]);
            ViewState["ClientID"] = 0;
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }

            if ((int)ViewState["ContractID"] == 0 && (int)ViewState["ClientID"] == 0) Response.Redirect("~/SubModule/DeskTop.aspx");
            #endregion

            BindDropDown();

            #region 创建空的列表
            ListTable<CM_ContractDetail> _details = new ListTable<CM_ContractDetail>(new CM_ContractBLL((int)ViewState["ContractID"]).Items, "ID");
            ViewState["MAXID"] = _details.GetListItem().Count > 0 ? _details.GetListItem().Max(p => p.ID) : 0;
            ViewState["Details"] = _details;
            #endregion

            if ((int)ViewState["ContractID"] != 0)
            {
                BindData();
                DropDownList ddl_PromotorCostPayMode = (DropDownList)pl_detail.FindControl("CM_Contract_PromotorCostPayMode");
                if (ddl_PromotorCostPayMode != null)
                {
                    //月付的管理费，不需上协议
                    if (ddl_PromotorCostPayMode.Items.FindByValue("1") != null)
                        ddl_PromotorCostPayMode.Items.FindByValue("1").Enabled = false;

                    if (ddl_PromotorCostPayMode.Items.FindByValue("20") != null)
                        ddl_PromotorCostPayMode.Items.FindByValue("20").Enabled = false;
                }
            }
            else if ((int)ViewState["ClientID"] != 0)
            {
                Label l = (Label)pl_detail.FindControl("CM_Contract_Client");
                CM_Client m = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                if (l != null) l.Text = m.FullName.ToString();

                DropDownList ddl_Classify = (DropDownList)pl_detail.FindControl("CM_Contract_Classify");
                if (ddl_Classify != null) ddl_Classify.SelectedValue = ViewState["Classify"].ToString();
                BindContractClassify((int)ViewState["Classify"]);

             

                if (tbx_BeginDate != null && tbx_EndDate != null)
                {
                    tbx_BeginDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    tbx_EndDate.Text = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");

                    if (((int)ViewState["Classify"] == 2 || (int)ViewState["Classify"] == 3) && CM_ContractBLL.GetModelList("State=3 AND Classify=" + ViewState["Classify"].ToString() + " AND Client=" + ViewState["ClientID"].ToString()).Count > 0)
                    {
                        MessageBox.Show(this, "该协议同时只能有一条生效，请先中止生效的协议后再新增");
                    }
                    //判断是否门店关联离职导购

                }

                if (ddl_PartyCSignMan != null) ddl_PartyCSignMan.Items.Clear();

                bt_Submit.Visible = false;
                bt_Delete.Visible = false;
                bt_Approve.Visible = false;
                bt_Disable.Visible = false;
                bt_FeeApply.Visible = false;
                UploadFile1.Visible = false;
                bt_print.Visible = false;
                if ((int)ViewState["Classify"] != 1)
                {
                    tr_AddDetail.Visible = false;
                    tr_ContractDetail.Visible = false;
                }
            }

            Header.Attributes["WebPageSubCode"] = "Classify=" + ViewState["Classify"].ToString();
        }
      
        #region 注册弹出窗口脚本
        string script = "function PopSetEndDate(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_SetContractEndDate.aspx") +
            "?ContractID=' + id +'&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pop_SetContractEndDate", script, true);
        #endregion
    }

 
    private void ddl_PartyC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_PartyCSignMan != null)
        {
            ddl_PartyCSignMan.Items.Clear();
            IList<CM_LinkMan> linkmanlist = CM_LinkManBLL.GetModelList("ClientID=" + ddl_PartyC.SelectedValue);
            foreach (CM_LinkMan linkman in linkmanlist)
            {
                ddl_PartyCSignMan.Items.Add(new ListItem(linkman.Name, linkman.ID.ToString()));
            }
        }

    }
    private void BindDropDown()
    {
        #region 绑定费用科目
        int ContractFeeType = ConfigHelper.GetConfigInt("ContractFeeType");
        int ContractAccountTitle = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ContractAccountTitle"]);

        #region 判断是否为KA的会计科目
        int KAType=0;
        if (ViewState["ContractID"] != null && (int)ViewState["ContractID"]>0)
        {
            int.TryParse(new CM_ContractBLL((int)ViewState["ContractID"]).Model["IsKA"], out KAType);
        }
        else if (ViewState["ClientID"] != null && (int)ViewState["ClientID"] > 0)
        {
            CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
            Dictionary<string, Dictionary_Data> dics = DictionaryBLL.GetDicCollections("CM_RT_KAList");
            if (!dics.ContainsKey(client.Model["VestKA"]))
                KAType = 2;
            else
                KAType = dics[client.Model["VestKA"]].Description.ToUpper() == "KA" ? 1 : 2;
            DropDownList ddl_KAType = null;
            if (pl_detail.FindControl("CM_Contract_IsKA") != null)
                ddl_KAType = pl_detail.FindControl("CM_Contract_IsKA") as DropDownList;
            if (ddl_KAType != null)
                ddl_KAType.SelectedValue = KAType.ToString();
        }
        else
            return;
        if (KAType == 1)
        {
            if (ConfigHelper.GetConfigInt("ContractFeeType-KA") > 0)
            {
                ContractFeeType = ConfigHelper.GetConfigInt("ContractFeeType-KA");
                ContractAccountTitle = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ContractAccountTitle-KA"]);
            }
        }
        #endregion

        ddl_AccountTitle.DataSource = AC_AccountTitleBLL.GetListByFeeType(ContractFeeType).Where(p => p.SuperID == ContractAccountTitle || p.ID == ContractAccountTitle).OrderBy(p => p.Code);
        ddl_AccountTitle.DataBind();


        #endregion

        #region 绑定付款周期
        ddl_PayMode.DataSource = DictionaryBLL.GetDicCollections("PUB_PayMode");
        ddl_PayMode.DataBind();
        //ddl_PayMode.Items.Insert(0, new ListItem("所有", "0"));
        ddl_PayMode.SelectedValue = "1";
        #endregion

        #region 绑定付款模式
        ddl_BearMode.DataSource = DictionaryBLL.GetDicCollections("PUB_BearMode");
        ddl_BearMode.DataBind();
        ddl_BearMode.SelectedValue = "1";
        #endregion

        ddl_YesNO.DataSource = DictionaryBLL.GetDicCollections("PUB_YesOrNo");
        ddl_YesNO.DataBind();
        ddl_YesNO.SelectedValue = "2";
        //ddl_YesNO.Items.Insert(0, new ListItem("所有", "0"));

        #region 绑定产品品牌列表
        cbl_Brand.DataTextField = "Name";
        cbl_Brand.DataValueField = "ID";
        cbl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=1");
        cbl_Brand.DataBind();
        cbx_CheckAll.Checked = true;
        cbx_CheckAll_CheckedChanged(null, null);
        #endregion
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbl_Brand.Items)
        {
            item.Selected = cbx_CheckAll.Checked;
        }
    }

    private void BindContractClassify(int classify)
    {
        switch (classify)
        {
            case 1:     //陈列合同
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_03", false);
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_04", false);
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_05", false);
                bt_FeeApply.Visible = false;
                break;
            case 2:     //返利合同
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_02", false);
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_04", false);
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_05", false);
                bt_FeeApply.Visible = false;
                tr_AddDetail.Visible = false;
                tr_ContractDetail.Visible = false;

                #region 获取适用的返利标准
                if (ddl_RebateLevel != null && (int)ViewState["ClientID"] > 0)
                {
                    ddl_RebateLevel.Items.Clear();
                    int city = new CM_ClientBLL((int)ViewState["ClientID"]).Model.OrganizeCity;

                    IList<CM_RebateRule> rules = CM_RebateRuleBLL.GetModelList
                        ("ApproveFlag = 1 AND ID IN (SELECT RebateRule FROM CM_RebateRule_ApplyCity WHERE OrganizeCity IN ("
                        + new Addr_OrganizeCityBLL(city).GetAllSuperNodeIDs() + "," + city.ToString() + "))");
                    foreach (CM_RebateRule item in rules)
                    {
                        ddl_RebateLevel.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                    }
                    ddl_RebateLevel.Items.Insert(0, new ListItem("请选择...", "0"));
                }
                #endregion

                #region 设置默认值
                if ((int)ViewState["ContractID"] == 0 && (int)ViewState["ClientID"] > 0)
                {
                    CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                    if (client != null)
                    {
                        TextBox tbx_BankName = (TextBox)pl_detail.FindControl("CM_Contract_BankName");
                        if (tbx_BankName != null) tbx_BankName.Text = client["BankName"];

                        TextBox tbx_BankAccountNo = (TextBox)pl_detail.FindControl("CM_Contract_BankAccountNo");
                        if (tbx_BankAccountNo != null) tbx_BankAccountNo.Text = client["BankAccountNo"];

                        TextBox tbx_AccountName = (TextBox)pl_detail.FindControl("CM_Contract_AccountName");
                        if (tbx_AccountName != null) tbx_AccountName.Text = client["AccountName"];
                    }
                }
                #endregion

                break;
            case 3:     //导购费用协议
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_02", false);
                pl_detail.SetPanelVisible("Panel_RT_RetailDetail_Contract_03", false);
                bt_FeeApply.Visible = false;
                #region 设置默认值
                if ((int)ViewState["ContractID"] == 0)
                {
                    TextBox tbx_PromotorCostRate = (TextBox)pl_detail.FindControl("CM_Contract_PromotorCostRate");
                    if (tbx_PromotorCostRate != null) tbx_PromotorCostRate.Text = "0";

                    TextBox tbx_PromotorAwardRate = (TextBox)pl_detail.FindControl("CM_Contract_PromotorAwardRate");
                    if (tbx_PromotorAwardRate != null) tbx_PromotorAwardRate.Text = "100";

                    //TextBox tbx_PromotorCount =pl_detail.FindControl("CM_Contract_PromotorCount")==null?null:(TextBox)pl_detail.FindControl("CM_Contract_PromotorCount");
                    //IList<PM_PromotorInRetailer> pms = PM_PromotorInRetailerBLL.GetModelList("Client=" + ViewState["ClientID"] + "AND EXISTS (SELECT 1 FROM MCS_Promotor.dbo.PM_Promotor " +
                    //                                                           " WHERE Dimission=1 AND ApproveFlag=1 AND PM_Promotor.ID=PM_PromotorInRetailer.Promotor)");
                    //if (tbx_PromotorCount != null && pms.Count > 0)
                    //{
                    //    tbx_PromotorCount.Text = pms.Count.ToString();
                    //}

                    DropDownList ddl_PromotorCostPayMode = (DropDownList)pl_detail.FindControl("CM_Contract_PromotorCostPayMode");
                    if (ddl_PromotorCostPayMode != null)
                    {
                        //月付的管理费，不需上协议
                        if (ddl_PromotorCostPayMode.Items.FindByValue("1") != null)
                            ddl_PromotorCostPayMode.Items.FindByValue("1").Enabled = false;

                        if (ddl_PromotorCostPayMode.Items.FindByValue("20") != null)
                            ddl_PromotorCostPayMode.Items.FindByValue("20").Enabled = false;
                    }
                    
                }
                #endregion
                lt_RebateRemark.Text = "<div style=\"color:red;margin-left:5px;\"><b>导购协议填报说明：</b><br/>方式一：门店仅需做预付管理费申请，请填写模块一，需填写导购员数量，管理费（每人每月每店）及预付款方式，模块二内容无需填写。<br/>方式二：门店仅需申请特殊提成分摊比例，请填写模块二，需填写经销商承担比例（%）。（注：模块一中的管理费（每人每月每店）填写数字0，预付款方式可按备选内容进行选填）<br/>方式三：门店既需预付管理费，又需要申请特殊特提成分摊比例，请分别填写模块一、二，所有必填字段均需按实际填写。</div><br>";
                tr_AddDetail.Visible = false;
                tr_ContractDetail.Visible = false;
                break;
            default:
                break;
        }
    }

    void ddl_RebateLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int level = int.Parse(ddl_RebateLevel.SelectedValue);

        CM_RebateRule Rebate = new CM_RebateRuleBLL(level).Model;
        if (Rebate != null)
        {
            TextBox tbx_RebateRate = (TextBox)pl_detail.FindControl("CM_Contract_RebateRate");
            if (tbx_RebateRate != null)
            {
                tbx_RebateRate.Text = Rebate.RebateRate.ToString("0.##");
                tbx_RebateRate.Enabled = false;
            }

            TextBox tbx_DIRebateRate = (TextBox)pl_detail.FindControl("CM_Contract_DIRebateRate");
            if (tbx_DIRebateRate != null)
            {
                if (Rebate.DIRebateRate + Rebate.RebateRate != 0)
                {
                    tbx_DIRebateRate.Text = Rebate.DIRebateRate.ToString("0.##");
                    tbx_DIRebateRate.Enabled = false;
                }
                else
                {
                    tbx_DIRebateRate.Text = "0";
                    tbx_DIRebateRate.Enabled = true;
                }
            }

            lt_RebateRemark.Text = Rebate.Remark;

            HyperLink hy = new HyperLink();
            hy.ID = "hy_ViewTradeInPrice";
            hy.Text = "查看返利价盘";
            hy.ForeColor = System.Drawing.Color.Blue;
            hy.Target = "_blank";
            hy.NavigateUrl = "~/SubModule/Product/PDT_StandardPriceDetail_OnlyTradeInPrice.aspx?PriceID=" + Rebate.StandardPrice.ToString();
            ddl_RebateLevel.Parent.Controls.Add(hy);
        }
        else
        {
            lt_RebateRemark.Text = "";
        }
    }
    /// <summary>
    /// 设置合同的甲乙丙三方
    /// </summary>
    public void SetPartyABC()
    {
        #region 甲方
        DropDownList ddl_Owner = (DropDownList)pl_detail.FindControl("CM_Contract_Owner");
        if (ddl_Owner != null)
        {
            ddl_Owner.Items.Insert(0, new ListItem("请选择", "0"));
            ddl_Owner.Items.Insert(1, new ListItem("本公司", "1"));
        }
        DropDownList ddl_OwnerSignMan = (DropDownList)pl_detail.FindControl("CM_Contract_OwnerSignMan");

        if (ddl_OwnerSignMan != null)
        {
            ddl_OwnerSignMan.DataSource = CM_ContractBLL.GetOwners((int)ViewState["ClientID"]);
            ddl_OwnerSignMan.DataTextField = "Name";
            ddl_OwnerSignMan.DataValueField = "ID";
            ddl_OwnerSignMan.DataBind();
            ddl_OwnerSignMan.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion

        #region 乙方签定人
        DropDownList ddl_SignMan = (DropDownList)pl_detail.FindControl("CM_Contract_SignMan");
        if (ddl_SignMan != null)
        {
            ddl_SignMan.DataSource = CM_LinkManBLL.GetModelList("ClientID=" + ViewState["ClientID"].ToString());
            ddl_SignMan.DataTextField = "Name";
            ddl_SignMan.DataValueField = "ID";
            ddl_SignMan.DataBind();
        }
        #endregion

        #region 丙方
        if (ddl_PartyC != null)
        {
            ddl_PartyC.DataSource = CM_ContractBLL.GetPartyC((int)ViewState["ClientID"]);
            ddl_PartyC.DataTextField = "Name";
            ddl_PartyC.DataValueField = "ID";
            ddl_PartyC.DataBind();
            ddl_PartyC.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion
    }

    public void BindData()
    {
        CM_Contract c = new CM_ContractBLL((int)ViewState["ContractID"]).Model;
        if (c != null)
        {
            if (c["Classify"] == "") c["Classify"] = "1";
            pl_detail.BindData(c);
            if (c["Classify"] != "1" && c["Classify"] != "2") bt_print.Visible = false;
            ViewState["ClientID"] = c.Client;
            ViewState["Classify"] = int.Parse(c["Classify"]);     //合同类别
            BindContractClassify((int)ViewState["Classify"]);
            if (c.Classify == 2 && ddl_RebateLevel != null)
            {
                ddl_RebateLevel.SelectedValue = c["RebateLevel"];
                //ddl_RebateLevel_SelectedIndexChanged(null, null);
            }
            BindGrid();
            UploadFile1.RelateID = c.ID;

            #region 绑定丙方负责人
            if (ddl_PartyC != null) ddl_PartyC_SelectedIndexChanged(ddl_PartyC, null);
            if (ddl_PartyCSignMan != null) ddl_PartyCSignMan.SelectedValue = c["PartyCSignMan"];
            #endregion

            if (c.ApproveFlag == 1 || c.State == 2 || c.State == 3)
            {
                //已审核
                pl_detail.SetControlsEnable(false);
                bt_Submit.Visible = false;
                bt_Approve.Visible = false;
                bt_OK.Visible = false;
                bt_AddDetail.Visible = false;
                bt_Delete.Visible = false;

                tr_AddDetail.Visible = false;
                gv_Detail.Columns[gv_Detail.Columns.Count - 1].Visible = false;

                UploadFile1.CanDelete = false;
                //UploadFile1.CanUpload = false;
            }
            if (c.State != 3)
            {
                bt_Disable.Visible = false;
                bt_FeeApply.Visible = false;
            }

            if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1101, "EditEndDate") && (c.State == 3 || c.State == 9))
            {
                tbx_EndDate.Enabled = true;
                bt_OK.Visible = true;
            }
            if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1101, "EditBeginDate") && (c.State == 3 || c.State == 9))
            {
                tbx_BeginDate.Enabled = true;
                bt_OK.Visible = true;
            }
            if (c.State == 9) UploadFile1.CanDelete = false;

            ///审批过程中，可以修改编码 Decision参数为在审批过程中传进来的参数
            if (c.State == 2 && Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
            {
                bt_OK.Visible = true;
                //gv_Detail.Enabled = false;
                pl_detail.SetControlsEnable(false);
            }

            if (tr_AddDetail.Visible)
                lbl_FeeCycle.Text = GetContractcycle().ToString();

            if (pl_detail.FindControl("hy_ViewTradeInPrice") != null)
            {
                ((HyperLink)pl_detail.FindControl("hy_ViewTradeInPrice")).Enabled = true;
            }



            bt_Disable.OnClientClick = "javascript:PopSetEndDate(" + ViewState["ContractID"].ToString() + ")";
        }
    }

    private void BindGrid()
    {
        TextBox tbx_SalesForcast = pl_detail.FindControl("CM_Contract_SalesForcast") != null ? (TextBox)pl_detail.FindControl("CM_Contract_SalesForcast") : null;

        if (tbx_SalesForcast != null)
        {
            decimal.TryParse(tbx_SalesForcast.Text, out SalesTarget);
            if (SalesTarget == 0) SalesTarget = 1m;
        }


        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        gv_Detail.BindGrid<CM_ContractDetail>(_details.GetListItem());

    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {

        CM_ContractBLL _bll = null;

        #region 判断合同编码是否重复
        //合同编码的获取
        TextBox tbx_ContractCode = pl_detail.FindControl("CM_Contract_ContractCode") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_ContractCode");
        if ((int)ViewState["ContractID"] == 0)
        {
            _bll = new CM_ContractBLL();

            if (tbx_ContractCode != null && tbx_ContractCode.Text != "" && CM_ContractBLL.GetModelList("ContractCode='" + tbx_ContractCode.Text.Trim() + "'").Count > 0)
            {
                MessageBox.Show(this, "对不起，合同编码" + tbx_ContractCode.Text.Trim() + "数据库已存在。");
                return;
            }
        }
        else
        {
            _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
            if (tbx_ContractCode != null && tbx_ContractCode.Text != "" && CM_ContractBLL.GetModelList("ContractCode='" + tbx_ContractCode.Text.Trim() + "' AND ID !=" + _bll.Model.ID.ToString()).Count > 0)
            {
                MessageBox.Show(this, "对不起，合同编码" + tbx_ContractCode.Text.Trim() + "数据库已存在。");
                return;
            }
        }
        #endregion

        pl_detail.GetData(_bll.Model);
        if (((int)ViewState["Classify"] == 2 || (int)ViewState["Classify"] == 3) && (int)ViewState["ContractID"] == 0)
        {
            AC_AccountMonth minbegainmonth = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetCurrentMonth() - 1).Model;
            if (_bll.Model.BeginDate < minbegainmonth.BeginDate)
            {
                MessageBox.Show(this, "对不起，新增协议的合作日期最小值为" + minbegainmonth.BeginDate.ToString("yyyy-MM-dd") + "。");
                return;
            }
        }

        if ((int)ViewState["Classify"] == 2 || (int)ViewState["Classify"] == 3)
        {

            IList<CM_Contract> _listcontract = CM_ContractBLL.GetModelList("ID!=" + ViewState["ContractID"].ToString() + " AND State IN(1,2,3,9) AND Classify=" + ViewState["Classify"].ToString() + " AND Client=" + ViewState["ClientID"].ToString() + " Order By EndDate DESC");
            if (_listcontract.Count > 0)
            {
                AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(_listcontract[0].EndDate)).Model;
                AC_AccountMonth bemonth = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(_listcontract[0].BeginDate)).Model;
                if (month == null)
                {
                     MessageBox.Show(this, "对不起，新增协议的合作日期与原有协议重复");
                    return;
                }
                if (_bll.Model.BeginDate <= month.EndDate && _bll.Model.EndDate >= bemonth.BeginDate)
                {
                    MessageBox.Show(this, "对不起，新增协议的合作日期与原有协议重复，请重新确认!建议的开始日期为" + new AC_AccountMonthBLL(month.ID + 1).Model.BeginDate.ToString("yyyy-MM-dd"));
                    return;
                }
            }
        }
        //导购工资
        #region 导购工资单独判断 2014-05-07 Jace
        if ((int)ViewState["Classify"] == 3)
        {
            //A、若开始日期范围在过去的最新一个会计月且单月管理费用已随工资流程生成，则该导购协议无法进行保存，并由系统提示：“本协议开始日期需重新填写，导购工资已生成过该笔费用”，若开始日期包含过去的最新一个会计月，但工资流程中并未产生该笔费用，则可以进行保存。
            int month = AC_AccountMonthBLL.GetMonthByDate(_bll.Model.BeginDate);
            int applyedmonth = _bll.CheckPMFeeApplyLastMonth(int.Parse(ViewState["ClientID"].ToString()));
            CM_ClientBLL cm = new CM_ClientBLL(int.Parse(ViewState["ClientID"].ToString()));
            if (month <= applyedmonth)
            {
                MessageBox.Show(this, "本协议开始日期需重新填写，导购工资已生成过该笔费用(最大可提前至" + new AC_AccountMonthBLL(applyedmonth + 1).Model.BeginDate + ")!");
                return;
            }
            //C、导购协议合同周期最多不超过2年。
            if (_bll.Model.BeginDate.AddYears(2) < _bll.Model.EndDate)
            {
                MessageBox.Show(this, "导购协议合同周期最多不超过2年!");
                return;
            }
            //B、导购人数录入的数量 不得超过当时申请时门店实际关联的导购人数，但可以少于门店实际人数，最小的基数为1。
            //若门店没有关联导购员或业务员，该门店不得添加导购费用协议，系统提示：“门店没有关联导购员或业务员，请关联后再做申请”。
            int conpmcount = 0, actpmcount = 0;
            int.TryParse(_bll.Model["PromotorCount"],out conpmcount);
            actpmcount = PM_PromotorInRetailerBLL.GetModelList("Client=" + ViewState["ClientID"].ToString()).Count(p => (new PM_PromotorBLL(p.Promotor).Model.Dimission == 1 || new PM_PromotorBLL(p.Promotor).Model.EndWorkDate < new AC_AccountMonthBLL(month).Model.BeginDate));
            if (actpmcount > 0)
            {
                if (actpmcount < conpmcount)
                {
                    MessageBox.Show(this, "导购人数录入的数量不得超过当时申请时门店实际关联的导购人数!");
                    return;
                }
            }
            else
            {
                MessageBox.Show(this, "门店没有关联导购员或业务员，请关联后再做申请！");
                return;
            }
            
            //该门店为LKA或NKA门店，但KA系统没有填写，同样不得添加导购费用协议，系统提示：“门店为KA店，KA系统没有填写，需填写后再做申请”。
            if (cm.Model["RTChannel"] == "1" || cm.Model["RTChannel"] == "2")
            {
                if (cm.Model["VestKA"] == "" || cm.Model["VestKA"] == "0")
                {
                    MessageBox.Show(this, "门店为KA店，KA系统没有填写，需填写后再做申请！");
                    return;
                }
            }
            //门店为非LKA或NKA门店，但KA系统有填写内容，不得添加导购费用协议，系统提示“门店为非KA店，但KA系统错填写内容，请修改后再做申请”。
            else if (cm.Model["VestKA"] != "" && cm.Model["VestKA"] != "0")
            {
                MessageBox.Show(this, "门店为非KA店，但KA系统错填写内容，请修改后再做申请！");
                return;
            }

        #endregion

        }
        if (ViewState["ClientID"] != null && (int)ViewState["Classify"] == 2)
        {
            IList<PM_Promotor> pmlist = PM_PromotorBLL.GetModelList("EXISTS (SELECT 1 FROM MCS_Promotor.dbo.PM_PromotorInRetailer WHERE PM_PromotorInRetailer.Promotor=PM_Promotor.ID AND Client=" + ViewState["ClientID"].ToString()
                                                                    + ")AND Dimission=2 AND ApproveFlag=1 ORDER BY EndWorkDate DESC");
         
            if (pmlist.Count > 0 )
            {
                AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(pmlist[0].EndWorkDate)).Model;
                if (month!=null&&_bll.Model.BeginDate <= month.EndDate)
                {
                    MessageBox.Show(this, "对不起，该开始日期期间为导购店，请重新确认!建议的开始日期为" + new AC_AccountMonthBLL(month.ID + 1).Model.BeginDate.ToString("yyyy-MM-dd"));
                    return;
                }

            }

        }

        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;

        int cycle = GetContractcycle();
        #region 判断必填字段
        //if (_bll.Model.BeginDate < DateTime.Today)
        //{
        //    MessageBox.Show(this, "对不起，合同起始日期不能小于今天!");
        //    return;
        //}
        if (_bll.Model.EndDate <= _bll.Model.BeginDate)
        {
            MessageBox.Show(this, "对不起，合同起始日期不能大于截止日期!");
            return;
        }

        if (_bll.Model.EndDate != _bll.Model.BeginDate.AddMonths(cycle).AddDays(-1))
        {
            MessageBox.Show(this, "提醒，合同截止日期与开始日期不足月匹配，请确认输入是否正确！建议的截止日期为" +
               _bll.Model.BeginDate.AddMonths(cycle).AddDays(-1).ToString("yyyy-MM-dd"));
            return;
        }

        if (_bll.Model.Classify==1 && (_bll.Model["IsKA"] == "" || _bll.Model["IsKA"] == "0"))
        {
            MessageBox.Show(this, "对不起请正确选择是否KA费用！");
            return;
        }
           
        if (_bll.Model.Classify == 2)
        {
            if (_bll.Model["RebateLevel"] == "0")
            {
                MessageBox.Show(this, "对不起请正确选择返利标准！");
                return;
            }
        }

        if (_bll.Model.Classify == 3)
        {
            if (_bll.Model["PromotorCostPayMode"] == "0")
            {
                MessageBox.Show(this, "对不起，请正确选择【导购管理费付款方式】!");
                return;
            }
            #region 增加导购管理费科目明细
            CM_ContractDetail item;
            int accounttitle = ConfigHelper.GetConfigInt("ContractAccountTitle-PromotorCost");
            item = _details.GetListItem().FirstOrDefault(p => p.AccountTitle == accounttitle);
            if (item == null)
            {
                //新增科目
                item = new CM_ContractDetail();
                item.AccountTitle = accounttitle;
                _details.Add(item);
            }
            else
            {
                //修改科目
                _details.Update(item);
            }

            item.ApplyLimit = decimal.Parse(_bll.Model["PromotorCost"]) * int.Parse(_bll.Model["PromotorCount"]);
            item.Amount = item.ApplyLimit * cycle;
            item.FeeCycle = cycle;
            item.BearPercent = 100 - decimal.Parse(_bll.Model["PromotorCostRate"]);
            item.PayMode = int.Parse(_bll.Model["PromotorCostPayMode"]);
            item.Remark = "导购管理费";
            #endregion
        }
        #endregion

        if ((int)ViewState["ContractID"] == 0)
        {

            _bll.Model.State = 1;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.Client = int.Parse(ViewState["ClientID"].ToString());
            CM_ClientBLL _cm = new CM_ClientBLL(_bll.Model.Client);
            //导购店添加返利协议
            if (_cm.Model["RTClassify"] == "3" && (int)ViewState["Classify"] == 2)
            {
                MessageBox.Show(this, _cm.CheckRealClassifyShowMessage(1));
                return;
            }
            _bll.Items = _details.GetListItem();

            ViewState["ContractID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateTime = DateTime.Now;
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();

            #region 修改明细
            _bll.Items = _details.GetListItem(ItemState.Added);
            _bll.AddDetail();

            foreach (CM_ContractDetail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                _bll.DeleteDetail(_deleted.ID);
            }

            foreach (CM_ContractDetail d in _details.GetListItem())
            {
                if (d.PayMode == 20)
                {
                    //一次性支付
                    if (d.ApplyLimit != d.Amount)
                    {
                        d.ApplyLimit = d.Amount;
                        _details.Update(d);
                    }
                }
                else
                {
                    //非一次性支付
                    if (d.ApplyLimit != 0 && d.Amount / d.ApplyLimit != cycle)
                    {
                        d.Amount = d.ApplyLimit * cycle;
                        _details.Update(d);
                    }
                }
            }
            _bll.Items = _details.GetListItem(ItemState.Modified);

            _bll.UpdateDetail();

            #endregion
        }

        if (sender != null)
        {
            if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
                MessageBox.Show(this, "合同编码保存成功！");
            else
                MessageBox.ShowAndRedirect(this, "保存零售商合同详细信息成功！", "RetailerContractDetail.aspx?ContractID=" + ViewState["ContractID"].ToString());
        }
        else
            ViewState["SaveResult"] = true;
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {



        bt_OK_Click(null, null);
        if (ViewState["SaveResult"] != null && (bool)ViewState["SaveResult"])
        {
            CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
            if (_bll.Model.ApproveTask > 0 && _bll.Model.State == 2)
            {
                MessageBox.Show(this, "对不起，该协议已发起过流程，请勿重发！");
                return;
            }
            CM_Client client = new CM_ClientBLL(_bll.Model.Client).Model;

            if (_bll.Model.Classify == 1 && _bll.Items.Count == 0)
            {
                MessageBox.Show(this, "对不起，陈列合同必须要录入合同具体的付款科目才能提交申请！");
                return;
            }

            _bll.Items.Sum(p => p.ApplyLimit);

            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", _bll.Model.ID.ToString());
            dataobjects.Add("Classify", _bll.Model.Classify.ToString());
            dataobjects.Add("PromotorAwardRate", _bll.Model["PromotorAwardRate"]);
            dataobjects.Add("OrganizeCityID", client.OrganizeCity.ToString());
            dataobjects.Add("RTChannel", client["RTChannel"]);
            dataobjects.Add("RTClassify", client["RTClassify"]);
            dataobjects.Add("ApplyCost", _bll.Items.Sum(p => p.ApplyLimit).ToString("0.##"));

            #region 最长一个周期的付款方式
            if (_bll.Items.Count > 0)
            {
                int paymode = _bll.Items.Max(p => p.PayMode);
                dataobjects.Add("PayMode", paymode.ToString());
            }
            #endregion
            decimal clientavgsales = new CM_ClientBLL(_bll.Model.Client).GetSalesVolumeAvg(AC_AccountMonthBLL.GetCurrentMonth(), 3);
            decimal FeeRate = 0;
            if (clientavgsales > 0)
            {
                FeeRate = Math.Round(_bll.Items.Sum(p => p.ApplyLimit) / clientavgsales * 100, 2);
            }
            dataobjects.Add("FeeRate", FeeRate.ToString());
            #region 判断是否首次新增该类型的合同
            if (CM_ContractBLL.GetModelList("Client=" + client.ID + " AND Classify=" + _bll.Model.Classify + " AND State IN (3,9)").Count == 0)
                dataobjects.Add("IsFirstContract", "Y");
            else
            {
                dataobjects.Add("IsFirstContract", "N");

                //返利合同，如果银行帐户信息发生变更，识为新增合同-----2013-11-16日注释去除限制
                //if (_bll.Model.Classify == 2)
                //    if (_bll.Model["BankName"] != client["BankName"] ||
                //        _bll.Model["BankAccountNo"] != client["BankAccountNo"] ||
                //        _bll.Model["AccountName"] != client["AccountName"])
                //        dataobjects["IsFirstContract"] = "Y";
            }
            #endregion

            #region 组织任务标题
            string _title = "";
            Label lb_Client = (Label)pl_detail.FindControl("CM_Contract_Client");
            if (lb_Client != null) _title += "门店名称:" + lb_Client.Text;

            DropDownList ddl_Classify = (DropDownList)pl_detail.FindControl("CM_Contract_Classify");
            if (ddl_Classify != null) _title += " 合同类别:" + ddl_Classify.SelectedItem.Text;

            if (_bll.Model.ContractCode != "") _title += " 合同编码:" + _bll.Model.ContractCode;
            #endregion

            int TaskID = EWF_TaskBLL.NewTask("CM_Contract_Flow", (int)Session["UserID"], _title,
                "~/SubModule/CM/RT/RetailerContractDetail.aspx?ContractID=" + ViewState["ContractID"].ToString(), dataobjects);

            if (TaskID > 0)
            {
                _bll.Model.State = 2;
                _bll.Model.ApproveTask = TaskID;
                _bll.Update();
                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }

            Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
        }
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        bt_OK_Click(null, null);
        if (ViewState["SaveResult"] != null && (bool)ViewState["SaveResult"])
        {
            CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);

            _bll.Approve(3, (int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "审核成功！", "RetailerDetail.aspx?ClientID=" + _bll.Model.Client.ToString());
        }

    }
    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        DateTime begindate = new DateTime();
        DateTime enddate = new DateTime();
        TextBox tbx_BeginDate = pl_detail.FindControl("CM_Contract_BeginDate") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_BeginDate");
        TextBox tbx_EndDate = pl_detail.FindControl("CM_Contract_EndDate") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_EndDate");
        if (tbx_BeginDate != null && tbx_EndDate != null)
        {
            DateTime.TryParse(tbx_BeginDate.Text, out begindate);
            DateTime.TryParse(tbx_EndDate.Text, out enddate);
            if (enddate <= begindate)
            {
                MessageBox.Show(this, "合同终止日期不能小于起始日期。");
                return;
            }
        }

        if (ddl_PayMode.SelectedValue == "0")
        {
            MessageBox.Show(this, "付款周期必选！");
            return;
        }

        if (ddl_BearMode.SelectedValue == "2" && ddl_PayMode.SelectedValue == "1")
        {
            MessageBox.Show(this, "付款周期为每月的不支付预提！");
            return;
        }
        decimal displaycount = 0;
        if (!decimal.TryParse(txt_count.Text.Trim(), out displaycount))
        {
            MessageBox.Show(this, "请正确填写数量！");
            return;
        }
        CM_ContractDetail item;
        if (ViewState["Selected"] == null)
        {
            if (ddl_AccountTitle.SelectedValue != "220" && ddl_AccountTitle.SelectedValue != "224")
            {
                IList<CM_ContractDetail> _list = (_details.GetListItem()).Where(p => p.AccountTitle == int.Parse(ddl_AccountTitle.SelectedValue)).ToList();
                //新增科目
                if (_details.GetListItem().Where(p => p.AccountTitle == int.Parse(ddl_AccountTitle.SelectedValue)).Count() > 0)
                {
                    MessageBox.Show(this, "该科目已添加！");
                    return;
                }
            }
            item = new CM_ContractDetail();
            ViewState["MAXID"] = ((int)ViewState["MAXID"]) + 1;
            item.ID = (int)ViewState["MAXID"];
            item.AccountTitle = int.Parse(ddl_AccountTitle.SelectedValue);
            if (TreeTableBLL.GetChild("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", item.AccountTitle).Rows.Count > 0)
            {
                MessageBox.Show(this, "费用科目必须选择最底级会计科目!" + ddl_AccountTitle.SelectedItem.Text);
                return;
            }
        }
        else
        {//修改科目

            item = _details[ViewState["Selected"].ToString()];
        }
        CM_Contract _m = new CM_Contract();
        pl_detail.GetData(_m);

        item.BearMode = int.Parse(ddl_BearMode.SelectedValue);
        item.FeeCycle = GetContractcycle();
        item.Amount = decimal.Parse(tbx_Amount.Text);
        item.BearPercent = decimal.Parse(tbx_BearPercent.Text);
        item.ApplyLimit = decimal.Parse(tbx_ApplyLimit.Text);
        item["DiaplayCount"] = txt_count.Text.Trim();
        item["ISCA"] = ddl_YesNO.SelectedValue;
        if (item.Amount == 0 || item.BearPercent == 0 || item.ApplyLimit == 0)
        {
            MessageBox.Show(this, "对不起，合同总额(元),每次付款金额(元),占月目标销售额比不能为0！");
            return;
        }
        item.PayMode = int.Parse(ddl_PayMode.SelectedValue);
        item.Remark = tbx_Remark.Text;

        #region 获取已选择关联的品牌
        if (cbl_Brand.Visible)
        {
            if (cbl_Brand.SelectedIndex < 0)
            {
                MessageBox.Show(this, "对不起，请选择该费用关联的产品品牌！");
                return;
            }

            item["RelateBrand"] = "";
            foreach (ListItem i in cbl_Brand.Items)
            {
                if (i.Selected) item["RelateBrand"] += i.Value + ",";
            }
        }
        #endregion

        if (ViewState["Selected"] == null)
            _details.Add(item);
        else
            _details.Update(item);

        gv_Detail.SelectedIndex = -1;
        BindGrid();

        tbx_Amount.Text = "0";
        tbx_BearPercent.Text = "100";
        tbx_ApplyLimit.Text = "0";
        tbx_Remark.Text = "";
        bt_AddDetail.Text = "新增";
        ViewState["Selected"] = null;
    }

    #region 科目的明细及删除
    protected void gv_Detail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int title = (int)gv_Detail.DataKeys[e.RowIndex]["ID"];
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        _details.Remove(title.ToString());
        BindGrid();
    }
    #endregion

    #region 计算合同周期，月度金额，月度占比
    protected int GetContractcycle()
    {
        DateTime begindate = new DateTime();
        DateTime enddate = new DateTime();
        TextBox tbx_BeginDate = pl_detail.FindControl("CM_Contract_BeginDate") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_BeginDate");
        TextBox tbx_EndDate = pl_detail.FindControl("CM_Contract_EndDate") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_EndDate");
        if (tbx_BeginDate != null && tbx_EndDate != null)
        {
            DateTime.TryParse(tbx_BeginDate.Text, out begindate);
            DateTime.TryParse(tbx_EndDate.Text, out enddate);
        }
        int cycle = (int)Math.Round((enddate.Subtract(begindate).Days + 1) / 30m, 0);
        if (cycle <= 0)
            cycle = 1;
        return cycle;
    }

    protected decimal GetSalesPercent(decimal Amount)
    {
        TextBox tbx_SalesForcast = pl_detail.FindControl("CM_Contract_SalesForcast") != null ? (TextBox)pl_detail.FindControl("CM_Contract_SalesForcast") : null;

        if (tbx_SalesForcast != null)
        {
            decimal.TryParse(tbx_SalesForcast.Text, out SalesTarget);
            if (SalesTarget == 0) SalesTarget = 1m;
        }
        return Math.Round(Amount / GetContractcycle() / SalesTarget * 100, 2);
    }
    #endregion
    protected void gv_Detail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int accounttitle = int.Parse(gv_Detail.DataKeys[e.NewSelectedIndex]["AccountTitle"].ToString());
        int _ID = int.Parse(gv_Detail.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        ddl_AccountTitle.SelectedValue = accounttitle.ToString();
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        tbx_Amount.Text = _details[_ID.ToString()].Amount.ToString();
        ddl_PayMode.SelectedValue = _details[_ID.ToString()].PayMode.ToString();
        tbx_ApplyLimit.Text = _details[_ID.ToString()].ApplyLimit.ToString();
        tbx_BearPercent.Text = _details[_ID.ToString()].BearPercent.ToString();
        tbx_Remark.Text = _details[_ID.ToString()].Remark;

        #region 将已选择的关联品牌显示到控件上
        cbx_CheckAll.Checked = false;
        cbx_CheckAll_CheckedChanged(null, null);

        foreach (string brand in _details[accounttitle.ToString()]["RelateBrand"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            ListItem item = cbl_Brand.Items.FindByValue(brand);
            if (item != null) item.Selected = true;
        }
        #endregion

        ViewState["Selected"] = _ID.ToString();
        bt_AddDetail.Text = "修改";
    }
    protected void gv_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
            int accounttitle = int.Parse(gv_Detail.DataKeys[e.Row.RowIndex]["AccountTitle"].ToString());
            int _id = int.Parse(gv_Detail.DataKeys[e.Row.RowIndex]["ID"].ToString());
            Label lb_RelateBrand = (Label)e.Row.FindControl("lb_RelateBrand");
            if (lb_RelateBrand != null)
            {
                foreach (string brand in _details[_id.ToString()]["RelateBrand"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    PDT_Brand b = new PDT_BrandBLL(int.Parse(brand)).Model;
                    if (b != null) lb_RelateBrand.Text += b.Name + ",";
                }
            }
            int ContractFeeType = ConfigHelper.GetConfigInt("ContractFeeType");
            int ContractAccountTitle = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ContractAccountTitle"]);

            #region 判断是否NKA店，如是NKA店，则获取NKA的会计科目
            int client = 0;
            if (ViewState["ContractID"] == null && ViewState["ClientID"] == null) return;
            if (ViewState["ClientID"] != null)
            {
                client = (int)ViewState["ClientID"];
            }
            else
            {
                CM_Contract c = new CM_ContractBLL((int)ViewState["ContractID"]).Model;
                if (c != null) client = c.Client;
            }

            if (client > 0)
            {
                CM_Client c = new CM_ClientBLL(client).Model;
                if (c != null && c["RTChannel"] == "1" && ConfigHelper.GetConfigInt("ContractFeeType-KA") > 0)
                {
                    ContractFeeType = ConfigHelper.GetConfigInt("ContractFeeType-KA");
                    ContractAccountTitle = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ContractAccountTitle-KA"]);
                }
            }
            #endregion


        }
    }
    protected void tbx_Amount_TextChanged(object sender, EventArgs e)
    {
        if (ddl_PayMode.SelectedValue == "20")
            tbx_ApplyLimit.Text = tbx_Amount.Text;
        else
            tbx_ApplyLimit.Text = Math.Round(Convert.ToDecimal(tbx_Amount.Text) / GetContractcycle(), 2).ToString();
        lbl_SalesPercent.Text = GetSalesPercent(Convert.ToDecimal(tbx_Amount.Text)).ToString();
    }
    protected void tbx_ApplyLimit_TextChanged(object sender, EventArgs e)
    {
        int c = GetContractcycle();
        lbl_FeeCycle.Text = c.ToString();
        if (ddl_PayMode.SelectedValue == "20")
            tbx_Amount.Text = tbx_ApplyLimit.Text;
        else
            tbx_Amount.Text = Math.Round(Convert.ToDecimal(tbx_ApplyLimit.Text) * c, 2).ToString();
        lbl_SalesPercent.Text = GetSalesPercent(Convert.ToDecimal(tbx_Amount.Text)).ToString();
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
        if (_bll != null && _bll.Model.ApproveFlag != 1)
        {
            int Client = _bll.Model.Client;
            _bll.Delete();
            MessageBox.ShowAndRedirect(this, "删除成功！", "RetailerDetail.aspx?ClientID=" + Client.ToString());
        }
    }
    protected void bt_Disable_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void bt_FeeApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/FNA/FeeApply/FeeApply_Contract.aspx?ContractID=" + ViewState["ContractID"].ToString());
    }
    protected void bt_print_Click(object sender, EventArgs e)
    {
        if (ViewState["Classify"] != null && ViewState["Classify"].ToString() == "1")
            Response.Redirect("RetailerCLContract_Print.aspx?ContractID=" + ViewState["ContractID"].ToString());
        else if (ViewState["Classify"] != null && ViewState["Classify"].ToString() == "2")
            Response.Redirect("RetailerFLContract_Print.aspx?ContractID=" + ViewState["ContractID"].ToString());
        else
            MessageBox.Show(this, "无法打印!");
    }

}