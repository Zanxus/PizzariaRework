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

namespace PizzariaRework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        public MainWindow()
        {
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
                    //if (!icPizzaList.ItemsSource.Equals(temp))
                    //{
                    //    icPizzaList.ItemsSource = temp;
                    //}
                });

            }
        }


        private void Setup()
        {
            PizzaController.CreatePizza(1, "Margherita", Pizza.PizzaSize.Normal, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping> {new Topping("Cheese"), new Topping("Oregano") }, 60);
            PizzaController.CreatePizza(2, "Vesuvio", Pizza.PizzaSize.Normal, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping> {new Topping("Cheese"), new Topping("Oregano"), new Topping("Ham") }, 60);
            PizzaController.CreatePizza(3, "Capricciosa", Pizza.PizzaSize.Normal, Pizza.PizzaDough.Wheat, Pizza.PizzaSauce.Tomato, new ObservableCollection<Topping> {new Topping("Cheese"), new Topping("Oregano"), new Topping("Ham"), new Topping("Mushrooms"), new Topping("Shrimp") }, 70);

            PresetCombobox.ItemsSource = PizzaController.PizzaList;
            
            SizeBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaSize)).Cast<Pizza.PizzaSize>();
            SizeBox.SelectedItem = PizzaController.PizzaList[PresetCombobox.SelectedIndex].Size;
            SauceBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaSauce)).Cast<Pizza.PizzaSauce>();
            SauceBox.SelectedItem = PizzaController.PizzaList[PresetCombobox.SelectedIndex].Sauce;
            DoughBox.ItemsSource = Enum.GetValues(typeof(Pizza.PizzaDough)).Cast<Pizza.PizzaDough>();
            DoughBox.SelectedItem = PizzaController.PizzaList[PresetCombobox.SelectedIndex].Dough;

            ToppingBox4.ItemsSource = PizzaController.ToppingList;
            ToppingBox3.ItemsSource = PizzaController.ToppingList;
            ToppingBox2.ItemsSource = PizzaController.ToppingList;
            ToppingBox1.ItemsSource = PizzaController.ToppingList;

            if (PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings.Count == 4)
            {

                ToppingBox2.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings[3].Name);

            }
            if (PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings.Count >= 3)
            {

                ToppingBox2.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings[2].Name);

            }
            if (PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings.Count >= 2)
            {
                ToppingBox2.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings[1].Name);

            }
            if (PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings.Count >= 1)
            {
                ToppingBox1.SelectedItem = PizzaController.ToppingList.FirstOrDefault(t => t.Name == PizzaController.PizzaList[PresetCombobox.SelectedIndex].Toppings[0].Name);
            }



            
        }
    }
}
