<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Journal_JournalList, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

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
                                ������־--�б���ͼ
                            </h2>
                        </td>
                        <td align="left">
                            ��ʼ����
                            <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox><span
                                style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                Display="Dynamic" ErrorMessage="��ʽ����" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                            ��
                            <asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox><span
                                style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="tbx_enddate" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_enddate"
                                Display="Dynamic" ErrorMessage="��ʽ����" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                            ��־����
                            <asp:DropDownList ID="ddl_JournalType" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="�� ѯ" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="д����־" Width="60px" OnClientClick="NewJournal(0)"
                                OnClick="bt_Add_Click" CausesValidation="false" />
                            <asp:Button ID="bt_CalendarView" runat="server" OnClick="bt_CalendarView_Click" Text="������ͼ"
                                CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�ҵ���־" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="������־" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="AdvanceFind.aspx" ImgURL="" Text="��־�߼���ѯ" Description=""
                            Value="2" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_Condition"
                            runat="server" visible="false" height="30px">
                            <tr>
                                <td width="60px" class="dataLabel">
                                    ����Ƭ��
                                </td>
                                <td class="dataField" width="220px">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                </td>
                                <td width="60px" class="dataLabel">
                                    Ա��ְ��
                                </td>
                                <td class="dataField" width="220px">
                                    <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" DisplayRoot="True" RootValue="0" />
                                </td>
                                <td class="dataField" width="160">
                                    <asp:CheckBox ID="cb_IncludeChild" runat="server" Checked="True" Style="color: #FF0000"
                                        Text="�����¼�ְλ" />
                                </td>
                                <td width="60px" class="dataLabel">
                                    Ա������
                                </td>
                                <td class="dataField" width="120px">
                                    <asp:TextBox ID="tbx_StaffName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr height="1px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="JN_Journal_ID" PageSize="15" Width="100%"
                            PanelCode="Panel_OA_JournalList_001" OnSelectedIndexChanged="gv_List_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                            Text="��ϸ" OnClientClick='<%#Bind("JN_Journal_ID","Javascript:OpenJournal({0})") %>'
                                            CssClass="listViewTdLinkS1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                ������
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
