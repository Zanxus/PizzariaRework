using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaRework
{
    public class Drink : IOrderable
    {
        public string Name { get; set; }
        public DrinkSize Size { get; set; }

        private decimal price;
        public decimal Price
        {
            get { return CalculatePrice(); }
            set { price = value; }
        }

        public enum DrinkSize
        {
            Small,Medium,Large
        }
        public Drink(string name, DrinkSize size)
        {
            Name = name;
            Size = size;
        }
        public decimal CalculatePrice()
        {

            if (Size == DrinkSize.Small)
            {
                return 18;
            }
            if (Size == DrinkSize.Medium)
            {
                return 25;
            }
            if (Size == DrinkSize.Large)
            {
                return 40;
            }
            return default;
        }
        public static decimal DispalyPrice(DrinkSize drinkSize)
        {

            if (drinkSize == DrinkSize.Small)
            {
                return 18;
            }
            if (drinkSize == DrinkSize.Medium)
            {
                return 25;
            }
            if (drinkSize == DrinkSize.Large)
            {
                return 40;
            }
            return default;
        }
    }
}
