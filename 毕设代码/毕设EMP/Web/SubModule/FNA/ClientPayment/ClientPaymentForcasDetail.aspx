<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="ClientPaymentForcasDetail.aspx.cs" Inherits="SubModule_FNA_ClientPayment_ClientPaymentForcasDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160" align="left">
                            <h2>
                                经销商回款预估</h2>
                        </td>
                        <td align="right" width="100%">
                            <asp:Button ID="bt_Save" runat="server"   Text="保 存" Width="80px" 
                                onclick="bt_Save_Click" />
                            <asp:Button ID="bt_Approve" runat="server" 
                                Text="审核" Width="80px" onclick="bt_Approve_Click" />
                                   <asp:Button ID="btn_CancleApprove" runat="server" Text="取消审核" 
                                onclick="btn_CancleApprove_Click"   />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_FNA_ClientPaymentForcastDetail">
                </mcs:UC_DetailView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

