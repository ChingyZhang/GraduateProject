using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;

public partial class SubModule_OA_KB_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Text"] != "")
            {
                txt_keyword.Text = Request.QueryString["Text"];
                btn_searcharticle_Click(null, null);
            }
        }
    }

    public void BindGrid()
    {
        ud_grid.ConditionString = " KB_Article.IsDelete = 'N' ";
        if (ViewState["ConditionString"] != null)
        {
            ud_grid.ConditionString = ud_grid.ConditionString + " and " + ViewState["ConditionString"];
        }

        ud_grid.BindGrid();
    }

    protected void btn_searcharticle_Click(object sender, EventArgs e)
    {
        if (!chb_title.Checked && !chb_keyword.Checked && !chb_content.Checked)
        {
            MessageBox.Show(this, "请选择要查询的范围!");
            return;
        }

        if (txt_keyword.Text.Trim() == "")
        {
            ud_grid.ConditionString = " KB_Article.IsDelete = 'N' ";
            ud_grid.BindGrid();
            return;
        }

        string condition = "";
        foreach (string con in txt_keyword.Text.Trim().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            string str = "";
            if (this.chb_title.Checked)
            {
                str += " KB_Article.Title like '%" + con + "%' ";
            }

            if (this.chb_content.Checked)
            {
                if (str != "") str += " OR ";
                str += " KB_Article.Content like '%" + con + "%' ";
            }
            if (this.chb_keyword.Checked)
            {
                if (str != "") str += " OR ";
                str += " KB_Article.KeyWord like '%" + con + "%' ";
            }
            str = "(" + str + ")";

            if (condition != "") condition += " AND ";
            condition += str;
        }


        if (chb_IncludePreCondition.Checked && ViewState["ConditionString"].ToString() != "")
        {
            if (!ViewState["ConditionString"].ToString().Contains(condition))
                condition += " AND " + ViewState["ConditionString"].ToString();
            else
                condition = ViewState["ConditionString"].ToString();
        }

        ud_grid.ConditionString = condition + " AND KB_Article.IsDelete = 'N' ";
        ud_grid.BindGrid();

        ViewState["ConditionString"] = condition;        
    }
}
