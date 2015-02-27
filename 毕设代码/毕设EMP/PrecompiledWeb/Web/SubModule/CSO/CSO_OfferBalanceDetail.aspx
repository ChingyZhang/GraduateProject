<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CSO_CSO_OfferBalanceDetail, App_Web_quved-rv" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0px">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="moduleTitle" width="100%" border="0px">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="������ϸҳ"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="����ΪExcel"
                                Width="100px" />
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="�ύ����" Width="80px" />
                            <asp:Button ID="bt_CreateFeeApply" runat="server" Text="���ɷ������뵥" Width="100px" OnClick="bt_CreateFeeApply_Click"
                                OnClientClick="return confirm('���ɷ������뵥�󣬸÷�����ȡ��������ɾ�����Ƿ�ȷ�����ɷ������뵥��')" Visible="false" />
                            <asp:Button ID="bt_Delete" runat="server" Text="ɾ����������" Width="100px" OnClick="bt_Delete_Click"
                                OnClientClick="return confirm('ȷ��ɾ������ȡ�����������ã�')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_CSO_OfferBalanceInfo_01">
                </mcs:UC_DetailView>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                <tr>
                                    <td width="120">
                                        <h3>
                                            ������ϸ</h3>
                                    </td>
                                    <td width="100">
                                        <asp:Button ID="bt_BatAdjust" runat="server" OnClick="bt_BatAdjust_Click" OnClientClick="return confirm('�Ƿ�ȷ�������۳�ѡ�е�ҽ����ã�')"
                                            Text="�����۳�����" Width="80px" />
                                    </td>
                                    <td width="500px">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                �����̣�
                                                <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                                    Width="180px" />
                                                &nbsp;Ӫ��רԱ��
                                                <asp:TextBox ID="tbx_StaffName" runat="server" Width="60px"></asp:TextBox>
                                                &nbsp;VIP������
                                                <asp:TextBox ID="tbx_OfferManName" runat="server" Width="60px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Button ID="bt_Find" runat="server" Text="�� ��" Width="60px" OnClick="bt_Find_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                PageSize="15" Width="100%" PanelCode="Panel_CSO_OfferBalanceDetailList_01" DataKeyNames="CSO_OfferBalance_Detail_ID,CSO_OfferBalance_Detail_Balance,CSO_OfferBalance_Detail_OfferMan,CSO_OfferBalance_Detail_OfferMode,CSO_OfferBalance_Detail_TrackStaff,CSO_OfferBalance_Detail_DoctorStandard,CSO_OfferBalance_Detail_Product"
                                OnRowDataBound="gv_List_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='' Target="_blank" Text="�鿴��ϸ"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" BorderStyle="None" />
                                            <asp:Button ID="bt_Adjust" runat="server" OnClientClick='<%# "javascript:PopAdjust("+Eval("CSO_OfferBalance_Detail_ID").ToString()+")" %>'
                                                Text="����" OnClick="bt_Adjust_Click"></asp:Button>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="button" />
                                    </asp:TemplateField>
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
