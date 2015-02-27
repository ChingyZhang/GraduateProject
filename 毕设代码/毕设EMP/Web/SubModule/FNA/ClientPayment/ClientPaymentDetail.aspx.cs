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
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_FNA_ClientPayment_ClientPaymentDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList ddl_PayType = UC_DetailView1.FindControl("FNA_ClientPaymentInfo_PayType") == null ? null : (DropDownList)UC_DetailView1.FindControl("FNA_ClientPaymentInfo_PayType");
        if (ddl_PayType != null)
        {
            ddl_PayType.SelectedIndexChanged += new EventHandler(ddl_PayType_SelectedIndexChanged);
            ddl_PayType.AutoPostBack = true;
        }
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] == 0)
            {
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                    Session["ClientID"] = ViewState["ClientID"];
                }
                else if (Session["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
                }

                if (ViewState["ClientID"] != null)
                {
                    CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                    MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("FNA_ClientPaymentInfo_Client");
                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = client.Model.FullName;

                    select_Client.Enabled = false;
                }

                TextBox tbx_ConfirmDate = (TextBox)UC_DetailView1.FindControl("FNA_ClientpaymentInfo_ConfirmDate");
                bt_Approve.Visible = false;
                btn_CanclePass.Visible = false;
                UploadFile1.Visible = false;
            }
            else
            {
                BindData();
            }          
            
        }
      
    }
    private void ddl_PayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_PayType = (DropDownList)sender;
        DropDownList ddl_AmountType = UC_DetailView1.FindControl("FNA_ClientPaymentInfo_AmountType") == null ? null : (DropDownList)UC_DetailView1.FindControl("FNA_ClientPaymentInfo_AmountType");
        IList<Dictionary_Data> amounttypelist = DictionaryBLL.Dictionary_Data_GetAlllList("Type=720 AND Description='" + ddl_PayType.SelectedValue + "'");
        ddl_AmountType.Items.Clear();
       foreach(Dictionary_Data amount in amounttypelist)
       {
           ddl_AmountType.Items.Add(new ListItem(amount.Name,amount.Code));
       }
        ddl_AmountType.Items.Insert(0, new ListItem("所有", "0"));
        ddl_AmountType.SelectedValue =(int)ViewState["ID"]!=0? new FNA_ClientPaymentInfoBLL((int)ViewState["ID"]).Model["AmountType"]:"0";
    }
    private void BindData()
    {
        FNA_ClientPaymentInfo m = new FNA_ClientPaymentInfoBLL((int)ViewState["ID"]).Model;
        UploadFile1.RelateID = (int)ViewState["ID"];
        UploadFile1.BindGrid();
        UC_DetailView1.BindData(m);

        if (m.ApproveFlag == 1)
        {
            UC_DetailView1.SetControlsEnable(false);
            bt_Save.Visible = false;
            bt_Approve.Visible = false;
            UploadFile1.CanDelete = false;         
        }
        else
        {
            btn_CanclePass.Visible = false;
           
        }
         DropDownList ddl_PayType = UC_DetailView1.FindControl("FNA_ClientPaymentInfo_PayType") == null ? null : (DropDownList)UC_DetailView1.FindControl("FNA_ClientPaymentInfo_PayType");
         if (ddl_PayType != null)
         {
             ddl_PayType_SelectedIndexChanged(ddl_PayType, null);
         }

    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        FNA_ClientPaymentInfoBLL bll;
        if ((int)ViewState["ID"] == 0)
            bll = new FNA_ClientPaymentInfoBLL();
        else
            bll = new FNA_ClientPaymentInfoBLL((int)ViewState["ID"]);

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
            ViewState["ID"] = bll.Add();
        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();
        }

        if (sender != null)
            Response.Redirect("ClientPaymentDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        FNA_ClientPaymentInfoBLL bll;
        if ((int)ViewState["ID"] > 0)
        {
            bt_Save_Click(null, null);

            TextBox tbx_ConfirmDate =UC_DetailView1.FindControl("FNA_ClientpaymentInfo_ConfirmDate")==null ?null: (TextBox)UC_DetailView1.FindControl("FNA_ClientpaymentInfo_ConfirmDate");

            DateTime confirmdate = DateTime.Now;
            if (tbx_ConfirmDate!=null&&!DateTime.TryParse(tbx_ConfirmDate.Text, out confirmdate))
            {  
                MessageBox.Show(this, "确认到账日期必需按正确日期格式yyyy-mm-dd填写！");
                return;
            }
            bll = new FNA_ClientPaymentInfoBLL((int)ViewState["ID"]);
            bll.Confirm((int)Session["UserID"], confirmdate);

            Response.Redirect("ClientPaymentList.aspx?ClientID=" + bll.Model.Client.ToString());
        }
    }
    protected void btn_CanclePass_Click(object sender, EventArgs e)
    {
        FNA_ClientPaymentInfoBLL bll;
        if ((int)ViewState["ID"] > 0)
        {
            bt_Save_Click(null, null);
            bll = new FNA_ClientPaymentInfoBLL((int)ViewState["ID"]);
            bll.CancleConfirm((int)Session["UserID"], DateTime.Now);
            Response.Redirect("ClientPaymentList.aspx?ClientID=" + bll.Model.Client.ToString());
        }

    }
}
