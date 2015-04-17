<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="TransferDetail0.aspx.cs" Inherits="SubModule_PBM_Delivery_Transfer_TransferDetail0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="调拨单详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="600px" border="0">
                            <tr>
                                <td class="dataLabel" width="80" height="28">单据类别</td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddl_Classify_SelectedIndexChanged">
                                        <asp:ListItem Value="3" Text="仓库调拨单" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="车销移库单"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="车销退库单"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" width="80" height="28">调出仓库</td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_SupplierWareHouse" runat="server" DataTextField="Name" DataValueField="ID" Width="200px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" width="80" height="28">调入仓库</td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_ClientWareHouse" runat="server" DataTextField="Name" DataValueField="ID" Width="200px"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="dataLabel" width="80" height="28">经办人</td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Salesman" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataField" colspan="2" height="28">
                                    <asp:Button ID="bt_OK" runat="server" Text="新增调拨单" OnClick="bt_OK_Click" /></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

