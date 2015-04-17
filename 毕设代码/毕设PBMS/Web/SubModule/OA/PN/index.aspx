<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="SubModule_OA_PN_index" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td>
                            <img height="16" src="../../../Images/icon/284.gif" width="16" alt="" />
                        </td>
                        <td>
                            <h2>
                                <asp:Label ID="lb_Title" runat="server" Text="��˾����"></asp:Label></h2>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
                �������ڷ�Χ��
                <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="70px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="���ڸ�ʽ����"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>��<asp:TextBox
                        ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="70px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="���ڸ�ʽ����"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                �ؼ��֣�<asp:TextBox ID="tbx_Search" Width="150" runat="server"></asp:TextBox>
                <asp:Button ID="btn_Search" OnClick="btn_Search_Click" runat="server" Width="60"
                    Text="����" />
                <asp:Button ID="writenewmail" runat="server" Visible="true" Width="65px" Text="�� ��"
                    OnClick="btnwritenewmail_Click"></asp:Button>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�����" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="δ���" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�ҷ�����" Description=""
                                    Value="2" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="ud_Notice" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="True" DataKeyNames="ID" OnPageIndexChanging="ud_Notice_PageIndexChanging"
                            Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0"
                            OnRowDeleting="ud_Notice_RowDeleting" OnSelectedIndexChanging="ud_Notice_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="״̬">
                                    <ItemTemplate>
                                        <%# (String)DataBinder.Eval(Container.DataItem, "[\"IsTop\"]") == "Y" ? "<img src='../../../Images/istop.jpg' title='�ö�����'/>" : ""%>
                                        <%#MCSFramework.BLL.OA.PN_HasReadStaffBLL.GetModelList("Notice=" + Eval("ID").ToString() + " AND Staff=" + Session["UserID"].ToString()).Count == 0 ? "<img src='../../../Images/hotfolder.gif' title='δ������'/>" : ""%>
                                        <%# (DateTime)DataBinder.Eval(Container.DataItem, "InsertTime") > DateTime.Today.AddDays(-3) ? "<img src='../../../Images/gif/gif-0304.gif' title='3���ڵĹ���'/>" : ""%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("ApproveFlag").ToString()=="1"? Eval("ID", "read.aspx?ID={0}"):Eval("ID", "NoticeDetail.aspx?ID={0}") %>'
                                            Target="_self" Text='<%# Eval("Topic") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                    <ItemStyle Width="400px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="InsertTime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                </asp:BoundField>
                                <asp:BoundField DataField="InsertStaff" HeaderText="������"></asp:BoundField>
                                <asp:BoundField DataField="KeyWord" HeaderText="�ؼ���"></asp:BoundField>
                                <asp:BoundField DataField="ApproveFlag" HeaderText="��˱�־"></asp:BoundField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClientClick="return confirm('�Ƿ�ȷ�ϳ�����˹���?')"
                                            CommandName="Select" Text="�������"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClientClick="return confirm('�Ƿ�ȷ��ɾ������?')"
                                            CommandName="Delete" Text="ɾ��"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                ������</EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
