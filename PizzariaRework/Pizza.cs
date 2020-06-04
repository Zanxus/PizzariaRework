using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaRework
{
    public class Pizza : IOrderable
    {
        public int PizzaID { get; set; }
        public string Name { get; set; }
        public PizzaSize Size { get; set; }
        public PizzaDough Dough { get; set; }
        public PizzaSauce Sauce { get; set; }
        private decimal price;
        public decimal Price
        {
            get { return CalculatePrice(); }
            set { price = value; }
        }
        public ObservableCollection<Topping> Toppings { get; set; }
        public Pizza(int pizzaID, string name, PizzaSize size, PizzaDough dough, PizzaSauce sauce, ObservableCollection<Topping> toppings)
        {
            this.PizzaID = pizzaID;
            this.Name = name;
            this.Size = size;
            this.Dough = dough;
            this.Sauce = sauce;
            this.Toppings = toppings;
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

        public decimal CalculatePrice()
        {
            decimal price = 30;
            if (Size == PizzaSize.Familiy)
            {
                price *= 2;
            };
                price += Toppings.Count*8;

            return price;
        }
    }
}