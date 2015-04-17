// ===================================================================
// 文件路径:SubModule/Reports/Rpt_ReportList.aspx.cs 
// 生成日期:2010/10/7 20:23:58 
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
using MCSFramework.BLL.RPT;
using MCSFramework.Model.RPT;

public partial class SubModule_Reports_Rpt_ReportList : System.Web.UI.Page
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
        IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetAssignedRightByUser(Session["UserName"].ToString());

        foreach (Rpt_Folder folder in Rpt_FolderBLL.GetModelList("SuperID=" + SuperID.ToString()))
        {
            if (folder.ID > 1 && rights.FirstOrDefault(p => p.Folder == folder.ID) == null)
            {
                continue;
            }
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
        if (tr_List.SelectedNode != null)
        {
            bt_Add.Visible = false;
            gv_List.Columns[1].Visible = false;

            int folder = int.Parse(tr_List.SelectedNode.Value);

            IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetAssignedRightByUser(Session["UserName"].ToString()).Where(p => p.Folder == folder).ToList();

            string ConditionStr = " Rpt_Report.Folder =  " + folder.ToString();
            if (rights.Where(p => p.Action == 1 || p.Action == 2).Count() == 0 && rights.Where(p => p.Action == 3).Count() > 0)
            {
                //只有创建、查看自己报表的权限
                ConditionStr += " AND Rpt_Report.InsertStaff = " + Session["UserID"].ToString();

                bt_Add.Visible = true;                                                            //新增报表
                gv_List.Columns[1].Visible = true;                                         //设计报表
            }
            else if (rights.Where(p => p.Action == 2).Count() > 0)
            {
                bt_Add.Visible = true;                                                            //新增报表
                gv_List.Columns[1].Visible = true;                                         //设计报表
            }

            gv_List.ConditionString = ConditionStr;
            gv_List.BindGrid();

        }
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if (tr_List.SelectedValue == "" || tr_List.SelectedValue == "1")
            MessageBox.Show(this, "请选择要新增报表的目录!");
        else
            Response.Redirect("Rpt_ReportDetail.aspx?Folder=" + tr_List.SelectedValue);
    }
}