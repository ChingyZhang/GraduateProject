<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RetailerList.aspx.cs" Inherits="SubModule_RM_RetailerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="float: left;">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2><asp:Label ID="Label1" runat="server" Text="零售商列表"></asp:Label><asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label></h2>
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
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="快捷查询" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="高级查询" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td height="30px">
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr runat="server" id="tr_basicsearch">
                        <td width="260px">管理片区<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                            ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    快捷查询
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_CM.dbo.CM_ClientManufactInfo.Code" Selected="True">门店编号</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName">门店全称</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.ShortName">门店简称</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.TeleNum">电话号码</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Address">客户地址</asp:ListItem>
                                    </asp:DropDownList>
                                    相似于
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                    活跃标志
                                    <asp:DropDownList ID="ddl_State" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                    <%--
                                    会员店类别
                                    <asp:DropDownList ID="ddl_RMSClassify" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                    是否返利店
                                    <asp:DropDownList ID="ddl_IsRebate" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>--%>
                                    状态
                                    <asp:DropDownList ID="ddl_SyncState" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                    <asp:Button ID="bt_Find" runat="server" Text="快捷查询" Width="60px" OnClick="bt_Find_Click" />
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
            <td height="1px"></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="CM_Client_ID" PageSize="15" Width="100%"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                            PanelCode="Page_RT_RetailerList">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" Visible="false"></asp:CommandField>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="RetailerDetail.aspx?ClientID={0}"
                                    DataTextField="CM_Client_FullName" HeaderText="门店全称" ControlStyle-CssClass="listViewTdLinkS1"
                                    SortExpression="CM_Client_FullName" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# Bind("CM_Client_ID","../ClientPictureList.aspx?ClientID={0}")%>'
                                            Text="图片管理" CssClass="listViewTdLinkS1" Target="_blank" ImageUrl='<%# MCSFramework.BLL.Pub.ATMT_AttachmentBLL.GetFirstPreviewPicture(30,(int)DataBinder.Eval(Container,"DataItem.CM_Client_ID"))%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <a style="cursor: pointer; color: #990033" onclick='<%#Eval("CM_Client_ID","PopShow({0})") %>'>地图</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
