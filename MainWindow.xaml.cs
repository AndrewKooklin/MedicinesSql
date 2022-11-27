using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace MedicineSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db = new AppContext();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Database.EnsureCreated();

            db.Medicines.Load();

            DataContext = db.Medicines.Local.ToObservableCollection();
        }

        //private async void LoadMedicinesFromDb()
        //{
        //    using (var db = new AppContext())
        //    {
        //        //MedicineItems.Items.Clear();
        //        var medicines = await db.Medicines.ToListAsync();
        //        foreach (var medicine in medicines)
        //        {
        //            MedicineItems.Items.Add(medicine);
        //        }
        //        Process.Content = $"Loaded medicines: {medicines.Count}";
        //    }
        //}

        //private void Load_Medicine(object sender, RoutedEventArgs e)
        //{
        //    db.Medicines.Load();
        //}

        private void Add_Medicine(object sender, RoutedEventArgs e)
        {
            AddMedicine AddMedicineWindow = new AddMedicine(new Medicine());

            AddMedicineWindow.BtnDialogWindow.Content = "Add";

            if (AddMedicineWindow.ShowDialog() == true)
            {
                Medicine Medicine = AddMedicineWindow.Medicine;
                db.Medicines.Add(Medicine);
                db.SaveChanges();
            }
        }

        private void Edit_Medicine(object sender, RoutedEventArgs e)
        {
            Medicine medicine = MedicineItems.SelectedItem as Medicine;

            if (medicine is null) return;

            AddMedicine AddMedicineWindow = new AddMedicine(new Medicine
            {
                Id = medicine.Id,
                NameMedicine = medicine.NameMedicine,
                PriceMedicine = medicine.PriceMedicine
            });

            AddMedicineWindow.BtnDialogWindow.Content = "Change";

            if (AddMedicineWindow.ShowDialog() == true)
            {
                

                medicine = db.Medicines.Find(AddMedicineWindow.Medicine.Id);
                if (medicine != null)
                {
                    medicine.NameMedicine = AddMedicineWindow.Medicine.NameMedicine;
                    medicine.PriceMedicine = AddMedicineWindow.Medicine.PriceMedicine;
                    db.SaveChanges();
                    MedicineItems.Items.Refresh();
                }
            }
        }

        private void Delete_Medicine(object sender, RoutedEventArgs e)
        {
            Medicine? medicine = MedicineItems.SelectedItem as Medicine;
            if (medicine is null) return;
            db.Medicines.Remove(medicine);
            db.SaveChanges();
        }

        
    }
}
