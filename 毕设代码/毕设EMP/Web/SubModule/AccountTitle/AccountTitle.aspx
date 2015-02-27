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
                                            ��ƿ�Ŀ�б�</h2>
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
                                                    ��ϸ��Ϣ</h2>
                                            </td>
                                            <td align="left">
                                                ID��:<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_Save" runat="server" Text="����" Width="60" OnClick="btn_Save_Click"
                                                    ValidationGroup="1" />
                                                <asp:Button ID="bt_AddSub" runat="server" Text="����¼���Ԫ" Width="80px" OnClick="bt_AddSub_Click"
                                                    Visible="False" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="ɾ��" Width="60" OnClick="btn_Delete_Click"
                                                    Visible="false" />
                                                <asp:Button ID="btn_Cancel" runat="server" Text="ȡ��" Width="60" OnClick="btn_Cancel_Click"
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
                                                ��Ŀ����
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Name" runat="server"></asp:TextBox>
                                                <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Name" ErrorMessage="����Ϊ��"
                                                    ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataLabel" width="60">
                                                ��Ŀ����
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Code" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel" width="60">
                                                ��Ŀ�ȼ�
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lbl_LevelName" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" height="28">
                                                �ϼ���Ŀ
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <mcs:MCSTreeControl ID="tree_SuperID" IDColumnName="ID" ParentColumnName="SuperID"
                                                    TableName="MCS_Pub.dbo.AC_AccountTitle" runat="server" NameColumnName="Name"
                                                    RootValue="0" Width="300px" />
                                            </td>
                                            <td class="dataLabel">
                                                �����ɳ�����<br />
                                                ���ñ���
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_OverPercent" runat="server" ToolTip="�������ÿ��Գ���������õİٷ�֮���٣���20����ɳ�120%��"
                                                    Width="50px">0</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_OverPercent"
                                                    Display="Dynamic" ErrorMessage="����" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_OverPercent"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" width="60" height="28">
                                                �Ƿ�ͣ��
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBox ID="cbx_IsDisable" runat="server" Text="��ͣ��" />
                                            </td>
                                            <td class="dataLabel" height="28">
                                                ����������
                                            </td>
                                            <td class="dataField" height="28">
                                                <asp:CheckBox ID="cbx_MustApplyFirst" runat="server" Text="����������ſɺ���" />
                                            </td>
                                            <td class="dataLabel" height="28">
                                                ��ͨ������
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBox ID="cbx_CanApplyInGeneralFlow" runat="server" Text="������ͨ������������" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" height="28">
                                                ��Ŀ����
                                            </td>
                                            <td class="dataField" colspan="5">
                                                <asp:TextBox ID="tbx_Description" runat="server" Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" height="28">
                                                �����������
                                            </td>
                                            <td class="dataField">
                                                <asp:CheckBoxList ID="cbl_FeeType" runat="server" DataTextField="Value" DataValueField="Key"
                                                    RepeatColumns="4" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </td>
                                            <td class="dataLabel">
                                                �¸�����������
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="txt_MonthsOverdue" runat="server" Width="50px">1</asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_MonthsOverdue"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                            </td>
                                            <td class="dataLabel">
                                                Ԥ������������
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="txt_YFMonthsOverdue" runat="server" Width="50px">1</asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_YFMonthsOverdue"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"
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
