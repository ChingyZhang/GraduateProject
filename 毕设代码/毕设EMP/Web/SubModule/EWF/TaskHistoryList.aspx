<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="TaskHistoryList.aspx.cs" Inherits="SubModule_EWF_TaskHistoryList" %>

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
                            <asp:Button ID="btn_Search" runat="server" Text="�� ѯ" OnClick="btn_Search_Click"
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="3" Width="100%">
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="dataLabel">
                                    ��ѯ����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_App" runat="server" AutoPostBack="True" DataTextField="Name"
                                        DataValueField="ID" OnSelectedIndexChanged="ddl_App_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ������Ƭ��
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                </td>
                                <td class="dataLabel">
                                    ���̷�����
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" Width="180px" PageUrl="~/SubModule/Staffmanage/Pop_Search_Staff.aspx" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �����������
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                        runat="server" ErrorMessage="���ڸ�ʽ����" Display="Dynamic" Operator="DataTypeCheck"
                                        Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="����" ControlToValidate="tbx_begin"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    ��
                                    <asp:TextBox ID="tbx_end" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator2"
                                        runat="server" ErrorMessage="���ڸ�ʽ����" Display="Dynamic" Operator="DataTypeCheck"
                                        Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_end"
                                        Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                </td>
                                <td class="dataLabel">
                                    ���̹ؼ���
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_KeyWords" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    ���̽���״̬
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_FinishStatus" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                Width="100%" AllowPaging="True" OnPageIndexChanging="gv_List_PageIndexChanging"
                                PageSize="15">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="TaskDetail.aspx?TaskID={0}"
                                        Text="��ϸ��Ϣ" ControlStyle-CssClass="listViewTdLinkS1" />
                                    <asp:BoundField DataField="ID" HeaderText="����������" />
                                    <asp:BoundField DataField="App" HeaderText="��������" />
                                    <asp:BoundField DataField="Title" HeaderText="���̱���" SortExpression="Title" />
                                    <asp:BoundField DataField="Initiator" HeaderText="������" />
                                    <asp:BoundField DataField="StartTime" HeaderText="���̷���ʱ��" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                    <asp:BoundField DataField="EndTime" HeaderText="���̽�ֹʱ��" SortExpression="EndTime" />
                                    <asp:BoundField DataField="FinishStatus" HeaderText="�������״̬" />
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
