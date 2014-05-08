using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Warehouse.Commands;
using Warehouse.DataLayer;
using Warehouse.DataPersisters;
using Warehouse.Views;

namespace Warehouse.ViewModels
{
    public class WarehouseViewModel 
    {
        private ObservableCollection<ProductViewModel> productsViewModels;
        private ProductViewModel newProductViewModel;
        private ICommand addNewProductCommand;
        private ICommand deleteProductCommand;
        private ICommand updateProductCommand;

        private ObservableCollection<VendorViewModel> vendorsViewModels;
        private VendorViewModel newVendorViewModel;
        private ICommand addNewVendorCommand;
        private ICommand deleteVendorCommand;

        private ObservableCollection<UserViewModel> usersViewModels;
        private UserViewModel newUserViewModel;
        private ICommand addNewUserCommand;
        private ICommand deleteUserCommand;
        private ICommand updateUserCommand;
        private ICommand loginUserCommand;

        private ICommand openAddNewProductWindow;
        private ICommand openAddNewUserWindow;
        private ICommand openAddNewVendorWindow;
        private ICommand openUsersWindow;
        private ICommand openVendorsWindow;

        public WarehouseViewModel()
        {
            //this.Products = new ObservableCollection<ProductViewModel>();
            this.newProductViewModel = new ProductViewModel();
            this.newVendorViewModel = new VendorViewModel();
            this.newUserViewModel = new UserViewModel();
        }

        public IEnumerable<ProductViewModel> Products
        {
            get
            {
                if (this.productsViewModels == null)
                {
                    this.Products = ProductDataPersister.GetAllProducts();
                }

                return productsViewModels.ToList();
            }
            set
            {
                if (this.productsViewModels == null)
                {
                    this.productsViewModels = new ObservableCollection<ProductViewModel>();
                }
                this.productsViewModels.Clear();
                foreach (var item in value)
                {
                    this.productsViewModels.Add(item);
                }
            }
        }

        public ProductViewModel NewProduct
        {
            get
            {
                return this.newProductViewModel;
            }
            set
            {
                this.newProductViewModel = value;
            }
        }

        public ICommand AddNewProduct
        {
            get
            {
                if (this.addNewProductCommand == null)
                {
                    this.addNewProductCommand = new RelayCommand(this.HandleAddNewProductCommand);
                }
                return this.addNewProductCommand;
            }
        }

        public ICommand DeleteProduct
        {
            get
            {
                if (this.deleteProductCommand == null)
                {
                    this.deleteProductCommand = new RelayCommand(this.HandleDeleteProductCommand);
                }
                return this.deleteProductCommand;
            }
        }

        public ICommand UpdateProduct
        {
            get
            {
                if (this.updateProductCommand == null)
                {
                    this.updateProductCommand = new RelayCommand(this.HandleUpdateProductCommand);
                }
                return this.updateProductCommand;
            }
        }

        private void HandleAddNewProductCommand(object obj)
        {
            try
            {
                ProductDataPersister.AddProduct(this.NewProduct);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleDeleteProductCommand(object obj)
        {
            try
            {
                ProductDataPersister.DeleteProduct(this.NewProduct);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleUpdateProductCommand(object obj)
        {
            try
            {
                ProductDataPersister.UpdateProduct(this.NewProduct);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public IEnumerable<VendorViewModel> Vendors
        {
            get
            {
                if (this.vendorsViewModels == null)
                {
                    this.Vendors = VendorDataPersister.GetAllVendors();
                }

                return vendorsViewModels.ToList();
            }
            set
            {
                if (this.vendorsViewModels == null)
                {
                    this.vendorsViewModels = new ObservableCollection<VendorViewModel>();
                }
                this.vendorsViewModels.Clear();
                foreach (var item in value)
                {
                    this.vendorsViewModels.Add(item);
                }
            }
        }

        public VendorViewModel NewVendor
        {
            get
            {
                return this.newVendorViewModel;
            }
            set
            {
                this.newVendorViewModel = value;
            }
        }

        public ICommand AddNewVendor
        {
            get
            {
                if (this.addNewVendorCommand == null)
                {
                    this.addNewVendorCommand = new RelayCommand(this.HandleAddNewVendorCommand);
                }
                return this.addNewVendorCommand;
            }
        }

        public ICommand DeleteVendor
        {
            get
            {
                if (this.deleteVendorCommand == null)
                {
                    this.deleteVendorCommand = new RelayCommand(this.HandleDeleteVendorCommand);
                }
                return this.deleteVendorCommand;
            }
        }

        private void HandleAddNewVendorCommand(object obj)
        {
            try
            {
                VendorDataPersister.AddVendor(this.NewVendor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleDeleteVendorCommand(object obj)
        {
            try
            {
                VendorDataPersister.DeleteVendor(this.NewVendor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public IEnumerable<UserViewModel> Users
        {
            get
            {
                if (this.usersViewModels == null)
                {
                    this.Users = UserDataPersister.GetAllUsers();
                }

                return usersViewModels.ToList();
            }
            set
            {
                if (this.usersViewModels == null)
                {
                    this.usersViewModels = new ObservableCollection<UserViewModel>();
                }
                this.usersViewModels.Clear();
                foreach (var item in value)
                {
                    this.usersViewModels.Add(item);
                }
            }
        }

        public UserViewModel NewUser
        {
            get
            {
                return this.newUserViewModel;
            }
            set
            {
                this.newUserViewModel = value;
            }
        }

        public ICommand AddNewUser
        {
            get
            {
                if (this.addNewUserCommand == null)
                {
                    this.addNewUserCommand = new RelayCommand(this.HandleAddNewUserCommand);
                }
                return this.addNewUserCommand;
            }
        }

        public ICommand DeleteUser
        {
            get
            {
                if (this.deleteUserCommand == null)
                {
                    this.deleteUserCommand = new RelayCommand(this.HandleDeleteUserCommand);
                }
                return this.deleteUserCommand;
            }
        }

        public ICommand UpdateUser
        {
            get
            {
                if (this.updateUserCommand == null)
                {
                    this.updateUserCommand = new RelayCommand(this.HandleUpdateUserCommand);
                }
                return this.updateUserCommand;
            }
        }

        public ICommand LoginUser
        {
            get
            {
                if (this.loginUserCommand == null)
                {
                    this.loginUserCommand = new RelayCommand(this.HandleLoginUserCommand);
                }
                return this.loginUserCommand;
            }
        }

        private void HandleLoginUserCommand(object obj)
        {
            try
            {
                UserDataPersister.UserLogin(this.NewUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleAddNewUserCommand(object obj)
        {
            try
            {
                UserDataPersister.AddUser(this.NewUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleDeleteUserCommand(object obj)
        {
            try
            {
                UserDataPersister.DeleteUser(this.NewUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleUpdateUserCommand(object obj)
        {
            try
            {
                UserDataPersister.UpdateUser(this.NewUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public ICommand AddNewProductWindow
        {
            get
            {
                if (this.openAddNewProductWindow == null)
                {
                    this.openAddNewProductWindow = new RelayCommand(this.HandleAddNewProductWindowCommand);
                }
                return this.openAddNewProductWindow;
            }
        }

        public ICommand AddNewUserWindow
        {
            get
            {
                if (this.openAddNewUserWindow == null)
                {
                    this.openAddNewUserWindow = new RelayCommand(this.HandleAddNewUserWindowCommand);
                }
                return this.openAddNewUserWindow;
            }
        }

        public ICommand AddNewVendorWindow
        {
            get
            {
                if (this.openAddNewVendorWindow == null)
                {
                    this.openAddNewVendorWindow = new RelayCommand(this.HandleAddNewVendorWindowCommand);
                }
                return this.openAddNewVendorWindow;
            }
        }

        public ICommand UsersWindow
        {
            get
            {
                if (this.openUsersWindow == null)
                {
                    this.openUsersWindow = new RelayCommand(this.HandleUsersWindowCommand);
                }
                return this.openUsersWindow;
            }
        }

        public ICommand VendorsWindow
        {
            get
            {
                if (this.openVendorsWindow == null)
                {
                    this.openVendorsWindow = new RelayCommand(this.HandleVendorsWindowCommand);
                }
                return this.openVendorsWindow;
            }
        }

        private void HandleVendorsWindowCommand(object obj)
        {
            Vendors vendorsWindow = new Vendors();

            vendorsWindow.Show();
        }

        private void HandleUsersWindowCommand(object obj)
        {
            Users usersWindow = new Users();

            usersWindow.Show();
        }

        private void HandleAddNewVendorWindowCommand(object obj)
        {
            AddNewVendor newVendor = new AddNewVendor();
            newVendor.Show();
        }

        private void HandleAddNewUserWindowCommand(object obj)
        {
            AddNewUser newUserWindow = new AddNewUser();
            newUserWindow.Show();
        }

        private void HandleAddNewProductWindowCommand(object obj)
        {
            AddNewProductWindow addNewProductWindow = new AddNewProductWindow();

            addNewProductWindow.Show();
        }
    }
}
