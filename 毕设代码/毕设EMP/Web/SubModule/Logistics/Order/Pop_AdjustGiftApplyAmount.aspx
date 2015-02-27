<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_AdjustGiftApplyAmount.aspx.cs"
    Inherits="SubModule_Logistics_Order_Pop_AdjustGiftApplyAmount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调整赠品预算</title>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                            调整赠品预算
                                        </h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存并返回" Width="80px"  OnClientClick="confirm('请确认是否保存调整!')" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <mcs:UC_DetailView ID="pn_Detail" runat="server" DetailViewCode="Page_GiftApplyAmountDetail">
                                    </mcs:UC_DetailView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>                   
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
