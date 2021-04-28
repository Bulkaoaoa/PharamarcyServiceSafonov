using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharamarcyService.Entities
{
    public partial class Order
    {
        public List<Product> ProductsOfOrder
        {
            get
            {
                List<Product> listOfProducts = new List<Product>();
                var listofUserOrderOfProduct = AppData.Context.UserOrderProduct.ToList().Where(p => p.OrderId == Id).ToList();
                foreach (var item in listofUserOrderOfProduct.ToList())
                {
                    listOfProducts.Add(item.Product);
                }
                return listOfProducts;
            }
        }
    }
}
