// ===================================================================
// 文件路径:SubModule/CAT/CAT_ActivityDetail.aspx.cs 
// 生成日期:2009/12/23 21:28:57 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CAT;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.QNA;
using MCSFramework.Common;
using MCSFramework.Model.CAT;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class SubModule_CAT_CAT_ActivityDetail : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        #region 获取举办医院控件
        MCSSelectControl select_StageClient = (MCSSelectControl)pl_detail.FindControl("CAT_Activity_StageClient");
        if (select_StageClient != null)
        {
            select_StageClient.SelectChange += new SelectChangeEventHandler(select_StageClient_SelectChange);
        }
        DropDownList ddl_classify = pl_detail.FindControl("CAT_Activity_Classify") != null ? (DropDownList)pl_detail.FindControl("CAT_Activity_Classify") : null;
        if (ddl_classify != null)
        {

        }
        #endregion

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 0;
            #endregion

            BindDropDown();

            ViewState["FeeListDetails"] = CAT_FeeApplyDetailBLL.GetModelList("Activity=" + ViewState["ID"].ToString());
            ViewState["GiftListDetails"] = CAT_GiftApplyDetailBLL.GetModelList("Activity=" + ViewState["ID"].ToString());
            ViewState["SalesListDetails"] = CAT_SalesVolumeDetailBLL.GetModelList("Activity=" + ViewState["ID"].ToString());
            ViewState["EditAdjust"] = false;
            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
                BindGrid();
            }
            else
            {
                //新增
                Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
                ((MCSTreeControl)pl_detail.FindControl("CAT_Activity_Officialcity")).SelectValue = staff.Model.OfficialCity.ToString();
                ((MCSTreeControl)pl_detail.FindControl("CAT_Activity_OrganizeCity")).SelectValue = staff.Model.OrganizeCity.ToString();

                string state = Request.QueryString["State"] != null ? Request.QueryString["State"] : "11";    //默认为排期中的活动
                ((DropDownList)pl_detail.FindControl("CAT_Activity_State")).SelectedValue = state;

                bt_Stage.Visible = false;

                bt_Submit.Visible = false;
                bt_Approve.Visible = false;
                bt_Complete.Visible = false;
                bt_Cancel.Visible = false;
                UploadFile001.Visible = false;
                gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 2].Visible = false;//调整按钮
                gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 3].Visible = false;//批复金额
                gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 4].Visible = false;//扣减额原因
                gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 5].Visible = false;//扣减额
                gv_GiftListDetail.Columns[2].Visible = false;//调整数量 
                gv_GiftListDetail.Columns[3].Visible = false;//使用数量 
                gv_GiftListDetail.Columns[4].Visible = false;//剩余数量 
                pl_detail.SetPanelVisible("Panel_CAT_ActivityDetail_02", false);
            }

        }

        if (ViewState["ApproveFlag"] == null || (int)ViewState["ApproveFlag"] != 1)
        {
            MCSTabControl1.Items[2].Visible = false;
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        DropDownList ddl_QNAProject = (DropDownList)pl_detail.FindControl("CAT_Activity_RelateQuestionnaire");
        if (ddl_QNAProject != null)
        {
            ddl_QNAProject.DataSource = QNA_ProjectBLL.GetModelList("Classify = 3 AND Enabled='Y'");
            ddl_QNAProject.DataTextField = "Name";
            ddl_QNAProject.DataValueField = "ID";
            ddl_QNAProject.DataBind();
            ddl_QNAProject.Items.Insert(0, new ListItem("请选择...", "0"));
        }
        IList<AC_AccountTitle> lists;
        if (ConfigHelper.GetConfigInt("ActivityFeeAccountTitle2") == 0)
            lists = AC_AccountTitleBLL.GetListByFeeType(ConfigHelper.GetConfigInt("HDM-CATFeeType"));
        else
            lists = AC_AccountTitleBLL.GetModelList("(ID = 1 OR SuperID=" + ConfigHelper.GetConfigString("ActivityFeeAccountTitle2") + ") AND MCS_SYS.dbo.UF_Spilt2('MCS_Pub.dbo.AC_AccountTitle',ExtPropertys,'IsDisable')<>'Y'");
        ddl_AccountTitle.DataSource = lists;
        ddl_AccountTitle.DataBind();

        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=1");
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("请选择...", "0"));
    }

    protected void select_StageClient_SelectChange(object sender, SelectChangeEventArgs e)
    {
        MCSSelectControl select_StageClient = (MCSSelectControl)pl_detail.FindControl("CAT_Activity_StageClient");
        int client = 0;
        if (int.TryParse(select_StageClient.SelectValue, out client) && client > 0)
        {
            CM_ClientBLL _c = new CM_ClientBLL(client);
            TextBox tbx_Address = (TextBox)pl_detail.FindControl("CAT_Activity_Address");
            if (tbx_Address != null && _c.Model != null) tbx_Address.Text = _c.Model.Address;
        }
    }

    #endregion

    private void BindData()
    {
        CAT_Activity m = new CAT_ActivityBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            lb_ActivityID.Text = "【" + m.ID.ToString() + "】";
            pl_detail.BindData(m);
            UploadFile001.RelateID = m.ID;
            ViewState["ApproveFlag"] = m.ApproveFlag;

            #region 根据活动状态设定界面控件可见属性
            switch (m.State)
            {
                case 1:     //筹备中 ApproveFlag=1
                    bt_Approve.Visible = false;
                    bt_AddFeeApply.Visible = false;
                    bt_OK.Visible = false;
                    bt_Submit.Visible = false;
                    bt_Stage.Visible = false;
                    if (m.PlanBeginDate != null)
                    {
                        TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                        TimeSpan ts2 = new TimeSpan(m.PlanBeginDate.Ticks);
                        TimeSpan ts = ts1.Subtract(ts2).Duration();
                        // if (ts.TotalHours > 121) bt_Cancel.Enabled = false;
                    }
                    tb_giftAdd.Visible = false;
                    tr_FeeApply.Visible = false;
                    tb_ActivitySales.Visible = true;
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 1].Visible = false;  //删除按钮
                    gv_GiftListDetail.Columns[gv_GiftListDetail.Columns.Count - 1].Visible = false;  //删除按钮
                    gv_GiftListDetail.Columns[gv_GiftListDetail.Columns.Count - 2].Visible = true;//调整按钮
                    gv_FeeListDetail.SetControlsEnable(false);
                    gv_GiftListDetail.SetControlsEnable(false);
                    pl_detail.SetPanelEnable("Panel_CAT_ActivityDetail_01", false);
                    TextBox tbx_ActualPeople = (TextBox)pl_detail.FindControl("CAT_Activity_ActiveJoinClientNumber");
                    break;
                case 2:     //已举办 ApproveFlag=1
                    bt_Approve.Visible = false;
                    bt_AddFeeApply.Visible = false;
                    bt_Cancel.Visible = false;
                    bt_OK.Visible = false;
                    bt_Submit.Visible = false;
                    bt_Complete.Visible = false;
                    bt_Stage.Visible = false;
                    UploadFile001.CanUpload = false;
                    UploadFile001.CanDelete = false;
                    td_AddSales.Visible = false;
                    tb_giftAdd.Visible = false;
                    tr_FeeApply.Visible = false;
                    tb_ActivitySales.Visible = true;
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 1].Visible = false;  //删除按钮
                    gv_GiftListDetail.Columns[gv_GiftListDetail.Columns.Count - 1].Visible = false;  //删除按钮
                    gv_SalesList.Columns[gv_SalesList.Columns.Count - 1].Visible = false;//删除按钮
                    pl_detail.SetPanelEnable("Panel_CAT_ActivityDetail_01", false);
                    pl_detail.SetPanelEnable("Panel_CAT_ActivityDetail_02", false);
                    gv_FeeListDetail.SetControlsEnable(false);
                    break;
                case 3:     //取消举办  ApproveFlag=1 OR  ApproveFlag=2
                    bt_Approve.Visible = false;
                    bt_AddFeeApply.Visible = false;
                    bt_Cancel.Visible = false;
                    bt_OK.Visible = false;
                    bt_Submit.Visible = false;
                    bt_Complete.Visible = false;
                    bt_Stage.Visible = false;
                    UploadFile001.CanUpload = false;
                    UploadFile001.CanDelete = false;
                    pl_detail.SetPanelEnable("Panel_CAT_ActivityDetail_01", false);
                    pl_detail.SetPanelEnable("Panel_CAT_ActivityDetail_02", false);
                    break;
                case 4:     //排期中 ApproveFlag=2
                    bt_Approve.Visible = false;
                    bt_AddFeeApply.Visible = false;
                    tr_FeeApply.Visible = false;
                    bt_Cancel.Visible = false;
                    bt_Submit.Visible = false;
                    bt_Complete.Visible = false;
                    pl_detail.SetPanelVisible("Panel_CAT_ActivityDetail_02", false);
                    break;
                case 11:   //未提交审批(草稿) ApproveFlag=2
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 2].Visible = false;//调整按钮
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 3].Visible = false;//批复金额
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 4].Visible = false;//扣减额原因
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 5].Visible = false;//扣减额
                    gv_GiftListDetail.Columns[2].Visible = false;//调整数量 
                    gv_GiftListDetail.Columns[3].Visible = false;//使用数量 
                    gv_GiftListDetail.Columns[4].Visible = false;//剩余数量 
                    bt_Cancel.Visible = false;
                    bt_Stage.Visible = false;
                    tb_giftAdd.Visible = false;
                    tr_FeeApply.Visible = false;
                    bt_Complete.Visible = false;
                    pl_detail.SetPanelVisible("Panel_CAT_ActivityDetail_02", false);
                    break;
                case 13:   //审批不通过                   
                    bt_Cancel.Visible = false;
                    bt_Stage.Visible = false;
                    bt_Complete.Visible = false;
                    pl_detail.SetPanelVisible("Panel_CAT_ActivityDetail_02", false);
                    break;
                case 12:   //提交审批中 ApproveFlag=2
                    bt_Approve.Visible = false;
                    bt_AddFeeApply.Visible = false;
                    bt_Cancel.Visible = false;
                    bt_OK.Visible = false;
                    bt_Submit.Visible = false;
                    bt_Stage.Visible = false;
                    bt_Complete.Visible = false;
                    tb_giftAdd.Visible = false;
                    tr_FeeApply.Visible = false;
                    gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 1].Visible = false;  //删除按钮
                    gv_GiftListDetail.Columns[gv_GiftListDetail.Columns.Count - 1].Visible = false;  //删除按钮
                    gv_GiftListDetail.Columns[3].Visible = true;//赠品调整数量 
                    pl_detail.SetPanelEnable("Panel_CAT_ActivityDetail_01", false);
                    pl_detail.SetPanelVisible("Panel_CAT_ActivityDetail_02", false);
                    if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
                    {
                        gv_FeeListDetail.Columns[gv_FeeListDetail.Columns.Count - 2].Visible = true;//调整按钮
                        gv_GiftListDetail.Columns[gv_GiftListDetail.Columns.Count - 2].Visible = true;//调整按钮
                    }
                    break;
                default:
                    break;
            }
            #endregion

            //上月（包括往月）申请的活动（按照费用活动费用申请生成时间算），在下月3日不允许点击“取消活动”按钮，变成灰色
            IList<FNA_FeeApply> feeApplyList = FNA_FeeApplyBLL.GetModelList("FeeType = 9 AND (SELECT  DATEADD(second,1,DATEADD(DAY,-23,EndDate))FROM MCS_Pub.dbo.AC_AccountMonth  WHERE ID = AccountMonth+1)< GETDATE() AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',5)='" + m.ID.ToString() + "'");
            if (feeApplyList.Count > 0)
            {
                bt_Cancel.Enabled = false;
            }
            string condition = "";

            //if (m.FeeApply != 0)
            //{
            //    gv_FeeListDetail.BindGrid(new FNA_FeeApplyBLL(m.FeeApply).Items);
            //}
        }

    }

    private void BindGrid()
    {
        IList<CAT_FeeApplyDetail> _details = (IList<CAT_FeeApplyDetail>)ViewState["FeeListDetails"];
        gv_FeeListDetail.BindGrid<CAT_FeeApplyDetail>(_details);
        IList<CAT_GiftApplyDetail> _giftdetail = (IList<CAT_GiftApplyDetail>)ViewState["GiftListDetails"];
        gv_GiftListDetail.BindGrid<CAT_GiftApplyDetail>(_giftdetail);
        IList<CAT_SalesVolumeDetail> _salesdetail = (IList<CAT_SalesVolumeDetail>)ViewState["SalesListDetails"];
        gv_SalesList.BindGrid<CAT_SalesVolumeDetail>(_salesdetail);
    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CAT_ActivityBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new CAT_ActivityBLL((int)ViewState["ID"]);

        }
        else
        {
            //新增
            _bll = new CAT_ActivityBLL();
        }


        pl_detail.GetData(_bll.Model);


        #region 判断必填项
        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "请选择活动举办的管理片区！");
            return;
        }

        if (_bll.Model.Officialcity == 0)
        {
            MessageBox.Show(this, "请选择活动所属城市！");
            return;
        }

        if (_bll.Model.Topic == "")
        {
            MessageBox.Show(this, "活动主题不能为空！");
            return;
        }
        if (_bll.Model.Address == "")
        {
            MessageBox.Show(this, "举办地址不能为空！");
            return;
        }

        if (_bll.Model.Classify == 0)
        {
            MessageBox.Show(this, "请选择活动的分类！");
            return;
        }
        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0 && sender != null) MessageBox.ShowAndRedirect(this, "修改成功!", "CAT_ActivityList.aspx");
        }
        else
        {
            //新增

            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.ApproveFlag = 2;
            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                IList<CAT_FeeApplyDetail> _details = (IList<CAT_FeeApplyDetail>)ViewState["FeeListDetails"];
                IList<CAT_GiftApplyDetail> _giftdetail = (IList<CAT_GiftApplyDetail>)ViewState["GiftListDetails"];
                CAT_FeeApplyDetailBLL _feebll = new CAT_FeeApplyDetailBLL();
                CAT_GiftApplyDetailBLL _giftbll = new CAT_GiftApplyDetailBLL();
                foreach (CAT_GiftApplyDetail _m in _giftdetail)
                {
                    _m.Activity = (int)ViewState["ID"];
                    _giftbll.Model = _m;
                    _giftbll.Add();
                }
                foreach (CAT_FeeApplyDetail _m in _details)
                {
                    _m.Activity = (int)ViewState["ID"];
                    _feebll.Model = _m;
                    _feebll.Add();
                }
                MessageBox.ShowAndRedirect(this, "新增成功!", "CAT_ActivityDetail.aspx?ID=" + ViewState["ID"].ToString());
            }

        }

    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                Response.Redirect("CAT_JoinInfoList.aspx?ID=" + ViewState["ID"].ToString());
                break;
        }
    }

    protected void bt_Stage_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            CAT_ActivityBLL a = new CAT_ActivityBLL((int)ViewState["ID"]);
            if (a.Model == null) return;

            if (a.Model.State != 4)
            {
                MessageBox.Show(this, "只有状态为排期中的活动，才可准备举办!");
                return;
            }

            a.Model.State = 11;
            a.Model["PlanToPrepareDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            a.Update();
            MessageBox.ShowAndRedirect(this, "操作成功!", "CAT_ActivityDetail.aspx?ID=" + ViewState["ID"].ToString());
        }
    }

    protected void bt_Complete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            CAT_ActivityBLL _bll = new CAT_ActivityBLL((int)ViewState["ID"]);
            CaculateActJoinNumber(_bll.Model.ID);
            pl_detail.GetData(_bll.Model);

            #region 判断必填项
            IList<CAT_ClientJoinInfo> ClientJoinInfoList = CAT_ClientJoinInfoBLL.GetModelList("CAT_ClientJoinInfo.Activity=" + _bll.Model.ID);

            if (ClientJoinInfoList.Count > 0)
            {
                foreach (CAT_ClientJoinInfo info in ClientJoinInfoList)
                {
                    if (info.JoinState == 0)
                    {
                        MessageBox.ShowAndRedirect(this, "参与客户情况界面的参与状态必填！", "CAT_JoinInfoList.aspx?ID=" + ViewState["ID"].ToString());
                        return;
                    }
                }
            }

            if (_bll.Model["CompleteDate"] != null && _bll.Model["CompleteDate"] == "1900-01-01")
            {
                MessageBox.Show(this, "请录入活动的实际完成举办日期！");
                return;
            }

            if (_bll.Model.ApproveFlag != 1)
            {
                MessageBox.Show(this, "请确保活动已审核完！");
                return;
            }
            else if (_bll.Model.ApproveFlag == 1 && _bll.Model["ApproveTime"] != "" && _bll.Model["ApproveTime"] != null)
            {
                //if (DateTime.Parse(_bll.Model["CompleteDate"]) < DateTime.Parse(_bll.Model["ApproveTime"]))
                //{
                //    MessageBox.Show(this, "活动的完成举办日期要求必需在审核通过日期之后！");
                //    return;
                //}
            }


            TextBox txt_Sales = (TextBox)pl_detail.FindControl("CAT_Activity_Sales");

            if (txt_Sales != null && txt_Sales.Text.Trim() == "")
            {
                MessageBox.Show(this, "请填写销售额！");
                return;
            }


            TextBox txt_ActLecture = (TextBox)pl_detail.FindControl("CAT_Activity_ActLecturer");
            if (txt_ActLecture != null && txt_ActLecture.Text.Trim() == "")
            {
                MessageBox.Show(this, "请填写实际参与活动的讲师！");
                return;
            }

            TextBox txt_ActLecturerTelenum = (TextBox)pl_detail.FindControl("CAT_Activity_ActLecturerTelenum");
            if (txt_ActLecturerTelenum != null && txt_ActLecturerTelenum.Text.Trim() == "")
            {
                MessageBox.Show(this, "请填写实际参与活动的讲师的联系方式！");
                return;
            }
            #endregion

            _bll.Model.State = 2;
            _bll.Update();

            BindData();
        }
    }

    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            CAT_ActivityBLL _bll = new CAT_ActivityBLL((int)ViewState["ID"]);

            _bll.Model.State = 3;
            _bll.Update();

            BindData();
        }
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_OK_Click(null, null);

            CAT_ActivityBLL bll = new CAT_ActivityBLL((int)ViewState["ID"]);

            if (bll.Model.FeeApply > 0)
            {
                MessageBox.ShowAndRedirect(this, "该活动已关联到费用信息，请至费用信息中提交该申请！", "../FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + bll.Model.FeeApply.ToString());
                return;
            }

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("FeeApply", bll.Model.FeeApply.ToString());
            #region 组合审批任务主题
            string title = bll.Model.Topic;
            #endregion

            int TaskID = EWF_TaskBLL.NewTask("CAT_ActivityApply", (int)Session["UserID"], title, "~/SubModule/CAT/CAT_ActivityDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID > 0)
            {
                bll.Model.State = 12;    //提交审批中
                bll.Model["TaskID"] = TaskID.ToString();
                bll.Update();

                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }
            #endregion

            Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
        }
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            CAT_ActivityBLL _bll = new CAT_ActivityBLL((int)ViewState["ID"]);
            _bll.Approve(1, (int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "审批成功!", "CAT_ActivityList.aspx");
        }
    }

    protected void CaculateActJoinNumber(int activityId)
    {
        TextBox txtActiveJoinClientNumber = (TextBox)pl_detail.FindControl("CAT_Activity_ActiveJoinClientNumber");
        txtActiveJoinClientNumber.Enabled = false;
        try
        {
            CAT_ClientJoinInfoBLL joinBll = new CAT_ClientJoinInfoBLL();
            IList<CAT_ClientJoinInfo> joinInfoList = joinBll._GetModelList("Activity=" + activityId + "And JoinState = 1 ");
            txtActiveJoinClientNumber.Text = joinInfoList.Count.ToString();
        }
        catch
        {
            txtActiveJoinClientNumber.Text = "0(自动填写失败)";
        }
    }

    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        IList<CAT_FeeApplyDetail> _details = (IList<CAT_FeeApplyDetail>)ViewState["FeeListDetails"];
        CAT_FeeApplyDetail _m = new CAT_FeeApplyDetail();
        int client = 0;
        int.TryParse(select_Client.SelectValue, out client);
        _m.Client = client;
        _m.AccountTitle = int.Parse(ddl_AccountTitle.SelectedValue);
        if (TreeTableBLL.GetChild("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", _m.AccountTitle).Rows.Count > 0)
        {
            MessageBox.Show(this, "费用科目必须选择最底级会计科目!" + ddl_AccountTitle.SelectedItem.Text);
            return;
        }
        _m.ApplyCost = decimal.Parse(tbx_ApplyCost.Text.Trim());
        _m.DICost = decimal.Parse(tbx_DICost.Text.Trim());
        _m.Remark = tbx_Remark.Text.Trim();
        if ((int)ViewState["ID"] != 0)
        {
            _m.BeginMonth = AC_AccountMonthBLL.GetMonthByDate(new CAT_ActivityBLL((int)ViewState["ID"]).Model.PlanBeginDate);
            _m.Activity = (int)ViewState["ID"];
        }
        else
        {
            _m.BeginMonth = AC_AccountMonthBLL.GetCurrentMonth();
        }
        if (_details.Where(p => p.Client == client && p.AccountTitle == _m.AccountTitle).Count() == 0)
        {
            if (_m.Activity > 0)
            {
                CAT_FeeApplyDetailBLL _bll = new CAT_FeeApplyDetailBLL();
                _bll.Model = _m;
                _bll.Add();
            }
            _details.Add(_m);
            BindGrid();
        }

    }
    protected void gv_FeeListDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IList<CAT_FeeApplyDetail> _details = (IList<CAT_FeeApplyDetail>)ViewState["FeeListDetails"];
        int ID = 0;

        if (int.TryParse(gv_FeeListDetail.DataKeys[e.RowIndex]["ID"].ToString(), out ID) && ID > 0)
        {
            CAT_FeeApplyDetailBLL _bll = new CAT_FeeApplyDetailBLL(ID);
            _bll.Delete();
        }
        _details.RemoveAt(e.RowIndex);
        BindGrid();
    }
    protected void bt_OpenAdjust_Click(object sender, EventArgs e)
    {
        Button bt_Adjust = (Button)sender;
        GridViewRow drv = (GridViewRow)bt_Adjust.NamingContainer;
        int rowIndex = drv.RowIndex;
        int ID = int.Parse(gv_FeeListDetail.DataKeys[rowIndex]["ID"].ToString());
        CAT_FeeApplyDetailBLL _bll = new CAT_FeeApplyDetailBLL(ID);
        TextBox tbx_AdjustReason = (TextBox)drv.FindControl("tbx_AdjustReason");
        TextBox tbx_AdjustCost = (TextBox)drv.FindControl("tbx_AdjustCost");
        _bll.Model.AdjustCost = decimal.Parse(tbx_AdjustCost.Text.Trim());
        _bll.Model.AdjustReason = tbx_AdjustReason.Text;
        _bll.Update();

    }
    #region 活动赠品

    protected void btn_AddGift_Click(object sender, EventArgs e)
    {
        IList<CAT_GiftApplyDetail> _giftListdetail = (IList<CAT_GiftApplyDetail>)ViewState["GiftListDetails"];
        CAT_GiftApplyDetail _m = new CAT_GiftApplyDetail();
        int product = 0;
        if (int.TryParse(select_GiftProduct.SelectValue, out product))
        {
            _m.Product = product;
        }
        else
        {
            MessageBox.Show(this, "产品必填！");
            return;
        }
        _m.ApplyQuantity = int.Parse(txt_ApplyCount.Text.Trim());
        _m.Remark = txt_GiftRemark.Text.Trim();
        _m.Activity = (int)ViewState["ID"];
        if (_giftListdetail.Where(p => p.Product == product).Count() == 0)
        {
            _giftListdetail.Add(_m);
            if (_m.Activity > 0)
            {
                CAT_GiftApplyDetailBLL _bll = new CAT_GiftApplyDetailBLL();
                _bll.Model = _m;
                _bll.Add();
            }
            BindGrid();
        }
    }
    protected void gv_GiftListDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IList<CAT_GiftApplyDetail> _giftListdetail = (IList<CAT_GiftApplyDetail>)ViewState["GiftListDetails"];
        int ID = 0;
        if (int.TryParse(gv_GiftListDetail.DataKeys[e.RowIndex]["ID"].ToString(), out ID) && ID > 0)
        {
            CAT_GiftApplyDetailBLL _bll = new CAT_GiftApplyDetailBLL(ID);
            _bll.Delete();
        }
        _giftListdetail.RemoveAt(e.RowIndex);
        BindGrid();
    }
    protected void bt_Adjust_Click(object sender, EventArgs e)
    {
        Button bt_Adjust = (Button)sender;
        GridViewRow drv = (GridViewRow)bt_Adjust.NamingContainer;
        int rowIndex = drv.RowIndex;
        int ID = int.Parse(gv_GiftListDetail.DataKeys[rowIndex]["ID"].ToString());
        TextBox txt_AdjustQuantity = (TextBox)drv.FindControl("txt_AdjustQuantity");
        TextBox txt_UsedQuantity = (TextBox)drv.FindControl("txt_UsedQuantity");
        TextBox txt_BalanceQuantity = (TextBox)drv.FindControl("txt_BalanceQuantity");
        TextBox tbx_Remark = (TextBox)drv.FindControl("tbx_Remark");
        if ((int)ViewState["ID"] > 0)
        {
            CAT_ActivityBLL catbll = new CAT_ActivityBLL((int)ViewState["ID"]);
            CAT_GiftApplyDetailBLL _gitfbll = new CAT_GiftApplyDetailBLL(ID);
            switch (catbll.Model.State)
            {
                case 12:
                    _gitfbll.Model.AdjustQuantity = int.Parse(txt_AdjustQuantity.Text.Trim());
                    break;
                case 1:
                    _gitfbll.Model.UsedQuantity = int.Parse(txt_UsedQuantity.Text.Trim());
                    _gitfbll.Model.BalanceQuantity = int.Parse(txt_BalanceQuantity.Text.Trim());
                    txt_AdjustQuantity.Text = "0";
                    break;
            }
            _gitfbll.Update();
        }
    }
    #endregion
    #region 活动销量
    protected void btn_AddSales_Click(object sender, EventArgs e)
    {
        IList<CAT_SalesVolumeDetail> _saleslist = (IList<CAT_SalesVolumeDetail>)ViewState["SalesListDetails"];
        CAT_SalesVolumeDetail _m = new CAT_SalesVolumeDetail();

        if (ddl_Brand.SelectedValue != "0")
        {
            _m.Brand = int.Parse(ddl_Brand.SelectedValue);
        }
        else
        {
            MessageBox.Show(this, "品牌必填！");
            return;
        }
        _m.Amount = decimal.Parse(txt_Amount.Text.Trim());
        _m.Remark = txt_salesremark.Text.Trim();
        _m.Activity = (int)ViewState["ID"];
        if (_saleslist.Where(p => p.Brand == _m.Brand).Count() == 0)
        {
            _saleslist.Add(_m);
            CAT_SalesVolumeDetailBLL _bll = new CAT_SalesVolumeDetailBLL();
            _bll.Model = _m;
            _bll.Add();
        }
        BindGrid();
    }
    protected void gv_SalesList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IList<CAT_SalesVolumeDetail> _saleslist = (IList<CAT_SalesVolumeDetail>)ViewState["SalesListDetails"];
        int ID = 0;
        if (int.TryParse(gv_SalesList.DataKeys[e.RowIndex]["ID"].ToString(), out ID) && ID > 0)
        {
            CAT_SalesVolumeDetailBLL _bll = new CAT_SalesVolumeDetailBLL(ID);
            _bll.Delete();
        }
        _saleslist.RemoveAt(e.RowIndex);
        BindGrid();
    }
    #endregion

}