using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaRework
{
    public class Pizza
    {
        public int PizzaID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PizzaSize Size { get; set; }
        public PizzaDough Dough { get; set; }
        public PizzaSauce Sauce { get; set; }
        public ObservableCollection<Topping> Toppings { get; set; }
        public Pizza(int pizzaID, string name, PizzaSize size, PizzaDough dough, PizzaSauce sauce, ObservableCollection<Topping> toppings, decimal price)
        {
            this.PizzaID = pizzaID;
            this.Name = name;
            this.Size = size;
            this.Dough = dough;
            this.Sauce = sauce;
            this.Toppings = toppings;
            this.Price = price;
        }

        public Pizza(int pizzaID, string name, PizzaSize size, PizzaDough dough, PizzaSauce sauce)
        {
            this.PizzaID = pizzaID;
            this.Name = name;
            this.Size = size;
            this.Dough = dough;
            this.Sauce = sauce;
        }

        public enum PizzaSize
        {
            Normal, Familiy
        }
        public enum PizzaDough
        {
            Wheat, WholeWheat, DeepDish
        }
        public enum PizzaSauce
        {
            Tomato, Taco, NoSauce
        }

        private decimal PizzaPrice()
        {
            decimal price = 60;
            if (Size == PizzaSize.Familiy)
            {
                price *= 2;
            };
            if (this.Toppings.Count > 5)
            {
                price += 8;
            }
            return price;
        }
    }
}