<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="webshopmen.view.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/admin.css"/ rel="stylesheet">
</head>
<body>
    <form id="form" runat="server">
        <nav>
            <ul>
                <li class="active">
                    <asp:LinkButton ID="productManagement" runat="server">Quản lý sản phẩm</asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="orderManagement" runat="server">Quản lý đơn hàng</asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="userManagement" runat="server">Quản lý người dùng</asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="Report" runat="server">Báo cáo</asp:LinkButton>
                </li>
            </ul>
        </nav>
        <main id="tableProduct">
            <div id="headerTableProduct">
                <ul>
                    <li>ID</li>
                    <li>Name</li>
                    <li>Price Old</li>
                    <li>Price Sale</li>
                    <li>Description</li>
                    <li>Classify</li>
                    <li>Image</li>
                    <li></li>
                </ul>
            </div>
            <div id="bodyTableProduct">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:GridView ID="productView" runat="server" AutoGenerateColumns="False" ShowHeader="False" OnRowEditing="productView_RowEditing" OnRowCancelingEdit="productView_RowCancelingEdit" 
                        DataKeyNames="productID, ImageURL, Classify"  OnRowDeleting="productView_RowDeleting" OnRowUpdating="productView_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Product ID" >
                                    <EditItemTemplate>
                                        <asp:Label ID="lbProductIDNew" runat="server" Text='<%# Eval("productID") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProductNameNew" runat="server" Text='<%# Eval("productName") %>' type="Text" Rows="10" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Old">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPriceOldNew" runat="server" Text='<%# Eval("priceOld") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("PriceOld") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Sale">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPriceSaleNew" runat="server" Text='<%# Eval("priceSale") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("PriceSale") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescriptionNew" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Description") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Classify">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Int32.Parse(Eval("Classify").ToString()) == 1 ? "Áo thun"
                                                :Int32.Parse(Eval("Classify").ToString()) == 2 ? "Quần âu" 
                                                :Int32.Parse(Eval("Classify").ToString()) == 3 ? "Áo khoác" 
                                                :Int32.Parse(Eval("Classify").ToString()) == 4 ? "Quần lót" 
                                                :Int32.Parse(Eval("Classify").ToString()) == 5 ? "Áo khoác da" 
                                                :Int32.Parse(Eval("Classify").ToString()) == 6 ? "Quần jean" 
                                                :Int32.Parse(Eval("Classify").ToString()) == 7 ? "Sơ mi" 
                                                :Int32.Parse(Eval("Classify").ToString()) == 8 ? "Sweater" 
                                                : "Hoodie"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ImageURL">
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="fileImageUrlNew" runat="server"  />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="ImageOld" runat="server" ImageUrl='<%# Eval("ImageURL")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" HeaderText="Command" ShowHeader="True"/>
                            </Columns>
                        </asp:GridView>
            </div>
            <div id="foodterTableProduct">
                <div id="headerFoodterTableProduct">
                    <asp:Label ID="lbText" runat="server" Text="Shop Men's"></asp:Label>
                </div>
                <div id="bodyFoodterTableProduct">
                    <div id="bodyFoodterTableProduct-input">
                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Product Name"></asp:TextBox>
                        <asp:TextBox ID="txtPriceOld" runat="server" placeholder="Enter Price Old"></asp:TextBox>
                        <asp:TextBox ID="txtPriceSale" runat="server" placeholder="Enter Price Sale"></asp:TextBox>
                        <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description"></asp:TextBox>
                    </div>
                    <div id="bodyFoodterTableProduct-choice">
                       <asp:RadioButtonList ID="rdClassify" runat="server" RepeatLayout="UnorderedList">
                            <asp:ListItem Value="1" Selected="True">Áo thun</asp:ListItem>
                            <asp:ListItem Value="2">Quần âu</asp:ListItem>
                            <asp:ListItem Value="3">Áo khoác</asp:ListItem>
                            <asp:ListItem Value="4">Quần lót</asp:ListItem>
                            <asp:ListItem Value="5">Áo khoác da</asp:ListItem>
                            <asp:ListItem Value="6">Quần jean</asp:ListItem>
                            <asp:ListItem Value="7">Sơ mi</asp:ListItem>
                            <asp:ListItem Value="8">Sweater</asp:ListItem>
                            <asp:ListItem Value="9">Hoodie</asp:ListItem>
                        </asp:RadioButtonList>
                        <div id="addImage">
                            <asp:FileUpload ID="fileImage" runat="server" placeholder="Choice Image"/>
                        </div>
                        <div id="addFoodterTableProduct">
                            <asp:Button ID="addProduct" runat="server" Text="Add Product" OnClick="addProduct_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="tableProductParameters">
                 <asp:GridView ID="productParamater" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                            OnRowCancelingEdit="productParamater_RowCancelingEdit"
                            OnRowDeleting="productParamater_RowDeleting"
                            OnRowEditing="productParamater_RowEditing"
                            OnRowUpdating="productParamater_RowUpdating" DataKeyNames="productParameterID,productID,ImgUrl">
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("productParameterID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProductIDNew" runat="server" Text='<%# Eval("productID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("productID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtColorNew" runat="server" Text='<%# Eval("color") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("color") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Int32.Parse(Eval("size").ToString()) == 1 ? "M" 
                                                : Int32.Parse(Eval("size").ToString()) == 2 ?  "L" 
                                                : Int32.Parse(Eval("size").ToString()) == 3 ? "XL" 
                                                : Int32.Parse(Eval("size").ToString()) == 4 ? "2XL" 
                                                : Int32.Parse(Eval("size").ToString()) == 5 ? "3XL" 
                                                : "4XL"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQuantityNew" runat="server" Text='<%# Eval("quantity") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="fileImgUrlNew" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImgUrl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Command" ShowDeleteButton="True" ShowEditButton="True" />
                            </Columns>
                        </asp:GridView>
            </div>

            <div id="foodterTableProductParameter">
                <div id="headerFoodterTableProductParameter">
                    <asp:Label ID="lbTextParameter" runat="server" Text="Shop Men's"></asp:Label>
                </div>
                <div id="bodyFoodterTableProductParameter">
                    <div id="bodyFoodterTableProductParameter-input">
                        <asp:TextBox ID="txtProductID" runat="server" placeholder="Enter Product ID"></asp:TextBox>
                        <asp:TextBox ID="txtColor" runat="server" placeholder="Enter Color"></asp:TextBox>
                        <asp:TextBox ID="txtQuantity" runat="server" placeholder="Enter Quantity"></asp:TextBox>
                    </div>
                    <div id="bodyFoodterTableProductParameter-choice">
                       <asp:RadioButtonList ID="rdSize" runat="server" RepeatLayout="UnorderedList">
                            <asp:ListItem Value="1" Selected="True">M</asp:ListItem>
                            <asp:ListItem Value="2">L</asp:ListItem>
                            <asp:ListItem Value="3">XL</asp:ListItem>
                            <asp:ListItem Value="4">2XL</asp:ListItem>
                            <asp:ListItem Value="5">3XL</asp:ListItem>
                            <asp:ListItem Value="6">4XL</asp:ListItem>
                        </asp:RadioButtonList>
                        <div id="addImageParameter">
                            <asp:FileUpload ID="FileUpload1" runat="server" placeholder="Choice Image"/>
                        </div>
                        <div id="addFoodterTableProductParameter">
                            <asp:Button ID="addProductParameter" runat="server" Text="Add" OnClick="addProductParameter_Click"  />
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
