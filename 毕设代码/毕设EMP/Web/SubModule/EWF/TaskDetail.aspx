<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="TaskDetail.aspx.cs" Inherits="SubModule_EWF_TaskDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <%--ҳ�����--%>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                ����������ϸ��Ϣ</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Start" runat="server" Text="ȷ������" Width="80px" OnClick="btn_OK_Click"
                                Visible="false" />
                            <asp:Button ID="bt_Restart" runat="server" Text="����ִ��" Visible="False" Width="80px"
                                OnClick="bt_Restart_Click" CausesValidation="false" />
                            <asp:Button ID="bt_Cancel" runat="server" OnClick="bt_Cancel_Click" Text="ȡ������" Width="80px"
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            ������Ϣ</h2>
                                    </td>
                                    <td height="18" colspan="11" align="right">
                                        <asp:HyperLink ID="hyl_RelateURL" runat="server" CssClass="listViewTdLinkS1" >�鿴��ϸ������Ϣ</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                <tr>
                                    <td align="left" height="18" class="dataLabel">
                                        ������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Applyer" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="left" height="18" class="dataLabel">
                                        ������ְλ
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Applyer_Position" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ��������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_AppName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ����״̬
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_Status" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="18" class="dataLabel">
                                        ���̱��
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_ID" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="left" height="18" class="dataLabel">
                                        ����
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Title" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ��������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_StartTime" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ��ֹ����
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_EndTime" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" id="tr_CurrentProcessInfo">
                                    <td valign="middle" align="left" height="18" class="dataLabel">
                                        ��ǰ����
                                    </td>
                                    <td height="18" class="dataField" colspan="7">
                                        <asp:Label ID="lb_CurrentJobInfo" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" height="18" class="dataLabel">
                                        �������
                                    </td>
                                    <td height="18" class="dataField" colspan="7">
                                        <font color="blue">
                                            <asp:Literal ID="lt_Remark" runat="server"></asp:Literal></font>
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
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_EWFPanel ID="pl_dataobjectinfo" runat="server">
                </mcs:UC_EWFPanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr runat="server" id="tr_RelateUrl">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            ������ϸ��Ϣ</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <iframe width="100%" height="400px" scrolling="auto" frameborder="no" runat="server"
                                id="frame_relateurl"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            ��ظ�����Ϣ</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="tr_UploadAtt" visible="false" runat="server">
                        <td height="28px">
                            ������<asp:FileUpload ID="FileUpload1" runat="server" />
                            ���ƣ�<asp:TextBox ID="tbx_AttachmentName" runat="server" Width="150px"></asp:TextBox>
                            ������<asp:TextBox ID="tbx_AttachmentDescription" runat="server" Width="350px"></asp:TextBox>
                            <asp:Button ID="btn_Up" runat="server" Text="�ϴ�" Width="60px" OnClick="btn_Up_Click"
                                ValidationGroup="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List_Attachment" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID,FilePath" Width="100%" OnSelectedIndexChanging="gv_List_Attachment_SelectedIndexChanging"
                                Binded="False" ConditionString="" PanelCode="" OnRowDeleting="gv_List_Attachment_RowDeleting">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="DownloadAttachment.aspx?GUID={0}"
                                        Text="����"  ControlStyle-CssClass="listViewTdLinkS1" />
                                    <asp:BoundField DataField="Name" HeaderText="����" />
                                    <asp:BoundField DataField="FileType" HeaderText="��������" />
                                    <asp:BoundField DataField="Description" HeaderText="����" />
                                    <asp:BoundField DataField="FileSize" HeaderText="������С(KB)" />
                                    <asp:BoundField DataField="UploadStaff" HeaderText="�ϴ���" />
                                    <asp:BoundField DataField="UploadTime" HeaderText="�ϴ�����" />
                                    <asp:ButtonField CommandName="Delete" Text="ɾ��" ControlStyle-CssClass="listViewTdLinkS1"
                                        ItemStyle-Width="30px" />
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
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                        <tr>
                                            <td height="28px">
                                                <h2>
                                                    ���̻����б�</h2>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List_JobList" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="ID" Width="100%" OnSelectedIndexChanging="gv_List_JobList_SelectedIndexChanging">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="�鿴��ϸ">
                                                <ItemStyle Width="100px" />
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="CurrentProcess" HeaderText="��������" />
                                            <asp:BoundField DataField="Status" HeaderText="��ǰ״̬" />
                                            <asp:BoundField DataField="StartTime" HeaderText="��ʼʱ��" />
                                            <asp:BoundField DataField="EndTime" HeaderText="����ʱ��" />
                                            <asp:BoundField DataField="ErrorMessage" HeaderText="������Ϣ" />
                                            <asp:BoundField DataField="Remark" HeaderText="��ע" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_RecipientProcess" visible="false">
                                <%--��Ա����������ϸ��Ϣ--%>
                                <td>
                                    <table id="Table1" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_JobDecision" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0">
                                                    <Columns>
                                                        <asp:BoundField DataField="RecipientStaff" HeaderText="�ռ���" />
                                                        <asp:BoundField DataField="DecisionStaff" HeaderText="������" />
                                                        <asp:BoundField DataField="DecisionResult" HeaderText="�������" />
                                                        <asp:BoundField DataField="DecisionTime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                        <asp:TemplateField HeaderText="�������">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="Label1" runat="server" Text='<%# Bind("DecisionComment") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReadFlag" HeaderText="��ȡ��־" />
                                                        <asp:BoundField DataField="ReadTime" HeaderText="��ȡʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
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
                            <tr runat="server" id="tr_CCProcess" visible="false">
                                <%--���ͻ�����ϸ��Ϣ--%>
                                <td>
                                    <table id="Table3" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_JobCC" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0">
                                                    <Columns>
                                                        <asp:BoundField DataField="RecipientStaff" HeaderText="�ռ���" />
                                                        <asp:TemplateField HeaderText="��ע���">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="Label1" runat="server" Text='<%# Bind("Comment") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReadFlag" HeaderText="��ȡ��־" />
                                                        <asp:BoundField DataField="ReadTime" HeaderText="��ȡʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
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
                            <tr runat="server" id="tr_ConditionProcess" visible="false">
                                <%--�����жϻ�����ϸ��Ϣ--%>
                                <td>
                                    <table id="Table5" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="tabForm">
                                                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                                    <tr>
                                                        <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                            ���ݶ�����
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_DataObjectName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" width="100" class="dataLabel">
                                                            ���ݶ���ֵ
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_DataObjectValue" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" class="dataLabel" width="100">
                                                            �Ƚ�����
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            &nbsp;<asp:Label ID="lbl_OperatorTypeName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                            ��ʾ����
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_DataObjectDisPlayName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" width="100" class="dataLabel">
                                                            �Ƚ�ֵ1
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_Value1" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" class="dataLabel" width="100">
                                                            �Ƚ�ֵ2
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_Value2" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_DataBaseProcess" visible="false">
                                <%--ִ�д洢���̻�����ϸ��Ϣ--%>
                                <td>
                                    <table id="Table6" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="tabForm">
                                                <table id="Table7" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                    <tr>
                                                        <td align="left" colspan="4" height="22" valign="middle">
                                                            <h4>
                                                                ִ�����ݿ���Ϣ</h4>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel" style="width: 120px; height: 30px;">
                                                            ���ݿ�����
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:Label ID="lbl_DSN" Width="120px" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="dataLabel" style="width: 120px; height: 30px;">
                                                            �洢��������
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:Label ID="lbl_StoreProcName" Width="120px" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table id="Table8" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                    <tr>
                                                        <td class="dataLabel" align="left">
                                                            <h4>
                                                                �洢���̲���</h4>
                                                        </td>
                                                        <td class="dataField">
                                                            &nbsp;
                                                        </td>
                                                        <td style="height: 30px;" align="right">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel" style="height: 30px;" colspan="3">
                                                            <mcs:UC_GridView ID="gv_List_ParamList" runat="server" AutoGenerateColumns="False"
                                                                Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ParamName" HeaderText="��������" />
                                                                    <asp:BoundField DataField="IsOutput" HeaderText="�Ƿ��������" />
                                                                    <asp:BoundField DataField="IsDataObject" HeaderText="�Ƿ����ݶ���" />
                                                                    <asp:BoundField DataField="DataObject" HeaderText="���ݶ�������" />
                                                                    <asp:TemplateField HeaderText="���ݶ���ֵ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# GetDataObjectValue((Guid)DataBinder.Eval(Container,"DataItem.DataObject")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ConstStrValue" HeaderText="�����ַ���" />
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
                                </td>
                            </tr>
                            <tr runat="server" id="tr_SendMailProcess" visible="false">
                                <%--�����ʼ�������ϸ��Ϣ--%>
                                <td>
                                    <table id="Table4" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="tabForm">
                                                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                                    <tr>
                                                        <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                            �ռ��˽�ɫ
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_ReciverRoleName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" width="100" class="dataLabel">
                                                            �ʼ�����
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_MailSubject" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" class="dataLabel" style="width: 125px" width="100">
                                                            �ʼ�����
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_MailContent" runat="server" Text=""></asp:Label>
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
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
