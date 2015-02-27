<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderProductApplyDetail.aspx.cs" Inherits="SubModule_Logistics_Order_OrderProductApplyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                定单请购申请单详细信息
                            </h2>
                        </td>
                        <td nowrap="noWrap" align="left">
                            &nbsp;
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保 存" Width="60px" />
                            <asp:Button ID="bt_Submit" runat="server" Text="提交申请" Width="60px" OnClientClick="return confirm(&quot;确认提交申请?&quot;)"
                                OnClick="bt_Submit_Click" />
                            <asp:Button ID="bt_Delete" runat="server" Text="删除" Width="60px" OnClientClick="return confirm(&quot;确认删除?&quot;)"
                                OnClick="bt_Delete_Click" />
                                <asp:Button ID="bt_print" runat="server" Text="打印" Width="60px" 
                                onclick="bt_print_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pn_OrderApply" runat="server" DetailViewCode="page_Product_OrderyDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                定单请购申请明细列表</h3>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="bt_SaveAdjust" runat="server" OnClick="bt_SaveAdjust_Click" Text="保存调整"
                                        Width="80px" Visible="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_ProductList" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID,Product">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1"
                                    Visible="false" SelectText="选择">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                </asp:CommandField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lb_State" runat="server" Text="" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Product" HeaderText="产品" SortExpression="Product" />
                                <asp:TemplateField HeaderText="产品编码">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_ProductCode" runat="server" Text='<%# new MCSFramework.BLL.Pub.PDT_ProductBLL((int)DataBinder.Eval(Container,"DataItem.Product")).Model.Code %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单价">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Price" runat="server" Text='<%#Bind("Price") %>' Width="40px" Enabled="<%#priceEnable %>"></asp:TextBox><asp:CompareValidator
                                            ID="CompareValidator1" runat="server" ControlToValidate="tbx_BookQuantity_T"
                                            Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_BookQuantity_T"
                                                Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="自有库库存">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_StoreInventory" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"StoreInventory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月最大进货量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_MaxPurchaseVolume" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"MaxPurchaseVolume") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本月最低进货量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_MinPurchaseVolume" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"MinPurchaseVolume") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="期初库存">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_PreQuantity" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"BeginningInventory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="累计有效订货量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_OrderVolume" runat="server" Text='<%# GetJXCQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),"OrderVolume") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="申请数量" SortExpression="BookQuantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_BookQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity"))).ToString() %>'></asp:TextBox><asp:CompareValidator
                                            ID="CompareValidator7" runat="server" ControlToValidate="tbx_BookQuantity_T"
                                            Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbx_BookQuantity_T"
                                                Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator><asp:Label ID="lb_BookPackaging_T"
                                                    runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><asp:TextBox
                                                        ID="tbx_BookQuantity" runat="server" Width="30px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity"))).ToString() %>'></asp:TextBox><asp:Label
                                                            ID="lb_BookPackaging" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><asp:CompareValidator
                                                                ID="CompareValidator8" runat="server" ControlToValidate="tbx_BookQuantity" Display="Dynamic"
                                                                ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbx_BookQuantity"
                                                                    Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调整数量" SortExpression="AdjustQuantity" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_AdjustQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AdjustQuantity"))).ToString() %>'></asp:TextBox><asp:CompareValidator
                                            ID="CompareValidator5" runat="server" ControlToValidate="tbx_AdjustQuantity_T"
                                            Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbx_AdjustQuantity_T"
                                                Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator><asp:Label ID="lb_Packaging_T"
                                                    runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><asp:TextBox
                                                        ID="tbx_AdjustQuantity" runat="server" Width="30px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AdjustQuantity"))).ToString() %>'></asp:TextBox><asp:Label
                                                            ID="lb_Packaging" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><asp:CompareValidator
                                                                ID="CompareValidator6" runat="server" ControlToValidate="tbx_AdjustQuantity"
                                                                Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbx_AdjustQuantity"
                                                                    Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调整原因" SortExpression="AdjustReason" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_AdjustReason" runat="server" Text='<%# Bind("AdjustReason") %>'
                                            Width="150px"></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额" SortExpression="DeliveryQuantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_BookCost" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.Price") * ((int)DataBinder.Eval(Container,"DataItem.BookQuantity") + (int)DataBinder.Eval(Container,"DataItem.AdjustQuantity"))).ToString()%>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="已发放数量" SortExpression="DeliveryQuantity" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_DeliveryQuantity" runat="server" Text='<%# GetQuantityString((int)DataBinder.Eval(Container,"DataItem.Product"),(int)DataBinder.Eval(Container,"DataItem.DeliveryQuantity"))%>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" />
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_SaveAdjust" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right" height="28">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        合计费用：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>元</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
