<%@ page language="C#" autoeventwireup="true" inherits="SubModule_StaffManage_RoleInfo, App_Web_it73ecr1" masterpagefile="~/MasterPage/BasicMasterPage.master" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top" width="180px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td align="right" width="20">
                                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td align="left" width="150">
                                                <h2>
                                                    角色列表</h2>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="180px" valign="top" height="300px">
                                    <asp:TreeView ID="tr_Role" runat="server" Width="100%" ImageSet="Msdn" Target="if_ListViewFrame"
                                        ExpandDepth="1" OnSelectedNodeChanged="tr_Role_SelectedNodeChanged">
                                        <NodeStyle CssClass="listViewTdLinkS1" />
                                        <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                    </asp:TreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
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
                                                    用户角色管理</h2>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        当前角色：<asp:Label ID="lb_RoleName" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="tr_Role" EventName="SelectedNodeChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="right">
                                                角色名称：<asp:TextBox ID="tbx_RoleName" runat="server"></asp:TextBox>
                                                <asp:Button ID="bt_AddRole" runat="server" Text="新增角色" OnClick="bt_AddRole_Click" />
                                                <asp:Button ID="bt_UpdateRole" runat="server" Text="编辑角色" OnClick="bt_UpdateRole_Click" />
                                                <asp:Button ID="bt_DeleteRole" runat="server" OnClick="bt_DeleteRole_Click" OnClientClick="return confirm(&quot;删除后将不可恢复，是否确认删除?&quot;)"
                                                    Text="删除角色" />
                                                <asp:Button ID="bt_Right" runat="server" OnClick="bt_Right_Click" Text="权限管理" />
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="tabForm">
                                                <tr>
                                                    <td>
                                                        职务：
                                                        <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                            Width="300px" AutoPostBack="true" ParentColumnName="SuperID" RootValue="0" OnSelected="tr_Position_Selected">
                                                        </mcs:MCSTreeControl>
                                                    </td>
                                                    <td>
                                                        关键字：<asp:TextBox ID="tbx_Search" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="查找" Width="60" />
                                                        <asp:Button ID="bt_In" runat="server" OnClick="bt_In_Click" Text="加入角色" />
                                                        <asp:Button ID="bt_Out" runat="server" OnClick="bt_Out_Click" Text="移出角色" />
                                                        <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                                            Text="全选" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                                Width="100%">
                                                <Items>
                                                    <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="角色成员"
                                                        Value="0" Visible="True" />
                                                    <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="非角色成员"
                                                        Value="1" Visible="True" />
                                                </Items>
                                            </mcs:MCSTabControl>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td nowrap>
                                                            <h3>
                                                                公司员工
                                                            </h3>
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" PanelCode="Panel_Staff_ListWithUser"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" Binded="False" DataKeyNames="aspnet_Users_UserName"
                                        TotalRecordCount="0" ConditionString="" OrderFields="">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("aspnet_Membership_StaffID", "StaffDetail.aspx?ID={0}") %>'
                                                         Text="员工详细资料" Visible='<%# Eval("aspnet_Membership_StaffID").ToString()!="" %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Submodule/Login/UserDetail.aspx?Username="+Server.UrlPathEncode(Eval("aspnet_Users_UserName").ToString()) %>'
                                                         Text="登录用户信息"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td nowrap>
                                                            <h3>
                                                                商业客户
                                                            </h3>
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_List2" runat="server" Width="100%" PanelCode="Panel_CMClientListWithUser"
                                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15" Binded="False" DataKeyNames="aspnet_Users_UserName"
                                                    TotalRecordCount="0" ConditionString="" OrderFields="">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("aspnet_Membership_ClientID", "../CM/RT/RetailerDetail.aspx?ClientID={0}") %>'
                                                                    Target="_blank" Text="客户详细资料" Visible='<%# Eval("aspnet_Membership_ClientID").ToString()!="" %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Submodule/Login/UserDetail.aspx?Username="+Server.UrlPathEncode(Eval("aspnet_Users_UserName").ToString()) %>'
                                                                    Target="_blank" Text="登录用户信息"></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
