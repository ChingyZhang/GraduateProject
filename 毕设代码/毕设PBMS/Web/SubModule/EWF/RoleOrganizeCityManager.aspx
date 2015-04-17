<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="RoleOrganizeCityManager.aspx.cs" Inherits="SubModule_EWF_RoleOrganizeCityManager" %>

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
                                管理片区经理角色信息</h2>
                        </td>
                        <td align="right">
                            角色ID:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left" colspan="4" height="22" valign="middle">
                            <h4>
                                基本信息</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            角色名称
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Name" Width="120px" runat="server"></asp:TextBox>
                            <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="dataLabel" style="width: 120px;">
                            角色类型
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" Width="120px"
                                runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            描述
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Description" Width="300px" runat="server"></asp:TextBox>
                        </td>
                        <td class="dataLabel">
                            管理片区级别
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Level" runat="server" DataTextField="Value" 
                                DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
