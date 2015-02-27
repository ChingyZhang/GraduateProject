<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PDT_ProductPriceDetail.aspx.cs" Inherits="SubModule_Product_PDT_ProductPriceDetail" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left" width="150">
                                    <h2>
                                        客户产品价格表</h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_Save" runat="server" Text="保 存" Width="60px" OnClick="btn_Save_Click" />
                                    <asp:Button ID="btn_Approve" runat="server" Text="审核通过" Width="60px" 
                                        OnClick="btn_Approve_Click" Visible="False" />
                                    <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="60px" OnClick="btn_Delete_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel" style="width: 10%; height: 30px;">
                                    客户
                                </td>
                                <td class="dataField" style="width: 40%; height: 30px;" align="left">
                                    <asp:Label ID="lbl_Client" runat="server"></asp:Label>
                                </td>
                                <td>
                                    价表有效日期:<asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                            ID="tbx_end" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="日期格式不对"
                                        Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server" id="tr_AddProduct">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            新增产品</h3>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lb_ErrInfo1" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tabForm">
                                                <tr>
                                                    <td class="dataLabel">
                                                        产品编码
                                                    </td>
                                                    <td class="datafield">
                                                        <mcs:MCSSelectControl runat="server" ID="select_ProductCode" PageUrl="../Product/Serarch_SelectProduct.aspx"
                                                            OnTextChange="select_ProductCode_TextChange" OnSelectChange="select_ProductCode_SelectChange"
                                                            TextBoxEnabled="True" Width=120px />
                                                    </td>
                                                    <td class="dataLabel">
                                                        产品名称
                                                    </td>
                                                    <td class="dataField">
                                                        <asp:Label ID="lb_ProductName" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="dataLabel">
                                                        出厂价
                                                    </td>
                                                    <td class="dataField">
                                                        <asp:Label ID="lb_FactoryPrice" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="dataLabel">
                                                        进货价
                                                    </td>
                                                    <td class="dataField">
                                                        <asp:TextBox ID="tbx_BuyingPrice" runat="server" Width="50px"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="tbx_BuyingPrice"
                                                            Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"
                                                            ValidationGroup="1"></asp:CompareValidator>
                                                        <asp:Label ID="lbl_TrafficPackaging" runat="server" Text="出货价"></asp:Label>
                                                        <asp:TextBox ID="tbx_SalesPrice" runat="server" Width="50px"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="tbx_SalesPrice"
                                                            Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"
                                                            ValidationGroup="1"></asp:CompareValidator>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="bt_AddProduct" runat="server" Text="增加产品" Width="70px" OnClick="bt_AddProduct_Click"
                                                            ValidationGroup="1" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td>
                                    <h3>
                                        产品价表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Product,BuyingPrice"
                                                Width="100%" OnRowDeleting="gv_List_RowDeleting" 
                                                onselectedindexchanging="gv_List_SelectedIndexChanging">
                                                <Columns>
                                                    <asp:CommandField SelectText="选择" ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1" />
                                                    <asp:TemplateField HeaderText="产品编码">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# GetERPCode(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Product" HeaderText="产品" />
                                                    <asp:TemplateField HeaderText="出厂价(元)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# GetFactoryPrice(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="买入价(元)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_BuyingPrice" runat="server" Text='<%# Bind("BuyingPrice","{0:0.###}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="出货价(元)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_FactoryPrice" runat="server" Text='<%# Bind("SalesPrice","{0:0.###}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="删除" ControlStyle-CssClass="button">
                                                        <ControlStyle CssClass="button" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无数据
                                                </EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="bt_AddProduct" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
