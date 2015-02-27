<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderProductApplyDetail_Print, App_Web_aozhikkk" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
    </style>
</head>
<body onload="javascript:window.print()">
  <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
           <td align="center">
                    <asp:Label ID="lb_Header" runat="server" CssClass="style1"></asp:Label>
                    <span class="style1">定单请购申请单详细信息</span>
                </td>
           
        </tr>
        <tr>
            <td>
            
                        <mcs:UC_DetailView ID="pn_OrderApply" runat="server" DetailViewCode="page_Product_OrderyDetail">
                        </mcs:UC_DetailView>
               
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                定单请购申请明细列表</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>              
                        <mcs:UC_GridView ID="gv_ProductList" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID,Product">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1"
                                    Visible="false" SelectText="选择">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                </asp:CommandField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lb_State" runat="server" Text="" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Product" HeaderText="产品" SortExpression="Product" />
                                <asp:TemplateField HeaderText="产品编码">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_ProductCode" runat="server" Text='<%# new MCSFramework.BLL.Pub.PDT_ProductBLL((int)DataBinder.Eval(Container,"DataItem.Product")).Model.Code %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单价">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_Price" runat="server" Text='<%#Bind("Price") %>' Width="40px"  ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="自有库库存">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_StoreInventory" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"StoreInventory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月最大进货量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_MaxPurchaseVolume" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"MaxPurchaseVolume") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月最低进货量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_MinPurchaseVolume" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"MinPurchaseVolume") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="期初库存">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_PreQuantity" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"BeginningInventory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="累计有效订货量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_OrderVolume" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"OrderVolume") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="申请数量" SortExpression="BookQuantity">
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_BookQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="lb_BookPackaging_T" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><asp:Label
                                            ID="tbx_BookQuantity" runat="server" Width="30px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity"))).ToString() %>'></asp:Label><asp:Label
                                                ID="lb_BookPackaging" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调整数量" SortExpression="AdjustQuantity"  >
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_AdjustQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AdjustQuantity"))).ToString() %>'></asp:Label><asp:Label ID="lb_Packaging_T"
                                                    runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><asp:Label
                                                        ID="tbx_AdjustQuantity" runat="server" Width="30px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AdjustQuantity"))).ToString() %>'></asp:Label><asp:Label
                                                            ID="lb_Packaging" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调整原因" SortExpression="AdjustReason" >
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_AdjustReason" runat="server" Text='<%# Bind("AdjustReason") %>'
                                            ></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额" SortExpression="DeliveryQuantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_BookCost" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.Price") * ((int)DataBinder.Eval(Container,"DataItem.BookQuantity") + (int)DataBinder.Eval(Container,"DataItem.AdjustQuantity"))).ToString()%>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="已发放数量" SortExpression="DeliveryQuantity" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_DeliveryQuantity" runat="server" Text='<%# GetQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),(int)DataBinder.Eval(Container,"DataItem.DeliveryQuantity"))%>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" />
                            </Columns>
                        </mcs:UC_GridView>
                 
            </td>
        </tr>
        <tr>
            <td align="right" height="28">
               
                        合计费用：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>元 
            </td>
        </tr>
    </table>
     </div>
    </form>
</body>
</html>
