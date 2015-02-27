<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="ClientPaymentDetail.aspx.cs" Inherits="SubModule_FNA_ClientPayment_ClientPaymentDetail" %>
<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    +<table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160" align="left">
                            <h2>
                                经销商回款记录</h2>
                        </td>
                        <td align="right" width="100%">
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保 存" Width="80px" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" 
                                Text="审核到账" Width="80px" />
                            <asp:Button ID="btn_CanclePass" runat="server"   
                                Text="取消审批到账" onclick="btn_CanclePass_Click"  />    
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_FNA_ClientPaymentDetail">
                </mcs:UC_DetailView>
            </td>
        </tr>
           <tr >
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="120" />
            </td>
        </tr>
    </table>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
