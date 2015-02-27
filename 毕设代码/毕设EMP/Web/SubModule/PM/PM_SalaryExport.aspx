<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="PM_SalaryExport.aspx.cs" Inherits="SubModule_PM_PM_SalaryExport" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div>
        <p style='font-size:20px; color:red;'>请选择具体营业部后分别导出工行和农行工资文件</p>
        </div>
    
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td class="dataLabel">
                    营业部
                </td>
                <td class="style1">
                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                        ParentColumnName="SuperID" Width="180px" />
                </td>
                <td class="dataLabel">
                    会计月
                </td>
                <td class="dataField">
                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="农行Excel工资单" Width="130px" oncommand="btn_Command" CommandName="ABCExcel" />
                    <asp:Button ID="btnABC" runat="server" Text="农行Txt工资单" Width="130px" oncommand="btn_Command" CommandName="ABC" />
                    <asp:Button ID="btnCCB" runat="server" Text="建行工资单" Width="130px" oncommand="btn_Command" CommandName="CCB" />
                </td>
            </tr>
        </table>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    </asp:Content>

