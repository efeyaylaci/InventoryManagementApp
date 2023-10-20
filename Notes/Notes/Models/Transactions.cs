using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace InventoryManagementApp.Models
{
    public class Transactions
    {
        [PrimaryKey,AutoIncrement]
        public int transaction_ID { get; set; }
        public int? transaction_productID { get; set; }
        public string product_name { get; set; }
        public int product_stock { get; set; }
        public int product_stock_change { get; set; }
        public int payment { get; set; }
        public DateTime Date { get; set; }
    }
}
