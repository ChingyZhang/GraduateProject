<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_Search_Product.aspx.cs"
    Inherits="SubModule_Product_Pop_Search_Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查找产品</title>
    <base target="_self" />

    <script language="javascript">
        function ReturnValue() {
            window.returnValue = document.all["tbx_SelectedProductID"].value + "|" + document.all["tbx_SelectedProductName"].value;
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <input onclick="ReturnValue()" type="button" value="确定" id="Button3" class="button"
                style="width: 50px" size="" />
            <input onclick="window.close()" type="button" value="取消" class="button" style="width: 50px"
                size="" /><br />
            <asp:TreeView ID="tr_Product" runat="server" Width="95%" ImageSet="Msdn" BorderWidth="1px"
                ExpandDepth="1" OnSelectedNodeChanged="tr_Product_SelectedNodeChanged">
                <SelectedNodeStyle CssClass="listViewHRS1" />
                <NodeStyle CssClass="listViewTdLinkS1" />
            </asp:TreeView>
            <div style="display:none">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="tbx_SelectedProductName" runat="server" Enabled="False" Width="10px"></asp:TextBox>
                        <asp:TextBox ID="tbx_SelectedProductID" runat="server" Enabled="False" Width="10px"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tr_Product" EventName="SelectedNodeChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
