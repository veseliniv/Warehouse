﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                           DayOfPurchase = ap.DayOfPurchase,
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
                from pta in entity.Products
                where ((pta.Name == product.Name) && (pta.Vendor1.Name == product.Vendor.Name)) 
                select pta;

            if (productToAdd.Count() != 0)
            {

                UpdateProduct(product);

            }
            else
            {
                var productVendor =
                    (from pv in entity.Vendors
                    where pv.Name == product.Vendor.Name
                    select pv).First();

                entity.Products.Add(new Product()
                                    {
                                        Name = product.Name,
                                        BuyPrice = product.BuyPrice,
                                        SellPrice = product.SellPrice,
                                        Quantity = product.Quantity,
                                        DayOfPurchase = DateTime.Now,
                                        Vendor = productVendor.VendorID
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
                    item.DayOfSale = DateTime.Now;
                    if (item.SaleQuantity == null)
                    {
                        item.SaleQuantity = product.Quantity;
                    }
                    else
                    {
                        item.SaleQuantity += product.Quantity;
                    }
                }

                MessageBox.Show("Product " + product.Name + " used", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                MessageBox.Show("Product " + product.Name + " doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        internal static IEnumerable<ProductViewModel> GetProductReportPurchases(ProductViewModel product)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var productReportPurchases =
                (from prp in entity.Products
                where prp.DayOfPurchase.Day.Equals(product.DayOfPurchase.Day)
                      && prp.DayOfPurchase.Month.Equals(product.DayOfPurchase.Month)
                      && prp.DayOfPurchase.Year.Equals(product.DayOfPurchase.Year)
                select new ProductViewModel()
                       {
                           Name = prp.Name,
                           Quantity = prp.Quantity,
                           BuyPrice = prp.BuyPrice,
                           SellPrice = prp.SellPrice,
                           DayOfPurchase = prp.DayOfPurchase,
                           Vendor = new VendorViewModel()
                           {
                               Name = prp.Vendor1.Name
                           }
                       }).ToList();

            return productReportPurchases; 
        }

        internal static IEnumerable<ProductViewModel> GetProductReportSales(ProductViewModel product)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var productReportSales =
                (from prs in entity.Products
                 where prs.DayOfSale.Day.Equals(product.DayOfSale.Day)
                       && prs.DayOfSale.Month.Equals(product.DayOfSale.Month)
                       && prs.DayOfSale.Year.Equals(product.DayOfSale.Year)
                 select new ProductViewModel()
                 {
                     Name = prs.Name,
                     Quantity = prs.Quantity,
                     SaleQuantity = prs.SaleQuantity,
                     BuyPrice = prs.BuyPrice,
                     SellPrice = prs.SellPrice,
                     DayOfSale = prs.DayOfSale,
                     Vendor = new VendorViewModel()
                     {
                         Name = prs.Vendor1.Name
                     }
                 }).ToList();

            return productReportSales;
        }
    }
}
