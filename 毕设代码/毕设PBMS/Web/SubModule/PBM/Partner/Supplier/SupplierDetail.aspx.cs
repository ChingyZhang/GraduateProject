using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Common;

public partial class SubModule_PBM_Supplier_SupplierDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
        if (!IsPostBack)
        {
            BindData();
        }
   
    }
    private void BindData()
    {
        CM_Client m = new CM_ClientBLL(int.Parse(ViewState["ID"].ToString())).Model;
        pl_detail.BindData(m);
    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll;
        if ((int)ViewState["ID"] == 0)
        {
            //新增
            _bll = new CM_ClientBLL();
        }
        else
        {
            //修改
            _bll = new CM_ClientBLL((int)ViewState["ID"]);
        }
        //新增
        pl_detail.GetData(_bll.Model);

        

        if ((int)ViewState["ID"] == 0)
        {
            _bll.Model.ClientType = 1;
            _bll.Model.InsertStaff = int.Parse(Session["UserID"].ToString());
            _bll.Model.OwnerClient = int.Parse(Session["OwnerClient"].ToString());
            _bll.Model.OwnerType = int.Parse(Session["OwnerType"].ToString());
            if (_bll.Add() > 0)
            {   
                MessageBox.ShowAndRedirect(this,"新增成功！","SupplierList.aspx");
            }
            else
            {
                MessageBox.Show(this,"添加失败！");
            }
        }
         //修改
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this,"修改成功！","SupplierList.aspx");
            }
        }
    }

 
}