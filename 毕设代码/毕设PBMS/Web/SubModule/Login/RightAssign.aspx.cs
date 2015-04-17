using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model;
using MCSFramework.BLL;
using System.Web.Security;
using System.Data;

public partial class SubModule_Login_RightAssign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindRoleTree(tr_Role.Nodes);
        }
    }

    private void BindRoleTree(TreeNodeCollection TNC)
    {
        TNC.Clear();
        string[] roles = Roles.GetAllRoles();

        foreach (string role in roles)
        {
            TreeNode tn = new TreeNode();
            tn.Text = role;
            tn.Value = role;
            if (Request.QueryString["RoleName"] != null && role == Request.QueryString["RoleName"])
            {
                tn.Selected = true;
            }
            TNC.Add(tn);
        }

        if (tr_Role.SelectedValue == "" && tr_Role.Nodes.Count > 0)
        {
            tr_Role.Nodes[0].Selected = true;
        }
        BindModuleTree();

    }

    private void BindModuleTree()
    {
        lb_RoleName.Text = tr_Role.SelectedValue;
        IList<Right_Assign> list = Right_Assign_BLL.GetModelList("RoleName='" + tr_Role.SelectedValue + "'");
        ViewState["RightAssign"] = list;
        tr_Module.Nodes.Clear();
        BindModuleTree(tr_Module.Nodes, 1);

        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    private void BindModuleTree(TreeNodeCollection TNC, int SuperID)
    {
        IList<Right_Assign> assignlist = (IList<Right_Assign>)ViewState["RightAssign"];

        IList<Right_Module> _moduleList = Right_Module_BLL.GetModelList("SuperID=" + SuperID.ToString() + " AND EnableFlag='Y'");
        foreach (Right_Module _module in _moduleList)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _module.Name + "(M:" + _module.ID.ToString() + ")";
            tn.Value = "M" + _module.ID.ToString();

            //判断有没有对该功能的浏览权限
            if (assignlist.FirstOrDefault(assign => assign.Module == _module.ID && assign.Action == 1) != null)
                tn.Checked = true;

            TNC.Add(tn);

            BindModuleTree(tn.ChildNodes, _module.ID);
        }

        IList<Right_Action> _actionlist = Right_ActionBLL.GetModelList("Module=" + SuperID.ToString());
        foreach (Right_Action _action in _actionlist)
        {
            TreeNode tn = new TreeNode();

            tn.Text = _action.Name + "(A:" + _action.ID.ToString() + ")";
            tn.Value = "A" + _action.ID.ToString();

            //判断有没有对该功能的浏览权限
            if (assignlist.FirstOrDefault(assign => assign.Module == SuperID && assign.Action == _action.ID) != null)
                tn.Checked = true;

            TNC.Add(tn);
        }
    }

    private void BindGrid()
    {
        IList<Right_Assign> list;
        if (tr_Module.SelectedValue != "")
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Right_Module", "ID", "SuperID", tr_Module.SelectedValue.Substring(1));
            string ids = tr_Module.SelectedValue.Substring(1) + ",";
            foreach (DataRow row in dt.Rows)
            {
                ids += row[0].ToString() + ",";
            }
            ids = ids.Substring(0, ids.Length - 1);
            list = Right_Assign_BLL.GetModelList("RoleName='" + tr_Role.SelectedValue + "' AND Module in (" + ids + ")");
        }
        else
        {
            list = (IList<Right_Assign>)ViewState["RightAssign"];
        }

        gv_list.TotalRecordCount = list.Count;
        gv_list.PageIndex = (int)ViewState["PageIndex"];
        gv_list.BindGrid<Right_Assign>(list);

    }

    protected void tr_Role_SelectedNodeChanged(object sender, EventArgs e)
    {
        BindModuleTree();
    }

    protected void tr_Module_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tr_Module.SelectedValue.StartsWith("M"))
        {
            ViewState["PageIndex"] = 0;
            BindGrid();
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        SaveRight(tr_Module.Nodes);

        BindModuleTree();
    }

    private void SaveRight(TreeNodeCollection TNC)
    {
        IList<Right_Assign> assignlist = (IList<Right_Assign>)ViewState["RightAssign"];
        foreach (TreeNode node in TNC)
        {
            int model = 1;
            int action = 1;
            if (node.Value.StartsWith("M"))
            {
                model = int.Parse(node.Value.Substring(1));
                action = 1;
            }
            else
            {
                action = int.Parse(node.Value.Substring(1));

                if (node.Parent != null)
                    model = int.Parse(node.Parent.Value.Substring(1));
            }

            Right_Assign assign = assignlist.FirstOrDefault(m => m.Module == model && m.Action == action);
            if (node.Checked)
            {
                if (assign == null)
                {
                    Right_Assign_BLL bll = new Right_Assign_BLL();
                    bll.Model.Module = model;
                    bll.Model.Action = action;
                    bll.Model.RoleName = tr_Role.SelectedValue;
                    bll.Model.Based_On = 2;
                    bll.Add();
                }
            }
            else
            {
                if (assign != null)
                {
                    Right_Assign_BLL bll = new Right_Assign_BLL(assign.ID);
                    bll.Delete();
                }
            }

            if (node.ChildNodes.Count > 0)
            {
                SaveRight(node.ChildNodes);
            }
        }
    }

    protected void bt_SaveGridView_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_list.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_check");

            if (cb_check.Checked)
            {
                Right_Assign_BLL bll = new Right_Assign_BLL((int)gv_list.DataKeys[row.RowIndex][0]);
                bll.Delete();
            }
        }
        BindModuleTree();
    }
    protected void gv_list_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
    protected void cb_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_list.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_check");
            cb_check.Checked = cb_CheckAll.Checked;
        }
    }
}
