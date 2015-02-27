<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Product_PDT_StandardPriceChangeHistory, App_Web_zt5rq-tu" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调整产品请购申请数量</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            价盘调整记录
                                        </h2>
                                    </td>
                                    <td align="right">
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabDetailView">
                            <mcs:UC_GridView AutoGenerateColumns="false" ID="gv_List" runat="server" PageSize="20" AllowPaging="true" Width="100%" PanelCode="StandardPriceChangeHistory">
                                <Columns>
                                    <asp:TemplateField HeaderText="变动类型">
                                        <ItemTemplate>
                                            <%#showChangeType((int)Eval("PDT_StandPriceChangeHistory_ChangeType"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
