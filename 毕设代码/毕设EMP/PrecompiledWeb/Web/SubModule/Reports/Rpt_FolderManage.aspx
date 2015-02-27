<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Reports_Rpt_FolderManage, App_Web_cab7yjjs" enableEventValidation="false" stylesheettheme="basic" %>

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
                                                <h2>
                                                    报表目录</h2>
                                            </td>
                                            <td align="right">
                                                &nbsp;
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
                                                <h2>
                                                    详细信息</h2>
                                            </td>
                                            <td align="right">
                                                ID：<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddSub" runat="server" Text="添加下级单元" Width="80px" OnClick="bt_AddSub_Click" />
                                                <asp:Button ID="btn_Save" runat="server" Text="保存" Width="60" OnClick="btn_Save_Click"
                                                    Enabled="false" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="60" OnClick="btn_Delete_Click"
                                                    Visible="false" />
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
                                            <td class="dataLabel" width="100">
                                                目录名称
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Name" runat="server"></asp:TextBox>
                                                <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataLabel" width="100">
                                                上级目录
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <mcs:MCSTreeControl ID="tree_SuperID" IDColumnName="ID" ParentColumnName="SuperID"
                                                    TableName="MCS_Reports.dbo.Rpt_Folder" runat="server" NameColumnName="Name" RootValue="0"
                                                    Width="350px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" class="h3Row" height="28px">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                报表目录权限设置</h3>
                                                        </td>
                                                        <td style="color: Red">
                                                            注:【权限2】级别最高，其次为【权限1】、【权限3】
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="5" cellpadding="5" width="100%">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                权限1：浏览统计目录下所有报表</h3>
                                                        </td>
                                                        <td>
                                                            <h3>
                                                                权限2：修改设计目录下所有报表</h3>
                                                        </td>
                                                        <td>
                                                            <h3>
                                                                权限3：创建、浏览及修改自己的报表</h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbx_SelectAll1" runat="server" Text="全选" AutoPostBack="true" OnCheckedChanged="cbx_SelectAll1_CheckedChanged" />
                                                            <asp:CheckBoxList ID="cbx_RoleList1" runat="server">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="cbx_SelectAll2" runat="server" Text="全选" AutoPostBack="true" OnCheckedChanged="cbx_SelectAll2_CheckedChanged" />
                                                            <asp:CheckBoxList ID="cbx_RoleList2" runat="server">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="cbx_SelectAll3" runat="server" Text="全选" AutoPostBack="true" OnCheckedChanged="cbx_SelectAll3_CheckedChanged" />
                                                            <asp:CheckBoxList ID="cbx_RoleList3" runat="server">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
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
