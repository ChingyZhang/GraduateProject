using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL;
using MCSFramework.BLL.RPT;
using MCSFramework.Model.RPT;
using MCSFramework.Common;
using System.Web.Security;

public partial class SubModule_Reports_Rpt_FolderManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            BindDropDown();
            BindTree(tr_List.Nodes, 0);

            if ((int)ViewState["ID"] != 0)
            {
                ExpandNode();
                BindData();
            }

        }
    }

    #region Reload the TreeNode by special id
    private void ExpandNode()
    {
        DataTable _fullpath = TreeTableBLL.GetFullPath("MCS_reports.dbo.Rpt_Folder", "ID", "SuperID", (int)ViewState["ID"]);
        for (int i = 0; i < _fullpath.Rows.Count; i++)
        {
            int _id = int.Parse(_fullpath.Rows[i]["ID"].ToString());
            if (_id != 1)
            {
                string _valuepath = "";
                for (int j = 0; j <= i; j++)
                {
                    _valuepath += _fullpath.Rows[j]["ID"].ToString() + "/";
                }
                _valuepath = _valuepath.Substring(0, _valuepath.Length - 1);

                TreeNode node = tr_List.FindNode(_valuepath);
                if (node.ChildNodes.Count == 0) BindTree(node.ChildNodes, _id);
                node.Expand();

                node.Selected = true;
            }
        }

    }
    #endregion


    private void BindDropDown()
    {

    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        Rpt_FolderBLL _bll = new Rpt_FolderBLL();
        IList<Rpt_Folder> _modellist = _bll._GetModelList("SuperID=" + SuperID.ToString());

        foreach (Rpt_Folder _model in _modellist)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _model.Name;
            tn.Value = _model.ID.ToString();
            tn.ImageUrl = "~/Images/gif/gif-0030.gif";
            TNC.Add(tn);
            //if (_model.ID == 1)
            BindTree(tn.ChildNodes, _model.ID);
        }
    }

    private void BindData()
    {
        Rpt_FolderBLL _bll = new Rpt_FolderBLL((int)ViewState["ID"]);
        lbl_ID.Text = _bll.Model.ID.ToString();
        tbx_Name.Text = _bll.Model.Name;
        tree_SuperID.SelectValue = _bll.Model.SuperID.ToString();

        btn_Save.Text = "修改";
        btn_Save.ForeColor = System.Drawing.Color.Red;
        btn_Delete.Enabled = true;
        MessageBox.ShowConfirm(btn_Delete, "数据删除将不可恢复，确定删除么?");
        btn_Cancel.Enabled = true;
        bt_AddSub.Enabled = true;

        lbl_AlertInfo.Text = "";

        if ((int)ViewState["ID"] == 1)
        {
            btn_Save.Enabled = false;
        }
        else
        {
            btn_Save.Enabled = true;
            BindRight();
        }

        btn_Delete.Enabled = Rpt_FolderBLL.GetModelList("SuperID=" + ViewState["ID"].ToString()).Count == 0;


    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 1) return;

        Rpt_FolderBLL _bll;
        if ((int)ViewState["ID"] == 0)
        {
            _bll = new Rpt_FolderBLL();
        }
        else
        {
            _bll = new Rpt_FolderBLL((int)ViewState["ID"]);
        }

        _bll.Model.Name = tbx_Name.Text;
        _bll.Model.SuperID = int.Parse(tree_SuperID.SelectValue);

        _bll.Model.Level = new Rpt_FolderBLL(_bll.Model.SuperID).Model.Level + 1;

        if ((int)ViewState["ID"] == 0)
        {
            int ret = _bll.Add();
            if (ret < 0)
            {
                lbl_AlertInfo.Text = "添加目录失败！";
                return;
            }
            else
                ViewState["ID"] = ret;
        }
        else
        {
            if (_bll.Model.SuperID == _bll.Model.ID) return;
            int ret = _bll.Update();

            switch (ret)
            {
                case -1:
                    lbl_AlertInfo.Text = "更新目录失败!";
                    return;
                case -2:
                    lbl_AlertInfo.Text = "更新目录失败，不能将当前目录设置为上级目录";
                    return;
                case -3:
                    lbl_AlertInfo.Text = "更新目录失败，不能将当前目录的子目录设置为上级目录";
                    return;
            }
        }

        SaveRight();

        DataCache.RemoveCache("Cache-TreeTableBLL-GetAllNode-MCS_Reports.dbo.Rpt_Folder");
        tree_SuperID.DataSource = null;

        Response.Redirect("Rpt_FolderManage.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_AddSub_Click(object sender, EventArgs e)
    {
        tbx_Name.Text = "";
        tree_SuperID.SelectValue = ViewState["ID"].ToString();

        ViewState["ID"] = 0;
        btn_Delete.Enabled = false;
        bt_AddSub.Enabled = false;
        btn_Save.Text = "保存";
        btn_Save.Enabled = true;
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Rpt_FolderBLL _bll = new Rpt_FolderBLL(int.Parse(lbl_ID.Text));
        if (_bll.Delete() < 0)
        {
            lbl_AlertInfo.Text = "该目录包含下级目录，请勿删除";
            return;
        }
        lbl_AlertInfo.Text = "";
        Response.Redirect("Rpt_FolderManage.aspx?SuperID=" + _bll.Model.SuperID.ToString());
    }

    protected void tr_List_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.tr_List.SelectedNode.Value != "0")
        {
            ViewState["ID"] = int.Parse(tr_List.SelectedNode.Value);
            BindData();
        }
        if (this.tr_List.SelectedNode.ChildNodes.Count == 0)
            BindTree(this.tr_List.SelectedNode.ChildNodes, int.Parse(this.tr_List.SelectedNode.Value));

        this.tr_List.SelectedNode.Expand();
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rpt_FolderManage.aspx");
    }

    #region 权限设计
    private void BindRight()
    {
        cbx_RoleList1.DataSource = Roles.GetAllRoles();
        cbx_RoleList1.DataBind();

        cbx_RoleList2.DataSource = Roles.GetAllRoles();
        cbx_RoleList2.DataBind();

        cbx_RoleList3.DataSource = Roles.GetAllRoles();
        cbx_RoleList3.DataBind();

        IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetModelList("Folder = " + ViewState["ID"].ToString());
        foreach (Rpt_FolderRight r in rights)
        {
            if (r.Based_On == 2)
            {
                ListItem item = null;
                switch (r.Action)
                {
                    case 1:
                        item = cbx_RoleList1.Items.FindByText(r.RoleName);
                        break;
                    case 2:
                        item = cbx_RoleList2.Items.FindByText(r.RoleName);
                        break;
                    case 3:
                        item = cbx_RoleList3.Items.FindByText(r.RoleName);
                        break;
                    default:
                        break;
                }
                if (item != null) item.Selected = true;
            }
        }
    }

    private void SaveRight()
    {
        foreach (ListItem item in cbx_RoleList2.Items)
        {
            IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetModelList("Folder = " + ViewState["ID"].ToString() +
                " AND Action = 2 AND Based_On = 2 AND RoleName='" + item.Text + "'");
            if (item.Selected)
            {
                if (rights.Count == 0)
                {
                    Rpt_FolderRightBLL bll = new Rpt_FolderRightBLL();
                    bll.Model.Folder = (int)ViewState["ID"];
                    bll.Model.Action = 2;
                    bll.Model.Based_On = 2;
                    bll.Model.RoleName = item.Text;
                    bll.Model.InsertStaff = (int)Session["UserID"];
                    bll.Add();
                }
            }
            else
            {
                if (rights.Count > 0) new Rpt_FolderRightBLL(rights[0].ID).Delete();
            }
        }

        foreach (ListItem item in cbx_RoleList1.Items)
        {
            IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetModelList("Folder = " + ViewState["ID"].ToString() +
                " AND Action = 1 AND Based_On = 2 AND RoleName='" + item.Text + "'");
            if (item.Selected)
            {
                if (rights.Count == 0 && !cbx_RoleList2.Items.FindByText(item.Text).Selected)
                {
                    Rpt_FolderRightBLL bll = new Rpt_FolderRightBLL();
                    bll.Model.Folder = (int)ViewState["ID"];
                    bll.Model.Action = 1;
                    bll.Model.Based_On = 2;
                    bll.Model.RoleName = item.Text;
                    bll.Model.InsertStaff = (int)Session["UserID"];
                    bll.Add();
                }
            }
            else
            {
                if (rights.Count > 0) new Rpt_FolderRightBLL(rights[0].ID).Delete();
            }
        }


        foreach (ListItem item in cbx_RoleList3.Items)
        {
            IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetModelList("Folder = " + ViewState["ID"].ToString() +
                " AND Action = 3 AND Based_On = 2 AND RoleName='" + item.Text + "'");
            if (item.Selected)
            {
                if (rights.Count == 0 && !cbx_RoleList1.Items.FindByText(item.Text).Selected && !cbx_RoleList2.Items.FindByText(item.Text).Selected)
                {
                    Rpt_FolderRightBLL bll = new Rpt_FolderRightBLL();
                    bll.Model.Folder = (int)ViewState["ID"];
                    bll.Model.Action = 3;
                    bll.Model.Based_On = 2;
                    bll.Model.RoleName = item.Text;
                    bll.Model.InsertStaff = (int)Session["UserID"];
                    bll.Add();
                }
            }
            else
            {
                if (rights.Count > 0) new Rpt_FolderRightBLL(rights[0].ID).Delete();
            }
        }
    }
    #endregion

    protected void cbx_SelectAll1_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbx_RoleList1.Items)
        {
            item.Selected = cbx_SelectAll1.Checked;
        }
    }
    protected void cbx_SelectAll2_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbx_RoleList2.Items)
        {
            item.Selected = cbx_SelectAll2.Checked;
        }
    }
    protected void cbx_SelectAll3_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbx_RoleList3.Items)
        {
            item.Selected = cbx_SelectAll3.Checked;
        }
    }
}
