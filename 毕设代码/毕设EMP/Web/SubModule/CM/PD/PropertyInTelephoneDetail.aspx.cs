// ===================================================================
// 文件路径:SubModule/CM/PD/PropertyInTelephoneDetail.aspx.cs 
// 生成日期:2012/3/6 23:14:38 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;

public partial class SubModule_CM_PD_PropertyInTelephoneDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["TelephoneID"] = Request.QueryString["TelephoneID"] != null ? int.Parse(Request.QueryString["TelephoneID"]) : 0;
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }

            if ((int)ViewState["TelephoneID"] == 0 && (int)ViewState["ClientID"] == 0) Response.Redirect("~/SubModule/DeskTop.aspx");
            #endregion

            BindDropDown();

            if ((int)ViewState["TelephoneID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                CM_PropertyInTelephone tele = new CM_PropertyInTelephone();
                tele.Client = (int)ViewState["ClientID"];
                tele.InsertStaff = (int)Session["UserID"];
                tele.InsertTime = DateTime.Now;
                tele.State = 1;
                tele.InstallDate = DateTime.Today;

                pl_detail.BindData(tele);

                bt_Submit.Visible = false;
                bt_Uninstall.Visible = false;
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
        CM_PropertyInTelephone m = new CM_PropertyInTelephoneBLL((int)ViewState["TelephoneID"]).Model;
        if (m != null) pl_detail.BindData(m);

        #region 状态控制
        switch (m.State)
        {
            case 1:
                bt_Install.Visible = false;
                bt_Uninstall.Visible = false;
                break;
            case 2:
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
                bt_Submit.Visible = false;
                bt_Install.Visible = false;
                bt_Uninstall.Visible = false;
                break;
            case 3:
                pl_detail.SetControlsEnable(false);
                ((TextBox)pl_detail.FindControl("CM_PropertyInTelephone_TeleNumber")).Enabled = true;
                bt_Submit.Visible = false;
                bt_OK.Visible = false;
                break;
            case 4:
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
                bt_Submit.Visible = false;
                bt_Install.Visible = false;
                break;
            case 5:
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
                bt_Submit.Visible = false;
                bt_Install.Visible = false;
                bt_Uninstall.Visible = false;
                break;
            default:
                break;
        }
        #endregion
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_PropertyInTelephoneBLL _bll;
        if ((int)ViewState["TelephoneID"] != 0)
        {
            //修改
            _bll = new CM_PropertyInTelephoneBLL((int)ViewState["TelephoneID"]);
        }
        else
        {
            //新增
            _bll = new CM_PropertyInTelephoneBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion
        if ((int)ViewState["TelephoneID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "PropertyDetail.aspx?ClientID=" + _bll.Model.Client.ToString());
            }
        }
        else
        {
            //新增
            _bll.Model.State = 1;
            _bll.Model.ApproveFlag = 2;

            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["TelephoneID"] = _bll.Add();
            if ((int)ViewState["TelephoneID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "PropertyDetail.aspx?ClientID=" + _bll.Model.Client.ToString());
            }
        }

    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["TelephoneID"] != 0)
        {
            string _title = "";

            CM_PropertyInTelephoneBLL _bll = new CM_PropertyInTelephoneBLL((int)ViewState["TelephoneID"]);
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", _bll.Model.ID.ToString());
            if (_bll.Model.Client > 0)
            {
                CM_Client _c = new CM_ClientBLL(_bll.Model.Client).Model;
                if (_c != null)
                {
                    dataobjects.Add("ClientID", _bll.Model.Client.ToString());
                    dataobjects.Add("ClientName", _c.FullName);
                    _title += _c.FullName;
                }
            }
            dataobjects.Add("TeleNumber", _bll.Model.TeleNumber.ToString());
            dataobjects.Add("TeleCost", _bll.Model.TeleCost.ToString("0.##"));
            dataobjects.Add("NetCost", _bll.Model.NetCost.ToString("0.##"));

            #region 组织任务标题

            _title += "电话号码:" + _bll.Model.TeleNumber;

            #endregion

            int TaskID = EWF_TaskBLL.NewTask("CM_PropertyInTelephone_Flow", (int)Session["UserID"], _title,
                "~/SubModule/CM/PD/PropertyInTelephoneDetail.aspx?TelephoneID=" + _bll.Model.ID.ToString(), dataobjects);
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

    protected void bt_Install_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["TelephoneID"] != 0)
        {
            CM_PropertyInTelephoneBLL _bll = new CM_PropertyInTelephoneBLL((int)ViewState["TelephoneID"]);
            pl_detail.GetData(_bll.Model);

            if (_bll.Model.TeleNumber == "")
            {
                MessageBox.Show(this, "请正确输入安装的电话号码!");
                return;
            }

            _bll.Model.State = 4;       //已安装状态
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "成功将该电话设为安装状态!", "PropertyDetail.aspx?ClientID=" + _bll.Model.Client.ToString());
            }
        }
    }
    protected void bt_Uninstall_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["TelephoneID"] != 0)
        {
            CM_PropertyInTelephoneBLL _bll = new CM_PropertyInTelephoneBLL((int)ViewState["TelephoneID"]);

            _bll.Model.UninstallDate = DateTime.Today;
            _bll.Model.State = 5;       //已拆机状态
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "成功将该电话设为已拆机状态，以后无法再申请该电话的费用!", "PropertyDetail.aspx?ClientID=" + _bll.Model.Client.ToString());
            }
        }
    }

}