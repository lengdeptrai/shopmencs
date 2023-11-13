using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webshopmen.model;

namespace webshopmen.database
{
    public class ProductData
    {
        private string connectionString = "Data Source=.;Initial Catalog=shopmen;Integrated Security=True";


        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            productID = Convert.ToInt32(reader["ProductID"]),
                            productName = reader["ProductName"].ToString(),
                            priceOld = Convert.ToDecimal(reader["PriceOld"]),
                            priceSale = Convert.ToDecimal(reader["PriceSale"]),
                            description = reader["Description"].ToString(),
                            classify = Convert.ToByte(reader["Classify"]),
                            imageURL = reader["ImageURL"].ToString()
                        };
                        products.Add(product);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE ProductID = @ProductId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            productID = Convert.ToInt32(reader["ProductID"]),
                            productName = reader["ProductName"].ToString(),
                            priceOld = Convert.ToDecimal(reader["PriceOld"]),
                            priceSale = Convert.ToDecimal(reader["PriceSale"]),
                            description = reader["Description"].ToString(),
                            classify = Convert.ToByte(reader["Classify"]),
                            imageURL = reader["ImageURL"].ToString()
                        };
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return product;
        }

        public void AddProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Products (ProductName, PriceOld, PriceSale, Description, Classify, ImageURL) " +
                                   "VALUES (@ProductName, @PriceOld, @PriceSale, @Description, @Classify, @ImageURL)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", product.productName);
                    command.Parameters.AddWithValue("@PriceOld", product.priceOld);
                    command.Parameters.AddWithValue("@PriceSale", product.priceSale);
                    command.Parameters.AddWithValue("@Description", product.description);
                    command.Parameters.AddWithValue("@Classify", product.classify);
                    command.Parameters.AddWithValue("@ImageURL", product.imageURL);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Products SET ProductName = @ProductName, PriceOld = @PriceOld, " +
                                   "PriceSale = @PriceSale, Description = @Description, Classify = @Classify, " +
                                   "ImageURL = @ImageURL WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", product.productID);
                    command.Parameters.AddWithValue("@ProductName", product.productName);
                    command.Parameters.AddWithValue("@PriceOld", product.priceOld);
                    command.Parameters.AddWithValue("@PriceSale", product.priceSale);
                    command.Parameters.AddWithValue("@Description", product.description);
                    command.Parameters.AddWithValue("@Classify", product.classify);
                    command.Parameters.AddWithValue("@ImageURL", product.imageURL);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public List<string> GetNonNullImageUrlsByProductID(int productID)
        {
            List<string> imageUrls = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT ImgUrl FROM ProductParameters WHERE ProductID = @ProductID AND ImgUrl IS NOT NULL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string imageUrl = reader["ImgUrl"].ToString();
                                imageUrls.Add(imageUrl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý ngoại lệ theo nhu cầu của bạn
                throw ex; // Nếu bạn muốn ném lại ngoại lệ cho các phần gọi hàm xử lý
            }
            return imageUrls;
        }

        public List<string> GetDistinctColorsByProductID(int productID)
        {
            List<string> colors = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Color FROM ProductParameters WHERE ProductID = @ProductID AND Color IS NOT NULL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string color = reader["Color"].ToString();
                                colors.Add(color);
                            }
                        }
                    }
                }

                // Loại bỏ các giá trị trùng lặp sử dụng hàm Distinct
                colors = colors.Distinct().ToList();
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý ngoại lệ theo nhu cầu của bạn
                throw ex; // Nếu bạn muốn ném lại ngoại lệ cho các phần gọi hàm xử lý
            }
            return colors;
        }

        public List<byte> GetDistinctSizesByProductID(int productID)
        {
            List<byte> sizes = new List<byte>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Size FROM ProductParameters WHERE ProductID = @ProductID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["Size"] != DBNull.Value)
                                {
                                    byte size = Convert.ToByte(reader["Size"]);
                                    sizes.Add(size);
                                }
                            }
                        }
                    }
                }

                // Loại bỏ các giá trị trùng lặp sử dụng hàm Distinct
                sizes = sizes.Distinct().ToList();
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý ngoại lệ theo nhu cầu của bạn
                throw ex; // Nếu bạn muốn ném lại ngoại lệ cho các phần gọi hàm xử lý
            }
            return sizes;
        }

        public int GetMaxProductId()
        {
            string sqlQuery = "SELECT MAX(ProductID) FROM Products";
            int maxProductId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            maxProductId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred: " + ex.Message);
            }

            return maxProductId;
        }

        public bool CheckProductExistence(int productID)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Products WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}