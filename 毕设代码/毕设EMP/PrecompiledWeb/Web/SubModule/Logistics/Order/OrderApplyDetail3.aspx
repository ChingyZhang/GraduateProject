<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderApplyDetail3, App_Web_b5n4-ayh" enableEventValidation="false" stylesheettheme="basic" %>

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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="定单申请--确认申请单"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right" nowrap="noWrap">
                                    <asp:Button ID="bt_EditCart" runat="server" Text="←.修改明细" OnClick="bt_EditCart_Click" Width="80px"/>
                                    <asp:Button ID="bt_Save" runat="server" Text="暂存申请单" OnClick="bt_Save_Click" Width="80px"/>
                                    <asp:Button ID="bt_Submit" runat="server" Text="提交申请" OnClick="bt_Submit_Click" OnClientClick="return confirm('是否确认提交申请该申请单?');" Width="80px"/>
                                    <asp:Button ID="bt_Delete" runat="server" Text="删除申请单" OnClientClick="return confirm('是否确认删除该申请单?');"
                                        OnClick="bt_Delete_Click" Width="80px"/>
                                    <asp:Button ID="bt_Finish" runat="server" OnClientClick="return confirm(&quot;是否确定将该申请单中尚未完成全部发放的产品终止发放申请?&quot;)"
                                        Text="终止申请" ToolTip="将剩余未发放的数量终止申请，不再发放" Width="80px" OnClick="bt_Finish_Click" />
                                    <asp:Button ID="bt_ReApply" runat="server" Text="重新申请" OnClick="bt_ReApply_Click"
                                        Width="80px" />
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
                        <mcs:UC_DetailView ID="pn_OrderApply" runat="server" DetailViewCode="Page_LGS_OrderApplyDetail">
                        </mcs:UC_DetailView>
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
                                <td align="right"  id="td_actfeerate" runat="server" visible="false">
                                    本次申请实际费率:<asp:Label ID="lb_ActFeeRate" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>%                                    
                                </td>
                                <td align="right"  id="td_AfterSubmitBalance" runat="server" visible="false">
                                    本单提交后余额:<asp:Label ID="lb_AfterSubmitBalance" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                    元
                                </td>
                                <td align="right" id="td_Percent" runat="server" visible="false">
                                    本单占可申请余额比:<asp:Label ID="lb_Percent" runat="server" Font-Size="Larger" ForeColor="Red"
                                        Text="0"></asp:Label>
                                    元
                                </td>
                                <td align="right">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="Product,ID" OnDataBound="gv_List_DataBound">
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
                                                        ForeColor="Red" Font-Bold="true"></asp:Label>
                                                    (<asp:Label ID="Label2" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"Code") %>'></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">规格:</font>
                                                    <asp:Label ID="lb_Spec" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"Spec") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">每箱容量:</font>
                                                    <asp:Label ID="lb_ConvertFactor" runat="server" Text='<%# GetProductInfo((int)DataBinder.Eval(Container,"DataItem.Product"),"ConvertFactor") %>'></asp:Label><asp:Label
                                                        ID="lb_Packaging_P" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>/<asp:Label
                                                            ID="lb_Packaging_T0" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">赠送坎级:</font>
                                                    <asp:Label ID="lb_GiveLevel" runat="server" Text='<%# GetPublishDetailGiveLevel((int)DataBinder.Eval(Container,"DataItem.Product")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <font color="blue">备注说明:</font>
                                                    <asp:Label ID="lb_Remark" runat="server" Text='<%# GetPublishDetailRemark((int)DataBinder.Eval(Container,"DataItem.Product")) %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="申请数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_BookQuantity_T" runat="server" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="lb_Packaging_T6" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="批复数量" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_AdjustQuantity_T" ForeColor="Red" Font-Bold="true" runat="server"
                                            Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BookQuantity") + (int)DataBinder.Eval(Container.DataItem,"AdjustQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="lb_Packaging_T7" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调整原因" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_AdjustReason" runat="server" Text='<%# Bind("AdjustReason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额">
                                    <ItemTemplate>
                                        <asp:Label ID="tbx_Fee" runat="server" ForeColor="Red" Font-Bold="true" Text='<%# (((int)DataBinder.Eval(Container.DataItem,"BookQuantity") + (int)DataBinder.Eval(Container.DataItem,"AdjustQuantity")) *(decimal)DataBinder.Eval(Container.DataItem,"Price")).ToString("0.00")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Button ID="bt_OpenAdjust" runat="server" Text="调整" OnClick="bt_OpenAdjust_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="已发放数量" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_DeliveryQuantity_T" runat="server" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity"))).ToString() %>'></asp:Label>
                                        <asp:Label ID="lb_Packaging_T8" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
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
