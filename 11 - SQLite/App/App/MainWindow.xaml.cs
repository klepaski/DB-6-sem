using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace Lite
{
    public partial class MainWindow : Window
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();


        public MainWindow()
        {
            InitializeComponent();
            SetConnection();

            sql_con = new SQLiteConnection("Data Source=Sklad.db;Version=3;New=False;Compress=True;");

            LoadData();
        }

        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=Sklad.db;Version=3;New=False;Compress=True;");
        }


        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        private void LoadData()
        {
            SetConnection();
            
            /*
            ExecuteQuery("create table Authors(" +
                "id integer primary key AUTOINCREMENT," +
                "nickname text unique," +
                "mobile_phone text" +
                "); ");

            ExecuteQuery("insert into Authors(nickname, mobile_phone) values " +
                "('Mikhail Bulgakov', '375332587856')," +
                "('Erich Maria Remarque', '375294457856')," +
                "('Oscar Wilde', '375252545856')," +
                "('Agatha Christie', '375332599876'); ");

            ExecuteQuery("create table Books(" +
                "id integer primary key AUTOINCREMENT," +
                "caption text," +
                "publication_year integer," +
                "author_id integer," +
                "cost money," +
                "FOREIGN KEY(author_id) REFERENCES Authors(id)); ");

            ExecuteQuery("insert into Books(caption, publication_year, author_id, cost) values " +
                "('Die Traumbude', 1920, 2, 56.8)," +
                "('The White Guard', 1926, 1, 57.4), " +
                "('Dorian Gray', 1980, 3, 41.3)," +
                "('Der Funke Leben', 1952, 2, 45.6), " +
                "('The Pale Horse', 1961, 4, 78.6), " +
                "('The Master and Margarita', 1967, 1, 35.7), " +
                "('Heart of a Dog', 1968, 1, 45.1); ");

            ExecuteQuery("create table SoldBooks" +
                "(" +
                "id integer primary key AUTOINCREMENT," +
                "book_id integer," +
                "nbook int check(nbook > 0)," +
                "order_data date," +
                "FOREIGN KEY(book_id) REFERENCES Books(id)); ");

            ExecuteQuery("insert into SoldBooks(book_id,nbook,order_data) values " +
                "(5, 63, '02-05-2018')," +
                "(6, 25, '02-05-2019'), " +
                "(7, 54, '02-05-2017'), " +
                "(3, 890, '02-05-2018'), " +
                "(2, 34, '02-05-2018'), " +
                "(1, 78, '02-05-2017'), " +
                "(4, 15, '02-05-2019'); ");
            */

            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select * from PRODUCTS;";

            using (var reader = sql_cmd.ExecuteReader())
            {
                List<PRODUCT> prod = new List<PRODUCT>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prod.Add(new PRODUCT
                        {
                            IdProduct = reader.GetInt32(0),
                            NameProduct = reader.GetString(1),
                            Kol = reader.GetInt32(2)
                        });
                    }
                }
                else
                {
                    MessageBox.Show("PRODUCTS Table is Empty");
                }
                PRODUCTS.ItemsSource = prod;
            }

            sql_cmd.CommandText = "select * from DILERS;";

            using (var reader = sql_cmd.ExecuteReader())
            {
                List<DILER> dil = new List<DILER>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dil.Add(new DILER
                        {
                            IdDiler = reader.GetInt32(0),
                            NameDiler = reader.GetString(1),
                            CityDiler = reader.GetString(2),
                            Phone = reader.GetString(3)
                        });
                    }
                }
                else
                {
                    MessageBox.Show("DILERS Table is Empty");
                }
                DILERS.ItemsSource = dil;
            }

            sql_cmd.CommandText = "select * from CLIENTS;";

            using (var reader = sql_cmd.ExecuteReader())
            {
                List<CLIENT> cl = new List<CLIENT>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cl.Add(new CLIENT
                        {
                            IdClient = reader.GetInt32(0),
                            NameClient = reader.GetString(1),
                            CityClient = reader.GetString(2)
                        });
                    }
                }
                else
                {
                    MessageBox.Show("CLIENTS Table is Empty");
                }

                CLIENTS.ItemsSource = cl;
            }

            sql_cmd.CommandText = "select * from SUPPLIES;";

            using (var reader = sql_cmd.ExecuteReader())
            {
                List<SUPPLY> sup = new List<SUPPLY>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sup.Add(new SUPPLY
                        {
                            IdSupply = reader.GetInt32(0),
                            Supp = reader.GetInt32(1),
                            IdDiler = reader.GetInt32(2),
                            IdProduct = reader.GetInt32(3)
                        });
                    }
                }
                else
                {
                    MessageBox.Show("SUPPLIES Table is Empty");
                }

                SUPPLIES.ItemsSource = sup;
            }

            sql_cmd.CommandText = "select * from ORDERS;";

            using (var reader = sql_cmd.ExecuteReader())
            {
                List<ORDER> ord = new List<ORDER>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ord.Add(new ORDER
                        {
                            IdOrder = reader.GetInt32(0),
                            Summ = reader.GetInt32(1),
                            IdDiler = reader.GetInt32(2),
                            IdClient = reader.GetInt32(3),
                            IdProduct = reader.GetInt32(4)
                        });
                    }
                }
                else
                {
                    MessageBox.Show("ORDERS Table is Empty");
                }

                ORDERS.ItemsSource = ord;
            }



            // П Р Е Д С Т А В Л Е Н И Е 

            ///ExecuteQuery("create view Brest_client as select * from clients where city_client='Брест';");

            ///sql_con.Open();
            sql_cmd.CommandText = "select * from Brest_Client;";

            using (var reader = sql_cmd.ExecuteReader())
            {
                List<BrestVIEW> data = new List<BrestVIEW>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new BrestVIEW
                        {
                            IdClient = reader.GetInt32(0),
                            NameClient = reader.GetString(1),
                            CityClient = reader.GetString(2)
                        });
                    }
                }
                else
                {
                    MessageBox.Show("View is Empty");
                }
                BrestVIEW.ItemsSource = data;
            }
            sql_con.Close();
        }
    }
}
