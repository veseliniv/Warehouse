using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse.DataLayer;
using Warehouse.ViewModels;
using Warehouse.Views;

namespace Warehouse.DataPersisters
{
    public class UserDataPersister
    {
        public static IEnumerable<UserViewModel> GetAllUsers()
        {
            WarehouseEntities entity = new WarehouseEntities();

            var allUsers =
                from au in entity.Users
                select new UserViewModel()
                {
                    Username = au.Username,
                    Rank = au.Rank
                };

            return allUsers;
        }

        public static void AddUser(UserViewModel user)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var userToAdd =
                from uta in entity.Users
                where uta.Username==user.Username
                select uta;

            if (userToAdd.Count()!=0)
            {
                MessageBox.Show("User " + user.Username + " exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                entity.Users.Add(new User()
                                     {
                                         Username = user.Username,
                                         Password = Convert.ToBase64String(System.Security.Cryptography.MD5.Create().
                                                    ComputeHash(Encoding.UTF8.GetBytes(user.Password))),
                                         Rank = user.Rank
                                     });

                    entity.SaveChanges();
                    MessageBox.Show("User " + user.Username + " added", "Confirmation", MessageBoxButton.OK);
            }
        }

        public static void DeleteUser(UserViewModel user)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var userToDelete =
                from utd in entity.Users
                where utd.Username.Contains(user.Username)
                select utd;

            if (userToDelete.Count() != 0)
            {
                foreach (var item in userToDelete)
                {
                    entity.Users.Remove(item);
                }

                entity.SaveChanges();

                MessageBox.Show("User " + user.Username + " deleted", "Confirmation", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("User " + user.Username + " doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        public static void UpdateUser(UserViewModel user)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var userToUpdate =
                from utu in entity.Users
                where utu.Username==user.Username
                select utu;

            if (userToUpdate.Count() != 0)
            {
                foreach (var item in userToUpdate)
                {
                    item.Rank = user.Rank;
                }

                MessageBox.Show("User " + user.Username + " updated", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                MessageBox.Show("User " + user.Username + " doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        public static void UserLogin(UserViewModel user)
        {
            var pass = Convert.ToString(Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password))));

            WarehouseEntities entity = new WarehouseEntities();

            var userToLogin =
                (from utl in entity.Users
                where utl.Username == user.Username && utl.Password == pass
                select utl).First();

            if(userToLogin!=null)
            {
                switch(userToLogin.Rank)
                {
                    case "Admin": 
                        AdminPanel adminPanelWindow = new AdminPanel();
                        adminPanelWindow.Show();
                        break;

                    case "Worker":
                        TechnicianPanelWindow techPanelWindow = new TechnicianPanelWindow();
                        techPanelWindow.Show();
                        break;

                    case "Trainee":
                        TraineePanelWindow traineePanelWindow=new TraineePanelWindow();
                        traineePanelWindow.Show();
                        break;
                }
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
