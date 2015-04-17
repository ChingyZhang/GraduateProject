<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="DistributorList.aspx.cs" Inherits="CM_Distributor_DistributorList" %>

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
                                 <asp:Label ID="lb_PageTitle" runat="server" Text="经销商列表"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="快捷查询" Description=""
                            Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="高级查询" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr runat="server" id="tr_basicsearch">
                        <td width="240px">
                            管理片区<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="180px" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    快捷查询
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">经销商编号</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName" Selected="True">经销商全称</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.ShortName">经销商简称</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.TeleNum">电话号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Address">客户地址</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_LinkMan.Name">首要联系人</asp:ListItem>
                                    </asp:DropDownList>
                                    相似于
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                    活跃标志 
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                    审核标志 
                                    <asp:DropDownList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="快捷查询" Width="60px" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_SearchType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="导出为Excel"
                                Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" PanelCode="Panel_DI_List_001"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                            DataKeyNames="CM_Client_ID" AllowSorting="true">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" Visible="false">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:CommandField>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="DistributorDetail.aspx?ClientID={0}"
                                    DataTextField="CM_Client_FullName" HeaderText="经销商全称" ControlStyle-CssClass="listViewTdLinkS1"
                                    SortExpression="CM_Client_FullName">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>                                
                            </Columns>
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
