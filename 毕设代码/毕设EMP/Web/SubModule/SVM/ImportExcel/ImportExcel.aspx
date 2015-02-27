<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ImportExcel.aspx.cs" Inherits="SubModule_SVM_ImportExcel_ImportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="��Excel���������̽�������������"></asp:Label></h2>
                        </td>
                        <td>
                            <font color="red">��������·�:<asp:Label ID="lb_MonthTitle" runat="server" Text=""></asp:Label></font>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr height="28px">
                                                <td nowrap>
                                                    <h3>
                                                        ��һ���������ŵ�����ģ��</h3>
                                                </td>
                                                <td>
                                                    <h3>
                                                        �ر����ѣ��ڲ������²���ʱ�������Ķ���ҳ���·�˵����������ֲ������󣬵��µ��벻�ɹ���</h3>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabForm">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td class="dataLabel" width="80">
                                                    ����ҵ��
                                                </td>
                                                <td class="dataField" width="500">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                                                Width="260px" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="bt_DownloadTemplate" runat="server" Text="1.��������ģ��" Width="100px"
                                                        OnClick="bt_DownloadTemplate_Click" />
                                                </td>
                                                <td>
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
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr height="28px">
                                                <td nowrap>
                                                    <h3>
                                                        �ڶ������ϴ��ŵ�����Excel���</h3>
                                                </td>
                                                <td nowrap>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabForm">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td class="dataLabel" width="80">
                                                    Excel�ļ�
                                                </td>
                                                <td class="dataField" width="380px">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                                                </td>
                                                <td align="left" width="120">
                                                    <asp:Button ID="bt_UploadExcel" runat="server" Text="2.�ϴ�Excel" Width="100px" OnClick="bt_UploadExcel_Click" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="bt_Import" runat="server" Text="3.ȷ���ύ" Width="100px" OnClick="bt_Import_Click"
                                                        OnClientClick="return confirm(&quot;��ȷ���ϴ���Excel�ļ�����׼ȷ��ȷ���ύ���룿&quot;)" />
                                                </td>
                                                <td>
                                                    <span style="color: #FF0000">��������ȷ���ϴ�Excel�ļ�����׼ȷ���ٵ��"ȷ���ύ"��ť��</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="lb_ErrorInfo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="false">
                                                <ContentTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                                                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                                                    <Items>
                                                                        <mcs:MCSTabItem Text="�����̽���" Value="1" />
                                                                        <mcs:MCSTabItem Text="����������" Value="2" />
                                                                    </Items>
                                                                </mcs:MCSTabControl>
                                                            </td>
                                                        </tr>
                                                        <tr class="tabForm">
                                                            <td>
                                                                <mcs:UC_GridView ID="gv_SellIn" runat="server" Width="100%" AutoGenerateColumns="true"
                                                                    OnDataBound="gv_Sell_DataBound" DataKeyNames="���" OnRowDeleting="gv_SellIn_RowDeleting">
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" DeleteText="ɾ��" />
                                                                    </Columns>
                                                                </mcs:UC_GridView>
                                                                <mcs:UC_GridView ID="gv_SellOut" runat="server" Width="100%" AutoGenerateColumns="true"
                                                                    OnDataBound="gv_Sell_DataBound" DataKeyNames="���" OnRowDeleting="gv_SellOut_RowDeleting">
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" DeleteText="ɾ��" />
                                                                    </Columns>
                                                                </mcs:UC_GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
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
                <span style="color: #FF0000; font-weight: bold">��ʾ��</span><br />
                <span style="color: #0000FF">һ���������裺<br />
                    �����ϸ�˳�������<br />
                    ��1�����ȴ�EMPϵͳ����һ��������ģ�壨���������̽�����������������������2����������<br />
                    ��2������������ģ���Ϸֱ���2������������¼���ݲ��ϴ���ϵͳ��<br />
                    ��3��������������ȷ���ύ��2��������ͬʱ���ύ����ϵͳ�Զ���ת�����´�����������<br />
                    ��������ģ���ʹ��<br />
                    1.ģ�嵼��ʱ�䣺ÿ��21��-25�գ�������һ����¹�����ģ�壬�Ա�֤����������̡�����Ա����Ʒ���Ƶ�����������׼ȷ�ԡ�<br />
                    2.ģ���ʽ���õ�����<br />
                    A.������ģ���С��ŵ�ID�����ŵ����ơ����ŵ���ࡱ������¡�������ID������������������Ʒ���ơ�����ģ�����Ѵ��ڵ�������Ϣ�����ø��ģ���������������ɾ�������̡�����Ա����Ʒ���ơ�<br />
                    B.Excelģ���У�����2���������ֱ�Ϊ�������̽���������������������������ɾ������Ĺ��������ơ�<br />
                    3.����ѭ������������Ļ����ϣ����轫�����̵Ľ������롾�����̽������������������롾�����������������������ж�������Ա���轫��������������ֺ��ٶ�Ӧ����Ա¼����ģ���У�������󼴿��ϴ���ϵͳ�С���¼ʱҲ��ͨ������ճ������ֵ������ģ�С�<br />
                    4.��¼����������ʱ�����밴��Ʒ��С���۰�װ�Ĳ�Ʒ������¼�������ޡ������е���С��λ����ĳһ��Ʒ������ʱ�����Կ��ţ�������0��<br />
                    5.ͬһ������ͬһ����·����н����Ķ�����ݺϲ���һ��ͬһ������ͬһ����Աͬһ��������������Ķ�����ݺϲ���һ�𣬾���ģ������һ�μ��ɡ�<br />
                    �������ݵ������<br />
                    1.������ڣ�ͬһ�����̡�ͬһ������ͬһ��Ʒ�������������ݣ�����ֶ�ε��룬ϵͳֻĬ�����һ����ȷ��������ݡ�<br />
                    2.������޷������¼���������ʾ��δ�ɹ����롱�������и������ٴε��� ����(Ϊ����ظ����룬����ѳɹ��������������������ɾ��)�� </span>
                <br />
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 50;
        divGridView.style.height = window.screen.availHeight - 500;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
