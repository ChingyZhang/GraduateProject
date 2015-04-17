<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="WebPageList.aspx.cs" Inherits="SubModule_UDM_WebPageList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" align="left">
                                    <h2>
                                        系统内WEB页面列表
                                    </h2>
                                </td>
                                <td align="right">
                                    页面标题或路径:<asp:TextBox ID="tbx_Find" runat="server" Width="150px"></asp:TextBox>
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                    <asp:Button ID="bt_Add" runat="server" Text="新增" Width="60px" 
                                        onclick="bt_Add_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID" AllowPaging="true" PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="页面标题" />
                                <asp:BoundField DataField="Path" HeaderText="页面路径" />
                                <asp:BoundField DataField="SubCode" HeaderText="子代码" />
                                <asp:BoundField DataField="Module" HeaderText="所属模块" />
                                <asp:HyperLinkField Text="详细信息" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="WebPageDetail.aspx?ID={0}"
                                    Target="_blank" ControlStyle-CssClass=listViewTdLinkS1 />
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
