<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PDT_StandardPrice.aspx.cs" Inherits="SubModule_Product_PDT_StandardPrice" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left" width="150">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="标准价表"></asp:Label></h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="查找" Width="60" />
                                    <asp:Button ID="btn_Add" runat="server" Text="新增价表" Width="60px" OnClick="btn_Add_Click" />
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
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chb_ToOrganizecityChild" runat="server" Text="包含子片区" />
                                </td>
                              </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="h3Row">
                            <tr>
                                <td height="28px">
                                    <h3>
                                        价表列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            Width="100%">
                            <Items>
                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="已生效价表"
                                    Value="0" Visible="True" />
                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="未生效价表"
                                    Value="1" Visible="True" />
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>--%>
                <tr class="tabForm">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PanelCode="Panel_PDT_StandardPrice" DataKeyNames="PDT_StandardPrice_ID" PageSize="50"
                                    Width="100%">
                                    <Columns>
                                        <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="PDT_StandardPrice_ID" DataNavigateUrlFormatString="PDT_StandardPriceDetail.aspx?PriceID={0}"
                                            ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px"></asp:HyperLinkField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("PDT_StandardPrice_TaskID", "../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                    Text="审批记录" Visible='<%# Eval("PDT_StandardPrice_TaskID").ToString()!="" %>'
                                                    ></asp:HyperLink>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="listViewTdLinkS1" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        无数据
                                    </EmptyDataTemplate>
                                </mcs:UC_GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Search" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
