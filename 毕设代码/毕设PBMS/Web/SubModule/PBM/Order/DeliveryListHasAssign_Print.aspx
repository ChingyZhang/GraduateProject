<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryListHasAssign_Print.aspx.cs" Inherits="SubModule_PBM_Order_DeliveryListHasAssign_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .PageTitle1 {
            font-size: 24px;
            text-align: center;
        }

        .PageTitle2 {
            font-size: 22px;
            text-align: center;
        }

        .style1 {
            width: 141px;
        }
    </style>
    <style media="print">
        .Noprint {
            display: none;
        }

        .PageNext {
            page-break-after: always;
        }
    </style>
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <asp:Literal ID="lb_RepeaterNextPage" runat="server">
                    </asp:Literal>
                    <div style="text-align: left; width: 241mm; height: 93mm; margin: 0px auto;">
                        <table style="width: 95%; margin: 0px auto;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>打印日期：<%= System.DateTime.Now.ToString("yyyy-MM-dd") %>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center"><span class="PageTitle2">
                                                <asp:Literal ID="lbTitle" runat="server" Text="无"></asp:Literal></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="spanRow" style="height: 30px;" cellspacing="2" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>预计送货日期：<asp:Label ID="lbDeliveryTime" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td><asp:Label ID="lbSalesMan" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </tr>
                                                    <tr>
                                                        <td>出库仓库：<asp:Label ID="lbSupplierWareHouse" runat="server" Text=""></asp:Label></td>
                                                        <td><asp:Label ID="lbDeliveryMan" runat="server" Text="" Font-Bold="true"></asp:Label></td>                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        Width="100%" GridLines="Both" BorderWidth="1" BorderColor="Black" CssClass="">
                                        <HeaderStyle BackColor="White" CssClass="" Font-Size="12px" Height="28px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="送货人" DataField="DeliveryManName" Visible="false"/>
                                            <asp:BoundField HeaderText="商品分类" DataField="CategoryName" Visible="false" />
                                            <asp:BoundField HeaderText="商品编码" DataField="ProduceCode" />
                                            <asp:BoundField HeaderText="商品名称" DataField="ProductName" />
                                            <asp:TemplateField HeaderText="数量" SortExpression="Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Quantity" runat="server" Text='<%# ((int)Eval("Quantity_T")==0?"":(Eval("Quantity_T").ToString()+Eval("Packagint_T").ToString()))+
                                                    ((int)Eval("Quantity_P")==0?"":(Eval("Quantity_P").ToString()+Eval("Packagint_P").ToString())) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="金额" SortExpression="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Amount" runat="server" Text='<%# Bind("Amount", "{0:0.##元}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="重量" SortExpression="Weight">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Weight" runat="server" Text='<%# Bind("Weight", "{0:0.#Kg}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
