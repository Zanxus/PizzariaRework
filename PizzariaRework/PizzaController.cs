using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaRework
{
    public static class PizzaController
    {
        //CRUD Class
        private static ObservableCollection<Pizza> pizzaList = new ObservableCollection<Pizza>();

        internal static ObservableCollection<Pizza> PizzaList { get => pizzaList; set => pizzaList = value; }

        public static ObservableCollection<Topping> ToppingList = new ObservableCollection<Topping> { new Topping("Cheese"), new Topping("Oregano"), new Topping("Ham"),
                                                                                                      new Topping("Kebarb"), new Topping("Pepperoni"), new Topping("Dressing"),
                                                                                                      new Topping("Mushrooms"), new Topping("Shrimp")};
            

        //CreatePizza -Makes the Pizza using the Pizza Class Constructor and addeds to the list of pizzas
        public static void CreatePizza(int pizzaID, string name, Pizza.PizzaSize size, Pizza.PizzaDough dough, Pizza.PizzaSauce sauce, ObservableCollection<Topping> toppings, decimal price)
        {
            if (PizzaList.FirstOrDefault(x => x.PizzaID == pizzaID) == null || PizzaList.Count == 0)
            {
                PizzaList.Add(new Pizza(pizzaID, name, size, dough, sauce, toppings, price));
            }
            else
            {
                throw new Exception("Pizza with that ID already exists");
            }

        }
        public static void CreatePizza(int pizzaID, string name, Pizza.PizzaSize size, Pizza.PizzaDough dough, Pizza.PizzaSauce sauce)
        {
            if (PizzaList.FirstOrDefault(x => x.PizzaID == pizzaID) == null || PizzaList.Count == 0)
            {
                PizzaList.Add(new Pizza(pizzaID, name, size, dough, sauce));
            }
            else
            {
                throw new Exception("Pizza with that ID already exists");
            }

        }
        //Reads a Pizza by ID
        public static Pizza ReadPizza(int pizzaID)
        {
            if (PizzaList.First(x => x.PizzaID == pizzaID) == null)
            {
                throw new Exception("Pizza with that ID doesn't exists");
            }
            else
            {
                return PizzaList.FirstOrDefault(x => x.PizzaID == pizzaID);
            }

        }

        //returns the index of the Pizza instead of the object
        public static int ReadPizzaByIndex(int pizzaID)
        {
            if (PizzaList.First(x => x.PizzaID == pizzaID) == null)
            {
                throw new Exception("Pizza with that ID doesn't exists");
            }
            else
            {
                return PizzaList.IndexOf(ReadPizza(pizzaID));
            }

        }

        //Overrides old data with new data
        public static void UpdatePizza(int pizzaID, string name, Pizza.PizzaSize size, Pizza.PizzaDough dough, Pizza.PizzaSauce sauce, ObservableCollection<Topping> toppings, decimal price)
        {
            Pizza pizza = ReadPizza(pizzaID);
            pizza.Name = name;
            pizza.Toppings = toppings;
            pizza.Size = size;
            pizza.Dough = dough;
            pizza.Sauce = sauce;
            pizza.Price = price;
        }
        //Overload version of the update, with no price
        public static void UpdatePizza(int pizzaID, string name, Pizza.PizzaSize size, Pizza.PizzaDough dough, Pizza.PizzaSauce sauce, ObservableCollection<Topping> toppings)
        {
            Pizza pizza = ReadPizza(pizzaID);
            pizza.Name = name;
            pizza.Toppings = toppings;
            pizza.Size = size;
            pizza.Dough = dough;
            pizza.Sauce = sauce;
        }
        public static void UpdatePizza(int pizzaID, Pizza.PizzaSize size, decimal price)
        {
            ReadPizza(pizzaID).Size = size;
            ReadPizza(pizzaID).Price = price;
        }
        //Removes a Pizza by ID
        public static void RemovePizza(int pizzaID)
        {
            PizzaList.Remove(ReadPizza(pizzaID));
        }
    }
}
