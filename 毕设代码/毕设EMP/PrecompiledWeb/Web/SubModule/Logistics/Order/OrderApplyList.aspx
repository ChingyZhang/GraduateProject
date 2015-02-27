<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_ORD_OrderApplyList, App_Web_aozhikkk" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upl_Order" runat="server">
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="���������б��ѯ"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="��������" OnClick="bt_Add_Click" Width="80px" />
                                    <asp:Button ID="bt_SpecialAdd" runat="server" Text="��������" OnClick="bt_Add_Click" Width="80px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_Condition_title"
                            runat="server">
                            <tr>
                                <td style="height: 28px">
                                    <h3>
                                        ��ѯ����</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tbl_Condition"
                            runat="server">
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
                                    <asp:DropDownList ID="ddl_Type" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ��Ʒ����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ProductType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    Ʒ��
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ����״̬
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ���뵥��
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SheetCode" runat="server" Width="140px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="����" Width="60px" />
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
                                        �����б�</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            PanelCode="Panel_LGS_OrderApplyList_001" DataKeyNames="ORD_OrderApply_ID" AllowPaging="True"
                            PageSize="25">
                            <Columns>
                                <asp:TemplateField HeaderText="���뵥��">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# GetUrl((int)DataBinder.Eval(Container,"DataItem.ORD_OrderApply_ID")) %>'
                                             Text='<%# Eval("ORD_OrderApply_SheetCode") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�ܽ��">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.ORD_OrderApply_ID").ToString())).ToString("0.###") %>'></asp:Label>Ԫ
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ORD_OrderApply_TaskID", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                            Text="������¼" Visible='<%# Eval("ORD_OrderApply_TaskID").ToString()!="" %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <%--<asp:HyperLinkField DataNavigateUrlFields="ORD_OrderApply_ID" DataNavigateUrlFormatString="OrderDeliveryList.aspx?ApplyID={0}"
                            Text="���ż�¼" ControlStyle-CssClass="listViewTdLinkS1"  Visible="false">
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:HyperLinkField>--%>
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
