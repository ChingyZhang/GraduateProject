<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FeeApplyList.aspx.cs" Inherits="SubModule_FNA_FeeApply_FeeApplyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function LinkPage(address) {
            window.location.href(address);
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
                                        ���������б��ѯ
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="����" Width="80px" />
                                    <asp:Button ID="bt_Add" runat="server" Text="�·�������(ͨ��)" OnClick="bt_Add_Click" Width="100px" />
                                    <asp:Button ID="bt_Add_CL" runat="server" Text="�·�������(����)" OnClick="bt_Add_CL_Click"
                                        Width="100px" />
                                    <asp:Button ID="bt_Add_FL" runat="server" Text="�·�������(����)" OnClick="bt_Add_FL_Click"
                                        Width="100px" />
                                    <asp:Button ID="bt_Add_Gift" runat="server" Text="�·�������(��Ʒ)" OnClick="bt_Add_Gift_Click"
                                        Width="100px" />
                                    <asp:Button ID="bt_Add_Car" runat="server" Text="�·�������(����)" OnClick="bt_Add_Car_Click"
                                        Width="100px" />
                                    <asp:Button ID="bt_Add_Promotor" runat="server" Text="Ԥ���������������" Width="100px" OnClick="bt_Add_Promotor_Click" />
                                    <asp:Button ID="bt_Missed" runat="server" Text="�鿴��������" OnClientClick="LinkPage('../../Reports/ReportViewer.aspx?Report=5b9bf477-673c-4b91-8e1b-81b2d49d2651')"
                                        Width="80px" Visible="false" />
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td class="dataLabel">
                                            ����Ƭ��
                                        </td>
                                        <td class="dataField">
                                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                ParentColumnName="SuperID" Width="180px" />
                                        </td>
                                        <td class="dataLabel">
                                            �����·�
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            ��������
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            ��ƿ�Ŀ(����)
                                        </td>
                                        <td class="dataField" colspan="3">
                                            <mcs:MCSTreeControl ID="tr_AccountTitle" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                ParentColumnName="SuperID" Width="260px" TableName="MCS_Pub.dbo.AC_AccountTitle"
                                                RootValue="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel">
                                            ���뱸����
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_SheetCode" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                        <td class="dataLabel">
                                            ���뵥����
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_Title" runat="server" Width="120px"></asp:TextBox>
                                        </td>
                                        <td class="dataLabel">
                                            ����״̬
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            ����״̬
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_WriteOffState" runat="server">
                                                <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="δ��ȫ����" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="����ȫ����" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            ¼����
                                        </td>
                                        <td class="dataField">
                                            <mcs:MCSSelectControl ID="Select_InsertStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                                Width="120px"></mcs:MCSSelectControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" height="28px">
                                            ���뾭����
                                        </td>
                                        <td align="left" class="dataField">
                                            <mcs:MCSSelectControl ID="select_ApplyClient" runat="server" Width="280px" PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1,3)\"' />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        ���������б�</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            PanelCode="Panel_FNA_FeeApplyList" DataKeyNames="FNA_FeeApply_ID" AllowPaging="True"
                            PageSize="15" AllowSorting="true">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeApply_ID" DataNavigateUrlFormatString="FeeApplyDetail3.aspx?ID={0}"
                                    DataTextField="FNA_FeeApply_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                    HeaderText="���뱸����" SortExpression="FNA_FeeApply_SheetCode">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
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
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_FeeApply_ApproveTask", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
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
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
