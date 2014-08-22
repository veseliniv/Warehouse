using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse.DataLayer;
using Warehouse.ViewModels;

namespace Warehouse.DataPersisters
{
    public class ProductDataPersister
    {
        internal static IEnumerable<ProductViewModel> GetAllProducts()
        {
            WarehouseEntities entity = new WarehouseEntities();

            var allProducts =
                from ap in entity.Products
                select new ProductViewModel()
                       {
                           Name = ap.Name,
                           Quantity = ap.Quantity,
                           BuyPrice = ap.BuyPrice,
                           SellPrice = ap.SellPrice,
                           Vendor = new VendorViewModel()
                                    {
                                        Name = ap.Vendor1.Name
                                    }
                       };

            return allProducts;
        }

        internal static void AddProduct(ProductViewModel product)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var productToAdd =
                (from pta in entity.Products
                where (pta.Name.Contains(product.Name) && pta.Vendor1.Name.Contains(product.Vendor.Name)) ||
                       (pta.Vendor1.Name.Contains(product.Vendor.Name))
                select pta).First();


            if (productToAdd.Name.Equals(product.Name) && productToAdd.Vendor1.Name.Equals(product.Vendor.Name))
            {
                MessageBox.Show("Product " + product.Name + " exists!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (productToAdd.Vendor1.Name.Equals(product.Vendor.Name))
            {
                entity.Products.Add(new Product()
                                    {
                                        Name = product.Name,
                                        BuyPrice = product.BuyPrice,
                                        SellPrice = product.SellPrice,
                                        Quantity = product.Quantity,
                                        Vendor = productToAdd.Vendor
                                    });

                MessageBox.Show("Product " + product.Name + " added!", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                entity.Products.Add(new Product()
                                    {
                                        Name = product.Name,
                                        BuyPrice = product.BuyPrice,
                                        SellPrice = product.SellPrice,
                                        Quantity = product.Quantity,
                                        Vendor1 = new Vendor()
                                                  {
                                                      Name = product.Vendor.Name
                                                  }
                                    });

                MessageBox.Show("Product " + product.Name + " added!", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
        }

        internal static void DeleteProduct(ProductViewModel product)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var productToDelete =
                from ptd in entity.Products
                where ptd.Name.Contains(product.Name) && ptd.Vendor1.Name.Contains(product.Vendor.Name)
                select ptd;

            if (productToDelete.Count() != 0)
            {
                foreach (var item in productToDelete)
                {
                    entity.Products.Remove(item);
                }

                MessageBox.Show("Product " + product.Name + " deleted", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                MessageBox.Show("Product " + product.Name + " doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        internal static void UpdateProduct(ProductViewModel product)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var productToUpdate =
                    from ptu in entity.Products
                    where ptu.Name.Contains(product.Name) && ptu.Vendor1.Name.Contains(product.Vendor.Name)
                    select ptu;

            if (productToUpdate.Count() != 0)
            {
                foreach (var item in productToUpdate)
                {
                    item.Quantity += product.Quantity;
                    item.BuyPrice = product.BuyPrice;
                }

                MessageBox.Show("Product " + product.Name + " updated", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                MessageBox.Show("Product " + product.Name + " doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        internal static void UseProduct(ProductViewModel product)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var productToUse =
                    from ptu in entity.Products
                    where ptu.Name.Contains(product.Name) && ptu.Vendor1.Name.Contains(product.Vendor.Name)
                    select ptu;

            if (productToUse.Count() != 0)
            {
                foreach (var item in productToUse)
                {
                    item.Quantity -= product.Quantity;
                }

                MessageBox.Show("Product " + product.Name + " used", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                MessageBox.Show("Product " + product.Name + " doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
