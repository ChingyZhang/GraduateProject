<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_Budget_BudgetSourceAssignInfo, App_Web_gigc93-l" enableEventValidation="false" stylesheettheme="basic" %>

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
                                        预算费用分配汇总表
                                    </h2>
                                </td>
                                <td style="width: 60px" class="dataLabel">
                                    管理片区
                                </td>
                                <td align="left" style="width: 200px">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" 
                                        onselected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    层级
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_CityLevel" runat="server" DataTextField="value" DataValueField="key"
                                        Enabled="true" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="2">省区</asp:ListItem>
                                        <asp:ListItem Value="4">城市</asp:ListItem>
                                        <asp:ListItem Value="5">县城</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" DataValueField="ID" DataTextField="Name"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataField" align="right">
                                    <asp:Button ID="btnFind" runat="server" Text="查看" Width="60px" OnClick="btnFind_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="true"
                            AllowPaging="True" PageSize="15" ondatabound="gv_List_DataBound" 
                            onpageindexchanging="gv_List_PageIndexChanging">
                            <Columns>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
