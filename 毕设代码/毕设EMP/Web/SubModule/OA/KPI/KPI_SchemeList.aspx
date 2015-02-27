<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="KPI_SchemeList.aspx.cs" Inherits="SubModule_OA_KPI_KPI_SchemeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upl_KPI_Item" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <h2>
                                        员工职位和其关联的考核项目维护</h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Select" runat="server" Text="查找" Width="60px" OnClick="bt_Select_Click" />
                                    <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" 
                                        PostBackUrl="~/SubModule/OA/KPI/KPI_SchemeDetail.aspx"/>                                      
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>                               
                                <td class="dataLabel">
                                    被考核职务
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_Position" runat="server" DisplayRoot="True" IDColumnName="ID"
                                        TableName="MCS_SYS.dbo.Org_Position" NameColumnName="Name" ParentColumnName="SuperID"
                                        Width="240px" />
                                </td>
                                <td class="dataLabel">
                                    <asp:CheckBox ID="chb_ToPositionChild" runat="server" Text="含子职位" />
                                </td>
                                <td>
                                审核标志
                                </td>
                                <td>
                                <asp:DropDownList ID="ddl_ApproveFlag"  DataTextField="Value" DataValueField="Key" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="4">
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="KPI_Scheme_ID" PanelCode="Panel_KPI_SchemeList" AllowPaging="true"
                                        PageSize="15" onpageindexchanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="KPI_Scheme_ID" DataNavigateUrlFormatString="KPI_SchemeDetail.aspx?ID={0}"
                                                ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
