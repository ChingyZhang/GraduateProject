using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CSO;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_CSO_CSO_SampleOfferDetail_GetByCondtion : System.Web.UI.Page
{
    private const int HDMSource = 20;        //营养教育来源
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取参数
            ViewState["OfferBalanceID"] = Request.QueryString["OfferBalanceID"] != null ? int.Parse(Request.QueryString["OfferBalanceID"].ToString()) : 0;
            ViewState["BeginTime"] = Request.QueryString["BeginTime"] != null ? Request.QueryString["BeginTime"].ToString() : "";
            ViewState["EndTime"] = Request.QueryString["EndTime"] != null ? Request.QueryString["EndTime"].ToString() : "";
            ViewState["Staff"] = Request.QueryString["Staff"] != null ? int.Parse(Request.QueryString["Staff"].ToString()) : 0;
            ViewState["Client"] = string.IsNullOrEmpty(Request.QueryString["Client"]) == true ? 0 : int.Parse(Request.QueryString["Client"].ToString());
            ViewState["OfferMan"] = string.IsNullOrEmpty(Request.QueryString["OfferMan"]) == true ? 0 : int.Parse(Request.QueryString["OfferMan"].ToString());
            ViewState["OfferMode"] = Request.QueryString["OfferMode"] != null ? int.Parse(Request.QueryString["OfferMode"].ToString()) : 0;
            ViewState["Source"] = Request.QueryString["Source"] != null ? int.Parse(Request.QueryString["Source"].ToString()) : 0;
            ViewState["Product"] = Request.QueryString["Product"] != null ? int.Parse(Request.QueryString["Product"]) : 0;
            ViewState["OfferModeName"] = "";
            ViewState["IsSummaryRow"] = Request.QueryString["IsSummaryRow"] != null ? Request.QueryString["IsSummaryRow"] : "N";

            #region 医务派发方式
            if (Request.QueryString["OfferModeName"] != null && (int)ViewState["OfferMode"] == 0)
            {
                ViewState["OfferModeName"] = Request.QueryString["OfferModeName"].ToString();
                string name = ViewState["OfferModeName"].ToString();

                Dictionary<string, Dictionary_Data> dic_offermode = DictionaryBLL.GetDicCollections("CSO_OfferMode");
                Dictionary_Data dic_data = dic_offermode.Values.FirstOrDefault(p => p.Name == name);
                if (dic_data != null)
                {
                    ViewState["OfferMode"] = int.Parse(dic_data.Code);
                }
            }
            #endregion
            
            #region 医生结算标准
            ViewState["DoctorStandard"] = 0;
            if (Request.QueryString["DoctorStandard"] != null)
            {
                string name = Request.QueryString["DoctorStandard"];

                Dictionary<string, Dictionary_Data> dic_standard = DictionaryBLL.GetDicCollections("CSO_DocStandard");
                Dictionary_Data dic_data = dic_standard.Values.FirstOrDefault(p => p.Name == name);
                if (dic_data != null) ViewState["DoctorStandard"] = int.Parse(dic_data.Code);
            }
            #endregion

            ViewState["ConfirmFlag"] = Request.QueryString["ConfirmFlag"] != null ? int.Parse(Request.QueryString["ConfirmFlag"].ToString()) : 0;
            ViewState["DataFlag"] = Request.QueryString["DataFlag"] != null ? int.Parse(Request.QueryString["DataFlag"].ToString()) : 0;
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] != null ? int.Parse(Request.QueryString["OrganizeCity"].ToString()) : 0;

            ViewState["PageIndex"] = 0;
            #endregion
            BindGrid();
        }
    }
    private void BindGrid()
    {
        gv_List.PageIndex = (int)ViewState["PageIndex"];

        gv_List.ConditionString = GetConditionString();
        gv_List.BindGrid();
    }
    /// <summary>
    /// 组合查询语句
    /// </summary>
    /// <returns></returns>
    private string GetConditionString()
    {
        string ConditionString = " 1=1";
        int offerbalanceid = (int)ViewState["OfferBalanceID"];
        string begintime = (string)ViewState["BeginTime"];
        string endtime = (string)ViewState["EndTime"];
        int acceptstaff = (int)ViewState["Staff"];
        int Client = (int)ViewState["Client"];
        int OfferMan = (int)ViewState["OfferMan"];
        int OfferMode = (int)ViewState["OfferMode"];
        int organizeCity = (int)ViewState["OrganizeCity"];
        int Source = (int)ViewState["Source"];
        int flag = (int)ViewState["ConfirmFlag"];
        int dataflag = (int)ViewState["DataFlag"];
        int product = (int)ViewState["Product"];

        if (offerbalanceid != 0) ConditionString += string.Format(" AND CSO_SampleOffer.OfferBalance={0}", offerbalanceid);

        if (begintime != "" && endtime != "")
        {
            if (dataflag != 2)
                ConditionString += string.Format(" AND CSO_SampleOffer.InsertTime between '{0}' AND '{1} 23:59:59'",
                    begintime, endtime);
            else
                ConditionString += string.Format(" AND CSO_SampleOffer.ConfirmDate between '{0}' AND '{1} 23:59:59'",
                    begintime, endtime);
        }

        if (organizeCity != 0 && organizeCity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizeCity);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += organizeCity;

            if (orgcitys != "")
                ConditionString += string.Format(" AND CSO_SampleOffer.OrganizeCity in({0})", orgcitys);
        }
        if (acceptstaff != 0)
            ConditionString += string.Format(" AND CSO_SampleOffer.TrackStaff={0}", acceptstaff);

        if (Source == HDMSource)
        {
            //医务渠道
            if (OfferMan != 0) ConditionString += string.Format(" AND CSO_SampleOffer.OfferMan={0}", OfferMan);
            if ((int)ViewState["DoctorStandard"] != 0)
            {
                ConditionString += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CSO.dbo.CSO_SampleOffer',CSO_SampleOffer.ExtPropertys,'DoctorStandard')='" + ViewState["DoctorStandard"].ToString() + "'";
            }
        }
        else
        {
            //非医务渠道
            if (Client != 0)
                ConditionString += string.Format(" AND CSO_SampleOffer.InfoCollectClient={0}", Client);
            else if (ViewState["IsSummaryRow"].ToString() != "Y")
                ConditionString += " AND ISNULL(CSO_SampleOffer.InfoCollectClient,0)= 0 ";

            if (OfferMan != 0)
                ConditionString += string.Format(" AND CSO_SampleOffer.InfoCollectPromotor={0}", OfferMan);
            else if (ViewState["IsSummaryRow"].ToString() != "Y")
                ConditionString += " AND ISNULL(CSO_SampleOffer.InfoCollectPromotor,0) = 0 ";
        }

        if (product != 0)
        {
            ConditionString += " AND CSO_SampleOffer.ConfirmProduct=" + product.ToString();
        }
        if (OfferMode != 0)
        {
            ConditionString += string.Format(" AND CSO_SampleOffer.OfferMode={0}", OfferMode);
            //if (OfferMan == 0)
            //    ConditionString += string.Format(" AND CSO_SampleOffer.OfferMan is null ");
        }
        if ((int)ViewState["Source"] != 0)
            ConditionString += " AND CSO_SampleOffer.InfoSource = " + ViewState["Source"].ToString();

        switch (flag)
        {
            case 1:        //派发总数
                ConditionString += " AND CSO_SampleOffer.OfferMode in(1,2,4) ";
                break;
            case 2:        //不需要核实数量
                ConditionString += " AND CSO_SampleOffer.ConfirmState=1 AND CSO_SampleOffer.OfferMode in(1,2,4) ";
                break;
            case 3:        //需要核实数量
                ConditionString += " AND CSO_SampleOffer.ConfirmState>1 AND CSO_SampleOffer.OfferMode in(1,2,4) ";
                break;
            case 4:        //已完成核实数量
                ConditionString += " AND CSO_SampleOffer.ConfirmState>2 AND CSO_SampleOffer.OfferMode in(1,2,4) ";
                break;
            case 5:        //核实为有效数量
                ConditionString += " AND CSO_SampleOffer.ConfirmState=3 AND CSO_SampleOffer.OfferMode in(1,2,4) ";
                break;
            case 6:        //核实为无效数量
                ConditionString += " AND CSO_SampleOffer.ConfirmState=4 AND CSO_SampleOffer.OfferMode in(1,2,4) ";
                break;
            case 7:        //派发购买
                ConditionString += " AND CSO_SampleOffer.ConfirmState=3 AND CSO_SampleOffer.OfferMode in(3)";
                break;
            case 8:     //派发不使用
                ConditionString += " AND CSO_SampleOffer.ConfirmState=3 AND CSO_SampleOffer.OfferMode=1 AND " +
                    "MCS_SYS.dbo.UF_Spilt2('MCS_CSO.dbo.CSO_SampleOffer',CSO_SampleOffer.ExtPropertys,'IsUsed')='1'";
                break;
            case 9:     //间接续吃
                ConditionString += " AND CSO_SampleOffer.ConfirmState=3 AND CSO_SampleOffer.OfferMode=4 AND " +
                    "CSO_SampleOffer.IndirectKeep=1";
                break;
            default:
                break;
        }
        
        return ConditionString;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        gv_List.Columns[0].Visible = false;
        gv_List.ConditionString = GetConditionString();
        gv_List.BindGrid();
        ToExcel(gv_List, "ExtportFile.xls");

        gv_List.AllowPaging = true;
        gv_List.Columns[0].Visible = false;
        gv_List.BindGrid();
    }

    private void ToExcel(Control ctl, string FileName)
    {
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void bt_Sign_Click(object sender, EventArgs e)
    {
        int orderid = 0;
        int opstaff = Int32.Parse(Session["UserID"].ToString());
        //遍历DataGrid获得checked的ID
        foreach (GridViewRow item in gv_List.Rows)
        {
            if (((CheckBox)item.FindControl("cb_CheckID")).Checked == true)
            {
                orderid = Int32.Parse(this.gv_List.DataKeys[item.RowIndex].Value.ToString());
                CSO_SampleOfferBLL h = new CSO_SampleOfferBLL(orderid);
                if (h.Model.OfferMode == 1 && h.Model.ConfirmState == 3 && h.Model.TrackStaff == opstaff && DateTime.Parse(h.Model.InsertTime.ToShortDateString()) != DateTime.Today && DateTime.Parse(h.Model.InsertTime.AddDays(7).ToShortDateString()) >= DateTime.Today)
                {
                    h.SignConfirmOffer(opstaff);
                }
            }
        }
        BindGrid();
    }
}
