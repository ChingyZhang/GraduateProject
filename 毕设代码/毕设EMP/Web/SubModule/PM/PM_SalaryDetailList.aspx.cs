using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;
using MCSFramework.Common;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using System.Data;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;

public partial class SubModule_PM_PM_SalaryDetailList : System.Web.UI.Page
{
    protected bool visibleflag = false, enableflag = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnCancel.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");

            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["Enable"] = false;
            select_promotor.PageUrl = "Search_SelectPromotor.aspx?ExtCondition=PM_Promotor.ID IN (SELECT Promotor FROM MCS_Promotor.dbo.PM_SalaryDetail WHERE SalaryID=" + ViewState["ID"] + ")";
            if ((int)ViewState["ID"] > 0)
            {
                BindData();
                UploadFile1.RelateID = (int)ViewState["ID"];
                UploadFile1.BindGrid();
            }
            else
                Response.Redirect("PM_SalaryList.aspx");
        }
        enableflag = (bool)ViewState["Enable"];
    }

    #region 获取指定管理片区的预算信息(暂不使用)
    //private void BindBudgetInfo(int month, int organizecity)
    //{
    //    tbl_BudgetInfo.Visible = true;
    //    int feetype = ConfigHelper.GetConfigInt("SalaryFeeType");

    //    lb_BudgetAmount.Text = (FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, feetype) +
    //        FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, 0)).ToString("0.###");

    //    lb_BudgetBalance.Text = (FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype) +
    //        FNA_BudgetBLL.GetUsableAmount(month, organizecity, 0)).ToString("0.###");
    //}
    #endregion



    private void BindData()
    {
        PM_SalaryBLL bll = new PM_SalaryBLL((int)ViewState["ID"]);
        pn_detail.BindData(bll.Model);
        ViewState["AccountMonth"] = bll.Model.AccountMonth;
        ViewState["ApplyMonth"] = AC_AccountMonthBLL.GetMonthByDate(bll.Model.InputTime);
        ViewState["State"] = bll.Model.State;

        #region 绑定管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();
        tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(bll.Model.OrganizeCity).Model.SuperID.ToString();
        tr_OrganizeCity.SelectValue = bll.Model.OrganizeCity.ToString();
        lbl_PromotorCount.Text = bll.Items.Count(p => p["FlagCancel"] != "1").ToString();
        lbl_SalesAmount.Text = bll.Items.Where(p => p["FlagCancel"] != "1").Sum(p => p.ActSalesVolume).ToString();
        #endregion
        if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y" && bll.Model.State == 2)
        {
            ViewState["Enable"] = true;
        }

        BindGrid();

        ShowBtnCancel(false);

        if (bll.Model.State != 1)
        {
            pn_detail.SetControlsEnable(false);

            bt_Delete.Visible = false;
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Merge.Visible = false;
        }
        else
        {
            //当前操作人为工资生成人(生成人未提交之前可以取消工资)
            int UserID = 0;
            if (int.TryParse(Session["UserID"].ToString(), out UserID) && UserID == bll.Model.InputStaff)
            {
                ShowBtnCancel(true);
            }
        }

        //if (bll.Model.State != 3)
        //{
        //    BindBudgetInfo((int)ViewState["ApplyMonth"], bll.Model.OrganizeCity);
        //}

        if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "工资不能单独审批", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btn_Pass').disabled='disabled'; </script>");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "工资不能单条审批", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_bt_SaveDecisionComment').disabled='disabled'; </script>");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "工资不能单条处理", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btn_WaitProcess').disabled='disabled'; </script>");
            string[] allowdays = Addr_OrganizeCityParamBLL.GetValueByType(1, 9).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
            if (!allowdays.Contains(DateTime.Now.Day.ToString()))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "超时不能单独审批", " <script>window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btn_NotPass').disabled='disabled'; </script>");
            }
            //流程中允许取消工资
            ShowBtnCancel(true);
        }

    }

    void ShowBtnCancel(bool flag)
    {
        btnCancel.Visible = flag;
        if (gv_List != null && gv_List.Columns.Count > 0)//不进行此判断当GridView数据为空时会出现空引用
        {
            gv_List.Columns[0].Visible = flag;
            if (gv_List.HeaderRow!=null&&gv_List.HeaderRow.Cells.Count>0)
            {
                gv_List.HeaderRow.Cells[0].Visible = flag;
            }
        }
    }

    private void BindGrid()
    {
        PM_SalaryBLL bll = new PM_SalaryBLL((int)ViewState["ID"]);
        string condition = "PM_SalaryDetail.SalaryID=" + ViewState["ID"].ToString() + " AND PM_SalaryDataObject.AccountMonth=" + ViewState["AccountMonth"].ToString();
        if (tr_OrganizeCity.SelectValue != bll.Model.OrganizeCity.ToString())
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND  MCS_Promotor.dbo.PM_Promotor.OrganizeCity in(" + orgcitys + ") ";
        }
        if (cb_OnlyDisplayZero.Checked)
        {
            condition += " AND PM_SalaryDetail.Bonus=0";
        }

        if (txt_Promotor.Text.Trim() != "")
        {
            condition += " AND PM_Promotor.Name LIKE '%" + txt_Promotor.Text.Trim() + "%'";
        }

        //获取未被取消的已生成的导购工资
        condition += " AND CAST(MCS_SYS.dbo.UF_Spilt(PM_SalaryDetail.ExtPropertys,'|',31) AS  INT)!=1 ";


        gv_List.OrderFields = "PM_Promotor_Name";
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
        MatrixTable.GridViewMatric(gv_List);

        lb_TotalCost.Text = PM_SalaryBLL.GetSumSalary((int)ViewState["ID"]).ToString("0.##");

        if ((int)ViewState["State"] == 2 || ((int)ViewState["State"] == 3))
        {

            foreach (GridViewRow gr in gv_List.Rows)
            {
                if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
                {
                    bt_SaveChange.Visible = true;
                    ((TextBox)gr.FindControl("tbx_PayAdjust_Approve")).Enabled = true;
                    ((TextBox)gr.FindControl("tbx_PayAdjust_Reason")).Enabled = true;
                    ((TextBox)gr.FindControl("tbx_DIPayAdjust_Approve")).Enabled = true;
                    ((TextBox)gr.FindControl("tbx_DIPayAdjust_Reason")).Enabled = true;

                    PM_SalaryDetail _detail = new PM_SalaryBLL().GetDetailModel((int)gv_List.DataKeys[gr.RowIndex][0]);

                    if (CM_ContractBLL.GetModelList(@" Classify=4 AND Client IN (SELECT Client FROM MCS_Promotor.dbo.PM_PromotorInRetailer
                    WHERE Promotor= " + _detail.Promotor + ") AND State=3 AND ApproveFlag=1 AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',3)<>''"
                     + "AND CONVERT(DECIMAL(18,3),MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',3))>0 AND '" + bll.Model.InputTime.ToString("yyyy-MM-dd") + "'"
                     + "Between BeginDate AND EndDate").Count == 0)
                    {
                        ((TextBox)gr.FindControl("tbx_PMFee_Adjust")).Enabled = true;
                        ((TextBox)gr.FindControl("tbx_PMFee_Adjust_Reason")).Enabled = true;
                    }
                    ((TextBox)gr.FindControl("tbx_PMFee1_Adjust")).Enabled = true;

                }
            }
        }
        for (int i = 0; i < gv_List.Columns.Count; i++)
        {

            if (gv_List.Columns[i].HeaderText.Contains("小计") ||
                gv_List.Columns[i].HeaderText.Contains("合计") ||
                gv_List.Columns[i].HeaderText.Contains("我司实发额"))
            {
                gv_List.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.Blue;
                gv_List.Columns[i].ItemStyle.ForeColor = System.Drawing.Color.Blue;
            }

            if (gv_List.Columns[i].HeaderText.Contains("导购实得薪资小计"))
            {
                gv_List.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.Brown;
                gv_List.Columns[i].ItemStyle.ForeColor = System.Drawing.Color.Brown;
                gv_List.Columns[i].ItemStyle.Font.Bold = true;
            }
        }

    }


    protected string PromotorInClient(string RetailerS)
    {
        if (RetailerS.Equals("")) return "";
        string clientname = "";

        IList<CM_Client> lists = CM_ClientBLL.GetModelList("ID IN (" + RetailerS + ")");
        int count = 0;
        foreach (CM_Client c in lists)
        {
            if (count < 2)
            {

                clientname += "<a href='../CM/RT/RetailerDetail.aspx?ClientID=" + c.ID.ToString() + "' target='_blank' class='listViewTdLinkS1'>"
                    + c.FullName + "</a><br/>";
            }
            else
            {
                break;
            }
            count++;
        }
        if (count > 1) clientname += "共" + lists.Count.ToString() + "个门名";
        return clientname;
    }

    protected string GetAccountMonth()
    {
        return ViewState["AccountMonth"].ToString();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        //PM_SalaryBLL bll = new PM_SalaryBLL((int)ViewState["ID"]);

        //pn_detail.GetData(bll.Model);
        //bll.Update();



        //if (sender != null)
        //    MessageBox.ShowAndRedirect(this, "导购员工资保存成功!", "PM_SalaryDetailList.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            bt_Save_Click(null, null);

            PM_SalaryBLL bll = new PM_SalaryBLL((int)ViewState["ID"]);

            #region 判断预算余额是否够工单申请(暂不使用)
            //int feetype = ConfigHelper.GetConfigInt("SalaryFeeType");

            //decimal budgetbalance = FNA_BudgetBLL.GetUsableAmount((int)ViewState["ApplyMonth"], bll.Model.OrganizeCity, feetype) +
            //    FNA_BudgetBLL.GetUsableAmount((int)ViewState["ApplyMonth"], bll.Model.OrganizeCity, 0);
            //if (budgetbalance < bll.GetSumSalary())
            //{
            //    MessageBox.Show(this, "对不起，您当前的预算余额不够申请此导购员工资申请，您当前的预算余额为:" + budgetbalance.ToString());
            //    return;
            //}
            #endregion

            #region 判断导购是否有销量
            //if(new PM_SalaryBLL((int)ViewState["ID"]).Items.Sum(p=>p.ActSalesVolume)==0)
            //{               
            //     MessageBox.Show(this, "该工资单中所有导购的销量均为零，请确认是否异常!");

            //     return;
            //}
            #endregion

            if (bll.Model["TaskID"] != "" && bll.Model.State == 2)
            {
                MessageBox.ShowAndRedirect(this, "该工资单已提交，请勿重复提交!", "PM_SalaryDetailList.aspx?ID=" + ViewState["ID"].ToString());
                return;
            }

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("ApplyCost", lb_TotalCost.Text);
            dataobjects.Add("PromotorNum", bll.Items.Count.ToString());

            //组合审批任务主题
            Addr_OrganizeCity _city = new Addr_OrganizeCityBLL(bll.Model.OrganizeCity).Model;
            string title = _city.Name + ",导购员工资总额:" + lb_TotalCost.Text + ",人数:" + bll.Items.Count.ToString();

            int TaskID = EWF_TaskBLL.NewTask("PM_SalaryApplyFlow", (int)Session["UserID"], title, "~/SubModule/PM/PM_SalaryDetailList.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID > 0)
            {
                bll.Submit((int)Session["UserID"], TaskID, 0);
                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }
            #endregion

            MessageBox.ShowAndRedirect(this, "导购员工资提交申请成功!", "../EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new PM_SalaryBLL((int)ViewState["ID"]).Delete();

            MessageBox.ShowAndRedirect(this, "导购员工资删除成功!", "PM_SalaryList.aspx");
        }
    }

    protected void bt_SaveChange_Click(object sender, EventArgs e)
    {
        PM_SalaryBLL bll = new PM_SalaryBLL((int)ViewState["ID"]);

        #region 保存每一个导购员工资情况
        foreach (GridViewRow row in gv_List.Rows)
        {
            string _promotorname = ((HyperLink)(row.FindControl("hy_PromotorName"))).Text;

            int id = (int)gv_List.DataKeys[row.RowIndex][0];
            PM_SalaryDetail detail = bll.Items.First<PM_SalaryDetail>(item => item.ID == id);

            decimal org_adjust = 0; decimal di_org_adjust = 0;
            decimal adjust = 0; decimal di_adjust = 0;
            decimal pm_fee = 0, pm_fee_adjust = 0, pm_fee1 = 0, pm_fee1_adjust = 0;
            decimal org_pm_fee_adjust = 0, org_pm_fee1_adjust = 0, pm_fee2 = 0;

            decimal.TryParse(detail["PayAdjust_Approve"], out org_adjust);
            decimal.TryParse(((TextBox)row.FindControl("tbx_PayAdjust_Approve")).Text, out adjust);
            decimal.TryParse(detail["DIPayAdjust_Approve"], out di_org_adjust);
            decimal.TryParse(((TextBox)row.FindControl("tbx_DIPayAdjust_Approve")).Text, out di_adjust);
            decimal.TryParse(((TextBox)row.FindControl("tbx_PMFee_Adjust")).Text, out pm_fee_adjust);
            decimal.TryParse(((TextBox)row.FindControl("tbx_PMFee1_Adjust")).Text, out pm_fee1_adjust);
            decimal.TryParse(detail["PMFee"], out pm_fee);
            decimal.TryParse(detail["PMFee1"], out pm_fee1);
            decimal.TryParse(detail["PMFee_Adjust"], out org_pm_fee_adjust);
            decimal.TryParse(detail["PMFee1_Adjust"], out org_pm_fee1_adjust);
            decimal.TryParse(detail["PMFee2"], out pm_fee2);

            if (detail.Bonus < 0)
                detail.Bonus = 0;
            if (detail.Pay1 < 0)
                detail.Pay1 = 0;
            if (org_adjust != adjust)
            {
                detail["PayAdjust_Approve"] = adjust.ToString();
                if (((TextBox)row.FindControl("tbx_PayAdjust_Reason")).Text == "")
                {
                    MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，原因不能为空!");
                    return;
                }
                detail.Sum1 += Math.Round(adjust - org_adjust, 1, MidpointRounding.AwayFromZero);
                if (detail.Sum1 + detail.Sum3 < 0)
                {
                    MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，导购实得薪资小计不能为负!");
                    return;
                }
                detail["Remark"] += "--" + Session["UserRealName"].ToString() + "将调整从" + org_adjust.ToString() + "修改为" + detail["PayAdjust_Approve"] + ";修改原因:" + ((TextBox)row.FindControl("tbx_PayAdjust_Reason")).Text;
                if (detail.Sum1 < 0)
                {
                    MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，我司提成不能为负!");
                    ((TextBox)row.FindControl("tbx_PayAdjust_Approve")).Focus();
                    return;
                }
                bll.UpdateDetail(detail);
                PM_SalaryBLL.UpdateAdjustRecord((int)ViewState["ID"], (int)Session["UserID"], org_adjust.ToString(), detail["PayAdjust_Approve"], _promotorname);
            }
            if (di_org_adjust != di_adjust)
            {
                detail["DIPayAdjust_Approve"] = di_adjust.ToString();
                if (((TextBox)row.FindControl("tbx_DIPayAdjust_Reason")).Text == "")
                {
                    MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，经销商调整原因不能为空!");
                    return;
                }
                detail.Sum3 += Math.Round(di_adjust - di_org_adjust, 1, MidpointRounding.AwayFromZero);
                if (detail.Sum1 + detail.Sum3 < 0)
                {
                    MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，导购实得薪资小计不能为负!");
                    return;
                }
                detail["DI_Remark"] += "--" + Session["UserRealName"].ToString() + "将调整从" + di_org_adjust.ToString() + "修改为" + detail["DIPayAdjust_Approve"] + ";修改原因:" + ((TextBox)row.FindControl("tbx_DIPayAdjust_Reason")).Text;
                if (detail.Sum3 < 0)
                {
                    MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，经销商薪资合计不能为负!");
                    ((TextBox)row.FindControl("tbx_DIPayAdjust_Approve")).Focus();
                    return;
                }
                bll.UpdateDetail(detail);
            }
            if (pm_fee_adjust - org_pm_fee_adjust != 0 || pm_fee1_adjust - org_pm_fee1_adjust != 0)
            {
                if (pm_fee_adjust - org_pm_fee_adjust != 0)
                {
                    detail["PMFee_Adjust"] = pm_fee_adjust.ToString();

                    if (((TextBox)row.FindControl("tbx_PMFee_Adjust_Reason")).Text == "")
                    {
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，管理费明细调整原因不能为空!");
                        return;
                    }

                    pm_fee2 = pm_fee2 + pm_fee_adjust;

                    if (pm_fee2 < 0)
                    {
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，导购管理费不能为负!");
                        return;
                    }
                    detail["PMFee_Remark"] += "--" + Session["UserRealName"].ToString() + "将管理费调整从" + org_pm_fee_adjust.ToString() + "修改为" + pm_fee_adjust.ToString() + ";修改原因:" + ((TextBox)row.FindControl("tbx_PMFee_Adjust_Reason")).Text;
                }
                if (pm_fee1_adjust - org_pm_fee1_adjust != 0)
                {
                    detail["PMFee1_Adjust"] = pm_fee1_adjust.ToString();
                    if (((TextBox)row.FindControl("tbx_PMFee_Adjust_Reason")).Text == "")
                    {
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，管理费明细调整原因不能为空!");
                        return;
                    }
                    pm_fee1 = pm_fee1 + pm_fee1_adjust;
                    if (pm_fee1 < 0)
                    {
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _promotorname + "的工资审批调整后，导购管理费小计不能为负!");
                        return;
                    }
                    detail["PMFee_Remark"] += "--" + Session["UserRealName"].ToString() + "将管理费小计调整从" + org_pm_fee1_adjust.ToString() + "修改为" + pm_fee1_adjust.ToString() + ";修改原因:" + ((TextBox)row.FindControl("tbx_PMFee_Adjust_Reason")).Text;
                }
                detail.CoPMFee = Math.Round(pm_fee2 + pm_fee1, 1, MidpointRounding.AwayFromZero);
                detail["PMFeeTotal"] = (detail.CoPMFee + detail.DIPMFee).ToString();
                bll.UpdateDetail(detail);
            }
        }


        #endregion

        BindGrid();
        MessageBox.Show(this, "工资调整金额保存成功!");
    }
    protected void bt_search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void bt_Merge_Click(object sender, EventArgs e)
    {
        int result = PM_SalaryBLL.Merge((int)ViewState["AccountMonth"], ViewState["ID"].ToString(), (int)Session["UserID"]);
        if (result > 0)
        {
            MessageBox.ShowAndRedirect(this, "合并成功!", "PM_SalaryDetailList.aspx?ID=" + result.ToString());
        }
        else
        {
            MessageBox.Show(this, "合并失败，此工资单上级管理片区无工资单。");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        List<int> failureList = new List<int>();

        //重置导购人数和门店总销售金额
        int promotorCount = int.Parse(lbl_PromotorCount.Text);
        decimal salesAmount = decimal.Parse(lbl_SalesAmount.Text);

        foreach (GridViewRow gr in gv_List.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                int SalaryDetailID = int.Parse(gv_List.DataKeys[gr.RowIndex][0].ToString());


                int UserID = int.Parse(Session["UserID"].ToString());
                int ret = PM_SalaryBLL.CancelSalaryDetail(SalaryDetailID, UserID);
                if (ret != 0)
                {
                    failureList.Add(SalaryDetailID);
                }
                else
                {
                    promotorCount--;//取消成功后人数减1
                    //扣除该导购销售额
                    PM_SalaryBLL bll = new PM_SalaryBLL(SalaryDetailID);
                    salesAmount -= bll.GetDetailModel(SalaryDetailID).ActSalesVolume;
                }
            }
        }

        lbl_PromotorCount.Text = promotorCount.ToString();
        lbl_SalesAmount.Text = salesAmount.ToString();

        if (failureList.Count == 0)
        {
            MessageBox.Show(this, "取消成功");
        }
        else
        {
            StringBuilder sb = new StringBuilder("错误代码：");
            foreach (int item in failureList)
            {
                sb.Append(item.ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            MessageBox.Show(this, sb.ToString());
        }

        BindGrid();
    }

    protected void gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewRec")
        {
            MessageBox.Show(this, e.CommandArgument.ToString());
        }
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        MatrixTable.GridViewMatric(gv_List);
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        visibleflag = true;
        gv_List.AllowPaging = false;

        BindGrid();
        if (gv_List.HeaderRow.Cells[0].Visible && !gv_List.Columns[0].Visible)
        {
            gv_List.HeaderRow.Cells[0].Visible = false;
        }
        string filename = HttpUtility.UrlEncode("导购工资单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();
        visibleflag = false;
        gv_List.AllowPaging = true;

        BindGrid();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

}
