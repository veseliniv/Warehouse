using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse.DataLayer;
using Warehouse.DataPersisters;
using Warehouse.Views;
namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AddNewProductWindow a = new AddNewProductWindow();
            a.Show();
            //ReportWindow a = new ReportWindow();
            //a.Show();
        }

        private void ButtonLoginClick(object sender, RoutedEventArgs e)
        {
            var pass = Convert.ToString(Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(TextBoxPassword.Password))));

            WarehouseEntities entity = new WarehouseEntities();
            try
            {
                var userToLogin =
                    (from utl in entity.Users
                     where utl.Username == TextBoxUsernameLogin.Text && utl.Password == pass
                     select utl).First();

                if (userToLogin != null)
                {
                    switch (userToLogin.Rank)
                    {
                        case "Admin":
                            AdminPanel adminPanelWindow = new AdminPanel();
                            adminPanelWindow.Show();
                            break;

                        case "Worker":
                            TechnicianPanelWindow techPanelWindow = new TechnicianPanelWindow();
                            techPanelWindow.Show();
                            break;
                    }
                    Application.Current.MainWindow.Close();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Wrong username or password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
