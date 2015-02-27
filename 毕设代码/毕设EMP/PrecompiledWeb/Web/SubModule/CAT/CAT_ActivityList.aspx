<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CAT_CAT_ActivityList, App_Web_k8itolrs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="��б�"></asp:Label></h2>
                        </td>
                        <td align="right">
                          <asp:Button ID="btn_Export" runat="server" Text="��  ��" OnClick="btn_Export_Click" />
                            <asp:Button ID="bt_Find" runat="server" Text="�� ��" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="�����" Width="80px" OnClick="bt_Add_Click" />
                            <asp:Button ID="bt_Add2" runat="server" Text="�����ﱸ�" Width="80px" OnClick="bt_Add2_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <table class="h3Row" cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td height="28px">
                                        <h3>
                                            ��ѯ����</h3>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                ����Ƭ��
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                            </td>
                                            <td class="dataLabel">
                                                <asp:DropDownList ID="ddl_SelectDateMethod" runat="server">
                                                    <asp:ListItem Value="CAT_Activity.InsertTime" Selected="True">�¼������</asp:ListItem>
                                                    <asp:ListItem Value="CAT_Activity.PlanBeginDate">�ƻ��ٰ�����</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="���ڸ�ʽ����"
                                                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>
                                                ��
                                                <asp:TextBox ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="���ڸ�ʽ����"
                                                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                            </td>
                                            <td class="dataLabel">
                                                �����
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                �����
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Topic" Width="200px" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel">
                                                �¼����
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Staff" runat="server" Width="220px" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx?MultiSelected=Y" />
                                            </td>
                                            <td class="dataLabel">
                                                �ٰ�ͻ�
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Client" runat="server" Width="320px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                                    <Items>
                                                        <mcs:MCSTabItem Text="�����еĻ" Value="4"  Visible="false"/>
                                                        <mcs:MCSTabItem Text="δ�ύ�Ļ" Value="11" />
                                                        <mcs:MCSTabItem Text="�����еĻ" Value="12" />
                                                        <mcs:MCSTabItem Text="�ﱸ�еĻ" Value="1" />
                                                        <mcs:MCSTabItem Text="�Ѿٰ�Ļ" Value="2" />
                                                        <mcs:MCSTabItem Text="��ȡ���Ļ" Value="3" Visible="false" />
                                                        <mcs:MCSTabItem Text="����δͨ��" Value="13" />
                                                    </Items>
                                                </mcs:MCSTabControl>
                                            </td>
                                        </tr>
                                        <tr class="tabForm">
                                            <td>
                                                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="CAT_Activity_ID" PageSize="15" Width="100%" PanelCode="Panel_CAT_ActivityList_01">
                                                    <Columns>
                                                        <asp:HyperLinkField DataNavigateUrlFields="CAT_Activity_ID" DataNavigateUrlFormatString="CAT_ActivityDetail.aspx?ID={0}"
                                                            Text="�鿴��ϸ" ControlStyle-CssClass="listViewTdLinkS1" >
                                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                                        </asp:HyperLinkField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="Hp_ApproveRegister" runat="server" NavigateUrl='<%# Eval("CAT_Activity_TaskID", "../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                                    Text="������¼" Visible='<%# Eval("CAT_Activity_TaskID").ToString()!="" %>' ></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        ������
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
