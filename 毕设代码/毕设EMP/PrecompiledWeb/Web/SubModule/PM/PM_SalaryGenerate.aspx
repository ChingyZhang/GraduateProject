<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_SalaryGenerate, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        ���ɵ���Ա����</h2>
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
                    <td class="tabForm" align="center">
                        <table cellpadding="0" cellspacing="0" border="0" width="50%" height="100">
                            <tr>
                                <td class="dataLabel" height="28">
                                    ����Ƭ��
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" AutoPostBack="False" IDColumnName="ID"
                                        NameColumnName="Name" ParentColumnName="SuperID" Width="220px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28">
                                    �·�
                                </td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="28">
                                    ������
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="280px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="bt_Generate" runat="server" Text="���ɹ���" Width="80px" OnClick="bt_Generate_Click"  Enabled="false"/>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" height="70" style="color: #FF0000">
                                    ע�������ɵ���Ա����֮ǰ������ȷ���������<br />
                                    1.�������ŵ�����¼�벢�����ˡ�<br />
                                    2.������ְ����Ա����Ϊ��ְ��<br />
                                    3.��¼�롾����Ա���ͼ���������������ˡ�
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle"
                align="center" runat="server" id="gv_Table" visible="false">
                <tr>
                    <td class="h3Row" height="30">
                        <h2>
                            н����Ϣ�����ϵ���</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                            PageIndex="10">
                            <Columns>
                                <asp:BoundField DataField="Promotor" HeaderText="����ԱID" ItemStyle-Width="100" />
                                <asp:HyperLinkField ControlStyle-CssClass="listViewTdLinkS1" DataNavigateUrlFields="Promotor"
                                    DataNavigateUrlFormatString="PM_PromotorDetail.aspx?PromotorID={0}" DataTextField="Name"
                                    HeaderText="����Ա����" Target="_blank" ItemStyle-Width="160">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="�����ŵ�" ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:Literal ID="lt_PromotorInClient" runat="server" Text='<%#PromotorInClient((int)Eval("Promotor")) %>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Err" HeaderText="�쳣ԭ��" />
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
