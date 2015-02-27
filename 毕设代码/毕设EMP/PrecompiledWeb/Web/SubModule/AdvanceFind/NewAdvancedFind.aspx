<%@ page language="C#" autoeventwireup="true" inherits="Controls_NewAdvancedFind, App_Web_qrw4oirc" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建查询条件</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellspacing="0" cellpadding="0" width="400px" border="0" align="center">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                        <tr>
                            <td width="24">
                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                            </td>
                            <td nowrap="noWrap" style="width: 180px">
                                <h2>
                                    新建查询条件</h2>
                            </td>
                            <td align="right">
                                <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" 
                                    onclick="bt_Save_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tabForm">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="dataLabel">所属查询
                            </td>
                            <td class="dataField">
                                <asp:Label ID="lb_PanelName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="dataLabel">条件名称
                            </td>
                            <td class="dataField">
                                <asp:TextBox ID="tbx_Name" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="tbx_Name" Display="Dynamic" ErrorMessage="必须填写"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="dataLabel">是否公用
                            </td>
                            <td class="dataField">
                                <asp:DropDownList ID="ddl_IsPublic" runat="server">
                                    <asp:ListItem Value="Y">公用</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="N">私有</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
