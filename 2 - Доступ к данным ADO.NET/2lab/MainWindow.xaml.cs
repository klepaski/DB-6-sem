using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2lab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string connStr = @"Data Source=USER-ПК;Initial Catalog=Sklad;Integrated Security=True";
        //Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        DataTable Clients = new DataTable();
        DataTable Dilers = new DataTable();
        DataTable Orders = new DataTable();
        DataTable Products = new DataTable();
        DataTable Supplies = new DataTable();

        #region Clients

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxNameClient.Text;
            string city = textBoxCityClient.Text;

            if (name.Length == 0 || city.Length == 0)
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.add_client(name, city);
                db.closeConnection();
            }
        }

        private void dropClient_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdClient.Text);
            if (textBoxIdClient.Text == null)
            {
                MessageBox.Show("Введите ID клиента");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.drop_client(id);
                db.closeConnection();
            }
        }

        private void changeClient_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdClient.Text);
            string name = textBoxNameClient.Text;
            string city = textBoxCityClient.Text;
            if (name.Length == 0 || city.Length == 0)
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.change_client(id, name, city);
                db.closeConnection();
            }
        }

        private void allClients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlExpression = "getAllClients";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Clients.Clear();
                    // Заполняем Dataset
                    command.Fill(Clients);
                    // Отображаем данные
                    usersGrid.ItemsSource = Clients.DefaultView;
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        #endregion Clients
        

        #region Products
        //id продукта, название, количество

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxProductName.Text;
            int count = Convert.ToInt32(textBoxProductCount.Text);

            if (name.Length == 0 || textBoxProductCount.Text == "")
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.add_product(name, count);
                db.closeConnection();
            }
        }

        private void dropProduct_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdProduct.Text);
            if (textBoxIdProduct.Text == null)
            {
                MessageBox.Show("Введите ID продукта");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.drop_product(id);
                db.closeConnection();
            }
        }

        private void changeProduct_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdProduct.Text);
            string name = textBoxProductName.Text;
            int count = Convert.ToInt32(textBoxProductCount.Text);

            
                DB db = new DB();
                db.openConnection(connStr);
                db.change_product(id, name, count);
                db.closeConnection();
        }

        private void allProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlExpression = "getAllProducts";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Products.Clear();
                    // Заполняем Dataset
                    command.Fill(Products);
                    // Отображаем данные
                    ProductsGrid.ItemsSource = Products.DefaultView;
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        #endregion Products


        #region Orders

    //	@id_order int,  @id_diler int, @id_client int, @id_product int, @summ int, @order_date date

    private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            //int id_order = Convert.ToInt32(textBoxIdOrder_order.Text);
            int id_diler = Convert.ToInt32(textBoxIdDiler_order.Text);
            int id_client = Convert.ToInt32(textBoxIdClient_order.Text);
            int id_product = Convert.ToInt32(textBoxIdProduct_order.Text);
            int summ = Convert.ToInt32(textBoxProductCount_order.Text);
            DateTime order_date = Convert.ToDateTime(DateOrder.Text);
            
            
                DB db = new DB();
                db.openConnection(connStr);
                db.add_order(id_diler, id_client, id_product, summ, order_date);
                db.closeConnection();
        }

        private void dropOrder_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdOrder_order.Text);
            if (textBoxIdProduct.Text == null)
            {
                MessageBox.Show("Введите ID продукта");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.drop_order(id);
                db.closeConnection();
            }
        }

        private void changeOrder_Click(object sender, RoutedEventArgs e)
        {
            int id_order = Convert.ToInt32(textBoxIdOrder_order.Text);
            int id_diler = Convert.ToInt32(textBoxIdDiler_order.Text);
            int id_client = Convert.ToInt32(textBoxIdClient_order.Text);
            int id_product = Convert.ToInt32(textBoxIdProduct_order.Text);
            int summ = Convert.ToInt32(textBoxProductCount_order.Text);
            DateTime order_date = Convert.ToDateTime(DateOrder.Text);

            DB db = new DB();
                db.openConnection(connStr);
                db.change_order(id_order, id_diler, id_client, id_product, summ, order_date);
                db.closeConnection();
        }

        private void allOrders_Click(object sender, RoutedEventArgs e)
        {
                string sqlExpression = "getAllOrders";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Orders.Clear();
                    // Заполняем Dataset
                    command.Fill(Orders);
                    // Отображаем данные
                    ordersGrid.ItemsSource = Orders.DefaultView;
                    connection.Close();
                }
        }

        #endregion Orders

           
        #region Supplies
        //id supply, siler, product, supp

        private void addSupply_Click(object sender, RoutedEventArgs e)
        {
            int id_diler = Convert.ToInt32(textBoxIdDiler_supply.Text);
            int id_product = Convert.ToInt32(textBoxIdProduct_supply.Text);
            int supp = Convert.ToInt32(textBoxSupplyCount.Text);
            
                DB db = new DB();
                db.openConnection(connStr);
                db.add_supply(id_diler, id_product, supp);
                db.closeConnection();
        }

        private void dropSupply_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdSupply.Text);
            if (textBoxIdSupply.Text == null)
            {
                MessageBox.Show("Введите ID поставки");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.drop_supply(id);
                db.closeConnection();
            }
        }

        private void changeSupply_Click(object sender, RoutedEventArgs e)
        {
            int id_supply = Convert.ToInt32(textBoxIdSupply.Text);
            int id_diler = Convert.ToInt32(textBoxIdDiler_supply.Text);
            int id_product = Convert.ToInt32(textBoxIdProduct_supply.Text);
            int supp = Convert.ToInt32(textBoxSupplyCount.Text);

                DB db = new DB();
                db.openConnection(connStr);
                db.change_supply(id_supply, id_diler, id_product, supp);
                db.closeConnection();
        }

        private void allSupplies_Click(object sender, RoutedEventArgs e)
        {
                string sqlExpression = "getAllSupplies";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Supplies.Clear();
                    // Заполняем Dataset
                    command.Fill(Supplies);
                    // Отображаем данные
                    supplyGrid.ItemsSource = Supplies.DefaultView;
                    connection.Close();
                }
        }

        #endregion Supplies

            
        #region Dilers
        //id diler, name, city, phone

        private void addDiler_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxNameDiler.Text;
            string city = textBoxCityDiler.Text;
            string phone = textBoxPhoneDiler.Text;
            
                DB db = new DB();
                db.openConnection(connStr);
                db.add_diler(name, city, phone);
                db.closeConnection();
        }

        private void dropDiler_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdDiler.Text);
            if (textBoxIdDiler.Text == null)
            {
                MessageBox.Show("Введите ID поставщика");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.drop_diler(id);
                db.closeConnection();
            }
        }

        private void changeDiler_Click(object sender, RoutedEventArgs e)
        {
            int id_diler = Convert.ToInt32(textBoxIdDiler.Text);
            string name = textBoxNameDiler.Text;
            string city = textBoxCityDiler.Text;
            string phone = textBoxPhoneDiler.Text;
            
                DB db = new DB();
                db.openConnection(connStr);
                db.change_diler(id_diler, name, city, phone);
                db.closeConnection();
        }

        private void allDilers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlExpression = "getAllDilers";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Dilers.Clear();
                    // Заполняем Dataset
                    command.Fill(Dilers);
                    // Отображаем данные
                    dilerGrid.ItemsSource = Dilers.DefaultView;
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        #endregion Dilers


        private void spisokOrders_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("spisok_orders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datestart", SqlDbType.Date).Value = DateStart.SelectedDate.Value;
                    cmd.Parameters.AddWithValue("@dateend", SqlDbType.Date).Value = DateEnd.SelectedDate.Value;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    ordersGrid.ItemsSource = dt.DefaultView;
                    dataAdapter.Update(dt);

                    con.Close();
                }

            }
        }
    }
}
