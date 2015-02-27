// ===================================================================
// 文件路径:SubModule/OA/Car/Car_DispatchRideDetail.aspx.cs 
// 生成日期:2011/6/20 8:12:25 
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
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;

public partial class SubModule_OA_Car_Car_DispatchRideDetail : System.Web.UI.Page
{
    private DropDownList ddl_CarID;

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
                TextBox tbx_PlanGoOutTime = (TextBox)pl_detail.FindControl("Car_DispatchRide_PlanGoOutTime");
                TextBox tbx_PlanGoBackTime = (TextBox)pl_detail.FindControl("Car_DispatchRide_PlanGoBackTime");

                if (tbx_PlanGoOutTime != null) tbx_PlanGoOutTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                if (tbx_PlanGoBackTime != null) tbx_PlanGoBackTime.Text = DateTime.Now.ToString("yyyy-MM-dd 18:00");
                pl_detail.SetPanelVisible("Panel_OA_Car_DispatchRide_Detail_02", false);
                pl_detail.SetPanelVisible("Panel_OA_Car_DispatchRide_Detail_03", false);
            }
        }

        ddl_CarID = (DropDownList)pl_detail.FindControl("Car_DispatchRide_CarID");
        if (ddl_CarID != null)
        {
            ddl_CarID.AutoPostBack = true;
            ddl_CarID.SelectedIndexChanged += new EventHandler(ddl_CarID_SelectedIndexChanged);
        }
    }

    void ddl_CarID_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox tbx_KilometresStart = (TextBox)pl_detail.FindControl("Car_DispatchRide_KilometresStart");
        if (tbx_KilometresStart != null)
        {
            int id = int.Parse(ddl_CarID.SelectedValue);
            if (id > 0)
            {
                Car_CarList c = new Car_CarListBLL(id).Model;
                if (c != null)
                    tbx_KilometresStart.Text = c.Kilometres.ToString();
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
        Car_DispatchRide m = new Car_DispatchRideBLL((int)ViewState["ID"]).Model;
        if (m != null) pl_detail.BindData(m);


        switch (m.State)
        {
            case 1:     //备车中
                if (m.ApproveFlag == 1)
                {
                    pl_detail.SetPanelEnable("Panel_OA_Car_DispatchRide_Detail_01", false);
                    bt_Save.Visible = false;
                    bt_ApproveFlag.Visible = false;
                }
                else
                {
                    pl_detail.SetPanelVisible("Panel_OA_Car_DispatchRide_Detail_02", false);
                    bt_GoOut.Visible = false;
                }

                bt_GoBack.Visible = false;
                pl_detail.SetPanelVisible("Panel_OA_Car_DispatchRide_Detail_03", false);
                break;
            case 2:     //已发车
                pl_detail.SetPanelEnable("Panel_OA_Car_DispatchRide_Detail_01", false);
                bt_Save.Visible = false;
                bt_ApproveFlag.Visible = false;
                bt_GoOut.Visible = false;
                break;
            case 3:     //已还车
            case 4:     //已取消
                pl_detail.SetControlsEnable(false);
                bt_Save.Visible = false;
                bt_ApproveFlag.Visible = false;
                bt_GoBack.Visible = false;
                bt_GoOut.Visible = false;
                bt_Cancel.Visible = false;
                break;
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Car_DispatchRideBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new Car_DispatchRideBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Car_DispatchRideBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "Car_DispatchRideList.aspx");
            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.State = 1;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.ApplyStaff = (int)Session["UserID"];

            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                if (_bll.Model.DispatchingCode == "")
                {
                    _bll.Model.DispatchingCode = ViewState["ID"].ToString();
                    _bll.Update();
                }
                MessageBox.ShowAndRedirect(this, "新增成功!", "Car_DispatchRideList.aspx?State=1");
            }
        }

    }

    protected void bt_ApproveFlag_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Car_DispatchRideBLL _bll = new Car_DispatchRideBLL((int)ViewState["ID"]);
            _bll.Model.ApproveFlag = 1;
            _bll.Update();

            MessageBox.ShowAndRedirect(this, "审核成功!", "Car_DispatchRideList.aspx?State=1");
        }
    }
    protected void bt_GoOut_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Car_DispatchRide m = new Car_DispatchRide();
            pl_detail.GetData(m);

            if (m.CarID == 0)
            {
                MessageBox.Show(this, "请选择车辆!");
                return;
            }

            if (m.DriverStaff == 0)
            {
                MessageBox.Show(this, "请选择驾驶员!");
                return;
            }

            Car_DispatchRideBLL _bll = new Car_DispatchRideBLL((int)ViewState["ID"]);
            _bll.GoOut(m.CarID, m.DriverStaff, m.KilometresStart, (int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "确认出车成功!", "Car_DispatchRideList.aspx?State=2");
        }
    }
    protected void bt_GoBack_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Car_DispatchRide m = new Car_DispatchRide();
            pl_detail.GetData(m);

            if (m.KilometresEnd == 0)
            {
                MessageBox.Show(this, "请输入还车时公里数!");
                return;
            }

            if (m.KilometresEnd < m.KilometresStart)
            {
                MessageBox.Show(this, "还车时公里数不能小于发车时公里数!");
                return;
            }

            Car_DispatchRideBLL _bll = new Car_DispatchRideBLL((int)ViewState["ID"]);
            _bll.GoBack(m.KilometresEnd, (int)Session["UserID"], m.RoadToll, m.FuelFee, m.ParkingFee, m.OtherFee);

            MessageBox.ShowAndRedirect(this, "确认还车成功!", "Car_DispatchRideList.aspx?State=3");
        }
    }
    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Car_DispatchRideBLL _bll = new Car_DispatchRideBLL((int)ViewState["ID"]);
            _bll.Model.State = 4;
            _bll.Model.ApproveFlag = 1;
            _bll.Update();

            MessageBox.ShowAndRedirect(this, "派车单取消成功!", "Car_DispatchRideList.aspx?State=1");
        }
    }
}