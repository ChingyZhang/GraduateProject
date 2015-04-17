<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FindCondition_List.aspx.cs" Inherits="SubMoudle_AdvanceFind_FindCondition_List"
    Title="客户高级查询条件管理列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                高级查询条件管理</h2>
                        </td>
                        <td align="right">
                            所属Panel:<asp:DropDownList ID="ddl_PanelList" runat="server" DataTextField="name"
                                DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddl_PanelList_SelectedIndexChanged">
                            </asp:DropDownList>
                            条件名称:<asp:TextBox ID="tbx_Find" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text=" 查 找 " /><asp:Button
                                ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="ID" PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging"
                                OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="查看详细">
                                        <ItemStyle Width="100px" />
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:CommandField>
                                    <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../Search/AdvancedSearch.aspx?ConditionID={0}"
                                        Text="客户高级查询" Visible="False">
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                    <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="创建日期" SortExpression="CreateDate" />
                                    <asp:BoundField DataField="IsPublic" HeaderText="是否公用" SortExpression="IsPublic" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
