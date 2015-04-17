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
public partial class SubModule_AddressManage_OfficialCityManage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            ViewState["ID"] = 0;
            BindTree(tr_List.Nodes, 0);
            if (Request.QueryString["SuperID"] != null)
            {
                ExpandNode();
            }
            //BindOfficialPopulation();
        }

    }

    #region Reload the TreeNode by special id

    #region 绑定下拉框
    private void BindDropDown()
    {
        ddl_CityAttributeFlag.DataSource = DictionaryBLL.GetDicCollections("Addr_OfficialCityAtrribute");
        ddl_CityAttributeFlag.DataBind();
        ddl_CityAttributeFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_CityAttributeFlag.SelectedValue = "0";

        ddl_RegionalizationType.DataSource = DictionaryBLL.GetDicCollections("HDM_HospitalClassify");
        ddl_RegionalizationType.DataBind();
        ddl_RegionalizationType.Items.Insert(0,new ListItem("所有", "0"));
        ddl_RegionalizationType.SelectedValue = "0";


    }
    #endregion
    private void ExpandNode()
    {
        int SuperID = int.Parse(Request.QueryString["SuperID"]);

        DataTable _fullpath = TreeTableBLL.GetFullPath("Addr_OfficialCity", "ID", "SuperID", SuperID);
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
            }
        }

    }

    #endregion

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        Addr_OfficialCityBLL _bll = new Addr_OfficialCityBLL();
        IList<Addr_OfficialCity> _modellist = _bll._GetModelList("SuperID=" + SuperID.ToString());

        foreach (Addr_OfficialCity _model in _modellist)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _model.Name;
            tn.Value = _model.ID.ToString();
            TNC.Add(tn);
            if (_model.ID == 1)
                BindTree(tn.ChildNodes, _model.ID);
        }
    }

    private void BindData(int ID)
    {
        ViewState["ID"] = ID;

        Addr_OfficialCityBLL _bll = new Addr_OfficialCityBLL(ID);
        lbl_ID.Text = _bll.Model.ID.ToString();
        tbx_Name.Text = _bll.Model.Name;
        tree_SuperID.SelectValue = _bll.Model.SuperID.ToString();
        tbx_Code.Text = _bll.Model.Code;
        tbx_CallAreaCode.Text = _bll.Model.CallAreaCode;
        tbx_PostCode.Text = _bll.Model.PostCode;
        tbx_Births.Text = _bll.Model.Births.ToString();
        ddl_CityAttributeFlag.SelectedValue = _bll.Model.Attribute.ToString();
        ddl_RegionalizationType.SelectedValue = _bll.Model.RegionalizationType.ToString();
        if (_bll.Model.Level != 0)
            lbl_LevelName.Text = DictionaryBLL.GetDicCollections("Addr_OfficialCityLevel")[_bll.Model.Level.ToString()].Name;

        btn_Save.Text = "修改";
        btn_Save.ForeColor = System.Drawing.Color.Red;
        btn_Delete.Visible = true;
        MessageBox.ShowConfirm(btn_Delete, "数据删除将不可恢复，确定删除么?");
        btn_Cancel.Visible = true;
        bt_AddSub.Visible = true;
        
        lbl_AlertInfo.Text = "";

        if ((int)ViewState["ID"] == 1)
        {
            btn_Save.Enabled = false;
        }
        else
        {
            btn_Save.Enabled = true;
        }

        btn_Delete.Visible = Addr_OfficialCityBLL.GetModelList("SuperID=" + ViewState["ID"].ToString()).Count == 0;

    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 1) return;

        Addr_OfficialCityBLL _bll;


        if ((int)ViewState["ID"] == 0)
        {
            _bll = new Addr_OfficialCityBLL();

        }
        else
        {
            _bll = new Addr_OfficialCityBLL((int)ViewState["ID"]);

        }

        _bll.Model.Name = tbx_Name.Text;
        _bll.Model.SuperID = int.Parse(tree_SuperID.SelectValue);
        _bll.Model.Code = tbx_Code.Text;
        _bll.Model.CallAreaCode = tbx_CallAreaCode.Text;
        _bll.Model.PostCode = tbx_PostCode.Text;
        _bll.Model.Level = new Addr_OfficialCityBLL(_bll.Model.SuperID).Model.Level + 1;
        _bll.Model.Births = int.Parse(tbx_Births.Text.Trim() == "" ? "0" : tbx_Births.Text.Trim());
        _bll.Model.Attribute = int.Parse(ddl_CityAttributeFlag.SelectedValue);
        _bll.Model.RegionalizationType = int.Parse(ddl_RegionalizationType.SelectedValue);
        if (DictionaryBLL.GetDicCollections("Addr_OfficialCityLevel")[_bll.Model.Level.ToString()] == null)
        {
            lbl_AlertInfo.Text = "添加城市失败，等级超出范围";
            return;
        }

        if ((int)ViewState["ID"] == 0)
        {
            int resultAdd = _bll.Add();
            if (resultAdd < 0)
            {
                lbl_AlertInfo.Text = "添加城市失败，城市名称已存在";
                return;
            }

        }
        else
        {
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

        Response.Redirect("OfficialCityManage.aspx?SuperID=" + _bll.Model.SuperID.ToString());
    }

    protected void bt_AddSub_Click(object sender, EventArgs e)
    {
        tbx_Code.Text = "";
        tbx_Name.Text = "";
        tree_SuperID.SelectValue = ViewState["ID"].ToString();
        Addr_OfficialCityBLL officialCityBLL = new Addr_OfficialCityBLL((int)ViewState["ID"]);
        ViewState["ID"] = 0;
        btn_Delete.Visible = false;
        bt_AddSub.Visible = false;
        btn_Save.Text = "保存";        

    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Addr_OfficialCityBLL _bll = new Addr_OfficialCityBLL(int.Parse(lbl_ID.Text));

        if (_bll.Delete() < 0)
        {
            lbl_AlertInfo.Text = "该城市包含下级城市，请勿删除";
            return;
        }
        Addr_OfficialCityPopulationBLL populationbll = new Addr_OfficialCityPopulationBLL(_bll.Model.ID);

        if (populationbll != null)
        {
            if (populationbll.Delete() < 0)
            {
                lbl_AlertInfo.Text = "删除该区域人口统计信息时出错！";
                return;
            }
        }

        lbl_AlertInfo.Text = "";
        Response.Redirect("OfficialCityManage.aspx?SuperID=" + _bll.Model.SuperID.ToString());
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
        Response.Redirect("OfficialCityManage.aspx");
    }
}
