using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;

public partial class SubModule_OA_Car_CarDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
        }
    }

    private void BindData()
    {
        Car_CarList m = new Car_CarListBLL((int)ViewState["ID"]).Model;
        pl_detail.BindData(m);
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Car_CarListBLL _bll = null;
        if ((int)ViewState["ID"] == 0)
        {
            _bll = new Car_CarListBLL();
        }
        else
        {
            _bll = new Car_CarListBLL((int)ViewState["ID"]);
        }

        pl_detail.GetData(_bll.Model);

        _bll.Model.CarNo = _bll.Model.CarNo.ToUpper();

        if ((int)ViewState["ID"] == 0)
        {
            ViewState["ID"] = _bll.Add();
        }
        else
        {
            _bll.Update();
        }
        Response.Redirect("CarList.aspx");
    }
}