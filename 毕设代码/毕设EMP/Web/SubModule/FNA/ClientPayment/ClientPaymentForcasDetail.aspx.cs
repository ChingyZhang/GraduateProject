using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSControls.MCSWebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Common;

public partial class SubModule_FNA_ClientPayment_ClientPaymentForcasDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            if ((int)ViewState["ID"] == 0)
            {

                if (ViewState["ClientID"] != null)
                {
                    CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
                    MCSSelectControl select_Client = UC_DetailView1.FindControl("FNA_ClientPaymentForcast_Client") == null ? null : (MCSSelectControl)UC_DetailView1.FindControl("FNA_ClientPaymentForcast_Client");
                    if (select_Client != null)
                    {
                        select_Client.SelectValue = ViewState["ClientID"].ToString();
                        select_Client.SelectText = client.Model.FullName;
                        select_Client.Enabled = false;
                    }
                }
                bt_Approve.Visible = false;
                btn_CancleApprove.Visible = false;
            }
            else
            {
                BindData();
            }
        }

    }
    private void BindData()
    {
        FNA_ClientPaymentForcast m = new FNA_ClientPaymentForcastBLL((int)ViewState["ID"]).Model;

        UC_DetailView1.BindData(m);

        if (m.ApproveFlag == 1)
        {
            UC_DetailView1.SetControlsEnable(false);
            bt_Save.Visible = false;
            bt_Approve.Visible = false;
        }
        else
        {
            UC_DetailView1.SetControlsEnable(true);
            bt_Save.Visible = true;
            bt_Approve.Visible = true;
            btn_CancleApprove.Visible = false;
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        FNA_ClientPaymentForcastBLL bll;
        if ((int)ViewState["ID"] == 0)
            bll = new FNA_ClientPaymentForcastBLL();
        else
            bll = new FNA_ClientPaymentForcastBLL((int)ViewState["ID"]);

        UC_DetailView1.GetData(bll.Model);

        if (bll.Model.Client == 0)
        {
            MessageBox.Show(this, "对不起，请选择回款的经销商!");
            return;
        }
        if ((int)ViewState["ID"] == 0)
        {
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model.ApproveFlag = 2;
            bll.Add();
        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();
        }

        if (sender != null)
            Response.Redirect("ClientPaymentForcastList.aspx?ClientID=" + bll.Model.Client.ToString());
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ApproveData((int)ViewState["ID"], 1);
        }
    }
    protected void btn_CancleApprove_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ApproveData((int)ViewState["ID"],2);
        }
    }

    private void ApproveData(int id,int approveflag)
    {
        FNA_ClientPaymentForcastBLL bll = new FNA_ClientPaymentForcastBLL(id);
        bll.Model.ApproveFlag = approveflag;
        bll.Update();
        Response.Redirect("ClientPaymentForcastList.aspx?ClientID=" + bll.Model.Client.ToString());
    }
}
