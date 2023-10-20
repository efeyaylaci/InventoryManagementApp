using System;
using System.ComponentModel.Design;
using System.IO;
using InventoryManagementApp.Models;
using Xamarin.Forms;

namespace InventoryManagementApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class ProductEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadProduct(value);
            }
        }

        public ProductEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.

            BindingContext = new Products();
        }

        async void LoadProduct(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the product and set it as the BindingContext of the page.
                Products product = await App.Database.GetProductAsync(id);
                BindingContext = product;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load product.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {


            var product = (Products)BindingContext;
            product.Date = DateTime.UtcNow;

            if (product.product_ID == 0)
            {
                long random = LongRandom(100000000000, 1000000000000, new Random());
                product.product_barcode = random;
            }
           

            if (string.IsNullOrWhiteSpace(product.product_name))
            {
                Console.WriteLine("Failed to Save. Please Enter Your Product Name Correctly.");
            }            
            else
            {
                await App.Database.SaveProductAsync(product);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        //Generate random barcode number

        long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (Products)BindingContext;
            await App.Database.DeleteProductAsync(product);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}