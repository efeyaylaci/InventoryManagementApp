using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using InventoryManagementApp.Models;

namespace InventoryManagementApp.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Products>().Wait();
            database.CreateTableAsync<Transactions>().Wait();
        }

        //Get all product.
        public Task<List<Products>> GetProductsAsync()
        {
            return database.Table<Products>().ToListAsync();
        }

        //Get all transactions.
        public Task<List<Transactions>> GetTransactionsAsync()
        {
            return database.Table<Transactions>().ToListAsync();
        }

        // Get a specific product.
        public Task<Products> GetProductAsync(int id)
        {
            return database.Table<Products>()
                            .Where(i => i.product_ID == id)
                            .FirstOrDefaultAsync();
        }

        // Get a specific transaction.
        public Task<Transactions> GetTransactionAsync(int id)
        {
            return database.Table<Transactions>()
                            .Where(i => i.transaction_ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveProductAsync(Products product)
        {
            if (product.product_ID != 0)
            {
                // Update an existing product.
                return database.UpdateAsync(product);
            }
            else
            {
                // Save a new product.
                return database.InsertAsync(product);
            }
        }

        public Task<int> SaveTransactionAsync(Transactions transaction)
        {
            if (transaction.transaction_ID != 0)
            {
                // Update an existing transaction.
                return database.UpdateAsync(transaction);
            }
            else
            {
                // Save a new transaction.
                return database.InsertAsync(transaction);
            }
        }

        // Delete a product.
        public Task<int> DeleteProductAsync(Products product)
        {
            return database.DeleteAsync(product);
        }

        // Delete a transaction.
        public Task<int> DeleteTransactionAsync(Transactions transaction)
        {
            return database.DeleteAsync(transaction);
        }
    }
}