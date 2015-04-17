using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;

public partial class SubModule_StaffManage_PositionManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindTree(trPosition.Nodes, 0);
            cbx_Enable.Checked = true;

            if (Request.QueryString["SuperID"] != null)
            {
                tr_SuperPosition.SelectValue = Request.QueryString["SuperID"];
            }
            else
            {
                if (trPosition.Nodes.Count > 0)
                {
                    trPosition.Nodes[0].Selected = true;
                    BindData(int.Parse(trPosition.Nodes[0].Value));
                }
            }
        }
    }

    private void BindDropDown()
    {
        //SuperPosition
        tr_SuperPosition.DataSource = Org_PositionBLL.GetAllPostion();

        //Depart
        ddl_Department.DataSource = DictionaryBLL.GetDicCollections("PUB_Department");
        ddl_Department.DataBind();
        ddl_Department.Items.Insert(0, new ListItem("请选择...", "0"));
    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        IList<Org_Position> _positionlist = Org_PositionBLL.GetModelList("SuperID=" + SuperID.ToString());
        foreach (Org_Position _position in _positionlist)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _position.Name + "(" + _position.ID.ToString() + ")";
            tn.Value = _position.ID.ToString();
            TNC.Add(tn);
            BindTree(tn.ChildNodes, _position.ID);
        }
    }

    private void BindData(int ID)
    {
        Org_PositionBLL _bll = new Org_PositionBLL(ID);
        lbl_ID.Text = _bll.Model.ID.ToString();
        tbx_Name.Text = _bll.Model.Name;
        tr_SuperPosition.SelectValue = _bll.Model.SuperID.ToString();
        cbx_IsHeadOffice.Checked = _bll.Model.IsHeadOffice == "Y";
        if (_bll.Model.Department != null)
            ddl_Department.SelectedValue = _bll.Model.Department.ToString();
        cbx_Enable.Checked = _bll.Model.Enabled == "Y";
        tbx_Remark.Text = _bll.Model.Remark;

        btn_AddSubPosition.Visible = true;
        btn_Delete.Visible = true;
        MessageBox.ShowConfirm(btn_Delete, "数据删除将不可恢复，确定删除么?");
        btn_Cancel.Visible = true;

        lbl_AlertInfo.Text = "";
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Org_PositionBLL _bll;
        if (lbl_ID.Text == "")
        {
            _bll = new Org_PositionBLL();
        }
        else
        {
            _bll = new Org_PositionBLL(int.Parse(lbl_ID.Text));
        }

        _bll.Model.Name = tbx_Name.Text;
        _bll.Model.SuperID = int.Parse(tr_SuperPosition.SelectValue);
        _bll.Model.IsHeadOffice = cbx_IsHeadOffice.Checked ? "Y" : "N";
        _bll.Model.Department = int.Parse(ddl_Department.SelectedValue);
        _bll.Model.Enabled = cbx_Enable.Checked ? "Y" : "N";
        _bll.Model.Remark = tbx_Remark.Text;

        if (lbl_ID.Text == "")
        {
            if (_bll.Add() < 0)
            {
                lbl_AlertInfo.Text = "添加职务失败，职务名称已存在";
                return;
            }
        }
        else
        {
            int ret = _bll.Update();

            switch (ret)
            {
                case -1:
                    lbl_AlertInfo.Text = "更新职务失败，职务名称已存在";
                    return;
                case -2:
                    lbl_AlertInfo.Text = "更新职务失败，不能将当前职务设置为上级职务";
                    return;
                case -3:
                    lbl_AlertInfo.Text = "更新职务失败，不能将当前职务的子职务设置为上级职务";
                    return;
            }
        }

        Response.Redirect("PositionManage.aspx");
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Org_PositionBLL _bll = new Org_PositionBLL(int.Parse(lbl_ID.Text));
        if (_bll.Delete() < 0)
        {
            lbl_AlertInfo.Text = "该职务包含下级职务，请勿删除";
            return;
        }
        lbl_AlertInfo.Text = "";
        Response.Redirect("PositionManage.aspx");
    }

    protected void trPosition_SelectedNodeChanged(object sender, EventArgs e)
    {
        BindData(int.Parse(this.trPosition.SelectedNode.Value));
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PositionManage.aspx");
    }
    protected void btn_AddSubPosition_Click(object sender, EventArgs e)
    {
        Response.Redirect("PositionManage.aspx?SuperID=" + trPosition.SelectedValue);
    }
}
