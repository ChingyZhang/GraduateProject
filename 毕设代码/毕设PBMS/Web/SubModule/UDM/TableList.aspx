<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="TableList.aspx.cs" Inherits="SubModule_UDM_TableList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td align="left" nowrap="noWrap">
                            <h2>
                                系统数据表 列表</h2>
                        </td>
                        <td align="right">
                            表名:<asp:TextBox ID="tbx_Find" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                            <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="新增" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="100%">
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                Width="100%" AllowPaging="true" PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="Name" HeaderText="表名" />
                                    <asp:BoundField DataField="DisplayName" HeaderText="显示名称" />
                                    <asp:BoundField DataField="ExtFlag" HeaderText="可扩展字段标志" />
                                    <asp:BoundField DataField="ModelClassName" HeaderText="Model实体名称" />
                                    <asp:BoundField DataField="TreeFlag" HeaderText="树形结构标志" />
                                    <asp:HyperLinkField Text="表详细信息" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="TableDetail.aspx?TableID={0}"
                                        ControlStyle-CssClass="listViewTdLinkS1" Target="_blank" />
                                    <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ModelFieldList.aspx?TableID={0}"
                                        Text="表字段列表" ControlStyle-CssClass="listViewTdLinkS1" Target="_blank" />
                                </Columns>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
