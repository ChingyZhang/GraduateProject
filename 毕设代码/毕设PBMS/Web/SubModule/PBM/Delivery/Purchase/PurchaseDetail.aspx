<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="PurchaseDetail.aspx.cs" Inherits="SubModule_PBM_Delivery_Purchase_PurchaseDetail" %>

<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function doprint(id) {
            window.open("PurchaseDetail_Print.aspx?ID=" + id);
        }
    </script>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../../DataImages/ClientManage.gif" width="16"></td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="采购单详细信息"></asp:Label></h2>
                                </td>
                                <td align="right">&nbsp;
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Height="23px" OnClick="bt_Delete_Click" OnClientClick="return confirm('是否确认删除该单据?');" Text="删除" Width="60px" />
                                    <asp:Button ID="bt_Confirm" runat="server" OnClick="bt_Confirm_Click" OnClientClick="return confirm('是否确认审核该单据?');" Text="审核" Width="60px" />
                                    <asp:Button ID="bt_Print" runat="server" Text="打印" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PBM_Delivery_PurchaseDetail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                                        <h3>付款信息</h3>
                                    </td>
                                    <td align="right"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="dataLabel" height="28" width="80">付款方式一</td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_PayMode1" runat="server">
                                            <asp:ListItem Selected="True" Value="1">现金</asp:ListItem>
                                            <asp:ListItem Value="2">POS</asp:ListItem>
                                            <asp:ListItem Value="11">余额付款</asp:ListItem>
                                            <asp:ListItem Value="13">记应付</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td class="dataLabel" width="80">付款金额</td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_PayAmount1" runat="server" Width="80px">0</asp:TextBox>元
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_PayAmount1" Display="Dynamic" ErrorMessage="数字格式不正确" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    </td>

                                    <td class="dataLabel" height="28" width="80">付款方式二</td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_PayMode2" runat="server">
                                            <asp:ListItem Value="1">现金</asp:ListItem>
                                            <asp:ListItem Value="2">POS</asp:ListItem>
                                            <asp:ListItem Value="11">余额付款</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="13">记应付</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td class="dataLabel" width="80">付款金额</td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_PayAmount2" runat="server" Width="80px">0</asp:TextBox>元
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_PayAmount2" Display="Dynamic" ErrorMessage="数字格式不正确" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                            <tr>
                                                <td>
                                                    <h3>采购商品清单</h3>
                                                </td>
                                                <td align="right"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="tr_AddDetail">
                                    <td class="tabForm">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tb_AddDetail">
                                            <tr>
                                                <td align="left" height="26" style="padding-left: 20px">商品</td>
                                                <td align="left" style="padding-left: 2px">
                                                    <asp:RadioButtonList ID="rbl_ln" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbl_ln_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="N">新入批号</asp:ListItem>
                                                        <asp:ListItem Value="Y">现有批号</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="left" style="padding-left: 10px">生产日期</td>
                                                <td align="left" style="padding-left: 10px">单位</td>
                                                <td align="left" style="padding-left: 10px">采购单价</td>
                                                <td align="left" style="padding-left: 10px">折扣率</td>
                                                <td align="left" style="padding-left: 10px">数量</td>
                                                <td align="left" style="padding-left: 20px">标志</td>
                                                <td align="right" rowspan="2">
                                                    <asp:Button ID="bt_AddDetail" runat="server" Text="新 增" Width="60px" ValidationGroup="bt_SaveDetail" OnClick="bt_AddDetail_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dataLabel">
                                                    <%--<telerik:RadComboBox ID="RadComboBox1" runat="server" AllowCustomText="false" Width="250px" DataTextField="FullName" DataValueField="ID" HighlightTemplatedItems="true" Filter="Contains"
                                                        DropDownAutoWidth="Disabled" NoWrap="true" EmptyMessage="请选择一个产品..." DropDownWidth="550px" AutoPostBack="True" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" MarkFirstMatch="true">
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td width="250px">产品名称</td>
                                                                    <td width="100px">编码</td>
                                                                    <td width="100px">规格</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td width="250px"><%# Eval("FullName") %></td>
                                                                    <td width="100px"><%# Eval("Code") %></td>
                                                                    <td width="100px"><%# Eval("Spec") %></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                    <cc1:ComboBox runat="server" ID="RadComboBox1" AllowCustomText="false" AllowEdit="true" DataTextField="FullName" DataValueField="ID" EmptyText="请选择一个产品..." FilterType="Contains" SelectionMode="Single" Mode="ComboBox" EnableVirtualScrolling="true" Height="150px" Width="250px" MenuWidth="450px" AutoPostBack="true" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged1">
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td width="250px" align="center">产品名称</td>
                                                                    <td width="100px" align="center">规格</td>
                                                                    <td width="100px" align="center">价格</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td width="250px"><%# Eval("FullName") %></td>
                                                                    <td width="100px"><%# Eval("Spec") %></td>
                                                                    <td width="100px" align="center"><%# ((decimal)Eval("SalesPrice")).ToString("0.###") %></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>

                                                    </cc1:ComboBox>--%>

                                                    <mcs:MCSSelectControl ID="select_Product" runat="server" OnSelectChange="select_Product_SelectChange" PageUrl="~/SubModule/PBM/Product/Pop_Search_Product_TDP.aspx" Width="200px" OnTextChange="select_Product_TextChange" TextBoxEnabled="True" />
                                                </td>
                                                <td class="dataLabel">
                                                    <asp:TextBox ID="tbx_LotNumber" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="tbx_LotNumber_TextChanged"></asp:TextBox>
                                                    <asp:DropDownList ID="ddl_LotNumber" runat="server" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddl_LotNumber_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="dataLabel">
                                                    <asp:TextBox ID="tbx_ProductDate" runat="server" Width="70px" onfocus="WdatePicker();"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ProductDate" Display="Dynamic" ErrorMessage="必需是日期格式" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                </td>
                                                <td class="dataLabel">
                                                    <asp:DropDownList ID="ddl_Unit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Unit_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="T">整件</asp:ListItem>
                                                        <asp:ListItem Value="P">零散</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="dataLabel">
                                                    <asp:TextBox ID="tbx_Price" runat="server" Text="0" Width="40px"></asp:TextBox>
                                                    <span class="overdueTask">*</span>&nbsp;<asp:Label ID="lb_TrafficPackagingName" runat="server" Text="元"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Price" Display="Dynamic" ErrorMessage="必填" ValidationGroup="bt_SaveDetail"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="tbx_Price" Display="Dynamic" ErrorMessage="必需是数字格式" Operator="DataTypeCheck" Type="Double" ValidationGroup="bt_SaveDetail"></asp:CompareValidator>
                                                </td>
                                                <td class="dataLabel">
                                                    <asp:TextBox ID="tbx_DiscountRate" runat="server" Text="100" Width="40px"></asp:TextBox>%
                                                    <span class="overdueTask">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_DiscountRate" Display="Dynamic" ErrorMessage="必填" ValidationGroup="bt_SaveDetail"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="tbx_DiscountRate" Display="Dynamic" ErrorMessage="必需是数字格式" Operator="DataTypeCheck" Type="Double" ValidationGroup="bt_SaveDetail"></asp:CompareValidator>
                                                </td>
                                                <td class="dataLabel">
                                                    <asp:TextBox ID="tbx_Quantity" runat="server" Text="0" Width="50px"></asp:TextBox>
                                                    <span class="overdueTask">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_Quantity" Display="Dynamic" ErrorMessage="必填" ValidationGroup="bt_SaveDetail"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_Quantity" Display="Dynamic" ErrorMessage="必需是数字格式" Operator="DataTypeCheck" Type="Integer" ValidationGroup="bt_SaveDetail"></asp:CompareValidator>
                                                </td>
                                                <td class="dataLabel" style="width: 60px">
                                                    <asp:DropDownList ID="ddl_SalesMode" runat="server" DataTextField="Value" DataValueField="Key">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                            DataKeyNames="ID,Product" PageSize="15" Width="100%" OnRowDeleting="gv_List_RowDeleting"
                                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnRowDataBound="gv_List_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="Product" HeaderText="商品" SortExpression="Product" />
                                                <asp:BoundField DataField="LotNumber" HeaderText="批号" SortExpression="LotNumber" />
                                                <asp:BoundField DataField="ProductDate" HeaderText="生产日期" SortExpression="ProductDate" DataFormatString="{0:yyyy-MM-dd}" />
                                                <asp:TemplateField HeaderText="采购价" SortExpression="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Price" runat="server" Text='<%# Bind("Price", "{0:0.###}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数量" SortExpression="DeliveryQuantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Quantity" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="折扣率" SortExpression="DiscountRate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_DiscountRate" runat="server" Text='<%# Bind("DiscountRate","{0:0.##%}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="金额" SortExpression="DeliveryQuantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_Fee" runat="server" Text='<%# (((int)Eval("DeliveryQuantity") * Math.Round((decimal)Eval("Price") * (int)Eval("ConvertFactor"), 2)*(decimal)Eval("DiscountRate")/(int)Eval("ConvertFactor")).ToString("0.##")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SalesMode" HeaderText="标志" SortExpression="SalesMode" />
                                                <asp:TemplateField HeaderText="库存数量" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_InventoryQuantity" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="修改"
                                                            CssClass="listViewTdLinkS1"></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除"
                                                            CssClass="listViewTdLinkS1" OnClientClick="return confirm('是否确认删除该记录?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                无数据
                                            </EmptyDataTemplate>
                                        </mcs:UC_GridView>

                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_AddDetail" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
