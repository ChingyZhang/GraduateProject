using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.BLL.Pub;

public partial class SubModule_CM_DI_KPIDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
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
            }
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
                BindData();
            else
            {
                if (ViewState["ClientID"] != null)
                {
                    MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("CM_KPI_Client");
                    CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = client.Model.FullName;
                    //select_Client.Enabled = false;
                }

                DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("CM_KPI_AccountMonth");
                ddl_Month.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();

                bt_Approve.Visible = false;
                bt_Delete.Visible = false;

            }
        }
        #region 增加计算合计得分按钮
        if (bt_OK.Visible)
        {
            Label lb_TotalScore = (Label)UC_DetailView1.FindControl("CM_KPI_TotalScore");
            Button bt_Compute = new Button();
            bt_Compute.ID = "bt_Compute";
            bt_Compute.Text = "计算总分";
            bt_Compute.Click += new EventHandler(bt_Compute_Click);
            lb_TotalScore.Parent.Controls.Add(bt_Compute);
        }
        #endregion
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }

    void ddl_ActiveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_ActiveFlag = (DropDownList)UC_DetailView1.FindControl("CM_Client_ActiveFlag");
        if (ddl_ActiveFlag.SelectedValue == "2")
        {
            ((TextBox)UC_DetailView1.FindControl("CM_Client_CloseTime")).Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            ((TextBox)UC_DetailView1.FindControl("CM_Client_CloseTime")).Text = "";
        }
    }
    #endregion

    private void BindData()
    {
        CM_KPI m = new CM_KPIBLL((int)ViewState["ID"]).Model;
        UC_DetailView1.BindData(m);

        if (m.ApproveFlag == 1)
        {
            bt_OK.Visible = false;
            bt_Approve.Visible = false;
            bt_Delete.Visible = false;
            UC_DetailView1.SetControlsEnable(false);
        }
        else
        {
            //#region 增加计算合计得分按钮
            //Label lb_TotalScore = (Label)UC_DetailView1.FindControl("CM_KPI_TotalScore");
            //Button bt_Compute = new Button();
            //bt_Compute.ID = "bt_Compute";
            //bt_Compute.Text = "计算总分";
            //bt_Compute.Click += new EventHandler(bt_Compute_Click);
            //lb_TotalScore.Parent.Controls.Add(bt_Compute);
            //#endregion
        }
    }

    void bt_Compute_Click(object sender, EventArgs e)
    {
        decimal score = ComputeTotalScore();
        Label lb = (Label)UC_DetailView1.FindControl("CM_KPI_TotalScore");
        lb.Text = score.ToString();
        
    }

    private decimal ComputeTotalScore()
    {
        decimal score = 0;
        for (int i = 1; i <= 6; i++)
        {
            TextBox tbx = (TextBox)UC_DetailView1.FindControl("CM_KPI_KPI" + i.ToString());

            if (tbx.Text != "")
            {
                score += decimal.Parse(tbx.Text);
            }
        }

        return score;
    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_KPIBLL _bll = null;
        if ((int)ViewState["ID"] == 0)
            _bll = new CM_KPIBLL();
        else
            _bll = new CM_KPIBLL((int)ViewState["ID"]);

        UC_DetailView1.GetData(_bll.Model);
        _bll.Model.TotalScore = ComputeTotalScore();

        #region 判断必填项
        if (_bll.Model.Client == 0)
        {
            MessageBox.Show(this, "经销商必填!");
            return;
        }
        #endregion


        if ((int)ViewState["ID"] == 0)
        {
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.ApproveFlag = 2;
            ViewState["ID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }

        MessageBox.ShowAndRedirect(this, "保存经销商KPI考核资料成功！", "KPIList.aspx?ClientID=" + _bll.Model.Client.ToString());

    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            CM_KPIBLL _bll = new CM_KPIBLL((int)ViewState["ID"]);
            _bll.Model.ApproveFlag = 1;
            _bll.Update();
            MessageBox.ShowAndRedirect(this, "审核经销商KPI资料成功！", "KPIList.aspx?ClientID=" + _bll.Model.Client.ToString());
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            CM_KPIBLL _bll = new CM_KPIBLL((int)ViewState["ID"]);
            _bll.Delete();
            MessageBox.ShowAndRedirect(this, "删除经销商KPI资料成功！", "KPIList.aspx");
        }
    }
}
