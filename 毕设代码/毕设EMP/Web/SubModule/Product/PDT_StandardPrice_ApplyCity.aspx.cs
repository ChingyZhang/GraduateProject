using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Pub;

public partial class SubModule_Product_PDT_StandardPrice_ApplyCity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PriceID"] = Request.QueryString["PriceID"] == null ? 0 : int.Parse(Request.QueryString["PriceID"]);

            if ((int)ViewState["PriceID"] > 0)
            {
                BindCheckBoxList();
            }
            else
            {
                MessageBox.ShowAndClose(this, "无标准价表ID参数!");
                return;
            }
        }
    }

    private void BindCheckBoxList()
    {
        if ((int)ViewState["PriceID"] > 0)
        {
            cbl_ApplyCity.Items.Clear();

            PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

            foreach (PDT_StandardPrice_ApplyCity city in bll.ApplyCityItems)
            {
                string cityname = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", city.OrganizeCity);
                cbl_ApplyCity.Items.Add(new ListItem(cityname, city.ID.ToString()));
            }

            tr_OrganizeCity.DataSource = TreeTableBLL.GetAllChildNodeByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", bll.Model.OrganizeCity.ToString());
            tr_OrganizeCity.RootValue = bll.Model.OrganizeCity.ToString();
            tr_OrganizeCity.SelectValue = tr_OrganizeCity.RootValue;

            if (bll.Model.ApproveFlag == 1)
            {
                bt_Add.Visible = false;
                bt_Delete.Visible = false;
                tr_OrganizeCity.Enabled = false;
            }
        }
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] > 0)
        {
            if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != tr_OrganizeCity.RootValue)
            {
                PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

                Addr_OrganizeCityBLL selectedcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string[] allparent = selectedcity.GetAllSuperNodeIDs().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (PDT_StandardPrice_ApplyCity city in bll.ApplyCityItems)
                {
                    if (selectedcity.Model.ID == city.OrganizeCity)
                    {
                        MessageBox.Show(this, "对不起,该区域已在适用区域内，请勿重复添加!");
                        return;
                    }

                    if (allparent.Contains(city.OrganizeCity.ToString()))
                    {
                        MessageBox.Show(this, "对不起,要新增的区域的上级【" + TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", city.OrganizeCity) + "】已在适用区域内!");
                        return;
                    }
                }

                PDT_StandardPrice_ApplyCity c = new PDT_StandardPrice_ApplyCity();
                c.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
                bll.AddApplyCity(c);

                BindCheckBoxList();
            }
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] > 0)
        {
            PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

            foreach (ListItem item in cbl_ApplyCity.Items)
            {
                if (item.Selected)
                {
                    bll.DeleteApplyCity(int.Parse(item.Value));
                }
            }

            BindCheckBoxList();
        }
    }
}
