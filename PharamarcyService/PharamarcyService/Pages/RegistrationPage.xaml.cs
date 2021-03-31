using PharamarcyService.Entities;
using System;
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

namespace PharamarcyService.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            CmbBoxCountry.ItemsSource = AppData.Context.Country.ToList();
        }


        private void BtnRegisterClient_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TxtBoxFirstName.Text)) errors += "Вы не ввели имя\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxLastName.Text)) errors += "Вы не ввели фамилию\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxLogin.Text)) errors += "Вы не ввели логин\r\n";
            if (string.IsNullOrWhiteSpace(PassBoxPass.Password)) errors += "Вы не ввели пароль\r\n";
            if (AppData.Context.User.ToList().Where(p => p.Login == TxtBoxLogin.Text).FirstOrDefault() != null) errors += "Пользователь с таким логином уже существует";

            if (ChkBoxSupplier.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(TxtBoxSupplierName.Text)) errors += "Вы не ввели название организации\r\n";
                if (string.IsNullOrWhiteSpace(TxtBoxINN.Text)) errors += "Вы не ввели ИИН\r\n";
                if (string.IsNullOrWhiteSpace(TxtBoxORGN.Text)) errors += "Вы не ввели ОРГН\r\n";
                if (CmbBoxCountry.SelectedItem == null) errors += "Вы не выбрали страну\r\n";
            }

            if (errors.Length == 0)
            {
                var newUser = new User()
                {
                    FirstName = TxtBoxFirstName.Text,
                    LastName = TxtBoxLastName.Text,
                    Patronymic = TxtBoxPatronymic.Text == "" ? null : TxtBoxPatronymic.Text,
                    Login = TxtBoxLogin.Text,
                    Password = PassBoxPass.Password,
                    RoleId = ChkBoxSupplier.IsChecked == true ? 2 : 1,
                };
                AppData.Context.User.Add(newUser);
                AppData.Context.SaveChanges();

                if (ChkBoxSupplier.IsChecked == true)
                {
                    var newSupplier = new Entities.Supplier()
                    {
                        Id = newUser.Id,
                        Name = TxtBoxSupplierName.Text,
                        INN = TxtBoxINN.Text,
                        ORGN = TxtBoxORGN.Text,
                        CountryId = (CmbBoxCountry.SelectedItem as Country).Id
                    };
                    AppData.Context.Supplier.Add(newSupplier);
                    AppData.Context.SaveChanges();
                }

                MessageBox.Show("Вы успешо зарегистрировались", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            else
                MessageBox.Show(errors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }



        private void ChkBoxSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (ChkBoxSupplier.IsChecked == true)
                StackPanelForSupplier.Visibility = Visibility.Visible;
            else
                StackPanelForSupplier.Visibility = Visibility.Hidden;
        }
    }
}
