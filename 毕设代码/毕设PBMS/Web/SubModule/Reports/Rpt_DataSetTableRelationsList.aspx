<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_DataSetTableRelationsList.aspx.cs" Inherits="SubModule_Reports_Rpt_DataSetTableRelationsList" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表数据集表关系列表"></asp:Label></h2>
                        </td>
                        <td class="dataLabel">
                            数据集:<asp:Label ID="lb_DataSetName" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="3">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" />
                        <mcs:MCSTabItem Text="数据集参数" Value="1" />
                        <mcs:MCSTabItem Text="包含数据表" Value="2" />
                        <mcs:MCSTabItem Text="数据表关系" Value="3" Enable="false" />
                        <mcs:MCSTabItem Text="数据集字段" Value="4" />
                        <mcs:MCSTabItem Text="查询条件" Value="5" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr class="tabForm">
                                <td>
                                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="dataLabel">
                                                主表
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_ParentTable" runat="server" DataTextField="DisplayName"
                                                    DataValueField="ID" OnSelectedIndexChanged="ddl_ParentTable_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                主表字段
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_ParentField" runat="server" DataTextField="DisplayName"
                                                    DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                子表
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_ChildTable" runat="server" DataTextField="DisplayName"
                                                    DataValueField="ID" OnSelectedIndexChanged="ddl_ChildTable_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                子表字段
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_ChildField" runat="server" DataTextField="DisplayName"
                                                    DataValueField="ID" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                关联方式
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_RelateionMode" runat="server" DataTextField="Value" DataValueField="Key">
                                                    <asp:ListItem Value="INNER JOIN" Selected="True">内联</asp:ListItem>
                                                    <asp:ListItem Value="LEFT OUTER JOIN">左关联</asp:ListItem>
                                                    <asp:ListItem Value="RIGHT OUTER JOIN">右关联</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                &nbsp;排序
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_SortID" runat="server" Width="50px">1</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_SortID"
                                                    Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                                    Display="Dynamic" ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                关联条件
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <asp:TextBox ID="tbx_RelationCondition" runat="server" Width="600px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Rpt_DataSetTableRelations_ID" PageSize="15" Width="100%" PanelCode="Panel_Rpt_DataSetTableRelations_List"
                                        OnSelectedIndexChanging="gv_List_SelectedIndexChanging" Binded="False" ConditionString=""
                                        OrderFields="" TotalRecordCount="0" OnRowDeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                        Text="编辑"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                        Text="删除" OnClientClick="return confirm('是否确认删除该记录?')"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Add" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
