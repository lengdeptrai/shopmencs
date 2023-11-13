using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webshopmen
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void menu_MenuItemClick(object sender, MenuEventArgs e)
        {
            int selectedValue = Int32.Parse(e.Item.Value);

            // Clear previous content
            content.InnerHtml = "";

            // Display content based on selected menu item
            switch (selectedValue)
            {
                case 1:
                    content.InnerHtml = "Welcome to the Home Page!";
                    break;
                case 2:
                    content.InnerHtml = "This is the About Us Page.";
                    break;
                case 3:
                    content.InnerHtml = "Explore our Services.";
                    break;
                case 4:
                    content.InnerHtml = "Contact Us at: contact@example.com";
                    break;
                default:
                    content.InnerHtml = "Invalid Selection.";
                    break;
            }
        }
    }
}