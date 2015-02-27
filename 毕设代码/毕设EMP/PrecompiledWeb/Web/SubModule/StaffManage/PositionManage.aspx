<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" inherits="SubModule_StaffManage_PositionManage, App_Web_it73ecr1" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top" width="300px">
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
                                                    职务列表</h2>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" valign="top">
                                    <asp:TreeView ID="trPosition" runat="server" Width="100%" ImageSet="Msdn" Target="if_ListViewFrame"
                                        ExpandDepth="1" OnSelectedNodeChanged="trPosition_SelectedNodeChanged">
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
                                                    职务信息</h2>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_AddSubPosition" runat="server" OnClick="btn_AddSubPosition_Click"
                                                    Text="添加下级职位" Visible="False" Width="80px" />
                                                <asp:Button ID="bt_Save" runat="server" OnClick="btn_Save_Click" Text="保存" Width="60px" />
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
                                                名称
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Name" runat="server"></asp:TextBox>
                                                <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataLabel">
                                                是否总部职务
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBox ID="cbx_IsHeadOffice" runat="server" />
                                            </td>
                                            <td class="dataLabel">
                                                是否启用
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBox ID="cbx_Enable" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" width="100">
                                                上级职务
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <mcs:MCSTreeControl ID="tr_SuperPosition" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" RootValue="0" Width="450px" />
                                            </td>
                                            <td class="dataLabel">
                                                部门
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Department" DataTextField="Value" DataValueField="Key" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                备注
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <asp:TextBox ID="tbx_Remark" runat="server" Width="450px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel" width="100">
                                                ID
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
