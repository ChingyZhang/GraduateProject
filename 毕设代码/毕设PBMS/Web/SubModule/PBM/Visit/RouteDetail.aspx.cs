// ===================================================================
// 文件路径:SubModule/PBM/Visit/RouteDetail.aspx.cs 
// 生成日期:2015-04-05 22:16:35 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.VST;
using MCSFramework.Model.VST;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
public partial class SubModule_PBM_Visit_RouteDetail : System.Web.UI.Page
{
    MCSTreeControl tr_OrganizeCity;
    DropDownList ddl_RelateStaff;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        #region 初始化界面控件
        tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("VST_Route_OrganizeCity");
        ddl_RelateStaff = (DropDownList)pl_detail.FindControl("VST_Route_RelateStaff");
        if (tr_OrganizeCity != null)
        {
            tr_OrganizeCity.Selected += tr_OrganizeCity_Selected;
            tr_OrganizeCity.AutoPostBack = true;
        }
        #endregion

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            //TDP维护自己的路线
            if ((int)Session["OwnerType"] == 3) Header.Attributes["WebPageSubCode"] = "OwnerType=3";

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                VST_Route m = new VST_Route();
                m.OrganizeCity = (int)Session["OrganizeCity"];
                m.EnableFlag = "Y";
                m.InsertTime = DateTime.Now;

                pl_detail.BindData(m);
                tr_OrganizeCity_Selected(null, null);
            }
        }
    }

    void tr_OrganizeCity_Selected(object sender, SelectedEventArgs e)
    {
        int city = 0;
        if (tr_OrganizeCity != null) int.TryParse(tr_OrganizeCity.SelectValue, out city);

        if (city > 0 && ddl_RelateStaff != null)
        {
            #region 判断当前可查询的管理区域范围
            string orgcitys = "";
            if ((int)Session["OwnerType"] != 3 && tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
                orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                if (orgcitys != "")
                {
                    string ConditionStr = " Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1";
                    ConditionStr += " AND Org_Staff.OrganizeCity IN (" + orgcitys + ") AND Org_Staff.OwnerType=" + Session["OwnerType"].ToString();

                    try
                    {
                        ddl_RelateStaff.DataTextField = "RealName";
                        ddl_RelateStaff.DataValueField = "ID";
                        ddl_RelateStaff.DataSource = Org_StaffBLL.GetStaffList(ConditionStr);
                        ddl_RelateStaff.DataBind();
                        ddl_RelateStaff.Items.Insert(0, new ListItem("请选择...", "0"));
                    }
                    catch { }
                }
            }
            #endregion


        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((int)Session["OwnerType"] == 3)
        {
            if (tr_OrganizeCity != null) tr_OrganizeCity.Enabled = false;
            if (ddl_RelateStaff != null)
            {
                ddl_RelateStaff.DataTextField = "RealName";
                ddl_RelateStaff.DataValueField = "ID";
                ddl_RelateStaff.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1");
                ddl_RelateStaff.DataBind();
                ddl_RelateStaff.Items.Insert(0, new ListItem("请选择...", "0"));
            }
        }
        else
        {

        }
    }
    #endregion

    private void BindData()
    {
        VST_Route m = new VST_RouteBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

            tr_OrganizeCity_Selected(null, null);
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        VST_RouteBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new VST_RouteBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new VST_RouteBLL();
            _bll.Model.OwnerType = (int)Session["OwnerType"];
            _bll.Model.OwnerClient = (int)Session["OwnerClient"];
            _bll.Model.ApproveFlag = 1;
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "RouteList.aspx");
            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "RouteList.aspx");
            }
        }

    }

}