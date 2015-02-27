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
using MCSFramework.BLL.FNA;
using MCSFramework.Model;

public partial class SubModule_FNA_StaffSalary_StaffSalaryGenerate : System.Web.UI.Page
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
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);


        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate Between dateadd(month,-4,getdate()) AND getdate()");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-1)).ToString();
        #endregion
    }
    #endregion

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        int PositionType = 0;

        int.TryParse(ddl_PositionType.SelectedValue, out PositionType);
        //#region 判断是否选择的是办事处
        //if (tr_OrganizeCity.SelectValue != "1")
        //{
        //    Addr_OrganizeCity city = new Addr_OrganizeCityBLL(organizectiy).Model;
        //    if (city.Level != 1)
        //    {
        //        MessageBox.Show(this, "工资只能以办事处为单位进行发放！");
        //        return;
        //    }
        //}
        //else
        //{
        //    MessageBox.Show(this, "请选择要生成工资的办事处！");
        //    return;
        //}
        //#endregion

        int id = FNA_StaffSalaryBLL.GenerateStaffSalary(int.Parse(ddl_Month.SelectedValue), PositionType);

        if (id > 0)
            MessageBox.ShowAndRedirect(this, "员工工资生成成功！", "StaffSalaryDetailList.aspx?ID=" + id.ToString());
        else if (id == -1)
            MessageBox.Show(this, "对不起，该月的员工工资已经生成!");
        else if (id == -2)
            MessageBox.Show(this, "对不起，该管理片区不存在员工！");
        else
            MessageBox.Show(this, "对不起，员工工资生成失败！错误码:" + id.ToString());
    }
}
