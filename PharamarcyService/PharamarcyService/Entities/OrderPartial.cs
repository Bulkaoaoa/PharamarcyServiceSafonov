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

        public string TotalProducts
        {
            get
            {
                return $"Корзина ({TotalCount})";
            }
        }

        public int TotalCount
        {
            get
            {
                return UserOrderProduct.ToList().Sum(i => i.Count);
            }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal price = 0;
                foreach (var item in UserOrderProduct)
                    price += item.Count * item.Product.Price;

                return price;
            }
        }
    }
}
