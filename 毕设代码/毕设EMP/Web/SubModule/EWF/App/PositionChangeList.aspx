<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PositionChangeList.aspx.cs" Inherits="SubModule_EWF_App_PositionChangeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                职位变更列表</h2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="HR_StaffPosition_Change_ID" PanelCode="Panel_EWF_App_PositionChangeList">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="HR_StaffPosition_Change_TaskID" DataNavigateUrlFormatString="../TaskDetail.aspx?TaskID={0}"
                            Text="查看详细" ControlStyle-CssClass="listViewTdLinkS1" />
                    </Columns>
                </mcs:UC_GridView>
                <%--</ContentTemplate>
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>--%>
                <%--</asp:UpdatePanel>--%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
