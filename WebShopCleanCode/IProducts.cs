using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public interface IProducts
    {
        string GetName();
        int GetPrice();
        int GetNrInStock();
        bool IsInStock();
        void PrintInfo();
    }
}
