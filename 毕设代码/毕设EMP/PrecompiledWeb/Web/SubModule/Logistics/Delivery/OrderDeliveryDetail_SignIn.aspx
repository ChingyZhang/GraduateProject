<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Delivery_OrderDeliveryDetail_SignIn, App_Web_1roaytgj" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="发货单详细信息"></asp:Label>
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保 存" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pn_OrderDelivery" runat="server" DetailViewCode="DV_LGS_OrderDelivery_Detail00">
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
                                发货品项列表</h3>
                        </td>
                        <td align="right" height="28">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lb_TotalFactoryValue" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                    <asp:Label ID="lb_TotalSalesValue" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:Button ID="bt_SignInAll" runat="server" Text="全部到货" Width="60px" OnClick="bt_SignInAll_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_OrderList" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="ID,Product">
                            <Columns>
                                <asp:TemplateField HeaderText="物料编码" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_ProductCode" runat="server" Text='<%# new MCSFramework.BLL.Pub.PDT_ProductBLL((int)Eval("Product")).Model.Code %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Product" HeaderText="产品名称" SortExpression="Product" />
                                <asp:BoundField DataField="FactoryPrice" HeaderText="厂价(元)" SortExpression="FactoryPrice"
                                    DataFormatString="{0:0.##}" Visible="false" />
                                <asp:BoundField DataField="Price" HeaderText="批发价(元)" SortExpression="Price" DataFormatString="{0:0.##}"
                                    Visible="false" />
                                <asp:TemplateField HeaderText="厂价金额(元)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_FactoryValue" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity")*(decimal)DataBinder.Eval(Container.DataItem,"FactoryPrice")).ToString("0.##") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="销售金额(元)">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SalesValue" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity")*(decimal)DataBinder.Eval(Container.DataItem,"Price")).ToString("0.##") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="实发数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_DeliveryQuantity_T" runat="server" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="Label10" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:Label ID="lb_DeliveryQuantity" runat="server" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="签收正品数量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_SignInQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"SignInQuantity"))).ToString() %>'
                                            AutoPostBack="true" OnTextChanged="tbx_SignInQuantity_TextChanged" Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:TextBox ID="tbx_SignInQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"SignInQuantity"))).ToString() %>'
                                            AutoPostBack="true" OnTextChanged="tbx_SignInQuantity_TextChanged" Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="破损数量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_BadQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BadQuantity"))).ToString() %>'
                                            AutoPostBack="true" OnTextChanged="tbx_BadQuantity_TextChanged" Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                        <asp:Label ID="Label3" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:TextBox ID="tbx_BadQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BadQuantity"))).ToString() %>'
                                            AutoPostBack="true" OnTextChanged="tbx_BadQuantity_TextChanged" Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                        <asp:Label ID="Label4" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="丢失数量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_LostQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"LostQuantity"))).ToString() %>'
                                            AutoPostBack="true" OnTextChanged="tbx_LostQuantity_TextChanged" Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                        <asp:Label ID="Label5" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:TextBox ID="tbx_LostQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"LostQuantity"))).ToString() %>'
                                            AutoPostBack="true" OnTextChanged="tbx_LostQuantity_TextChanged" Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                        <asp:Label ID="Label6" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="在途数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_TransitQuantity_T" runat="server" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity")-(int)DataBinder.Eval(Container.DataItem,"SignInQuantity")-(int)DataBinder.Eval(Container.DataItem,"BadQuantity")-(int)DataBinder.Eval(Container.DataItem,"LostQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:Label ID="lb_TransitQuantity" runat="server" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity")-(int)DataBinder.Eval(Container.DataItem,"SignInQuantity")-(int)DataBinder.Eval(Container.DataItem,"BadQuantity")-(int)DataBinder.Eval(Container.DataItem,"LostQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_SignInAll" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
