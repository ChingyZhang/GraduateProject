<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="OrderList.aspx.cs" Inherits="SubModule_PBM_Order_OrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" /></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="订单列表"></asp:Label></h2>
                        </td>

                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">

                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="dataLabel">订货日期
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" ControlToValidate="tbx_begin"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            至
                                    <asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator2"
                                runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_end"
                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                        </td>
                        <td class="dataLabel">零售店
                        </td>
                        <td class="dataField">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="~/SubModule/PBM/Retailer/Pop_Search_SelectClient.aspx"
                                        Width="300px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="dataLabel">单据类别
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID" Width="200px">
                                <asp:ListItem Value="0" Text="请选择..." Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="销售订单"></asp:ListItem>
                                <asp:ListItem Value="2" Text="退货订单" Enabled="false"></asp:ListItem>
                                <asp:ListItem Value="4" Text="换货订单" Enabled="false"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="dataLabel">单号
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_SheetCode" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="dataLabel">状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                <asp:ListItem Value="0" Text="请选择..." Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="制单"></asp:ListItem>
                                <asp:ListItem Value="2" Text="提交"></asp:ListItem>
                                <asp:ListItem Value="3" Text="派单"></asp:ListItem>
                                <asp:ListItem Value="4" Text="发货中"></asp:ListItem>
                                <asp:ListItem Value="5" Text="已完成"></asp:ListItem>
                                <asp:ListItem Value="8" Text="取消"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">业务员
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Salesman" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList>

                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                           <%-- <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="PBM_Order_ID" PageSize="15" Width="100%"
                                        PanelCode="Panel_PBM_Order_List_01">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="PBM_Order_ID" DataNavigateUrlFormatString="OrderDetail.aspx?ID={0}"
                                                Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                               <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
