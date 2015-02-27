<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_RoleList, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 355px">
                            <h2>
                                角色列表</h2>
                        </td>
                        <td align="right">
                            名称:
                            <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" Text="查询" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="btn_Del" runat="server" Text="删 除" Width="60px" OnClick="btn_SetEnableFlag_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="ID,Type" PageSize="15" Width="100%"
                OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnPageIndexChanging="gv_List_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbx_ID" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="RoleBasicDetail.aspx?Role={0}"
                        DataTextField="Name" HeaderText="名称" ControlStyle-CssClass="listViewTdLinkS1" />
                    <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" />
                    <asp:BoundField DataField="Type" HeaderText="角色类型" SortExpression="Type" />
                </Columns>
                <EmptyDataTemplate>
                    无数据
                </EmptyDataTemplate>
            </mcs:UC_GridView>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
