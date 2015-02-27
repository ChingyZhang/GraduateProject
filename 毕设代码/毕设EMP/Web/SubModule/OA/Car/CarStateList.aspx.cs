using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;

public partial class SubModule_OA_Car_CarStateList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string condition = "";
        if (MCSTabControl1.SelectedIndex == 0)
        {
            condition = "Car_CarList.State = 1 AND Car_CarList.ID NOT IN (SELECT CarID FROM MCS_OA.dbo.Car_DispatchRide WHERE Car_DispatchRide.State = 2)";
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
        }
        else
        {
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = true;
            condition = "Car_CarList.State = 1 AND Car_CarList.ID IN (SELECT CarID FROM MCS_OA.dbo.Car_DispatchRide WHERE Car_DispatchRide.State = 2)";
        }
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }


    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];

        Response.Redirect("CarDetail.aspx?ID=" + id.ToString());
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 1 && e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hy_DispatchRide = (HyperLink)e.Row.FindControl("hy_DispatchRide");
            if (hy_DispatchRide != null)
            {
                int carid = (int)gv_List.DataKeys[e.Row.RowIndex][0];
                IList<Car_DispatchRide> lists = Car_DispatchRideBLL.GetModelList("Car_DispatchRide.State = 2 AND Car_DispatchRide.CarID=" + carid.ToString());

                if (lists.Count > 0)
                {
                    Car_DispatchRide m = lists[lists.Count - 1];

                    hy_DispatchRide.Text = m.Destination;
                    hy_DispatchRide.NavigateUrl = "Car_DispatchRideDetail.aspx?ID=" + m.ID.ToString();
                }
            }
        }
    }
}