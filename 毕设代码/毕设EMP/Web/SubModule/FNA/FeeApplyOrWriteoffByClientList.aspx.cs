using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Data;
using MCSFramework.BLL.CM;
using MCSFramework.Common;

public partial class SubModule_FNA_FeeApplyOrWriteoffByClientList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]); //客户类型，２：经销商，３：终端门店
            #endregion

            BindDropDown();

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
                if (Request.QueryString["ClientType"] != null && client.Model.ClientType != (int)ViewState["ClientType"])
                {
                    Session["ClientID"] = null;
                    Response.Redirect(Request.Url.PathAndQuery);
                }
                ViewState["ClientType"] = client.Model.ClientType;

                select_Client.SelectValue = client.Model.ID.ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString();

                switch (client.Model.ClientType)
                {
                    case 2:
                        lb_ClientInfo.NavigateUrl = "~/SubModule/CM/DI/DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString();
                        break;
                    case 3:
                        lb_ClientInfo.NavigateUrl = "~/SubModule/CM/RT/RetailerDetail.aspx?ClientID=" + ViewState["ClientID"].ToString();
                        break;
                    case 5:
                        lb_ClientInfo.NavigateUrl = "~/SubModule/CM/Hospital/HospitalDetail.aspx?ClientID=" + ViewState["ClientID"].ToString();
                        break;
                    case 6:
                        lb_ClientInfo.NavigateUrl = "~/SubModule/CM/PD/PropertyDetail.aspx?ClientID=" + ViewState["ClientID"].ToString();
                        break;
                    default:
                        lb_ClientInfo.Visible = false;
                        break;
                }
                
                BindGrid();
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "../CM/RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name);
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));
        //ddl_AccountTitle.Items.Insert(0, new ListItem("全部", "0"));
    }
    #endregion

    private void BindGrid()
    {
        if (select_Client.SelectValue != "")
        {
            DataTable dt = FNA_FeeApplyBLL.GetFeeApplyOrWriteoffByClient(int.Parse(select_Client.SelectValue), int.Parse(ddl_BeginMonth.SelectedValue), int.Parse(ddl_EndMonth.SelectedValue));

            decimal _SumAppCost = 0, _SumAvailCost = 0, _SumWriteOffCost = 0;
            int _PreApplyDetailID = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (_PreApplyDetailID != (int)dt.Rows[i]["ApplyDetailID"] && (int)dt.Rows[i]["ApplyState"]==3)
                {
                    if (dt.Rows[i]["sumApplyCost"].ToString() != "")
                        _SumAppCost += (decimal)dt.Rows[i]["sumApplyCost"];

                    if (dt.Rows[i]["AvailCost"].ToString() != "")
                        _SumAvailCost += (decimal)dt.Rows[i]["AvailCost"];

                    _PreApplyDetailID = (int)dt.Rows[i]["ApplyDetailID"];
                }
                if (dt.Rows[i]["sumWriteOffCost"].ToString() != "")
                    _SumWriteOffCost += (decimal)dt.Rows[i]["sumWriteOffCost"];
            }
            DataRow dr = dt.NewRow();
            dr["FeeType"] = "合计(仅批复通过)";
            dr["sumApplyCost"] = _SumAppCost;
            dr["AvailCost"] = _SumAvailCost;
            dr["sumWriteOffCost"] = _SumWriteOffCost;
            dt.Rows.Add(dr);
            DataView dv = dt.DefaultView;
            if (ddl_FeeType.SelectedValue != "0")
            {
                dv.RowFilter = " FeeType='" + ddl_FeeType.SelectedItem.ToString() + "'";
            }
            
            gv_List.DataSource = dv;
            gv_List.DataBind();

        }

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }  
}
