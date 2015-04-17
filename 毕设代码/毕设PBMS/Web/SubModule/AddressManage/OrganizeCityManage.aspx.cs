using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using System.Data;
public partial class SubModule_AddressManage_OrganizeCityManage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = 0;
            BindDropDown();
            BindTree(tr_List.Nodes, 0);

            if (Request.QueryString["SuperID"] != null)
            {
                ExpandNode();
            }

        }
    }

    #region Reload the TreeNode by special id
    private void ExpandNode()
    {
        int SuperID = int.Parse(Request.QueryString["SuperID"]);

        DataTable _fullpath = TreeTableBLL.GetFullPath("Addr_OrganizeCity", "ID", "SuperID", SuperID);
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
                BindTree(tr_List.FindNode(_valuepath).ChildNodes, _id);
                tr_List.FindNode(_valuepath).Expand();
                //tr_List.FindNode(_valuepath).Select();
            }
        }
        //BindData(SuperID);
    }
    #endregion


    private void BindDropDown()
    {
        //DataTable dt = TreeTableBLL.GetRelationTableSourceData("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name");
        //tree_SuperID.DataSource = dt;
    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        Addr_OrganizeCityBLL _bll = new Addr_OrganizeCityBLL();
        IList<Addr_OrganizeCity> _modellist = _bll._GetModelList("SuperID=" + SuperID.ToString());

        foreach (Addr_OrganizeCity _model in _modellist)
        {
            TreeNode tn = new TreeNode();
            tn.Text = "";
            if (_model.Code != "") tn.Text = "(" + _model.Code + ")";
            tn.Text += _model.Name;
            tn.Value = _model.ID.ToString();
            TNC.Add(tn);
            if (_model.ID == 1)
                BindTree(tn.ChildNodes, _model.ID);
        }
    }

    private void BindData(int ID)
    {
        ViewState["ID"] = ID;

        Addr_OrganizeCityBLL _bll = new Addr_OrganizeCityBLL(ID);
        lbl_ID.Text = _bll.Model.ID.ToString();
        tbx_Name.Text = _bll.Model.Name;
        if (_bll.Model.SuperID != 0)
            tree_SuperID.SelectValue = _bll.Model.SuperID.ToString();
        tbx_Code.Text = _bll.Model.Code;

        if (_bll.Model.Manager != 0)
        {
            select_Manager.SelectValue = _bll.Model.Manager.ToString();
            select_Manager.SelectText = new Org_StaffBLL(_bll.Model.Manager).Model.RealName;
        }
        else
        {
            select_Manager.SelectValue = "";
            select_Manager.SelectText = "";
        }

        if (_bll.Model.Level != 0)
            lbl_LevelName.Text = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel")[_bll.Model.Level.ToString()].Name;

        tbx_ManageRegion.Text = _bll.Model["ManageRegion"];

        if (ID == 1)
        {
            btn_Save.Visible = false;
            btn_Delete.Visible = false;
        }
        else
        {
            btn_Save.Visible = true;
            btn_Delete.Visible = true;
        }
        bt_AddSub.Visible = true;
        MessageBox.ShowConfirm(btn_Delete, "数据删除将不可恢复，确定删除么?");

        btn_Delete.Visible = Addr_OrganizeCityBLL.GetModelList("SuperID=" + ViewState["ID"].ToString()).Count == 0;
        lbl_AlertInfo.Text = "";


        #region 绑定已关联的行政区县列表
        if (_bll.Model.Level.ToString() == ConfigHelper.GetConfigString("OrganizeCity-CityLevel"))
        {
            tb_OfficialCityInOrganizeCity.Visible = true;
            cb_CheckAll.Checked = false;
            cbl_OfficialList.Items.Clear();
            IList<Addr_OfficialCityInOrganizeCity> lists = Addr_OfficialCityInOrganizeCityBLL.GetModelList("OrganizeCity = " + ID.ToString());
            foreach (Addr_OfficialCityInOrganizeCity e in lists)
            {
                Addr_OfficialCityBLL city = new Addr_OfficialCityBLL(e.OfficialCity);
                string fullname = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", city.Model.ID);
                ListItem item = new ListItem(fullname, e.ID.ToString());

                cbl_OfficialList.Items.Add(item);
            }
            bt_AddOfficialCity.OnClientClick = "PopAddOfficialCity(" + ID.ToString() + ")";
        }
        else
        {
            tb_OfficialCityInOrganizeCity.Visible = false;
        }
        #endregion
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 1) return;

        Addr_OrganizeCityBLL _bll;
        if ((int)ViewState["ID"] == 0)
        {
            _bll = new Addr_OrganizeCityBLL();
        }
        else
        {
            _bll = new Addr_OrganizeCityBLL((int)ViewState["ID"]);
        }

        _bll.Model.Name = tbx_Name.Text;
        _bll.Model.SuperID = int.Parse(tree_SuperID.SelectValue);
        _bll.Model.Code = tbx_Code.Text;
        _bll.Model.Level = new Addr_OrganizeCityBLL(_bll.Model.SuperID).Model.Level + 1;

        if (select_Manager.SelectValue != "")
            _bll.Model.Manager = int.Parse(select_Manager.SelectValue);

        if (DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel")[_bll.Model.Level.ToString()] == null)
        {
            lbl_AlertInfo.Text = "添加城市失败，等级超出范围";
            return;
        }

        _bll.Model["ManageRegion"] = tbx_ManageRegion.Text;
        if ((int)ViewState["ID"] == 0)
        {
            if (_bll.Add() < 0)
            {
                lbl_AlertInfo.Text = "添加城市失败。";
                return;
            }
        }
        else
        {
            if (_bll.Model.SuperID == _bll.Model.ID) return;
            int ret = _bll.Update();

            switch (ret)
            {
                case -1:
                    lbl_AlertInfo.Text = "更新城市失败，城市名称已存在";
                    return;
                case -2:
                    lbl_AlertInfo.Text = "更新城市失败，不能将当前城市设置为上级城市";
                    return;
                case -3:
                    lbl_AlertInfo.Text = "更新城市失败，不能将当前城市的子城市设置为上级城市";
                    return;
            }
        }

        DataCache.RemoveCache("Cache-TreeTableBLL-GetAllNode-MCS_SYS.dbo.Addr_OrganizeCity");
        Response.Redirect("OrganizeCityManage.aspx?SuperID=" + _bll.Model.SuperID.ToString());
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Addr_OrganizeCityBLL _bll = new Addr_OrganizeCityBLL(int.Parse(lbl_ID.Text));
        if (_bll.Delete() < 0)
        {
            lbl_AlertInfo.Text = "该片区包含下级片区，请勿删除";
            return;
        }
        lbl_AlertInfo.Text = "";
        Response.Redirect("OrganizeCityManage.aspx?SuperID=" + _bll.Model.SuperID.ToString());
    }

    protected void tr_List_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.tr_List.SelectedNode.Value != "0")
            BindData(int.Parse(this.tr_List.SelectedNode.Value));

        if (this.tr_List.SelectedNode.ChildNodes.Count == 0)
            BindTree(this.tr_List.SelectedNode.ChildNodes, int.Parse(this.tr_List.SelectedNode.Value));
        
        this.tr_List.SelectedNode.Expand();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrganizeCityManage.aspx");
    }
    protected void bt_AddSub_Click(object sender, EventArgs e)
    {
        tbx_Code.Text = "";
        tbx_Name.Text = "";
        tree_SuperID.SelectValue = ViewState["ID"].ToString();

        select_Manager.SelectValue = "";
        select_Manager.SelectText = "";
        ViewState["ID"] = 0;
        btn_Delete.Visible = false;
        bt_AddSub.Visible = false;
    }
    protected void bt_SyncManager_Click(object sender, EventArgs e)
    {
        Addr_OrganizeCityBLL.SyncManager();
        MessageBox.Show(this, "同步结束！");
    }
    protected void cb_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbl_OfficialList.Items)
        {
            if (item.Enabled)
            {
                item.Selected = cb_CheckAll.Checked;
            }
        }
    }
    protected void bt_DeleteOfficialCity_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in cbl_OfficialList.Items)
        {
            if (item.Selected && item.Enabled)
            {
                Addr_OfficialCityInOrganizeCityBLL bll = new Addr_OfficialCityInOrganizeCityBLL(int.Parse(item.Value));
                bll.Delete();
            }
        }
        BindData((int)ViewState["ID"]);
    }
    protected void bt_AddOfficialCity_Click(object sender, EventArgs e)
    {
        BindData((int)ViewState["ID"]);
    }
}
