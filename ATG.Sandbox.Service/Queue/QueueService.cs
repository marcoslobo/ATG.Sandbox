using ATG.Sandbox.Domain;
using ATG.Sandbox.Model;
using Microsoft.Extensions.Configuration;
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
    public class QueueService : IQueueService
    {
        
        private  string replyQueueName;
        private  EventingBasicConsumer consumer;
        private IConfiguration configuration;
        
        
        public QueueService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void AddOrderInQueue(Order order)
        {
            try
            {

                using (IConnection connection = new ConnectionFactory() { HostName = configuration.GetSection("QueueAddress").Value, Port = int.Parse(configuration.GetSection("QueuePort").Value.ToString()) }.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        replyQueueName = channel.QueueDeclare().QueueName;
                        consumer = new EventingBasicConsumer(channel);


                        var tcs = new TaskCompletionSource<string>();
                        var resultTask = tcs.Task;

                        var correlationId = Guid.NewGuid().ToString();

                        IBasicProperties props = channel.CreateBasicProperties();
                        props.CorrelationId = correlationId;
                        props.ReplyTo = replyQueueName;


                        EventHandler<BasicDeliverEventArgs> handler = null;
                        handler = (model, ea) =>
                        {
                            if (ea.BasicProperties.CorrelationId == correlationId)
                            {
                                consumer.Received -= handler;

                                var body = ea.Body;
                                var response = Encoding.UTF8.GetString(body);

                                tcs.SetResult(response);
                            }
                        };
                        consumer.Received += handler;

                        String jsonified = JsonConvert.SerializeObject(OrderTransformation.TransformOrderInQueueModel(order));
                        byte[] orderBuffer = Encoding.UTF8.GetBytes(jsonified);
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: configuration.GetSection("QueueName").Value,
                            basicProperties: props,
                            body: orderBuffer);


                        channel.BasicConsume(
                            consumer: consumer,
                            queue: replyQueueName,
                            autoAck: true);


                        var result = JsonConvert.DeserializeObject<QueueResult>(resultTask.Result);


                        if (result.Status == true)
                            order.Status = "Processado!";
                        else order.Status = result.Msgs[0].ToString();
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

      


    }
}


