// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;

public partial class SubModule_RM_RetailerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindDropDown();
            Session["ClientID"] = null;
            Session["MCSMenuControl_FirstSelectIndex"] = "42";
            BindGrid();
        }
        string script = "function PopShow(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../Map/ClientInMap.aspx") +
            "?ClientID=' + id + '&tempid='+tempid, window, 'dialogWidth:800px;DialogHeight=550px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopShow", script, true);

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((int)Session["AccountType"] == 1)
        {
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
            tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

            if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
            {
                tr_OrganizeCity.RootValue = "0";
                tr_OrganizeCity.SelectValue = "1";
            }
            else
            {
                tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
                tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
            }
        }
        else if ((int)Session["AccountType"] == 2)
        {
            CM_Client client = new CM_ClientBLL((int)Session["UserID"]).Model;
            if (client != null)
            {
                Addr_OrganizeCityBLL citybll = new Addr_OrganizeCityBLL(client.OrganizeCity);
                tr_OrganizeCity.DataSource = citybll.GetAllChildNodeIncludeSelf();
                tr_OrganizeCity.RootValue = citybll.Model.SuperID.ToString();
                tr_OrganizeCity.SelectValue = client.OrganizeCity.ToString();
            }
        }

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ApproveFlag.SelectedValue = "1";

        //ddl_ActiveFlag.DataSource = DictionaryBLL.GetDicCollections("CM_ActiveFlag");
        //ddl_ActiveFlag.DataBind();
        //ddl_ActiveFlag.Items.Insert(0, new ListItem("所有", "0"));
        //ddl_ActiveFlag.SelectedValue = "1";

        ddl_RTClassify.DataSource = DictionaryBLL.GetDicCollections("CM_RT_Classify");
        ddl_RTClassify.DataBind();
        ddl_RTClassify.Items.Insert(0, new ListItem("所有", "0"));
        ddl_RTClassify.SelectedValue = "0";

        //ddl_RTChannel.DataSource = DictionaryBLL.GetDicCollections("CM_RT_Channel");
        //ddl_RTChannel.DataBind();
        //ddl_RTChannel.Items.Insert(0, new ListItem("所有", "0"));
        //ddl_RTChannel.SelectedValue = "0";

        //ddl_MarketType.DataSource = DictionaryBLL.GetDicCollections("CM_MarketType");
        //ddl_MarketType.DataBind();
        //ddl_MarketType.Items.Insert(0, new ListItem("所有", "0"));
        //ddl_MarketType.SelectedValue = "0";



    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " CM_Client.ID IN (SELECT  TOP 1000 CM_Client.ID FROM MCS_CM.dbo.CM_Client LEFT OUTER JOIN MCS_CM.dbo.CM_LinkMan ON CM_Client.ChiefLinkMan = CM_LinkMan.ID LEFT OUTER JOIN MCS_SYS.dbo.Org_Staff ON CM_Client.ClientManager = Org_Staff.ID WHERE CM_Client.ClientType = 3 ";
        if (tbx_Condition.Text.Trim() != "")
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";

        string orgcitys = "";

        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            ConditionStr += " And CM_Client.ApproveFlag =" + ddl_ApproveFlag.SelectedValue;
        }
        //if (ddl_RTChannel.SelectedValue != "0")
        //{
        //    ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CM.dbo.CM_Client',CM_Client.ExtPropertys,'RTChannel')=" + ddl_RTChannel.SelectedValue;
        //}
        if (ddl_RTClassify.SelectedValue != "0")
        {
            ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CM.dbo.CM_Client',CM_Client.ExtPropertys,'RTClassify')=" + ddl_RTClassify.SelectedValue;
        }
        //if (ddl_MarketType.SelectedValue != "0")
        //{
        //    ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_CM.dbo.CM_Client',CM_Client.ExtPropertys,'MarketType')=" + ddl_MarketType.SelectedValue;
        //}
        //if (ddl_ActiveFlag.SelectedValue != "0")
        //{
        //    ConditionStr += " And MCS_CM.dbo.CM_Client.ActiveFlag =" + ddl_ActiveFlag.SelectedValue;
        //}

        //是否有权限“仅查看自己的门店”，如果是，限制客户经理条件,如果是经销商账号则只看查看供货商为自己的门店
        if ((int)Session["AccountType"] == 1)
        {
            if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 11, "OlnyViewMyClient"))
            {

                #region 获取当前员工的关联经销商
                Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);

                if (staff.Model["RelateClient"] != "")
                {
                    ConditionStr += " And CM_Client.Supplier=" + staff.Model["RelateClient"];
                }
                else
                {
                    ConditionStr += " And CM_Client.ClientManager=" + Session["UserID"];
                }
                #endregion
            }
        }
        else if ((int)Session["AccountType"] == 2)
        {
            CM_Client client = new CM_ClientBLL((int)Session["UserID"]).Model;

            if (client.ClientType == 3)
            {
                ConditionStr += " And CM_Client.ID = " + Session["UserID"].ToString();
            }
            else if (client.ClientType == 2)
            {
                ConditionStr += " And CM_Client.Supplier = " + Session["UserID"].ToString();
            }


        }
        ConditionStr += ")";

        if (ViewState["PageIndex"] != null)
        {
            gv_List.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }

        gv_List.ConditionString = ConditionStr;
        //gv_List.OrderFields = "CM_Client_OrganizeCity";
        gv_List.BindGrid();


    }

    #region 分页、排序、选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Session["ClientID"] = id;
        if (Request.QueryString["URL"] != null) Response.Redirect(Request.QueryString["URL"]);
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int clientid = (int)gv_List.DataKeys[e.Row.RowIndex]["CM_Client_ID"];

            #region 显示销量库存录入情况
            int month = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(0 - ConfigHelper.GetConfigInt("JXCDelayDays")));

            DataTable dt = SVM_SalesVolumeBLL.GetCountByClient(month, clientid);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    HyperLink link = null;
                    switch (row["Title"].ToString())
                    {
                        case "SaleIn":
                            if (e.Row.Cells[e.Row.Cells.Count - 5].Controls.Count > 0)
                                link = (HyperLink)(e.Row.Cells[e.Row.Cells.Count - 5].Controls[0]);
                            break;
                        case "SaleOut":
                            if (e.Row.Cells[e.Row.Cells.Count - 4].Controls.Count > 0)
                                link = (HyperLink)(e.Row.Cells[e.Row.Cells.Count - 4].Controls[0]);
                            break;
                        case "Difference":
                            if (e.Row.Cells[e.Row.Cells.Count - 3].Controls.Count > 0)
                                link = (HyperLink)(e.Row.Cells[e.Row.Cells.Count - 3].Controls[0]);
                            break;
                        //case "Inventory":
                        //    if (e.Row.Cells[e.Row.Cells.Count - 2].Controls.Count > 0)
                        //        link = (HyperLink)(e.Row.Cells[e.Row.Cells.Count - 2].Controls[0]);
                        //    break;
                        case "SalesForcast":
                            if (e.Row.Cells[e.Row.Cells.Count - 2].Controls.Count > 0)
                                link = (HyperLink)(e.Row.Cells[e.Row.Cells.Count - 2].Controls[0]);
                            break;
                    }
                    if (link != null)
                    {
                        link.Text += "<br/>";
                        if (row["ApprovedCount"] != DBNull.Value && (int)row["ApprovedCount"] > 0)
                        {
                            link.ToolTip += " 已审核:" + row["ApprovedCount"].ToString();
                            link.Text += "-<font color=red>" + row["ApprovedCount"].ToString() + "</font>";
                        }
                        if (row["SubmitedCount"] != DBNull.Value && (int)row["SubmitedCount"] > 0)
                        {
                            link.ToolTip += " 已提交:" + row["SubmitedCount"].ToString();
                            link.Text += "-<font color=blue>" + row["SubmitedCount"].ToString() + "</font>";
                        }
                        if (row["InputedCount"] != DBNull.Value && (int)row["InputedCount"] > 0)
                        {
                            link.ToolTip += " 未提交:" + row["InputedCount"].ToString();
                            link.Text += "-<font color=black>" + row["InputedCount"].ToString() + "</font>";
                        }
                    }
                }
            }
            #endregion

            #region 显示附件图片
            HyperLink hy_attach = (HyperLink)e.Row.FindControl("hy_attach");
            if (hy_attach != null)
            {
                string imgpath = ATMT_AttachmentBLL.GetFirstPreviewPicture(30, clientid);
                if (!string.IsNullOrEmpty(imgpath))
                {
                    hy_attach.Text = "";
                    Image img = new Image();
                    img.ImageUrl = imgpath;
                    hy_attach.Controls.Add(img);
                    img.Width = new Unit(60);
                }
            }
            #endregion
        }
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Session["ClientID"] = null;
        Response.Redirect("RetailerDetail.aspx?Mode=New");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0"://快捷查询

                break;
            //case "1"://高级查询
            //    Response.Redirect("AdvanceFind.aspx");
            //    break;
        }
    }

    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        //    DataTable dt = Retailer.GetAllList(int.Parse(Session["UserID"].ToString()));;
        //    //获取数据源
        //    if (gv_List != null && gv_List.Rows.Count > 0)
        //    {
        //        string sFileName = "Export_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
        //        System.IO.StringWriter sw = new System.IO.StringWriter();
        //        sw.WriteLine("门店编号	区域	城市	客户名称	门店名称	电话	客户渠道	活跃状态	门店分类	销售代表");
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string str = "";
        //            str = dt.Rows[i]["Code"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["SalesTerritoryName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["SalesCityName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ClientName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["Name"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["TeleNum"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ChannelName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ActiveStatusName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ClassifyName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["SalesPersonName"].ToString().Replace("	", "");
        //            sw.WriteLine(str);
        //        }
        //        Response.Buffer = true;
        //        Response.Charset = "gb2312";
        //        Response.AppendHeader("Content-Disposition", "attachment;filename=" + sFileName);
        //        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");//设置输出流为简体中文
        //        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        //        Response.Write(sw);
        //        sw.Close();
        //        Response.End();
        //    }
        //    else
        //    {
        //        MessageBox.Show(this, "无数据待导出！");
        //        return;
        //    }
    }

    protected string setmap(int id)
    {
        CM_ClientBLL client = new CM_ClientBLL(id);
        CM_ClientGeoInfo info = CM_ClientGeoInfoBLL.GetGeoInfoByClient(id);
        if (info == null)
            return "showmap('',0,0)";
        else
            return string.Format("showmap(\"{0}\",{1},{2})", client.Model.FullName, info.Longitude, info.Latitude);
    }
}
