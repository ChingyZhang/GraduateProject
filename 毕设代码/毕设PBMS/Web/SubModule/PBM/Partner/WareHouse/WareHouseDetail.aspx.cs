// ===================================================================
// 文件路径:SubModule/CM/WareHouse/CM_WareHouseDetail.aspx.cs 
// 生成日期:2012-7-25 11:19:01 
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
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSControls.MCSWebControls;

public partial class SubModule_PBM_Partner_WareHouse_WareHouseDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                ViewState["ClientID"] = 0;


                CM_WareHouse m = new CM_WareHouse();
                m.ActiveState = 1;
                m.Client = (int)Session["OwnerClient"];

                CM_Client c = new CM_ClientBLL(m.Client).Model;
                if (c != null)
                {
                    m.OfficialCity = c.OfficialCity;
                    m.Address = c.Address;
                    m.TeleNum = c.TeleNum;
                }

                pl_detail.BindData(m);
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        CM_WareHouse m = new CM_WareHouseBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            if (m.Client != (int)Session["OwnerClient"])
            {
                Response.Redirect("~/SubModule/Desktop.aspx");
            }

            pl_detail.BindData(m);
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_WareHouseBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new CM_WareHouseBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new CM_WareHouseBLL();
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
                Response.Redirect("WareHouseList.aspx");
            }
        }
        else
        {
            //新增
            _bll.Model.Client = (int)Session["OwnerClient"];
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.ApproveFlag = 1;
            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "WareHouseList.aspx");
            }
        }

    }

}