using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.Pub;
using System.Collections.Specialized;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSControls.MCSWebControls;
using MCSFramework.Model;
public partial class SubModule_FNA_HR_SpecialApplyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                DropDownList ddl_AccountMonth = (DropDownList)pl_detail.FindControl("HR_SpecialApply_AccountMonth");
                if (ddl_AccountMonth != null)
                    ddl_AccountMonth.SelectedValue = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetCurrentMonth()).Model.ID.ToString();

                bt_Submit.Visible = false;
            }
        }

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        HR_SpecialApply m = new HR_SpecialApplyBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

            if (m.Task > 0 || m.ApproveFlag == 3 || m.InsertStaff != (int)Session["UserID"])//ApproveFlag 状态采用费用申请State审批状态
            {
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
                bt_Submit.Visible = false;
            }

            if (!string.IsNullOrEmpty(m.Task.ToString()) && m.Task != 0)
            {
                bt_Submit.Visible = false;
            }

            if (m.ApproveFlag == 1)//ApproveFlag 状态采用费用申请State审批状态
            {

            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        HR_SpecialApplyBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new HR_SpecialApplyBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new HR_SpecialApplyBLL();
            _bll.Model.AccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "必须正确选择管理片区!");
            return;
        }
        if (_bll.Model.AccountMonth == 0)
        {
            MessageBox.Show(this, "必须正确选择会计月!");
            return;
        }
        if (_bll.Model.AccountTitleType == 0)
        {
            MessageBox.Show(this, "必须正确选择科目!");
            return;
        }
        //if (_bll.Model["SectorName"]=="0")
        //{
        //    MessageBox.Show(this, "必须正确选择审批部门名称!");
        //    return;
        //}

        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                if (sender != null) MessageBox.ShowAndRedirect(this, "修改成功!", "HR_SpecialApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }
        else
        {
            //新增

            _bll.Model.ApproveFlag = 1;//ApproveFlag 状态采用费用申请State审批状态
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
            bt_Submit.Visible = true;
            if ((int)ViewState["ID"] > 0)
            {
                if (sender != null) MessageBox.ShowAndRedirect(this, "新增成功!", "HR_SpecialApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }

    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_OK_Click(null, null);

            HR_SpecialApplyBLL bll = new HR_SpecialApplyBLL((int)ViewState["ID"]);

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("AccountTitleType", bll.Model.AccountTitleType.ToString());
            dataobjects.Add("IsKA", bll.Model["IsKA"].ToString());
            dataobjects.Add("SectorName", bll.Model["SectorName"].ToString());

            #region 组合审批任务主题
            string title = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", bll.Model.OrganizeCity) + " 特殊申请ID=:" + bll.Model.ID.ToString();
            #endregion

            int TaskID = EWF_TaskBLL.NewTask("EWF_SpecialApply", (int)Session["UserID"], title, "~/SubModule/FNA/HR_SpecialApplyDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID <= 0)
            {
                MessageBox.Show(this, "对不起，工作流发起失败，请与管理员联系！");
                return;
            }
            bll.Model.Task = TaskID;
            bll.Model.ApproveFlag = 2;//ApproveFlag 状态采用费用申请State审批状态
            bll.Update();
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            #endregion

            MessageBox.ShowAndRedirect(this, "特殊申请提交成功！", Page.ResolveClientUrl("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString()));
        }
    }
}