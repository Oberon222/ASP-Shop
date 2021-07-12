using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2.Models.ViewModels
{
    public class CartVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
