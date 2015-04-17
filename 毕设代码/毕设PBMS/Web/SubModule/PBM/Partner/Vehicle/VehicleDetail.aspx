<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="VehicleDetail.aspx.cs" Inherits="SubModule_PBM_Partner_Vehicle_VehicleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="车辆详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审核" Width="60px" />
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_TDP_Vehicle_Detail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table class="h3Row" height="28" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <h3>跟车销售送货员工</h3>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="cbx_Staff" runat="server" DataTextField="RealName" DataValueField="ID" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="5"></asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: red">注：货用车的车辆，在审核后会自动创建该车辆对应关联的车仓库！</span>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
