<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="CatalogManage.aspx.cs" Inherits="SubModule_OA_BBS_CatalogManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                <tr>
                 <td>
                  <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td background="../../../Images/bbsback.jpg">
                            <img src="../../../Images/bbs.jpg">
                        </td>
                        <td id="Td1" background="../../../Images/bbsback.jpg" runat="server" visible="true">
                          
                                 <asp:Button ID="bt_Search" runat="server" Visible="true" Text="搜索" OnClick="bt_Search_Click">
                                 </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
               </td>
          </tr>
          <tr>
             <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" >
                    <tr>
                        <td align="left" style="width:20px">
                            <img   src="../../../Images/icon/057.gif" alt="" style="width: 16px; height:16px" />
                        </td>
                        <td align="left">
                           <h2>分类管理</h2>
                        </td>
                         <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="添加" OnClick="bt_OK_Click" />  &nbsp;
                            <asp:Button ID="bt_Back" runat="server" Width="60px" Text="主页面" OnClick="bt_Back_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                         </td>
                     </tr>
                </table>
             </td>
          </tr>
          <tr>
            <td>
                <mcs:UC_DetailView ID="UC_Catalog" runat="server" DetailViewCode="Page_BBS_CatalogDetail" 
                    Visible="true">
                </mcs:UC_DetailView>

            </td>
            </tr>
          </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

