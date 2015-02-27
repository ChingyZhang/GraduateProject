<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PM_StdInsuranceCostInCity.aspx.cs"
    Inherits="SubModule_PM_PM_StdInsuranceCostInCity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>标准价表适用区域</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                    </td>
                                    <td>
                                        <h2>
                                            保险适用区域</h2>
                                    </td>
                                    <td>
                                        新增区域
                                    </td>
                                    <td>
                                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server"
                                            IDColumnName="ID" NameColumnName="Name" ParentColumnName="SuperID" Width="180px" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Add" runat="server" Text="增加区域" OnClick="bt_Add_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h3>
                                            已适用区域</h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Delete" runat="server" Text="删除区域" OnClick="bt_Delete_Click" OnClientClick="return confirm('是否确认删除已选中的区域?');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="cbl_ApplyCity" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
