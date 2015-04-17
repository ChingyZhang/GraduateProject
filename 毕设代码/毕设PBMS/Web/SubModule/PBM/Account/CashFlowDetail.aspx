<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="CashFlowDetail.aspx.cs" Inherits="SubModule_PBM_Account_CashFlowDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>
				<table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
					<tr>
						<td width="24">
							<img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
						<td nowrap="noWrap" style="width: 180px">
							<h2><asp:Label ID="lb_PageTitle" runat="server" Text="收付款单详细信息"></asp:Label></h2>
						</td>
						<td align="right">
							<asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click"/>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PBM_AC_CashFlowDetail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

