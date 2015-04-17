// ===================================================================
// 文件路径:CM/Distributor/DistributorDetail.aspx.cs 
// 生成日期:2008-12-19 10:11:21 
// 作者:	  yangwei
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.IFStrategy;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using System.Collections.Specialized;

public partial class CM_PBM_Partner_PartnerDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
        
                BindData();
            
        }


      

    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }

    #endregion

    private void BindData()
    {
        CM_Client m = new CM_ClientBLL((int)Session["OwnerClient"]).Model;
        if (m != null)
            pl_detail.BindData(m);
        else
            MessageBox.Show(this, "找不到经销商！");
       
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        int ClientID = (int)Session["OwnerClient"];
     
        _bll = new CM_ClientBLL(ClientID);

        pl_detail.GetData(_bll.Model);
        #region 判断修改项

        #endregion
        _bll.Model.UpdateStaff = (int)Session["UserID"];
        _bll.Model.OwnerClient = (int)Session["OwnerClient"];
        if(_bll.Update()==0)

        MessageBox.Show(this, "经销商资料修改成功！");

    }



    
}