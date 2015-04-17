<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="OrderListNeedAssign.aspx.cs" Inherits="SubModule_PBM_Order_OrderListNeedAssign" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="预售单列表-待派单"></asp:Label></h2>
                        </td>

                        <td align="right">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                <tr>
                                    <td>
                                        <h3>派单查询条件</h3>
                                    </td>
                                    <td align="right"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28">
                                            <td class="dataLabel">期望送货日期
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
                                            <td class="dataLabel">业务员
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Salesman" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查 找" Width="60px" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                <tr>
                                    <td>
                                        <h3>派单信息</h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Assign" runat="server" OnClick="bt_Assign_Click" Text="派 单" Width="60px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="dataLabel" width="80" height="28">送货人</td>
                                            <td class="dataField" align="left">
                                                <asp:DropDownList ID="ddl_DeliveryMan" runat="server" DataTextField="RealName" DataValueField="ID" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddl_DeliveryMan_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td class="dataLabel" width="80" height="28">送货车辆</td>
                                            <td class="dataField" align="left">
                                                <asp:DropDownList ID="ddl_DeliveryVehicle" runat="server" DataTextField="VehicleNo" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddl_DeliveryVehicle_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td class="dataLabel" width="80" height="28">出货仓库</td>
                                            <td class="dataField" align="left">
                                                <asp:DropDownList ID="ddl_SupplierWareHouse" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList></td>

                                            <td class="dataLabel" width="80" height="28">要求送货日期</td>
                                            <td class="dataField" align="left">
                                                <asp:TextBox ID="tbx_PreArrivalDate" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator3"
                                                    runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                                    Type="Date" ControlToValidate="tbx_PreArrivalDate"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填" ControlToValidate="tbx_PreArrivalDate"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="PBM_Order_ID" PageSize="15" Width="100%"
                                        PanelCode="Panel_PBM_Order_List_01">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbx" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField DataNavigateUrlFields="PBM_Order_ID" DataNavigateUrlFormatString="OrderDetail.aspx?ID={0}"
                                                Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_Assign" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
