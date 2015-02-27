using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.Common;
using System.Data;
using MCSFramework.BLL;

public partial class SubModule_AccountTitle_AccountTitle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = 0;
            BindDropdown();
            BindTree(tr_List.Nodes, 0);
            if (Request.QueryString["SuperID"] != null)
            {
                ExpandNode();
            }
            tree_SuperID.SelectValue = "1";
        }
    }

    #region Reload the TreeNode by special id
    private void ExpandNode()
    {
        int SuperID = int.Parse(Request.QueryString["SuperID"]);

        DataTable _fullpath = TreeTableBLL.GetFullPath("MCS_Pub.dbo.AC_AccountTitle", "ID", "SuperID", SuperID);
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


    private void BindDropdown()
    {
        //DataTable dt = TreeTableBLL.GetRelationTableSourceData("MCS_Pub.dbo.AC_AccountTitle", "ID", "Name");
        //tree_SuperID.DataSource = dt;

        cbl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType");
        cbl_FeeType.DataBind();
    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        AC_AccountTitleBLL _bll = new AC_AccountTitleBLL();
        IList<AC_AccountTitle> _modellist = _bll._GetModelList("SuperID=" + SuperID.ToString());

        foreach (AC_AccountTitle _model in _modellist)
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

        AC_AccountTitleBLL _bll = new AC_AccountTitleBLL(ID);
        lbl_ID.Text = _bll.Model.ID.ToString();
        tbx_Name.Text = _bll.Model.Name;
        if (_bll.Model.SuperID != 0)
        {
            tree_SuperID.SelectValue = _bll.Model.SuperID.ToString();
        }
        tbx_Code.Text = _bll.Model.Code;
        tbx_Description.Text = _bll.Model.Description;
        tbx_OverPercent.Text = _bll.Model["OverPercent"] == "" ? "0" : _bll.Model["OverPercent"];
        cbx_MustApplyFirst.Checked = _bll.Model["MustApplyFirst"] != "N";
        cbx_IsDisable.Checked = _bll.Model["IsDisable"] == "Y";
        cbx_CanApplyInGeneralFlow.Checked = _bll.Model["CanApplyInGeneralFlow"] != "N";
        txt_MonthsOverdue.Text = _bll.Model["MonthsOverdue"] == "" ? "1" : _bll.Model["MonthsOverdue"];
        txt_YFMonthsOverdue.Text = _bll.Model["YFMonthsOverdue"] == "" ? "1" : _bll.Model["YFMonthsOverdue"]; 
        if (_bll.Model.Level != 0)
            lbl_LevelName.Text = DictionaryBLL.GetDicCollections("AC_AccountTitleLevel")[_bll.Model.Level.ToString()].Name;

        if (_bll.Model.Level > 3)
            cbl_FeeType.Enabled = false;
        else
            cbl_FeeType.Enabled = true;

        cbl_FeeType.Items.Clear();
        cbl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType");
        cbl_FeeType.DataBind();

        foreach (AC_AccountTitleInFeeType item in AC_AccountTitleInFeeTypeBLL.GetModelList("AccountTitle=" + ID.ToString()))
        {
            cbl_FeeType.Items.FindByValue(item.FeeType.ToString()).Selected = true;
        }

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
        btn_Delete.Visible = AC_AccountTitleBLL.GetModelList("SuperID=" + ViewState["ID"].ToString()).Count == 0;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 1) return;

        AC_AccountTitleBLL _bll;
        if ((int)ViewState["ID"] == 0)
        {
            _bll = new AC_AccountTitleBLL();
        }
        else
        {
            _bll = new AC_AccountTitleBLL((int)ViewState["ID"]);
        }

        _bll.Model.Name = tbx_Name.Text;
        _bll.Model.SuperID = int.Parse(tree_SuperID.SelectValue);
        _bll.Model.Code = tbx_Code.Text;
        _bll.Model.Description = tbx_Description.Text;

        _bll.Model.Level = new AC_AccountTitleBLL(_bll.Model.SuperID).Model.Level + 1;
        _bll.Model["OverPercent"] = tbx_OverPercent.Text;
        _bll.Model["MustApplyFirst"] = cbx_MustApplyFirst.Checked ? "Y" : "N";
        _bll.Model["IsDisable"] = cbx_IsDisable.Checked ? "Y" : "N";
        _bll.Model["CanApplyInGeneralFlow"] = cbx_CanApplyInGeneralFlow.Checked ? "Y" : "N";
        int MonthsOverdue = 1, YFMonthsOverdue=1;
        int.TryParse(txt_YFMonthsOverdue.Text.Trim(), out YFMonthsOverdue);
        int.TryParse(txt_MonthsOverdue.Text.Trim(), out MonthsOverdue);
        _bll.Model["MonthsOverdue"] = MonthsOverdue.ToString();
        _bll.Model["YFMonthsOverdue"] = YFMonthsOverdue.ToString();

        if (DictionaryBLL.GetDicCollections("AC_AccountTitleLevel")[_bll.Model.Level.ToString()] == null)
        {
            lbl_AlertInfo.Text = "添加会计科目失败，等级超出范围";
            return;
        }

        if ((int)ViewState["ID"] == 0)
        {
            int ret = _bll.Add();
            if (ret < 0)
            {
                lbl_AlertInfo.Text = "添加会计科目失败，会计科目名称已存在";
                return;
            }
            _bll.Model.ID = ret;
        }
        else
        {
            if (_bll.Model.SuperID == _bll.Model.ID) return;
            int ret = _bll.Update();

            switch (ret)
            {
                case -1:
                    lbl_AlertInfo.Text = "更新会计科目失败，会计科目名称已存在";
                    return;
                case -2:
                    lbl_AlertInfo.Text = "更新会计科目失败，不能将当前会计科目设置为上级会计科目";
                    return;
                case -3:
                    lbl_AlertInfo.Text = "更新会计科目失败，不能将当前会计科目的子会计科目设置为上级会计科目";
                    return;
            }
        }

        IList<AC_AccountTitleInFeeType> lists = AC_AccountTitleInFeeTypeBLL.GetModelList("AccountTitle=" + _bll.Model.ID);
        foreach (ListItem item in cbl_FeeType.Items)
        {
            if (item.Selected)
            {
                if (lists.FirstOrDefault(p => p.FeeType.ToString() == item.Value) == null)
                {
                    AC_AccountTitleInFeeTypeBLL b = new AC_AccountTitleInFeeTypeBLL();
                    b.Model.AccountTitle = _bll.Model.ID;
                    b.Model.FeeType = int.Parse(item.Value);
                    b.Add();
                }
            }
            else
            {
                if (lists.FirstOrDefault(p => p.FeeType.ToString() == item.Value) != null)
                {
                    AC_AccountTitleInFeeTypeBLL b = new AC_AccountTitleInFeeTypeBLL(lists.FirstOrDefault(p => p.FeeType.ToString() == item.Value).ID);
                    b.Delete();
                }
            }
        }

        DataCache.RemoveCache("Cache-TreeTableBLL-GetAllNode-MCS_Pub.dbo.AC_AccountTitle");
        Response.Redirect("AccountTitle.aspx?SuperID=" + _bll.Model.SuperID.ToString());
    }

    protected void bt_AddSub_Click(object sender, EventArgs e)
    {
        tbx_Code.Text = "";
        tbx_Name.Text = "";
        tree_SuperID.SelectValue = ViewState["ID"].ToString();

        ViewState["ID"] = 0;
        btn_Save.Enabled = true;
        btn_Delete.Visible = false;
        bt_AddSub.Visible = false;
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        AC_AccountTitleBLL _bll = new AC_AccountTitleBLL(int.Parse(lbl_ID.Text));
        if (_bll.Delete() < 0)
        {
            lbl_AlertInfo.Text = "该会计科目包含下级会计科目，请勿删除";
            return;
        }
        lbl_AlertInfo.Text = "";
        Response.Redirect("AccountTitle.aspx?SuperID=" + _bll.Model.SuperID.ToString());
    }

    protected void tr_List_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.tr_List.SelectedNode.Value != "0")
            BindData(int.Parse(this.tr_List.SelectedNode.Value));

        if (this.tr_List.SelectedNode.ChildNodes.Count == 0)
        {
            BindTree(this.tr_List.SelectedNode.ChildNodes, int.Parse(this.tr_List.SelectedNode.Value));
        }
       
        this.tr_List.SelectedNode.Expand();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountTitle.aspx");
    }
}
