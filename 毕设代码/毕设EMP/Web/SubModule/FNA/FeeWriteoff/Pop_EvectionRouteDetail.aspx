<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_EvectionRouteDetail.aspx.cs"
    Inherits="SubModule_FNA_FeeWriteoff_Pop_EvectionRouteDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head id="Head1" runat="server">
    <title>差旅行程详细信息</title>
    <base target="_self">
    </base>

    <script src="../../../App_Themes/basic/meizzDate.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                        <tr>
                            <td width="24">
                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                            </td>
                            <td nowrap="noWrap">
                                <h2>
                                    差旅行程详细信息
                                </h2>
                            </td>
                            <td align="right">
                                <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click" />
                                <asp:Button ID="bt_Delete" runat="server" OnClientClick="return confirm(&quot;删除后将数据将不可恢复，是否确认删除?&quot;)"
                                    Text="删 除" Width="60px" OnClick="bt_Delete_Click" />
                                <asp:Button ID="bt_ToJournal" runat="server" Text="转工作日志" Width="60px" OnClick="bt_ToJournal_Click"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <mcs:UC_DetailView ID="pn_detail" runat="server" DetailViewCode="Page_FNA_EvectionRouteDetail">
                            </mcs:UC_DetailView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="color: Red">
                    注：如果是总部或省区组织的培训产生的差旅费用，请先填报工作日志，在日志类型中选择相应的培训类型，再从日志页面里切换到该页面填写相应的差旅行程!
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html> 