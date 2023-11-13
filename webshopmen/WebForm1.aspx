<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="webshopmen.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="menu" runat="server" OnMenuItemClick="menu_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Home" Value="1" />
                <asp:MenuItem Text="About" Value="2" />
                <asp:MenuItem Text="Services" Value="3" />
                <asp:MenuItem Text="Contact" Value="4" />
            </Items>
        </asp:Menu>

        <div id="content" runat="server">
            <!-- Content will be displayed here -->
        </div>
    </form>
</body>
</html>
