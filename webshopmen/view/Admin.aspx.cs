using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webshopmen.database;
using webshopmen.model;

namespace webshopmen.view
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Accessible"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                loadTableProduct();
                loadTableProductParameter();
            }
        }

        private void loadTableProduct()
        {
            ProductData data = new ProductData();
            productView.DataSource = data.GetAllProducts();
            productView.DataBind();
        }

        private void loadTableProductParameter()
        {
            ProductParameterRepository repository = new ProductParameterRepository();
            productParamater.DataSource = repository.GetAllProductParameters();
            productParamater.DataBind();
        }

        protected void productView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            productView.EditIndex = e.NewEditIndex;
            loadTableProduct();
            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productView.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.NewEditIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex - 1].scrollIntoView();
                                rows[canceledRowIndex - 1].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);
        }

        protected void productView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            productView.EditIndex = -1;
            loadTableProduct();

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productView.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.RowIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);
        }

        protected void addProduct_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các trường nhập liệu có trống không
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPriceOld.Text) ||
                string.IsNullOrWhiteSpace(txtPriceSale.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                // Hiển thị thông báo lỗi nếu có trường nào đó trống
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please fill out all fields.');", true);
                return; // Dừng việc thực hiện tiếp để tránh thêm sản phẩm với dữ liệu không hợp lý
            }

            // Kiểm tra xem người dùng đã chọn loại sản phẩm (RadioButtonList) chưa
            if (string.IsNullOrWhiteSpace(rdClassify.SelectedValue))
            {
                // Hiển thị thông báo lỗi nếu loại sản phẩm chưa được chọn
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a product category.');", true);
                return;
            }

            // Tiếp tục xử lý nếu tất cả các trường nhập liệu không trống và đã chọn loại sản phẩm
            string productName = txtName.Text;
            decimal priceOld, priceSale;

            // Kiểm tra xem giá cũ và giá sale có định dạng số hợp lý không
            if (!decimal.TryParse(txtPriceOld.Text, out priceOld) || !decimal.TryParse(txtPriceSale.Text, out priceSale))
            {
                // Hiển thị thông báo lỗi nếu giá cũ hoặc giá sale không hợp lý
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid price format.');", true);
                return;
            }

            // Kiểm tra xem người dùng đã chọn ảnh chưa
            string imageURL = "";
            if (fileImage.HasFile)
            {
                string fileExtension = Path.GetExtension(fileImage.PostedFile.FileName);

                // Tạo một chuỗi ngẫu nhiên dựa trên thời gian hiện tại để đảm bảo tính duy nhất
                string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Ghép tên file mới với chuỗi ngẫu nhiên và phần mở rộng của file
                string newFileName = $"shopmen{uniqueId}{fileExtension}";

                string imagePath = Server.MapPath("../assets/imgs/") + newFileName;
                fileImage.SaveAs(imagePath);
                imageURL = "../assets/imgs/" + newFileName;
            }

            // Thực hiện thêm sản phẩm vào cơ sở dữ liệu
            Product newProduct = new Product
            {
                productName = productName,
                priceOld = priceOld,
                priceSale = priceSale,
                description = txtDescription.Text,
                classify = Byte.Parse(rdClassify.SelectedValue),
                imageURL = imageURL
            };

            // Gọi phương thức để thêm sản phẩm vào cơ sở dữ liệu
            ProductData data = new ProductData();
            data.AddProduct(newProduct);

            // Hiển thị thông báo thành công hoặc làm các hành động khác sau khi thêm sản phẩm
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product added successfully!');", true);

            // Làm sạch các trường nhập liệu
            txtName.Text = "";
            txtPriceOld.Text = "";
            txtPriceSale.Text = "";
            txtDescription.Text = "";
            rdClassify.ClearSelection();
            rdClassify.SelectedValue = "1";

            loadTableProduct();
        }

        protected void productView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(productView.DataKeys[e.RowIndex].Values["productID"]);
            ProductData data = new ProductData();
            data.DeleteProduct(productId);
            loadTableProduct();

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productView.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.RowIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);
        }

        protected void productView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int productID = Convert.ToInt32(productView.DataKeys[e.RowIndex].Values["productID"]);

            // Lấy các giá trị được chỉnh sửa từ GridView
            string txtProductNameNew = ((TextBox)productView.Rows[e.RowIndex].FindControl("txtProductNameNew")).Text;
            string txtPriceOldNew = ((TextBox)productView.Rows[e.RowIndex].FindControl("txtPriceOldNew")).Text;
            string txtPriceSaleNew = ((TextBox)productView.Rows[e.RowIndex].FindControl("txtPriceSaleNew")).Text;
            string txtDescriptionNew = ((TextBox)productView.Rows[e.RowIndex].FindControl("txtDescriptionNew")).Text;
            FileUpload fileImageUrlNew = (FileUpload)productView.Rows[e.RowIndex].FindControl("fileImageUrlNew");

            // Kiểm tra và lưu trữ giá trị hình ảnh mới
            string imageUrl = productView.DataKeys[e.RowIndex].Values["ImageURL"].ToString();
            if (fileImageUrlNew.HasFile)
            {
                string fileExtension = Path.GetExtension(fileImageUrlNew.PostedFile.FileName);

                // Tạo một chuỗi ngẫu nhiên dựa trên thời gian hiện tại để đảm bảo tính duy nhất
                string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Ghép tên file mới với chuỗi ngẫu nhiên và phần mở rộng của file
                string newFileName = $"shopmen{uniqueId}{fileExtension}";

                string imagePath = Server.MapPath("../assets/imgs/") + newFileName;
                fileImageUrlNew.SaveAs(imagePath);
                imageUrl = "../assets/imgs/" + newFileName;
            }

            // Tạo đối tượng Product và cập nhật thông tin
            Product product = new Product
            {
                productID = productID,
                productName = txtProductNameNew,
                priceOld = Convert.ToDecimal(txtPriceOldNew),
                priceSale = Convert.ToDecimal(txtPriceSaleNew),
                description = txtDescriptionNew,
                classify = Convert.ToByte(productView.DataKeys[e.RowIndex].Values["Classify"]),
                imageURL = imageUrl
            };

            // Gọi phương thức UpdateProduct để cập nhật sản phẩm trong cơ sở dữ liệu
            ProductData data = new ProductData();
            data.UpdateProduct(product);


            productView.EditIndex = -1;


            // Reload dữ liệu cho GridView hoặc thực hiện các bước cần thiết để cập nhật giao diện
            loadTableProduct(); // Hàm này cần được xây dựng để tải dữ liệu mới vào GridView

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productView.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.RowIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);
        }

        protected void productParamater_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            productParamater.EditIndex = -1;
            loadTableProductParameter();

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productParamater.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.RowIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);

        }

        protected void productParamater_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productParameterID = Convert.ToInt32(productParamater.DataKeys[e.RowIndex].Values[0]);
            ProductParameterRepository repository = new ProductParameterRepository();
            repository.DeleteProductParameter(productParameterID);
            loadTableProductParameter();

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productParamater.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.RowIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);
        }

        protected void productParamater_RowEditing(object sender, GridViewEditEventArgs e)
        {
            productParamater.EditIndex = e.NewEditIndex;
            loadTableProductParameter();

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productParamater.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.NewEditIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);
        }

        protected void productParamater_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int productParameterID = Convert.ToInt32(productParamater.DataKeys[e.RowIndex].Values[0]);
            TextBox txtProductID = (TextBox)productParamater.Rows[e.RowIndex].FindControl("txtProductIDNew");
            TextBox txtColor = (TextBox)productParamater.Rows[e.RowIndex].FindControl("txtColorNew");
            TextBox txtQuantity = (TextBox)productParamater.Rows[e.RowIndex].FindControl("txtQuantityNew");
            FileUpload fileUpload = (FileUpload)productParamater.Rows[e.RowIndex].FindControl("fileImgUrlNew");

            string imgUrl = productParamater.DataKeys[e.RowIndex].Values["ImgUrl"].ToString(); ; // Giữ nguyên giá trị của cột ImgUrl

            // Kiểm tra xem người dùng đã chọn hình ảnh mới hay không
            if (fileUpload.HasFile)
            {
                string fileExtension = Path.GetExtension(fileUpload.PostedFile.FileName);

                // Tạo một chuỗi ngẫu nhiên dựa trên thời gian hiện tại để đảm bảo tính duy nhất
                string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Ghép tên file mới với chuỗi ngẫu nhiên và phần mở rộng của file
                string newFileName = $"shopmen{uniqueId}{fileExtension}";

                string imagePath = Server.MapPath("../assets/imgs/") + newFileName;
                fileUpload.SaveAs(imagePath);
                imgUrl = "../assets/imgs/" + newFileName;
            }

            ProductParameter productParameter = new ProductParameter
            {
                productParameterID = productParameterID,
                productID = Convert.ToInt32(txtProductID.Text),
                color = txtColor.Text,
                quantity = Convert.ToInt32(txtQuantity.Text),
                ImgUrl = imgUrl // Sử dụng imgUrl mới hoặc giữ nguyên giá trị cũ của ImgUrl
                                // You may need to handle other fields as well, depending on your requirements
            };

            ProductParameterRepository repository = new ProductParameterRepository();
            repository.UpdateProductParameter(productParameter);

            productParamater.EditIndex = -1;
            loadTableProductParameter();

            string script = @"<script type='text/javascript'>
                        function setFocusOnCanceledRow() {
                            var gridView = document.getElementById('" + productParamater.ClientID + @"');
                            var rows = gridView.getElementsByTagName('tr');
                            var canceledRowIndex = " + e.RowIndex + @";
                            if (canceledRowIndex >= 0 && canceledRowIndex < rows.length) {
                                rows[canceledRowIndex].scrollIntoView();
                                rows[canceledRowIndex].focus();
                            }
                        }
                        setFocusOnCanceledRow();
                    </script>";

            // Thêm mã JavaScript vào trang web
            ScriptManager.RegisterStartupScript(this, GetType(), "FocusScript", script, false);

        }

        protected void addProductParameter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductID.Text) || string.IsNullOrEmpty(txtColor.Text) || string.IsNullOrEmpty(txtQuantity.Text))
            {
                // Hiển thị thông báo lỗi nếu dữ liệu không đầy đủ
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Vui lòng nhập đầy đủ thông tin.');", true);
                return;
            }

            // Kiểm tra xem ProductID có tồn tại không
            int productID;
            if (!int.TryParse(txtProductID.Text, out productID) || !new ProductData().CheckProductExistence(productID))
            {
                // Hiển thị thông báo lỗi nếu ProductID không hợp lệ hoặc không tồn tại
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('ProductID không hợp lệ hoặc không tồn tại.');", true);
                return;
            }

            // Kiểm tra xem đã nhập số lượng hợp lệ chưa
            int quantity;
            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                // Hiển thị thông báo lỗi nếu số lượng không hợp lệ
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Số lượng không hợp lệ.');", true);
                return;
            }

            // Tạo đối tượng ProductParameter
            ProductParameter productParameter = new ProductParameter
            {
                productID = productID,
                color = txtColor.Text,
                size = Convert.ToByte(rdSize.SelectedValue),
                quantity = quantity
            };

            // Kiểm tra xem có file được chọn hay không
            if (FileUpload1.HasFile)
            {
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                // Tạo một chuỗi ngẫu nhiên dựa trên thời gian hiện tại để đảm bảo tính duy nhất
                string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Ghép tên file mới với chuỗi ngẫu nhiên và phần mở rộng của file
                string newFileName = $"shopmen{uniqueId}{fileExtension}";

                string imagePath = Server.MapPath("../assets/imgs/") + newFileName;
                FileUpload1.SaveAs(imagePath);

                // Lưu đường dẫn của file trong thư mục Uploads vào cơ sở dữ liệu
                productParameter.ImgUrl = "../assets/imgs/" + newFileName;
            }
            else
            {
                // Nếu không có file được chọn, đặt ImgUrl thành chuỗi trống
                productParameter.ImgUrl = "";
            }

            // Thêm thông tin sản phẩm vào cơ sở dữ liệu
            ProductParameterRepository repository = new ProductParameterRepository();
            repository.AddProductParameter(productParameter);

            // Hiển thị thông báo thành công
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Thêm sản phẩm thành công.');", true);

            // Xóa dữ liệu trên các ô input sau khi thêm sản phẩm thành công (nếu cần)
            txtProductID.Text = "";
            txtColor.Text = "";
            txtQuantity.Text = "";
            rdSize.SelectedIndex = 0;

            loadTableProductParameter();
        }
    }
}