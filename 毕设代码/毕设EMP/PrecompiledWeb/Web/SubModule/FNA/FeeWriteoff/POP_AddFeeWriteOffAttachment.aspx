<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_POP_AddFeeWriteOffAttachment, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>费用核销明细附件</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
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
                                    编辑费用核销申请明细
                                </h2>
                            </td>
                            <td align="right">
                                <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存并返回" Width="80px" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tabForm">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="dataLabel">
                                申请单备案号：
                            </td>
                            <td class="dataField">
                                <asp:Label ID="lbl_applyCode" Text="" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td class="dataLabel">
                                发生客户(门店或经销商)：
                            </td>
                            <td class="dataField">
                                <asp:Label ID="lbl_Client" Text="" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td class="dataLabel">
                                申请金额</td>
                            <td class="dataField">
                                <asp:Label ID="lb_AvailCost" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <mcs:UC_DetailView ID="pn_Detail" runat="server" DetailViewCode="Page_FNA_FeeWriteOffDetail_Invoice">
                            </mcs:UC_DetailView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="102" CanSetDefaultImage="false" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
