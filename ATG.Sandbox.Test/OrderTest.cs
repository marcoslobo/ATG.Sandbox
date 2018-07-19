using ATG.Sandbox.Domain;
using ATG.Sandbox.Repository;
using ATG.Sandbox.Service;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Text;
using Xunit;

namespace ATG.Sandbox.Test
{
    public class OrderTest
    {
        [Fact]
        public void OrderSave()
        {
            var options = new DbContextOptionsBuilder<PersistentContext>()
                .UseInMemoryDatabase(databaseName: "order_test")
                .Options;

            // Run the test against one instance of the context
            var context = new PersistentContext(options);
            var orderRepository = new OrderRepository(context);
            var queueService = new QueueService();

                OrderService orderService = new OrderService(orderRepository, queueService);


            var order = new Order() { Id = 32322, Quantity = 2, Side = "buy", Symbol = "PETR4", Price = 11 };
            queueService.AddOrderInQueue(order);
        }
    }
}
