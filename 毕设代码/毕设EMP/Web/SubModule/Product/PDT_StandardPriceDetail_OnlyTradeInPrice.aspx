<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PDT_StandardPriceDetail_OnlyTradeInPrice.aspx.cs"
    Inherits="SubModule_Product_PDT_StandardPriceDetail_OnlyTradeInPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="产品标准价盘零供价格表"></asp:Label></h2>
                        </td>
                        <td class="dataLabel" style="color: Red; font-weight: bold;">
                            单价为零售单位(听\袋\盒)的价格
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
                        </td>
                    </tr>
                </table>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="DV_StandardPrice" Visible="true">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    Binded="False" DataKeyNames="ID,Product">
                    <Columns>
                        <asp:TemplateField HeaderText="产品编码">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# GetERPCode(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Product" HeaderText="产品名称" />
                        <asp:BoundField DataField="TradeInPrice" HeaderText="零供价(元)" DataFormatString="{0:0.###}"
                            HtmlEncodeFormatString="false" />
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
