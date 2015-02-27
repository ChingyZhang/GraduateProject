<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="TC_TrackCardListByStaff.aspx.cs" Inherits="SubModule_OA_TrackCard_TC_TrackCardListByStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="销售人员日跟踪表"></asp:Label>
                            </h2>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="dataLabel" width="60px">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" width="60px">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel" width="60px">
                                    销售人员
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="150px" />
                                </td>
                                 <td class="dataLabel" width="60px">
                                    是否万元柜台
                                </td>
                                <td class="dataField">
                                   <asp:DropDownList ID="ddl_ISWanYuan" DataTextField="Value" DataValueField="Key" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Load" Width="80px" runat="server" Text="查 看" OnClick="bt_Load_Click" />
                                    <asp:Button ID="bt_TrackEdit" Width="80px" runat="server" Text="填报日跟踪" OnClick="bt_TrackEdit_Click" />
                                     <asp:Button ID="bt_Export" Width="80px" runat="server" Text="导出报表" 
                                        onclick="bt_Export_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                    <tr>
                        <td height="28px">
                            <h3>
                                日跟踪表列表
                            </h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="True"
                                GridLines="Both" CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
                                BorderWidth="0px" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging">
                                <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                <RowStyle BackColor="White" HorizontalAlign="Center" Height="28px" />
                                <Columns>
                                </Columns>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Load" EventName="Click" />
                            <asp:PostBackTrigger ControlID="bt_Export"  />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
