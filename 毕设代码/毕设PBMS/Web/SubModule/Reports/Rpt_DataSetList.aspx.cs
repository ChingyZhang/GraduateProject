// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetList.aspx.cs 
// 生成日期:2010/9/30 12:37:31 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.RPT;
using MCSFramework.BLL.RPT;

public partial class SubModule_Reports_Rpt_DataSetList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindTree(tr_List.Nodes, 0);

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {

        foreach (Rpt_Folder folder in Rpt_FolderBLL.GetModelList("SuperID=" + SuperID.ToString()))
        {
            TreeNode tn = new TreeNode();
            tn.Text = folder.Name;
            tn.Value = folder.ID.ToString();
            tn.ImageUrl = "~/Images/gif/gif-0030.gif";
            TNC.Add(tn);
            //if (folder.ID == 1)
            BindTree(tn.ChildNodes, folder.ID);
        }
    }
    protected void tr_List_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.tr_List.SelectedNode.ChildNodes.Count == 0)
            BindTree(this.tr_List.SelectedNode.ChildNodes, int.Parse(this.tr_List.SelectedNode.Value));

        this.tr_List.SelectedNode.Expand();

        gv_List.PageIndex = 0;
        BindGrid();
    }

    private void BindGrid()
    {
        string ConditionStr = "Enabled = '" + (MCSTabControl1.SelectedIndex == 0 ? "Y" : "N") + "' AND Folder = " + tr_List.SelectedValue;
        
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rpt_DataSetDetail.aspx");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}