using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InventoryManagementApp.Models;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using InventoryManagementApp.Data;
using System.Transactions;


namespace InventoryManagementApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]

    public partial class TransactionEntryPage : ContentPage
    {
        public bool isEditing;
        public int transactionBefore;

        public string ItemId
        {
            set
            {
                LoadTransaction(value);
            }
        }

        public TransactionEntryPage()
        {
            InitializeComponent();

            fillPicker();

            BindingContext = new Transactions();
        }

        async void fillPicker()
        {
            Picker.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void LoadTransaction(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the product and set it as the BindingContext of the page.
                Transactions transaction = await App.Database.GetTransactionAsync(id);
                
                //set picker for editing transactions;
                if(!string.IsNullOrWhiteSpace(transaction.product_name))
                {
                    Picker.ItemsSource = null;
                    Picker.Items.Add(transaction.product_name);
                    Picker.SelectedIndex = 0;

                    isEditing = true;
                    transactionBefore = transaction.product_stock_change;
                }

                BindingContext = transaction;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var transaction = (Transactions)BindingContext;
            transaction.Date = DateTime.UtcNow;
            Products product;
            //Products product = await App.Database.GetProductAsync(Picker.SelectedIndex+1);

            if (transaction.transaction_productID == null)
            {
                product = await App.Database.GetProductAsync(Picker.SelectedIndex + 1);
                transaction.transaction_productID = product.product_ID;
            }
            else
            {
                product = await App.Database.GetProductAsync(transaction.transaction_productID.Value);
            }

            if (Picker.SelectedIndex != -1 && transaction.product_stock_change != 0)
            {

                if (isEditing)
                {
                    product.product_stock = product.product_stock + transaction.product_stock_change - transactionBefore;
                }
                else
                {
                    transaction.product_name = product.product_name;
                    product.product_stock = product.product_stock + transaction.product_stock_change;
                }
                await App.Database.SaveProductAsync(product);
                await App.Database.SaveTransactionAsync(transaction);
            }
            else
            {
                Console.WriteLine("Failed to Save. Please enter your transaction correctly.");
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var transaction = (Transactions)BindingContext;

            Products product = await App.Database.GetProductAsync(transaction.transaction_productID.Value);
            product.product_stock = product.product_stock - transaction.product_stock_change;

            await App.Database.SaveProductAsync(product);
            await App.Database.DeleteTransactionAsync(transaction);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }    
}