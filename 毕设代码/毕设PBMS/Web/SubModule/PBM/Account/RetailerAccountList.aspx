<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="RetailerAccountList.aspx.cs" Inherits="SubModule_PBM_Account_RetailerAccountList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="终端客户预收及应收账户列表"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="AC_CurrentAccount_TradeClient" PageSize="15" Width="100%"
                                        PanelCode="Panel_PBM_AC_CurrentAccount_List_01">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="AC_CurrentAccount_TradeClient" DataNavigateUrlFormatString="~/SubModule/PBM/Retailer/RetailerDetail.aspx?ClientID={0}"
                                                DataTextField="CM_Client_FullName" HeaderText="终端客户" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="预收款">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hy_PreReceivedAmount" Text='<%#Eval("AC_CurrentAccount_PreReceivedAmount","{0:0.##}") %>' CssClass="listViewTdLinkS1"
                                                        NavigateUrl='<%# Eval("AC_CurrentAccount_TradeClient","BalanceUsageList.aspx?TradeClient={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="应收款">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hy_AR" Text='<%#Eval("AC_CurrentAccount_AR","{0:0.##}") %>' CssClass="listViewTdLinkS1"
                                                        NavigateUrl='<%# Eval("AC_CurrentAccount_TradeClient","ARList.aspx?TradeClient={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

