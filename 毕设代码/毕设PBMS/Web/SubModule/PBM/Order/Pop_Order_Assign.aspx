<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_Order_Assign.aspx.cs" Inherits="SubModule_PBM_Order_Pop_Order_Assign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>销售订单配货派单</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left" width="150">
                                    <h2>销售订单配货派单</h2>
                                </td>
                                <td align="right">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td class="dataLabel" width="80" height="28">送货人</td>
                                        <td class="dataField" align="left">
                                            <asp:DropDownList ID="ddl_DeliveryMan" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddl_DeliveryMan_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" width="80" height="28">送货车辆</td>
                                        <td class="dataField" align="left">
                                            <asp:DropDownList ID="ddl_DeliveryVehicle" runat="server" DataTextField="VehicleNo" DataValueField="ID" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddl_DeliveryVehicle_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" width="80" height="28">出货仓库</td>
                                        <td class="dataField" align="left">
                                            <asp:DropDownList ID="ddl_SupplierWareHouse" runat="server" DataTextField="Name" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" width="80" height="28">要求送货日期</td>
                                        <td class="dataField" align="left">
                                            <asp:TextBox ID="tbx_PreArrivalDate" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                            <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                                runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                                Type="Date" ControlToValidate="tbx_PreArrivalDate"></asp:CompareValidator><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" ControlToValidate="tbx_PreArrivalDate"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="dataField" colspan="2" height="28" align="center">
                        <asp:Button ID="bt_OK" runat="server" Text="确认配货" OnClick="bt_OK_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
