<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RetailerDetail.aspx.cs" Inherits="SubModule_RM_RetailerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24" style="height: 24px">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap" style="height: 24px;">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="零售商详细信息" /></h2>
                                </td>

                                <td align="right" style="height: 24px">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Map" runat="server" Text="地图位置" Width="60px" OnClick="bt_Map_Click" />

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" visible="false">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>账户信息
                                                </h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                        <tr>
                                            <td width="200px">
                                                <asp:Label ID="lb_PreReceivedAmount" ForeColor="Red" runat="server" Text=""></asp:Label></td>
                                            <td width="120px">预收款：</td>
                                            <td width="120px">
                                                <asp:Label ID="lb_AR" runat="server" Text=""></asp:Label></td>
                                            <td width="120px">应收款：</td>
                                            <td width="120px">
                                                <asp:Label ID="lb_PreReceivedBalance" runat="server" Text=""></asp:Label></td>
                                            <td width="120px">预收余额：</td>
                                            <td width="120px">
                                                <asp:HyperLink ID="hy_AccountPoints" runat="server" Text="" Target="_blank" ForeColor="Blue" Font-Bold="true">应收款明细</asp:HyperLink></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PBM_RT_RetailDetail">
                                </mcs:UC_DetailView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="tr_LinkMan" runat="server" visible="false">
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>客户联系人列表
                                                </h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddLinkMan" runat="server" Text="新 增" Width="60px" OnClick="bt_AddLinkMan_Click"
                                                    UseSubmitBehavior="False" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                                DataKeyNames="CM_LinkMan_ID" PanelCode="Panel_LM_List_001">
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="CM_LinkMan_ID" DataNavigateUrlFormatString="../LM/LinkManDetail.aspx?ID={0}"
                                                        DataTextField="CM_LinkMan_Name" HeaderText="联系人姓名" ControlStyle-CssClass="listViewTdLinkS1"
                                                        Target="_blank" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无数据
                                                </EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
