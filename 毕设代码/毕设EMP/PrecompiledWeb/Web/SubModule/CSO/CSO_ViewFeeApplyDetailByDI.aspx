<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CSO_CSO_ViewFeeApplyDetailByDI, App_Web_quved-rv" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0px">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0px" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="营养新客费打款信息表"></asp:Label></h2>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Save" runat="server" Text="保存凭证号" Width="80px" OnClick="bt_Save_Click" />
                            <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="导出为Excel"
                                Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap>
                            <h3>
                                查询条件
                            </h3>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="80px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0px">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" DisplayRoot="True" />
                                </td>
                                <td>
                                    经销商
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="200px" />
                                </td>
                                <td>
                                    月份
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" AutoPostBack="True" DataTextField="Name"
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_Flag" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="A" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="N">未打款</asp:ListItem>
                                        <asp:ListItem Value="Y">已打款</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="15" Width="100%" DataKeyNames="ID" OnPageIndexChanging="gv_List_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="开户行">
                            <ItemTemplate>
                                <asp:Label ID="lb_DoctorAccountBankName" runat="server" Text='<%# SplitString(Eval("Remark").ToString(),1) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="账号">
                            <ItemTemplate>
                                <asp:Label ID="lb_DoctorBankAccount" runat="server" Text='<%# SplitString(Eval("Remark").ToString(),2) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="账户名">
                            <ItemTemplate>
                                <asp:Label ID="lb_DoctorAccountName" runat="server" Text='<%# SplitString(Eval("Remark").ToString(),3) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="打款金额" DataField="ApplyCost" DataFormatString="{0:0.00}" />
                        <asp:TemplateField HeaderText="凭证号">
                            <ItemTemplate>
                                <asp:Label ID="bt_BankVoucherNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem[\"BankVoucherNo\"]").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="录入打款凭证号">
                            <ItemTemplate>
                                <asp:TextBox ID="tbx_BankVoucherNo" runat="server" Text=""></asp:TextBox>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
