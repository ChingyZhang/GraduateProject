<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowDataObjectList.aspx.cs" Inherits="SubModule_EWF_FlowDataObjectList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                工作流数据字段列表</h2>
                        </td>
                        <td align="right">
                            流程名称:
                            <asp:HyperLink ID="lb_AppName" runat="server" CssClass="listViewTdLinkS1">[HyperLink4]</asp:HyperLink>
                        </td>
                        <td align="right">
                            名称:
                            <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" Text="查询" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保 存" Width="60px"
                                ForeColor="Red" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" SelectedIndex="2" OnOnTabClicked="MCSTabControl1_OnTabClicked"
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="Name" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                            OnRowDeleting="gv_FeeDetialList_RowDeleting" Binded="False" ConditionString=""
                            PanelCode="" TotalRecordCount="0">
                            <Columns>
                                <asp:ButtonField CommandName="Select" Text="选择" ControlStyle-CssClass="listViewTdLinkS1"
                                    ItemStyle-Width="30px">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="Name" HeaderText="变量名称" SortExpression="Name" />
                                <asp:BoundField DataField="DisplayName" HeaderText="显示名称" SortExpression="DisplayName" />
                                <asp:BoundField DataField="DataType" HeaderText="数据类型" SortExpression="DataType" />
                                <asp:TemplateField HeaderText="显示顺序" SortExpression="SortID">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SortID" runat="server" Text='<%# Bind("SortID") %>' Width="20px"></asp:Label>
                                        <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="+" />
                                        <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="-" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ReadOnly" HeaderText="只读" SortExpression="ReadOnly" />
                                <asp:BoundField DataField="Visible" HeaderText="可见" SortExpression="Visible" />
                                <asp:BoundField DataField="RelationType" HeaderText="关联类型" SortExpression="RelationType" />
                                <asp:BoundField DataField="RelationTableName" HeaderText="关联表名" SortExpression="RelationTableName" />
                                <asp:ButtonField CommandName="Delete" Text="删除" ControlStyle-CssClass="listViewTdLinkS1"
                                    ItemStyle-Width="30px">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:ButtonField>
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
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row" height="28">
                    <tr>
                        <td>
                            <h3>
                                工作流数据字段详细设置</h3>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="新 增" ValidationGroup="2"
                                        Width="60px" /></ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    数据类型
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_DataType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    变量名称
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_Name" runat="server" Width="120px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空" Display="Dynamic"
                                        ValidationGroup="2"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbx_Name"
                                        ErrorMessage="请输入英文字母" ValidationExpression='([a-z]|[A-Z]|_)([a-z]|[A-Z]|[0-9]|_)*'
                                        Display="Dynamic" ValidationGroup="2"></asp:RegularExpressionValidator>
                                </td>
                                <td class="dataLabel">
                                    显示名称
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_DisplayName" Width="120px" runat="server"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server" ControlToValidate="tbx_DisplayName" ErrorMessage="不能为空" Display="Dynamic"
                                        ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    只读属性
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_ReadOnly" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">只读</asp:ListItem>
                                        <asp:ListItem Value="N" Selected="True">非只读</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel">
                                    编辑属性
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_Enable" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y" Selected="True">可编辑</asp:ListItem>
                                        <asp:ListItem Value="N">不可编辑</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel">
                                    可见属性
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_Visible" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="Y" Selected="True">可见</asp:ListItem>
                                        <asp:ListItem Value="N">不可见</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    控件类型
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ControlType" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    控件宽
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ControlWidth" runat="server" Width="60px">100</asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_ControlWidth"
                                        Display="Dynamic" ErrorMessage="只能为数字" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    控件高
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ControlHeight" runat="server" Width="60px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_ControlHeight"
                                        Display="Dynamic" ErrorMessage="只能为数字" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    控件样式
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ControlStyle" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    占列宽数
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ColSpan" runat="server" Width="60px">1</asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_ColSpan"
                                        Display="Dynamic" ErrorMessage="只能为数字" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    显示顺序
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_SortID" runat="server" Width="60px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server" ControlToValidate="tbx_SortID" Display="Dynamic" ErrorMessage="不能为空"
                                        ValidationGroup="2"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="只能为数字" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    是否必填
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_IsRequireField" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">必填</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">非必填</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel">
                                    正则表达式
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_RegularExpression" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    格式字符串
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_FormatString" runat="server" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    字段说明
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_Description" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    关联查询页
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_SearchPageURL" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    关联类型
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_RelationType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_RelationType_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="1">字典</asp:ListItem>
                                        <asp:ListItem Value="2">表</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="3">不关联</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr_1" runat="server" visible="false">
                                <td class="dataLabel">
                                    关联表名
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_RelationTableName" runat="server" DataTextField="TableName"
                                        DataValueField="TableName" OnSelectedIndexChanged="ddl_RelationTableName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="tr_2" runat="server" visible="false">
                                <td class="dataLabel">
                                    关联值字段
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_RelationValueField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    &nbsp;关联文本字段
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_RelationTextField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row" height="28">
                    <tr>
                        <td>
                            <h3>
                                增加预定义数据字段</h3>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                    <tr>
                        <td class="dataLabel" width="120px">
                            可选的预定义数据字段
                        </td>
                        <td class="dataField">
                            <asp:CheckBoxList ID="cbx_PreDefineDO" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="Position">发起人职位</asp:ListItem>
                                <asp:ListItem Value="OrganizeCity">当前管理片区</asp:ListItem>
                                <asp:ListItem Value="OfficeCity">所属办事处(省区)</asp:ListItem>
                                <asp:ListItem Value="Remark">备注</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_AddDefineDataObject" runat="server" Text="增 加" Width="60px" 
                                onclick="bt_AddDefineDataObject_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
