<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" stylesheettheme="basic" autoeventwireup="true" inherits="SubModule_Product_ManufacturerManage, App_Web_r43usu2p" enableEventValidation="false" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                        生产基地维护
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="添加" OnClick="bt_OK_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="Page_PDT_003">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row" height="30px">
                            <tr>
                                <td align="left" width="150">
                                    <h3>
                                        生产基地列表</h3>
                                </td>
                                <td>
                                    生产基地名称：<asp:TextBox ID="tbx_Search" Width="120" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button1" OnClick="btn_Search_Click" runat="server" Width="60" Text="查找" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_SelectAll" runat="server" Text="全 选" OnClick="btn_SelectAll_Click" />
                                    <asp:Button ID="btn_SelectBack" runat="server" Text="反 选" OnClick="btn_SelectBack_Click" />
                                    <asp:Button ID="btn_Delete" runat="server" Text="删除基地" OnClick="btn_Delete_Click"
                                        OnClientClick="javascript:return confirm('确定删除选中的记录？');" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_PDT_List_004" PageSize="10"
                            PageIndex="0" AutoGenerateColumns="false" Width="100%" OnSelectedIndexChanging="ud_grid_SelectedIndexChanging"
                            DataKeyNames="PDT_Manufacturer_ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" />
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
