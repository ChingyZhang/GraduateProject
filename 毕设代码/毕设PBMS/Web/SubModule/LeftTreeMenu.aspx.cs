using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL;

public partial class SubModule_LeftTreeMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            GenarateMainMenu();
        }
    }

    #region 生成主菜单
    protected void GenarateMainMenu()
    {
        #region 生成主菜单datatable
        DataTable dt = Right_Module_BLL.GetBrowseMoudleByUser(Session["UserName"].ToString());
        DataRow[] RootRows = dt.Select("SuperID=1");
        if (dt != null && RootRows.Length > 0)
        {
            for (int i = 0; i < RootRows.Length; i++)
            {
                DataRow dr = RootRows[i];
                int superid = (int)dr["SuperID"];
                int menuid = (int)dr["ID"];

                if (superid == 1)
                {
                    AjaxControlToolkit.AccordionPane p = new AjaxControlToolkit.AccordionPane();
                    p.ID = "AccordionPane" + menuid.ToString();
                    HyperLink hy = new HyperLink();
                    hy.Text = (string)dr["Name"];
                    p.HeaderContainer.Controls.Add(hy);

                    TreeView tr = new TreeView();
                    tr.ImageSet = TreeViewImageSet.Custom;
                    tr.ShowExpandCollapse = true;
                    tr.ExpandImageUrl = "~/Images/icon/TreeExpand.gif";
                    tr.NoExpandImageUrl = "~/Images/icon/TreeNode.gif";
                    tr.CollapseImageUrl = "~/Images/icon/TreeCollapse.gif";

                    tr.SelectedNodeStyle.Font.Bold = true;
                    tr.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
                    tr.SelectedNodeStyle.BackColor = System.Drawing.Color.Yellow;
                    tr.EnableViewState = true;

                    GenareateSubMenu(dt, i, tr.Nodes, menuid);
                    tr.SelectedNodeChanged += tr_SelectedNodeChanged;
                    tr.TreeNodePopulate += tr_TreeNodePopulate;

                    p.ContentContainer.Controls.Add(tr);

                    Ac_MainMenu.Panes.Add(p);

                    #region 只有当前已选择的树节点及至所有父节点才展开，其余折叠
                    if (Session["ActiveRootNode"] != null && (int)Session["ActiveRootNode"] == i && ViewState["CurrentRootNode"] != null)
                    {
                        string nodepath = (string)ViewState["CurrentRootNode"];

                        TreeNode node = tr.FindNode(nodepath);
                        if (node != null)
                        {
                            //node.ImageUrl = "~/Images/icon/TreeCollapse.gif";
                            node.ExpandAll();
                        }
                    }
                    #endregion

                }
            }

            if (Session["ActiveRootNode"] != null) Ac_MainMenu.SelectedIndex = (int)Session["ActiveRootNode"];
            //wc.DataSource = dt;
        }
        else
            Response.Write("对不起,当前用户没有分配任何浏览权限!");
        #endregion
    }

    void tr_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {

        DataTable dtMenu = Right_Module_BLL.GetBrowseMoudleByUser(Session["UserName"].ToString());
        string[] values = e.Node.Value.Split(new char[] { '|' });
        int rootmenuindex = 0, menuid = 0;

        int.TryParse(values[0], out rootmenuindex);
        int.TryParse(values[1], out menuid);

        GenareateSubMenu(dtMenu, rootmenuindex, e.Node.ChildNodes, menuid);
    }

    void tr_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeView tr = (TreeView)sender;

        string[] values = tr.SelectedValue.Split(new char[] { '|' });
        if (values.Length != 3)
        {
            if (values.Length > 1)
            {
                //记下当前结点所属的主模块
                Session["ActiveRootNode"] = int.Parse(values[0]);
                Ac_MainMenu.SelectedIndex = (int)Session["ActiveRootNode"];
            }

            if (tr.SelectedNode.ChildNodes.Count == 0)
            {
                //如果没有生成子节点，则自动生成下级子节点
                int rootmenuindex = 0, menuid = 0;

                int.TryParse(values[0], out rootmenuindex);
                int.TryParse(values[1], out menuid);

                DataTable dtMenu = Right_Module_BLL.GetBrowseMoudleByUser(Session["UserName"].ToString());
                GenareateSubMenu(dtMenu, rootmenuindex, tr.SelectedNode.ChildNodes, menuid);
            }
            //tr.SelectedNode.ImageUrl = "~/Images/icon/TreeCollapse.gif";
            tr.SelectedNode.Expand();
            
        }
        //else
        //{
        //    //有设置最终跳转页面的树节点，记录当前主菜单ID及树节点ID，并跳转至相应页面
        //    Session["AcMenu_SelectIndex"] = int.Parse(values[0]);
        //    Session["ActiveModule"] = int.Parse(values[1]);
        //    if (values[2] != "")
        //    {
        //        Response.Redirect("~/SubModule/switch.aspx?Module=" + values[1]);
        //    }
        //}

    }

    private void GenareateSubMenu(DataTable dtMenu, int RootMenuIndex, TreeNodeCollection TNC, int SuperID)
    {
        foreach (DataRow row in dtMenu.Select("SuperID=" + SuperID.ToString()))
        {
            int menuid = (int)row["ID"];
            TreeNode tn = new TreeNode();
            tn.Text = (string)row["Name"];
            if (dtMenu.Select("SuperID=" + menuid.ToString()).Length == 0)
            {
                tn.Value = RootMenuIndex.ToString() + "|" + menuid.ToString() + "|" + row["Remark"].ToString();
                tn.NavigateUrl = "~/SubModule/switch.aspx?Action=1&Module=" + menuid.ToString() + "&ActiveRootNode=" + RootMenuIndex.ToString();
                tn.Target = "fr_Main";
            }
            else
            {
                tn.Value = RootMenuIndex.ToString() + "|" + menuid.ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
            }
            

            if (Session["ActiveModule"] != null && menuid == (int)Session["ActiveModule"])
            {
                tn.Select();
                //tn.Text = "<span style='color:red'>" + tn.Text + "</span>";
            }

            TNC.Add(tn);

            //if (dtMenu.Select("SuperID=" + menuid.ToString()).Length > 0 &&
            //    Session["ActiveRootNode"] != null && (int)Session["ActiveRootNode"] == RootMenuIndex)
            {
                //同主模块级下的树形先展开
                GenareateSubMenu(dtMenu, RootMenuIndex, tn.ChildNodes, menuid);
                tn.Collapse();
            }

            #region 获取当前已选择中节点的根节点，以便展开
            if (Session["ActiveModule"] != null && menuid == (int)Session["ActiveModule"])
            {
                TreeNode node = tn;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                ViewState["CurrentRootNode"] = node.ValuePath;
            }
            #endregion
        }
    }
    #endregion
}
