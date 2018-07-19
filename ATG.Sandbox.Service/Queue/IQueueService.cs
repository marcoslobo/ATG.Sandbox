using ATG.Sandbox.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Sandbox.Service
{
    public interface IQueueService
    {
        void AddOrderInQueue(Order order);
      
    }
}
