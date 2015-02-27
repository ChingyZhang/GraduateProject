<%@ page language="C#" autoeventwireup="true" inherits="SubModule_AddressManage_OrganizeCity_AddOfficialCity, App_Web_zwnb1_b0" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在管理片区架构中加入行政区县</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                        <tr>
                            <td width="24">
                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                            </td>
                            <td nowrap="noWrap" style="width: 120px">
                                <h2>
                                    <asp:Label ID="lb_PageTitle" runat="server" Text="加入行政区县"></asp:Label></h2>
                            </td>
                            <td align="left">
                                管理片区:<asp:Label ID="lb_OrganizeCityName" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="cb_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_CheckAll_CheckedChanged"
                                    Text="全选" />
                                <asp:Button ID="bt_AddOfficialCity" Text="确定加入" runat="server" Width="60px" OnClick="bt_AddOfficialCity_Click"
                                    OnClientClick="return confirm('是否确认加入所选的行政区县?');" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30">
                    行政城市：<mcs:MCSTreeControl ID="tr_OfficialCity" runat="server" IDColumnName="ID" ParentColumnName="SuperID"
                        NameColumnName="Name" RootValue="1" Width="240px" AutoPostBack="true" TableName="MCS_SYS.dbo.Addr_OfficialCity"
                        OnSelected="tr_OfficialCity_Selected" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="cbl_OfficialList" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
