using ATG.Sandbox.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATG.Sandbox.Service
{
    public class OrderService : IOrderService
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private IOrderRepository orderRepository;
        private IQueueService queueService;

        public OrderService(IOrderRepository orderRepository, IQueueService queueService)
        {
            this.orderRepository = orderRepository;
            this.queueService = queueService;

            var factory = new ConnectionFactory() { HostName = "35.199.98.99", Port = 5672 };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

        }
        public void Save(Order order)
        {
            try
            {
               orderRepository.Save(order);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddInQueue(Order order)
        {
            try
            {
                Save(order);
                queueService.AddOrderInQueue(order);
                Save(order);
                                
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<Order> GetAll()
        {
            return orderRepository.GetAll();
        }


    }
}


