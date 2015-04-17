using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model;
using MCSFramework.BLL;

public partial class SubModule_UDM_DetailViewList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string condition = "";
        if (tbx_Find.Text != "")
            condition = "Name like '%" + tbx_Find.Text + "%' OR Code like '%" + tbx_Find.Text + "%'";

        IList<UD_DetailView> source = UD_DetailViewBLL.GetModelList(condition);
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.TotalRecordCount = source.Count;

        gv_List.BindGrid<UD_DetailView>(source);
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[e.NewSelectedIndex][0];
        UD_DetailView m = new UD_DetailViewBLL(id).Model;

        tbx_Code.Text = m.Code;
        tbx_Name.Text = m.Name;

        ViewState["ID"] = id;
        bt_SaveDetailView.Text = "修改";
    }
    protected void bt_SaveDetailView_Click(object sender, EventArgs e)
    {
        if (ViewState["ID"]==null)
        {
            UD_DetailViewBLL bll = new UD_DetailViewBLL();
            bll.Model.Code = tbx_Code.Text;
            bll.Model.Name = tbx_Name.Text;
            bll.Add();
        }
        else
        {
            UD_DetailViewBLL bll = new UD_DetailViewBLL((Guid)ViewState["ID"]);
            bll.Model.Code = tbx_Code.Text;
            bll.Model.Name = tbx_Name.Text;
            bll.Update();
        }
        tbx_Code.Text = "";
        tbx_Name.Text = "";
        ViewState["ID"] = null;
        bt_SaveDetailView.Text = "新增";
        BindGrid();

    }
}
