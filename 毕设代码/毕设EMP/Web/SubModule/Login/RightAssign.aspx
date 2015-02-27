<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="RightAssign.aspx.cs" Inherits="SubModule_Login_RightAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left" width="180">
                            <h2>
                                系统权限分配
                            </h2>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    当前角色：<asp:Label ID="lb_RoleName" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tr_Role" EventName="SelectedNodeChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr height="28">
                        <td>
                            <h2>
                                系统角色列表</h2>
                        </td>
                        <td>
                            <h2>
                                系统模块及功能列表<asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存"
                                    Width="60px" />
                            </h2>
                        </td>
                        <td>
                            <h2>
                                已分配权限列表<asp:Button ID="bt_SaveGridView" runat="server" OnClick="bt_SaveGridView_Click"
                                    Text="删除选中的权限" OnClientClick="return confirm(&quot;是否确认删除？&quot;)" />
                                <asp:CheckBox ID="cb_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_CheckAll_CheckedChanged"
                                    Text="全选" />
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="180px">
                            <asp:TreeView ID="tr_Role" runat="server" Width="100%" ImageSet="Msdn" Target="if_ListViewFrame"
                                ExpandDepth="1" OnSelectedNodeChanged="tr_Role_SelectedNodeChanged">
                                <NodeStyle CssClass="listViewTdLinkS1" />
                                <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                            </asp:TreeView>
                        </td>
                        <td valign="top" width="300px" height="300px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TreeView ID="tr_Module" runat="server" Width="100%" ImageSet="Msdn" ExpandDepth="0"
                                        ShowCheckBoxes="All" OnSelectedNodeChanged="tr_Module_SelectedNodeChanged">
                                        <NodeStyle CssClass="listViewTdLinkS1" />
                                        <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                    </asp:TreeView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tr_Role" EventName="SelectedNodeChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_SaveGridView" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_list" runat="server" AllowPaging="True" PageSize="10" Width="100%"
                                        AutoGenerateColumns="False" Binded="False" ConditionString="" DataKeyNames="ID"
                                        OnPageIndexChanging="gv_list_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_check" runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                            <asp:BoundField DataField="Module" HeaderText="模块名称" SortExpression="Module" />
                                            <asp:BoundField DataField="Action" HeaderText="动作权限" SortExpression="Action" />
                                            <asp:BoundField DataField="RoleName" HeaderText="角色名称" SortExpression="RoleName" />
                                        </Columns>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tr_Module" EventName="SelectedNodeChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="tr_Role" EventName="SelectedNodeChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_SaveGridView" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cb_CheckAll" EventName="CheckedChanged" />
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
