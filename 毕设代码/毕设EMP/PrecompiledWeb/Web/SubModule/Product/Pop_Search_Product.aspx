<%@ page language="C#" autoeventwireup="true" inherits="SubModule_Product_Pop_Search_Product, App_Web_r43usu2p" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查找产品</title>
    <base target="_self" />

    <script language="javascript">
        function ReturnValue() {
            window.returnValue = document.all["tbx_SelectedProductID"].value + "|" + document.all["tbx_SelectedProductName"].value;
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        产品名称
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:DropDownList ID="dl_brand" runat="server" DataTextField="name" 
                    DataValueField="id" AutoPostBack="true" 
                    onselectedindexchanged="dl_brand_SelectedIndexChanged"></asp:DropDownList>
                     <asp:DropDownList ID="dl_classify" runat="server" DataTextField="name" 
                    DataValueField="id" AutoPostBack="true" onselectedindexchanged="dl_classify_SelectedIndexChanged" ></asp:DropDownList>
                    <asp:DropDownList ID="dl_packaging" runat="server" DataTextField="name" 
                    DataValueField="id" AutoPostBack="true" 
                    onselectedindexchanged="dl_packaging_SelectedIndexChanged"></asp:DropDownList>
                  <asp:DropDownList ID="dl_product" runat="server" DataTextField="fullname" 
                    DataValueField="id" AutoPostBack="true" 
                    onselectedindexchanged="dl_product_SelectedIndexChanged"  ></asp:DropDownList>
                <asp:TextBox ID="tbx_SelectedProductName" runat="server" Enabled="False" Width="150px">
                </asp:TextBox>
                <asp:TextBox ID="tbx_SelectedProductID" runat="server" Enabled="False" Width="20px">
                </asp:TextBox></ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tr_Product" EventName="SelectedNodeChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <input onclick="ReturnValue()" type="button" value="确定" id="Button3" class="button"
            style="width: 50px" size="" />
        <input onclick="window.close()" type="button" value="取消" class="button" style="width: 50px"
            size="" /><br />
        <asp:TreeView ID="tr_Product" runat="server" Width="95%" ImageSet="Msdn" BorderWidth="1px"
            ExpandDepth="1" onselectednodechanged="tr_Product_SelectedNodeChanged">
            <SelectedNodeStyle CssClass="listViewHRS1" />
            <NodeStyle CssClass="listViewTdLinkS1" />
        </asp:TreeView>
    </div>
    </form>
</body>
</html>

