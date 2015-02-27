<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_BBS_BoardMasterManage, App_Web_3-csmuv_" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                <tr>
                 <td>
                  <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td background="../../../Images/bbsback.jpg">
                            <img src="../../../Images/bbs.jpg">
                        </td>
                        <td id="Td2" background="../../../Images/bbsback.jpg" runat="server" visible="true">
                          
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
                           <h2>论坛成员管理</h2>
                        </td>
                         <td align="right">
                            <asp:Button ID="bt_Back" runat="server" Width="60px" Text="主页面" OnClick="bt_Back_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                         </td>
                     </tr>
                </table>
             </td>
          </tr>
          <tr>
             <td >
                 <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse"
                            bordercolor="#111111" width="517" height="240" id="Table1" class="tabForm">
                            <tr class="h3Row">
                                <td align="left" nowrap height="25">
                                     <h2>设定版主</h2>
                                </td>
                            </tr>
                            <tr class="h3Row">
                              <td align="center" class="tabForm">
                                <table  border="0" cellpadding="0" width="600"  cellspacing="0" >
                                    <tr id="isPublic" runat="server">
                                      <td></td>
                                      <td></td>
                                          <td align="left">
                                         
                                                成员选择
                                               <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                            ParentColumnName="SuperID" AutoPostBack="True"  onselected="tr_Position_Selected" Width="200" />    
                                            <asp:CheckBox ID="cb_IncludeChild" runat="server" Checked="true" Text="包含下级职位" />
                                         
                                           </td>
                                        </tr>
                                   <tr>
                                       <td  height="240" valign="top" rowspan="4" nowrap >
                                        版 主:
                                        <asp:ListBox ID="lbBoardMasterList" runat="server" Height="218px" Width="230px" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                    <td width="50" height="86" nowrap >
                                    </td>
                                    <td width="400" height="240" valign="top" align="left" rowspan="4" nowrap>
                                               成 员:
                                            <asp:ListBox ID="lbRemainStaff" runat="server"  Height="218px" Width="240px" 
                                                   SelectionMode="Multiple">
                                            </asp:ListBox>
                                    </td>
                                   </tr>
                                   <tr>
                                        <td width="50" height="10" align="middle" nowrap>
                                            <asp:Button ID="btn_in" runat="server" Text="<<<"  OnClick="btn_in_Click" Width="57px">
                                            </asp:Button></td>
                                    </tr>
                                    <tr>
                                        <td width="50" height="35" align="middle" nowrap>
                                            <asp:Button ID="btn_out" runat="server" Text=">>>"  OnClick="btn_out_Click" Width="55px">
                                            </asp:Button></td>
                                    </tr>
                                    <tr>
                                        <td width="50" height="100" nowrap>
                                        </td>
                                   </tr>
                                </table>
                              </td>
                                
                            </tr>
                            
                        </table>
             </td>
          </tr>
        </table>
       </ContentTemplate>
     <Triggers>
        <asp:AsyncPostBackTrigger ControlID="tr_Position" EventName="Selected" />
        <asp:AsyncPostBackTrigger ControlID="btn_in" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btn_out" EventName="Click" />
     </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
