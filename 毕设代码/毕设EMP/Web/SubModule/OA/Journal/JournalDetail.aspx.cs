// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.OA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.Model;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using System.Collections.Generic;
//using MCSFramework.BLL.CAT;

public partial class SubModule_OA_Journal_JournalDetail : System.Web.UI.Page
{
    private MCSSelectControl select_RelateClient;
    private DropDownList ddl_RelateLinkMan;
    private RadioButtonList rbl_HasSynergeticStaff;
    public DropDownList ddl_RelateActivity;
    private Const_IPLocation userAddress;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        #region 初始化页面控件
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));

        select_RelateClient = (MCSSelectControl)pl_detail.FindControl("JN_Journal_RelateClient");
        if (select_RelateClient != null)
            select_RelateClient.SelectChange += new SelectChangeEventHandler(select_RelateClient_SelectChange);

        ddl_RelateLinkMan = (DropDownList)pl_detail.FindControl("JN_Journal_RelateLinkMan");
        if (ddl_RelateLinkMan != null)
        {
            ddl_RelateLinkMan.DataTextField = "Name";
            ddl_RelateLinkMan.DataValueField = "ID";
        }

        rbl_HasSynergeticStaff = (RadioButtonList)pl_detail.FindControl("JN_Journal_HasSynergeticStaff");
        if (rbl_HasSynergeticStaff != null)
        {
            rbl_HasSynergeticStaff.AutoPostBack = true;
            rbl_HasSynergeticStaff.SelectedIndexChanged += new EventHandler(rbl_HasSynergeticStaff_SelectedIndexChanged);
        }

        ddl_RelateActivity = (DropDownList)pl_detail.FindControl("JN_Journal_RelateActivity");
        if (ddl_RelateActivity != null)
        {
            ddl_RelateActivity.DataTextField = "Topic";
            ddl_RelateActivity.DataValueField = "ID";
            ddl_RelateActivity.AutoPostBack = true;
            ddl_RelateActivity.SelectedIndexChanged += new EventHandler(ddl_RelateActivity_SelectedIndexChanged);
        }
        #endregion

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? Int32.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                if (rbl_HasSynergeticStaff != null) rbl_HasSynergeticStaff.SelectedValue = "2";    //默认非协同拜访
                BindData();
            }
            else
            {

                if (Request.QueryString["Day"] != null && Request.QueryString["Day"] != "0")
                    tbx_begindate.Text = DateTime.Today.AddDays(int.Parse(Request.QueryString["Day"]) - DateTime.Today.DayOfYear).ToString("yyyy-MM-dd");
                else
                    tbx_begindate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                #region 获取日志填报人信息
                Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
                select_Staff.SelectText = staff.Model.RealName;
                select_Staff.SelectValue = Session["UserID"].ToString();
                Org_PositionBLL position = new Org_PositionBLL(staff.Model.Position);
                lbl_Position.Text = position.Model.Name;

                MCSTreeControl tr_OfficialCity = pl_detail.FindControl("JN_Journal_OfficialCity") != null ? (MCSTreeControl)pl_detail.FindControl("JN_Journal_OfficialCity") : null;
                if (tr_OfficialCity != null) tr_OfficialCity.SelectValue = staff.Model.OfficialCity.ToString();
                #endregion

                #region 获取本机IP

                TextBox tbx_IPAddress = pl_detail.FindControl("JN_Journal_IPAddress") != null ? (TextBox)pl_detail.FindControl("JN_Journal_IPAddress") : null;
                if (tbx_IPAddress != null)
                {
                    tbx_IPAddress.Text = Request.UserHostAddress;

                    TextBox tbx_IPLocation = pl_detail.FindControl("JN_Journal_IPLocation") != null ? (TextBox)pl_detail.FindControl("JN_Journal_IPLocation") : null;
                    userAddress = Const_IPLocationBLL.FindByIP(Request.UserHostAddress);
                    if (tbx_IPLocation != null) tbx_IPLocation.Text = userAddress != null ? userAddress.Location : "";
                }
                #endregion

                #region 新增日志时，联系人字段不可编辑
                if (ddl_RelateLinkMan != null)
                {
                    ddl_RelateLinkMan.Items.Clear();
                    ddl_RelateLinkMan.Enabled = false;
                }
                #endregion

                pl_detail.SetPanelVisible("Panel_OA_JournalDetail_02", false);
                pl_detail.SetPanelVisible("Panel_OA_JournalDetail_03", false);
                pl_detail.SetPanelVisible("Panel_OA_JournalDetail_04", false);

                #region 默认无领导协同拜访
                if (rbl_HasSynergeticStaff != null)
                {
                    rbl_HasSynergeticStaff.SelectedValue = "2";    //默认非协同拜访
                    rbl_HasSynergeticStaff_SelectedIndexChanged(null, null);
                }
                #endregion

                bt_AddNewClient.Visible = false;
                bt_Delete.Visible = false;
                tbl_comment.Visible = false;
                UploadFile1.Visible = false;
                bt_ToEvectionRoute.Visible = false;
            }
        }
        #region 注册脚本
        string script = "function OpenClientInput(Journalid,OfferMan,Activityid){\r\n";
        script += "window.open('../../CSO/CSO_SampleOfferDetail.aspx?JournalID='+Journalid+'&OfferMan='+OfferMan+'&ActivityID='+Activityid);}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenClientInput", script, true);
        #endregion
    }


    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_JournalType.DataSource = DictionaryBLL.GetDicCollections("OA_JournalType");
        ddl_JournalType.DataBind();

        ddl_WorkingClassify.DataSource = DictionaryBLL.GetDicCollections("OA_WorkingClassify");
        ddl_WorkingClassify.DataBind();
        ddl_WorkingClassify.Items.Insert(0, new ListItem("请选择...", "0"));

        for (int i = 0; i < 24; i++)
        {
            ddl_BeginHour.Items.Add(new ListItem(i.ToString("d2"), i.ToString("d2")));
            ddl_EndHour.Items.Add(new ListItem(i.ToString("d2"), i.ToString("d2")));
        }
        ddl_BeginHour.SelectedValue = "08";
        ddl_EndHour.SelectedValue = "17";

        DropDownList ddl_OpponentBrand1 = (DropDownList)pl_detail.FindControl("JN_Journal_OpponentBrand1");
        ddl_OpponentBrand1.DataTextField = "Name";
        ddl_OpponentBrand1.DataValueField = "ID";
        ddl_OpponentBrand1.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=2");
        ddl_OpponentBrand1.DataBind();
        ddl_OpponentBrand1.Items.Insert(0, new ListItem("请选择...", "0"));

        DropDownList ddl_OpponentBrand2 = (DropDownList)pl_detail.FindControl("JN_Journal_OpponentBrand2");
        ddl_OpponentBrand2.DataTextField = "Name";
        ddl_OpponentBrand2.DataValueField = "ID";
        ddl_OpponentBrand2.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=2");
        ddl_OpponentBrand2.DataBind();
        ddl_OpponentBrand2.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    private void BindRelateActivity()
    {
        DateTime JournalDate = DateTime.Parse(tbx_begindate.Text);
        int OrganizeCity = new Org_StaffBLL((int)Session["UserID"], true).Model.OrganizeCity;

        string ConditionStr = "PlanBeginDate BETWEEN '" + JournalDate.AddDays(-15).ToString("yyyy-MM-dd") + "' AND '"
                    + JournalDate.AddDays(15).ToString("yyyy-MM-dd") + "' ";

        if (OrganizeCity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(OrganizeCity, true);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += OrganizeCity.ToString();

            if (orgcitys != "") ConditionStr += " AND OrganizeCity IN (" + orgcitys + ") ";
        }
        //if(ddl_RelateActivity!=null)
        //{
        //ddl_RelateActivity.DataSource = CAT_ActivityBLL.GetModelList(ConditionStr);
        //ddl_RelateActivity.DataBind();
        //ddl_RelateActivity.Items.Insert(0, new ListItem("请选择您参与举办的活动...", "0"));
        //ddl_RelateActivity_SelectedIndexChanged(null, null);
        //}
    }
    protected void ddl_JournalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_JournalType.SelectedValue == "1")
        {
            //日报
            ddl_BeginHour.Enabled = true;
            ddl_BeginMinute.Enabled = true;
            ddl_EndHour.Enabled = true;
            ddl_EndMinute.Enabled = true;
            ddl_WorkingClassify.Enabled = true;
        }
        else
        {
            ddl_BeginHour.Enabled = false;
            ddl_BeginMinute.Enabled = false;
            ddl_EndHour.Enabled = false;
            ddl_EndMinute.Enabled = false;
            ddl_WorkingClassify.Enabled = false;

            ddl_BeginHour.SelectedValue = "00";
            ddl_BeginMinute.SelectedValue = "00";
            ddl_EndHour.SelectedValue = "00";
            ddl_EndMinute.SelectedValue = "00";
            // ddl_WorkingClassify.SelectedValue = "0";
            ddl_WorkingClassify.SelectedValue = "11";
        }
    }
    protected void ddl_WorkingClassify_SelectedIndexChanged(object sender, EventArgs e)
    {
        pl_detail.SetPanelVisible("Panel_OA_JournalDetail_02", false);
        pl_detail.SetPanelVisible("Panel_OA_JournalDetail_03", false);
        pl_detail.SetPanelVisible("Panel_OA_JournalDetail_04", false);
        switch (ddl_WorkingClassify.SelectedValue)
        {
            case "1":  //客户拜访
                pl_detail.SetPanelVisible("Panel_OA_JournalDetail_02", true);
                break;
            case "2":  //协同拜访
                pl_detail.SetPanelVisible("Panel_OA_JournalDetail_03", true);
                break;
            case "3":  //活动举办  
                pl_detail.SetPanelVisible("Panel_OA_JournalDetail_04", true);
                BindRelateActivity();
                break;
            default:

                break;
        }
    }
    protected void select_RelateClient_SelectChange(object sender, SelectChangeEventArgs e)
    {
        if (!string.IsNullOrEmpty(select_RelateClient.SelectValue))
        {
            try
            {
                ddl_RelateLinkMan.DataSource = CM_LinkManBLL.GetModelList("ClientID=" + select_RelateClient.SelectValue +
                    " AND MCS_SYS.dbo.UF_Spilt2('MCS_CM.dbo.CM_LinkMan',ExtPropertys,'Dimission')!='2'");
                ddl_RelateLinkMan.DataBind();
                ddl_RelateLinkMan.Enabled = true;
                ddl_RelateLinkMan.Items.Insert(0, new ListItem("请选择", "0"));
            }
            catch { }
        }
    }
    protected void select_Staff_SelectChange(object sender, SelectChangeEventArgs e)
    {
        if (select_Staff.SelectValue != "")
        {
            Org_StaffBLL staff = new Org_StaffBLL(int.Parse(select_Staff.SelectValue), true);
            select_Staff.SelectText = staff.Model.RealName;
            select_Staff.SelectValue = staff.Model.ID.ToString();
            Org_PositionBLL position = new Org_PositionBLL(staff.Model.Position);
            lbl_Position.Text = position.Model.Name;
            MCSTreeControl tr_OfficialCity = (MCSTreeControl)pl_detail.FindControl("JN_Journal_OfficialCity");
            tr_OfficialCity.SelectValue = staff.Model.OfficialCity.ToString();
        }
        else
        {
            MessageBox.Show(this, "请正确选择日志填报人!");
            return;
        }
    }
    void rbl_HasSynergeticStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        MCSSelectControl select_SynergeticStaff = (MCSSelectControl)pl_detail.FindControl("JN_Journal_SynergeticStaff");
        if (select_SynergeticStaff != null && rbl_HasSynergeticStaff != null)
        {
            if (rbl_HasSynergeticStaff.SelectedValue == "1")
            {
                select_SynergeticStaff.Enabled = true;
            }
            else
            {
                select_SynergeticStaff.Enabled = false;
                select_SynergeticStaff.SelectText = "";
                select_SynergeticStaff.SelectValue = "";
            }
        }
    }
    #endregion

    private void BindData()
    {
        JN_Journal m = new JN_JournalBLL((int)ViewState["ID"]).Model;

        Org_StaffBLL staff = new Org_StaffBLL(m.Staff, true);
        select_Staff.SelectValue = m.Staff.ToString();
        select_Staff.SelectText = staff.Model.RealName;
        select_Staff.Enabled = false;
        Org_PositionBLL position = new Org_PositionBLL(staff.Model.Position);
        lbl_Position.Text = position.Model.Name;

        ddl_JournalType.SelectedValue = m.JournalType.ToString();
        ddl_JournalType_SelectedIndexChanged(null, null);
        ddl_JournalType.Enabled = false;

        tbx_begindate.Text = m.BeginTime.ToString("yyyy-MM-dd");
        tbx_enddate.Text = m.EndTime.ToString("yyyy-MM-dd");
        try
        {
            ddl_BeginHour.SelectedValue = m.BeginTime.Hour.ToString("d2");
            ddl_BeginMinute.SelectedValue = m.BeginTime.Minute.ToString("d2");
            ddl_EndHour.SelectedValue = m.EndTime.Hour.ToString("d2");
            ddl_EndMinute.SelectedValue = m.EndTime.Minute.ToString("d2");
        }
        catch { }

        ddl_WorkingClassify.SelectedValue = m.WorkingClassify.ToString();
        ddl_WorkingClassify_SelectedIndexChanged(null, null);
        ddl_WorkingClassify.Enabled = false;

        pl_detail.BindData(m);

        rbl_HasSynergeticStaff_SelectedIndexChanged(null, null);

        select_RelateClient_SelectChange(null, null);
        if (ddl_RelateLinkMan.Items.FindByValue(m.RelateLinkMan.ToString()) != null)
            ddl_RelateLinkMan.SelectedValue = m.RelateLinkMan.ToString();

        if (m.WorkingClassify == 1 && m.RelateLinkMan > 0)
        {
            //客户拜访
            bt_AddNewClient.OnClientClick = "javascript:OpenClientInput(" + m.ID.ToString() + "," + m.RelateLinkMan + ",0)";
            bt_AddNewClient.Enabled = true;
        }
        else if (m.WorkingClassify == 3 && !string.IsNullOrEmpty(m["RelateActivity"]))
        {
            //活动举办
            bt_AddNewClient.OnClientClick = "javascript:OpenClientInput(" + m.ID.ToString() + ",0," + m["RelateActivity"] + ")";
            bt_AddNewClient.Enabled = true;
        }
        else
        {
            bt_AddNewClient.Enabled = false;
        }
        #region 显示日志填报时的IP地址
        TextBox tbx_IPAddress = (TextBox)pl_detail.FindControl("JN_Journal_IPAddress");
        if (m["IPAddress"] != "" && m["IPLocation"] == "")
        {
            TextBox tbx_IPLocation = (TextBox)pl_detail.FindControl("JN_Journal_IPLocation");
            userAddress = Const_IPLocationBLL.FindByIP(Request.UserHostAddress);
            if (tbx_IPLocation != null) tbx_IPLocation.Text = userAddress != null ? userAddress.Location : "";
        }
        #endregion

        Label lbl_InsertStaff = (Label)pl_detail.FindControl("JN_Journal_InsertStaff");

        if (lbl_InsertStaff != null)
        {
            Org_StaffBLL _staff = new Org_StaffBLL(m.InsertStaff, true);
            Org_PositionBLL _position = new Org_PositionBLL(_staff.Model.Position);
            lbl_InsertStaff.Text = lbl_InsertStaff.Text + "(职位:" + _position.Model.Name + ")";
        }

        if (m.InsertStaff != (int)Session["UserID"] || m.ApproveFlag == 1 || (DateTime.Today - m.InsertTime.Date).Days > 7)
        {
            ddl_JournalType.Enabled = false;
            tbx_begindate.Enabled = false;
            ddl_WorkingClassify.Enabled = false;
            ddl_BeginHour.Enabled = false;
            ddl_BeginMinute.Enabled = false;
            ddl_EndHour.Enabled = false;
            ddl_EndMinute.Enabled = false;

            pl_detail.SetControlsEnable(false);
            TextBox tbx_remark = pl_detail.FindControl("JN_Journal_Remark") != null ?
                (TextBox)pl_detail.FindControl("JN_Journal_Remark") : null;
            if (tbx_remark != null)
            {
                tbx_remark.Enabled = true;
                tbx_remark.ReadOnly = true;
            }
            UploadFile1.CanDelete = false;
            UploadFile1.CanUpload = false;
            bt_OK.Visible = false;
            bt_Delete.Visible = false;
        }

        if ((DateTime.Today - m.InsertTime.Date).Days > 0) bt_Delete.Visible = false;       //只能删除当日填写的日志

        #region 展示附件
        UploadFile1.RelateID = (int)ViewState["ID"];
        UploadFile1.RelateType = 90;
        UploadFile1.BindGrid();
        #endregion

        int commentcounts = JN_JournalCommentBLL.GetModelList("JournalID=" + ViewState["ID"].ToString()).Count;
        lb_CommentCounts.Text = commentcounts.ToString();
        if (commentcounts > 0)
        {
            table_comment.Visible = true;
            BindGridList();
            btn_LookComment.Visible = false;
        }
    }

    #region
    protected string FormatTxt(string content)
    {
        return (content.Replace(((char)13).ToString(), "<br>"));
    }
    #endregion

    #region 评论操作
    #region 显示评论内容
    protected void btn_LookComment_Click(object sender, EventArgs e)
    {
        table_comment.Visible = true;
        BindGridList();
    }
    #endregion

    #region 到数据库中查出评论内容
    private void BindGridList()
    {
        dgshow.DataSource = JN_JournalCommentBLL.GetModelList("JournalID=" + ViewState["ID"].ToString());
        dgshow.DataBind();
    }
    #endregion

    #region 取消发表评论的内容
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        content.Text = "";
    }
    #endregion

    #region 向数据库中添加一条新的评论内容
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        JN_JournalCommentBLL commentbll = new JN_JournalCommentBLL();
        commentbll.Model.JournalID = Convert.ToInt32(ViewState["ID"]);
        commentbll.Model.Staff = int.Parse(Session["UserID"].ToString());
        commentbll.Model.Content = content.Text;
        commentbll.Model.CommentTime = DateTime.Now;
        commentbll.Add();

        BindGridList();

        //清空评论板内的内容
        content.Text = "";
    }
    #endregion

    protected void dgshow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgshow.PageIndex = e.NewPageIndex;
        BindGridList();
    }

    #endregion

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if ((DateTime.Today - Convert.ToDateTime(tbx_begindate.Text)).Days > 7)
        {
            MessageBox.Show(this, "不能填写一周之前的日志!");
            return;
        }
        JN_JournalBLL _bll = null;
        if ((int)ViewState["ID"] == 0)
        {
            _bll = new JN_JournalBLL();
        }
        else
        {
            _bll = new JN_JournalBLL((int)ViewState["ID"]);
        }

        #region 绑定界面控件
        _bll.Model.Staff = int.Parse(select_Staff.SelectValue);
        _bll.Model.OrganizeCity = new Org_StaffBLL(int.Parse(select_Staff.SelectValue)).Model.OrganizeCity;
        _bll.Model.BeginTime = DateTime.Parse(tbx_begindate.Text + " " + ddl_BeginHour.SelectedValue + ":" + ddl_BeginMinute.SelectedValue);
        _bll.Model.EndTime = DateTime.Parse(tbx_begindate.Text + " " + ddl_EndHour.SelectedValue + ":" + ddl_EndMinute.SelectedValue);
        _bll.Model.JournalType = int.Parse(ddl_JournalType.SelectedValue);
        _bll.Model.WorkingClassify = int.Parse(ddl_WorkingClassify.SelectedValue);

        pl_detail.GetData(_bll.Model);

        #endregion

        #region 判断必填项
        if (_bll.Model.JournalType == 0)
        {
            MessageBox.Show(this, "日志类型必填!");
            return;
        }
        if (_bll.Model.WorkingClassify == 0)
        {
            MessageBox.Show(this, "工作类别必填!");
            return;
        }
        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "所属的管理片区必填!");
            return;
        }
        //if ((_bll.Model.WorkingClassify == 1 || _bll.Model.WorkingClassify == 7) && (_bll.Model["StartPoint"] == "" || _bll.Model["EndPoint"] == ""))
        //{
        //    MessageBox.Show(this, "【出差始发地】、【出差目的地】必填!");
        //    return;
        //}
        #endregion

        if ((int)ViewState["ID"] == 0)
        {
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }

        //   MessageBox.ShowAndClose(this, "日志保存成功！");
        Response.Redirect("JournalDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            JN_JournalBLL bll = new JN_JournalBLL((int)ViewState["ID"]);
            bll.Delete();
            MessageBox.ShowAndClose(this, "日志删除成功!");
        }
    }

    protected void bt_ToEvectionRoute_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            JN_Journal j = new JN_JournalBLL((int)ViewState["ID"]).Model;

            IList<FNA_EvectionRoute> evections = FNA_EvectionRouteBLL.GetModelList("RelateStaff=" + j.Staff +
              " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_EvectionRoute',ExtPropertys,'RelateJournal')='"
              + j.ID.ToString() + "'");
            if (evections.Count > 0)
            {
                //系统中已有关联于该工作日志的差旅行程
                Response.Redirect("~/SubModule/FNA/FeeWriteOff/Pop_EvectionRouteDetail.aspx?ID=" + evections[0].ID.ToString());
            }
            else
            {
                if (j.InsertStaff == (int)Session["UserID"])
                {
                    FNA_EvectionRouteBLL evectionbll = new FNA_EvectionRouteBLL();
                    evectionbll.Model.RelateStaff = j.Staff;
                    evectionbll.Model.BeginDate = j.BeginTime;
                    evectionbll.Model.EndDate = j.EndTime;
                    evectionbll.Model.EvectionLine = j["StartPoint"] + "-" + j["EndPoint"];
                    evectionbll.Model["HotelName"] = j["HotelName"];
                    evectionbll.Model["HotelTele"] = j["HotelPhone"];
                    evectionbll.Model["RelateJournal"] = j.ID.ToString();

                    evectionbll.Model.InsertStaff = j.InsertStaff;
                    evectionbll.Model.ApproveFlag = 2;
                    int id = evectionbll.Add();

                    Response.Redirect("~/SubModule/FNA/FeeWriteOff/Pop_EvectionRouteDetail.aspx?ID=" + id.ToString());
                }
                else
                {
                    MessageBox.Show(this, "对不起，只有日志填报人才可填报该日志的差旅行程信息!");
                    return;
                }
            }
        }
    }

    protected void ddl_RelateActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_RelateActivity.SelectedValue != "0" && ViewState["ID"] != null)
        {
            bt_AddNewClient.OnClientClick = "javascript:OpenClientInput(" + ViewState["ID"].ToString() + "," + ddl_RelateActivity.SelectedValue + ")";
            bt_AddNewClient.Enabled = true;
        }
    }

}
