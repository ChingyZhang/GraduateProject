// ===================================================================
// 文件路径:SubModule/PBM/Partner/Vehicle/VehicleDetail.aspx.cs 
// 生成日期:2015-02-04 16:10:23 
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
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_PBM_Partner_Vehicle_VehicleDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数

            int ID = Request.QueryString["ID"] != null && int.TryParse(Request.QueryString["ID"].ToString(), out ID) ? ID : 0;
            ViewState["ID"] = ID;

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
                CM_Vehicle v = new CM_Vehicle();
                v.VehicleClassify = 2;
                v.State = 1;
                pl_detail.BindData(v);

                bt_Approve.Visible = false;
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        string condition = " Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() +
            " AND Dimission=1 AND Position IN (1030,1050)";
        cbx_Staff.DataSource = Org_StaffBLL.GetStaffList(condition);
        cbx_Staff.DataBind();

        DropDownList ddl_RelateStaff = (DropDownList)pl_detail.FindControl("CM_Vehicle_RelateStaff");
        if (ddl_RelateStaff != null)
        {
            ddl_RelateStaff.DataTextField = "RealName";
            ddl_RelateStaff.DataValueField = "ID";
            ddl_RelateStaff.DataSource = Org_StaffBLL.GetStaffList(condition);
            ddl_RelateStaff.DataBind();
            ddl_RelateStaff.Items.Insert(0, new ListItem("请选择...", "0"));
        }
    }
    #endregion

    private void BindData()
    {
        CM_Vehicle m = new CM_VehicleBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            if (m.Client != (int)Session["OwnerClient"])
            {
                Response.Redirect("~/SubModule/Desktop.aspx");
            }

            pl_detail.BindData(m);
            if (m.ApproveFlag == 1) bt_Approve.Visible = false;

            foreach (Org_Staff staff in CM_VehicleInStaffBLL.GetStaffByVehicle(m.ID))
            {
                ListItem item = cbx_Staff.Items.FindByValue(staff.ID.ToString());
                if (item != null) item.Selected = true;
            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_VehicleBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new CM_VehicleBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new CM_VehicleBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.VehicleClassify == 0)
        {
            MessageBox.Show(Page, "车辆类别必选");
            return;
        }
        if (_bll.Model.State == 0)
        {
            MessageBox.Show(Page, "车牌状态必选");
            return;
        }

        #endregion
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {

            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.Client = (int)Session["OwnerClient"];
            _bll.Model.ApproveFlag = 2;
            ViewState["ID"] = _bll.Add();

            if ((int)ViewState["ID"] > 0)
            {

            }
        }

        if (_bll.Model.RelateStaff > 0)
        {
            ListItem item = cbx_Staff.Items.FindByValue(_bll.Model.RelateStaff.ToString());
            if (item != null && !item.Selected) item.Selected = true;
        }

        foreach (ListItem item in cbx_Staff.Items)
        {
            IList<CM_VehicleInStaff> lists = CM_VehicleInStaffBLL.GetModelList("Vehicle=" + ViewState["ID"].ToString() + " AND Staff=" + item.Value);
            if (item.Selected)
            {
                if (lists.Count == 0)
                {
                    CM_VehicleInStaffBLL b = new CM_VehicleInStaffBLL();
                    b.Model.Vehicle = (int)ViewState["ID"];
                    b.Model.Staff = int.Parse(item.Value);
                    b.Model.InsertStaff = (int)Session["UserID"];
                    b.Add();
                }
            }
            else
            {
                if (lists.Count > 0)
                {
                    CM_VehicleInStaffBLL b = new CM_VehicleInStaffBLL(lists[0].ID);
                    b.Delete();
                }
            }
        }

        if (_bll.Model.ApproveFlag == 2)
            Response.Redirect("VehicleDetail.aspx?ID=" + ViewState["ID"].ToString());
        else
            Response.Redirect("VehicleList.aspx");
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        int ID = ViewState["ID"] != null && int.TryParse(ViewState["ID"].ToString(), out ID) ? ID : 0;
        CM_VehicleBLL _VehicleBLL = new CM_VehicleBLL(ID);
        if (ID == 0 || _VehicleBLL.Model == null | _VehicleBLL.Model.ApproveFlag == 1)
        {
            MessageBox.Show(Page, "审核失败");
            return;
        }


        CM_WareHouseBLL _WareHouseBLL = new CM_WareHouseBLL();
        _WareHouseBLL.Model.Client = (int)Session["OwnerClient"];
        _WareHouseBLL.Model.Code = _VehicleBLL.Model.VehicleNo;
        _WareHouseBLL.Model.Name = _VehicleBLL.Model.VehicleNo + "-仓库";
        _WareHouseBLL.Model.OfficialCity = new CM_ClientBLL((int)Session["OwnerClient"]).Model.OfficialCity;
        _WareHouseBLL.Model.Classify = 3;       //车仓库
        _WareHouseBLL.Model.RelateVehicle = ID;
        _WareHouseBLL.Model.ActiveState = 1;
        _WareHouseBLL.Model.InsertStaff = (int)Session["UserID"];
        int warehouse = _WareHouseBLL.Add();

        if (warehouse > 0)
        {
            _VehicleBLL.Model.RelateWareHouse = warehouse;
            _VehicleBLL.Model.ApproveFlag = 1;
            _VehicleBLL.Model.UpdateStaff = (int)Session["UserID"];
            _VehicleBLL.Update();
            MessageBox.ShowAndRedirect(this, "审核成功!", "VehicleList.aspx");
        }
    }
}