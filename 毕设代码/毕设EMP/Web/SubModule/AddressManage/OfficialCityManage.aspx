<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OfficialCityManage.aspx.cs" Inherits="SubModule_AddressManage_OfficialCityManage" %>

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
                                                    官方城市列表</h2>
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
                                            <td align="right">
                                                ID：<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddSub" runat="server" Text="添加下级单元" Width="80px" OnClick="bt_AddSub_Click"
                                                    Visible="False" />
                                                <asp:Button ID="btn_Save" runat="server" Text="保存" Width="60" OnClick="btn_Save_Click" />
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
                                            <td class="dataLabel" width="100">
                                                上级行政单元
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <mcs:MCSTreeControl ID="tree_SuperID" IDColumnName="ID" ParentColumnName="SuperID"
                                                    TableName="MCS_SYS.dbo.Addr_OfficialCity" runat="server" NameColumnName="Name"
                                                    RootValue="0" Width="350px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                代码
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Code" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel">
                                                电话区号
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_CallAreaCode" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel">
                                                邮政编码
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_PostCode" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                等级
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lbl_LevelName" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="dataLabel">
                                                出生数
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Births" runat="server"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="必须为整数！"
                                                    Display="Dynamic" ControlToValidate="tbx_Births" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                            </td>
                                            <td class="dataLabel">
                                                属性
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_CityAttributeFlag" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="OfficialPopulation" width="100%">
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btn_SavePopulation" runat="server" Text="保存" Width="60px" OnClick="btn_SavePopulation_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <mcs:UC_DetailView ID="pl_Details" runat="server" DetailViewCode="OfficialCityPopulation_PanelDetail">
                                            </mcs:UC_DetailView>
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
