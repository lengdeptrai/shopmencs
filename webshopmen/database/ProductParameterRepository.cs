using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webshopmen.model;

namespace webshopmen.database
{
    public class ProductParameterRepository
    {
        private string connectionString = "Data Source=.;Initial Catalog=shopmen;Integrated Security=True";

        public void AddProductParameter(ProductParameter productParameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ProductParameters (ProductID, Color, Size, Quantity, ImgUrl) VALUES (@ProductID, @Color, @Size, @Quantity, @ImgUrl)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productParameter.productID);
                    command.Parameters.AddWithValue("@Color", productParameter.color);
                    command.Parameters.AddWithValue("@Size", productParameter.size);
                    command.Parameters.AddWithValue("@Quantity", productParameter.quantity);
                    command.Parameters.AddWithValue("@ImgUrl", productParameter.ImgUrl);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProductParameter(ProductParameter productParameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE ProductParameters SET ProductID=@ProductID, Color=@Color, Size=@Size, Quantity=@Quantity, ImgUrl=@ImgUrl WHERE ProductParameterID=@ProductParameterID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductParameterID", productParameter.productParameterID);
                    command.Parameters.AddWithValue("@ProductID", productParameter.productID);
                    command.Parameters.AddWithValue("@Color", productParameter.color);
                    command.Parameters.AddWithValue("@Size", productParameter.size);
                    command.Parameters.AddWithValue("@Quantity", productParameter.quantity);
                    command.Parameters.AddWithValue("@ImgUrl", productParameter.ImgUrl);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProductParameter(int productParameterID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ProductParameters WHERE ProductParameterID=@ProductParameterID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductParameterID", productParameterID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ProductParameter> GetAllProductParameters()
        {
            List<ProductParameter> productParameters = new List<ProductParameter>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ProductParameters";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductParameter productParameter = new ProductParameter
                            {
                                productParameterID = Convert.ToInt32(reader["ProductParameterID"]),
                                productID = Convert.ToInt32(reader["ProductID"]),
                                color = reader["Color"].ToString(),
                                size = Convert.ToByte(reader["Size"]),
                                quantity = Convert.ToInt32(reader["Quantity"]),
                                ImgUrl = reader["ImgUrl"].ToString()
                            };
                            productParameters.Add(productParameter);
                        }
                    }
                }
            }
            return productParameters;
        }
    }
}