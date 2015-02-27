<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Car_Car_DispatchRideDetail, App_Web_dk3o7pe1" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="车辆派车单详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Save" runat="server" Width="60px" Text="保 存" OnClick="bt_Save_Click" />
                            <asp:Button ID="bt_ApproveFlag" runat="server" Text="审核确认" OnClick="bt_ApproveFlag_Click"
                                OnClientClick="return confirm('确认审核?')" />
                            <asp:Button ID="bt_GoOut" runat="server" Text="车辆发出" OnClick="bt_GoOut_Click" OnClientClick="return confirm('确认车辆发出?')" />
                            <asp:Button ID="bt_GoBack" runat="server" Text="车辆驶回" OnClick="bt_GoBack_Click" OnClientClick="return confirm('确认车辆驶回?')" />
                            <asp:Button ID="bt_Cancel" runat="server" Text="取消派车" OnClick="bt_Cancel_Click" OnClientClick="return confirm('确认取消派车?')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_OA_Car_DispatchRide_Detail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
