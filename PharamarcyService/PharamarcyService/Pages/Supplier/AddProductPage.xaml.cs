﻿using Microsoft.Win32;
using PharamarcyService.Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PharamarcyService.Pages.Supplier
{
    /// <summary>
    /// Interaction logic for AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private Product _currProduct;
        private byte[] _selectedImg = null;

        public AddProductPage()
        {
            InitializeComponent();
            CmbBoxManufacturer.ItemsSource = AppData.Context.Manufacturer.ToList();
            CmbBoxSupplier.ItemsSource = AppData.Context.Supplier.ToList();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TxtBoxName.Text)) errors += "Вы не ввели название\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxPrice.Text)) errors += "Вы не ввели цену\r\n";
            if (CmbBoxManufacturer.SelectedItem == null) errors += "Вы не выбрали производителя\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxComposition.Text)) errors += "Вы не ввели состав\r\n";
            decimal price = 0;
            try { price = decimal.Parse(TxtBoxPrice.Text); } catch { errors += "Вы ввели неккоректные значения цены\r\n"; }
            if (price <= 0) errors += "Вы не можете ввести отрицательные или нулевые значения для цены\r\n";

            if (errors.Length == 0)
            {
                if (_currProduct == null)
                {
                    var newProduct = new Product()
                    {
                        Name = TxtBoxName.Text,
                        Price = price,
                        ManufacturerId = (CmbBoxManufacturer.SelectedItem as Manufacturer).Id,
                        Composition = TxtBoxComposition.Text,
                        Description = TxtBoxDescription.Text.Trim() == "" ? null : TxtBoxDescription.Text,
                        Indications = TxtBoxIndications.Text.Trim() == "" ? null : TxtBoxIndications.Text,
                        ContrIndications = TxtBoxContrIndications.Text.Trim() == "" ? null : TxtBoxContrIndications.Text,
                        Image = _selectedImg,
                        IsArchived = false,
                        SupplierId = (CmbBoxSupplier.SelectedItem as Entities.Supplier).Id,
                    };
                    AppData.Context.Product.Add(newProduct);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно добавили новый продукт", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            else
                MessageBox.Show(errors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnSelectImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image files | *.jpg; *.png; *.jpeg; *.jpe; *.jfif"
            };
            if (ofd.ShowDialog() == true)
            {
                _selectedImg = File.ReadAllBytes(ofd.FileName);
                ImgOfProduct.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
