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

namespace PharamarcyService.Pages.Client
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private User _user;
        public ProfilePage(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(TxtBoxLastName.Text)) error += "Введите фамилию\n";
            if (string.IsNullOrWhiteSpace(TxtBoxFirstName.Text)) error += "Введите имя\n";
            if (PassBoxPassword.Password != PassBoxRePassword.Password) error += "Пароли не совпадают";

            if (error.Length != 0)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(PassBoxPassword.Password))
                _user.Password = PassBoxPassword.Password;
            _user.LastName = TxtBoxLastName.Text;
            _user.FirstName = TxtBoxFirstName.Text;
            _user.Patronymic = TxtBoxPatronymic.Text;

            AppData.Context.SaveChanges();
            MessageBox.Show("Данные успешно изменены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Отменить изменения", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                NavigationService.GoBack();
        }
    }
}
