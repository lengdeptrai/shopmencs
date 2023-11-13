using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webshopmen.database;
using webshopmen.model;

namespace webshopmen.view
{
    public partial class ProductInformation : System.Web.UI.Page
    {
        private ProductData getSQL = new ProductData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productID = Request.QueryString["productID"];

                if (!string.IsNullOrEmpty(productID))
                {
                    try
                    {
                        Product product = getSQL.GetProductById(Int32.Parse(productID));
                        imgMain.ImageUrl = product.imageURL != "" ? product.imageURL : "../assets/imgs/logo.png";
                        productName.Text = product.productName;
                        priceOld.Text = product.priceOld.ToString("0đ");
                        priceSale.Text = product.priceSale.ToString("0đ");
                        isSale.Text = "Giảm " + product.discountPercentage.ToString() + "%";
                        string str = product.description;
                        string formattedString = str.Replace(".", ".\n");
                        description.Text = formattedString;

                        product.relatedImages = getSQL.GetNonNullImageUrlsByProductID(product.productID);
                        listRelated.DataSource = product.relatedImages;
                        listRelated.DataBind();

                        product.color = getSQL.GetDistinctColorsByProductID(product.productID);
                        listColor.DataSource = product.color;
                        listColor.DataBind();

                        product.size = getSQL.GetDistinctSizesByProductID(product.productID);
                        List<string> sizes = new List<string>();
                        foreach(byte size in product.size)
                        {
                            if (size == 1) sizes.Add("M");
                            else if (size == 2) sizes.Add("L");
                            else if (size == 3) sizes.Add("XL");
                            else if (size == 4) sizes.Add("2XL");
                            else if (size == 5) sizes.Add("3XL");
                            else sizes.Add("4XL");
                        }

                        listSize.DataSource = sizes;
                        listSize.DataBind();

                    }
                    catch (Exception) { }


                }
            }
        }

        protected void listRelated_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Active")
            {
                // Lấy đường dẫn từ dữ liệu của item được chọn
                string selectedImagePath = e.CommandArgument.ToString();

                // Gán đường dẫn của asp:Image từ item được chọn
                imgMain.ImageUrl = selectedImagePath;

            }
        }

    }


}
