<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_FlowInitPosition, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                流程允许发起的职务列表</h2>
                        </td>
                        <td align="left" style="width: 76px; height: 24px">
                            &nbsp;</td>
                        <td align="right" style="height: 24px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" SelectedIndex="3" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    Width="100%">
                    <Items>
                        <mcs:MCSTabItem Text="流程基本信息" Value="0" />
                        <mcs:MCSTabItem Text="流程环节列表" Value="1" />
                        <mcs:MCSTabItem Text="流程数据字段" Value="2" />
                        <mcs:MCSTabItem Text="允许发起职位" Value="3" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                        <tr>
                                            <td class="dataLabel">
                                                职位
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    RootValue="0" Width="200px" ParentColumnName="SuperID" />
                                            </td>
                                            <td class="dataLabel">
                                                <asp:CheckBox ID="cb_IncludeChild" Text="包含所有下级职务" runat="server" />
                                            </td>
                                            <td class="dataLabel">
                                                允许发起开始日
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_BeginDay" runat="server" Width="60px">1</asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server" ControlToValidate="tbx_BeginDay" Display="Dynamic" ErrorMessage="必填"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_BeginDay"
                                                    Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbx_BeginDay"
                                                    Display="Dynamic" ErrorMessage="必需大于1小于31" MaximumValue="31" MinimumValue="1"
                                                    Type="Integer" ValidationGroup="1"></asp:RangeValidator>
                                            </td>
                                            <td class="dataLabel">
                                                允许发起截止日
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_EndDay" runat="server" Width="60px">31</asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server" ControlToValidate="tbx_EndDay" Display="Dynamic" ErrorMessage="必填"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_EndDay"
                                                    Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbx_EndDay"
                                                    Display="Dynamic" ErrorMessage="必需大于1小于31" MaximumValue="31" MinimumValue="1"
                                                    Type="Integer" ValidationGroup="1"></asp:RangeValidator>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Add" runat="server" Text="增加" OnClick="bt_Add_Click" ValidationGroup="1"
                                                    Width="60px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Binded="False" ConditionString="" PanelCode="" TotalRecordCount="0" DataKeyNames="ID"
                                        OnRowDeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="App" HeaderText="所属流程" SortExpression="App" />
                                            <asp:BoundField DataField="Position" HeaderText="职务名称" SortExpression="Position" />
                                            <asp:BoundField DataField="BeginDay" HeaderText="允许发起开始日" SortExpression="BeginDay" />
                                            <asp:BoundField DataField="EndDay" HeaderText="允许发起截止日" SortExpression="EndDay" />
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:CommandField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
