using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using webshopmen.model;

namespace webshopmen.database
{
    public class UserData
    {
        private string connectionString = "Data Source=.;Initial Catalog=shopmen;Integrated Security=True";

        public bool ValidateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM UserShops WHERE userName = @Username AND password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool GetAccessibleByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT accessible FROM UserShops WHERE userName = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return (bool)result;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Trả về giá trị mặc định hoặc xác định để biểu thị lỗi
            }
        }

        public bool AddUser(UserShop user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO UserShops (Name, userName, password, address, email, accessible) VALUES (@Name, @Username, @Password, @Address, @Email, @Accessible)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Username", user.userName);
                        command.Parameters.AddWithValue("@Password", user.password);
                        command.Parameters.AddWithValue("@Address", user.address);
                        command.Parameters.AddWithValue("@Email", user.email);
                        command.Parameters.AddWithValue("@Accessible", user.accessible);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM UserShops WHERE userName = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ ở đây, ví dụ: ghi log, thông báo lỗi, v.v.
                Console.WriteLine("Error: " + ex.Message);
                return false; // Trả về false để biểu thị rằng có lỗi xảy ra trong quá trình kiểm tra
            }
        }
    }
}