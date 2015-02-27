<%@ page language="C#" autoeventwireup="true" inherits="SubModule_CM_Pop_PDTChangeHistory, App_Web_zt5rq-tu" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="../../Controls/InfoChangeHistory.ascx" TagName="InfoChangeHistory" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <uc1:InfoChangeHistory ID="ich_Product" runat="server" TableName="MCS_Pub.dbo.PDT_Product" />
    </div>
    </form>
</body>
</html>