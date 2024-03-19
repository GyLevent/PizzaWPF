using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzakDoga
{
    public partial class Window1 : Window
    {
        public Pizza SelectedPizza { get; set; }
        public int SelectedSize { get; set; }
        public int Quantity { get; set; }

        public ObservableCollection<Pizza> Pizzak { get; set; }

        public Window1()
        {
            InitializeComponent();
            DataContext = this;
            LoadPizzasFromFile("pizzak.txt");
            pizzaListView.ItemsSource = Pizzak;
        }


        private void LoadPizzasFromFile(string filePath)
        {
            Pizzak = new ObservableCollection<Pizza>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 4)
                    {
                        Pizzak.Add(new Pizza
                        {
                            Nev = parts[0],
                            KisPizzaAr = int.Parse(parts[1]),
                            NagyPizzaAr = int.Parse(parts[2]),
                            Darabszam = int.Parse(parts[3])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az adatok beolvasása során: " + ex.Message);
            }
        }

        private void pizzaListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pizzaListView.SelectedItem != null)
            {
                SelectedPizza = (Pizza)pizzaListView.SelectedItem; 
                MessageBox.Show($"Kiválasztott pizza: {SelectedPizza.Nev}, KisPizzaAr: {SelectedPizza.KisPizzaAr}, NagyPizzaAr: {SelectedPizza.NagyPizzaAr}, Darabszám: {SelectedPizza.Darabszam}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPizza == null)
            {
                MessageBox.Show("Kérem válasszon ki egy pizzát!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedSize == 0)
            {
                MessageBox.Show("Kérem válasszon méretet!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Quantity <= 0)
            {
                MessageBox.Show("A darabszámnak pozitív egész számnak kell lennie!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal price = SelectedSize == 1 ? SelectedPizza.KisPizzaAr : SelectedPizza.NagyPizzaAr;
            decimal totalPrice = price * Quantity; 
            OsszArTextBox.Text = totalPrice.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectedPizza = null;
            SelectedSize = 0;
            Quantity = 0;
            OsszArTextBox.Text = "";
        }

        public class Pizza
        {
            public string Nev { get; set; }
            public int KisPizzaAr { get; set; }
            public int NagyPizzaAr { get; set; }
            public int Darabszam { get; set; }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }



}

