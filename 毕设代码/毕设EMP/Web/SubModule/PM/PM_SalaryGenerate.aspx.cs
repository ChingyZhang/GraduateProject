using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Promotor;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;
using System.Data;
using MCSFramework.Model.Promotor;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

public partial class SubModule_PM_PM_SalaryGenerate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
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
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate Between dateadd(month,-4,getdate()) AND getdate()");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-1)).ToString();
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"";

    }
    #endregion

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        int organizecity = 0;
        int client = 0;
        int month = 0;

        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_Month.SelectedValue, out month);

        if (organizecity == 0)
        {
            MessageBox.Show(this, "请选择管理片区!");
            return;
        }

        if (client == 0)
        {
            MessageBox.Show(this, "请选择经销商!");
            return;
        }

        #region 判断指定区域下是否还有门店销量未审核

        Addr_OrganizeCityBLL _bll = new Addr_OrganizeCityBLL(organizecity);
        if (_bll.Model.Level < ConfigHelper.GetConfigInt("OrganizeCity-CityLevel"))
        {
            MessageBox.Show(this, "对不起，导购工资不能在营业部及以上层级生成!");
            return;
        }

        string citys = _bll.GetAllChildNodeIDs();
        if (citys == "")
            citys = organizecity.ToString();
        else
            citys += "," + organizecity.ToString();
        string condition = "Type=3 AND AccountMonth=" + month.ToString() +
            " AND Promotor IS NOT NULL AND OrganizeCity IN (" + citys + ") AND ApproveFlag=2 AND Flag=1";

        int counts = SVM_SalesVolumeBLL.GetModelList(condition).Count;
        if (counts > 0)
        {
            MessageBox.Show(this, "对不起，您区域还有" + counts.ToString() + "条导购销量未审核");
            return;
        }
        #endregion
      
        int id = PM_SalaryBLL.GenerateSalary(organizecity, client, month, (int)Session["UserID"]);

        if (id > 0)
            MessageBox.ShowAndRedirect(this, "导购员工资生成成功，请及时核对工资信息并提交！", "PM_SalaryList.aspx?Client=" + select_Client.SelectValue);
        else if (id == 0)
            MessageBox.Show(this, "对不起，该经销商下无导购员需要生成工资!");
        else if (id == -1)
            MessageBox.Show(this, "对不起，该月该经销商已有导购员工资生成!，请选择正确的会计月与经销商！");
        else if (id == -2)
            MessageBox.Show(this, "对不起，该经销商下不存在导购员！");
        else if (id == -3)
            MessageBox.ShowAndRedirect(this, "对不起，请先维护导购员的奖惩调整项,并且审核之后才能生成工资！", "PM_SalaryDataObject.aspx?AccountMonth=" + month.ToString());
        else if (id == -4)
        {            
            MessageBox.Show(this, "对不起，有部分导购员的薪酬定义没有正确维护，请正确设定薪酬定义信息后，再生成工资！");

        }
        else
            MessageBox.Show(this, "对不起，导购员工资生成失败！错误码:" + id.ToString());
    }
    protected string PromotorInClient(int promotor)
    {
        string clientname = "";
        IList<PM_PromotorInRetailer> lists = PM_PromotorInRetailerBLL.GetModelList("Promotor=" + promotor.ToString());
        foreach (PM_PromotorInRetailer item in lists)
        {
            CM_Client c = new CM_ClientBLL(item.Client).Model;
            if (c != null) clientname += "<a href='../SVM/SalesVolumeList.aspx?Type=3&SellOutClientID=" + c.ID.ToString() + "' target='_blank' class='listViewTdLinkS1'>"
                + c.ShortName + "</a><br/>";
        }
        return clientname;
    }
    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        bt_Generate.Enabled = true;
        int organizecity = 0;
        int client = 0;
        int month = 0;

        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_Month.SelectedValue, out month);
        DataTable promotors = PM_PromotorBLL.GetByDIClient(organizecity, client, month);

       
   
        if ( promotors.Select("ErrType=2").Count()> 0)
        {
            promotors.DefaultView.RowFilter = "ErrType=2";
            gv_List.DataSource = promotors.DefaultView.ToTable();
            gv_List.DataBind();
            gv_Table.Visible = true;
            bt_Generate.Enabled = false;
            MessageBox.Show(this, "对不起，有部分导购员的薪酬定义没有正确维护，请正确设定薪酬定义信息后，再生成工资！");            
        }



        if (promotors.Select("ErrType=3").Count() > 0)
        {
            promotors.DefaultView.RowFilter = "ErrType=3";
            gv_List.DataSource = promotors.DefaultView.ToTable();
            gv_List.DataBind();
            gv_Table.Visible = true;
            bt_Generate.Enabled = false;
            MessageBox.Show(this, "对不起，片区内有导购专兼职转换流程未完成，需审批完成后才可生成工资（生成工资前请到奖惩设定与调整项中刷新提成）！");           
        }

        if (promotors.Select("ErrType=1").Count() > 0)
        {
            promotors.DefaultView.RowFilter = "ErrType=1";
            gv_List.DataSource = promotors.DefaultView.ToTable();
            gv_List.DataBind();
            gv_Table.Visible = true;  
            MessageBox.Show(this, "以下导购员所在门店已生成返利费用或有生效返利协议，不能生成工资，请确认是否排除这些导购？");   
        }
    }
}
