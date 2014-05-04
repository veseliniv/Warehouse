using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Warehouse.Views
{
    /// <summary>
    /// Interaction logic for UserLoginWindow.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void ButtonAddNewProductClick(object sender, RoutedEventArgs e)
        {
            AddNewProductWindow addNewProductWindow = new AddNewProductWindow();

            addNewProductWindow.Show();
        }

        private void ButtonShowUsersClick(object sender, RoutedEventArgs e)
        {
            Users usersWindow = new Users();

            usersWindow.Show();
        }

        private void ButtonShowVendorsClick(object sender, RoutedEventArgs e)
        {
            Vendors vendorsWindow = new Vendors();

            vendorsWindow.Show();
        }

        private void ButtonAddNewUserClick(object sender, RoutedEventArgs e)
        {
            AddNewUser newUserWindow = new AddNewUser();
            newUserWindow.Show();
        }

        private void ButtonAddNewVendorClick(object sender, RoutedEventArgs e)
        {
            AddNewVendor newVendor = new AddNewVendor();
            newVendor.Show();
        }
    }
}
