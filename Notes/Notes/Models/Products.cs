using System;
using System.Numerics;
using SQLite;

namespace InventoryManagementApp.Models
{
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int product_ID { get; set; }
        public string product_name { get; set; }
        public int product_stock { get; set; }
        
        public string product_details { get; set; }

        public long product_barcode { get; set; } 

        public DateTime Date { get; set; }
    }
}