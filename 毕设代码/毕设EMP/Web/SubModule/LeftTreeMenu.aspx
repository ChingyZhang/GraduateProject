<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeftTreeMenu.aspx.cs" Inherits="SubModule_LeftTreeMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="tr_List" runat="server" Width="100%" ImageSet="Msdn"
            BackColor="White" ExpandDepth="0" EnableViewState="false">
            <NodeStyle CssClass="listViewTdLinkS1" NodeSpacing="1px" />
            <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
