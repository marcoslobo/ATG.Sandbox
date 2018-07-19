using ATG.Sandbox.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Sandbox.Service
{
    public interface IOrderService
    {
        void Save(Order order);
        IEnumerable<Order> GetAll();
        void AddInQueue(Order order);
    }
}
