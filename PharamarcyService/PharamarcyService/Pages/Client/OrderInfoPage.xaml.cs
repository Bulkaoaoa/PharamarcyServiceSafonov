using mshtml;
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
    /// Interaction logic for OrderInfoPage.xaml
    /// </summary>
    public partial class OrderInfoPage : Page
    {
        private Order _order;
        private StringBuilder _builder;
        public OrderInfoPage(Order order)
        {
            InitializeComponent();
            _order = order;
            Load();
        }

        private void Load()
        {
            _builder = new StringBuilder();

            _builder.Append("<html>");
            _builder.Append("<meta charset=\"utf-8\"/>");
            _builder.Append("<body>");

            _builder.Append($"<h1 align=\"center\"> <b>Счёт</b> </h1>");
            _builder.Append($"<h3> <b>Всего записей: </b>{_order.TotalCount}</h3>");

            _builder.Append("<table width=\"100%\" align=\"center\" border=\"1\">");

            _builder.Append("<tr>");

            _builder.Append("<td align=\"center\"> <b>Номер</b> </td>");
            _builder.Append("<td align=\"center\"> <b>Товар</b> </td>");
            _builder.Append("<td align=\"center\"> <b>Количество</b> </td>");
            _builder.Append("<td align=\"center\"> <b>Цена за шт</b> </td>");

            _builder.Append("</tr>");
            int i = 0;
            foreach(var item in _order.UserOrderProduct)
            {
                _builder.Append("<tr>");
                _builder.Append($"<td align=\"center\"> <b>{++i}</b> </td>");
                _builder.Append($"<td align=\"center\"> <b>{item.Product.Name}</b> </td>");
                _builder.Append($"<td align=\"center\"> <b>{item.Count}</b> </td>");
                _builder.Append($"<td align=\"center\"> <b>{item.Product.Price}</b> </td>");
                _builder.Append("</tr>");
            }
            _builder.Append("</table>");

            _builder.Append($"<h3 align=\"right\"> <b>Итого: </b>{_order.TotalPrice}</h3>");
            _builder.Append($"<h3 align=\"right\"> <b>Клиент: </b>{_order.User.FIO}</h3>");

            _builder.Append("</body>");
            _builder.Append("</html>");

            WBDoc.NavigateToString(_builder.ToString());
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            IHTMLDocument2 document = WBDoc.Document as IHTMLDocument2;
            document.execCommand("Print");
            BtnBack_Click(null, null);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.GoBack();
        }
    }
}
