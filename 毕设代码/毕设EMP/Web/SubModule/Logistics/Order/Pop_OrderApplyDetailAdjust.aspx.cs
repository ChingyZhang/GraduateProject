using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;

public partial class SubModule_Logistics_Order_Pop_OrderApplyDetailAdjust : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }

            BindData();
        }
    }

    private void BindData()
    {
        ORD_OrderApplyDetail m = new ORD_OrderApplyBLL().GetDetailModel((int)ViewState["ID"]);
        PDT_Product product = new PDT_ProductBLL(m.Product).Model;
        lb_Product.Text = product.FullName;

        img_Product.ImageUrl = ATMT_AttachmentBLL.GetFirstPreviewPicture(11, m.Product);
        if (img_Product.ImageUrl == "") img_Product.Visible = false;

        lb_Package1.Text = DictionaryBLL.GetDicCollections("PDT_Packaging")[product.TrafficPackaging.ToString()].Name;
        lb_Package2.Text = lb_Package1.Text;

        lb_BookQuantity_T.Text = (m.BookQuantity / product.ConvertFactor).ToString();
        tbx_ApproveQuantity_T.Text = ((m.BookQuantity + m.AdjustQuantity) / product.ConvertFactor).ToString();
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ORD_OrderApplyDetail m = new ORD_OrderApplyBLL().GetDetailModel((int)ViewState["ID"]);
        PDT_Product product = new PDT_ProductBLL(m.Product).Model;

        if (int.Parse(tbx_ApproveQuantity_T.Text) * product.ConvertFactor > m.BookQuantity)
        {
            MessageBox.Show(this, "批复请购数量不能超过申请请购数量！");
            return;
        }

        if (int.Parse(tbx_ApproveQuantity_T.Text) * product.ConvertFactor == m.BookQuantity + m.AdjustQuantity)
        {
            MessageBox.Show(this, "批复请购数量与前一次调整值相同，没有发生变化，不需要调整！");
            return;
        }

        decimal OldAdjustQuantity = m.AdjustQuantity;

        m.AdjustQuantity = int.Parse(tbx_ApproveQuantity_T.Text) * product.ConvertFactor - m.BookQuantity;
        m.AdjustReason += "批复人：【" + Session["UserRealName"].ToString() + "】 批复数量：" + tbx_ApproveQuantity_T.Text + " 调整原因：" + tbx_AdjustReason.Text + "<br/>";


        ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL(m.ApplyID);
        bll.UpdateDetail(m);

        Session["SuccessFlag"] = true;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
    }
}
