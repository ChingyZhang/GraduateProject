<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PM_PromotorDetail_Print.aspx.cs" Inherits="SubModule_PM_PM_PromotorDetail_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
    </style>
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div>
     <table cellspacing="0" cellpadding="0" width="100%" border="0">
      <tr>
                <td align="center">
                    <asp:Label ID="lb_Header" runat="server" CssClass="style1"></asp:Label>
                    <span class="style1">导购员明细</span>
                </td>
            </tr>
        <tr>
            <td>              
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="Page_PM_002_Print">
                        </mcs:UC_DetailView>                   
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbl_Promotor" cellspacing="0" cellpadding="0" width="100%" border="0"
                    runat="server">                 
                    <tr>
                    <td nowrap>
                                        <h3>
                                            导购员所属门店列表
                                        </h3>
                                    </td>
                                    </tr>
                                    <tr>
                        <td>                          
                                    <mcs:UC_GridView ID="gv_list" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="PM_PromotorInRetailer_ID" PanelCode="Panel_PromotorInRetail_List_002"
                                        Binded="False" ConditionString="" TotalRecordCount="0" OrderFields="" >
                                        <Columns>
                                            <asp:BoundField DataField="CM_Client_FullName" HeaderText="客户全称" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
                                    </mcs:UC_GridView>                             
                        </td>
                    </tr>
                 
                </table>
    </table>
    </div>
    </form>
</body>
</html>
