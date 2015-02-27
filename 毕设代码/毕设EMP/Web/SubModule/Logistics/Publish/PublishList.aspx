<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PublishList.aspx.cs" Inherits="SubModule_Logistics_Publish_PublishList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="产品请购发布列表"></asp:Label>
                            </h2>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="dataLabel">
                                        开始月
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        截止月
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="bt_Find" runat="server" Width="60px" Text="查找" OnClick="bt_Find_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add1" runat="server" Text="新增成品发布" Width="100px" OnClick="bt_Add1_Click" />
                            <asp:Button ID="bt_Add2" runat="server" Text="新促销品发布" Width="100px" OnClick="bt_Add2_Click" />
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
                                请购发布列表</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    PanelCode="Panel_ORD_ApplyPublish_List_01" DataKeyNames="ORD_ApplyPublish_ID"
                    AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="ORD_ApplyPublish_ID" DataNavigateUrlFormatString="PublishDetail.aspx?ID={0}"
                            Text="详细资料" ControlStyle-CssClass="listViewTdLinkS1"></asp:HyperLinkField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
