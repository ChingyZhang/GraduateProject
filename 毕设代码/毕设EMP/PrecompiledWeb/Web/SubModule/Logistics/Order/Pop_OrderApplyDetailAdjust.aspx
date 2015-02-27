<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Logistics_Order_Pop_OrderApplyDetailAdjust, App_Web_b5n4-ayh" enableEventValidation="false" stylesheettheme="basic" %>

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
                                            调整产品请购申请数量
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
                                        请购品项
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_Product" runat="server" Text=""></asp:Label>
                                        <br />
                                        <asp:Image ID="img_Product" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        请购数量
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:Label ID="lb_BookQuantity_T" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lb_Package1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        批复数量
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_ApproveQuantity_T" runat="server" Text=""></asp:TextBox>
                                        <asp:Label ID="lb_Package2" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_ApproveQuantity_T"
                                            Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApproveQuantity_T"
                                            Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabDetailViewDL">
                                        扣减原因
                                    </td>
                                    <td class="tabDetailViewDF">
                                        <asp:TextBox ID="tbx_AdjustReason" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="tbx_AdjustReason" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
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
