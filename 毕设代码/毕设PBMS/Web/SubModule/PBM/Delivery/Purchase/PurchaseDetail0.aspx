<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="PurchaseDetail0.aspx.cs" Inherits="SubModule_PBM_Delivery_Purchase_PurchaseDetail0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="新建采购单"></asp:Label></h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="tabForm">
                <table cellspacing="0" cellpadding="0" width="600px" border="0" >
                    <tr>
                        <td class="dataLabel" width="80" height="28">采购单位</td>
                        <td class="dataField" align="left">
                            <asp:DropDownList ID="ddl_Supplier" runat="server" DataTextField="FullName" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                    </tr>
                     <tr>
                        <td class="dataLabel" width="80" height="28">仓库</td>
                        <td class="dataField" align="left">
                            <asp:DropDownList ID="ddl_ClientWareHouse" runat="server" DataTextField="Name" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="dataLabel" width="80" height="28">经办人</td>
                        <td class="dataField" align="left">
                            <asp:DropDownList ID="ddl_Salesman" runat="server" DataTextField="RealName" DataValueField="ID" Width="200px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="dataField" colspan="2" height="28">
                            <asp:Button ID="bt_OK" runat="server" Text="新增入库单" OnClick="bt_OK_Click" /></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

