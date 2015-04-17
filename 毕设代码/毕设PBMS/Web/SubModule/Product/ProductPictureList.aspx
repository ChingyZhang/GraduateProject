<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ProductPictureList.aspx.cs" Inherits="SubModule_Product_ProductPictureList" %>

<%@ Register Src="../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="产品附件及图片列表"></asp:Label>
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查  找" Width="60px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="1">
                    <Items>
                        <mcs:MCSTabItem Text="详细资料" />
                        <mcs:MCSTabItem Text="图片附件" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                查询条件</h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                产品
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl runat="server" ID="select_ProductCode" PageUrl="Pop_Search_Product.aspx"
                                                    Width="200px" OnSelectChange="select_ProductCode_SelectChange" />
                                            </td>
                                            <td class="dataLabel">
                                                产品名称
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_ProductName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="11" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
