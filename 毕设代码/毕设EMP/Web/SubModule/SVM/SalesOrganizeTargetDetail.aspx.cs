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
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.Promotor;
using System.Collections.Generic;
public partial class SubModule_SVM_SalesOrganizeTargetDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["TargetID"] = Request.QueryString["TargetID"] == null ? 0 : int.Parse(Request.QueryString["TargetID"]);

            if ((int)ViewState["TargetID"] == 0 && (int)ViewState["ClientID"] == 0)
            {
                Response.Redirect("../desktop.aspx");
            }


            #endregion
            BindData();
            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        bt_Save_Click(null, null);
        SVM_OrganizeTargetBLL target = new SVM_OrganizeTargetBLL((int)ViewState["TargetID"]);
        target.Model.ApproveFlag = 4;
        target.Model.UpdateStaff = (int)Session["UserID"];
        target.Update();
    }


    private void BindData()
    {
        SVM_OrganizeTarget target=new SVM_OrganizeTargetBLL((int)ViewState["TargetID"]).Model;
        dv_detail.BindData(target);
        if (target.ApproveFlag == 1 || target.ApproveFlag == 4)
        {
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Approve.Visible = true;
        }
        else
        {
            bt_Approve.Visible = false;
        }
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        gv_List.ConditionString = " TargetID=" + ViewState["TargetID"];
        gv_List.BindGrid();
    }
    #endregion

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        decimal val;
        if (decimal.TryParse(Addr_OrganizeCityParamBLL.GetValueByType(1, 8), out val))
        {
            SVM_OrganizeTargetBLL _bll = new SVM_OrganizeTargetBLL((int)ViewState["TargetID"]);
            decimal oldvalue = _bll.Model.SalesTarget;
            dv_detail.GetData(_bll.Model);
            decimal changevalue = _bll.Model.SalesTargetAdjust;
            if (oldvalue * val >= Math.Abs(changevalue))
            {
                _bll.Model.SalesTargetAdjust = val;
                _bll.Update();
                BindData();
            }
            else
            {
                MessageBox.Show(this, "系统限定必须在原有目标值基础上浮动调整" + val + "%以内，请重新调整！");
                return;
            }

        }
        else
            MessageBox.Show(this, "系统参数中未维护调整浮动比例，或浮动比例维护有误！");

        if (sender != null)
            MessageBox.Show(this, "修改成功!");
    }
    #endregion

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        SVM_OrganizeTargetBLL target = new SVM_OrganizeTargetBLL((int)ViewState["TargetID"]);
        target.Model.ApproveFlag = 1;
        target.Model.UpdateStaff = (int)Session["UserID"];
        target.Update();
    }
}

