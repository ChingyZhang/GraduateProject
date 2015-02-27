using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.FNA;
using MCSFramework.Model.OA;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;

public partial class SubModule_FNA_FeeWriteoff_Pop_EvectionRouteDetail : System.Web.UI.Page
{
    protected DropDownList ddl_Transport, ddl_CarID;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 获取界面控件
        ddl_Transport = (DropDownList)pn_detail.FindControl("FNA_EvectionRoute_Transport");
        if (ddl_Transport != null)
        {
            ddl_Transport.AutoPostBack = true;
            ddl_Transport.SelectedIndexChanged += new EventHandler(ddl_Transport_SelectedIndexChanged);
        }

        ddl_CarID = (DropDownList)pn_detail.FindControl("Car_DispatchRide_CarID");
        if (ddl_CarID != null)
        {
            ddl_CarID.AutoPostBack = true;
            ddl_CarID.SelectedIndexChanged += new EventHandler(ddl_CarID_SelectedIndexChanged);
        }
        #endregion

        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            BindDropDown();
            if ((int)ViewState["ID"] != 0)
            {
                BindData();
            }
            else
            {
                Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
                MCSSelectControl select_Staff = (MCSSelectControl)pn_detail.FindControl("FNA_EvectionRoute_RelateStaff");
                select_Staff.SelectText = staff.Model.RealName;
                select_Staff.SelectValue = staff.Model.ID.ToString();

                TextBox tbx_BeginDate = (TextBox)pn_detail.FindControl("FNA_EvectionRoute_BeginDate");
                if (tbx_BeginDate != null) tbx_BeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                TextBox tbx_EndDate = (TextBox)pn_detail.FindControl("FNA_EvectionRoute_EndDate");
                if (tbx_EndDate != null) tbx_EndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                bt_Delete.Visible = false;
                pn_detail.SetPanelVisible("Panel_OA_Car_DispatchRide_ByEvectionRoute", false);
            }
        }
    }

    void ddl_CarID_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region 获取该车辆最后行程公里数
        int carID = int.Parse(ddl_CarID.SelectedValue);
        if (carID > 0)
        {
            Car_CarListBLL carBll = new Car_CarListBLL(carID);

            TextBox tbx_KilometresStart = (TextBox)pn_detail.FindControl("Car_DispatchRide_KilometresStart");
            if (tbx_KilometresStart != null)
            {
                if ((int)ViewState["ID"] == 0) tbx_KilometresStart.Text = carBll.Model.Kilometres.ToString();

                if (tbx_KilometresStart.Text != "" && tbx_KilometresStart.Text != "0")
                    tbx_KilometresStart.Enabled = false;
                else
                    tbx_KilometresStart.Enabled = true;
            }

            if (carBll.Model["CarUsage"] == "5")
            {
                //自备车，不可录入费用信息
                if (pn_detail.FindControl("Car_DispatchRide_RoadToll") != null)
                    ((TextBox)pn_detail.FindControl("Car_DispatchRide_RoadToll")).Enabled = false;
                if (pn_detail.FindControl("Car_DispatchRide_FuelFee") != null)
                    ((TextBox)pn_detail.FindControl("Car_DispatchRide_FuelFee")).Enabled = false;
                if (pn_detail.FindControl("Car_DispatchRide_ParkingFee") != null)
                    ((TextBox)pn_detail.FindControl("Car_DispatchRide_ParkingFee")).Enabled = false;
                if (pn_detail.FindControl("Car_DispatchRide_OtherFee") != null)
                    ((TextBox)pn_detail.FindControl("Car_DispatchRide_OtherFee")).Enabled = false;
            }
        }
        #endregion
    }

    void ddl_Transport_SelectedIndexChanged(object sender, EventArgs e)
    {
        pn_detail.SetPanelVisible("Panel_OA_Car_DispatchRide_ByEvectionRoute", ddl_Transport.SelectedValue == "4");
    }

    private void BindDropDown()
    {
        int organizecity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
        ddl_CarID.DataTextField = "CarNo";
        ddl_CarID.DataValueField = "ID";

        ddl_CarID.DataSource = Car_CarListBLL.GetCarListByOrganizeCity(organizecity).OrderBy(p => p.CarNo).ToList();
        ddl_CarID.DataBind();
        ddl_CarID.Items.Insert(0, new ListItem("请选择", "0"));
    }

    #region 绑定行程单详细信息
    private void BindData()
    {
        int id = (int)ViewState["ID"];

        FNA_EvectionRoute m = new FNA_EvectionRouteBLL(id).Model;
        if (m != null)
        {
            pn_detail.BindData(m);
            ddl_Transport_SelectedIndexChanged(null, null);

            if (m["RelateJournal"] == "")
                bt_ToJournal.Visible = false;
            else
                ViewState["RelateJournal"] = m["RelateJournal"];

            if (m.Transport == "4" && m["RelateCarDispatch"] != "")
            {
                int dispatch = 0;
                if (int.TryParse(m["RelateCarDispatch"], out dispatch) && dispatch > 0)
                {
                    Car_DispatchRide d = new Car_DispatchRideBLL(dispatch).Model;
                    if (d != null)
                    {
                        pn_detail.BindData(d);
                        ddl_CarID_SelectedIndexChanged(null, null);
                    }
                }
            }

            if (m.WriteOffID > 0 || m.InsertStaff != (int)Session["UserID"] || m.ApproveFlag == 1)
            {
                pn_detail.Enabled = false;
                bt_Delete.Visible = false;
                bt_Save.Visible = false;
            }
        }
    }
    #endregion

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        FNA_EvectionRouteBLL bll;
        Car_DispatchRideBLL car_ridebll = null;

        if ((int)ViewState["ID"] == 0)
            bll = new FNA_EvectionRouteBLL();
        else
            bll = new FNA_EvectionRouteBLL((int)ViewState["ID"]);

        pn_detail.GetData(bll.Model);

        #region 保存车辆使用信息
        if (bll.Model.Transport == "4")
        {
            //自驾车时，关联派车单
            if (string.IsNullOrEmpty(bll.Model["RelateCarDispatch"]))
            {
                car_ridebll = new Car_DispatchRideBLL();
                pn_detail.GetData(car_ridebll.Model);
                if (car_ridebll.Model.KilometresEnd < car_ridebll.Model.KilometresStart)
                {
                    MessageBox.Show(this, "对不起，还车公里数不能小于发车公里数!");
                    return;
                }

                car_ridebll.Model.Destination = bll.Model.EvectionLine;
                car_ridebll.Model.Matters = "差旅行程";
                car_ridebll.Model.State = 3; //已交车
                car_ridebll.Model.OrganizeCity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
                car_ridebll.Model.ActGoBackTime = DateTime.Now;
                car_ridebll.Model.ApproveFlag = 2;
                car_ridebll.Model.ApplyStaff = bll.Model.RelateStaff;
                car_ridebll.Model.InsertStaff = (int)Session["UserID"];

                bll.Model["RelateCarDispatch"] = car_ridebll.Add().ToString();
            }
            else
            {
                car_ridebll = new Car_DispatchRideBLL(int.Parse(bll.Model["RelateCarDispatch"]));
                pn_detail.GetData(car_ridebll.Model);
                if (car_ridebll.Model.KilometresEnd < car_ridebll.Model.KilometresStart)
                {
                    MessageBox.Show(this, "对不起，还车公里数不能小于发车公里数!");
                    return;
                }

                car_ridebll.Model.Destination = bll.Model.EvectionLine;
                car_ridebll.Model.UpdateStaff = (int)Session["UserID"];
                car_ridebll.Update();
            }

            //更新车辆最后的公里数
            Car_CarListBLL CarBll = new Car_CarListBLL(car_ridebll.Model.CarID);
            if (CarBll.Model != null)
            {
                if (Car_DispatchRideBLL.GetModelList("CarID=" + car_ridebll.Model.CarID.ToString() + 
                    " AND ID > " + car_ridebll.Model.ID.ToString()).Count == 0)
                {
                    //如果当前用车信息是该车辆最后一次用车记录，则更新车辆最后公里数
                    CarBll.Model.Kilometres = car_ridebll.Model.KilometresEnd;
                    CarBll.Update();
                }
            }
        }
        #endregion

        if ((int)ViewState["ID"] == 0)
        {
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = bll.Add();
        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();
        }

        if (car_ridebll != null && car_ridebll.Model["RelateEvectionRoute"] == "")
        {
            car_ridebll.Model["RelateEvectionRoute"] = ViewState["ID"].ToString();
            car_ridebll.Update();
        }

        if (sender != null)
            MessageBox.ShowAndClose(this, "保存成功！");
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            FNA_EvectionRouteBLL bll = new FNA_EvectionRouteBLL((int)ViewState["ID"]);
            bll.Delete();
            MessageBox.ShowAndClose(this, "删除成功！");
        }
    }
    protected void bt_ToJournal_Click(object sender, EventArgs e)
    {
        if (ViewState["RelateJournal"] != null)
            Response.Redirect("~/SubModule/OA/Journal/JournalDetail.aspx?ID=" + ViewState["RelateJournal"]);
    }
}
