<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FeeApplyDetail0.aspx.cs" Inherits="SubModule_FNA_FeeApply_FeeApplyDetail0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="新费用申请"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="tabForm">
                        <table border="0" cellpadding="0" cellspacing="0" width="500px">
                            <tr>
                                <td class="dataLabel" height="28px">
                                    申请月份
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    费用类型
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_FeeType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="tr_AccountTitle2" runat="server" visible="false">
                                <td class="dataLabel" height="28px">
                                    科目类别
                                </td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_AccountTitle2" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="240px" AutoPostBack="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    费用归属品牌</td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" 
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ImageButton ID="bt_Confirm" runat="server" ImageUrl="~/Images/gif/gif-0024.gif"
                                        OnClick="bt_Confirm_Click" ImageAlign="AbsMiddle" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
