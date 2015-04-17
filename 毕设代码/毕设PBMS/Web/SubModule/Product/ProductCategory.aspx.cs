using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_Product_ProductCategory : System.Web.UI.Page
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

            //TDP维护自己的分类
            if ((int)Session["OwnerType"] == 3) Header.Attributes["WebPageSubCode"] = "OwnerType=3";
        }
    }

    #region Reload the TreeNode by special id
    private void ExpandNode()
    {
        DataTable _fullpath = TreeTableBLL.GetFullPath("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", (int)ViewState["ID"]);
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
                if (node != null)
                {
                    if (node.ChildNodes.Count == 0) BindTree(node.ChildNodes, _id);
                    node.Expand();

                    node.Selected = true;
                }
            }
        }

    }
    #endregion


    private void BindDropDown()
    {
        DataTable dt = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"]);
        tree_SuperID.DataSource = dt;
        tree_SuperID.DataBind();
    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        DataTable dt = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"], "");
        dt.DefaultView.RowFilter = "SuperID=" + SuperID.ToString();

        foreach (DataRowView dr in dt.DefaultView)
        {
            TreeNode tn = new TreeNode();
            tn.Text = dr["Name"].ToString();
            tn.Value = dr["ID"].ToString();
            tn.ImageUrl = "~/Images/gif/gif-0030.gif";
            TNC.Add(tn);
            //if (_model.ID == 1)
            BindTree(tn.ChildNodes, (int)dr["ID"]);
        }
    }

    private void BindData()
    {
        PDT_CategoryBLL _bll = new PDT_CategoryBLL((int)ViewState["ID"]);
        if (_bll.Model == null) return;

        lbl_ID.Text = _bll.Model.ID.ToString();
        tbx_Name.Text = _bll.Model.Name;
        tree_SuperID.SelectValue = _bll.Model.SuperID.ToString();
        ddl_EnabledFlag.SelectedValue = _bll.Model.EnabledFlag;

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
            if ((int)Session["OwnerType"] == _bll.Model.OwnerType && (int)Session["OwnerClient"] == _bll.Model.OwnerClient)
            {
                btn_Save.Enabled = true;
                btn_Delete.Enabled = true;
            }
            else
            {
                btn_Save.Enabled = false;
                btn_Delete.Enabled = false;
                //bt_AddSub.Enabled = false;
            }
        }

        btn_Delete.Enabled = PDT_CategoryBLL.GetModelList("SuperID=" + ViewState["ID"].ToString()).Count == 0;

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 1) return;

        PDT_CategoryBLL _bll;
        if ((int)ViewState["ID"] == 0)
        {
            _bll = new PDT_CategoryBLL();
        }
        else
        {
            _bll = new PDT_CategoryBLL((int)ViewState["ID"]);
        }

        _bll.Model.Name = tbx_Name.Text;
        _bll.Model.SuperID = int.Parse(tree_SuperID.SelectValue);
        _bll.Model.EnabledFlag = ddl_EnabledFlag.SelectedValue;

        if (_bll.Model.SuperID == 0) return;

        #region 为方便操作，分类最多开放至3级
        int level = 1, superid = _bll.Model.SuperID;
        while (superid != 1)
        {
            level++;
            superid = new PDT_CategoryBLL(superid).Model.SuperID;
        }
        if (level > 3)
        {
            MessageBox.Show(this, "商品级别超预设层级数!");
            return;
        }
        #endregion

        if ((int)ViewState["ID"] == 0)
        {
            _bll.Model.ApproveFlag = 1;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.OwnerType = (int)Session["OwnerType"];
            _bll.Model.OwnerClient = (int)Session["OwnerClient"];

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

        DataCache.RemoveCache("Cache-TreeTableBLL-GetAllNode-MCS_Pub.dbo.PDT_Category");
        tree_SuperID.DataSource = null;

        Response.Redirect("ProductCategory.aspx?ID=" + tree_SuperID.SelectValue);
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
        PDT_CategoryBLL _bll = new PDT_CategoryBLL(int.Parse(lbl_ID.Text));
        if (_bll.Delete() < 0)
        {
            lbl_AlertInfo.Text = "该目录包含下级目录，或该目录已被使用，请勿删除";
            return;
        }
        lbl_AlertInfo.Text = "";
        Response.Redirect("ProductCategory.aspx?SuperID=" + _bll.Model.SuperID.ToString());
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
        Response.Redirect("ProductCategory.aspx");
    }


}