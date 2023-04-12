using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class Database
    {
        // We just pretend this accesses a real database.
        private List<ProductProxy> productsInDatabase;
        private List<Customer> customersInDatabase;

        public Product GetProduct(string name, int price, int nrInStock)
        {
            return new Product(name, price, nrInStock);
        }
        public Database()
        {
            productsInDatabase = new List<ProductProxy>();
            productsInDatabase.Add(new ProductProxy("Mirror", 300, 2, this));
            productsInDatabase.Add(new ProductProxy("Car", 2000000, 2, this));
            productsInDatabase.Add(new ProductProxy("Candle", 50, 2, this));
            productsInDatabase.Add(new ProductProxy("Computer", 100000, 2, this));
            productsInDatabase.Add(new ProductProxy("Game", 599, 2, this));
            productsInDatabase.Add(new ProductProxy("Painting", 399, 2, this));
            productsInDatabase.Add(new ProductProxy("Chair", 500, 2, this));
            productsInDatabase.Add(new ProductProxy("Table", 1000, 2, this));
            productsInDatabase.Add(new ProductProxy("Bed", 20000, 2, this));

            customersInDatabase = new List<Customer>();
            customersInDatabase.Add(new Customer("jimmy", "jimisthebest", "Jimmy", "Jamesson", "jj@mail.com", 22, "Big Street 5", "123456789"));
            customersInDatabase.Add(new Customer("jake", "jake123", "Jake", null, null, 0, null, null));
        }

        public List<ProductProxy> GetProducts()
        {
            return productsInDatabase;
        }

        public List<Customer> GetCustomers()
        {
            return customersInDatabase;
        }
    }
}
