<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FLFeeApplySummary2.aspx.cs" Inherits="SubModule_FNA_FeeApply_ApplySummary_FLFeeApplySummary" %>

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
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                ���������ᱨ������Ϣ
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" Text="����Excel" OnClick="bt_Export_Click"
                                Width="90px" />
                        </td>
                        <td align="right" nowrap="noWrap" width="300px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Approve" runat="server" ForeColor="Blue" OnClick="bt_Approve_Click"
                                        OnClientClick="return confirm('�Ƿ�ȷ�Ͻ���������Χ���������뵥������Ϊ����ͨ����ע��ò������ܺ�ʱ�ϳ��������ĵȴ���')"
                                        Text="��������ͨ��" Width="90px" Visible="false" />
                                    <asp:Button ID="bt_UnApprove" runat="server" ForeColor="Red" OnClick="bt_UnApprove_Click"
                                        OnClientClick="return confirm('�Ƿ�ȷ�Ͻ���������Χ���������뵥������Ϊ������ͨ�����ò������������������ע��ò������ܺ�ʱ�ϳ��������ĵȴ�')"
                                        Text="����������ͨ��" Width="90px" Visible="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                <asp:UpdatePanel ID="upl_conditon" runat="server">
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
                                    ����״̬
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Value="0">����(�����ύ����ͨ��)</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">�������������뵥</asp:ListItem>
                                        <asp:ListItem Value="2">������ͨ�������뵥</asp:ListItem>
                                        <asp:ListItem Value="3">��������ͨ�������뵥</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="����" Width="80px" OnClick="bt_Find_Click" />
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
                                    <td align="left" height="28px" valign="bottom">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="�������û���" Value="0" />
                                                <mcs:MCSTabItem Text="����������ϸ�б�" Value="1" />  
                                                <mcs:MCSTabItem Text="����������ϸ" Value="2" />
                                                <mcs:MCSTabItem Text="�޵�������������" Value="3" NavigateURL="../../../Reports/ReportViewer.aspx?Report=76e8b35b-b7fb-4012-b919-af5871335d4a" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" OnDataBound="gv_List_DataBound"
                                            GridLines="Both" CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" CssClass=""
                                            BorderWidth="0px" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging">
                                            <HeaderStyle BackColor="#EEEEEE" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_DetailList" runat="server" Width="100%" Visible="false" PanelCode="Panel_FNA_FLFeeDetail" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" DataKeyNames="FNA_FeeApply_ID,FNA_FeeApply_ApproveTask" >
                                        <Columns>
                                             <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    ȫѡ<asp:CheckBox ID="chkHeader" runat="server" ToolTip="ȫѡ" onclick="javascript:SelectAll(this);">
                                                        </asp:CheckBox>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Selected" runat="server" />
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("FNA_FeeApply_ID", "../FeeApplyDetail3.aspx?ID={0}") %>'
                                                        Text='<%# Eval("FNA_FeeApply_SheetCode").ToString() %>' Visible='<%# Eval("FNA_FeeApply_ID").ToString()!="" %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="�������">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeApply_ID").ToString())).ToString("0.###") %>'></asp:Label>Ԫ
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="�ɺ������">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_AvailCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeApplyBLL.GetAvailCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeApply_ID").ToString())).ToString("0.###") %>'></asp:Label>Ԫ
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_FeeApply_ApproveTask", "../../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                        Text="������¼" Visible='<%# Eval("FNA_FeeApply_ApproveTask").ToString()!="" %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_UnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
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

    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
