using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Warehouse.Views
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            datePickerPurchase.SelectedDate = DateTime.Now.Date;
            datePickerSale.SelectedDate = DateTime.Now.Date;
        }

        private void PurchasesExportClick(object sender, RoutedEventArgs e)
        {
            DataGridPurchase.SelectAllCells();
            DataGridPurchase.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DataGridPurchase);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            DataGridPurchase.UnselectAllCells();

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel files (*.xls)|*.xls";
            saveFile.FileName = "Purchases" + DateTime.Now.ToShortDateString();
            if(saveFile.ShowDialog()==true)
            {
                File.WriteAllText(saveFile.FileName, result);
                MessageBox.Show("Exporting DataGrid data to Excel file created");
            }
            
        }

        private void SalesExportClick(object sender, RoutedEventArgs e)
        {
            DataGridSale.SelectAllCells();
            DataGridSale.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DataGridSale);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            DataGridSale.UnselectAllCells();

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel files (*.xls)|*.xls";
            saveFile.FileName = "Sales" + DateTime.Now.ToShortDateString();
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, result);
                MessageBox.Show("Exporting DataGrid data to Excel file created");
            }

            
        }
    }
}
