using System;
using System.Linq;
using System.Text;
using System.Windows;
using Warehouse.DataLayer;
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
