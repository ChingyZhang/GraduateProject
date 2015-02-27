<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Reports_Rpt_ReportMatrixTable, App_Web_cab7yjjs" enableEventValidation="false" stylesheettheme="basic" %>

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
                            <asp:Button ID="bt_Save" runat="server" onclick="bt_Save_Click" Text="保 存" 
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="2">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" />
                        <mcs:MCSTabItem Text="列表字段" Value="1" />
                        <mcs:MCSTabItem Text="透视表字段" Value="2" Enable="false" />
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
                        <table cellspacing="5" cellpadding="5" border="0" width="100%">
                            <tr>
                                <td valign="top" align="left">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                数据集字段</h3>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="bt_Add_ColumnGroup" runat="server" Text="→加入列组" Width="80px" 
                                                                onclick="bt_Add_ColumnGroup_Click" />
                                                            <asp:Button ID="bt_Add_RowGroup" runat="server" Text="↓加入行组" Width="80px" 
                                                                onclick="bt_Add_RowGroup_Click" />
                                                            <asp:Button ID="bt_Add_ValueGroup" runat="server" Text="↘加入数值" Width="80px" 
                                                                onclick="bt_Add_ValueGroup_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="cbxl_Fields" runat="server" DataTextField="DisplayName" DataValueField="ID"
                                                    RepeatColumns="4" CellPadding="-1" CellSpacing="5">
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
                                                        <td width="16px">
                                                            <img src="../../Images/gif/MatrixColumnGroup.png" />
                                                        </td>
                                                        <td>
                                                            <h3>
                                                                列标签</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_List_ColumnGroup" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" Width="100%" OnRowDataBound="gv_List_ColumnGroup_RowDataBound"
                                                    Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0"
                                                    OnRowDeleting="gv_List_ColumnGroup_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="DataSetField" HeaderText="字段" SortExpression="DataSetField" />
                                                        <asp:TemplateField HeaderText="显示名称" SortExpression="DisplayName">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_DisplayName" runat="server" Text='<%#Bind("DisplayName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="排序" SortExpression="GroupSortID">
                                                            <ItemTemplate>
                                                                <asp:Button ID="bt_Increase_ColumnGroup" runat="server" OnClick="bt_Increase_ColumnGroup_Click"
                                                                    Text="↓" />
                                                                <asp:Button ID="bt_Decrease_ColumnGroup" runat="server" OnClick="bt_Decrease_ColumnGroup_Click"
                                                                    Text="↑" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="列小计">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rbl_AddSummary" runat="server" RepeatDirection="Horizontal"
                                                                    RepeatColumns="2">
                                                                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibt_Delete_ColumnGroup" ImageUrl="~/Images/gif/gif-0965.gif"
                                                                    CommandName="Delete" ToolTip="删除" runat="server" OnClientClick="return confirm('是否确认删除?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                    <tr>
                                                        <td width="16px">
                                                            <img src="../../Images/gif/MatrixRowGroup.png" />
                                                        </td>
                                                        <td>
                                                            <h3>
                                                                行标签</h3>
                                                        </td>
                                                        <td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_List_RowGroup" runat="server" AutoGenerateColumns="False"
                                                    Binded="False" DataKeyNames="ID" Width="100%" OnRowDataBound="gv_List_RowGroup_RowDataBound"
                                                    OnRowDeleting="gv_List_RowGroup_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="DataSetField" HeaderText="字段" SortExpression="DataSetField" />
                                                        <asp:TemplateField HeaderText="显示名称" SortExpression="DisplayName">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_DisplayName" runat="server" Text='<%#Bind("DisplayName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="排序" SortExpression="GroupSortID">
                                                            <ItemTemplate>
                                                                <asp:Button ID="bt_Increase_RowGroup" runat="server" OnClick="bt_Increase_RowGroup_Click"
                                                                    Text="↓" />
                                                                <asp:Button ID="bt_Decrease_RowGroup" runat="server" OnClick="bt_Decrease_RowGroup_Click"
                                                                    Text="↑" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="行小计">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rbl_AddSummary" runat="server" RepeatDirection="Horizontal"
                                                                    RepeatColumns="2">
                                                                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibt_Delete_RowGroup" ImageUrl="~/Images/gif/gif-0965.gif" CommandName="Delete"
                                                                    ToolTip="删除" runat="server" OnClientClick="return confirm('是否确认删除?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" align="left">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                ∑数值</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_List_ValueGroup" runat="server" AutoGenerateColumns="False"
                                                    Binded="False" DataKeyNames="ID" Width="100%" OnRowDataBound="gv_List_ValueGroup_RowDataBound"
                                                    OnRowDeleting="gv_List_ValueGroup_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="DataSetField" HeaderText="字段" SortExpression="DataSetField" />
                                                        <asp:TemplateField HeaderText="显示名称" SortExpression="DisplayName">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_DisplayName" runat="server" Text='<%#Bind("DisplayName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="排序" SortExpression="ValueSortID">
                                                            <ItemTemplate>
                                                                <asp:Button ID="bt_Increase_ValueGroup" runat="server" OnClick="bt_Increase_ValueGroup_Click"
                                                                    Text="↓" />
                                                                <asp:Button ID="bt_Decrease_ValueGroup" runat="server" OnClick="bt_Decrease_ValueGroup_Click"
                                                                    Text="↑" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="统计方式">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddl_StatisticMode" runat="server" DataTextField="Value" DataValueField="Key" >
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibt_Delete_ValueGroup" ImageUrl="~/Images/gif/gif-0965.gif"
                                                                    CommandName="Delete" ToolTip="删除" runat="server" OnClientClick="return confirm('是否确认删除?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="dataLabel" colspan="2" style="color: #FF0000" 
                                    valign="top">
                                    注:当行标签及数值字段发生变化后，请重新设定图表设置，以保存图表正常呈现。</td>
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
