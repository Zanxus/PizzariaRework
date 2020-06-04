using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;

namespace PizzariaRework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        BackgroundWorker worker = new BackgroundWorker();

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal discount;

        public decimal Discount
        {
            get { return discount; }
            set {
                if (discount != value)
                {
                    discount = value;
                    OnPropertyChanged();
                }
            }
        }


        private decimal presetDrinkPrice;

        public decimal PresetDrinkPrice
        {
            get { return presetDrinkPrice; }
            set { 
                if (presetDrinkPrice != value) 
                {
                    presetDrinkPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        private decimal pizzaPrice;

        public decimal PizzaPrice
        {
            get { return pizzaPrice; }
            set {
                if (pizzaPrice != value)
                {
                    pizzaPrice = value;
                    OnPropertyChanged();
                }
                }
        }

        private decimal totalPrice;

        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged();
                }
            }
        }




        public MainWindow()
        {
            DataContext = this;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync(1000);
            InitializeComponent();
            Setup();


            //Normal Test Pizzas 
            //shows Enum selection in a combo box
            //TestBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaSize)).Cast<Pizza.PizzaSize>();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int waitTime = (int)e.Argument;
            while (true)
            {
                Thread.Sleep(waitTime);
                ObservableCollection<Pizza> temp = PizzaController.PizzaList;
                App.Current.Dispatcher.Invoke(delegate
                {
                    PresetDrinkPrice = Drink.DispalyPrice((Drink.DrinkSize)DrinkSizeBox.SelectedItem);
                    PizzaPrice = DisplayPizzaPrice();
                    Discount = PizzaController.DiscountCheck();
                    TotalPrice = PizzaController.OrderPrice;
                });

            }
        }

        //does inital setup
        private void Setup()
        {
            PizzaController.CreatePizza(1, "Margherita", Pizza.PizzaSize.Normal, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping> {new Topping("Cheese"), new Topping("Oregano") });
            PizzaController.CreatePizza(2, "Vesuvio", Pizza.PizzaSize.Familiy, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping> {new Topping("Cheese"), new Topping("Oregano"), new Topping("Ham") });
            PizzaController.CreatePizza(3, "Capricciosa", Pizza.PizzaSize.Normal, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping> {new Topping("Cheese"), new Topping("Oregano"), new Topping("Ham"), new Topping("Mushrooms")});
            PizzaController.CreatePizza(4, "Make Your Own", Pizza.PizzaSize.Normal, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping>());
            UpdateComboBoxes(PresetCombobox);
            Cart.ItemsSource = PizzaController.OrderList;
        }


        //Goes through all the code needed to update all the comboxes based on The selected Pizza
        private void UpdateComboBoxes(ComboBox comboBox)
        {
            comboBox.ItemsSource = PizzaController.PizzaList;

            if (PresetCombobox.SelectedIndex == -1)
            {
                PresetCombobox.SelectedIndex = 0;
            }

            //Makes it so the enum Name is shown instead of the type
            SizeBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaSize)).Cast<Pizza.PizzaSize>();
            SizeBox.SelectedItem = PizzaController.PizzaList[comboBox.SelectedIndex].Size;
            SauceBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaSauce)).Cast<Pizza.PizzaSauce>();
            SauceBox.SelectedItem = PizzaController.PizzaList[comboBox.SelectedIndex].Sauce;
            DoughBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaDough)).Cast<Pizza.PizzaDough>();
            DoughBox.SelectedItem = PizzaController.PizzaList[comboBox.SelectedIndex].Dough;

            DrinkPreset.ItemsSource = PizzaController.DrinkList;
            DrinkSizeBox.ItemsSource = Enum.GetValues(typeof(Drink.DrinkSize)).Cast<Drink.DrinkSize>();

            //assigns Topping selection
            ToppingBox4.ItemsSource = PizzaController.ToppingList;
            ToppingBox3.ItemsSource = PizzaController.ToppingList;
            ToppingBox2.ItemsSource = PizzaController.ToppingList;
            ToppingBox1.ItemsSource = PizzaController.ToppingList;


            //Resets Toppings selections to nothing
            ToppingBox1.SelectedIndex = -1;
            ToppingBox2.SelectedIndex = -1;
            ToppingBox3.SelectedIndex = -1;
            ToppingBox4.SelectedIndex = -1;

            if (PizzaController.PizzaList[comboBox.SelectedIndex].Toppings.Count == 4)
            {
                
                ToppingBox4.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[comboBox.SelectedIndex].Toppings[3].Name);

            }
            if (PizzaController.PizzaList[comboBox.SelectedIndex].Toppings.Count >= 3)
            {

                ToppingBox3.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[comboBox.SelectedIndex].Toppings[2].Name);

            }
            if (PizzaController.PizzaList[comboBox.SelectedIndex].Toppings.Count >= 2)
            {
                ToppingBox2.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[comboBox.SelectedIndex].Toppings[1].Name);

            }
            if (PizzaController.PizzaList[comboBox.SelectedIndex].Toppings.Count >= 1)
            {
                ToppingBox1.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[comboBox.SelectedIndex].Toppings[0].Name);
            }
        }

        private void PresetCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            UpdateComboBoxes(comboBox);
        }

        private void BuyPizza_Button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Topping> toppings = new ObservableCollection<Topping>();

            if (ToppingBox1.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox1.SelectedItem);
            }
            if (ToppingBox2.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox2.SelectedItem);
            }
            if (ToppingBox3.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox3.SelectedItem);
            }
            if (ToppingBox4.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox4.SelectedItem);
            }

            PizzaController.OrderList.Add(new Pizza(PizzaController.OrderList.Count, PresetCombobox.Text, (Pizza.PizzaSize)SizeBox.SelectedItem, (Pizza.PizzaDough)DoughBox.SelectedItem, (Pizza.PizzaSauce)SauceBox.SelectedItem, toppings));
        }

        public decimal DisplayPizzaPrice()
        {
            ObservableCollection<Topping> toppings = new ObservableCollection<Topping>();

            if (ToppingBox1.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox1.SelectedItem);
            }
            if (ToppingBox2.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox2.SelectedItem);
            }
            if (ToppingBox3.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox3.SelectedItem);
            }
            if (ToppingBox4.SelectedIndex != -1)
            {
                toppings.Add((Topping)ToppingBox4.SelectedItem);
            }

            Pizza pizza = new Pizza(PizzaController.OrderList.Count, PresetCombobox.Text, (Pizza.PizzaSize)SizeBox.SelectedItem, (Pizza.PizzaDough)DoughBox.SelectedItem, (Pizza.PizzaSauce)SauceBox.SelectedItem, toppings);
            return pizza.CalculatePrice();
        }

        private void BuyDrink_Button_Click(object sender, RoutedEventArgs e)
        {
            PizzaController.OrderList.Add(new Drink(DrinkPreset.Text, (Drink.DrinkSize)DrinkSizeBox.SelectedItem));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<IOrderable> temp = new ObservableCollection<IOrderable>(PizzaController.OrderList);
            foreach (IOrderable item in temp)
            {
                if (Cart.SelectedItems.Contains(item))
                {
                    PizzaController.OrderList.Remove(item);
                }
            }
        }
    }
}
