﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainSupplierPage.xaml
    /// </summary>
    public partial class MainSupplierPage : Page
    {
        public MainSupplierPage()
        {
            InitializeComponent();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Supplier.AddProductPage());
        }
    }
}
