using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.IO;

namespace WAndCAssignment.Models
{
    public class DAO
    {
        public SqlConnection conn;
        public string message = "";

        public void Connection()
        {
            string databaseConn = WebConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn = new SqlConnection(databaseConn);
        }


        public int InsertCustomerCreditCard(string credCardNum,string email)
        {
            int count = 0;

            Connection();

            string query = "UPDATE Customers SET CustomerCreditCard = @credCard WHERE CustomerEmail = @email";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@credCard", credCardNum);
            cmd.Parameters.AddWithValue("@email", email);


            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }


            return count;
        }


        //Insert TO Customers Query
        public int Insert(Customers m)
        {
            int count = 0;

            //Call connection method
            Connection();

            //Create Query
            string query = "INSERT INTO Customers (CustomerEmail,CustomerFirstName,CustomerLastName,CustomerUserName,CustomerPassword) VALUES(@email,@fname,@lname,@uname,@pass)";

            SqlCommand cmd = new SqlCommand(query, conn);

            //add parameter values
            cmd.Parameters.AddWithValue("@email", m.Email);
            cmd.Parameters.AddWithValue("@fname", m.FirstName);
            cmd.Parameters.AddWithValue("@lname", m.LastName);
            cmd.Parameters.AddWithValue("@uname", m.Username);
            cmd.Parameters.AddWithValue("@pass", m.Password);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }


            return count;
        }


        //SHow All General Products
        public List<Product> AllProducts()
        {
            Connection();
            List<Product> list = new List<Product>();
            //Prepare data reader
            SqlDataReader reader;

            //prepare Command
            SqlCommand cmd = new SqlCommand("uspAllProducts", conn);
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                //open connectiond
                conn.Open();
                reader = cmd.ExecuteReader();

                //while there's data use it
                while (reader.Read())
                {
                    //create an instance of product class
                    Product prod = new Product();
                    prod.ProductID = reader["ProductId"].ToString();
                    prod.ProductModel = reader["ProductModel"].ToString();
                    prod.ProductDescription = reader["ProductDescription"].ToString();
                    prod.ProductPrice = decimal.Parse(reader["ProductPrice"].ToString());
                    prod.ProductBrand = reader["ProductBrand"].ToString();
                    prod.ProductImage = reader["ProductImage"].ToString();
                    list.Add(prod);
                }
             }

             finally
            {
                conn.Close();
            }

            return list;
        }

        #region Products
        //Show Only Phone
        public List<Product> AllPhones()
        {
            Connection();
            List<Product> list = new List<Product>();
            //Prepare data reader
            SqlDataReader reader;

            //prepare Command
            SqlCommand cmd = new SqlCommand("uspAllPhone", conn);
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                //open connectiond
                conn.Open();
                reader = cmd.ExecuteReader();

                //while there's data use it
                while (reader.Read())
                {
                    //create an instance of product class
                    Product prod = new Product();
                    prod.ProductID = reader["ProductId"].ToString();
                    prod.ProductModel = reader["ProductModel"].ToString();
                    prod.ProductDescription = reader["ProductDescription"].ToString();
                    prod.ProductPrice = decimal.Parse(reader["ProductPrice"].ToString());
                    prod.ProductBrand = reader["ProductBrand"].ToString();
                    prod.ProductImage = reader["ProductImage"].ToString();
                    list.Add(prod);
            

                }

                //always close reader
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }

            return list;
        }

        //Electronic list
        public List<Product> Allelectronic()
        {
            List<Product> productlist = new List<Product>();
            SqlDataReader reader;
            Connection();



            SqlCommand cmd = new SqlCommand("uspallelectronic", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();

                    product.ProductID = reader["ProductId"].ToString();
                    product.ProductBrand = reader["ProductBrand"].ToString();
                    product.ProductModel = reader["ProductModel"].ToString();
                    product.ProductBrand = reader["ProductBrand"].ToString();
                    product.ProductPrice = (decimal)reader["ProductPrice"];
                    product.ProductImage = reader["ProductImage"].ToString();
                    productlist.Add(product);
                }
            }
            finally
            {
                conn.Close();
            }

            return productlist;
        }

        //Laptop list
        public List<Product> AllLaptop()
        {
            List<Product> productlist = new List<Product>();
            SqlDataReader reader;
            Connection();


            SqlCommand cmd = new SqlCommand("uspalllaptop", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductID = reader["ProductId"].ToString();
                    product.ProductBrand = reader["ProductBrand"].ToString();
                    product.ProductModel = reader["ProductModel"].ToString();
                    product.ProductBrand = reader["ProductBrand"].ToString();
                    product.ProductPrice = (decimal)reader["ProductPrice"];
                    product.ProductImage = reader["ProductImage"].ToString();

                    productlist.Add(product);
                }
            }
            finally
            {
                conn.Close();
            }

            return productlist;
        }

       /* public Product GetProductById(string id)
        {
            Product p = new Product();
            p.ProductID = id;
            SqlDataReader reader;
            Connection();

            string query = "SELECT * FROM Products WHERE ProductId = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", p.ProductID);


            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p.ProductID = reader["ProductId"].ToString();
                }

            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return p;
        } */

        #endregion


        //check if user exists
        public Customers CheckLogin(Customers customer)
        {
            customer.FirstName = null;
            SqlDataReader reader;
            Connection();

            string query = "SELECT CustomerEmail,CustomerFirstName FROM Customers WHERE CustomerUsername = @uname AND CustomerPassword = @pass";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@uname", customer.Username);
            cmd.Parameters.AddWithValue("@pass", customer.Password);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer.FirstName = reader["CustomerFirstName"].ToString();
                    customer.Email = reader["CustomerEmail"].ToString();
                }

            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return customer;
        }

        #region Shopping Cart Methods

        public int AddOrder(string orderid, DateTime date,string customerEmail, decimal totalPrice)
        {
            int count = 0;
            Connection();
            string query = "INSERT INTO Orders VALUES(@OrderId,@OrderDate,@CustomerEmail,@ProductPrice)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@OrderId", orderid);
            cmd.Parameters.AddWithValue("@OrderDate", date);
            cmd.Parameters.AddWithValue("@CustomerEmail", customerEmail);
            cmd.Parameters.AddWithValue("@ProductPrice", totalPrice);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                conn.Close();

            }

            return count;
        }

        public int AddLineItem(int quantity,string orderid, string productid)
        {
            int count = 0;
            Connection();
            string query = "INSERT INTO LineItems VALUES(@lineItemQuantity,@orderId,@productId)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@lineItemquantity", quantity);
            cmd.Parameters.AddWithValue("@orderId", orderid);
            cmd.Parameters.AddWithValue("@productId", productid);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;



        }

   

        public List<Product> countFindItems(string search)
        {
            List<Product> list = new List<Product>();
            Connection();

            SqlCommand cmd = new SqlCommand("Select * From Products WHERE ProductBrand = @search", conn);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@search", search);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.ProductBrand = reader["ProductBrand"].ToString();
                    prod.ProductDescription = reader["ProductDescription"].ToString();
                    prod.ProductModel = reader["ProductModel"].ToString();
                    prod.ProductImage = reader["ProductImage"].ToString();
                    prod.ProductPrice = decimal.Parse(reader["ProductPrice"].ToString());
                    prod.ProductType = (ProductType)Enum.Parse(typeof(ProductType),reader["ProductType"].ToString());
                    list.Add(prod);

                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }


            return list;
        }

        #endregion



    }
}