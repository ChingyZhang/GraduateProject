<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="AccountTitle.aspx.cs" Inherits="SubModule_AccountTitle_AccountTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                            会计科目列表</h2>
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
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TreeView ID="tr_List" runat="server" Width="100%" ImageSet="Msdn" Target="if_ListViewFrame"
                                        ExpandDepth="1" OnSelectedNodeChanged="tr_List_SelectedNodeChanged">
                                        <NodeStyle CssClass="listViewTdLinkS1" />
                                        <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                            <td align="left">
                                                ID号:<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_Save" runat="server" Text="保存" Width="60" OnClick="btn_Save_Click"
                                                    ValidationGroup="1" />
                                                <asp:Button ID="bt_AddSub" runat="server" Text="添加下级单元" Width="80px" OnClick="bt_AddSub_Click"
                                                    Visible="False" />
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
                                            <td class="dataLabel" width="60" height="28">
                                                科目名称
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Name" runat="server"></asp:TextBox>
                                                <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空"
                                                    ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataLabel" width="60">
                                                科目代码
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Code" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel" width="60">
                                                科目等级
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lbl_LevelName" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" height="28">
                                                上级科目
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <mcs:MCSTreeControl ID="tree_SuperID" IDColumnName="ID" ParentColumnName="SuperID"
                                                    TableName="MCS_Pub.dbo.AC_AccountTitle" runat="server" NameColumnName="Name"
                                                    RootValue="0" Width="300px" />
                                            </td>
                                            <td class="dataLabel">
                                                报销可超申请<br />
                                                费用比例
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_OverPercent" runat="server" ToolTip="报销费用可以超过申请费用的百分之多少（如20代表可超120%）"
                                                    Width="50px">0</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_OverPercent"
                                                    Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_OverPercent"
                                                    Display="Dynamic" ErrorMessage="必需为整形" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" width="60" height="28">
                                                是否停用
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBox ID="cbx_IsDisable" runat="server" Text="已停用" />
                                            </td>
                                            <td class="dataLabel" height="28">
                                                必须先申请
                                            </td>
                                            <td class="dataField" height="28">
                                                <asp:CheckBox ID="cbx_MustApplyFirst" runat="server" Text="必须先申请才可核销" />
                                            </td>
                                            <td class="dataLabel" height="28">
                                                在通用流程
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBox ID="cbx_CanApplyInGeneralFlow" runat="server" Text="允许在通用流程中申请" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" height="28">
                                                科目描述
                                            </td>
                                            <td class="dataField" colspan="5">
                                                <asp:TextBox ID="tbx_Description" runat="server" Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" height="28">
                                                归属费用类别
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBoxList ID="cbl_FeeType" runat="server" DataTextField="Value" DataValueField="Key"
                                                    RepeatColumns="4" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </td>
                                            <td class="dataLabel">
                                                月付可逾期月数
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="txt_MonthsOverdue" runat="server" Width="50px">1</asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_MonthsOverdue"
                                                    Display="Dynamic" ErrorMessage="必需为整形" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                            </td>
                                            <td class="dataLabel">
                                                预付可逾期月数
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="txt_YFMonthsOverdue" runat="server" Width="50px">1</asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_YFMonthsOverdue"
                                                    Display="Dynamic" ErrorMessage="必需为整形" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tr_List" EventName="SelectedNodeChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
