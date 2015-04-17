<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrganizeCityManage.aspx.cs" Inherits="SubModule_AddressManage_OrganizeCityManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function PopAddOfficialCity(id) {
            var tempid = Math.random() * 10000;
            window.showModalDialog('OrganizeCity_AddOfficialCity.aspx?OrganizeCity=' + id + '&tempid=' + tempid, window, 'dialogWidth:450px;DialogHeight=550px;status:no');
        }
    </script>

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
                                                    区域架构列表</h2>
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
                                    <asp:TreeView ID="tr_List" runat="server" Width="100%" ImageSet="Msdn" Target="if_ListViewFrame"
                                        ExpandDepth="1" OnSelectedNodeChanged="tr_List_SelectedNodeChanged">
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
                                            <td>
                                                ID:<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                <asp:Button ID="bt_SyncManager" runat="server" CausesValidation="False" OnClick="bt_SyncManager_Click"
                                                    OnClientClick="return confirm(&quot;是否确认同步管理片区经理?&quot;)" Text="同步管理片区经理" Width="120px" />
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddSub" runat="server" Text="添加下级片区" Width="80px" OnClick="bt_AddSub_Click"
                                                    Visible="False" />
                                                <asp:Button ID="btn_Save" runat="server" Text="保 存" Width="60" OnClick="btn_Save_Click" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="60" OnClick="btn_Delete_Click"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel">
                                                            名称
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_Name" runat="server"></asp:TextBox>
                                                            <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="dataLabel">
                                                            上级管理单元
                                                        </td>
                                                        <td class="dataField" colspan="3">
                                                            <mcs:MCSTreeControl ID="tree_SuperID" runat="server" IDColumnName="ID" ParentColumnName="SuperID"
                                                                NameColumnName="Name" TableName="MCS_SYS.dbo.Addr_OrganizeCity" RootValue="0"
                                                                Width="350px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel" style="height: 25px">
                                                            等级
                                                        </td>
                                                        <td class="dataField" style="height: 25px">
                                                            <asp:Label ID="lbl_LevelName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td class="dataLabel" style="height: 25px">
                                                            代码
                                                        </td>
                                                        <td class="dataField" style="height: 25px">
                                                            <asp:TextBox ID="tbx_Code" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="dataLabel" style="height: 25px">
                                                            经理
                                                        </td>
                                                        <td class="dataField" style="height: 25px">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <mcs:MCSSelectControl ID="select_Manager" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                                                        Width="180px"></mcs:MCSSelectControl>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel">
                                                            管辖范围</td>
                                                        <td class="dataField" colspan="5">
                                                            <asp:TextBox ID="tbx_ManageRegion" runat="server" Width="600px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tb_OfficialCityInOrganizeCity"
                                        visible="false">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td nowrap>
                                                            <h3>
                                                                管理片区对应的行政城市(行政区县)
                                                            </h3>
                                                        </td>
                                                        <td align="right">
                                                            <asp:CheckBox ID="cb_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_CheckAll_CheckedChanged"
                                                                Text="全选" />
                                                            <asp:Button ID="bt_AddOfficialCity" runat="server" Text="新 增" Width="60px" OnClick="bt_AddOfficialCity_Click" />
                                                            <asp:Button ID="bt_DeleteOfficialCity" runat="server" Text="删 除" Width="60px" OnClick="bt_DeleteOfficialCity_Click"
                                                                OnClientClick="return confirm(&quot;是否确认删除所选中的行政区县?&quot;)" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="cbl_OfficialList" runat="server">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
