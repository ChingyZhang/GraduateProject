using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Model.CM;

public partial class SubModule_CM_Map_ClientInMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);

            if ((int)ViewState["ClientID"] == 0 && int.Parse(Session["AccountType"].ToString()) == 2)
            {
                ViewState["ClientID"] = Int32.Parse(Session["UserID"].ToString());
            }

            if (!Right_Assign_BLL.GetAccessRight((string)Session["Username"], 11, "SetMapPosition") &&
                !Right_Assign_BLL.GetAccessRight((string)Session["Username"], 1101, "SetMapPosition"))
            {
                btn_snyc.Visible = false;
                btn_addpoint.Visible = false;
                btn_OK.Visible = false;
            }
        }
        CM_ClientBLL cm_client = new CM_ClientBLL((int)ViewState["ClientID"]);
        if (cm_client.Model != null)
        {
            CM_ClientGeoInfo info = CM_ClientGeoInfoBLL.GetGeoInfoByClient((int)ViewState["ClientID"]);
            if (info != null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "addComplexMarker(" + info.Longitude + ", " + info.Latitude + ",\"m" + cm_client.Model.ID + "\",\"" + cm_client.Model.FullName + "\",\"" + cm_client.Model.TeleNum + "\",\"" + cm_client.Model.Address + "\");mapInit();initmethod(\"m" + cm_client.Model.ID + "\",\"" + cm_client.Model.FullName + "\",\"" + cm_client.Model.TeleNum + "\",\"" + cm_client.Model.Address + "\")", true);
                ViewState["InfoID"] = info.ID;
            }
            else if (lngX.Value != "" && latY.Value != "")
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "addComplexMarker(" + lngX.Value + ", " + latY.Value + ",\"m" + cm_client.Model.ID + "\",\"" + cm_client.Model.FullName + "\",\"" + cm_client.Model.TeleNum + "\",\"" + cm_client.Model.Address + "\");mapInit();initmethod(\"m" + cm_client.Model.ID + "\",\"" + cm_client.Model.FullName + "\",\"" + cm_client.Model.TeleNum + "\",\"" + cm_client.Model.Address + "\")", true);
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "mapInit();initmethod(\"m" + cm_client.Model.ID + "\",\"" + cm_client.Model.FullName + "\",\"" + cm_client.Model.TeleNum + "\",\"" + cm_client.Model.Address + "\")", true);
        }
        else
            MessageBox.ShowAndClose(this, "请选择门店！");
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        if (lngX.Value == "" || latY.Value == "")
        {
            MessageBox.Show(this, "没有获取到定位点，请重新选择定位点！");
            return;
        }
        CM_ClientGeoInfoBLL bll;
        int ret;
        if (ViewState["InfoID"] != null)
        {
            bll = new CM_ClientGeoInfoBLL((int)ViewState["InfoID"]);
            bll.Model.UpdateUser = (Guid)Session["aspnetUserId"];
            bll.Model.UpdateTime = DateTime.Now;
        }
        else
        {
            bll = new CM_ClientGeoInfoBLL();
            bll.Model.InsertTime = DateTime.Now;
            bll.Model.InsertUser = (Guid)Session["aspnetUserId"];
        }
        bll.Model.Longitude = decimal.Parse(lngX.Value);
        bll.Model.Latitude = decimal.Parse(latY.Value);
        bll.Model.Client = (int)ViewState["ClientID"];
        if (ViewState["InfoID"] != null)
            ret = bll.Update();
        else
            ret = bll.Add();
        if (ret >= 0)
            MessageBox.ShowAndClose(this, "更改成功！");
    }
}