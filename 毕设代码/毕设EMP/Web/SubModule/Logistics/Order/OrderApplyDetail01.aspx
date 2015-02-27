<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderApplyDetail01.aspx.cs" Inherits="SubModule_Logistics_Order_OrderApplyDetail0" %>

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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="产品申请申请"></asp:Label>
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
                                    会计月
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    申请类别
                                </td>
                                <td class="dataField" align="left">
                                    <asp:RadioButtonList ID="rbl_Type" runat="server" AutoPostBack="True" DataTextField="Value"
                                        DataValueField="Key" OnSelectedIndexChanged="rbl_Type_SelectedIndexChanged" RepeatColumns="2"
                                        RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    品牌
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" AutoPostBack="true"
                                        DataValueField="ID" OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    订单分类
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_OrderType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    申请品项目录
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Publish" runat="server" DataTextField="Topic" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="dataLabel" height="28px">
                                <td class="dataLabel" height="28px">
                                    是否特殊订单
                                </td>
                                <td class="dataField" align="left">
                                    <asp:RadioButtonList ID="rbl_IsSpecial" runat="server" DataTextField="Value" DataValueField="Key"
                                        RepeatColumns="2" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr_selectClient" runat="server">
                                <td class="dataLabel" height="28px">
                                    客户
                                </td>
                                <td align="left" class="dataField">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="280px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ImageButton ID="bt_Confirm" runat="server" ImageUrl="~/Images/gif/gif-0024.gif"
                                        ToolTip="显示品项明细" OnClick="bt_Confirm_Click" ImageAlign="AbsMiddle" />
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
