using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;

public partial class SubModule_PM_PM_SalaryLevelDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                this.txt_jzwcl.Enabled = true;
                this.txt_kswcl.Enabled = true;
                this.txt_tcxs.Enabled = true;
                this.btn_del_levelRate.Visible = true;
                this.btn_Salary_AddRate.Enabled = true;
                this.ud_grid_salary.Visible = true;
                this.btn_Salary_AddLevel.Text = "修  改";
                ((MCSControls.MCSWebControls.MCSTreeControl)this.panel2.FindControl("PM_SalaryLevel_OrganizeCity")).Enabled = false;
                BindData();
                //this.ud_grid_salary.Visible = false;
            }
        }
    }

    public void BindData()
    {
        PM_SalaryLevelBLL bll = new PM_SalaryLevelBLL((int)ViewState["ID"]);
        //panel1.BindData(bll.Model);
        panel2.BindData(bll.Model);
        ud_grid_salary.ConditionString = " MCS_Promotor.dbo.PM_SalaryLevelDetail.LevelID= " + (int)ViewState["ID"];
        ud_grid_salary.BindGrid();
    }
    protected void btn_Salary_AddRate_Click(object sender, EventArgs e)
    {
        PM_SalaryLevelBLL bll = new PM_SalaryLevelBLL((int)ViewState["ID"]);
        PM_SalaryLevelDetail item=new PM_SalaryLevelDetail();
        item.Complete1 = decimal.Parse(this.txt_kswcl.Text);
        item.Complete2 = decimal.Parse(this.txt_jzwcl.Text);
        item.Rate = decimal.Parse(this.txt_tcxs.Text);
        bll.AddDetail(item);
        this.txt_kswcl.Text = this.txt_jzwcl.Text;
        this.txt_tcxs.Text = "";
        this.txt_jzwcl.Text = "";
        BindData();
        
        
    }
    protected void btn_Salary_AddLevel_Click(object sender, EventArgs e)
    {
        PM_SalaryLevelBLL bll = null;
        if (btn_Salary_AddLevel.Text == "添  加")
        {
            bll = new PM_SalaryLevelBLL();
            bll.Model.InputStaff = (int)Session["UserID"];
            
        }
        else 
        {
            bll = new PM_SalaryLevelBLL((int)ViewState["ID"]);
            bll.Model.UpdateStaff = (int)Session["UserID"];
        }
        panel2.GetData(bll.Model);
        if (btn_Salary_AddLevel.Text == "添  加")
        {
            bll.Model.ApproveFlag = 2;
            ViewState["ID"] = bll.Add();
        }
        else 
        {
            bll.Update();
        }
        BindData();
        Response.Redirect("PM_SalaryLevelDetail.aspx?ID=" + (int)ViewState["ID"]);
    }
    protected void btn_del_levelRate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid_salary.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                int id = int.Parse(ud_grid_salary.DataKeys[gr.RowIndex]["PM_SalaryLevelDetail_ID"].ToString());

                PM_SalaryLevelBLL bll = new PM_SalaryLevelBLL((int)ViewState["ID"]);
                PM_SalaryLevelDetail pm = new PM_SalaryLevelDetail();
                bll.DeleteDetail(id);

                //ListTable<PM_SalaryLevelDetail> _details = ViewState["Details"] as ListTable<PM_SalaryLevelDetail>;
                //_details.Remove(id.ToString());
                //ViewState["Details"] = _details;

                //PM_SalaryLevelBLL bll = new PM_SalaryLevelBLL((int)ViewState["ID"]);
                //bll.Model.ID = (int)ViewState["ID"];
                ////PM_SalaryLevelDetail iitem = new PM_SalaryLevelDetail(int.Parse(ud_grid.DataKeys[gr.RowIndex]["ID"].ToString()));
                //bll.DeleteDetail();
            }
        }
        BindData();
    }
    protected void btn_Salary_ApprovLevel_Click(object sender, EventArgs e)
    {
        PM_SalaryLevelBLL bll = new PM_SalaryLevelBLL((int)ViewState["ID"]);
        bll.Model.ApproveFlag = 1;
        bll.Model.UpdateStaff = (int)Session["UserID"];
        bll.Update();
        BindData();
    }
}
