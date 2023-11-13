using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using webshopmen.database;
using webshopmen.model;
using ZXing;

namespace webshopmen.view
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserData user = new UserData();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (user.ValidateUser(username, HashPassword(password)))
            {
                if (user.GetAccessibleByUsername(username)) {
                    Session["Accessible"] = user.GetAccessibleByUsername(username);
                    Response.Redirect("Admin.aspx");
                }
                else
                {
                Session["Username"] = username;
                Response.Redirect("Default.aspx");
                }
            }
            else
            {
                statusLogin.Text = "Tên người dùng hoặc mật khẩu không đúng.";
            }
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            UserData userDataManager = new UserData();
            string name = txtNewName.Text.Trim();
            string newUserName = txtNewUserName.Text.Trim();
            string password = txtNewPassword.Text.Trim();
            string checkPassword = txtCheckPassword.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (userDataManager.IsUsernameExists(newUserName))
            {
                statusRegister.Text = "Tên tài khoản đã tồn tại";
                return;
            }
            else if (String.Compare(password, checkPassword) != 0)
            {
                statusRegister.Text = "Mật khẩu không trùng nhau";
                return;
            }
            else if (!IsGmailAddress(email))
            {
                statusRegister.Text = "Email không hợp lệ";
                return;
            }
            else
            {
                UserShop userShop = new UserShop
                {
                    userID = 0,
                    Name = name,
                    userName = newUserName,
                    password = HashPassword(password),
                    email = email,
                    address = "",
                    accessible = false
                };

                if (userDataManager.AddUser(userShop))
                {
                    createQRcode(userShop.userName, userShop.password);
                }
                else
                    statusRegister.Text = "Đăng ký không thành công. Vui lòng thử lại sau.";
            }
            string script = @"<script type='text/javascript'>
                        Handle();
                    </script>";

            // Đăng ký script để chạy sau khi UpdatePanel được cập nhật
            ScriptManager.RegisterStartupScript(this, GetType(), "RegisterStartupScript", script, false);

        }

        public bool IsGmailAddress(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@gmail\.com$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        public static string HashPassword(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);

            string base64String = Convert.ToBase64String(data);

            return base64String;
        }

        [WebMethod]
        public static string ProcessQRCode(string qrCode)
        {
            if (IsValidQRCode(qrCode))
            {
                HttpContext.Current.Session["UserName"] = qrCode.Split(':')[0];
                return "Success";
            }
            return "Is Not";

        }

        // Phương thức kiểm tra mã QR (ví dụ)
        private static bool IsValidQRCode(string qrCode)
        {
            string[] str = qrCode.Split(':');
            string username = str[0];
            string password = str[1];
            UserData data = new UserData();
            return data.ValidateUser(username, password);
        }


        public static void createQRcode(string username, string password)
        {
            string qrCodeText = $"{username}:{password}";
            try
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                Bitmap qrCodeBitmap = barcodeWriter.Write(qrCodeText);

                // Lưu mã QR vào tệp hình ảnh
                string qrCodeFilePath = HttpContext.Current.Server.MapPath($"../assets/imgs/{username}.png");
                qrCodeBitmap.Save(qrCodeFilePath);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ví dụ: ghi log, hiển thị thông báo lỗi cho người dùng, v.v.
                // Đồng thời, bạn cũng có thể throw lại exception nếu muốn chuyển nó lên tầng gọi hàm để xử lý ở đó.
                // throw ex;
            }
        }
    }
}