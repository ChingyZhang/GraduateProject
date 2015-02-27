<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="SalesOrganizeTargetList.aspx.cs" Inherits="SubModule_SVM_SalesOrganizeTargetList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                ���´�����Ŀ���б�</h2>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" Text="����Excel" OnClick="bt_Export_Click"
                                Width="70px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="dataLabel"
                    height="50">
                    <tr>
                        <td class="dataLabel">
                            ����Ƭ��
                        </td>
                        <td>
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="180px" AutoPostBack="True" />
                        </td>
                        <td class="dataLabel">
                            �����
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            ������־
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="bt_Find" runat="server" Text="�� ��" Width="70px" OnClick="bt_Find_Click" />
                        </td>
                        <td>
                            <asp:Button ID="bt_Approve" runat="server" Text="�� ��" OnClick="bt_Approve_Click"
                                ToolTip="Ƭ�������" Width="60px" />
                        </td>
                        <td>
                            <asp:Button ID="bt_UnApprove" runat="server" Text="ȡ�����" ToolTip="Ƭ����ȡ�����" Width="60px"
                                OnClick="bt_UnApprove_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap>
                            <h3>
                                ��¼�б�</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_detail"
                    visible="true">
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                PanelCode="Panel_SVM_OrganizeTargetList" DataKeyNames="SVM_OrganizeTarget_ID"
                                PageSize="25" Width="100%">
                                <Columns>
                                    <asp:HyperLinkField Text="�鿴��ϸ" DataNavigateUrlFields="SVM_OrganizeTarget_ID" DataNavigateUrlFormatString="SalesOrganizeTargetDetail.aspx?TargetID={0}"
                                        ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                        <ItemStyle Width="80px" />
                                    </asp:HyperLinkField>
                                   
                                </Columns>
                                <EmptyDataTemplate>
                                    ������
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">���ݴ����У����Ժ�...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
