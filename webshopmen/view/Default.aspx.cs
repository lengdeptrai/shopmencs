using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using webshopmen.database;
using webshopmen.model;

namespace webshopmen.view
{
    public partial class Default : System.Web.UI.Page
    {
        private ProductData getSQL = new ProductData();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Đăng ký sự kiện Page_PreRender
            if (!IsPostBack)
            {
                Page.PreRender += new EventHandler(Page_PreRender);
                List<Product> products = getSQL.GetAllProducts();
                loadOptionUser();
                loadListSaleOff(products);
                loadListProduct(products);
            }
        }

        private List<Product> getProductByClassify(int classify , List<Product> products)
        {
            List<Product> productsByClassify = new List<Product>();
            foreach(Product p in products)
            {
                if(classify == p.classify)
                {
                    productsByClassify.Add(p);
                }
            }
            return productsByClassify;
        }

        protected void menu_MenuItemClick(object sender, MenuEventArgs e)
        {
            List<Product> products = getSQL.GetAllProducts();
            byte value = Byte.Parse(e.Item.Value);
            switch (value)
            {
                case 1:
                    {
                        Response.Redirect("Login.aspx");
                        listProduct.DataSource = getProductByClassify(1, products);
                        listProduct.DataBind();
                        break;
                    }
                case 2:
                    {
                        listProduct.DataSource = getProductByClassify(2, products);
                        listProduct.DataBind();
                        break;
                    }
                case 3:
                    {
                        listProduct.DataSource = getProductByClassify(3, products);
                        listProduct.DataBind();
                        break;
                    }
                case 4:
                    {
                        listProduct.DataSource = getProductByClassify(4, products);
                        listProduct.DataBind();
                        break;
                    }
                case 5:
                    {
                        listProduct.DataSource = getProductByClassify(5, products);
                        listProduct.DataBind();
                        break;
                    }
                case 6:
                    {
                        listProduct.DataSource = getProductByClassify(6, products);
                        listProduct.DataBind();
                        break;
                    }
                case 7:
                    {
                        listProduct.DataSource = getProductByClassify(7, products);
                        listProduct.DataBind();
                        break;
                    }
                case 8:
                    {
                        listProduct.DataSource = getProductByClassify(8, products);
                        listProduct.DataBind();
                        break;
                    }

                default:
                    {
                        listProduct.DataSource = getProductByClassify(9, products);
                        listProduct.DataBind();
                        break;
                    }


            }

        }

        protected void listSaleOff_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewProduct")
            {
                string productID = e.CommandArgument.ToString();
                Response.Redirect("ProductInformation.aspx?productID=" + productID);
            }
        }

        private void loadOptionUser()
        {

            List<string> options = new List<string>
                {
                    "Tài khoản",
                    "Đăng xuất"
                };

            listOption.DataSource = options;
            listOption.DataBind();
        }
        
        private void loadListSaleOff(List<Product> products)
        {
            List<Product> listSale = new List<Product>();
            foreach (Product product in products)
                if(listSale.Count() < 6)
                    if (product.discountPercentage > 0) listSale.Add(product);
            listSaleOff.DataSource = listSale;
            listSaleOff.DataBind();
        }

        private void loadListProduct(List<Product> products)
        {
            listProduct.DataSource = products;
            listProduct.DataBind();
        }




        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                    btnLogin.Visible = false;
                    linkLogin.Visible = true;
                    lbUser.Text = Session["Username"].ToString();
                    listOptionUser.Attributes.Add("class", "list_option-user isUser");
            }
                
        }

        protected void listOption_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ItemClick")
            {
                int result = Int32.Parse(e.CommandArgument.ToString());
                if (result == 1)
                {
                    Session["Username"] = null;
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnSeach_Click(object sender, EventArgs e)
        {
            string name = seach.Text.Trim();
            ProductData data = new ProductData();
            List<Product> products = data.GetAllProducts();
            List<Product> productsByName = new List<Product>();
            foreach(Product p in products)
            {
                if((p.productName).IndexOf(name, StringComparison.OrdinalIgnoreCase) >=0)
                {
                    productsByName.Add(p);
                }
            }

            listProduct.DataSource = productsByName;
            listProduct.DataBind();

        }

        protected void menu_MenuItemClick1(object sender, MenuEventArgs e)
        {
            byte value = Byte.Parse(e.Item.Value);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please fill out all fields.');", true);
        }
    }
}