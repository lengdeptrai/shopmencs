<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webshopmen.view.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/login.css" rel="stylesheet"/>
    <link href="../assets/css/responsiveLogin.css" rel="stylesheet"/>
    <link href ="../assets/font/fontawesome-free-6.2.0-web/css/all.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cover">
            <div class ="contentForm">
                <div ID ="login">
                    <h2>Đăng nhập</h2>
                    <asp:Label ID="statusLogin" runat="server" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtUsername" CssClass="inputLogin" runat="server" placeholder="Tài khoản"></asp:TextBox><br />
                    <asp:TextBox ID="txtPassword" CssClass="inputLogin" runat="server" TextMode="Password" placeholder="Mật khẩu"></asp:TextBox><br />
                    <asp:CheckBox ID="rememberUser" CssClass="radio" Text="Nhớ đăng nhập" runat="server" /><br />
                    <asp:Button ID="btnLoginGG" CssClass="btn btnGG" runat="server" Text="Tiếp tục với Googgle" OnClientClick="return false;"/><br />
                    <asp:Button ID="btnLoginFA" CssClass="btn btnFa" runat="server" Text="Tiếp tục với Facebook" OnClientClick="return false;" /><br />
                    <asp:Button ID="btnQR" CssClass="btn btnQR" runat="server" Text="QR" OnClientClick="startQRScanner(); return false;"/><br />
                    <asp:Button ID="btnLogin" CssClass="btn" runat="server" Text="Đăng nhập" OnClick="btnLogin_Click"/>
                </div>
                <div ID ="register">
                    <h2>Đăng ký</h2>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="statusRegister" runat="server" ForeColor="Red"></asp:Label>
                            <asp:TextBox ID="txtNewName" CssClass="inputLogin" runat="server" placeholder="Tên người dùng"></asp:TextBox><br />
                            <asp:TextBox ID="txtNewUserName" CssClass="inputLogin" runat="server" placeholder="Tài khoản"></asp:TextBox><br />
                            <asp:TextBox ID="txtNewPassword" CssClass="inputLogin" runat="server" TextMode="Password" placeholder="Mật khẩu"></asp:TextBox><br />
                            <asp:TextBox ID="txtCheckPassword" CssClass="inputLogin" runat="server" TextMode="Password" placeholder="Nhập lại mật khẩu"></asp:TextBox><br />
                            <asp:TextBox ID="txtEmail" CssClass="inputLogin" runat="server" placeholder="Email"></asp:TextBox><br />
                            <asp:Button ID="btnRegister" CssClass="btn" runat="server" Text="Đăng ký" OnClick="btnRegister_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="btnController">
                    <asp:Button ID="isLogin" CssClass="active btn" runat="server" Text="Đã có tài khoản" OnClientClick="return false;"/>
                    <asp:Button ID="isRegister" CssClass="btn" runat="server" Text="Chưa có tài khoản" OnClientClick="return false;"/>
                </div>
                <div id="scanner-container">
                    <video id="scanner-video"></video>
                    <button id="stop-button">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
            </div>
        </div>
    </form>
     <script src="https://cdn.rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>
    <script src="../assets/scripts/login.js" ></script>
</body>
</html>
