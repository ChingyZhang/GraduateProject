<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ClientSalesTarget.aspx.cs" Inherits="SubModule_SVM_ClientSalesTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
                                员工绩效--门店销售目标</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr>
            <td class="h3Row" height="25">
               <h2>查询条件</h2>
            </td>
        </tr>--%>
        <tr class="tabForm">
            <td height="30px">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="dataLabel">
                            管理片区
                        </td>
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="220px" AutoPostBack="True" />
                        </td>
                        <td class="dataLabel">
                            零售商
                        </td>
                        <td class="dataField">
                            <mcs:MCSSelectControl ID="select_Client" runat="server" Width="280px" PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=3' />
                        </td>
                        <td class="dataLabel">
                            申请月份
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Save" runat="server" Text="保存" Width="80px" OnClick="bt_Save_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                    PanelCode="Panel_ClientSalesTarget" AutoGenerateColumns="False" DataKeyNames="ID"
                    PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="目标调整(元)" SortExpression="ReturnedVolume">
                            <ItemTemplate>
                                <asp:TextBox ID="tbx_SalesTargetAdjust" runat="server" Text='<%# Bind("SVM_ClientSalesTarget_SalesTargetAdjust")%>'
                                    Width="40px"  ></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="tbx_SalesTargetAdjust"
                                    Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
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
