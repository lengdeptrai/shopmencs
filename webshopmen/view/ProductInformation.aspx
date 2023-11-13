<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductInformation.aspx.cs" Inherits="webshopmen.view.ProductInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../assets/font/fontawesome-free-6.2.0-web/css/all.min.css" rel="stylesheet" />
    <link href="../assets/css/productInformation.css" rel="stylesheet"/>
    <link href="../assets/css/responsiveProductInformation.css" rel="stylesheet"/>
</head>
<body>
    <form id="form" runat="server">
        <header>
            <img src="../assets/imgs/baner.jpg" alt="" />
        </header>
        <main>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                <ContentTemplate>
                    <div class ="view-img">
                        <div class="imgMain">
                            <asp:Image ID="imgMain" runat="server" />
                        </div>
                        <div class="relatedProducts">
                            <ul class="listRelated">
                                <asp:ListView ID="listRelated" runat="server" OnItemCommand="listRelated_ItemCommand">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton CssClass="imgURL" runat="server" CommandName="Active" CommandArgument='<%# Container.DataItem %>'>
                                                <img src="<%# Container.DataItem %>" alt="" />
                                            </asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                 </asp:ListView>
                            </ul>
                            <div class="btn btnPrev">
                                <i class="fa-solid fa-angle-left"></i>
                            </div>
                            <div class="btn btnNext">
                                <i class="fa-solid fa-angle-right"></i>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="listRelated" EventName="ItemCommand" />
                </Triggers>
            </asp:UpdatePanel>
            <div class ="information">
                <div class="productName">
                    <div class="iconShop onLike">
                        <div class="isLike">
                            <i class="fa-solid fa-check"></i>
                            <span>Yêu thích</span>
                        </div>
                        <div class="isMail">
                            <span>Mail</span>
                        </div>
                    </div>
                    <asp:Label ID="productName" runat="server"></asp:Label>
                </div>
                <div class="productPrice">
                    <asp:Label ID="priceOld" runat="server" Text="120000"></asp:Label>
                    <asp:Label ID="priceSale" runat="server" Text="100000"></asp:Label>
                    <asp:Label ID="isSale" runat="server" Text="Giảm 10%"></asp:Label>
                </div>
                <div class="productColor">
                    <span>Màu</span>
                    <ul class="listColor">
                        <asp:ListView ID="listColor" runat="server">
                            <ItemTemplate>
                                <li class="color">
                                    <span><%# Container.DataItem %></span>
                                    <div class="icon">
                                        <i class="fa-regular fa-circle-check"></i>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
                <div class="productSize">
                    <span>Kích thước</span>
                    <ul class="listSize">
                        <asp:ListView ID="listSize" runat="server">
                            <ItemTemplate>
                                <li class="size">
                                    <span><%# Container.DataItem %></span>
                                    <div class="icon">
                                        <i class="fa-regular fa-circle-check"></i>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
                <div class="productQuantity">
                    <span>Số Lượng</span>
                    <div class="inputQuantity">
                        <asp:Button ID="reduce" runat="server" Text="-" />
                        <asp:TextBox ID="inputQuantity" runat="server" Text="1"></asp:TextBox>
                        <asp:Button ID="increase" runat="server" Text="+" />
                    </div>
                    <span runat="server" id ="hhh">123 sản phẩm có sẵn</span>
                </div>
                <div class="productBtn">
                    <div class="addCart">
                        <span>Thêm vào giỏ hàng</span>
                        <i class="fa-solid fa-cart-shopping"></i>
                    </div>
                    <asp:Button ID="buyNow" runat="server" Text="Mua ngay" />
                </div>
            </div>
        </main>
        <footer>
            <div class="description-text">
                <span>MÔ TẢ SẢN PHẨM</span>
            </div>
            <div class="description">
                <asp:Label ID="description" runat="server" Text="Label"></asp:Label>
            </div>
        </footer>
    </form>
</body>
</html>
