// ===================================================================
// 文件路径:SubModule/RM/RetailerDetail.aspx.cs 
// 生成日期:2007-12-29 14:26:36 
// 作者:	  
// ===================================================================
using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model;
using System.Collections.Generic;
using MCSFramework.BLL.VST;


public partial class SubModule_RM_RetailerDetail : System.Web.UI.Page
{
    MCSTreeControl tr_OfficialCity = null, tr_OrganizeCity = null, tr_Channel = null;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        #region 给行政城市加事件
        tr_OfficialCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OfficialCity");
        if (tr_OfficialCity != null)
        {
            tr_OfficialCity.AutoPostBack = true;
            tr_OfficialCity.Selected += tr_OfficialCity_Selected;
        }

        tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("CM_ClientManufactInfo_OrganizeCity");
        if (tr_OrganizeCity != null)
        {
            tr_OrganizeCity.AutoPostBack = true;
            tr_OrganizeCity.Selected += tr_OrganizeCity_Selected;
        }

        tr_Channel = (MCSTreeControl)pl_detail.FindControl("CM_ClientManufactInfo_Channel");
        if (tr_Channel != null)
        {
            tr_Channel.AutoPostBack = true;
            tr_Channel.Selected += tr_Channel_Selected;
        }
        #endregion

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
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
            #endregion

            Session["MCSMenuControl_FirstSelectIndex"] = "11";

            BindDropDown();

            if (ViewState["ClientID"] != null)
            {
                BindData();
            }
            else if (Request.QueryString["Mode"] == "New")
            {
                #region 新增门店时的初始值
                DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
                if (ddl_ActiveFlag != null) ddl_ActiveFlag.SelectedValue = "1";


                TextBox tbx_OpenTime = (TextBox)pl_detail.FindControl("CM_Client_OpenTime");
                if (tbx_OpenTime != null) tbx_OpenTime.Text = DateTime.Today.ToString("yyyy-MM-dd");

                bt_Approve.Visible = false;

                #endregion
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }

        #region 给活跃标志加事件
        //DropDownList ddl_ActiveFlag_1 = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        //ddl_ActiveFlag_1.AutoPostBack = true;
        //ddl_ActiveFlag_1.SelectedIndexChanged += new EventHandler(ddl_ActiveFlag_SelectedIndexChanged);
        #endregion

        //TextBox tbx_Code = (TextBox)pl_detail.FindControl("CM_Client_Code");
        //tbx_Code.AutoPostBack = true;
        //tbx_Code.TextChanged += new EventHandler(tbx_Code_TextChanged);

        #region 注册弹出窗口脚本
        string script = "function PopReplaceClientManager(id,clienttype){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../ReplaceClientManager.aspx") +
            "?ClientManager=' + id + '&ClientType='+clienttype+'&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopReplaceClientManager", script, true);


        script = "function Pop_SetPrimaryAccount(client){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/RM/AccountOpen/Pop_SetPrimaryAccount.aspx") +
            "?Client=' + client + '&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=400px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pop_SetPrimaryAccount", script, true);

        script = "function Pop_MapShow(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.open('" + Page.ResolveClientUrl("../Map/ClientInMap.aspx") +
            "?ClientID=' + id + '&tempid='+tempid);}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopShow", script, true);
        #endregion
    }    

    #region 绑定下拉列表框
    public void BindDropDown()
    {
        MCSTreeControl tr_OfficialCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OfficialCity");
        if (tr_OfficialCity != null)
        {
            if ((int)Session["OwnerType"] == 2 && (int)Session["OwnerClient"] > 0)
            {
                CM_Client c = new CM_ClientBLL((int)Session["OwnerClient"]).Model;
                if (c != null)
                {
                    int prov = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OfficialCity", c.OfficialCity, 1);
                    tr_OfficialCity.RootValue = prov.ToString();
                }
            }
        }

    }

    void ddl_ActiveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        if (ddl_ActiveFlag.SelectedValue == "2")
        {
            ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = "";
        }
    }

    void BindGeoCode(int city)
    {
        if (city > 0)
        {

            DropDownList ddl_GeoCode = (DropDownList)pl_detail.FindControl("CM_ClientManufactInfo_GeoCode");
            if (ddl_GeoCode != null)
            {
                ddl_GeoCode.DataValueField = "ID";
                ddl_GeoCode.DataTextField = "Name";

                ddl_GeoCode.DataSource = CM_GeoCodeBLL.GetListByCityCode(CM_ClientBLL.GetORGCOD2ByCity(city));
                ddl_GeoCode.DataBind();
                ddl_GeoCode.Items.Insert(0, new ListItem("请选择...", "0"));
            }
        }
    }

    void BindVisitRoute(int city)
    {
        if (city > 0)
        {
            DropDownList ddl_VisitRoute = (DropDownList)pl_detail.FindControl("CM_ClientManufactInfo_VisitRoute");
            if (ddl_VisitRoute != null)
            {
                ddl_VisitRoute.DataValueField = "ID";
                ddl_VisitRoute.DataTextField = "Name";

                ddl_VisitRoute.DataSource = VST_RouteBLL.GetByOrganizeCity(city);
                ddl_VisitRoute.DataBind();
                ddl_VisitRoute.Items.Insert(0, new ListItem("请选择...", "0"));
            }
        }
    }

    void tr_Channel_Selected(object sender, SelectedEventArgs e)
    {
        //根据子渠道自动设定价格渠道
        if (tr_Channel == null) return;

        try
        {
            int channel = 0;
            int.TryParse(tr_Channel.SelectValue, out channel);
            if (channel > 0)
            {
                CM_RTChannel_SYS _ch = new CM_RTChannel_SYSBLL(channel).Model;
                if (_ch != null)
                {
                    DropDownList ddl_MKTSGM = (DropDownList)pl_detail.FindControl("CM_ClientManufactInfo_MKTSGM");
                    if (ddl_MKTSGM != null && ddl_MKTSGM.Items.FindByValue(_ch["MKTSGM"]) != null)
                    {
                        ddl_MKTSGM.SelectedValue = _ch["MKTSGM"];
                    }
                }
            }
        }
        catch { }
    }

    void tr_OrganizeCity_Selected(object sender, SelectedEventArgs e)
    {
        if (tr_OrganizeCity == null) return;

        int city = 0;
        int.TryParse(tr_OrganizeCity.SelectValue, out city);
        BindVisitRoute(city);
    }

    void tr_OfficialCity_Selected(object sender, SelectedEventArgs e)
    {
        if (tr_OfficialCity == null) return;

        int city = 0;
        int.TryParse(tr_OfficialCity.SelectValue, out city);
        BindGeoCode(city);
    }
    #endregion

    private void BindData()
    {
        CM_ClientBLL clientbll = new CM_ClientBLL((int)ViewState["ClientID"]);
        CM_Client m = clientbll.Model;
        if (m == null) Response.Redirect("RetailerList.aspx");

        switch (m.ClientType)
        {
            case 1:
                Response.Redirect("../Store/StoreDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            case 2:
                Response.Redirect("../DI/DistributorDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            case 3:
                break;
            default:
                MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
                break;
        }

        CM_ClientSupplierInfo supplierinfo = clientbll.GetSupplierInfoByManufacturer((int)Session["Manufacturer"]);
        CM_ClientManufactInfo manufactinfo = clientbll.GetManufactInfo((int)Session["Manufacturer"]);

        pl_detail.BindData(m);

        if (supplierinfo != null) pl_detail.BindData(supplierinfo);
        if (manufactinfo != null) 
        {
            BindGeoCode(m.OfficialCity); 
            BindVisitRoute(manufactinfo.OrganizeCity); 
            pl_detail.BindData(manufactinfo);
        }

       

        MCSSelectControl select_ClientManager = (MCSSelectControl)pl_detail.FindControl("CM_ClientManufactInfo_ClientManager");
        if (select_ClientManager != null)
        {
            select_ClientManager.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + manufactinfo.OrganizeCity.ToString() + "&IncludeSuperManager=Y";
        }

        if (manufactinfo != null)
        {
            if (manufactinfo.SyncState != 0 && manufactinfo.SyncState != 1 && manufactinfo.SyncState != 8) bt_Submit.Visible = false;
            if (manufactinfo.SyncState != 2) { bt_Approve.Visible = false; bt_UnApprove.Visible = false; }

            //有公司客户编号后，页面为编辑权限模式
            if (!string.IsNullOrEmpty(manufactinfo.Code)) Header.Attributes["WebPageSubCode"] = "Modify";
        }

        bt_Map.OnClientClick = "javascript:Pop_MapShow(" + m.ID.ToString() + ")";
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        if (ViewState["ClientID"] == null)
        {
            _bll = new CM_ClientBLL();
        }
        else
        {
            _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        }

        #region 获取原供货及厂商管理信息
        CM_ClientSupplierInfo supplierinfo = _bll.GetSupplierInfoByManufacturer((int)Session["Manufacturer"]);
        CM_ClientManufactInfo manufactinfo = _bll.GetManufactInfo((int)Session["Manufacturer"]);

        if (supplierinfo == null) supplierinfo = new CM_ClientSupplierInfo();
        if (manufactinfo == null)
        {
            manufactinfo = new CM_ClientManufactInfo();
            manufactinfo.Manufacturer = (int)Session["Manufacturer"];
        }
        #endregion

        pl_detail.GetData(_bll.Model);
        pl_detail.GetData(supplierinfo);
        pl_detail.GetData(manufactinfo);

        #region 判断必填项
        if (_bll.Model.OfficialCity == 0)
        {
            MessageBox.Show(this, "所属的行政城市必填!");
            return;
        }
        #endregion

        #region 判断活跃标志
        if (manufactinfo.State == 1 && manufactinfo.EndDate != new DateTime(1900, 1, 1))
            manufactinfo.EndDate = new DateTime(1900, 1, 1);

        if (manufactinfo.State == 2 && manufactinfo.EndDate == new DateTime(1900, 1, 1))
            manufactinfo.EndDate = DateTime.Now;
        #endregion

        if (ViewState["ClientID"] == null)
        {
            _bll.Model.ClientType = 3;
            _bll.Model.ApproveFlag = 1;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.OwnerType = 2;
            _bll.Model.OwnerClient = (int)Session["Manufacturer"];
            ViewState["ClientID"] = _bll.Add();

            _bll.SetSupplierInfo(supplierinfo);
            _bll.SetManufactInfo(manufactinfo);
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
            _bll.SetSupplierInfo(supplierinfo);
            _bll.SetManufactInfo(manufactinfo);
        }

        if (sender != null) Response.Redirect("RetailerList.aspx");

    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            bt_OK_Click(null, null);
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            CM_ClientManufactInfo manufactinfo = _bll.GetManufactInfo((int)Session["Manufacturer"]);

            #region 判断必填项

            #endregion

            if (manufactinfo.SyncState == 0 || manufactinfo.SyncState == 1 || manufactinfo.SyncState == 8)
            {
                CM_ClientManufactInfoBLL.Approve((int)ViewState["ClientID"], (int)Session["Manufacturer"], (int)Session["UserID"], 2);
                Response.Redirect("RetailerList.aspx?SyncState=1");
            }
        }
    }


    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            bt_OK_Click(null, null);

            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            CM_ClientManufactInfo manufactinfo = _bll.GetManufactInfo((int)Session["Manufacturer"]);

            //if (string.IsNullOrEmpty(manufactinfo.Code))
            //{
            //    MessageBox.Show(this, "客户编号为空，不能审核通过!");
            //    return;
            //}

            if (manufactinfo.SyncState == 2)
            {
                CM_ClientManufactInfoBLL.Approve((int)ViewState["ClientID"], (int)Session["Manufacturer"], (int)Session["UserID"], 3);

                Response.Redirect("RetailerList.aspx?SyncState=2");
            }
        }
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            bt_OK_Click(null, null);

            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            CM_ClientManufactInfo manufactinfo = _bll.GetManufactInfo((int)Session["Manufacturer"]);

            if (manufactinfo.SyncState == 2)
            {
                CM_ClientManufactInfoBLL.Approve((int)ViewState["ClientID"], (int)Session["Manufacturer"], (int)Session["UserID"], 8);

                Response.Redirect("RetailerList.aspx?SyncState=2");
            }
        }
    }
}
