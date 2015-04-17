using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_OA_KB_CatalogManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ViewState["Catalog"] = 1;
            BindDropDown();
            //BindGrid();
        }
    }

    private void BindDropDown()
    {
        BindTree(tr_Catalog.Nodes, 0);
    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        IList<KB_Catalog> _list = KB_CatalogBLL.GetModelList("SuperID=" + SuperID.ToString()); ;
        foreach (KB_Catalog _m in _list)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _m.Name;
            tn.Value = _m.ID.ToString();
            TNC.Add(tn);
            BindTree(tn.ChildNodes, _m.ID);
        }
    }

    protected void trPosition_SelectedNodeChanged(object sender, EventArgs e)
    {
        ViewState["Position"] = int.Parse(tr_Catalog.SelectedValue);
        //BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        KB_CatalogBLL bll = new KB_CatalogBLL((int)ViewState["Catalog"]);
        panel1.GetData(bll.Model);
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":


                break;
            case "1":

                ((TextBox)panel1.FindControl("KB_Catalog_Name")).Text = " ";
                break;

        }
        //BindGrid();
    }

    protected void tr_Catalog_SelectedNodeChanged(object sender, EventArgs e)
    {
        ViewState["Catalog"] = int.Parse(this.tr_Catalog.SelectedNode.Value);
        KB_CatalogBLL bll = new KB_CatalogBLL((int)ViewState["Catalog"]);
        panel1.BindData(bll.Model);
        if (int.Parse(MCSTabControl1.SelectedTabItem.Value.ToString()) == 1)
        {

            ((TextBox)panel1.FindControl("KB_Catalog_Name")).Text = " ";

        }

        //BindGrid();
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (int.Parse(MCSTabControl1.SelectedTabItem.Value.ToString()) == 0)
        {
            KB_CatalogBLL bll = new KB_CatalogBLL((int)ViewState["Catalog"]);
            panel1.GetData(bll.Model);
            bll.Update();
        }
        else if (int.Parse(MCSTabControl1.SelectedTabItem.Value.ToString()) == 1)
        {
            KB_CatalogBLL bll = new KB_CatalogBLL();
            panel1.GetData(bll.Model);

            if (bll.Model.SuperID == 0) bll.Model.SuperID = 1;

            bll.Add();
        }
        DataCache.RemoveCache("Cache-TreeTableBLL-GetRelationTableSourceData-MCS_OA.dbo.KB_Catalog-MCS_OA.dbo.KB_Catalog-ID-Name");
        DataCache.RemoveCache("Cache-TreeTableBLL-GetAllNode-MCS_OA.dbo.KB_Catalog");
        Response.Redirect("CatalogManager.aspx");
        //panel1.BindData(bll.Model);
    }
}
