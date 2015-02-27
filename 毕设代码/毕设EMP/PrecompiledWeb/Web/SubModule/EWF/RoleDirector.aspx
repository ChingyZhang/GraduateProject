<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_RoleDirector, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

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
                                发起人上级领导角色信息</h2>
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
                            直属领导类别
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_DirectorType" runat="server">
                                <asp:ListItem Selected="True" Value="1">行政领导</asp:ListItem>
                                <asp:ListItem Value="2">业务领导</asp:ListItem>
                            </asp:DropDownList>
                            层级<asp:DropDownList ID="ddl_Level" runat="server">
                                <asp:ListItem Selected="True" Value="1">上1级</asp:ListItem>
                                <asp:ListItem Value="2">上2级</asp:ListItem>
                                <asp:ListItem Value="3">上3级</asp:ListItem>
                                <asp:ListItem Value="4">上4级</asp:ListItem>
                                <asp:ListItem Value="5">上5级</asp:ListItem>
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
