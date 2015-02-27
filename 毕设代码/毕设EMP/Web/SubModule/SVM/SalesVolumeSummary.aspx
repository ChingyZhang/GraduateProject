<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="SalesVolumeSummary.aspx.cs" Inherits="SubModule_SVM_SalesVolumeSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="500">
    </asp:Timer>

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
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lbl_Message" runat="server"></asp:Label>
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
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                ��ѯ����</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
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
                                    �����·�
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    �������
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Flag" runat="server">
                                        <asp:ListItem Value="1">��Ʒ</asp:ListItem>
                                        <asp:ListItem Value="2">��Ʒ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ����״̬
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
                                        <asp:ListItem Value="0">����</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">δ�ύ</asp:ListItem>
                                        <asp:ListItem Value="2">�����</asp:ListItem>
                                        <asp:ListItem Value="3">�����</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="����" Width="80px" OnClick="bt_Find_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Text="�������" Width="80px" 
                                        OnClick="bt_Approve_Click" 
                                        onclientclick="return confirm(&quot;�Ƿ�ȷ���������?&quot;)" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="�����ύ" Width="80px" 
                                        OnClick="bt_Submit_Click" 
                                        onclientclick="return confirm(&quot;�Ƿ�ȷ�������ύ?&quot;)" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="�鿴����(���)" Value="1" />
                                                <mcs:MCSTabItem Text="���ͻ���SKU����(����)" Value="2" />
                                                <mcs:MCSTabItem Text="�鿴��ϸ" Value="3" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_Summary" runat="server" Width="96%" CellPadding="1" CellSpacing="1"
                                            BorderWidth="0px" AllowPaging="False" GridLines="Both" BackColor="#CCCCCC" OnRowDataBound="gv_Summary_RowDataBound"
                                            CssClass="">
                                            <HeaderStyle BackColor="#EEEEEE" CssClass="" Height="28px" />
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                            <Columns>
                                            </Columns>
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            PanelCode="Panel_SVM_SalesVolumnList_1" DataKeyNames="SVM_SalesVolume_ID" PageSize="25"
                                            Width="100%">
                                            <Columns>
                                                <asp:HyperLinkField Text="�鿴��ϸ" DataNavigateUrlFields="SVM_SalesVolume_ID" DataNavigateUrlFormatString="SalesVolumeBatchInput.aspx?VolumeID={0}"
                                                    ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px" >
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                    <ItemStyle Width="80px" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField HeaderText="�����۽��" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# GetTotalValue(DataBinder.Eval(Container,"DataItem.SVM_SalesVolume_ID").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="���۽��">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# GetTotalFactoryPriceValue(DataBinder.Eval(Container,"DataItem.SVM_SalesVolume_ID").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
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
                            <asp:AsyncPostBackTrigger ControlID="ddl_State" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Submit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;
        divGridView.style.height = window.screen.availHeight - 350;         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
