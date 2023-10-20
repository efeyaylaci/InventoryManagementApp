using Xamarin.Forms;
using InventoryManagementApp.Views;

namespace InventoryManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProductEntryPage), typeof(ProductEntryPage));
            Routing.RegisterRoute(nameof(TransactionEntryPage), typeof(TransactionEntryPage));
        }
    }
}