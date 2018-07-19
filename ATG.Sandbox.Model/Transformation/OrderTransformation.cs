using ATG.Sandbox.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Sandbox.Model
{
    public class OrderTransformation
    {
        public static OrderQueueModel TransformOrderInQueueModel(Order order)
        {
            return new OrderQueueModel()
            {
                Id = order.Id,
                Price = order.Price,
                Quantity = order.Quantity,
                Side = order.Side,
                Symbol = order.Symbol                
                
            };

        }
        public static OrderResultModel TransformOrderInModel(Order order)
        {
            return new OrderResultModel()
            {
                Id = order.Id,
                Price = order.Price,
                Quantity = order.Quantity,
                Side = order.Side.ToUpper() == "BUY" ? "Compra" : "Venda" ,
                Symbol = order.Symbol,
                Status = order.Status

            };

        }

        public static Order TransformOrderResultModelInDomain(OrderQueueModel order)
        {
            return new Order()
            {
                Id = order.Id,
                Price = order.Price,
                Quantity = order.Quantity,
                Side = order.Side,
                Symbol = order.Symbol                
            };
        }
        public static IEnumerable<OrderResultModel> TransformOrdersInModels(IEnumerable<Order> orders)
        {
            var listReturn = new List<OrderResultModel>();
            foreach (var order in orders)
            {
                listReturn.Add(TransformOrderInModel(order));
            }
            return listReturn;
        }
    }
}
