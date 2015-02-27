<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_Budget_BudgetTransfer, App_Web_gigc93-l" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                        预算额度调拔分配
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Transfer" runat="server" OnClick="bt_Transfer_Click" Text="调拔预算"
                                        OnClientClick="return confirm('是否确认调拔预算？')" Width="80px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr height="28px">
                                                        <td nowrap>
                                                            <h3>
                                                                （源）预算管理单元</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabForm">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel">
                                                            管理片区
                                                        </td>
                                                        <td class="dataField">
                                                            <mcs:MCSTreeControl ID="tr_FromOrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_FromOrganizeCity_Selected" />
                                                        </td>
                                                        <td class="dataLabel">
                                                            月份
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:DropDownList ID="ddl_FromMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddl_FromMonth_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <mcs:UC_GridView ID="gv_FromBalance" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                DataKeyNames="ID,FeeType" RowStyle-Height="26px">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="费用类型" DataField="FeeType" />
                                                                    <asp:BoundField HeaderText="预算余额" DataField="CostBalance" HtmlEncode="false" DataFormatString="{0:0.##}" />
                                                                    <asp:TemplateField HeaderText="调拨金额">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="tbx_TransferAmount" runat="server" Width="60px" Text='0'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="调拨说明">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="tbx_Remark" runat="server" Width="160px" Text=''></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="10px">
                                    →<br />
                                    →<br />
                                    →<br />
                                    →<br />
                                    →<br />
                                    →<br />
                                </td>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr height="28px">
                                                        <td nowrap>
                                                            <h3>
                                                                （目的）预算管理单元</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabForm">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel">
                                                            管理片区
                                                        </td>
                                                        <td class="dataField">
                                                            <mcs:MCSTreeControl ID="tr_ToOrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_ToOrganizeCity_Selected" />
                                                        </td>
                                                        <td class="dataLabel">
                                                            月份
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:DropDownList ID="ddl_ToMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddl_ToMonth_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <mcs:UC_GridView ID="gv_ToBalance" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                RowStyle-Height="26px">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="费用类型" DataField="FeeType" />
                                                                    <asp:BoundField HeaderText="预算余额" DataField="CostBalance" HtmlEncode="false" DataFormatString="{0:0.##}" />
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
