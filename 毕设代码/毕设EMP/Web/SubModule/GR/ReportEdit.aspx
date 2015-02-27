<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="ReportEdit.aspx.cs" Inherits="SubModule_GR_ReportEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../CreateControl.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript">
        var Report = "<%=GetGrPath()%>";
        if (Report != null && Report != "")
            CreateDesignerEx("100%", "100%", Report, "DesignReportSave.aspx?Report=<%=Request.QueryString["Report"]%>", Data,
	        "<param name=OnSaveReport value=OnSaveReport>");
    </script>

</body>
</html>
