using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class ProductProxy : IProducts
    {
        string Name;
        int Price;
        int NrInStock;
        Product Product;
        Database database;

        public ProductProxy(string name, int price, int nrInStock, Database database)
        {
            this.Name = name;
            this.Price = price;
            NrInStock = nrInStock;
            this.database = database;
        }

        public void LoadedCheck()
        {
            if (Product != null)
            {
                Console.WriteLine(Name + "Loaded");
                Product = database.GetProduct(Name, Price, NrInStock);
            }
        }

        public string GetName()
        {
            LoadedCheck();
            return Product.GetName();
        }

        public int GetNrInStock()
        {
            LoadedCheck();
            return Product.GetNrInStock();
        }

        public int GetPrice()
        {
            LoadedCheck();
            return Product.GetPrice();
        }

        public bool IsInStock()
        {
            return GetNrInStock() > 0;
        }

        public void RemoveFromStock()
        {
            LoadedCheck();
            Product.NrInStock--;
        }


        public void PrintInfo()
        {
            Console.WriteLine(GetName() + ": " + GetPrice() + "kr " + GetNrInStock() + "in stock!");
        }

    }
}
