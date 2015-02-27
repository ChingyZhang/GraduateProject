<%@ page language="C#" autoeventwireup="true" inherits="SubModule_CM_ReplaceClientManager, App_Web_bemqpquv" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="500px">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            批量替换客户的客户经理
                                        </h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Replace" runat="server" OnClick="bt_Replace_Click" Text="确认替换"
                                            Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabDetailView">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="tabDetailViewDL">
                                        原客户经理
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <mcs:MCSSelectControl ID="select_OrgClientManager" runat="server" PageUrl="../StaffManage/Pop_Search_Staff.aspx"
                                            Width="260px" OnSelectChange="select_OrgClientManager_SelectChange" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        新客户经理
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <mcs:MCSSelectControl runat="server" ID="select_NewClientManager" PageUrl="../StaffManage/Pop_Search_Staff.aspx"
                                            Width="260px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lb_Prompt" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
