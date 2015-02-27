<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Store_StoreList, App_Web_yl0tklmc" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                公司仓库列表<asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="30px" class="tabForm">
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr runat="server" id="tr_basicsearch">
                        <td width="260px">
                            管理片区<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    快捷查询
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">仓库编号</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName">仓库全称</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.ShortName">仓库简称</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.TeleNum">电话号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Address">客户地址</asp:ListItem>
                                    </asp:DropDownList>
                                    等于或相似于
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_SearchType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Button ID="bt_Find" runat="server" Text="快捷查询" Width="60px" OnClick="bt_Find_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="导出为Excel"
                                Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="1px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="CM_Client_ID" PageSize="15" Width="100%"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" PanelCode="Panel_CM_StoreList001">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1">
                                </asp:CommandField>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="StoreDetail.aspx?ClientID={0}"
                                    DataTextField="CM_Client_FullName" HeaderText="客户全称" ControlStyle-CssClass="listViewTdLinkS1"
                                     />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/JXCSummary_Detail2.aspx?ClientID={0}"
                                    Text="进销存" ControlStyle-CssClass="listViewTdLinkS1"  Visible="false" />
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
