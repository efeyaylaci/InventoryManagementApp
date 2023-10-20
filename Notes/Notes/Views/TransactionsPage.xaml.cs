using System;
using System.IO;
using InventoryManagementApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManagementApp.Views
{
    public partial class TransactionsPage : ContentPage
    {       
        public TransactionsPage()
        {
            InitializeComponent();

            //BindingContext = new Transactions();

        }

        protected override async void OnAppearing()
        {

            // Retrieve all the notes from the Database, and set them as the
            // data source for the CollectionView.

            Transactions_collectionView.ItemsSource = await App.Database.GetTransactionsAsync();
        }

        async void OnAddClickedT(object sender, EventArgs e)
        {
            // Navigate to the ProductEntryPage, without passing any data.

            await Shell.Current.GoToAsync(nameof(TransactionEntryPage));
        }

        async void OnSelectionChangedT(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the ProductEntryPage, passing the ID as a query parameter.
                Transactions transaction = (Transactions)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(TransactionEntryPage)}?{nameof(TransactionEntryPage.ItemId)}={transaction.transaction_ID.ToString()}");
            }
        }

        async void transactions_searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var getList = await App.Database.GetTransactionsAsync();

            var filteredList = getList.Where(a => a.product_name.StartsWith(e.NewTextValue));
            Transactions_collectionView.ItemsSource = filteredList;
        }
    }
}