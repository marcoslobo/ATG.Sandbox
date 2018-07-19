using ATG.Sandbox.Model;
using ATG.Sandbox.Service;
using System;

namespace ATG.Sandbox.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            QueueService queueService = new QueueService();
            var order = new Order() { Id = 13, Quantity = 1, Side = "sell", Symbol = "BRL" };
            queueService.IncludeInQueue(order);

            Console.Read();
        }
    }
}
