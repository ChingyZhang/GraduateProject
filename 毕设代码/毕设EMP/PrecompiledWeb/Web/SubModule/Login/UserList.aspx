<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Login_UserList, App_Web_atkk77yx" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                用户登录帐号列表</h2>
                        </td>
                        <td align="right">
                            用户名：<asp:TextBox ID="tbx_Find" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" UseSubmitBehavior="False"
                                OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="UserName" OnRowDataBound="gv_List_RowDataBound" AllowPaging="true"
                    PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="CreationDate" HeaderText="创建日期" SortExpression="CreationDate" />
                        <asp:BoundField DataField="LastLoginDate" HeaderText="最后登录日期" SortExpression="LastLoginDate" />
                        <asp:TemplateField HeaderText="所属员工/客户">
                            <ItemTemplate>
                                <asp:Label ID="lb_StaffName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IsApproved" HeaderText="是否有效" SortExpression="IsOnline" />
                        <asp:CheckBoxField DataField="IsOnline" HeaderText="是否在线" SortExpression="IsOnline" />
                        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="是否锁定" SortExpression="IsLockedOut" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "UserDetail.aspx?Username="+Server.UrlPathEncode(Eval("UserName").ToString()) %>'
                                     Text="详细信息"></asp:HyperLink>
                            </ItemTemplate>
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:TemplateField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
