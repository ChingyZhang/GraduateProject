<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ImportHistory.aspx.cs" Inherits="SubModule_SVM_ImportExcel_ImportHistory" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="��Excel���������̽���������������¼"></asp:Label></h2>
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
                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tb_jxc">
                    <tr>
                        <td class="dataLabel" width="80">
                            ��Ա
                        </td>
                        <td class="dataField" width="300">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                        Width="260px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="dataLabel">
                            �����
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            �ͻ�����
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_ClientType" runat="server">
                                <asp:ListItem Text="����" Value="0"></asp:ListItem>
                                <asp:ListItem Text="������" Value="2"></asp:ListItem>
                                <asp:ListItem Text="������" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            ״̬
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_ExcelState" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            Ʒ�����
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Opponent" runat="server">
                                <asp:ListItem Text="����" Value="0"></asp:ListItem>
                                <asp:ListItem Text="��Ʒ" Value="1"></asp:ListItem>
                                <asp:ListItem Text="��Ʒ" Value="9"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Button ID="bt_search" runat="server" Text="�� ѯ" OnClick="bt_search_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tb_keytarget"
                    visible="false">
                    <tr>
                        <td class="dataLabel" width="80">
                            Ӫҵ��
                        </td>
                        <td class="dataField" width="400">
                            <asp:DropDownList ID="ddl_OrganizeCity" runat="server" Width="180px" AutoPostBack="true" >
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            �����
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_AccountMonth2" runat="server" DataTextField="Name" 
                                DataValueField="ID"  AutoPostBack="true" AppendDataBoundItems="true"
                                onselectedindexchanged="ddl_AccountMonth2_SelectedIndexChanged">
                                  <asp:ListItem Text="����" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            ״̬
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_ExcelState2" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Button ID="Button1" runat="server" Text="�� ѯ" OnClick="bt_search_Click" />
                        </td>
                        <td>
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
                            <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="ģ������״̬" Value="1" />
                                                <mcs:MCSTabItem Text="�����ϴ�״̬" Value="2" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_downtemplate" runat="server" Width="100%" AutoGenerateColumns="False"
                                            DataKeyNames="SVM_DownloadTemplate_ID" PanelCode="SVM_DownloadTemplate_Panel"
                                            AllowPaging="True" PageSize="15" AllowSorting="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="�ȴ�����">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_waitdown" runat="server" Text='<%#GetDownWait( DataBinder.Eval(Container,"DataItem.SVM_DownloadTemplate_ID").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="bt_DownloadTemplate" runat="server" Text="����ģ��" Width="100px" OnClick="bt_DownloadTemplate_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_uptemplate" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="SVM_UploadTemplate_Panel" AllowPaging="True" PageSize="15" AllowSorting="true"
                                            DataKeyNames="SVM_UploadTemplate_ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="�ȴ�����">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_waitup" runat="server" Text='<%#GetUpWait( DataBinder.Eval(Container,"DataItem.SVM_UploadTemplate_ID").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ԥ�����ʱ��">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_waituptime" runat="server" Text='<%#GetUpWaitTime( DataBinder.Eval(Container,"DataItem.SVM_UploadTemplate_ID").ToString()) %>'
                                                            ToolTip="�������Ԥ��ʱ��̫������ϵ����Ա��"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="bt_ViewRemark" runat="server" Text="�鿴������" Width="100px" OnClick="bt_ViewRemark_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                            <%--    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_search" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
