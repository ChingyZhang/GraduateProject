<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderAlibraryDetail, App_Web_b5n4-ayh" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                出库列表</h3>
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
                                <asp:BoundField DataField="SumPrice" HeaderText="发货金额（厂价）" SortExpression="SumPrice" />
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

