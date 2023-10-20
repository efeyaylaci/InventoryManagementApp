using System;
using System.IO;
using InventoryManagementApp.Data;
using Xamarin.Forms;

namespace InventoryManagementApp
{
    public partial class App : Application
    {
        static Database database;

        // Create the ProductDatabase connection as a singleton.
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InventoryManagementApp.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}