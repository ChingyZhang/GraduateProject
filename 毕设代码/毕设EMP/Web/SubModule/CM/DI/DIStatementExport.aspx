<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    CodeFile="DIStatementExport.aspx.cs" Inherits="SubModule_CM_DI_DIStatementExport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle"
        height="28">
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left">
                            <h2>
                                查看统计报表</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" OnClick="bt_Refresh_Click" Text=" 批量导出PDF "
                                Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="28px">
                            <h3>
                                请设置统计报表参数信息</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    起始会计月
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    截止会计月
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_AccountMonthEnd" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    经销商&nbsp;<mcs:MCSSelectControl ID="Select_Client_Begin" runat="server" Width="260px"
                                        PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ShowParent=Y" />
                                </td>
                                <td class="dataLabel">
                                    至
                                </td>
                                <td>
                                    <mcs:MCSSelectControl ID="Select_Client_End" runat="server" Width="260px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ShowParent=Y" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
