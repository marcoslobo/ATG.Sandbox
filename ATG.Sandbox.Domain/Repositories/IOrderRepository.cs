using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Sandbox.Domain
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        void Save(Order entity);
    }
}
