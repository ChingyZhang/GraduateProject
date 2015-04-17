<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ApproveAgencyDetail.aspx.cs" Inherits="SubModule_EWF_ApproveAgencyDetail" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="工作流代理审批授权信息"></asp:Label>
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保 存" Width="60px" />
                            <asp:Button ID="bt_Disable" runat="server" Text="失 效" Width="60px" 
                                onclick="bt_Disable_Click" onclientclick="return confirm('是否确认失效该授权?')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" Width="100%" DetailViewCode='DV_EWF_ApproveAgency_Detail_01'>
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_AppList" runat="server" visible="false">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                        <tr>
                                            <td height="28px">
                                                <h2>
                                                    待授权的工作流列表</h2>
                                            </td>
                                            <td align="right">
                                                <asp:CheckBox ID="cbx_All" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_All_CheckedChanged"
                                                    Text="全选" />
                                                <asp:Button ID="btn_Search" runat="server" Text="显示所有" OnClick="btn_Search_Click"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <asp:CheckBoxList ID="cbx_AppList" runat="server" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
