<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_Budget_BudgetPercentDetail, App_Web_gigc93-l" enableEventValidation="false" stylesheettheme="basic" %>

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
                                        预算按费用类型设定分配比例
                                    </h2>
                                </td>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="False" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                    <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID,FeeType" AllowPaging="True" PageSize="15">
                            <Columns>
                                <asp:BoundField DataField="FeeType" HeaderText="费用类型" />
                                <asp:TemplateField HeaderText="占比">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_BudgetPercent" runat="server" Width="80px" Text='<%#Bind("BudgetPercent","{0:0.##}") %>'></asp:TextBox>%
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_BudgetPercent"
                                            Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_BudgetPercent"
                                            Display="Dynamic" ErrorMessage="必需为数值型" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_BudgetPercent"
                                            Display="Dynamic" ErrorMessage="必需大于等于0" Operator="GreaterThanEqual" Type="Double"
                                            ValueToCompare="0"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_BudgetPercent"
                                            Display="Dynamic" ErrorMessage="必需小于等于100" Operator="LessThanEqual" Type="Double"
                                            ValueToCompare="100"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
                <tr>
                    <td height="38px" valign="bottom">
                        &nbsp;请正确维护各费用类型所占预算的百分比率。所有百分比之和必须小于或等于100，当小于100时，其余部分用作机动费用！
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
