<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="Receipt_PreReceived.aspx.cs" Inherits="SubModule_PBM_Account_Receipt_PreReceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="收预收款"></asp:Label></h2>
                        </td>
                        <td align="right">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="600px" border="0">
                            <tr>
                                <td class="dataLabel" width="80" height="28">零售店</td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="~/SubModule/PBM/Retailer/Pop_Search_SelectClient.aspx"
                                        Width="300px" />
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28" width="80">收款方式</td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_PayMode" runat="server">
                                        <asp:ListItem Selected="True" Value="1">现金</asp:ListItem>
                                        <asp:ListItem Value="2">POS</asp:ListItem>
                                        <asp:ListItem Value="3">银行转账</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" width="80" height="28">收款金额</td>
                                <td class="dataField" align="left">
                                    <asp:TextBox ID="tbx_Amount" runat="server"></asp:TextBox>元<span class="overdueTask">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_Amount" Display="Dynamic" ErrorMessage="必填" ValidationGroup="bt_SaveDetail"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_Amount" Display="Dynamic" ErrorMessage="必需是数字格式" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" width="80" height="28">经办人</td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_AgentStaff" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28" width="80">备注</td>
                                <td align="left" class="dataField">
                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataField" colspan="2" height="28">
                                    <asp:Button ID="bt_OK" runat="server" Text="确认收款" OnClick="bt_OK_Click" OnClientClick="return confirm(&quot;是否确认收款?&quot;)" /></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

