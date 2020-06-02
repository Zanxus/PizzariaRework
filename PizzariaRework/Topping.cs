using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaRework
{
    public class Topping
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Topping(string name, decimal price = 8)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
