<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowProcessList.aspx.cs" Inherits="SubModule_EWF_FlowProcessList" %>

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
                                流程环节列表</h2>
                        </td>
                        <td align="right">
                            流程名称:
                            <asp:HyperLink ID="lb_AppName" runat="server" CssClass="listViewTdLinkS1">[HyperLink4]</asp:HyperLink>
                            <font color="red">新增时请选择环节类型：</font><asp:DropDownList ID="ddl_Type" DataTextField="Value"
                                DataValueField="Key" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" SelectedIndex="1" OnOnTabClicked="MCSTabControl1_OnTabClicked"
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
            <td align="center">
                <mcs:UC_GridView ID="gv_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="ID" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Sort" HeaderText="排序号" SortExpression="Sort" />
                        <asp:TemplateField HeaderText="环节名称">
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'
                                    runat="server" ID="lb_Name"></asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Type" HeaderText="环节类型" SortExpression="Type" />
                        <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" />
                        <asp:BoundField DataField="DefaultNextProcess" HeaderText="默认下一环节" SortExpression="Description" />
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
