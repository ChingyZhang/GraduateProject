<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Car_CarList, App_Web_dk3o7pe1" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 220px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="">公司车辆列表</asp:Label></h2>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="dataLabel" width="60px">
                                                管理片区
                                            </td>
                                            <td class="dataField" width="220px" align="left">
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                            </td>
                                            <td class="dataLabel" width="60px">
                                                车号
                                            </td>
                                            <td class="dataField" align="left">
                                                <asp:TextBox ID="tbx_CarNo" runat="server"></asp:TextBox>
                                            </td>
                                            <td width="70" align="right">
                                                <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="新增车辆" Width="80px" OnClick="bt_Add_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        公司车辆列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="Car_CarList_ID" PanelCode="Panel_OA_Car_CarList_01" AllowPaging="True"
                            PageSize="15">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="Car_CarList_ID" DataNavigateUrlFormatString="CarDetail.aspx?ID={0}"
                                    Text="车辆详细资料" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                 <asp:HyperLinkField DataNavigateUrlFields="Car_CarList_ID" DataNavigateUrlFormatString="Car_UsageList.aspx?CarID={0}"
                                    Text="行程及费用" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
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
