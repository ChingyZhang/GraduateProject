<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderStorageDetail, App_Web_aozhikkk" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="入库反馈详细信息"></asp:Label>
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
<%--            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pn_OrderDelivery" runat="server" DetailViewCode="DV_LGS_OrderDelivery_Detail01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>--%>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                入库反馈列表</h3>
                        </td>
                        <td align="right" height="28">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
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
                            DataKeyNames="Code">
                            <Columns>
                                <asp:BoundField DataField="Code" HeaderText="物料编码" SortExpression="Code" />
                                <asp:BoundField DataField="FullName" HeaderText="产品名称" SortExpression="FullName" />
                                <asp:BoundField DataField="CountMun" HeaderText="实发数量" SortExpression="CountMun" />
                                <asp:BoundField DataField="SumPrice" HeaderText="销售金额(元)" SortExpression="SumPrice" />
                                <asp:BoundField DataField="scantime" HeaderText="反馈日期" SortExpression="scantime" />
<%--                                <asp:BoundField DataField="SumMun" HeaderText="应反馈数量" SortExpression="SumMun" />
                                <asp:BoundField DataField="SumMun" HeaderText="实际反馈数量" SortExpression="SumMun" />--%>
                                 <asp:TemplateField HeaderText="应反馈数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SumMun" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderDeliveryBLL.GetSumNum(DataBinder.Eval(Container,"DataItem.Code").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="实际反馈数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SumMun" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderDeliveryBLL.GetSumNum(DataBinder.Eval(Container,"DataItem.Code").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

