<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="OrderDetail0.aspx.cs" Inherits="SubModule_PBM_Order_OrderDetail0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="订单详细信息"></asp:Label></h2>
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
                                <td class="dataLabel" width="80" height="28">要求送货日期</td>
                                <td class="dataField" align="left">
                                    <asp:TextBox ID="tbx_ArriveTime" runat="server" onfocus="WdatePicker()"></asp:TextBox><span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                        runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                        Type="Date" ControlToValidate="tbx_ArriveTime"></asp:CompareValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" ControlToValidate="tbx_ArriveTime"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                            </tr>

                            <tr>
                                <td class="dataLabel" width="80" height="28">经办人</td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_Salesman" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="dataField" colspan="2" height="28">
                                    <asp:Button ID="bt_OK" runat="server" Text="新增订货单" OnClick="bt_OK_Click" /></td>
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

