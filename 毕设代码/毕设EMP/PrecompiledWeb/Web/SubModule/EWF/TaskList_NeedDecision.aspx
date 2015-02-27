<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_TaskList_NeedDecision, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                ����������Ϣ</h2>
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                Text="ȫѡ" Visible="true" />
                            <asp:Button ID="btn_pass" runat="server" Text="����ͨ��" OnClick="btn_pass_Click" OnClientClick="return confirm(&quot;�Ƿ�ȷ�Ͻ�ѡ�е�������������Ϊͨ����&quot;)"
                                Visible="true" />
                            <asp:Button ID="btn_nopass" runat="server" Text="������ͨ��" OnClick="btn_nopass_Click"
                                OnClientClick="return confirm(&quot;�Ƿ�ȷ�Ͻ�ѡ�е�������������Ϊ��ͨ����&quot;)" Visible="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��������" Description=""
                            Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��������" Description=""
                            Value="1" Enable="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="���ҷ���" Description=""
                            Value="2" Enable="True"></cc1:MCSTabItem>
                        <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="���������" Description=""
                            Value="3" Enable="True"></cc1:MCSTabItem>
                    </Items>
                </cc1:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="dataLabel">
                            ��ѯ����
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_App" runat="server" AutoPostBack="True" DataTextField="Name"
                                Width="200px" DataValueField="ID" OnSelectedIndexChanged="ddl_App_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            ������Ƭ��
                        </td>
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="150px" DisplayRoot="True" />
                        </td>
                        <td class="dataLabel">
                            ����������
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_InitiatorStaffName" runat="server" Width="70px"></asp:TextBox>
                        </td>
                        <td class="dataLabel">
                            ����
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_MessageSubject" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Search" runat="server" Text="�� ѯ" OnClick="btn_Search_Click"
                                Width="60px" />
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
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID,CurrentJobID,DecisionID"
                                Width="100%" AllowPaging="True" OnPageIndexChanging="gv_List_PageIndexChanging"
                                OnRowDataBound="gv_List_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%# (string)DataBinder.Eval(Container.DataItem, "ReadFlag").ToString() != "Y" ? "<img src='../../Images/mailclose.gif' title='δ��'/>" : "<img src='../../Images/mailopen.gif' title='�Ѷ�'/>"%>
                                            <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="Recipient.aspx?CurrentJobID=<%# DataBinder.Eval(Container.DataItem, "CurrentJobID")%>&TaskID=<%# DataBinder.Eval(Container.DataItem,"TaskID")%>"
                                                class="listViewTdLinkS1" >����</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OrganizeCity1" HeaderText="����" />
                                    <asp:BoundField DataField="OrganizeCity3" HeaderText="Ӫҵ��" />
                                    <asp:BoundField DataField="OrganizeCity4" HeaderText="���´�" />
                                    <asp:BoundField DataField="TaskID" HeaderText="��ʶ" />
                                    <asp:BoundField DataField="Title" HeaderText="����" SortExpression="Title" />
                                    <asp:BoundField DataField="MessageSubject" Visible="false" HeaderText="��Ϣ����" SortExpression="MessageSubject" />
                                    <asp:BoundField DataField="AppName" HeaderText="��������" />
                                    <asp:BoundField DataField="ProcessName" HeaderText="��ǰ����" />
                                    <asp:BoundField DataField="ApplyStaffName" HeaderText="������" />
                                    <asp:BoundField DataField="TaskStartTime" HeaderText="���뷢��ʱ��" SortExpression="TaskStartTime"
                                        DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                    <asp:BoundField DataField="JobStartTime" HeaderText="��������ʱ��" SortExpression="JobStartTime"
                                        DataFormatString="{0:yyyy-MM-dd HH:mm}" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
