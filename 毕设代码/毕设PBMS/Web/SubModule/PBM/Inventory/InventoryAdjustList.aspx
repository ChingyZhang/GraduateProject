<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="InventoryAdjustList.aspx.cs" Inherits="SubModule_PBM_Inventory_InventoryAdjustList" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="盘点单列表"></asp:Label></h2>
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
                        <td class="dataLabel">销售日期
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
                        <td class="dataLabel">仓库
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_WareHouse" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                <asp:ListItem Value="0" Text="请选择..." Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="制单"></asp:ListItem>
                                <asp:ListItem Value="4" Text="确认"></asp:ListItem>
                            </asp:DropDownList>
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

                            <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="PBM_Delivery_ID" PageSize="15" Width="100%"
                                PanelCode="Panel_PBM_Delivery_InventoryAdjust_01">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="PBM_Delivery_ID" DataNavigateUrlFormatString="InventoryAdjustDetail.aspx?ID={0}"
                                        Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

