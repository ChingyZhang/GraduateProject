<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="ProductCategory.aspx.cs" Inherits="SubModule_Product_ProductCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top" width="180px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td align="right" width="20">
                                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td align="left" width="150">
                                                <h2>商品类别维护</h2>
                                            </td>
                                            <td align="right">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="180px" valign="top">
                                    <asp:TreeView ID="tr_List" runat="server" Width="100%" ImageSet="Msdn" ExpandDepth="1"
                                        OnSelectedNodeChanged="tr_List_SelectedNodeChanged">
                                        <NodeStyle CssClass="listViewTdLinkS1" />
                                        <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                    </asp:TreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td width="24">
                                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td nowrap="noWrap" style="width: 180px">
                                                <h2>详细信息</h2>
                                            </td>
                                            <td align="right">ID：<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddSub" runat="server" Text="添加下级单元" Width="80px" OnClick="bt_AddSub_Click" />
                                                <asp:Button ID="btn_Save" runat="server" Text="保存" Width="60" OnClick="btn_Save_Click"
                                                    Enabled="false" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="60" OnClick="btn_Delete_Click" Enabled="false" />
                                                <asp:Button ID="btn_Cancel" runat="server" Text="取消" Width="60" OnClick="btn_Cancel_Click"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="dataLabel" width="100">目录名称
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Name" runat="server"></asp:TextBox>
                                                <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataLabel" width="100">上级目录
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tree_SuperID" IDColumnName="ID" ParentColumnName="SuperID"
                                                    runat="server" NameColumnName="Name" RootValue="0"
                                                    Width="200px" />
                                            </td>
                                            <td class="dataLabel" width="100">是否启用
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_EnabledFlag" runat="server">
                                                    <asp:ListItem Selected="True" Value="Y">启用</asp:ListItem>
                                                    <asp:ListItem Value="N">停用</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
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

