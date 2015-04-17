<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeftTreeMenu.aspx.cs" Inherits="SubModule_LeftTreeMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1_AC" runat="server" RenderMode="Block" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Accordion ID="Ac_MainMenu" runat="server" SelectedIndex="0"
                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                        ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40"
                        TransitionDuration="250" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    </asp:Accordion>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
