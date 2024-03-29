﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class Product : IProducts
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int NrInStock { get; set; }
        public Product(string name, int price, int nrInStock)
        {
            Name = name;
            Price = price;
            NrInStock = nrInStock;
        }
        public bool InStock()
        {
            return NrInStock > 0;
        }
        public void PrintInfo()
        {
            Console.WriteLine(Name + ": " + Price + "kr, " + NrInStock + " in stock.");
        }
        public string GetName()
        {
            return Name;
        }

        public int GetPrice()
        {
            return Price;
        }

        public int GetNrInStock()
        {
            return NrInStock;
        }

        public bool IsInStock()
        {
            return NrInStock > 0;
        }
    }
}
