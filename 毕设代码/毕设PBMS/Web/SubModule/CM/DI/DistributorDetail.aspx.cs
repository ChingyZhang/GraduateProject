// ===================================================================
// 文件路径:CM/Distributor/DistributorDetail.aspx.cs 
// 生成日期:2008-12-19 10:11:21 
// 作者:	  yangwei
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.IFStrategy;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using System.Collections.Specialized;
using System.Web.Security;
using MCSFramework.BLL.VST;
using MCSFramework.Model;

public partial class CM_Distributor_DistributorDetail : System.Web.UI.Page
{
    MCSTreeControl tr_OfficialCity = null, tr_OrganizeCity = null;
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
        #endregion
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

            Session["MCSMenuControl_FirstSelectIndex"] = "12";

            BindDropDown();
            if (ViewState["ClientID"] != null)
            {
                BindData();
            }
            else if (Request.QueryString["Mode"] == "New")
            {
                //新增客户时的初始值
                DropDownList ddl_State = (DropDownList)pl_detail.FindControl("CM_ClientManufactInfo_State");
                if (ddl_State != null) ddl_State.SelectedValue = "1";

                TextBox tbx_OpenTime = (TextBox)pl_detail.FindControl("CM_ClientManufactInfo_BeginDate");
                if (tbx_OpenTime != null) tbx_OpenTime.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        #region 给活跃标志加事件
        //DropDownList ddl_ActiveFlag_1 = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
        //ddl_ActiveFlag_1.AutoPostBack = true;
        //ddl_ActiveFlag_1.SelectedIndexChanged += new EventHandler(ddl_ActiveFlag_SelectedIndexChanged);


        #endregion

        #region 给判断重复编码加事件
        //TextBox tbx_Code = (TextBox)pl_detail.FindControl("CM_Client_Code");
        //tbx_Code.AutoPostBack = true;
        //tbx_Code.TextChanged += new EventHandler(tbx_Code_TextChanged);
        #endregion

    }

    #region 绑定下拉列表框
    private void BindDropDown()
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

        if ((int)Session["OwnerType"] == 2 && (int)Session["OrganizeCity"] > 1)
        {
            DropDownList ddl_VisitRoute = (DropDownList)pl_detail.FindControl("CM_ClientManufactInfo_VisitRoute");
            if (ddl_VisitRoute != null)
            {
                ddl_VisitRoute.DataValueField = "ID";
                ddl_VisitRoute.DataTextField = "Name";

                ddl_VisitRoute.DataSource = VST_RouteBLL.GetByOrganizeCity((int)Session["OrganizeCity"]);
                ddl_VisitRoute.DataBind();
                ddl_VisitRoute.Items.Insert(0, new ListItem("请选择...", "0"));
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

    void tr_OrganizeCity_Selected(object sender, SelectedEventArgs e)
    {
        if (tr_OrganizeCity == null) return;

        int city = 0;
        int.TryParse(tr_OrganizeCity.SelectValue, out city);
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

            #region 设置营业所编码
            Addr_OrganizeCity citymodel = new Addr_OrganizeCityBLL(city).Model;
            TextBox tbx_OUTLOC = (TextBox)pl_detail.FindControl("CM_ClientManufactInfo_OUTLOC");
            if (citymodel != null && tbx_OUTLOC != null)
            {
                tbx_OUTLOC.Text = citymodel.Code;
            }

            citymodel = new Addr_OrganizeCityBLL(citymodel.SuperID).Model;
            TextBox tbx_SALGRP = (TextBox)pl_detail.FindControl("CM_ClientManufactInfo_SALGRP");
            if (citymodel != null && tbx_SALGRP != null)
            {
                tbx_SALGRP.Text = citymodel.Code;
            }
            #endregion
        }
    }

    void tr_OfficialCity_Selected(object sender, SelectedEventArgs e)
    {
        if (tr_OfficialCity == null) return;

        int city = 0;
        int.TryParse(tr_OfficialCity.SelectValue, out city);
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

    #endregion

    private void BindData()
    {
        int clientID = 0;
        if (ViewState["ClientID"] == null || !int.TryParse(ViewState["ClientID"].ToString(), out clientID)) return;

        CM_ClientBLL clientbll = new CM_ClientBLL(clientID);
        CM_Client m = clientbll.Model;
        if (m == null) Response.Redirect("DistributorList.aspx");

        switch (m.ClientType)
        {
            case 1:
                Response.Redirect("../Store/StoreDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            case 2:
                break;
            case 3:
                Response.Redirect("../RT/RetailerDetail.aspx?ClientID=" + m.ID.ToString());
                break;
            default:
                MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                break;
        }
        pl_detail.BindData(m);

        tr_OfficialCity_Selected(null, null);
        tr_OrganizeCity_Selected(null, null);

        CM_ClientSupplierInfo supplierinfo = clientbll.GetSupplierInfoByManufacturer((int)Session["Manufacturer"]);
        CM_ClientManufactInfo manufactinfo = clientbll.GetManufactInfo((int)Session["Manufacturer"]);

        if (supplierinfo != null) pl_detail.BindData(supplierinfo);
        if (manufactinfo != null) pl_detail.BindData(manufactinfo);

        MCSSelectControl select_ClientManager = (MCSSelectControl)pl_detail.FindControl("CM_ClientManufactInfo_ClientManager");
        if (select_ClientManager != null && manufactinfo != null)
        {
            select_ClientManager.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + manufactinfo.OrganizeCity.ToString() + "&IncludeSuperManager=Y";
        }
        if (manufactinfo != null && manufactinfo.ApproveFlag == 1)
        {
            //已审核
            TextBox tbx_BeginDate = (TextBox)pl_detail.FindControl("CM_ClientManufactInfo_BeginDate");
            if (tbx_BeginDate != null && tbx_BeginDate.Text != "") tbx_BeginDate.Enabled = false;
            TextBox tbx_Code = (TextBox)pl_detail.FindControl("CM_ClientManufactInfo_Code");
            if (tbx_Code != null) tbx_Code.Enabled = false;

            bt_Approve.Visible = false;
            Header.Attributes["WebPageSubCode"] = "Modify";
        }
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

        if (supplierinfo == null)
        {
            supplierinfo = new CM_ClientSupplierInfo();
            supplierinfo.Supplier = (int)Session["Manufacturer"];
        }

        if (manufactinfo == null)
        {
            manufactinfo = new CM_ClientManufactInfo();
            manufactinfo.Manufacturer = (int)Session["Manufacturer"];
        }
        #endregion

        pl_detail.GetData(_bll.Model);
        pl_detail.GetData(supplierinfo);
        pl_detail.GetData(manufactinfo);

        #region 查重判断
        string chkrpt = "Code='" + manufactinfo.Code + "'";
        if (_bll.Model.ID > 0) chkrpt += " AND Client <> " + _bll.Model.ID.ToString();

        IList<CM_ClientManufactInfo> cmlist = CM_ClientManufactInfoBLL.GetModelList(chkrpt);
        if (cmlist.Count > 0)
        {
            MessageBox.Show(this, "客户编码重复，请检查!" + chkrpt.Replace("'", ""));
            return;
        }
        #endregion

        #region 判断必填项
        if (_bll.Model.OfficialCity == 0)
        {
            MessageBox.Show(this, "所属的行政城市必填!");
            return;
        }
        if (manufactinfo.State == 0)
        {
            MessageBox.Show(this, "合作状态必填!");
            return;
        }
        Addr_OrganizeCity orgcity = new Addr_OrganizeCityBLL(manufactinfo.OrganizeCity).Model;
        if (orgcity == null || orgcity.Level < 6)
        {
            MessageBox.Show(this, "销售区域必需选择到营业部级!");
            return;
        }
        #endregion

        #region 判断活跃标志
        if (manufactinfo.State == 1 && manufactinfo.EndDate != new DateTime(1900, 1, 1))
            manufactinfo.EndDate = new DateTime(1900, 1, 1);

        if (manufactinfo.State == 2 && manufactinfo.EndDate == new DateTime(1900, 1, 1))
            manufactinfo.EndDate = DateTime.Now;
        #endregion

        if (ViewState["ClientID"] == null)//新增经销商
        {
            _bll.Model.ClientType = 2;      //客户类型默认为经销商
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.OwnerType = 2;       //所有权属性默认为厂商级
            _bll.Model.OwnerClient = (int)Session["Manufacturer"];
            ViewState["ClientID"] = _bll.Add();

            _bll.SetSupplierInfo(supplierinfo);
            _bll.SetManufactInfo(manufactinfo);
            MessageBox.ShowAndRedirect(this, "保存经销商资料成功！", "DistributorDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
        else//修改时供应商和经销商只能为关联厂商
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
            _bll.SetSupplierInfo(supplierinfo);
            _bll.SetManufactInfo(manufactinfo);

            Response.Redirect("DistributorList.aspx");
        }



    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            CM_ClientManufactInfo manufactinfo = _bll.GetManufactInfo((int)Session["Manufacturer"]);

            if (string.IsNullOrEmpty(manufactinfo.Code))
            {
                MessageBox.Show(this, "客户编号为空，不能审核通过!");
                return;
            }

            CM_ClientManufactInfoBLL.Approve((int)ViewState["ClientID"], (int)Session["Manufacturer"], (int)Session["UserID"], 3);

            MessageBox.ShowAndRedirect(this, "审核经销商资料成功！", "DistributorList.aspx");
        }
    }

}
