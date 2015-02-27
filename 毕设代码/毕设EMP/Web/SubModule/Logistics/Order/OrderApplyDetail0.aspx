<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderApplyDetail0.aspx.cs" Inherits="SubModule_Logistics_Order_OrderApplyDetail0" %>

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
                                    申购月份
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    管理片区
                                </td>
                                <td align="left" class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" AutoPostBack="True" IDColumnName="ID"
                                        NameColumnName="Name" OnSelected="tr_OrganizeCity_Selected" ParentColumnName="SuperID"
                                        Width="180px" />
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
                                    申请品项目录
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Publish" runat="server" DataTextField="Topic" 
                                        DataValueField="ID" AutoPostBack="True" 
                                        onselectedindexchanged="ddl_Publish_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    经销商
                                </td>
                                <td align="left" class="dataField">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"OrganizeCity>1\"'
                                        Width="280px" onselectchange="select_Client_SelectChange" />
                                </td>
                            </tr>
                             <tr>
                                <td class="dataLabel" height="28px">
                                    收货经销商
                                </td>
                                <td align="left" class="dataField">
                                    <mcs:MCSSelectControl ID="select_Receiver" runat="server" PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"OrganizeCity>1\"'
                                        Width="280px" onselectchange="select_Receiver_SelectChange" Enabled="false" />
                                </td>
                            </tr>
                          <%--  <tr visible="false">
                                <td class="dataLabel" height="28px">
                                    收货方地址<red>*</red>
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Address" runat="server" DataTextField="Address" 
                                        DataValueField="AddressID" >
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="dataLabel" height="28px">
                                    赠品申请额度</td>
                                <td align="left" class="dataField">
                                    <asp:Label ID="lb_GiftApplyAmount" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
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
