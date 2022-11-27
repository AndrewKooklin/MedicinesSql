using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedicineSql
{
    /// <summary>
    /// Логика взаимодействия для AddMedicine.xaml
    /// </summary>
    public partial class AddMedicine : Window
    {
        public Medicine Medicine { get; private set; }
        public AddMedicine(Medicine medicine)
        {
            InitializeComponent();

            Medicine = medicine;

            DataContext = Medicine;
        }

        private void Add_Medicine(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
