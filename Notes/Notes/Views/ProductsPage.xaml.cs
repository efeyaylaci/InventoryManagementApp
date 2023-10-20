using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InventoryManagementApp.Data;
using InventoryManagementApp.Models;
using Xamarin.Forms;
using System.Globalization;


namespace InventoryManagementApp.Views
{
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //base.OnAppearing();

            // Retrieve all the notes from the ProductDatabase, and set them as the
            // data source for the CollectionView.
            Products_collectionView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the ProductEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(ProductEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the ProductEntryPage, passing the ID as a query parameter.
                Products product = (Products)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(ProductEntryPage)}?{nameof(ProductEntryPage.ItemId)}={product.product_ID.ToString()}");
            }
        }

        async void products_searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            var getList = await App.Database.GetProductsAsync();
             
            /*
            string data = products_searchBar.Text;
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            products_searchBar.Text = myTI.ToTitleCase(data);
            */

            var filteredList = getList.Where(a => a.product_name.StartsWith(e.NewTextValue));
            Products_collectionView.ItemsSource = filteredList;
        }

    }
}