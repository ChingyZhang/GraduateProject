<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleOutDetail_Print.aspx.cs" Inherits="SubModule_PBM_Delivery_SaleOut_SaleOutDetail_Print" %>

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
    <%--window.close();--%>
    <form id="form1" runat="server">
        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
            <ItemTemplate>
                <asp:Literal ID="lb_RepeaterNextPage2" runat="server">
                </asp:Literal>
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
                                                            <td nowrap>单号：<asp:Label ID="lbSheetCode" runat="server" Text="无"></asp:Label>
                                                            </td>
                                                            <td>日期：<asp:Label ID="lbDeliveryTime" runat="server" Text="无"></asp:Label>
                                                            </td>
                                                            <td>业务员：<asp:Label ID="lbSalesMan" runat="server" Text="无"></asp:Label>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">零售商：<asp:Label ID="lbClient" runat="server" Text="无" Font-Bold="true"></asp:Label></td>
                                                            <td>仓库：<asp:Label ID="lbSupplierWareHouse" runat="server" Text="无"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="false"
                                            DataKeyNames="ID" OnRowDataBound="gv_List_RowDataBound" GridLines="Both" BorderWidth="1" BorderColor="Black" CssClass="">
                                            <HeaderStyle BackColor="White" CssClass="" Font-Size="12px" Height="28px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="编号" ItemStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="产品名称" ItemStyle-Width="280px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbProduct" Text='' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="批号" DataField="LotNumber" />
                                                <asp:TemplateField HeaderText="单价">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Price" runat="server" Text=''></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数量">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Quantity" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Fee" runat="server" Text=''></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="折扣率">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbDiscountRate" Text='' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="折扣率" DataField="DiscountRate" />
                                        <asp:BoundField HeaderText="生产日期" DataField="ProductDate" DataFormatString="{0:yyyy-MM-dd}" />--%>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="White" />
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                                <tr><td></td></tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="5" border="1" cellspacing="0" style="height: 30px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; WIDTH: 100%; BORDER-COLLAPSE: collapse; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BACKGROUND-COLOR: white">
                                            <tr style="font-weight: bold;" runat="server" id="tr_TotalInfo">
                                                <td colspan="2">大写金额：&nbsp;<asp:Label ID="lbTotalCostCN" runat="server" Font-Size="14px" Text="大写金额"></asp:Label>
                                                </td>
                                                <td width="100px">金额&nbsp;<asp:Label ID="lbTotalCost" runat="server" Text="301.4"></asp:Label>
                                                </td>
                                                <td width="100px">优惠：&nbsp;<asp:Label ID="lbWipeAmount" runat="server" Text="40"></asp:Label>
                                                </td>
                                                <td width="100px">折扣：&nbsp;<asp:Label ID="lbDiscountAmount" runat="server" Text="40"></asp:Label>
                                                </td>
                                                <td width="100px">总数量：&nbsp;<asp:Label ID="lbTotalCount" runat="server" Text="40"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr height="25px">
                                                <td width="100px">经办&nbsp;<asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td width="30%">库管&nbsp;<asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td width="100px">制单&nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td width="100px">会计&nbsp;<asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td width="100px">&nbsp;<asp:Label ID="lb_PageInfo" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
