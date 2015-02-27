<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select.aspx.cs" Inherits="SubModule_OA_PN_select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
      <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
        <tr>
            <td align="left">
            <table>
               <tr>
                   <td>   
                        <img height="16" src="../../../Images/icon/284.gif" width="16" alt="" />
                   </td>
                   <td>  
                       <h1>
                         公告热度</h1>
                   </td>
               </tr>
            </table>
              
            </td> 
        </tr>
         <tr align="center">
            <td><h2>
                阅读数：<asp:Label ID="lab_hasRead" runat="server"></asp:Label></h2>
            </td>
         </tr>
         <tr align="center">
            <td><h2>
                评论数：<asp:Label ID="lab_comment" runat="server"></asp:Label></h2>
            </td>
         </tr>
      </table>
    </center>
    </div>
    </form>
</body>
</html>
