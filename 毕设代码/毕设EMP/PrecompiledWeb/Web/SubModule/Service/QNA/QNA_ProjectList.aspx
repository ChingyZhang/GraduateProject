<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Service_QNA_QNA_ProjectList, App_Web_pltcgdyp" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="�ʾ��б�ҳ"></asp:Label></h2>
                        </td>
                         <td align="right">
               �ʾ�¼�����ڷ�Χ��
                <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="���ڸ�ʽ����"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>��<asp:TextBox
                        ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="���ڸ�ʽ����"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                �ʾ����⣺<asp:TextBox ID="tbx_Search" Width="150" runat="server"></asp:TextBox>
               
                            <asp:Button ID="bt_Find" runat="server" Text="�� ��" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="�� ��" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="QNA_Project_ID" PageSize="15" Width="100%" 
                                        PanelCode="Panel_QNA_ProjectList_01" 
                                        onselectedindexchanged="gv_List_SelectedIndexChanged">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="QNA_Project_ID" DataNavigateUrlFormatString="QNA_ProjectDetail.aspx?ID={0}"
                                                DataTextField="QNA_Project_Name" HeaderText="�ʾ�����" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="�ѵ��з���" SortExpression="ResultCount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ResultCount" runat="server" Text='<%# GetResultCount((int)Eval("QNA_Project_ID")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField DataNavigateUrlFields="QNA_Project_ID" DataNavigateUrlFormatString="QNA_ProjectStatistics.aspx?Project={0}"
                                                Text="�鿴���н��" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
