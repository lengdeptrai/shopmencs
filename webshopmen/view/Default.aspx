<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webshopmen.view.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shop Men</title>
    <link href="../assets/css/main.css"/ rel ="stylesheet">
    <link href="../assets/css/responsiveMain.css" rel ="stylesheet"/>
    <link href="../assets/font/fontawesome-free-6.2.0-web/css/all.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header id="1">
                <div class="header_first">
                    <div class="logo">
                        <img  src="../assets/imgs/baner.jpg" alt="logo"/>
                    </div>
                    <ul class="list_item">
                        <li>
                            <a href="" class="link">
                                <i class="nav-icon fa-regular fa-bell"></i>
                                Thông Báo
                            </a>
                        </li>
                        <li>
                            <a href="" class="link ">
                                <i class="nav-icon fa-solid fa-circle-question"></i>
                                Trợ giúp
                            </a>
                        </li>
                        <li id="user">
                            <asp:HyperLink ID="btnLogin" NavigateUrl="~/view/Login.aspx" runat="server">LOGIN</asp:HyperLink>
                            <asp:LinkButton PostBackUrl="~/view/Account.aspx" ID="linkLogin" CssClass="link" runat="server" Visible="False">
                                <asp:Image ID="avatar" ImageUrl="../assets/imgs/user.png" CssClass="user" runat="server" />
                                <asp:Label ID="lbUser" runat="server"></asp:Label>
                            </asp:LinkButton>
                            <ul class="list_option-user" id="listOptionUser" runat="server">
                                <asp:ListView runat="server"  ID="listOption" OnItemCommand="listOption_ItemCommand">
                                    <ItemTemplate>
                                        <li runat="server">
                                            <asp:LinkButton runat="server" CssClass="link"  CommandName="ItemClick" CommandArgument='<%# Container.DisplayIndex.ToString()%>'  Text='<%# Container.DataItem %>' />
                                        </li>
                                    </ItemTemplate>
                                 </asp:ListView>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div class="header_second">
                    <div class="search">
                        <asp:TextBox ID="seach" CssClass="input" placeholder="Tìm kiếm sản phẩm" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="btnSeach" CssClass="btn_search" runat="server" PostBackUrl="#top_selling" OnClick="btnSeach_Click"> Tìm</asp:LinkButton>
                    </div>
                    <a href="Cart.aspx" class="cart link">
                        <i class="fa-solid fa-cart-shopping"></i>
                        <span>3</span>
                    </a>
                </div>
            </header>
            <nav>
                <div class="radio_img">
                    <span class="activated"></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
                <div class="slider">
                    <img src="../assets/imgs/img1.jpg" alt="">
                    <img src="../assets/imgs/img2.png" alt="">
                    <img src="../assets/imgs/img3.jpg" alt="">
                    <img src="../assets/imgs/img4.jpg" alt="">
                    <img src="../assets/imgs/img5.jpg" alt="">
                </div>
            </nav>
            <main>
                <!-- Danh muc -->
                <div class="cover_list_of_product" id="sale_off">
                    <span>DANH MỤC</span>
                    <asp:Menu ID="menu" runat="server" ForeColor="White"  OnMenuItemClick="menu_MenuItemClick" >
                        <Items>
                            <asp:MenuItem Value="1" ImageUrl="../assets/imgs/aothun.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="2" ImageUrl="../assets/imgs/quanau.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="3" ImageUrl="../assets/imgs/aokhoac.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="4" ImageUrl="../assets/imgs/quanlot.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="5" ImageUrl="../assets/imgs/aoda.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="6" ImageUrl="../assets/imgs/quanjean.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="7" ImageUrl="../assets/imgs/somi.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="8" ImageUrl="../assets/imgs/sweater.png" NavigateUrl="#top_selling"></asp:MenuItem>
                            <asp:MenuItem Value="9" ImageUrl="../assets/imgs/hoodie.png" NavigateUrl="#top_selling"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                    <input class="range" type="range" min="1" max="2" step="1" value="1">
                </div>
                <!-- List sale off -->
                <div class="sale_off" >
                    <!-- Dem nguoc sale off -->
                    <div class="countdown_sale_off">
                       <div class="countdown">
                        <span>SALE OFF</span>
                        <span class="hour_sale">00</span>
                        <span class="minute_sale">00</span>
                        <span class="second_sale">00</span>
                       </div>
                        <a href="#top_selling" class="link view_all_sale_off">
                            <span>Xem tất cả</span>
                            <i class="fa-solid fa-chevron-right"></i>
                        </a>
                    </div>
                    <!-- item sale off -->
                    <ul class="list_sale_off" >
                        <asp:ListView ID="listSaleOff" runat="server" OnItemCommand="listSaleOff_ItemCommand">
                        <ItemTemplate>
                            <li data-key='<%# Eval("productID") %>'>
                                <asp:LinkButton ID="link" CssClass="link item_sale_off" commandname="ViewProduct" commandargument='<%# Eval("productID") %>' runat="server">
                                    <div class="item_sale_off_img">
                                        <img src='<%# (Eval("imageURL") != DBNull.Value && !string.IsNullOrEmpty(Eval("imageURL").ToString())) ? Eval("imageURL") : "../assets/imgs/logo.png" %>' alt='<%# Eval("productName") %>' />
                                    </div>
                                    <div class="item_sale_off_content">
                                        <div class="item_sale_off_content-des">
                                            <%# Eval("productName") %>
                                        </div>
                                        <div class="item_sale_off_-prices">
                                            <span class="price-old"><%# Eval("priceOld", "{0:N0}đ") %></span>
                                            <span class="price-sale"><%# Eval("priceSale", "{0:N0}đ") %></span>
                                        </div>
                                    </div>
                                    <div class="item-sale">
                                        <span><%# Eval("discountPercentage") %>%</span>
                                        <span>GIẢM</span>
                                    </div>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                    </ul>
                    <input class="range" type="range" min="1" max="2" step="1" value="1">
                </div>
                <!-- Text Danh sách sản phẩm -->
                <div class="suggestions" id="top_selling">
                    <span>DANH SÁCH SẢN PHẨM</span>
                </div>
                <!-- List sản phẩm -->
                <div class="top_selling">
                    <ul class="list_top_selling">
                        <asp:ListView ID="listProduct" runat="server" OnItemCommand="listSaleOff_ItemCommand">
                            <ItemTemplate>
                                <li class="item_top_selling" data-key="<%# Eval("productID") %>">
                                    <div class="add_item" onclick= "alert('Chức năng đang trong quá trình hoàn thiện')">
                                        <span>Thêm vào giỏ hàng</span>
                                        <i class="fa-solid fa-plus"></i>
                                    </div>
                                    <asp:LinkButton ID="linkListProduct" CssClass="link link_item_top_selling"  commandname="ViewProduct" commandargument='<%# Eval("productID") %>'  runat="server">
                                        <div class="item_img">
                                            <img src="<%# (Eval("imageURL") != DBNull.Value && !string.IsNullOrEmpty(Eval("imageURL").ToString())) ? Eval("imageURL") : "../assets/imgs/logo.png" %>" alt="<%# Eval("productName") %>">
                                        </div>
                                        <div class="item_content">
                                            <div class="item_content_des">
                                                <%# Eval("productName") %> 
                                            </div>
                                            <div class="item_content_prices">
                                                <span class="price-old" style='display: <%# ((int)Eval("discountPercentage") > 0) ? "block" : "none" %>'><%# Eval("priceOld", "{0:N0}đ") %></span>
                                                <span class="price-sale"><%# Eval("priceSale", "{0:N0}đ") %></span>
                                            </div>
                                            <div class="item_content_review">
                                                <div class="review-vote">
                                                    <i class="fa-solid fa-star like"></i>
                                                    <i class="fa-solid fa-star like"></i>
                                                    <i class="fa-solid fa-star like"></i>
                                                    <i class="fa-solid fa-star like"></i>
                                                    <i class="fa-solid fa-star "></i>
                                                </div>
                                                <div class="review-buys">Đã bán 3,5k</div>
                                            </div>
                                            <div class="favouriteOrMall-item onFavourite">
                                                <i class="fa-solid fa-check"></i>
                                                <span class="favouriteItem">Yêu thích</span>
                                                <span class="mallItem">Mall</span>
                                            </div>
                                            <asp:Panel runat="server" ID="panelItem" CssClass="item-sale" Visible='<%# (int)Eval("discountPercentage")> 0 ? true : false %>' >
                                                <span><%# Eval("discountPercentage") %>%</span>
                                                <span>GIẢM</span>
                                            </asp:Panel>
                                        </div>
                                    </asp:LinkButton>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
                <ul class="listPage">
                    <li class="activeContainer" onclick="prevPage(maxPage)" >
                        <a href="#top_selling" class="link">PREV</a>
                    </li>
                    <li class="activeContainer" onclick="nextPage(maxPage)" >
                        <a href="#top_selling" class="link">NEXT</a>
                    </li>
                </ul>
            </main>
            <footer> 
                <div class="footer-list">
                    <div class="footer-list-item">
                        <ul class="customer-care-list">
                            <span>CHĂM SÓC KHÁCH HÀNG</span>
                            <li class="customer-care-list-item">
                                <a href="">Trung Tâm Trợ Giúp</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Shop Men's Blog</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Hướng Dẫn Mua Hàng</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Thanh Toán</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Trả Hàng & Hoàn Tiền</a>
                            </li>
                        </ul>
                    </div>
                    <div class="footer-list-item">
                        <ul class="customer-care-list">
                            <span>VỀ SHOP MEN'S</span>
                            <li class="customer-care-list-item">
                                <a href="">Giới Thiệu Về Shop Men's</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Tuyển Dụng</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Điều Khoản Shop Men's</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Chính Sách Bảo mật</a>
                            </li>
                            <li class="customer-care-list-item">
                                <a href="">Chính Hãng</a>
                            </li>
                        </ul>
                    </div>
                    <div class="footer-list-item">
                        <span>THANH TOÁN</span>
                        <div class="list-img-pay">
                            <img src="http://www.travelandtourismnews.com/wp-content/uploads/2008/07/logo-visa.jpg" alt="">
                            <img src="https://logos-download.com/wp-content/uploads/2016/03/Mastercard_Logo_1990-2048x1223.png " alt="">
                            <img src="" alt="">
                            <img src="" alt="">
                        </div>
                        <span>VẬN CHUYỂN</span>
                        <div class="list-img-pay">
                            <img src="https://www.hoteljob.vn/uploads/images/2021/08/09-11/111111111111.jpg" alt="">
                            <img src="https://tse2.mm.bing.net/th?id=OIP.Lp9AmkbLMgD4Lt0pDhiw3QHaCc&pid=Api&P=0g" alt="">
                            <img src="https://4g.viettel.vn/images/logo_vtpost.png" alt="">
                            <img src="https://www.biliranisland.com/wp-content/uploads/2019/04/JT-Express-Philippines.jpg" alt="">
                            <img src="https://static.mothership.sg/1/2020/12/grab-gojek-merger.png" alt="">
                        </div>
                    </div>
                    <div class="footer-list-item">
                        <ul class="customer-care-list">
                            <span>THEO DÕI CHÚNG TÔI TRÊN</span>
                            <li class="customer-care-list-item">
                                <i class="fa-brands fa-facebook"></i>
                                <a href="">Facebook</a>
                            </li>
                            <li class="customer-care-list-item">
                                <i class="fa-brands fa-instagram"></i>
                                <a href="">Instagram</a>
                            </li>
                            <li class="customer-care-list-item">
                                <i class="fa-brands fa-linkedin-in"></i>
                                <a href="">Linkedln</a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="footer_moile">
                    <div class="sale">
                        <a href="#sale_off" class="link">
                            <i class="fa-solid fa-bolt"></i>
                            <span>Sale</span>
                        </a>
                    </div>
                    <div class="live" onclick= "alert('Chức năng đang trong quá trình hoàn thiện !')">
                        <a href="" class="link">
                            <i class="fa-solid fa-video"></i>
                            <span>Live</span>
                        </a>
                    </div>
                    <div class="home activated">
                        <a href="#" class="link">
                            <i class="fa-solid fa-house "></i>
                            <span>Home</span>
                        </a>
                    </div>
                    <div class="notifiation">
                        <a href="" class="link">
                            <i class="fa-regular fa-bell"></i>
                            <span>Thông báo</span>
                        </a>
                    </div>
                    <div class="user">
                        <a href="Login.aspx" class="link">
                            <i class="fa-regular fa-user"></i>
                            <span>Tôi</span>
                        </a>
                    </div>
                </div>
            </footer>
        </div>
    </form>
    <script src="../assets/scripts/app.js"></script>
</body>
</html>
