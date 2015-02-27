<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderApplyDetail2, App_Web_b5n4-ayh" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="定单申请--购物车明细"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right" nowrap="noWrap">
                                    <asp:Button ID="bt_Continue" runat="server" OnClick="bt_Continue_Click" Text="←选择品项" />
                                    <asp:Button ID="bt_Confirm" runat="server" OnClick="bt_Confirm_Click" Text="下一步→" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tb_GiftBudgetInfo" runat="server" visible="false" border="0" cellpadding="0"
                            cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    预算额度信息</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tabDetailView">
                                        <tr>
                                            <td class="tabDetailViewDL">
                                                管理片区
                                            </td>
                                            <td class="tabDetailViewDF">
                                                <asp:Label ID="lb_OrganizeCityName" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tabDetailViewDL">
                                                客户
                                            </td>
                                            <td class="tabDetailViewDF">
                                                <asp:Label ID="lb_ClientName" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="tabDetailViewDL">
                                                收货经销商
                                            </td>
                                            <td>
                                                <asp:Label ID="lb_Receiver" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabDetailViewDL">
                                                赠品可用预算
                                            </td>
                                            <td class="tabDetailViewDF">
                                                <asp:Label ID="lb_TotalBudget" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                <asp:HyperLink ID="hl_ViewBudget" runat="server" ForeColor="#CC0000" NavigateUrl="~/SubModule/FNA/Budget/BudgetBalance.aspx"
                                                    >查看预算信息</asp:HyperLink>
                                            </td>
                                            <td class="tabDetailViewDL">
                                                该品牌可申请额度
                                            </td>
                                            <td class="tabDetailViewDF">
                                                <asp:Label ID="lb_AvailableAmount" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="tabDetailViewDL">
                                                该品牌可申请余额
                                            </td>
                                            <td class="tabDetailViewDF">
                                                <asp:Label ID="lb_BalanceAmount" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
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
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        <img alt="" src="../../../Images/gif/gif-0045.gif" style="width: 45px; height: 29px" />
                                        购物车内品项明细</h3>
                                </td>
                                <td align="right">
                                    本单合计金额:<asp:Label ID="lb_TotalCost" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                    元
                                </td>
                                 <td align="right" >
                                    本次申请实际费率:<asp:Label ID="lb_ActFeeRate" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>%                                    
                                </td>
                                <td align="right">
                                    本单提交后余额:<asp:Label ID="lb_AfterSubmitBalance" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                    元
                                </td>
                                <td align="right">
                                    本单占可申请余额比:<asp:Label ID="lb_Percent" runat="server" Font-Size="Larger" ForeColor="Red"
                                        Text="0"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                        Text="全选" />
                                    <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" Text="删除" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="Product" OnRowDeleting="gv_List_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="申请品项">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# Bind("Product","~/SubModule/Product/ProductPictureList.aspx?ID={0}")%>'
                                                        Text="" CssClass="listViewTdLinkS1"  ImageUrl='<%# MCSFramework.BLL.Pub.ATMT_AttachmentBLL.GetFirstPreviewPicture(11,(int)DataBinder.Eval(Container,"DataItem.Product"))%>'>
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lb_ProductName" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"FullName") %>'
                                                        ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                                    (<asp:Label ID="Label2" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"Code") %>'></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">规格:</font><asp:Label ID="lb_Spec" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"Spec") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">每箱容量:</font><asp:Label ID="lb_ConvertFactor" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"ConvertFactor") %>'></asp:Label><asp:Label
                                                        ID="lb_Packaging_P" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>/<asp:Label
                                                            ID="lb_Packaging_T0" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">赠送坎级:</font><asp:Label ID="lb_GiveLevel" runat="server" Text='<%# GetPublishDetailGiveLevel((int)DataBinder.Eval(Container,"DataItem.Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">备注说明:</font><asp:Label ID="lb_Remark" runat="server" Text='<%# GetPublishDetailRemark((int)DataBinder.Eval(Container,"DataItem.Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="价格">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_Price" ForeColor="Red" runat="server" Text='<%# Bind("Price", "{0:0.00}") %>'></asp:Label>元/<asp:Label
                                            ID="lb_Packaging_P0" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><br />
                                        <br />
                                        <asp:Label ID="lb_Price_T" ForeColor="Red" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"Price")*Decimal.Parse(GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"ConvertFactor"))).ToString("0.00") %>'></asp:Label>元/<asp:Label
                                            ID="lb_Packaging_T1" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="申请数量说明">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr visible='<%# (int)DataBinder.Eval(Container.DataItem,"MinQuantity")>0 %>' runat="server">
                                                <td width="80px" height="28px">
                                                    单次最少
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_MinQuantity_T" runat="server" Font-Bold="true" ForeColor="Red"
                                                        Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"MinQuantity"))).ToString() %>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging_T2" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr visible='<%# (int)DataBinder.Eval(Container.DataItem,"MaxQuantity")>0 %>' runat="server">
                                                <td width="80px" height="28px">
                                                    单次最大
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_MaxQuantity_T" runat="server" Font-Bold="true" ForeColor="Red"
                                                        Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"MaxQuantity"))).ToString() %>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging_T3" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr visible='<%# (int)DataBinder.Eval(Container.DataItem,"AvailableQuantity")>0 %>'
                                                runat="server">
                                                <td width="80px" height="28px">
                                                    总计可申请量
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_AvailableQuantity_T" runat="server" Font-Bold="true" ForeColor="Red"
                                                        Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AvailableQuantity"))).ToString() %>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging_T4" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr visible='<%# (int)DataBinder.Eval(Container.DataItem,"AvailableQuantity")>0 %>'
                                                runat="server">
                                                <td width="80px" height="28px">
                                                    已申请总量<br />
                                                    (含其他区域)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_SubmitQuantity" runat="server" Font-Bold="true" ForeColor="Red"
                                                        Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"SubmitQuantity"))).ToString()%>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging_T5" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="申请数量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_BookQuantity_T" runat="server" Width="50px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity"))).ToString() %>'
                                            AutoPostBack="True" OnTextChanged="tbx_BookQuantity_T_TextChanged"></asp:TextBox>
                                        <asp:Label ID="lb_Packaging_T6" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:ImageButton ID="bt_Sub" runat="server" ImageUrl="~/Images/gif/gif-0900.gif"
                                            OnClick="bt_Sub_Click" />
                                        <asp:ImageButton ID="bt_Add" runat="server" ImageUrl="~/Images/gif/gif-0901.gif"
                                            OnClick="bt_Add_Click" Style="width: 12px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额">
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_Fee" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"BookQuantity") *(decimal)DataBinder.Eval(Container.DataItem,"Price")).ToString("0.00元")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbt_Select" CommandName="Delete" runat="server" ImageUrl="~/Images/gif/gif-0965.gif"
                                            OnClientClick="return confirm('是否确认从购物车中移除该产品?')" ToolTip="删除" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
