using ATG.Sandbox.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ATG.Sandbox.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public DbSet<Order> entities;
        private readonly PersistentContext context;
        public OrderRepository(PersistentContext context)
        {
            this.context = context;
            this.entities = context.Orders;
        }
        public IEnumerable<Order> GetAll()
        {
            return entities.ToList();
        }
        public void Save(Order entity)
        {
            if (entity.Id > 0)
                entities.Update(entity);
            else entities.Add(entity);
            context.SaveChanges();
        }
        
    }
}
