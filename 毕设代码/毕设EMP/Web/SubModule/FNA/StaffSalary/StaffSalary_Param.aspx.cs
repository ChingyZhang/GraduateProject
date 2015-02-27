using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
public partial class SubModule_FNA_StaffSalary_StaffSalary_Param : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }
    }
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);

        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
  
        #region 如果非总部职位，其只能选择自己职位及以下职位
        Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
        if (p != null && p.IsHeadOffice != "Y" && p.Remark != "OfficeHR")
        {
            tr_Position.RootValue = p.SuperID.ToString();
            tr_Position.SelectValue = staff.Model.Position.ToString();
           
        }
        else
        {
            tr_Position.RootValue = "1";
          
        }
        tr_Position.SelectValue = tr_Position.RootValue; 
        #endregion

    }
    private void BindGrid()
    {
        string condtion = "1=1";
        if (tr_Position.SelectValue != "1")
        {
            condtion = " AND Position=" + tr_Position.SelectValue;
        }
        gvList.ConditionString = condtion;
        gvList.BindGrid();
 
    }
    protected void gvList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = int.Parse(gvList.DataKeys[e.NewSelectedIndex][0].ToString());
   
        FNA_StaffSalary_Param m = new FNA_StaffSalary_ParamBLL(id).Model;
        UC_DetailView1.BindData(m); 
        ViewState["ID"] = id;
  

    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gvList.DataKeys[e.RowIndex][0].ToString());
        new FNA_StaffSalary_ParamBLL(id).Delete();
        MessageBox.Show(this, "删除成功！");
        ViewState["ID"] = null;
        BindGrid();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        FNA_StaffSalary_ParamBLL bll = ViewState["ID"] != null ? new FNA_StaffSalary_ParamBLL((int)ViewState["ID"]) : new FNA_StaffSalary_ParamBLL();

        UC_DetailView1.GetData(bll.Model);
        if (bll.Model.Position == 1)
        {
            MessageBox.Show(this, "请选择职位");
            return;
        }
        if (ViewState["ID"] != null)
        {
            bll.Update();
        }
        else
        {
            if ( FNA_StaffSalary_ParamBLL.GetModelList("Position="+bll.Model.Position.ToString()).Count>0)
            {
                MessageBox.Show(this, "对不起，该职位的绩效参数已维护，请勿重复！");
                return;
            }
            bll.Add();
        }
        UC_DetailView1.BindData(new FNA_StaffSalary_Param());
        ViewState["ID"] = null;
        BindGrid();
    }
}
