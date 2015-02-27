<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_PromotorSalaryDetail, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 220px">
                                    <h2>
                                        ����Աн�궨����ϸ��Ϣ
                                    </h2>
                                </td>
                                <td>
                                 <a href="SalaryModelFieldDescribe.htm"><h3>�ᱨ˵��</h3></a> 
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="�� ��" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Width="60" Text="ɾ��" OnClientClick="return confirm('�Ƿ�ȷ��ɾ����н�ꣿ')"
                                        OnClick="bt_Delete_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Width="60px" Text="�� ��" OnClick="bt_Approve_Click"
                                        OnClientClick="return confirm(&quot;ȷ������Ա�����ͨ��?&quot;)" Visible="false" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="�ύ����" Width="60px" OnClick="bt_Submit_Click"
                                        OnClientClick="return confirm(&quot;ȷ����������н������?&quot;)" />                                  
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
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                        SelectedIndex="1" Width="100%">
                                        <Items>
                                            <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��������" Description=""
                                                Value="0" Enable="True"></mcs:MCSTabItem>
                                            <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="н�궨��" Description=""
                                                Value="1" Enable="True"></mcs:MCSTabItem>
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="Page_PM_003">
                                                        </mcs:UC_DetailView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tbl_Promotor" cellspacing="0" cellpadding="0" width="100%" border="0"
                                                    runat="server">
                                                    <tr>
                                                        <td>
                                                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td nowrap>
                                                                        <h3>
                                                                            ����Ա��н�궨���б�
                                                                        </h3>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Button ID="bt_Add" runat="server" Text="����н��" Width="60px" Visible="true" UseSubmitBehavior="False"
                                                                            OnClick="bt_Add_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <mcs:UC_GridView ID="gv_list" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                DataKeyNames="PM_PromotorSalary_ID" PanelCode="Panel_PM_PromotorSalaryDefine_List"
                                                                OnSelectedIndexChanging="gv_list_SelectedIndexChanging">
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Link" ControlStyle-CssClass="listViewTdLinkS1" ShowSelectButton="True"
                                                                        SelectText="�鿴��ϸ" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "~/SubModule/EWF/TaskDetail.aspx?TaskID="+DataBinder.Eval(Container,"DataItem.PM_PromotorSalary_ApproveTask").ToString()%>'
                                                                                 Text='������¼' Visible='<%# DataBinder.Eval(Container,"DataItem.PM_PromotorSalary_ApproveTask").ToString()!="" %>'
                                                                                CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    ������</EmptyDataTemplate>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
