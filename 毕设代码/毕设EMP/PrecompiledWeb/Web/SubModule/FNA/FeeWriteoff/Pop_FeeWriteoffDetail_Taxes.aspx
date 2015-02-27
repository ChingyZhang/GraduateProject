<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_Pop_FeeWriteoffDetail_Taxes, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>调整费用核销批复金额</title>
    <base target="_self">
    </base>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="500px">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            调整税金
                                        </h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="确认调整" Width="80px" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabDetailView">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="tabDetailViewDL">
                                        费用科目
                                    </td>
                                    <td class="tabDetailViewDF">
                                        税金
                                    </td>
                                </tr>                               
                               
                                <tr>
                                    <td class="tabDetailViewDL">
                                       税金金额
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_Taxes" runat="server"></asp:TextBox>
                                        <span class="style1">元*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_Taxes" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Taxes"
                                            Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                  
                                <tr>
                                    <td class="tabDetailViewDL">
                                        备注
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_Remark" runat="server" Width="300px"></asp:TextBox>                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
