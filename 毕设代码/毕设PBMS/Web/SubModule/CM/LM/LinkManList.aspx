<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="LinkManList.aspx.cs" Inherits="CM_LinkMan_LinkManList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                联系人列表</h2>
                        </td>
                        <asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap>
                            <h3>
                                查询条件</h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                客户
                                            </td>
                                            <td>
                                                <mcs:MCSSelectControl runat="server" ID="select_Retailer" PageUrl="../PopSearch/Search_SelectClient.aspx"
                                                    Width="400px" />
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Find" runat="server" Text="查  找" Width="60px" OnClick="bt_Find_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap>
                            <h3>
                                联系人列表</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" DataKeyNames="CM_LinkMan_ID" PanelCode="Panel_LM_List_001"
                            Binded="False" ConditionString="" TotalRecordCount="0">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_LinkMan_ID" DataNavigateUrlFormatString="LinkManDetail.aspx?ID={0}"
                                    DataTextField="CM_LinkMan_Name" HeaderText="联系人姓名" ControlStyle-CssClass="listViewTdLinkS1"
                                    Target="_blank" />
                            </Columns>
                            <EmptyDataTemplate>
                                无数据</EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
