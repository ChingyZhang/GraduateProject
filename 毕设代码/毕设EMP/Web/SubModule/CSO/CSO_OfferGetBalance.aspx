<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="CSO_OfferGetBalance.aspx.cs" Inherits="SubModule_CSO_CSO_OfferGetBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="提取费用页"></asp:Label></h2>
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td nowrap>
                                    <h3>
                                        提取条件
                                    </h3>
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm" align="center">
                        <table cellpadding="0" cellspacing="0" border="0" width="600px">
                            <tr>
                                <td class="dataLabel" height="22px">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" DisplayRoot="True" IDColumnName="ID"
                                        NameColumnName="Name" ParentColumnName="SuperID" Width="200px" AutoPostBack="True"
                                        OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px" width="150px">
                                    结算月份
                                </td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" AutoPostBack="True" DataTextField="Name"
                                        DataValueField="ID" OnSelectedIndexChanged="ddl_AccountMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px" width="150px">
                                    派发录入日期
                                </td>
                                <td align="left" class="dataField">
                                    <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begin"
                                        Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    至
                                    <asp:TextBox ID="tbx_end" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_end"
                                        Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td class="dataLabel" height="22px">
                                    经销商
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="230px" />
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td class="dataLabel" height="22px">
                                    营养教育专员
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="180px" />
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td class="dataLabel" height="22px">
                                    VIP
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Doctor" runat="server" PageUrl="~/SubModule/CM/Hospital/Pop_Search_SelectDoctor.aspx"
                                        Width="180px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataField" align="center" colspan="2" height="22px">
                                    <asp:Button ID="bt_Balance" runat="server" OnClick="bt_Balance_Click" OnClientClick="return confirm(&quot;是否确认提取?&quot;)"
                                        Text="提取" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="bt_Balance" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
