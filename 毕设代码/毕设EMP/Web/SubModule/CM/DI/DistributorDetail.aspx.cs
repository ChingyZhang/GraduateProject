// ===================================================================
// 文件路径:CM/Distributor/DistributorDetail.aspx.cs 
// 生成日期:2008-12-19 10:11:21 
// 作者:	  yangwei
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.IFStrategy;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using System.Collections.Specialized;
using MCSFramework.Model;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;

public partial class CM_Distributor_DistributorDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
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
            #endregion

            if (ViewState["ClientID"] != null)
            {
                BindData();
            }
            else if (Request.QueryString["Mode"] == "New")
            {
                #region 新增客户时的初始值
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

                DropDownList ddl_ChiefLinkMan = (DropDownList)pl_detail.FindControl("CM_Client_ChiefLinkMan");
                if (ddl_ChiefLinkMan != null)
                {
                    ddl_ChiefLinkMan.Items.Clear();
                    ddl_ChiefLinkMan.Enabled = false;
                }

                DropDownList ddl_DIClassify = pl_detail.FindControl("CM_Client_DIClassify") == null ? null : (DropDownList)pl_detail.FindControl("CM_Client_DIClassify");
                if (ddl_DIClassify != null)
                {
                    ddl_DIClassify.SelectedValue = "2";
                    ddl_DIClassify.Enabled = false;

                }
                #endregion

                bt_Add.Enabled = false;
                bt_Approve.Visible = false;
                tbl_LinkMan.Visible = false;
                bt_Analysis.Visible = false;
                bt_AddApply.Visible = false;
                bt_RevocationApply.Visible = false;
                bt_Record.Visible = false;
                bt_ReplaceClientManager.Visible = false;
                bt_ReplaceSupplier.Visible = false;
                bt_DIACUpgrade.Visible = false;
                bt_ViewSubClient.Visible = false;
                bt_DIUP.Visible = false;
                #endregion
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }

        #region 给活跃标志加事件
        DropDownList ddl_ActiveFlag_1 = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        ddl_ActiveFlag_1.AutoPostBack = true;
        ddl_ActiveFlag_1.SelectedIndexChanged += new EventHandler(ddl_ActiveFlag_SelectedIndexChanged);


        #endregion

        #region 选择经销商级别 一级则弹出仓库，二级则弹出经销商，并指定所属片区
        MCSSelectControl select_Supplier = (MCSSelectControl)pl_detail.FindControl("CM_Client_Supplier");
        //select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ExtCondition=ClientType in (1,2)";
        MCSTreeControl select_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OrganizeCity");

        DropDownList select_DIClassify = (DropDownList)pl_detail.FindControl("CM_Client_DIClassify");
        select_DIClassify.AutoPostBack = true;
        int type = 0;
        type = select_DIClassify.SelectedIndex;
        switch (select_DIClassify.SelectedValue)
        {
            case "1":
                select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=1&OrganizeCity=" + select_OrganizeCity.SelectValue;     //只可以选择仓库
                break;
            case "2":
            case "3":
                select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"&OrganizeCity=" + select_OrganizeCity.SelectValue;
                break;
            default:
                select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType in (1,2)&OrganizeCity=" + select_OrganizeCity.SelectValue;  //可以选择仓库及经销商
                break;
        }
        #endregion

        #region 给判断重复编码加事件
        TextBox tbx_Code = (TextBox)pl_detail.FindControl("CM_Client_Code");
        tbx_Code.AutoPostBack = true;
        tbx_Code.TextChanged += new EventHandler(tbx_Code_TextChanged);
        #endregion
        #region 给账号/开户行文本框添加事件
        TextBox tbx_BankName = (TextBox)pl_detail.FindControl("CM_Client_BankName");
        tbx_BankName.AutoPostBack = true;
        tbx_BankName.TextChanged += new EventHandler(tbx_Bank_TextChanged);
        TextBox tbx_BankAccountNo = (TextBox)pl_detail.FindControl("CM_Client_BankAccountNo");
        tbx_BankAccountNo.AutoPostBack = true;
        tbx_BankAccountNo.TextChanged += new EventHandler(tbx_Bank_TextChanged);
        #endregion


        #region 注册弹出窗口脚本
        string script = "function PopReplaceSupplier(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../ReplaceSupplier.aspx") +
            "?Supplier=' + id + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReplaceSupplier", script, true);

        script = "function PopReplaceSupplier2(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../ReplaceSupplier.aspx") +
            "?Supplier2=' + id + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReplaceSupplier2", script, true);

        script = "function PopReplaceClientManager(id,clienttype){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../ReplaceClientManager.aspx") +
            "?ClientManager=' + id + '&ClientType='+clienttype+'&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReplaceClientManager", script, true);
        #endregion

    }
    #region 绑定下拉列表框

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

    void tbx_Bank_TextChanged(object sender, EventArgs e)
    {
        TextBox tbx_BankAccountNo = (TextBox)pl_detail.FindControl("CM_Client_BankAccountNo");
        TextBox tbx_BankName = (TextBox)pl_detail.FindControl("CM_Client_BankName");
        if (!tbx_BankName.Text.Trim().Contains("农业银行") && !tbx_BankName.Text.Trim().Contains("建设银行")
            && !tbx_BankName.Text.Trim().Contains("农行") && !tbx_BankName.Text.Trim().Contains("建行"))
            return;
        else
        {
            if (tbx_BankName.Text.Trim().Contains("农业银行") || tbx_BankName.Text.Trim().Contains("农行"))
            {
                if (!tbx_BankAccountNo.Text.Trim().StartsWith("6228") && !tbx_BankAccountNo.Text.Trim().StartsWith("9559"))
                    MessageBox.Show(this, "所填账号非农行卡号，请检查!");
            }
            else if (tbx_BankName.Text.Trim().Contains("建设银行") || tbx_BankName.Text.Trim().Contains("建行"))
                if (tbx_BankAccountNo.Text.Trim().StartsWith("6228") && tbx_BankAccountNo.Text.Trim().StartsWith("9559"))
                    MessageBox.Show(this, "所填账号非建行卡号，请检查！");
        }
    }
    #endregion

    private void BindData()
    {
        CM_Client m = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        switch (m.ClientType)
        {
            case 1:
                Response.Redirect("../Store/StoreDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            case 2:
                break;
            case 3:
                Response.Redirect("../RT/RetailerDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            default:
                MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                break;
        }
        pl_detail.BindData(m);




        MCSSelectControl select_ClientManager = (MCSSelectControl)pl_detail.FindControl("CM_Client_ClientManager");
        select_ClientManager.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + m.OrganizeCity;

        if (m.ApproveFlag == 1)
        {
            //已审核
            TextBox tbx_OpenTime = (TextBox)pl_detail.FindControl("CM_Client_OpenTime");
            if (tbx_OpenTime != null && tbx_OpenTime.Text != "") tbx_OpenTime.Enabled = false;

            if (m.ActiveFlag == 1)
            {
                bt_AddApply.Visible = false;
                bt_DIUP.Visible = false;
            }
            else
            {
                TextBox tbx_CloseTime = (TextBox)pl_detail.FindControl("CM_Client_CloseTime");
                if (tbx_CloseTime != null && tbx_CloseTime.Text != "") tbx_CloseTime.Enabled = false;
                bt_RevocationApply.Visible = false;
            }
            bt_Approve.Visible = false;
        }

        if (m.ActiveFlag == 1)
        {
            Header.Attributes["WebPageSubCode"] = "Modify";
            bt_AddApply.Visible = false;
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
                bt_DIACUpgrade.Visible = false;
                bt_DIUP.Visible = false;
            }
        }

        #region 绑定该客户的首要联系人
        DropDownList ddl_ChiefLinkMan = (DropDownList)pl_detail.FindControl("CM_Client_ChiefLinkMan");
        try
        {
            ddl_ChiefLinkMan.DataTextField = "Name";
            ddl_ChiefLinkMan.DataValueField = "ID";
            ddl_ChiefLinkMan.DataSource = CM_LinkManBLL.GetModelList("ClientID=" + ViewState["ClientID"].ToString());
            ddl_ChiefLinkMan.DataBind();
        }
        catch { }

        ddl_ChiefLinkMan.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_ChiefLinkMan.SelectedValue = m.ChiefLinkMan.ToString();

        #endregion

        BindGrid();
        bt_Add.Enabled = true;

        if (m.ClientType != 2 || m["DIClassify"] != "3")
            bt_DIACUpgrade.Visible = false;
        if (m.ClientType != 2 || m["DIClassify"] == "2")
            bt_DIUP.Visible = false;

        bt_ReplaceSupplier.OnClientClick = "javascript:PopReplaceSupplier(" + m.ID.ToString() + ")";
        bt_ReplaceSupplier2.OnClientClick = "javascript:PopReplaceSupplier2(" + m.ID.ToString() + ")";
        bt_ReplaceClientManager.OnClientClick = "javascript:PopReplaceClientManager(" + m.ClientManager.ToString() + "," + m.ClientType.ToString() + ")";
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        string ContractProves = "";
        if (ViewState["ClientID"] == null)
        {
            _bll = new CM_ClientBLL();
        }
        else
        {
            _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        }

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
        if (_bll.Model.ClientManager == 0)
        {
            MessageBox.Show(this, "销售代表必填!");
            return;
        }
        if (_bll.Model["RTChannel"] == "0")
        {
            MessageBox.Show(this, "主营渠道必填!");
            return;
        }
        if (_bll.Model["DIClassify"] == "0")
        {
            MessageBox.Show(this, "经销商分类必填!");
            return;
        }
        if (_bll.Model["OperateProperty"] == "0")
        {
            MessageBox.Show(this, "归属办事处类型必填!");
            return;
        }
        #endregion

        #region 判断活跃标志
        if (_bll.Model.ActiveFlag == 1 && _bll.Model.CloseTime != new DateTime(1900, 1, 1))
            _bll.Model.CloseTime = new DateTime(1900, 1, 1);

        if (_bll.Model.ActiveFlag == 2 && _bll.Model.CloseTime == new DateTime(1900, 1, 1))
            _bll.Model.CloseTime = DateTime.Now;
        #endregion


       

        if (ViewState["ClientID"] == null)
        {
            _bll.Model.ClientType = 2;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ClientID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Model.Supplier == _bll.Model.ID)
            {
                MessageBox.Show(this, "不能将经销商自己作为供货商!");
            }
            _bll.Update();
        }
        if (ContractProves.EndsWith("，"))
        {
            MessageBox.ShowAndRedirect(this, "该经销商必须上传以下附件，" + ContractProves.Substring(0, ContractProves.Length - 1), "../ClientPictureList.aspx?ClientID=" + ViewState["ClientID"].ToString());
            return;
        }
        if (sender != null)
            MessageBox.ShowAndRedirect(this, "保存经销商资料成功！", "DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());

    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            _bll.Model.ActiveFlag = 1;
            if (_bll.Model.OpenTime.Year == 1900) _bll.Model.OpenTime = DateTime.Today;
            _bll.Model.CloseTime = new DateTime(1900, 1, 1);
            _bll.Model.ApproveFlag = 1;
            _bll.Update();
            MessageBox.ShowAndRedirect(this, "审核经销商资料成功！", "DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }


    private void BindGrid()
    {
        if (ViewState["ClientID"] != null)
        {
            string ConditionStr = "MCS_CM.dbo.CM_LinkMan.ClientID = " + ViewState["ClientID"].ToString();
            gv_List.ConditionString = ConditionStr;
            gv_List.BindGrid();
            bt_Add.Enabled = true;
        }
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("../LM/LinkManDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&URL=~/SubModule/CM/DI/DistributorDetail.aspx");
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
        CM_ClientBLL _bll = new CM_ClientBLL();
        string code = ((TextBox)pl_detail.FindControl("CM_Client_Code")).Text;
        if (ViewState["ClientID"] != null && code != "")
        {
            if (_bll._GetModelList(" Code = '" + code + "' and ID<>" + ViewState["ClientID"].ToString()).Count > 0)
                return false;

        }
        else
        {
            if (_bll._GetModelList(" Code = '" + code + "'").Count > 0)
                return false;
        }
        return true;
    }

    protected void bt_Analysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistributorAnalysis.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    protected void bt_AddApply_Click(object sender, EventArgs e)
    {
        bt_OK_Click(null, null);
        if ((int)ViewState["ClientID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }
 
        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ClientID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("ClientName", bll.Model.FullName.ToString());
        dataobjects.Add("OperateClassify", bll.Model["OperateClassify"]);
        dataobjects.Add("DIClassify", bll.Model["DIClassify"]);


        int TaskID = EWF_TaskBLL.NewTask("Add_Distributor", (int)Session["UserID"], "新增经销商流程,经销商名称：" + bll.Model.FullName, "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString(), dataobjects);
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
        #region 判断有无下游客户
        if (CM_ClientBLL.GetModelList("Supplier=" + ViewState["ClientID"].ToString() + " AND ActiveFlag=1").Count > 0)
        {
            MessageBox.Show(this, "对不起，该经销商下还有活跃的下游分销商或门店,无法申请撤销!");
            return;
        }
        #endregion
        if (new FNA_FeeApplyBLL().GetDetail(@" Flag IN(1,2,4) AND AvailCost>0 AND EXISTS 
            (SELECT ID FROM MCS_FNA.dbo.FNA_FeeApply WHERE State=3 AND ApproveFlag=1 AND ID=FNA_FeeApplyDetail.ApplyID
            AND Client=" + ViewState["ClientID"] + ")").Count > 0)
        {
            MessageBox.Show(this, "该经销商下还有费用未做核销，请核销后再撤销！");
            return;
        }

        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ClientID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("ClientName", bll.Model.FullName.ToString());
        dataobjects.Add("CloseTime", DateTime.Now.ToShortDateString());
        dataobjects.Add("Remark", "经销商终止合作");
        dataobjects.Add("OperateClassify", bll.Model["OperateClassify"]);
        dataobjects.Add("DIClassify", bll.Model["DIClassify"]);

        string Title = TreeTableBLL.GetSuperNameByLevel("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name", "SuperID", bll.Model.OrganizeCity, ConfigHelper.GetConfigInt("OrganizeCity-CityLevel")) + "-" + "经销商中止合作流程";//办事处+经分销商名称+中止流程
        int TaskID = EWF_TaskBLL.NewTask("Revocation_Distributor", (int)Session["UserID"], Title, "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }
    protected void bt_ReplaceSupplier_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void bt_DIACUpgrade_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] > 0)
        {
            int ret = CM_ClientBLL.DISubACUpgrade((int)ViewState["ClientID"]);
            switch (ret)
            {
                case 0:
                    MessageBox.Show(this, "子户头升级成功，原主户头自动降级子户头！");
                    CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
                    Response.Redirect("DistributorDetail.aspx?ClientID=" + _bll.Model.Supplier.ToString());
                    break;
                case -1:
                    MessageBox.Show(this, "必须在子户头上进行升级!");
                    break;
                case -2:
                    MessageBox.Show(this, "该子户头的供货商(即要降级的主户头)，必须为经销商主户头!");
                    break;
                default:
                    MessageBox.Show(this, "子户头升级失败!错误码：" + ret.ToString());
                    break;
            }
        }
    }

    protected void bt_ReplaceSupplier2_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void bt_ViewSubClient_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubClientList.aspx");
    }
    protected void bt_DIUP_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }
        bt_OK_Click(null, null);

        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ClientID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("ClientName", bll.Model.FullName.ToString());
        dataobjects.Add("OperateClassify", bll.Model["OperateClassify"]);
        dataobjects.Add("DIClassify", bll.Model["DIClassify"]);


        int TaskID = EWF_TaskBLL.NewTask("Distributor_Upgrade", (int)Session["UserID"], "分经销商转经销商,经销商名称：" + bll.Model.FullName, "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
            //new EWF_TaskBLL(TaskID).Start();        //直接启动流程
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }
}
