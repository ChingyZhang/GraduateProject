<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Reports_Rpt_ReportGridColumns, App_Web_cab7yjjs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="1">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" />
                        <mcs:MCSTabItem Text="列表字段" Value="1" Enable="false" />
                        <mcs:MCSTabItem Text="透视表字段" Value="2" />
                        <mcs:MCSTabItem Text="图表定义" Value="3" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <table cellspacing="3" cellpadding="3" width="100%" border="0">
                            <tr>
                                <td width="260px" valign="top">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                数据集字段</h3>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="cbx_SelectAllFields" runat="server" Text="全选" TextAlign="Right"
                                                                OnCheckedChanged="cbx_SelectAllFields_CheckedChanged" AutoPostBack="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="bt_Add" runat="server" Text="加 入" Width="60px" 
                                                                onclick="bt_Add_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="cbxl_Fields" runat="server" DataTextField="DisplayName" DataValueField="ID">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" align="left">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                报表输出字段</h3>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" 
                                                                onclick="bt_Save_Click" />
                                                            <asp:Button ID="bt_DeleteColumn" runat="server" Text="移 除" Width="60px" 
                                                                onclick="bt_DeleteColumn_Click" 
                                                                onclientclick="return confirm('是否确认移除选中字段列?')" />
                                                            <asp:CheckBox ID="cbx_SelectAllColumn" runat="server" AutoPostBack="true" 
                                                                OnCheckedChanged="cbx_SelectAllColumn_CheckedChanged" Text="全选" 
                                                                TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" Binded="False"
                                                    DataKeyNames="ID" Width="100%" OnRowDataBound="gv_List_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbx" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DataSetField" HeaderText="字段" SortExpression="DataSetField" />
                                                        <asp:TemplateField HeaderText="显示名称" SortExpression="DisplayName">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_DisplayName" runat="server" Text='<%#Bind("DisplayName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="排序号" SortExpression="ColumnSortID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_ColumnSortID" runat="server" Text='<%#Bind("ColumnSortID") %>'></asp:Label>
                                                                <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="↓" />
                                                                <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="↑" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="可见否" SortExpression="Visible">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rbl_Visible" runat="server" RepeatDirection="Horizontal"
                                                                    RepeatColumns="2">
                                                                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="求行总计否">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rbl_AddSummary" runat="server" RepeatDirection="Horizontal"
                                                                    RepeatColumns="2">
                                                                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
