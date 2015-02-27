<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PublishDetail.aspx.cs" Inherits="SubModule_Logistics_Publish_PublishDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td width="24">
                                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td nowrap="noWrap">
                                                <h2>
                                                    <asp:Label ID="lb_PageTitle" runat="server" Text="请购目录发布"></asp:Label></h2>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_Save" runat="server" Text="保 存" Width="60px" OnClick="btn_Save_Click" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="60px" OnClick="btn_Delete_Click"
                                                    OnClientClick="return confirm(&quot;是否确认要删除该发布请购目录?&quot;)" CausesValidation="False" />
                                                <asp:Button ID="bt_Publish" runat="server" CausesValidation="False" OnClick="bt_Publish_Click"
                                                    OnClientClick="return confirm(&quot;是否确认开启发布？开启发布后产品目录将不可再更改！&quot;);" Text="确认发布"
                                                    ToolTip="开始允许请购" Width="60px" />
                                                <asp:Button ID="bt_Modify" runat="server" CausesValidation="False" Text="修改发布" ToolTip="重新修改发布"
                                                    Width="60px" OnClick="bt_Modify_Click" OnClientClick="return confirm(&quot;是否确认修改已确认的发布单？修改后的发布信息不会影响之前已成功申请的请购单！&quot;);" />
                                                <asp:Button ID="bt_Close" runat="server" CausesValidation="False" OnClientClick="return confirm(&quot;是否确定关闭发布？已申请的请购单保持有效。&quot;)"
                                                    Text="关闭发布" ToolTip="停止请购申请,已请购申请的保持有效" Width="60px" OnClick="bt_Close_Click" />
                                                <asp:Button ID="bt_Cancel" runat="server" CausesValidation="False" OnClientClick="return confirm(&quot;是否确定停止请购申请，并取消所有已申请的请购单？&quot;)"
                                                    Text="取消发布" ToolTip="停止请购申请，已申请的请购全部取消" Width="60px" Visible="False" OnClick="bt_Cancel_Click" />
                                                <asp:Button ID="bt_ViewApplyList" runat="server" Text="查看请购列表" OnClick="bt_ViewApplyList_Click" />
                                                <asp:Button ID="bt_Copy" runat="server" Text="复制目录" Width="60px" OnClick="bt_Copy_Click" />
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_DetailView ID="pl_ApplyPublish" runat="server" DetailViewCode="DV_ORD_ApplyPublish_Detail">
                                    </mcs:UC_DetailView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="28px" runat="server"
                                                id="tbl_publish">
                                                <tr>
                                                    <td valign="bottom">
                                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                                            Width="100%">
                                                            <Items>
                                                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="发布目录"
                                                                    Value="0" Visible="True" />
                                                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="非发布目录"
                                                                    Value="1" Visible="True" />
                                                            </Items>
                                                        </mcs:MCSTabControl>
                                                    </td>
                                                    <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #999999">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="480px" id="tbl_Find" visible="false"
                                                            runat="server">
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                                        OnSelectedIndexChanged="rbl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                        AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                                                        RepeatDirection="Horizontal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    赠品关键字:<asp:TextBox ID="tbx_ProductText" runat="server" Width="60px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="bt_Find" runat="server" Text="查 找" OnClick="bt_Find_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #999999">
                                                        <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                                            Text="全选" />
                                                        <asp:Button ID="bt_In" runat="server" OnClick="bt_In_Click" Text="加入发布目录" Visible="False" />
                                                        <asp:Button ID="bt_Out" runat="server" OnClick="bt_Out_Click" Text="移出发布目录" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="tabForm" runat="server" id="tr_List">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" 
                                        AutoGenerateColumns="False"   PageSize="30"
                                        DataKeyNames="ID,Product" AllowPaging="True" onpageindexchanging="gv_List_PageIndexChanging" 
                                        >
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server" Enabled="<%#CanEnabled %>"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="品项编码">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="品项名称">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ProductName" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"FullName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="规格">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Spec" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"Spec") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="包装<br/>系数">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ConvertFactor" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"ConvertFactor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Price" HeaderText="单个<br/>价格" HtmlEncode="false" DataFormatString="{0:0.##}"  />
                                            <asp:TemplateField HeaderText="整箱<br/>价格">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_CasePrice" runat="server" Text='<%# (int.Parse(GetProductInfo((int)Eval("Product"),"ConvertFactor"))*(decimal)Eval("Price")).ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="合计可供<br/>请购数量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_AvailableQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AvailableQuantity"))).ToString() %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                    <asp:Label ID="lb_Packaging_T1" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_AvailableQuantity_T"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"
                                                        SetFocusOnError="True"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_AvailableQuantity_T"
                                                        Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbx_AvailableQuantity_T"
                                                        Display="Dynamic" ErrorMessage="必须大于或等于0" MinimumValue="0" SetFocusOnError="True"
                                                        MaximumValue="1000000" Type="Integer"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单次最少<br/>请购量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_MinQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"MinQuantity"))).ToString() %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                    <asp:Label ID="lb_Packaging_T2" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'>
                                                    </asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_MinQuantity_T"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_MinQuantity_T"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbx_MaxQuantity_T"
                                                        Display="Dynamic" ErrorMessage="必须大于或等于0" MinimumValue="0" MaximumValue="1000000"
                                                        SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单次最大<br/>请购量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_MaxQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"MaxQuantity"))).ToString() %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                    <asp:Label ID="lb_Packaging_T3" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_MaxQuantity_T"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_MaxQuantity_T"
                                                        Display="Dynamic" ErrorMessage="不能为空">
                                                    </asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator3" runat="server"
                                                        ControlToValidate="tbx_MaxQuantity_T" Display="Dynamic" ErrorMessage="必须大于或等于0"
                                                        MinimumValue="0" MaximumValue="1000000" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="赠送坎级">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_GiveLevel" runat="server" Width="120px" Text='<%# DataBinder.Eval(Container,"DataItem[\"GiveLevel\"]") %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="备注说明">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="200px" Text='<%# Bind("Remark") %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="已请购总量">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubmitQuantity" runat="server" Font-Bold="true" ForeColor="Red"
                                                        Text='<%# GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),GetSubmitQuantity((int)DataBinder.Eval(Container,"DataItem.Product"))).ToString()%>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging_T5" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                    <font color="red">*【可供请购数量】为0时，即不限制所有片区同时最多的请购数量，【单次最大请购量】为0时，即不限制单次最大的请购数量。<br />
                                    </font>
                                </td>
                            </tr>
                            <tr class="tabForm" runat="server" id="tr_NotInList">
                                <td>
                                    <mcs:UC_GridView ID="gv_NotInList" runat="server" Width="100%" AutoGenerateColumns="False" 
                                        DataKeyNames="ID" AllowPaging="true" PageSize="30" OnPageIndexChanging="gv_NotInList_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Code" HeaderText="代码" />
                                            <asp:BoundField DataField="Brand" HeaderText="品牌" />
                                            <asp:BoundField DataField="Classify" HeaderText="分类" />
                                            <asp:BoundField DataField="Fullname" HeaderText="产品" />
                                        </Columns>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
