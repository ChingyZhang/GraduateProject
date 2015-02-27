<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_RT_RetailerCloseAndOpen, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        �ŵ�״̬��������
                                    </h2>
                                </td>
                                <td>
                                    <div style="float: right">
                                        <asp:Button ID="bt_OK" runat="server" Text="ȷ  ��" OnClientClick='confirm("��ȷ���ŵ�ID��ȷ���رյ��ŵ꽫�������뷵�����ã�")'
                                            Width="62px" OnClick="bt_OK_Click" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div width="100%">
                <h3>
                    ʧЧ�ŵ�ID:</h3>
                <span>����˵��:����EXCEL��ʧЧ�ŵ�ID���ŵ�ʧЧ֮�����ɷ�������Ϊ0</span>
                <div style="display: inline; margin-right: 40">
                    <asp:TextBox ID="txt_LoseID" runat="server" Height="274px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_LoseErrorInfo" runat="server"></asp:Literal>
                <h3>
                    �ر��ŵ�ID:</h3>
                <span>����˵��:����EXCEL�йر��ŵ�ID���ŵ�ر�֮�����ɷ�������Ϊ0</span>
                <div>
                    <asp:TextBox ID="txt_CloseID" runat="server" Height="274px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_CloseErrorInfo" runat="server"></asp:Literal>
                <h3>
                    ��ͨ�ŵ�ID:</h3>
                <span>����˵��:����EXCEL�п����ŵ�ID</span>
                <div style="display: inline; margin-right: 40">
                    <asp:TextBox ID="txt_OpenID" runat="server" Height="274px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </div>
                <asp:Literal ID="lb_OpenErrorInfo" runat="server"></asp:Literal>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="bt_OK" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
