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
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.VST;
using MCSFramework.Model.VST;


public partial class SubModule_RM_RetailerDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            int ClientID = Request.QueryString["ClientID"] != null && int.TryParse(Request.QueryString["ClientID"].ToString(), out ClientID) ? ClientID : 0;
            ViewState["ClientID"] = ClientID;

            BindDropDown();

            if ((int)ViewState["ClientID"] > 0)
            {
                BindData();
            }
            else
            {
                #region 新增门店时的初始值
                CM_Client tdp = new CM_ClientBLL((int)Session["OwnerClient"]).Model;
                if (tdp == null)
                {
                    Response.Redirect("RetailerList.aspx");
                    return;
                }

                CM_Client m = new CM_Client();
                m.OfficialCity = tdp.OfficialCity;
                m.PostCode = tdp.PostCode;

                CM_ClientSupplierInfo supplierinfo = new CM_ClientSupplierInfo();
                supplierinfo.State = 1;
                supplierinfo.BeginDate = DateTime.Today;
                supplierinfo.StandardPrice = PDT_StandardPriceBLL.GetDefaultPrice(tdp.ID);

                pl_detail.BindData(m);
                pl_detail.BindData(supplierinfo);

                bt_Map.Visible = false;
                bt_AddLinkMan.Visible = false;
                tr_LinkMan.Visible = false;

                #region 新增客户时，首要联系人字段不可编辑
                DropDownList ddl_ChiefLinkMan = (DropDownList)pl_detail.FindControl("CM_Client_ChiefLinkMan");
                try
                {
                    if (ddl_ChiefLinkMan != null)
                    {
                        ddl_ChiefLinkMan.Items.Clear();
                        ddl_ChiefLinkMan.Enabled = false;
                    }
                }
                catch { }
                #endregion

                #endregion
            }

        }

        #region 给活跃标志加事件
        //DropDownList ddl_ActiveFlag_1 = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        //ddl_ActiveFlag_1.AutoPostBack = true;
        //ddl_ActiveFlag_1.SelectedIndexChanged += new EventHandler(ddl_ActiveFlag_SelectedIndexChanged);
        #endregion


        #region 注册弹出窗口脚本
        string script = "function Pop_MapShow(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/CM/Map/ClientInMap.aspx") +
            "?ClientID=' + id + '&tempid='+tempid, window, 'dialogWidth:800px;DialogHeight=550px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopShow", script, true);
        #endregion
    }


    #region 绑定下拉列表框
    public void BindDropDown()
    {
        #region 业务人员
        DropDownList ddlSalesMan = pl_detail.FindControl("CM_ClientSupplierInfo_Salesman") as DropDownList;
        if (ddlSalesMan != null)
        {
            ddlSalesMan.DataTextField = "RealName";
            ddlSalesMan.DataValueField = "ID";
            IList<Org_Staff> listStaff = Org_StaffBLL.GetStaffList("Dimission=1 AND Position=1030 AND OwnerClient= " + Session["OwnerClient"].ToString());
            if (listStaff == null) listStaff = new List<Org_Staff>(1);
            listStaff.Insert(0, new Org_Staff { RealName = "请选择", ID = 0 });
            ddlSalesMan.DataSource = listStaff;
            ddlSalesMan.DataBind();
        }
        #endregion

        #region 经销商自分渠道
        DropDownList ddlTDPChannel = pl_detail.FindControl("CM_ClientSupplierInfo_TDPChannel") as DropDownList;
        if (ddlTDPChannel != null)
        {
            ddlTDPChannel.DataTextField = "Name";
            ddlTDPChannel.DataValueField = "ID";
            IList<CM_RTChannel_TDP> listChannel = CM_RTChannel_TDPBLL.GetModelList("OwnerClient= " + Session["OwnerClient"].ToString());
            if (listChannel == null) listChannel = new List<CM_RTChannel_TDP>(1);
            listChannel.Insert(0, new CM_RTChannel_TDP { Name = "请选择", ID = 0 });
            ddlTDPChannel.DataSource = listChannel;
            ddlTDPChannel.DataBind();
        }
        #endregion

        #region 经销商所属区域
        DropDownList ddlTDPSalesArea = pl_detail.FindControl("CM_ClientSupplierInfo_TDPSalesArea") as DropDownList;
        if (ddlTDPSalesArea != null)
        {
            ddlTDPSalesArea.DataTextField = "Name";
            ddlTDPSalesArea.DataValueField = "ID";
            IList<CM_RTSalesArea_TDP> listSalesArea = CM_RTSalesArea_TDPBLL.GetModelList("OwnerClient= " + Session["OwnerClient"].ToString());
            if (listSalesArea == null) listSalesArea = new List<CM_RTSalesArea_TDP>(1);
            listSalesArea.Insert(0, new CM_RTSalesArea_TDP { Name = "请选择", ID = 0 });
            ddlTDPSalesArea.DataSource = listSalesArea;
            ddlTDPSalesArea.DataBind();
        }
        #endregion

        #region 经销商价表
        DropDownList ddlStandardPrice = pl_detail.FindControl("CM_ClientSupplierInfo_StandardPrice") as DropDownList;
        if (ddlStandardPrice != null)
        {
            ddlStandardPrice.DataTextField = "Name";
            ddlStandardPrice.DataValueField = "ID";
            IList<PDT_StandardPrice> listPrice = PDT_StandardPriceBLL.GetAllPrice_BySupplier((int)Session["OwnerClient"]);
            if (listPrice == null) listPrice = new List<PDT_StandardPrice>(1);
            listPrice.Insert(0, new PDT_StandardPrice { Name = "请选择", ID = 0 });
            ddlStandardPrice.DataSource = listPrice;
            ddlStandardPrice.DataBind();
        }
        #endregion

        #region 经销商自营路线
        DropDownList ddlVisitRoute = pl_detail.FindControl("CM_ClientSupplierInfo_VisitRoute") as DropDownList;
        if (ddlVisitRoute != null)
        {
            ddlVisitRoute.DataTextField = "Name";
            ddlVisitRoute.DataValueField = "ID";
            IList<VST_Route> listRoute = VST_RouteBLL.GetRouteListByTDP((int)Session["OwnerClient"]);
            listRoute.Insert(0, new VST_Route { Name = "请选择", ID = 0 });
            ddlVisitRoute.DataSource = listRoute;
            ddlVisitRoute.DataBind();
        }
        #endregion

        #region 行政城市默认为厂商所在省份
        MCSTreeControl tr_OfficialCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OfficialCity");
        if (tr_OfficialCity != null)
        {
            if ((int)Session["Manufacturer"] > 0)
            {
                CM_Client c = new CM_ClientBLL((int)Session["Manufacturer"]).Model;
                if (c != null)
                {
                    int prov = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OfficialCity", c.OfficialCity, 1);
                    tr_OfficialCity.RootValue = prov.ToString();
                }
            }
        }
        #endregion
    }



    void ddl_ActiveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        //if (ddl_ActiveFlag.SelectedValue == "2")
        //{
        //    ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = DateTime.Now.ToString("yyyy-MM-dd");
        //}
        //else
        //{
        //    ((TextBox)pl_detail.FindControl("CM_Client_CloseTime")).Text = "";
        //}
    }
    #endregion

    private void BindData()
    {
        CM_ClientBLL _ClientBll = new CM_ClientBLL((int)ViewState["ClientID"]);

        if (_ClientBll.Model == null || _ClientBll.Model.ClientType != 3)
        {
            MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
            return;
        }

        pl_detail.BindData(_ClientBll.Model);

        CM_ClientSupplierInfo clientSupplierInfo = _ClientBll.GetSupplierInfo((int)Session["OwnerClient"]);
        if (clientSupplierInfo != null) pl_detail.BindData(clientSupplierInfo);

        #region 绑定该客户的首要联系人
        //DropDownList ddl_ChiefLinkMan = (DropDownList)pl_detail.FindControl("CM_Client_ChiefLinkMan");
        //try
        //{
        //    ddl_ChiefLinkMan.DataTextField = "Name";
        //    ddl_ChiefLinkMan.DataValueField = "ID";
        //    ddl_ChiefLinkMan.DataSource = CM_LinkManBLL.GetModelList("ClientID=" + ViewState["ClientID"].ToString());
        //    ddl_ChiefLinkMan.DataBind();
        //    ddl_ChiefLinkMan.Items.Insert(0, new ListItem("请选择", "0"));
        //    ddl_ChiefLinkMan.SelectedValue = m.ChiefLinkMan.ToString();
        //}
        //catch { }

        #endregion

        BindGrid();

        //bt_Map.OnClientClick = "javascript:Pop_MapShow(" + _ClientBll.Model.ID.ToString() + ")";
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        if (ViewState["ClientID"] == null || (int)ViewState["ClientID"] == 0)
        {
            _bll = new CM_ClientBLL();
        }
        else
        {
            _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        }

        CM_ClientSupplierInfo supplierinfo = _bll.GetSupplierInfo((int)Session["OwnerClient"]);
        CM_ClientManufactInfo manufactinfo = _bll.GetManufactInfo((int)Session["Manufacturer"]);

        if (supplierinfo == null)
        {
            supplierinfo = new CM_ClientSupplierInfo();
            supplierinfo.Supplier = (int)Session["OwnerClient"];
        }
        if (manufactinfo == null)
        {
            manufactinfo = new CM_ClientManufactInfo();
            manufactinfo.Manufacturer = (int)Session["Manufacturer"];

            //门店所属区域为经销商对应区域
            CM_ClientBLL s = new CM_ClientBLL((int)Session["OwnerClient"]);
            manufactinfo.OrganizeCity = s.GetManufactInfo().OrganizeCity;
        }

        pl_detail.GetData(_bll.Model);
        pl_detail.GetData(supplierinfo);

        #region 判断必填项
        #endregion

        #region 判断活跃标志
        if (supplierinfo.State == 1 && supplierinfo.EndDate != new DateTime(1900, 1, 1))
            supplierinfo.EndDate = new DateTime(1900, 1, 1);

        if (supplierinfo.State == 2 && supplierinfo.EndDate == new DateTime(1900, 1, 1))
            supplierinfo.EndDate = DateTime.Now;
        #endregion

        if (ViewState["ClientID"] == null || (int)ViewState["ClientID"] == 0)
        {
            _bll.Model.ClientType = 3;
            _bll.Model.ApproveFlag = 1;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.OwnerType = 3;           //所属经销商
            _bll.Model.OwnerClient = (int)Session["OwnerClient"];
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

        Response.Redirect("RetailerDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());

    }


    private void BindGrid()
    {
        if (ViewState["ClientID"] != null)
        {
            string ConditionStr = "MCS_CM.dbo.CM_LinkMan.ClientID = " + ViewState["ClientID"].ToString();
            gv_List.ConditionString = ConditionStr;
            gv_List.BindGrid();
        }
    }

    protected void bt_AddLinkMan_Click(object sender, EventArgs e)
    {
        Response.Redirect("../LM/LinkManDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&URL=~/SubModule/CM/RT/RetailerDetail.aspx");
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void tbx_Code_TextChanged(object sender, EventArgs e)
    {

        MessageBox.Show(this, "该门店的编号已经存在,请重新输入!");
        ((TextBox)pl_detail.FindControl("CM_Client_Code")).Text = "";

    }

    protected void bt_ReplaceClientManager_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void bt_Map_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/CM/Map/ClientInMap.aspx?ClientID="+ViewState["ClientID"].ToString());
    }
}
