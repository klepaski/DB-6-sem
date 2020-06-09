using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _2lab
{
    class DB
    {
        SqlConnection conn;
        public void openConnection(string connStr)
        {
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void closeConnection()
        {
            conn.Close();
        }

        #region client
        public void add_client(string name, string city)
        {
            using (SqlCommand cmd = new SqlCommand("add_client", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.ExecuteNonQuery();
            }

        }
        public void drop_client(int id)
        {
            using (SqlCommand cmd = new SqlCommand("drop_client", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }

        public void change_client(int id, string name, string city)
        {
            using (SqlCommand cmd = new SqlCommand("change_client", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion client


        #region Products
        public void add_product(string name, int count)
        {
            using (SqlCommand cmd = new SqlCommand("add_product", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@kol", count);
                cmd.ExecuteNonQuery();
            }
        }

        public void drop_product(int id)
        {
            using (SqlCommand cmd = new SqlCommand("drop_product", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }

        public void change_product(int id, string name, int kol)
        {
            using (SqlCommand cmd = new SqlCommand("change_product", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@kol", kol);
                cmd.ExecuteNonQuery();
            }

        }
        #endregion Products


        #region Orders

        //	@id_order int,  @id_diler int, @id_client int, @id_product int, @summ int, @order_date date

        public void add_order(int id_diler, int id_client, int id_product, int summ, DateTime order_date)
        {
                using (SqlCommand cmd = new SqlCommand("add_order", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_diler", id_diler);
                    cmd.Parameters.AddWithValue("@id_client", id_client);
                    cmd.Parameters.AddWithValue("@id_product", id_product);
                    cmd.Parameters.AddWithValue("@summ", summ);
                    cmd.Parameters.AddWithValue("@order_date", order_date);
                    cmd.ExecuteNonQuery();
                }
        }

        public void drop_order(int id)
        {
            using (SqlCommand cmd = new SqlCommand("drop_order", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }

        public void change_order(int id_order, int id_diler, int id_client, int id_product, int summ, DateTime order_date)
        {
            using (SqlCommand cmd = new SqlCommand("change_order", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_order", id_order);
                cmd.Parameters.AddWithValue("@id_diler", id_diler);
                cmd.Parameters.AddWithValue("@id_client", id_client);
                cmd.Parameters.AddWithValue("@id_product", id_product);
                cmd.Parameters.AddWithValue("@summ", summ);
                cmd.Parameters.AddWithValue("@order_date", order_date);
                cmd.ExecuteNonQuery();
            }

        }

        #endregion Orders


        #region Supplies

        //id supply, diler, product, supp

        public void add_supply(int id_diler, int id_product, int supp)
        {
            using (SqlCommand cmd = new SqlCommand("add_supply", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_diler", id_diler);
                cmd.Parameters.AddWithValue("@id_product", id_product);
                cmd.Parameters.AddWithValue("@supp", supp);
                cmd.ExecuteNonQuery();
            }
        }

        public void drop_supply(int id)
        {
            using (SqlCommand cmd = new SqlCommand("drop_supply", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }

        public void change_supply(int id_supply, int id_diler, int id_product, int supp)
        {
            using (SqlCommand cmd = new SqlCommand("change_supply", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_supply", id_supply);
                cmd.Parameters.AddWithValue("@id_diler", id_diler);
                cmd.Parameters.AddWithValue("@id_product", id_product);
                cmd.Parameters.AddWithValue("@supp", supp);
                cmd.ExecuteNonQuery();
            }

        }

        #endregion Supplies


        #region Dilers

        //id diler, name, city, phone

        public void add_diler(string name, string city, string phone)
        {
            using (SqlCommand cmd = new SqlCommand("add_diler", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
            }
        }

        public void drop_diler(int id)
        {
            using (SqlCommand cmd = new SqlCommand("drop_diler", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }

        public void change_diler(int id_diler, string name, string city, string phone)
        {
            using (SqlCommand cmd = new SqlCommand("change_diler", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id_diler);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
            }

        }

        #endregion Dilers
    }
}
