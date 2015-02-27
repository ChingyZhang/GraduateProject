<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JXCSummary_List.aspx.cs" Inherits="SubModule_SVM_JXCSummary_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="�ͻ�������ͳ��"></asp:Label>
                            </h2>
                        </td>
                        <td class="dataLabel" width="60px">
                            �����
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_BeginMonth" DataValueField="ID" DataTextField="Name" runat="server">
                            </asp:DropDownList>
                            ��<asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataField" align="right">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server"
                            id="tb_Head">
                            <tr>
                                <td class="dataLabel">
                                    ����Ƭ��
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    �ͻ�
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx?NoParent=Y"
                                        Width="200px" />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_IsOpponent" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_IsOpponent_SelectedIndexChanged">
                                        <asp:ListItem Text="��Ʒ" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="��Ʒ" Value="9"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="�� ��" Width="60px" OnClick="bt_Search_Click" />
                                    <asp:Button ID="bt_Edit" runat="server" Text="�� ��" Width="60px" OnClick="bt_Edit_Click" />
                                    <asp:Button ID="bt_BathApprove" runat="server" Text="�������" Width="60px" OnClick="bt_BathApprove_Click"
                                        OnClientClick="return confirm('�Ƿ�ȷ�Ͻ�ѡ�е�����������Ϊ�����ͨ����')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>
                                        �������б�</h3>
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_ApproveFlag_SelectedIndexChanged"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                        OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                        <Items>
                                            <mcs:MCSTabItem ID="MCSTabItem1" Text="ʵ�ʾ������ۺ���" runat="server" Value="1" />
                                            <mcs:MCSTabItem ID="MCSTabItem2" Text="�������۽��ͳ��" runat="server" Value="2" />
                                            <mcs:MCSTabItem ID="MCSTabItem3" Text="�����ۼ۽��ͳ��" runat="server" Value="3" Visible="false" />
                                            <mcs:MCSTabItem ID="MCSTabItem4" Text="��˾��׼���ۺ���" runat="server" Value="4" Visible="false" />
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Client,AccountMonth" PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_ID" runat="server" Visible='<%#  Eval("ApproveFlag").ToString()=="δ���"%>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "JXCSummary_Detail.aspx?ClientID="+Eval("Client").ToString() +"&AccountMonth="+Eval("AccountMonth").ToString()+"&IsOpponent="+ rbl_IsOpponent.SelectedValue%>'
                                                        Text="��ϸ"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrganizeCityName" HeaderText="��������" SortExpression="OrganizeCityName" />
                                            <asp:BoundField DataField="AccountMonthName" HeaderText="�·�" SortExpression="AccountMonthName" />
                                            <asp:BoundField DataField="ClientName" HeaderText="�ͻ�ȫ��" SortExpression="ClientName" />
                                            <asp:BoundField DataField="ClientTypeName" HeaderText="�ͻ�����" SortExpression="ClientTypeName"
                                                Visible="false" />
                                            <asp:BoundField DataField="BeginningInventory" HeaderText="�ڳ����(���½�ֹ20��ʵ�����ת)" SortExpression="BeginningInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="PurchaseVolume" HeaderText="21-20��ERP����(�Է�������Ϊ׼)" SortExpression="PurchaseVolume"
                                                DataFormatString="{0:0.##}" />
                                             <asp:BoundField DataField="SignInVolume" HeaderText="21-20�տͻ�ʵ�ﵽ��ǩ��(����һ�������;����ʵ�ﵽ��)" SortExpression="SignInVolume"
                                                DataFormatString="{0:0.##}" />                                          
                                            <asp:BoundField DataField="RecallVolume" HeaderText="�����˻�" SortExpression="RecallVolume"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="Green" HeaderStyle-ForeColor="Green">
                                                <HeaderStyle ForeColor="Green" />
                                                <ItemStyle ForeColor="Green" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SalesVolume" HeaderText="���������۳���" SortExpression="SalesVolume"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="Blue" HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle ForeColor="Blue" />
                                                <ItemStyle ForeColor="Blue" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GiftVolume" HeaderText="����" SortExpression="GiftVolume"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="Red" HeaderStyle-ForeColor="Red" Visible="false">
                                                <HeaderStyle ForeColor="Red" />
                                                <ItemStyle ForeColor="Red" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ReturnedVolume" HeaderText="�˻ع�˾" SortExpression="ReturnedVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="StaleInventory" HeaderText="�̴�ӯ(+)��(-)" SortExpression="StaleInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferInVolume" HeaderText="����-����" SortExpression="TransferInVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferOutVolume" HeaderText="����-����" SortExpression="TransferOutVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="EndingInventory" HeaderText="��ĩ�̴�" SortExpression="EndingInventory" Visible="false"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="#993300" HeaderStyle-ForeColor="#993300">
                                                <HeaderStyle ForeColor="#993300" />
                                                <ItemStyle ForeColor="#993300" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComputInventory" HeaderText="������20��ʵ����" SortExpression="ComputInventory"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="#993300" HeaderStyle-ForeColor="#993300">
                                                <HeaderStyle ForeColor="#993300" />
                                                <ItemStyle ForeColor="#993300" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="TransitInventory" HeaderText="��ĩ��;(��ֹ20��)" SortExpression="TransitInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="PaperInventory" HeaderText="������ϼƣ�ʵ��+��;��" SortExpression="PaperInventory"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="#993300" HeaderStyle-ForeColor="#993300" Visible="false">
                                                <HeaderStyle ForeColor="#993300" />
                                                <ItemStyle ForeColor="#993300" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="�����˻�" HeaderStyle-ForeColor="Green" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubReturnedVolume" ForeColor="Green" runat="server" Text='<%# GetSubJXC((int)Eval("AccountMonth"),(int)Eval("Client"),"ReturnedVolume").ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="Green" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���ν���" HeaderStyle-ForeColor="Blue" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubPurchaseVolume" ForeColor="Blue" runat="server" Text='<%# GetSubJXC((int)Eval("AccountMonth"),(int)Eval("Client"),"PurchaseVolume").ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="Blue" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��������" HeaderStyle-ForeColor="Red" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubGiftVolume" ForeColor="Red" runat="server" Text='<%# GetSubJXC((int)Eval("AccountMonth"),(int)Eval("Client"),"GiftVolume").ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="Red" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApproveFlag" HeaderText="��˱�־" SortExpression="ApproveFlag" />
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
                        <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_BathApprove" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_ApproveFlag" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_IsOpponent" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
