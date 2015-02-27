<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="DetailViewList.aspx.cs" Inherits="SubModule_UDM_DetailViewList" %>

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
                                        详细显示视图列表
                                    </h2>
                                </td>
                                <td>
                                    代码:<asp:TextBox ID="tbx_Code" runat="server" Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Code"
                                        Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    视图名称:<asp:TextBox ID="tbx_Name" runat="server" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_Name"
                                        Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:Button ID="bt_SaveDetailView" runat="server" Text="新增" Width="60px" OnClick="bt_SaveDetailView_Click"
                                        ValidationGroup="1" />
                                </td>
                                <td align="right">
                                    关键字:<asp:TextBox ID="tbx_Find" runat="server"></asp:TextBox>
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID" AllowPaging="True" PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ControlStyle-CssClass="listViewTdLinkS1"
                                    SelectText="选择" />
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="Code" HeaderText="详细视图代码" />
                                <asp:BoundField DataField="Name" HeaderText="详细视图名称" />
                                <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PanelList.aspx?DetailViewID={0}"
                                    Text="自定义板块"  ControlStyle-CssClass="listViewTdLinkS1" />
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
