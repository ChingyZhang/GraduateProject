<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderApplyGiftSummary.aspx.cs" Inherits="SubModule_Logistics_Order_OrderApplyGiftSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SelectAll(tempControl) {
            var theBox = tempControl;
            sState = theBox.checked;
            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id) {
                    if (elem[i].checked != sState) {
                        elem[i].click();
                    }
                }
            }
        }
    
    </script>

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                ��Ʒ����������Ϣ
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Export" runat="server" Text="����Excel" OnClick="bt_Export_Click"
                                Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    ��ѯ����</h3>
                                            </td>
                                            <td align="right">
                                                <asp:CheckBox ID="chkHeader1" runat="server" AutoPostBack="False" Text="ȫѡ" onclick="javascript:SelectAll(this);">
                                                </asp:CheckBox>
                                                <asp:Button ID="bt_Find" runat="server" Text="����" Width="80px" OnClick="bt_Find_Click" />
                                                <asp:Button ID="bt_Approve" runat="server" ForeColor="Blue" OnClick="bt_Approve_Click"
                                                    OnClientClick="return confirm('�Ƿ�ȷ�Ͻ���������Χ���������뵥������Ϊ����ͨ����')" Text="��������ͨ��" Width="90px" />
                                                <asp:Button ID="bt_UnApprove" runat="server" ForeColor="Red" OnClick="bt_UnApprove_Click"
                                                    OnClientClick="return confirm('�Ƿ�ȷ�Ͻ���������Χ���������뵥������Ϊ������ͨ�����ò������к󽫲��ɳ���������ϸȷ�ϣ�')"
                                                    Text="����������ͨ��" Width="90px" />
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
                                                ����Ƭ��
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                            </td>
                                            <td class="dataLabel">
                                                �鿴�㼶
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                ������
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                                    Width="280px" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                �����·�
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                ����״̬
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">����(�����ύ����ͨ��)</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">�������������뵥</asp:ListItem>
                                                    <asp:ListItem Value="2">������ͨ�������뵥</asp:ListItem>
                                                    <asp:ListItem Value="3">��������ͨ�������뵥</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                ��Ʒ�������
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_GiftClassify" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel">
                                                ��Ʒϵ��
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left">
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                        OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                        <Items>
                                            <mcs:MCSTabItem Text="�鿴�������(������������Ʒ����������)" Value="0" />
                                            <mcs:MCSTabItem Text="�鿴������ϸ" Value="1" />
                                            <mcs:MCSTabItem Text="�鿴�������" Value="2" />
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_Summary" runat="server" Width="96%" DataKeyNames="Key" CellPadding="1"
                                        BackColor="#BBBBBB" CssClass="" CellSpacing="1" BorderWidth="0px" AllowPaging="True"
                                        PageSize="50" OnPageIndexChanging="gv_Summary_PageIndexChanging" OnDataBound="gv_Summary_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="-">
                                                <ItemStyle Width="10px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbx" runat="server" Visible="<%#bt_Approve.Enabled %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                        <RowStyle CssClass="" BackColor="White" Height="28px" />
                                        <AlternatingRowStyle CssClass="" BackColor="White" Height="28px" />
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                        GridLines="Horizontal" DataKeyNames="ORD_OrderApply_ID,ORD_OrderApply_TaskID"
                                        AllowPaging="True" PageSize="15" 
                                        onpageindexchanging="gv_ListDetail_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="���뵥��" HeaderText="���뵥��" />
                                            <asp:TemplateField HeaderText="����״̬" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_State" runat="server" Text='<%# Eval("ORD_OrderApply_State") %>'></asp:Label><br />
                                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#  Eval("ORD_OrderApply_ID","OrderApplyDetail3.aspx?ID={0}") %>'
                                                        Text='�򿪶���'></asp:HyperLink><br />
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ORD_OrderApply_TaskID", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                        Text="������¼" Visible='<%# Eval("ORD_OrderApply_TaskID").ToString()!="" %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="����Ƭ��" HeaderText="����Ƭ��" />
                                             <asp:BoundField DataField="�����̱���" HeaderText="�����̱���" />
                                            <asp:BoundField DataField="������" HeaderText="������" />
                                            <asp:BoundField DataField="����Ʒ��" HeaderText="����Ʒ��" />
                                            <asp:BoundField DataField="�������" HeaderText="�������" />
                                            <asp:BoundField DataField="��Ʒ���" HeaderText="��Ʒ���" />
                                            <asp:BoundField DataField="��Ʒ����" HeaderText="��Ʒ����" />
                                            <asp:BoundField DataField="��Ʒ����" HeaderText="��Ʒ����" />
                                            <asp:BoundField DataField="����" HeaderText="����" />
                                            <asp:BoundField DataField="��������" HeaderText="��������" />
                                            <asp:BoundField DataField="��������" HeaderText="��������" />
                                            <asp:TemplateField HeaderText="����ԭ��">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbx_AdjustReason" runat="server" Text='<%# Bind("����ԭ��") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:BoundField DataField="������" HeaderText="������" />
                                        </Columns>
                                    </mcs:UC_GridView>
                                    <mcs:UC_GridView ID="gv_Total" runat="server" Width="96%" CellPadding="1" BackColor="#BBBBBB"
                                        CssClass="" CellSpacing="1" BorderWidth="0px" AllowPaging="True" PageSize="25"
                                        OnPageIndexChanging="gv_Total_PageIndexChanging">
                                        <Columns>
                                        </Columns>
                                        <HeaderStyle BackColor="#EEEEEE" CssClass="" Height="28px" />
                                        <RowStyle CssClass="" BackColor="White" Height="28px" />
                                        <AlternatingRowStyle CssClass="" BackColor="White" Height="28px" />
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_Approve" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_UnApprove" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        <asp:AsyncPostBackTrigger ControlID="ddl_State" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
