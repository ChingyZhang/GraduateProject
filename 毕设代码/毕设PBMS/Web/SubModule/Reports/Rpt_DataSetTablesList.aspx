<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_DataSetTablesList.aspx.cs" Inherits="SubModule_Reports_Rpt_DataSetTablesList" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表数据集包含表列表"></asp:Label></h2>
                        </td>
                        <td class="dataLabel">
                            数据集:<asp:Label ID="lb_DataSetName" runat="server" Text=""></asp:Label>
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
                    SelectedIndex="2">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" />
                        <mcs:MCSTabItem Text="数据集参数" Value="1" />
                        <mcs:MCSTabItem Text="包含数据表" Value="2" Enable="false" />
                        <mcs:MCSTabItem Text="数据表关系" Value="3" />
                        <mcs:MCSTabItem Text="数据集字段" Value="4" />
                        <mcs:MCSTabItem Text="查询条件" Value="5" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="dataLabel">
                                        数据表名
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_TableName" runat="server" DataTextField="DisplayName" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataField" align="right">
                                        <asp:Button ID="bt_Add" runat="server" Text="新 增" OnClick="bt_Add_Click" Width="60px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Rpt_DataSetTables_ID" PageSize="15" Width="100%" PanelCode="Panel_Rpt_DataSetTables_List"
                                        Binded="False" ConditionString="" OrderFields="" TotalRecordCount="0" 
                                        onrowdeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClientClick="return confirm('是否确认删除该记录?')"
                                                        CommandName="Delete" Text="删除"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Add" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
