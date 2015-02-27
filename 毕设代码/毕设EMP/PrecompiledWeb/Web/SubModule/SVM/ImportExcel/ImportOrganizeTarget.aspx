<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_ImportExcel_ImportOrganizeTarget, App_Web_v4fiyk14" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="300">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="��Excel�����ص�Ʒ��Ŀ�꼰����"></asp:Label></h2>
                        </td>                        
                        <td class="dataLabel">
                            <font color="red">�����</font>
                      
                            <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                    <Items>
                        <mcs:MCSTabItem Text="����ģ��" Value="1" />
                        <mcs:MCSTabItem Text="�ϴ����´��ص�Ʒ��" Value="2" />
                        <mcs:MCSTabItem Text="�ϴ����´�����" Value="3"  Visible="false" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tb_step1">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr height="28px">
                                                <td nowrap>
                                                    <h3>
                                                        <asp:Label ID="lb_setp1" runat="server" Text=" ��һ�������ذ��´��ص�Ʒ�����ģ��"></asp:Label></h3>
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
                                                <td class="dataLabel">
                                                    Ʒ��
                                                </td>
                                                <td class="dataField">
                                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="bt_DownloadTemplate" runat="server" Text="1.����ģ��" Width="100px" OnClick="bt_DownloadTemplate_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tabForm" colspan="5">
                                                    <asp:CheckBoxList runat="server" RepeatColumns="12" CellPadding="2" CellSpacing="10"
                                                        RepeatDirection="Horizontal" DataTextField="Name" DataValueField="ID" ID="chk_Citylist">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:UpdatePanel ID="udp_product" runat="server">
                                                        <ContentTemplate>
                                                            <div id="div_gift" align="center" runat="server" style="overflow: scroll; height: 500px">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr class="tabForm">
                                                                        <td>
                                                                            <mcs:UC_GridView ID="gv_product" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                Binded="False" ConditionString="" DataKeyNames="PDT_Product_ID" PageSize="20"
                                                                                PanelCode="Panel_PDT_List_001" Width="100%">
                                                                                <Columns>
                                                                                    <asp:TemplateField ItemStyle-Width="20px">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged"
                                                                                                Text="ȫѡ" Width="60px" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_ID" runat="server" AutoPostBack="true" Checked='<%#Produtcts.ToString().IndexOf(","+Eval("PDT_Product_ID").ToString()+",")!=-1 %>'
                                                                                                OnCheckedChanged="chk_ID_CheckedChanged" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    �����ݡ�����
                                                                                </EmptyDataTemplate>
                                                                            </mcs:UC_GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddl_Brand" EventName="selectedindexchanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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
                                                        <asp:Label ID="lb_setp2" runat="server" Text="  �ڶ������ϴ����´��ص�Ʒ��Excel���"></asp:Label></h3>
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
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
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
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000; font-weight: bold">��ʾ��</span><br />
                <span style="color: #0000FF">һ����������<br />
                    �����ϸ�˳�������<br />
                    ��1�����Ƚ����ص�Ʒ��ɸѡ<br />
                    ��2��������һ��������ģ�壨��3����Ʒ��3����������<br />
                    ��3������������ģ���Ϸֱ���3������������¼��������Ʒ�������ݲ��ϴ���ϵͳ��<br />
                    ��4��������������ȷ���ύ��3��������ͬʱ���ύ����ϵͳ�Զ���ת�����´�����������<br />
                    ��������ģ���ʹ��<br />
                    1.ģ�嵼��ʱ�䣺ÿ��21��-25�գ�������һ����¹�����ģ�壬�Ա�֤������������̼���ƷƷ������������׼ȷ�ԡ�<br />
                    2.ģ���ʽ���õ�����<br />
                    A.������ģ���С��ŵ�ID�����ŵ����ơ��� �ŵ���ࡱ�� ����¡��� ��Ʒ���ơ�����ģ�����Ѵ��ڵ�������Ϣ�����ø��ģ���������������ɾ�������̻���Ʒ ���ơ�<br />
                    B.Excelģ���У�����3������������ɾ������Ĺ��������ơ�<br />
                    C.��д����ģ��ʱ������������������ˡ����ء�����������ϴ�ģ��֮ǰ����Щ���С�ȡ�����ء�������Ӱ�����ݵ��������ԡ�<br />
                    D.�ϴ�ϵͳ����ģ����ȷ��ģ����Я�����ꡱ�ļ���������ܵ������ݵ��뱨��<br />
                    3.����ѭ������������Ļ����ϣ����轫������3����Ʒ�����ֱ�����3���������У�����󼴿�һ�����ϴ���ϵͳ�С���¼ʱҲ��ͨ������ճ������ֵ������ģ�С�<br />
                    4.��¼��Ʒ��������ʱ�����밴��Ʒ��С��װ��λ��¼�������ޡ������е���С��λ����ĳһ��Ʒ������ʱ�����Կ��ţ�������0��<br />
                    5.ͬһ����·ݡ�ͬһ�����̡�ͬһ��Ʒ��ʽ������������ϲ���һ��������ģ�塣<br />
                    �������ݵ������<br />
                    1.ͬһ����·ݡ�ͬһ�����̡�ͬһ��Ʒ�������ݣ����ж�����ݵ��룬ϵͳֻĬ�����һ����ȷ��������ݡ�<br />
                    2.������޷������¼���������ʾ��δ�ɹ����롱�������и������ٴε��� ����(Ϊ����ظ����룬����ѳɹ��������������������ɾ��)��<br />
                </span>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
