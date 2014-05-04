using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse.DataLayer;
using Warehouse.ViewModels;

namespace Warehouse.DataPersisters
{
    public class VendorDataPersister
    {
        public static IEnumerable<VendorViewModel> GetAllVendors()
        {
            WarehouseEntities entity = new WarehouseEntities();

            var allVendors =
                from av in entity.Vendors
                select new VendorViewModel()
                       {
                           Name = av.Name
                       };

            return allVendors;
        }

        internal static void AddVendor(VendorViewModel vendor)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var vendorToAdd =
                from vta in entity.Vendors
                where vta.Name.Contains(vendor.Name)
                select vta;

            if (vendorToAdd.Count()!=0)
            {
                MessageBox.Show("Vendor " + vendor.Name + " exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                entity.Vendors.Add(new Vendor()
                                   {
                                       Name = vendor.Name
                                   });

                MessageBox.Show("Vendor " + vendor.Name + " added", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
        }

        internal static void DeleteVendor(VendorViewModel vendor)
        {
            WarehouseEntities entity = new WarehouseEntities();

            var vendorToDelete =
                (from vtd in entity.Vendors
                where vtd.Name==vendor.Name
                select vtd).First();

            if (vendorToDelete!=null)
            {
                var del = vendorToDelete.Products;
                foreach (var item in del.ToList())
                {
                    entity.Products.Remove(item);
                    //entity.SaveChanges();
                }
                
                entity.Vendors.Remove(vendorToDelete);

                MessageBox.Show("Vendor " + vendor.Name + " deleted", "Confirmation", MessageBoxButton.OK);

                entity.SaveChanges();
            }
            else
            {
                MessageBox.Show("Vendor " + vendor.Name + " doesn't exist", "Confirmation", MessageBoxButton.OK);
            }
        }
    }
}
