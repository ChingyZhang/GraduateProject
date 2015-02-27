<%@ page language="C#" autoeventwireup="true" inherits="SubModule_OA_Journal_JournalDetail, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>������־��ϸ��Ϣ</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table cellspacing="0" cellpadding="2" width="98%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="Table1">
                                <tr>
                                    <td align="left" width="160" height="28px">
                                        <h2>
                                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                            ������־��ϸ��Ϣ</h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_OK" runat="server" Width="80px" Text="����" OnClick="bt_OK_Click"
                                            ToolTip="���浱ǰ����־" />
                                        <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm(&quot;�Ƿ�ȷ��ɾ���ù�����־?&quot;)"
                                            Text="ɾ����־" Width="80px" ToolTip="ɾ����ǰ����־" />
                                        <asp:Button ID="bt_ToEvectionRoute" runat="server" Text="ת�����г�" OnClick="bt_ToEvectionRoute_Click"
                                            Width="80px" />
                                        <asp:Button ID="bt_AddNewClient" runat="server" Text="�����˿�����" />
                                        <%-- <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="���" Width="60px" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                                <tr>
                                    <td class="dataLabel">
                                        <asp:Label ID="lbl_Staff" runat="server">���</asp:Label>
                                    </td>
                                    <td class="dataField">
                                        <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                            Width="150px" OnSelectChange="select_Staff_SelectChange" />
                                    </td>
                                    <td class="dataLabel">
                                        ְ��
                                    </td>
                                    <td class="dataLabel">
                                        <asp:Label ID="lbl_Position" runat="server"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        ��־����
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_JournalType" runat="server" DataTextField="Value" DataValueField="Key"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_JournalType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dataLabel">
                                        ��ʼ����ʱ��
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                            Display="Dynamic" ErrorMessage="��ʽ����" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddl_BeginHour" runat="server">
                                        </asp:DropDownList>
                                        :
                                        <asp:DropDownList ID="ddl_BeginMinute" runat="server">
                                            <asp:ListItem Selected="True">00</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        ��ֹʱ��
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"
                                            Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Enabled="false" runat="server"
                                            ControlToValidate="tbx_enddate" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" Enabled="false" ControlToValidate="tbx_enddate"
                                            Display="Dynamic" ErrorMessage="��ʽ����" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddl_EndHour" runat="server">
                                        </asp:DropDownList>
                                        :
                                        <asp:DropDownList ID="ddl_EndMinute" runat="server">
                                            <asp:ListItem Selected="True">00</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="dataLabel">
                                        �������
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_WorkingClassify" runat="server" DataTextField="Value" DataValueField="Key"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_WorkingClassify_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_OA_JournalDetail">
                                                </mcs:UC_DetailView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table cellpadding="0" cellspacing="2" border="0" width="98%" runat="server">
            <tr>
                <td>
                    <uc1:UploadFile ID="UploadFile1" runat="server" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="2" border="0" width="98%" runat="server" id="tbl_comment">
                    <tr>
                        <td align="right" height="28px" class="dataField">
                            ����<asp:Label ID="lb_CommentCounts" runat="server" ForeColor="Red" Text="Label"></asp:Label>������<asp:Button
                                ID="btn_LookComment" runat="server" OnClick="btn_LookComment_Click" Text="�鿴����������"
                                Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="table_comment"
                                visible="false">
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="dgshow" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AllowPaging="True" OnPageIndexChanging="dgshow_PageIndexChanging" PageSize="8">
                                            <Columns>
                                                <asp:TemplateField HeaderText="��־����">
                                                    <ItemTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 196px">
                                                                    �ظ��ˣ�<asp:Literal ID="replayer" runat="server" Text='<%# new MCSFramework.BLL.Org_StaffBLL(int.Parse(DataBinder.Eval(Container.DataItem,"Staff").ToString())).Model.RealName %>'> </asp:Literal>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 196px">
                                                                    �ظ�ʱ�䣺<asp:Literal ID="replaytime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CommentTime")%>'> </asp:Literal>
                                                                </td>
                                                                <td>
                                                                    <%# FormatTxt(MCSFramework.SQLDAL.OA.BBS_ForumItemDAL.txtMessage(DataBinder.Eval(Container.DataItem, "Content").ToString())) %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <h2>
                                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16"> </img>
                                                        ��������</h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="content" runat="server" TextMode="MultiLine" Width="600px" Height="100px"></asp:TextBox>
                                                    <br />
                                                    <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="��������" />
                                                    &nbsp;
                                                    <asp:Button ID="btn_cancel" runat="server" Text="ȡ������" OnClick="btn_cancel_Click" />
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
    </div>
    </form>
</body>
</html>
