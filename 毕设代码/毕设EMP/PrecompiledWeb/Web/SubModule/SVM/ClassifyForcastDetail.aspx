<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_SalesForcastDetail, App_Web_nv-hfo9a" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24" style="height: 24px">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" width="160" align="left">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="�ͻ�����Ԥ����"></asp:Label>
                                    </h2>
                                </td>
                                <td align="left">
                                </td>
                                <td align="right" width="100%">
                                    <asp:CheckBox ID="cbx_UpdateAccountMonth" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_UpdateAccountMonth_CheckedChanged"
                                        Text="�޸Ļ����" />
                                    <asp:Button ID="btn_SalesForcast" runat="server" Text="�Ԥ��" Width="80px" OnClick="btn_SalesForcast_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="����Ԥ���������"
                                        OnClientClick="return confirm('�Ƿ�ȷ����˸ü�¼����˺�����ݽ������ٸ��ģ�')" Width="120px" />
                                    <asp:Button ID="bt_DirectApprove" runat="server" Text="ֱ�����" OnClick="bt_DirectApprove_Click" />
                                    <asp:Button ID="bt_Save" runat="server" Text="�� ��" OnClick="bt_Save_Click" Width="60px"
                                        UseSubmitBehavior="False" />
                                        <asp:Button ID="bt_Refresh" runat="server" Text="ˢ�¾�����" Width="80px" onclick="bt_Refresh_Click"
                                         />
                                    <asp:Button ID="bt_Del" runat="server" Text="ɾ ��" Width="60px" OnClientClick="return confirm('�Ƿ�ȷ��ɾ���ü�¼?')"
                                        OnClick="bt_Del_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_ClassifyForcast_Detail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbx_UpdateAccountMonth" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                    <tr>
                        <td nowrap style="width: 100px" colspan="1">
                            <h3>
                                Ԥ���б�</h3>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="dataLabel" style="color: #FF0000">
                                                Ʒ��
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                    OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel" style="color: #FF0000">
                                                ϵ��
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                                    RepeatDirection="Horizontal">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Search" runat="server" Text="�� ��" Width="60px" OnClick="bt_Search_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SVM_ClassifyForcast_Detail_ID"
                                        PanelCode="Panel_SVM_ClassifyForcast_DetailList" Width="100%" AllowPaging="false">
                                        <Columns>     
                                         <asp:TemplateField HeaderText="��������(Ԫ)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Sales" runat="server" Text='<%# Bind("SVM_ClassifyForcast_Detail_AvgSales","{0:0.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                     
                                            <asp:TemplateField HeaderText="������">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Rate" runat="server" Text='<%# Bind("SVM_ClassifyForcast_Detail_Rate","{0:0.##}") %>'
                                                        Width="80px"></asp:TextBox>%
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Rate"
                                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ԥ�����۶�(Ԫ)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Amount" runat="server" Text='<%# Bind("SVM_ClassifyForcast_Detail_Amount","{0:0.##}") %>'
                                                        Enabled='<%# decimal.Parse(Eval("SVM_ClassifyForcast_Detail_AvgSales").ToString())==0 %>' Width="80px"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Amount"
                                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��ע">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Remark" Width="320px" runat="server" Text='<%# Bind("SVM_ClassifyForcast_Detail_Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_SalesForcast" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_Save" EventName="Click" />
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">���ݴ����У����Ժ�...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
