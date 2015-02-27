using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.SVM;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL;


public partial class SubModule_SVM_TransferVolumeDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["VolumeID"] = Request.QueryString["VolumeID"] == null ? 0 : int.Parse(Request.QueryString["VolumeID"]);
            ViewState["IsCXP"] = Request.QueryString["IsCXP"] == null ? false : int.Parse(Request.QueryString["IsCXP"]) != 0;    //是否是赠品销量录入 0:成品 1:赠品

            BindDropDown();

            if ((int)ViewState["VolumeID"] == 0)
            {
                ViewState["TransferOutClientID"] = Request.QueryString["TransferOutClientID"] == null ? 0 : int.Parse(Request.QueryString["TransferOutClientID"]);
                ViewState["TransferInClientID"] = Request.QueryString["TransferInClientID"] == null ? 0 : int.Parse(Request.QueryString["TransferInClientID"]);

               ViewState["Type"] = 6;         //销量类型  6：经销商成品调拨单               

                if ((int)ViewState["TransferOutClientID"] == 0)
                {
                    MessageBox.ShowAndRedirect(this, "对不起，必须选择要调出货的经销商!", "TransferVolumeList.aspx");
                    return;
                }
            }
            #endregion

            select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"";
            select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"";

            #region 调入客户的管理片区为当前人的上一级，以便向管辖外的区域调货
            try
            {
                int organizecity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
                if (organizecity > 1)
                {
                    int superid = new Addr_OrganizeCityBLL(organizecity).Model.SuperID;

                    select_Client.PageUrl += "&OrganizeCity=" + superid.ToString();
                }
            }
            catch { }
            #endregion

            if ((int)ViewState["VolumeID"] == 0)
            {
                tbx_VolumeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindClientInfo((int)ViewState["TransferOutClientID"], (int)ViewState["TransferInClientID"]);

                int month = int.Parse(ddl_AccountMonth.SelectedValue);

                DataTable dt = SVM_SalesVolumeBLL.InitProductList((int)ViewState["VolumeID"], (int)ViewState["TransferOutClientID"], (int)ViewState["Type"], month, (bool)ViewState["IsCXP"]); //初始化产品列表
                DataColumn[] keys = { dt.Columns["Product"] };
                dt.PrimaryKey = keys;
                ViewState["DTDetail"] = dt;

                bt_Delete.Visible = false;
                bt_Approve.Visible = false;
            }
            else
            {
                cb_OnlyDisplayUnZero.Checked = true;
                BindData();
            }

            BindDropDown_ByIsCXP();

            //Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();
    }
    private void BindDropDown_ByIsCXP()
    {
        if (ViewState["IsCXP"] == null) return;

        if ((bool)ViewState["IsCXP"])
        {
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('9')");
        }
        else
        {
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('1')");
        }
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand_SelectedIndexChanged(null, null);
    }

    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = new PDT_ClassifyBLL()._GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
    }
    #endregion

    protected void tbx_VolumeDate_TextChanged(object sender, EventArgs e)
    {
        DateTime date;
        if (DateTime.TryParse(tbx_VolumeDate.Text, out date))
        {
            int month = AC_AccountMonthBLL.GetMonthByDate(date);
            ddl_AccountMonth.SelectedValue = month.ToString();
        }
    }

    #region 绑定客户信息
    /// <summary>
    /// 绑定客户信息
    /// </summary>
    /// <param name="transferoutclient">调出客户</param>
    /// <param name="transferinclient">调进客户</param>
    private void BindClientInfo(int transferoutclient, int transferinclient)
    {
        if (transferoutclient > 0)
        {
            CM_ClientBLL _s = new CM_ClientBLL(transferoutclient);
            select_Supplier.SelectText = _s.Model.FullName;
            select_Supplier.SelectValue = _s.Model.ID.ToString();
        }
        if (transferinclient > 0)
        {
            CM_ClientBLL _r = new CM_ClientBLL(transferinclient);
            select_Client.SelectText = _r.Model.FullName;
            select_Client.SelectValue = _r.Model.ID.ToString();
        }

    }
    #endregion

    private void BindData()
    {
        SVM_SalesVolume sv = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]).Model;
        ViewState["Type"] = sv.Type;
        ViewState["IsCXP"] = (sv.Flag > 10);

        BindClientInfo(sv.Supplier, sv.Client);
        select_Client.Enabled = false;
        select_Supplier.Enabled = false;

        tbx_VolumeDate.Text = sv.SalesDate.ToString("yyyy-MM-dd");
        ddl_AccountMonth.SelectedValue = sv.AccountMonth.ToString();
        tbx_Remark.Text = sv.Remark;

        DataTable dt = SVM_SalesVolumeBLL.InitProductList((int)ViewState["VolumeID"], sv.Supplier,
            (int)ViewState["Type"], sv.AccountMonth, false); //初始化产品列表
        DataColumn[] keys = { dt.Columns["Product"] };
        dt.PrimaryKey = keys;
        ViewState["DTDetail"] = dt;

        BindGrid();

        if (sv.ApproveFlag == 1)
        {
            #region 已审核，不可再修改
            tbx_VolumeDate.Enabled = false;
            ddl_AccountMonth.Enabled = false;
            tbx_Remark.Enabled = false;
            gv_List.SetControlsEnable(false);

            bt_Save.Visible = false;
            bt_Delete.Visible = false;
            bt_Approve.Visible = false;
            #endregion
        }
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];

        string condition = " 1=1 ";
        if (ddl_Classify.SelectedValue != "" && ddl_Classify.SelectedValue != "0")
        {
            condition += " AND Classify=" + ddl_Classify.SelectedValue;
        }
        else if (ddl_Brand.SelectedValue != "" && ddl_Brand.SelectedValue != "0")
        {
            condition += " AND Brand=" + ddl_Brand.SelectedValue;
        }
        if (tbx_ProductCode.Text != "")
        {
            condition += " AND Code like '%" + tbx_ProductCode.Text + "%'";
        }
        if (cb_OnlyDisplayUnZero.Checked)
        {
            condition += " AND Quantity <> 0";
        }

        dt.DefaultView.RowFilter = condition;
        gv_List.DataSource = dt.DefaultView;
        gv_List.DataBind();

        if (!bt_Save.Visible)
        {
            gv_List.SetControlsEnable(false);
        }
    }
    #endregion

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["VolumeID"] == 0)
        {
            int transferinclient = 0;

            if (int.TryParse(select_Client.SelectValue, out transferinclient) &&
                SVM_SalesVolumeBLL.CheckSalesVolume(transferinclient, DateTime.Parse(tbx_VolumeDate.Text), 6, 1) > 0)
            {
                MessageBox.Show(this, "该客户在当前日期:" + tbx_VolumeDate.Text + " 已经填报过数据，请检查以免重复录入!");
            }
        }
        if (bt_Save.Visible) SaveMyViewState();
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void cb_OnlyDisplayUnZero_CheckedChanged(object sender, EventArgs e)
    {
        if (bt_Save.Visible) SaveMyViewState();
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void SaveMyViewState()
    {
        DataTable dt = (DataTable)ViewState["DTDetail"];
        foreach (GridViewRow gr in gv_List.Rows)
        {
            DataRow[] rows = dt.Select("ID=" + gv_List.DataKeys[gr.RowIndex]["ID"].ToString());

            if (rows.Length > 0)
            {
                DataRow dr = rows[0];

                int convertfactor = string.IsNullOrEmpty(dr["ConvertFactor"].ToString()) ? 1 : (int)dr["ConvertFactor"];
                //dr["Price"] = decimal.Parse(((TextBox)gr.FindControl("tbx_Price")).Text);

                int quantity1 = int.Parse(((TextBox)gr.FindControl("tbx_Quantity1")).Text);
                int quantity2 = int.Parse(((TextBox)gr.FindControl("tbx_Quantity2")).Text);
                dr["Quantity"] = quantity1 * convertfactor + quantity2;
            }
        }
    }

    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (DateTime.Parse(this.tbx_VolumeDate.Text.Trim()) > DateTime.Now.Date)
        {
            MessageBox.Show(this, "发生日期不能超出当天日期！");
            return;
        }

        SaveMyViewState();
        int transferinclient = 0, transferoutclient = 0;
        if (int.TryParse(select_Client.SelectValue, out transferinclient) && int.TryParse(select_Supplier.SelectValue, out transferoutclient))
        {
            #region 更新销量内容

            #region 保存销量头信息
            SVM_SalesVolumeBLL bll;

            if ((int)ViewState["VolumeID"] == 0)
            {
                CM_ClientBLL _s = new CM_ClientBLL(transferoutclient);

                bll = new SVM_SalesVolumeBLL();
                bll.Model.Type = (int)ViewState["Type"];
                bll.Model.OrganizeCity = _s.Model.OrganizeCity;
                bll.Model.Supplier = transferoutclient;
                bll.Model.Client = transferinclient;
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Model.ApproveFlag = 2;

                if ((bool)ViewState["IsCXP"])
                    bll.Model.Flag = 16;                            //赠品调拨单
                else
                    bll.Model.Flag = 6;                              //成品调拨单
            }
            else
            {
                bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            }

            bll.Model.SalesDate = DateTime.Parse(this.tbx_VolumeDate.Text.Trim());
            bll.Model.AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
            bll.Model.Remark = tbx_Remark.Text;

            if ((int)ViewState["VolumeID"] == 0)
                bll.Model.ID = bll.Add();
            else
                bll.Update();
            #endregion

            #region 更新产品明细数据
            DataTable dt = (DataTable)ViewState["DTDetail"];
            foreach (DataRow dr in dt.Rows)
            {
                int product = (int)dr["ID"];
                int quantity = (int)dr["Quantity"];

                SVM_SalesVolume_Detail _detail = bll.Items.FirstOrDefault(m => m.Product == product);
                if (_detail == null)
                {
                    //销量明细里不存在该产品的记录
                    if (quantity == 0) continue;     //新增销量时，数量为0的不保存到数据库中

                    _detail = new SVM_SalesVolume_Detail();
                    _detail.Product = product;
                    _detail.SalesPrice = (decimal)dr["Price"];
                    _detail.Quantity = quantity;
                    _detail.FactoryPrice = (decimal)dr["FactoryPrice"];
                    bll.AddDetail(_detail);
                }
                else
                {
                    //销量明细里已存在该产品的记录
                    if (quantity == 0)
                    {
                        bll.DeleteDetail(_detail.ID);
                        continue;
                    }
                    else if (_detail.Quantity != quantity || _detail.SalesPrice != (decimal)dr["Price"])
                    {
                        _detail.SalesPrice = (decimal)dr["Price"];
                        _detail.Quantity = quantity;
                        bll.UpdateDetail(_detail);
                    }
                }
            }
            #endregion

            #endregion

            if (sender != null)
            {
                MessageBox.ShowAndRedirect(this, "填报数据保存成功!", "TransferVolumeDetail.aspx?VolumeID=" + bll.Model.ID.ToString());
            }
        }
        else
        {
            MessageBox.Show(this, "请正确选择调入及调出客户!");
            return;
        }


    }
    #endregion

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["VolumeID"] != 0)
        {
            SVM_SalesVolumeBLL bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bll.Delete();

            MessageBox.ShowAndRedirect(this, "数据删除成功!", "TransferVolumeList.aspx?TransferOutClientID=" +
                select_Supplier.SelectValue + "&TransferInClientID=" + select_Client.SelectValue);
        }
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if (ViewState["VolumeID"] != null)
        {
            bt_Save_Click(null, null);

            SVM_SalesVolumeBLL bll = new SVM_SalesVolumeBLL((int)ViewState["VolumeID"]);
            bll.Approve((int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "数据审核成功!", "TransferVolumeList.aspx?TransferOutClientID=" +
                select_Supplier.SelectValue + "&TransferInClientID=" + select_Client.SelectValue);
        }
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (bt_Save.Visible) SaveMyViewState();
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
